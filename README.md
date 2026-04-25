# ArchipelagoULTRAKILL

## What is this?

[Archipelago](https://archipelago.gg) is a multiworld, multi-game randomizer. By connecting to an Archipelago server, any of the [supported games](https://archipelago.gg/games) can play together in a multiworld.

## What does randomization do to ULTRAKILL?

All weapons and arms are randomized. Extra abilities can also be randomized, including the ability to dash, walljump, slide, slam, and use a weapon's alternate fire.

The revolver, shotgun, and nailgun can start in either their standard or alternate forms, and the other form must be found before it can be used.

Items are found by collecting secrets, and optionally by completing level challenges, getting Perfect Ranks, and more.

Levels are unlocked by finding access to them within the multiworld. Levels can either be unlocked individually, or whole layers at once.

One level must be chosen as a goal for the randomizer, and a number of levels must be completed to unlock the goal. Secret missions, Prime Sanctums, and Encore levels will also count.

Individual levels can be excluded, which will prevent them from being considered in logic in any way.

Levels will be unlocked for all difficulties, and you can switch difficulties at any time.

Skulls can be randomized, meaning they will not appear in any levels until found in the multiworld.

Note that the logic for the randomizer assumes that the player is willing to take intentional damage to get items.

Because there are far fewer items than locations in ULTRAKILL, there are many filler and trap items added to make the item and location amounts equal:

- +10,000P: Instantly gives 10,000P.
- Overheal: Sets the player's health to 200.
- Dual Wield: Gain an additional copy of the currently held weapon for 15 seconds.
- Infinite Stamina: Dash around infinitely with no cooldown for 15 seconds.
- Air Jump: Use your wall jumps as additional midair jumps for 15 seconds.
- Soap: Spawns soap in the player's hand, or on the ground at the player's position if already holding something.
- Hard Damage: Instantly take 75 hard damage.
- Stamina Limiter: Removes all dashes for 15 seconds.
- Wall Jump Limiter: Removes all wall jumps for 15 seconds.
- Weapon Malfunction: Removes all weapon ammo and prevents cooldowns, such as marksman coins, nailgun ammo, and railcannon charge for 15 seconds.
- Radiant Aura: All enemies are radiant for 15 seconds.
- Confusing Aura: All enemies will ignore the player and attack each other for 15 seconds.
- Quick Charge: Weapon ammo and cooldowns recharge at 3x speed for 15 seconds.
- Hands-Free Mode: All arms cannot be used for 15 seconds.

# Setup

### Creating a `.yaml` file

To play an Archipelago game, you will need a `.yaml` file for the game you would like to play.

1. Download and install [Archipelago](https://github.com/ArchipelagoMW/Archipelago/releases).

2. Download `ultrakill.apworld` from the mod's [releases page](https://github.com/TRPG0/ArchipelagoULTRAKILL/releases) and install it, either by double clicking the `.apworld` file, opening the Archipelago Launcher and selecting "Install APWorld", or by selecting "Browse Files" and adding it to the `custom_worlds` folder.

3. To create a `.yaml` file, open the Archipelago Launcher and search for "Options Creator". You can then customize all of the options for ULTRAKILL, and then save the `.yaml` file.

	- Alternatively, if you would prefer to manually edit the `.yaml` file in a text editor, you can click "Generate Template Options" in the Archipelago Launcher instead.

### Generating a multiworld

When you have collected all `.yaml` files for each player in the multiworld, move them all into the `Players` folder. You can easily find the Players folder by opening the Archipelago Launcher and clicking "Browse Files".

Note that only one player in the multiworld will need to generate. If the player who is hosting is not yourself, they will also need to install the apworld for ULTRAKILL.

Once all `.yaml` files are ready, click "Generate" in the launcher. A command prompt window will open, and if successful it will close itself. Your generated multiworld will appear in the `output` folder.

You can host the game by [uploading](https://archipelago.gg/uploads) it to the Archipelago website and clicking Create New Room. The address you will need to connect to the server will be displayed at the top of the room page.

- You can also host the game yourself by clicking "Host" in the launcher. You will need to know how to port forward if you want other players to be able to connect to a manually hosted server.
	
### Connecting to a server

Download and install the Archipelago mod with a mod manager such as [r2modman](https://github.com/ebkr/r2modmanPlus/releases) or [Gale](https://github.com/Kesomannen/gale/releases), or manually by extracting the contents of the zip file into the `plugins` folder. If you decide to install manually, you will also need to install [PluginConfigurator](https://github.com/eternalUnion/UKPluginConfigurator).

Optionally, you can also download NoTutorial ([GitHub](https://github.com/TRPG0/UK-NoTutorial) / [Thunderstore](https://thunderstore.io/c/ultrakill/p/TRPG/NoTutorial/)) to avoid accidentally entering the tutorial when making new save files.

1. If connecting for the first time, select a new save slot. Do not complete the tutorial - it will set flags on the save slot that will prevent you from connecting.

2. Open the options menu, click the PLUGIN CONFIG button, click Configure next to Archipelago, and open the PLAYER SETTINGS menu.

3. Enter your name, the server's address as `address:port`, and a password if necessary, then click the Connect button.

There are some additional commands that can be used by pressing `F8` to open the console:

- `connect [address:port] [player] [password]` - Connect to an Archipelago server.
- `disconnect`: Disconnect from an Archipelago server.
- `say [message]`: Send messages or commands to the server. Does not work if not currently connected.

# Can I play ULTRAKILL randomizer without Archipelago?

Maybe later. I would like to make a standalone version of the randomizer eventually, but since the Archipelago version already works well enough, I'm releasing it now.

# Something's not working!

If you have trouble setting up ULTRAKILL randomizer or have found any bugs that need fixing, feel free to join the [Archipelago](https://discord.gg/archipelago) Discord server and ask about it in the `#ultrakill` channel.