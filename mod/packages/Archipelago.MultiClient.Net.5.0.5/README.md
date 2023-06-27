# Archipelago.MultiClient.Net

A client library for use with .NET based applications for interfacing with Archipelago hosts. This client conforms with the latest stable [Archipelago Network Protocol Specification](https://github.com/ArchipelagoMW/Archipelago/blob/main/docs/network%20protocol.md).

# Documentation

## Create Session Instance

```csharp
var session = ArchipelagoSessionFactory.CreateSession("localhost", 38281);

// alternatively...
var session = ArchipelagoSessionFactory.CreateSession(new Uri("ws://localhost:38281"));
var session = ArchipelagoSessionFactory.CreateSession("localhost:38281");
var session = ArchipelagoSessionFactory.CreateSession("localhost");
```

The freshly created `ArchipelagoSession` object is the entrypoint for all incoming and outgoing interactions with the server. Keep it in scope for at least the lifetime of the connection. If the room changes ports, or the user needs to connect to a different room, then a new session needs to be created at the new host and port.

## Connect to Room

Connect to a server at a specific room slot using the following method:

```csharp
LoginResult TryConnectAndLogin(
        string game, // Name of the game implemented by this client, SHOULD match what is used in the world implementation
        string name, // Name of the slot to connect as (a.k.a player name)
        ItemsHandlingFlags itemsHandlingFlags, /* One of the following (see AP docs for details):
                NoItems
                RemoteItems
                IncludeOwnItems
                IncludeStartingInventory
                AllItems
            */
        Version version = null, // Minimum Archipelago world specification version which this client can successfuly interface with
        string[] tags = null, /* One of the following (see AP docs for details)
                "DeathLink"
                "Tracker"
                "TextOnly"
            */
        string uuid = null, // Unique identifier for this player/client, if null randomly generated
        string password = null, // Password that was set when the room was created
	bool requestSlotData = true // If the LoginResult should contain the slot data
    );
```

For example,

```csharp
LoginResult result = session.TryConnectAndLogin("Risk of Rain 2", "Ijwu", ItemsHandlingFlags.AllItems);
```

Would attempt to connect to a password-less room at the slot `Ijwu`, and report the game `Risk of Rain 2` with a minimum apworld version of `v2.1.0`.

Once connected, you have access to a suite of helper objects which provide an interface for sending/receiving information with the server.

### Example Connection

```csharp
private static void Connect(string server, string user, string pass)
{
    LoginResult result;

    try
    {
        // handle TryConnectAndLogin attempt here and save the returned object to `result`
    }
    catch (Exception e)
    {
        result = new LoginFailure(e.GetBaseException().Message);
    }

    if (!result.Successful)
    {
        LoginFailure failure = (LoginFailure)result;
        string errorMessage = $"Failed to Connect to {server} as {user}:";
        foreach (string error in failure.Errors)
        {
            errorMessage += $"\n    {error}";
        }
        foreach (ConnectionRefusedError error in failure.ErrorCodes)
        {
            errorMessage += $"\n    {error}";
        }

        return; // Did not connect, show the user the contents of `errorMessage`
    }
    
    // Successfully connected, `ArchipelagoSession` (assume statically defined as `session` from now on) can now be used to interact with the server and the returned `LoginSuccessful` contains some useful information about the initial connection (e.g. a copy of the slot data as `loginSuccess.SlotData`)
    var loginSuccess = (LoginSuccessful)result;
}
```

If using .net 4.0 or higher, you can use `ConnectAsync` and `LoginAsync` to prevent hitching for injection-based implementations like harmony.

## Helper Overview

```csharp
session.Socket         // Payload-agnostic interface for sending/receving the most basic transmission units between client/server
session.Items          // Helpers for handling receipt of items
session.Locations      // Helpers for reporting visited locations to the servers
session.Players        // Helpers for translating player number, alias, name etc.
session.DataStorage    // Helpers for reading/writing globally accessible to any client connected in the room
session.ConnectionInfo // Helpers for reading/handling useful information on the current connection
session.RoomState      // Information on the state of the room
session.MessageLog     // Interface for the server to push info messages to the user
```

## Players

The player helper provides methods for accessing details about the other players currently connected to the Archipelago session.

### Get All Player Names

```csharp
var sortedPlayerNames = session.Players.AllPlayers.Select(x => x.Name).OrderBy(x => x);
```

### Get Current Player Name

```csharp
string playerName = session.Players.GetPlayerAliasAndName(session.ConnectionInfo.Slot);
```

## Locations

### Report Collected Location(s)

Call the following method to inform the server of locations whose items have been "found", and therefore should be distributed (if neccessary):

```csharp
// Report multiple at once
session.Locations.CompleteLocationChecks(new []{1,3,8});

// Or report one at a time
session.Locations.CompleteLocationChecks(3);
```

The location ID used is of that defined in the AP world.

### Location ID <--> Name

```csharp
string locationName = session.Locations.GetLocationNameFromId(42) ?? $"Location: {locationId}";
long locationId = session.Locations.GetLocationIdFromName(locationName);
```

### Scout Location Checks

Scouting means asking the server what is stored in a specific location *without* collecting it:

```csharp
session.Locations.ScoutLocationsAsync(locationInfoPacket => Console.WriteLine(locationInfoPacket.Locations.Count));
```

## Items

### Item ID --> Name

```csharp
string itemName = session.Items.GetItemName(88) ?? $"Item: {itemId}";
```

### Access Received Items

At any time, you can access the current inventory for the active session/slot via the `Items` helper like so:

```csharp
foreach(NetworkItem item in session.Items.AllItemsReceived)
{
    long itemId = item.Item;
}
```

*Note: The list of received items will never shrink and the collection is guaranteed to be in the order that the server sent the items. Because of this, it is safe to assume that if the size of this collection has changed it's because new items were received and appended to the end of the collection.*

### Received Item Callback Handler (Asynchronous)

```csharp
// Must go AFTER a successful connection attempt
session.Items.ItemReceived += (receivedItemsHelper) => {
	var itemReceivedName = receivedItemsHelper.PeekItemName() ?? $"Item: {itemId}";

	// ... Handle item receipt here

	receivedItemsHelper.DequeueItem();
};
```

*Note: This callback event will occur for every item on connection and reconnection. Whether or not it includes the first batch of remote items depends on your `ItemHandlingFlags` when connecting.*

## RoomState

The roomstate helper provides access to values that represent the current state of the multiworld room, with information such as the cost of a hint and or your current accumulated amount of hint point or the permissions for things like forfeiting

```csharp
Console.WriteLine($"You have {session.RoomState.HintPoints}, and need {session.RoomState.HintCost} for a hint");
```

## ConnectionInfo

The conection info helper provides access to values under which you are currently connected, such as your slot number or your currently used tags and item handling flags

```csharp
Console.WriteLine($"You are connected on slot {session.ConnectionInfo.Slot}, on team {session.ConnectionInfo.Team}");
```

## ArchipelagoSocket

The socket helper is a lower level API allowing for direct access to the socket which the session object uses to communicate with the Archipelago server. You may use this object to hook onto when messages are received or you may use it to send any packets defined in the library. Various events are exposed to allow for receipt of errors or notifying of socket close.

```csharp
session.Socket.SendPacket(new SayPacket(){Text = "Woof woof!"});
```

## DeathLink

DeathLink support is included in the library. You may enable it by using the `CreateDeathLinkService` in the `DeathLinkProvider` class, and the `EnableDeathlink` method on the service. Deathlink can be toggled on an off by the the `EnableDeathlink` and `DisableDeathlink` methods on the service

```csharp
var session = ArchipelagoSessionFactory.CreateSession("localhost", 38281);

var deathLinkService = session.CreateDeathLinkService().EnableDeathlink();
session.TryConnectAndLogin("Risk of Rain 2", "Ijwu", ItemsHandlingFlags.AllItems);

deathLinkService.OnDeathLinkReceived += (deathLinkObject) => {
	// ... Kill your player(s).
};

// ... On death:
deathLinkService.SendDeathLink(new DeathLink("Ijwu", "Died to exposure."));
```

## DataStorage

DataStorage support is included in the library. You may save values on the archipelago server in order to share them across other player's sessions or to simply keep track of values outside of your game's state.

The DataStorage provides an interface based on keys and their scope. By assigning a value to a key, that value is stored on the server and by reading from a key a value is retrieved from the server. 
Assigning and reading values from the store can be done using simple assignments `=`:
* `= session.DataStorage["Key"]`, read value from the data storage synchronously
* `session.DataStorage["Key"] =`, write value to the data storage asynchronously
* Complex objects need to be stored and retrieved in the form of a `JObject`, therefor you must wrap them into a `JObject.FromObject()`

The DataStorage also provides methods to retrieve the value of a key asynchronously using `[key].GetAsync`. 
To set the initial value of a key without overriding any existing value the `[key].Initialize` method can be used.
If you're interested in keeping track of when a value of a certain key is changed by any client you can use the `[key].OnValueChanged` handler to register a callback for when the value gets updated.

Mathematical operations on values stored on the server are supported using the following operators:
* `+`, Add right value to left value
* `-`, Subtract right value from left value
* `*`, Multiply left value by right value
* `/`, Divide left value by right value
* `%`, Gets remainder after dividing left value by right value
* `^`, Multiply left value by the power of the right value

Bitwise operations on values stored on the server are supported using the following opperations:
* `+ Bitwise.Xor(x)`, apply logical exclusive OR to the left value using value x
* `+ Bitwise.Or(x)`, apply logical OR to the left value using value x
* `+ Bitwise.And(x)`, apply logical AND to the left value using value x
* `+ Bitwise.LeftShift(x)`, binary shift the left value to the left by x
* `+ Bitwise.RightShift(x)`, binary shift the left value to the right by x

Other operations on values stored on the server are supported using the following opperations:
* `+ Operation.Min(x)`, get the lowest value of the left value and x
* `+ Operation.Max(x)`, get the highest value of the left value and x
* `+ Operation.Remove(x)`, when the left value is a list, removes the first element with value x
* `+ Operation.Pop(x)`, when the left value is a list or dictionary, removes the element at index x or key x
* `+ Operation.Update(x)`, when the left value is a list or dictionary, updates the dictionary with the keys/values in x

Operation specific callbacks are supported, these get called only once with the results of the current operation:
* `+ Callback.Add((oldValue, newValue) => {});`, calls this method after your operation or chain of operations are proccesed by the server

Mathematical operations, bitwise operations and callbacks can be chained, given the extended syntax with `()` around each operation.

Examples:
```csharp
var session = ArchipelagoSessionFactory.CreateSession("localhost", 38281);
session.TryConnectAndLogin("Timespinner", "Jarno", ItemsHandlingFlags.AllItems);

//Initializing
session.DataStorage["B"].Initialize(20); //Set initial value for B in global scope if it has no value assigned yet

//Storing/Updating
session.DataStorage[Scope.Slot, "SetPersonal"] = 20; //Set `SetPersonal` to 20, in scope of the current connected user\slot
session.DataStorage[Scope.Global, "SetGlobal"] = 30; //Set `SetGlobal` to 30, in global scope shared among all players (the default scope is global)
session.DataStorage["Add"] += 50; //Add 50 to the current value of `Add`
session.DataStorage["Divide"] /= 2; //Divide current value of `Divide` in half
session.DataStorage["Max"] += + Operation.Max(80); //Set `Max` to 80 if the stored value is lower than 80
session.DataStorage["Dictionary"] = JObject.FromObject(new Dictionary<string, int>()); //Set `Dictionary` to a Dictionary
session.DataStorage["SetObject"] = JObject.FromObject(new SomeClassOrStruct()); //Set `SetObject` to a custom object
session.DataStorage["BitShiftLeft"] += Bitwise.LeftShift(1); //Bitshift current value of `BitShiftLeft` to left by 1
session.DataStorage["Xor"] += Bitwise.Xor(0xFF); //Modify `Xor` using the Bitwise exclusive or operation
session.DataStorage["DifferentKey"] = session.DataStorage["A"] - 30; //Get value of `A`, Assign it to `DifferentKey` and then subtract 30
session.DataStorage["Array"] = new []{ "One", "Two" }; //Arrays can be stored directly, List's needs to be converted ToArray() first 
session.DataStorage["Array"] += new []{ "Three" }; //Append array values to existing array on the server

//Chaining operations
session.DataStorage["Min"] = (session.DataStorage["Min"] + 40) + Operation.Min(100); //Add 40 to `Min`, then Set `Min` to 100 if `Min` is bigger than 100
session.DataStorage["C"] = ((session.DataStorage["C"] - 6) + Bitwise.RightShift(1)) ^ 3; //Subtract 6 from `C`, then multiply `C` by 2 using bitshifting, then take `C` to the power of 3

//Update callbacks
//EnergyLink deplete pattern, subtract 50, then set value to 0 if its lower than 0
session.DataStorage["EnergyLink"] = ((session.DataStorage["EnergyLink"] - 50) + Operation.Min(0)) + Callback.Add((oldData, newData) => {
    var actualDepleted = (float)newData - (float)oldData; //calculate the actual change, might differ if there was less than 50 left on the server
});

//Keeping track of changes
session.DataStorage["OnChangeHandler"].OnValueChanged += (oldData, newData) => {
	var changed = (int)newData - (int)oldData; //Keep track of changes made to `OnChangeHandler` by any client, and calculate the difference
};

//Keeping track of change (but for more complex data structures)
session.DataStorage["OnChangeHandler"].OnValueChanged += (oldData, newData) => {
    var old_dict = oldData.ToObject<Dictionary<int, int>>();
    var new_dict = newData.ToObject<Dictionary<int, int>>();
};

//Retrieving
session.DataStorage["Async"].GetAsync<string>(s => { string r = s }); //Retrieve value of `Async` asynchronously
float f = session.DataStorage["Float"]; //Retrieve value for `Float` synchronously and store it as a float
var d = session.DataStorage["DateTime"].To<DateTime>() //Retrieve value for `DateTime` as a DateTime struct
var array = session.DataStorage["Strings"].To<string[]>() //Retrieve value for `Strings` as string Array

//Handling anonymous object, if the target type is not known you can use `To<JObject>()` and use its interface to access the members
session.DataStorage["Anonymous"] = JObject.FromObject(new { Number = 10, Text = "Hello" }); //Set `Anonymous` to an anonymous object
var obj = session.DataStorage["Anonymous"].To<JObject>(); //Retrieve value for `Anonymous` where an anonymous object was stored
var number = (int)obj["Number"]; //Get value for anonymous object key `Number`
var text = (string)obj["Text"]; //Get value for anonymous object key `Text`

```

## Message Logging

The Archipelago server can send messages to client to be displayed on screen as sort of a log, this is done by handling the `PrintJsonPacket` packets. This library simplifies this process into a single handler for you to handle.
```csharp
var session = ArchipelagoSessionFactory.CreateSession("localhost", 38281);
session.MessageLog.OnMessageReceived += OnMessageReceived;
session.TryConnectAndLogin("Timespinner", "Jarno", ItemsHandlingFlags.AllItems, new Version(0,3,5));

static void OnMessageReceived(LogMessage message)
{
	DisplayOnScreen(message.ToString());
}
```

In some cased you might want extra information that is provided by the server in such cases you can use type checking

```csharp
static void OnMessageReceived(LogMessage message)
{
	switch (message)
	{
		case ItemHintLogMessage hintLogMessage:
			var receiver = itemSendLogMessage.Receiver;
			var sender = itemSendLogMessage.Sender;
			var networkItem = itemSendLogMessage.Item;
			var found = hintLogMessage.IsFound;
			break;
		case ItemSendLogMessage itemSendLogMessage: 
			var receiver = itemSendLogMessage.Receiver;
			var sender = itemSendLogMessage.Sender;
			var networkItem = itemSendLogMessage.Item;
			break;
	}
	DisplayOnScreen(message.ToString());
}
```

If you want more control over how the message is displayed, like for example you might want to color certain parts of the message,
Then you can use the `Parts` property. This returns each part of the message in order of appearnce with the `Text` to be displayed and also the `Color` it would normally be diplayed in.
If `IsBackgroundColor` is true, then the color should be applied to the message background instead.
The MessagePart message can also contain additional information that can be retrieved by type checking.


```csharp
foreach (part in message.Parts)
{
	switch (part)
	{
		case ItemMessagePart itemMessagePart: 
			var itemId = itemMessagePart.ItemId;
			var flags = itemMessagePart.Flags;
			break;
		case LocationMessagePart locationMessagePart:
			var locationId = locationMessagePart.LocationId;
			break;
		case PlayerMessagePart playerMessagePart:
			var slotId = playerMessagePart.SlotId;
			var isCurrentPlayer = playerMessagePart.IsActivePlayer;
			break;
	}

	DisplayOnScreen(part.Text, part.Color, part.IsBackgroundColor);
}
```

## Send Completion

You can report the completion of the player's goal like so:

```csharp
public static void send_completion()
{
    var statusUpdatePacket = new StatusUpdatePacket();
    statusUpdatePacket.Status = ArchipelagoClientState.ClientGoal;
    Session.Socket.SendPacket(statusUpdatePacket);
}
```


