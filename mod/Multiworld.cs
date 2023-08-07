using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ArchipelagoULTRAKILL.Structures;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace ArchipelagoULTRAKILL
{
    public static class Multiworld
    {
        public static int[] AP_VERSION = new int[] { 0, 4, 1 };
        public static DeathLinkService DeathLinkService = null;
        public static bool DeathLinkKilling = false;

        public static bool Authenticated;
        public static ArchipelagoSession Session;
        public static List<string> messages = new List<string>();

        public static bool Connect()
        {
            if (Authenticated)
            {
                return true;
            }

            string url = Core.data.host_name;
            int port = 38281;

            if (url.Contains(":"))
            {
                var splits = url.Split(new char[] { ':' });
                url = splits[0];
                if (!int.TryParse(splits[1], out port)) port = 38281;
            }

            Session = ArchipelagoSessionFactory.CreateSession(url, port);
            Session.Socket.SocketClosed += SocketClosed;
            Session.Socket.ErrorReceived += ErrorReceived;
            Session.Socket.PacketReceived += PacketReceived;
            Session.Items.ItemReceived += ItemReceived;

            LoginResult loginResult = Session.TryConnectAndLogin(
                "ULTRAKILL",
                Core.data.slot_name,
                ItemsHandlingFlags.IncludeStartingInventory,
                new Version(AP_VERSION[0], AP_VERSION[1], AP_VERSION[2]),
                null,
                null,
                Core.data.password == "" ? null : Core.data.password,
                true
                );

            if (loginResult is LoginSuccessful success)
            {
                Authenticated = true;

                ConfigManager.isConnected.value = true;
                ConfigManager.playerName.interactable = false;
                ConfigManager.serverAddress.interactable = false;
                ConfigManager.serverPassword.interactable = false;

                switch (int.Parse(success.SlotData["goal"].ToString()))
                {
                    case 0:
                        Core.data.goal = "1-4";
                        break;
                    case 1:
                        Core.data.goal = "2-4";
                        break;
                    case 2:
                        Core.data.goal = "3-2";
                        break;
                    case 3:
                        Core.data.goal = "4-4";
                        break;
                    case 4:
                        Core.data.goal = "5-4";
                        break;
                    case 6:
                        Core.data.goal = "P-1";
                        break;
                    case 7:
                        Core.data.goal = "P-2";
                        break;
                    case 5:
                    default:
                        Core.data.goal = "6-2";
                        break;
                }

                Core.data.goalRequirement = int.Parse(success.SlotData["goal_requirement"].ToString());
                Core.data.challengeRewards = bool.Parse(success.SlotData["challenge_rewards"].ToString());
                Core.data.pRankRewards = bool.Parse(success.SlotData["p_rank_rewards"].ToString());
                Core.data.fishRewards = bool.Parse(success.SlotData["fish_rewards"].ToString());
                Core.data.randomizeFire2 = bool.Parse(success.SlotData["randomize_secondary_fire"].ToString());
                Core.data.randomizeSkulls = bool.Parse(success.SlotData["randomize_skulls"].ToString());

                if (Core.data.randomizeSkulls && UIManager.skullIcons.Count == 0) UIManager.CreateSkullIcons();

                if (!Core.DataExists())
                {
                    Core.data.hasArm = bool.Parse(success.SlotData["start_with_arm"].ToString());
                    Core.data.dashes = int.Parse(success.SlotData["starting_stamina"].ToString());
                    Core.data.walljumps = int.Parse(success.SlotData["starting_walljumps"].ToString());
                    Core.data.canSlide = bool.Parse(success.SlotData["start_with_slide"].ToString());
                    Core.data.canSlam = bool.Parse(success.SlotData["start_with_slam"].ToString());
                    Core.data.multiplier = int.Parse(success.SlotData["point_multiplier"].ToString());
                    Core.data.musicRandomizer = bool.Parse(success.SlotData["music_randomizer"].ToString());
                    if (Core.data.musicRandomizer) Core.data.music = JsonConvert.DeserializeObject<Dictionary<string, string>>(success.SlotData["music"].ToString());

                    PrefsManager.Instance.SetInt("difficulty", 3);
                    PrefsManager.Instance.SetInt("weapon.arm0", 1);
                    GameProgressSaver.SetIntro(true);
                    GameProgressSaver.SetTutorial(true);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Revolver);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Shotgun);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Nailgun);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Railcannon);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.RocketLauncher);
                    GameProgressSaver.SaveProgress(26);
                    GameProgressSaver.SetPrime(1, 1);
                    GameProgressSaver.SetPrime(2, 1);
                    Core.SaveData();
                }

                //if (!Core.data.hasArm) PrefsManager.Instance.SetInt("weapon.arm0", 0);
                //else PrefsManager.Instance.SetInt("weapon.arm0", 1);

                LocationManager.locations.Clear();
                LocationManager.ukitems.Clear();
                LocationManager.apitems.Clear();

                foreach (RawLocationData data in ((JArray)success.SlotData["locations"]).ToObject<RawLocationData[]>())
                {
                    LocationManager.locations.Add(data.id, new UKLocation() 
                    {
                        id = data.id,
                        ap_id = data.ap_id,
                        ukitem = data.ukitem,
                        @checked = Core.data.@checked.Contains(data.id)
                    });

                    if (data.ukitem)
                    {
                        LocationManager.ukitems.Add(data.id, new UKItem()
                        {
                            item_name = data.item_name,
                            type = (Enums.UKItemType)Enum.Parse(typeof(Enums.UKItemType), data.item_type),
                            player_name = data.player_name
                        });
                    }
                    else
                    {
                        string flag = "None";
                        if (data.item_type == "useful") flag = "NeverExclude";
                        else if (data.item_type == "progression") flag = "Advancement";
                        else if (data.item_type == "trap") flag = "Trap";
                        LocationManager.apitems.Add(data.id, new APItem()
                        {
                            item_name = data.item_name,
                            type = (ItemFlags)Enum.Parse(typeof(ItemFlags), flag),
                            player_name = data.player_name
                        });
                    }
                }

                if (Core.DataExists())
                {
                    if (Core.data.@checked.Count > 0)
                    {
                        foreach (string loc in Core.data.@checked)
                        {
                            LocationManager.CheckLocation(loc);
                        }
                    }
                }

                ConfigManager.LoadStats();
                Core.logger.LogInfo("Successfully connected to server as player \"" + Core.data.slot_name + "\".");
                ConfigManager.connectionInfo.text = "Successfully connected to server as player \"" + Core.data.slot_name + "\".";
                UIManager.menuIcon.GetComponent<Image>().color = LocationManager.colors["green"];
                string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
                UIManager.menuText.GetComponent<Text>().text = "Archipelago\n" + Core.ModVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n" + Core.data.@checked.Count + "/" + totalLocations;
            }
            else if (loginResult is LoginFailure loginFailure)
            {
                Authenticated = false;
                //GameConsole.Console.Instance.PrintLine(String.Join("\n", loginFailure.Errors));
                Core.logger.LogError(String.Join("\n", loginFailure.Errors));
                ConfigManager.connectionInfo.text = String.Join("\n", loginFailure.Errors);
                Session.Socket.DisconnectAsync();
                Session = null;
                UIManager.menuIcon.GetComponent<Image>().color = LocationManager.colors["red"];
            }
            return loginResult.Successful;
        }

        public static void Disconnect()
        {
            ConfigManager.isConnected.value = false;
            ConfigManager.playerName.interactable = true;
            ConfigManager.serverAddress.interactable = true;
            ConfigManager.serverPassword.interactable = true;
            if (Session != null && Session.Socket != null) Session.Socket.DisconnectAsync();
            if (SceneHelper.CurrentScene == "Main Menu") UIManager.menuIcon.GetComponent<Image>().color = LocationManager.colors["red"];
            //GameConsole.Console.Instance.PrintLine("Disconnected from Archipelago server.");
            //Debug.Log("Disconnected from Archipelago server.");
            UIManager.log.GetComponent<Text>().text = "";
            messages.Clear();
            Session = null;
            Authenticated = false;
        }

        public static void SocketClosed(string reason)
        {
            Core.logger.LogError("Lost connection to Archipelago server. " + reason);
            Disconnect();
        }

        public static void ErrorReceived(Exception e, string message)
        {
            Core.logger.LogError(message);
            if (e != null) Core.logger.LogError(e.ToString());
            Disconnect();
        }

        public static void PacketReceived(ArchipelagoPacketBase packet)
        {
            switch (packet.PacketType)
            {
                case ArchipelagoPacketType.PrintJSON:
                    {
                        if (messages.Count >= UIManager.lines) messages.RemoveAt(0);

                        var p = packet as PrintJsonPacket;

                        string richText = "";
                        string plainText = "";
                        string color = "<color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.colors["white"]) + "FF>";

                        foreach (var messagePart in p.Data)
                        {
                            switch (messagePart.Type)
                            {
                                case JsonMessagePartType.PlayerId:
                                    if (Session.Players.GetPlayerName(int.Parse(messagePart.Text)) == Core.data.slot_name) color = "<color=#" + ColorUtility.ToHtmlStringRGB(ConfigManager.APPlayerSelf.value) + "FF>";
                                    else color = "<color=#" + ColorUtility.ToHtmlStringRGB(ConfigManager.APPlayerOther.value) + "FF>";
                                    if (int.TryParse(messagePart.Text, out int playerSlot))
                                    {
                                        string playerName = Session.Players.GetPlayerAlias(playerSlot) ?? $"Slot: {playerSlot}";
                                        richText += color + playerName + "</color>";
                                        plainText += playerName;
                                    }
                                    else
                                    {
                                        richText += $"{color}{messagePart.Text}</color>";
                                        plainText += messagePart.Text;
                                    }
                                    break;
                                case JsonMessagePartType.ItemId:
                                    switch (messagePart.Flags)
                                    {
                                        case ItemFlags.Advancement:
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(ConfigManager.APItemAdvancement.value) + "FF>";
                                            break;
                                        case ItemFlags.NeverExclude:
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(ConfigManager.APItemNeverExclude.value) + "FF>";
                                            break;
                                        case ItemFlags.Trap:
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(ConfigManager.APItemTrap.value) + "FF>";
                                            break;
                                        default:
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(ConfigManager.APItemFiller.value) + "FF>";
                                            break;
                                    }
                                    if (int.TryParse(messagePart.Text, out int itemId))
                                    {
                                        string itemName = Session.Items.GetItemName(itemId) ?? $"Item: {itemId}";
                                        richText += color + itemName + "</color>";
                                        plainText += itemName;
                                    }
                                    else
                                    {
                                        richText += $"{color}{messagePart.Text}</color>";
                                        plainText += messagePart.Text;
                                    }
                                    break;
                                case JsonMessagePartType.LocationId:
                                    color = "<color=#" + ColorUtility.ToHtmlStringRGB(ConfigManager.APLocation.value) + "FF>";
                                    if (int.TryParse(messagePart.Text, out int locationId))
                                    {
                                        string locationName = Session.Locations.GetLocationNameFromId(locationId) ?? $"Location: {locationId}";
                                        richText += color + locationName + "</color>";
                                        plainText += locationName;
                                    }
                                    else
                                    {
                                        richText += $"{color}{messagePart.Text}</color>";
                                        plainText += messagePart.Text;
                                    }
                                    break;
                                default: 
                                    richText += messagePart.Text;
                                    plainText += messagePart.Text;
                                    break;
                            }
                        }
                        messages.Add(richText);
                        //GameConsole.Console.Instance.PrintLine("[AP] " + plainText);
                        UIManager.log.GetComponent<Text>().text = string.Join("\n", messages.ToArray());
                        break;
                    }
            }
        }

        public static void ItemReceived(ReceivedItemsHelper helper)
        {
            if (helper.Index > Core.data.index)
            {
                Core.logger.LogInfo(helper.PeekItem().Player);
                string player = (Session.Players.GetPlayerAlias(helper.PeekItem().Player) == "") ? "?" : Session.Players.GetPlayerAlias(helper.PeekItem().Player);
                Core.logger.LogInfo("Name: \"" + helper.PeekItemName() + "\" | Type: " + LocationManager.GetTypeFromName(helper.PeekItemName()) + " | Player: \"" + player + "\"");

                UKItem item = new UKItem()
                {
                    item_name = helper.PeekItemName(),
                    type = LocationManager.GetTypeFromName(helper.PeekItemName()),
                    player_name = Core.data.slot_name
                };

                LocationManager.tempItems.Add(new KeyValuePair<string, UKItem>(player, item));
                //LocationManager.GetUKItem(item, player);

                Core.data.index++;
            }
            helper.DequeueItem();
        }

        public static void ToggleDeathLink()
        {

        }

        public static void DeathLinkRecieved()
        {

        }

        public static void SendCompletion()
        {
            var packet = new StatusUpdatePacket() { Status = ArchipelagoClientState.ClientGoal };
            Session.Socket.SendPacket(packet);
        }
    }
}
