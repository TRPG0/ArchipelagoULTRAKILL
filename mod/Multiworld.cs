using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using System;
using System.Collections.Generic;
using UnityEngine;
using ArchipelagoULTRAKILL.Structures;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace ArchipelagoULTRAKILL
{
    public static class Multiworld
    {
        public static int[] AP_VERSION = new int[] { 0, 4, 4 };
        public static DeathLinkService DeathLinkService = null;
        public static bool DeathLinkKilling = false;

        public static bool Authenticated;
        public static bool HintMode = false;
        public static ArchipelagoSession Session;
        public static List<string> messages = new List<string>();

        public static void TryGetGoal(ref string goal, Dictionary<string, object> slotData, string defaultValue)
        {
            try
            {
                switch (int.Parse(slotData["goal"].ToString()))
                {
                    case 0:
                        goal = "1-4";
                        break;
                    case 1:
                        goal = "2-4";
                        break;
                    case 2:
                        goal = "3-2";
                        break;
                    case 3:
                        goal = "4-4";
                        break;
                    case 4:
                        goal = "5-4";
                        break;
                    case 6:
                        goal = "P-1";
                        break;
                    case 7:
                        goal = "P-2";
                        break;
                    case 8:
                        goal = "7-4";
                        break;
                    case 5:
                    default:
                        goal = "6-2";
                        break;
                }
            }
            catch (Exception)
            {
                Core.Logger.LogError($"Couldn't parse goal. Something has gone very wrong. Using default value ({defaultValue})");
                goal = defaultValue;
            }
        }

        public static void TryGetSlotDataValue(ref int option, Dictionary<string, object> slotData, string key, int defaultValue)
        {
            try { option = int.Parse(slotData[key].ToString()); }
            catch (KeyNotFoundException) 
            {
                Core.Logger.LogWarning($"No key found for option \"{key}\". Using default value ({defaultValue})");
                option = defaultValue; 
            }
        }

        public static void TryGetSlotDataValue(ref bool option, Dictionary<string, object> slotData, string key, bool defaultValue)
        {
            try { option = bool.Parse(slotData[key].ToString()); }
            catch (KeyNotFoundException) 
            {
                Core.Logger.LogWarning($"No key found for option \"{key}\". Using default value ({defaultValue})");
                option = defaultValue; 
            }
        }

        public static void TryGetSlotDataValue(ref BossOptions option, Dictionary<string, object> slotData, string key, BossOptions defaultValue)
        {
            try { option = (BossOptions)int.Parse(slotData[key].ToString()); }
            catch (KeyNotFoundException) 
            {
                Core.Logger.LogWarning($"No key found for option \"{key}\". Using default value ({defaultValue})");
                option = defaultValue; 
            }
        }

        public static bool Connect()
        {
            if (Authenticated)
            {
                return true;
            }

            Session = ArchipelagoSessionFactory.CreateSession(Core.data.host_name);
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
                ConfigManager.hintMode.interactable = false;
                ConfigManager.chat.interactable = true;

                TryGetGoal(ref Core.data.goal, success.SlotData, "6-2");

                TryGetSlotDataValue(ref Core.data.goalRequirement, success.SlotData, "goal_requirement", 15);
                TryGetSlotDataValue(ref Core.data.bossRewards, success.SlotData, "boss_rewards", BossOptions.Disabled);
                TryGetSlotDataValue(ref Core.data.challengeRewards, success.SlotData, "challenge_rewards", false);
                TryGetSlotDataValue(ref Core.data.pRankRewards, success.SlotData, "p_rank_rewards", false);
                TryGetSlotDataValue(ref Core.data.fishRewards, success.SlotData, "fish_rewards", false);
                TryGetSlotDataValue(ref Core.data.randomizeFire2, success.SlotData, "randomize_secondary_fire", false);
                TryGetSlotDataValue(ref Core.data.randomizeSkulls, success.SlotData, "randomize_skulls", false);

                if (Core.data.randomizeSkulls && UIManager.skullIcons.Count == 0) UIManager.CreateSkullIcons();

                if (!Core.DataExists())
                {
                    TryGetSlotDataValue(ref Core.data.hasArm, success.SlotData, "start_with_arm", true);
                    TryGetSlotDataValue(ref Core.data.dashes, success.SlotData, "starting_stamina", 3);
                    TryGetSlotDataValue(ref Core.data.walljumps, success.SlotData, "starting_walljumps", 3);
                    TryGetSlotDataValue(ref Core.data.canSlide, success.SlotData, "start_with_slide", true);
                    TryGetSlotDataValue(ref Core.data.canSlam, success.SlotData, "start_with_slam", true);
                    TryGetSlotDataValue(ref Core.data.multiplier, success.SlotData, "point_multiplier", 1);

                    TryGetSlotDataValue(ref Core.data.musicRandomizer, success.SlotData, "music_randomizer", false);
                    if (Core.data.musicRandomizer) Core.data.music = JsonConvert.DeserializeObject<Dictionary<string, string>>(success.SlotData["music"].ToString());

                    TryGetSlotDataValue(ref Core.data.cybergrindHints, success.SlotData, "cybergrind_hints", true);

                    try { ConfigManager.uiColorRandomizer.value = (ColorOptions)Enum.Parse(typeof(ColorOptions), success.SlotData["ui_color_randomizer"].ToString()); }
                    catch (KeyNotFoundException) { ConfigManager.uiColorRandomizer.value = ColorOptions.Off; }
                    try { ConfigManager.gunColorRandomizer.value = (ColorOptions)Enum.Parse(typeof(ColorOptions), success.SlotData["gun_color_randomizer"].ToString()); }
                    catch (KeyNotFoundException) { ConfigManager.gunColorRandomizer.value = ColorOptions.Off; }

                    PrefsManager.Instance.SetInt("difficulty", 3);
                    PrefsManager.Instance.SetInt("weapon.arm0", 1);
                    GameProgressSaver.SetIntro(true);
                    GameProgressSaver.SetTutorial(true);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Revolver);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Shotgun);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Nailgun);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Railcannon);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.RocketLauncher);
                    GameProgressSaver.SaveProgress(30);
                    GameProgressSaver.SetPrime(1, 1);
                    GameProgressSaver.SetPrime(2, 1);
                    Core.SaveData();

                    if (ConfigManager.uiColorRandomizer.value != ColorOptions.Off) ColorRandomizer.RandomizeUIColors();
                    if (ConfigManager.gunColorRandomizer.value != ColorOptions.Off) ColorRandomizer.RandomizeGunColors();

                    Core.data.deathLink = bool.Parse(success.SlotData["death_link"].ToString());
                }

                if (Core.data.deathLink) EnableDeathLink();

                PrefsManager.Instance.SetInt("weapon.arm0", 1);

                LocationManager.locations.Clear();

                foreach (RawLocationData data in ((JArray)success.SlotData["locations"]).ToObject<RawLocationData[]>())
                {
                    AItem item;
                    if (data.ukitem)
                    {
                        item = new UKItem()
                        {
                            itemName = data.item_name,
                            playerName = data.player_name,
                            type = (UKType)int.Parse(data.item_type)
                        };
                    }
                    else
                    {
                        item = new APItem()
                        {
                            itemName = data.item_name,
                            playerName = data.player_name,
                            type = (ItemFlags)int.Parse(data.item_type)
                        };
                    }

                    LocationManager.locations.Add(data.id, new Location() 
                    {
                        id = data.id,
                        ap_id = data.ap_id,
                        item = item,
                        @checked = Core.data.@checked.Contains(data.id)
                    });
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
                Core.Logger.LogInfo("Successfully connected to server as player \"" + Core.data.slot_name + "\".");
                ConfigManager.connectionInfo.text = "Successfully connected to server as player \"" + Core.data.slot_name + "\".";
                UIManager.menuIcon.GetComponent<Image>().color = Colors.Green;
                string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
                UIManager.menuText.GetComponent<Text>().text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n" + Core.data.@checked.Count + "/" + totalLocations;
            }
            else if (loginResult is LoginFailure failure)
            {
                Authenticated = false;
                //GameConsole.Console.Instance.PrintLine(String.Join("\n", loginFailure.Errors));
                Core.Logger.LogError(String.Join("\n", failure.Errors));
                ConfigManager.connectionInfo.text = String.Join("\n", failure.Errors);
                Session.Socket.DisconnectAsync();
                Session = null;
                UIManager.menuIcon.GetComponent<Image>().color = Colors.Red;
            }
            return loginResult.Successful;
        }

        public static bool ConnectBK()
        {
            if (Authenticated)
            {
                return true;
            }

            Session = ArchipelagoSessionFactory.CreateSession(Core.data.host_name);
            Session.Socket.SocketClosed += SocketClosed;
            Session.Socket.ErrorReceived += ErrorReceived;
            Session.Socket.PacketReceived += PacketReceived;

            LoginResult loginResult = Session.TryConnectAndLogin(
                "",
                Core.data.slot_name,
                ItemsHandlingFlags.NoItems,
                new Version(AP_VERSION[0], AP_VERSION[1], AP_VERSION[2]),
                new string[] { "TextOnly" },
                null,
                Core.data.password == "" ? null : Core.data.password,
                false
                );

            if (loginResult is LoginSuccessful success)
            {
                Authenticated = true;
                HintMode = true;

                ConfigManager.isConnected.value = true;
                ConfigManager.playerName.interactable = false;
                ConfigManager.serverAddress.interactable = false;
                ConfigManager.serverPassword.interactable = false;
                ConfigManager.hintMode.interactable = false;
                ConfigManager.chat.interactable = true;

                Core.Logger.LogInfo("Successfully connected to server in hint mode as player \"" + Core.data.slot_name + "\".");
                ConfigManager.connectionInfo.text = "Successfully connected to server in hint mode as player \"" + Core.data.slot_name + "\".";
                UIManager.menuIcon.GetComponent<Image>().color = Colors.Green;
                UIManager.menuText.GetComponent<Text>().text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\nHint Mode";
            }
            else if (loginResult is LoginFailure failure)
            {
                Authenticated = false;
                HintMode = false;
                //GameConsole.Console.Instance.PrintLine(String.Join("\n", loginFailure.Errors));
                Core.Logger.LogError(String.Join("\n", failure.Errors));
                ConfigManager.connectionInfo.text = String.Join("\n", failure.Errors);
                Session.Socket.DisconnectAsync();
                Session = null;
                UIManager.menuIcon.GetComponent<Image>().color = Colors.Red;
            }
            return loginResult.Successful;
        }

        public static void Disconnect()
        {
            ConfigManager.isConnected.value = false;
            ConfigManager.playerName.interactable = true;
            ConfigManager.serverAddress.interactable = true;
            ConfigManager.serverPassword.interactable = true;
            ConfigManager.hintMode.interactable = true;
            ConfigManager.chat.interactable = false;
            if (Session != null && Session.Socket != null) Session.Socket.DisconnectAsync();
            if (SceneHelper.CurrentScene == "Main Menu") UIManager.menuIcon.GetComponent<Image>().color = Colors.Red;
            //GameConsole.Console.Instance.PrintLine("Disconnected from Archipelago server.");
            //Debug.Log("Disconnected from Archipelago server.");
            UIManager.log.GetComponent<Text>().text = "";
            messages.Clear();
            Session = null;
            DeathLinkService = null;
            Authenticated = false;
            HintMode = false;
        }

        public static void SocketClosed(string reason)
        {
            Core.Logger.LogError("Lost connection to Archipelago server. " + reason);
            Disconnect();
        }

        public static void ErrorReceived(Exception e, string message)
        {
            Core.Logger.LogError(message);
            if (e != null) Core.Logger.LogError(e.ToString());
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
                        string color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.White) + "FF>";

                        foreach (var messagePart in p.Data)
                        {
                            switch (messagePart.Type)
                            {
                                case JsonMessagePartType.PlayerId:
                                    if (Session.Players.GetPlayerName(int.Parse(messagePart.Text)) == Core.data.slot_name) color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.PlayerSelf) + "FF>";
                                    else color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.PlayerOther) + "FF>";
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
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemAdvancement) + "FF>";
                                            break;
                                        case ItemFlags.NeverExclude:
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemNeverExclude) + "FF>";
                                            break;
                                        case ItemFlags.Trap:
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemTrap) + "FF>";
                                            break;
                                        default:
                                            color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemFiller) + "FF>";
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
                                    color = "<color=#" + ColorUtility.ToHtmlStringRGB(Colors.Location) + "FF>";
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
                string name = helper.PeekItemName();
                string player = (Session.Players.GetPlayerAlias(helper.PeekItem().Player) == "") ? "?" : Session.Players.GetPlayerAlias(helper.PeekItem().Player);
                Core.Logger.LogInfo("Name: \"" + name + "\" | Type: " + LocationManager.GetTypeFromName(name) + " | Player: \"" + player + "\"");

                UKItem item = new UKItem()
                {
                    itemName = name,
                    type = LocationManager.GetTypeFromName(name),
                    playerName = Core.data.slot_name
                };

                if (item.type == UKType.Level || item.type == UKType.Layer || item.type == UKType.Skull) LocationManager.GetUKItem(item, player);
                else LocationManager.itemQueue.Add(new KeyValuePair<string, UKItem>(player, item));

                Core.data.index++;
            }
            helper.DequeueItem();
        }

        public static void EnableDeathLink()
        {
            if (DeathLinkService == null)
            {
                DeathLinkService = Session.CreateDeathLinkService();
                DeathLinkService.OnDeathLinkReceived += DeathLinkReceived;
            }
            DeathLinkService.EnableDeathLink();
        }

        public static void DisableDeathLink()
        {
            if (DeathLinkService == null) return;
            else DeathLinkService.DisableDeathLink();
        }

        public static void DeathLinkReceived(DeathLink deathLink)
        {
            if (Core.IsInLevel)
            {
                DeathLinkKilling = true;
                string cause = "{0} has died.";
                if (deathLink.Cause != "") cause = deathLink.Cause;
                else cause = string.Format(cause, deathLink.Source);

                messages.Add($"[DeathLink] {cause}");
                if (Core.uim.deathLinkMessage != null) Core.uim.deathLinkMessage.SetDeathMessage(cause);
                NewMovement.Instance.GetHurt(200, false, 0);
            }
            else Core.Logger.LogWarning("Received DeathLink, but player cannot be killed right now.");
        }

        public static void SendCompletion()
        {
            var packet = new StatusUpdatePacket() { Status = ArchipelagoClientState.ClientGoal };
            Session.Socket.SendPacket(packet);
        }
    }
}
