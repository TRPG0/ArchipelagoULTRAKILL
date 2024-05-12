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
using Archipelago.MultiClient.Net.Models;
using Color = UnityEngine.Color;

namespace ArchipelagoULTRAKILL
{
    public static class Multiworld
    {
        public static int[] AP_VERSION = new int[] { 0, 4, 6 };
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

        public static void TryGetSlotDataValue(ref WeaponForm option, Dictionary<string, object> slotData, string key, WeaponForm defaultValue)
        {
            try { option = (WeaponForm)int.Parse(slotData[key].ToString()); }
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

            Core.data.unlockedSkulls1_4 = 0;
            Core.data.unlockedSkulls5_1 = 0;

            Session = ArchipelagoSessionFactory.CreateSession(Core.data.host_name);
            Session.Socket.SocketClosed += SocketClosed;
            Session.Socket.ErrorReceived += ErrorReceived;
            Session.Socket.PacketReceived += PacketReceived;
            Session.Items.ItemReceived += ItemReceived;

            LoginResult loginResult = Session.TryConnectAndLogin(
                "ULTRAKILL",
                Core.data.slot_name,
                ItemsHandlingFlags.AllItems,
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
                TryGetSlotDataValue(ref Core.data.hankRewards, success.SlotData, "hank_rewards", false);
                TryGetSlotDataValue(ref Core.data.clashReward, success.SlotData, "randomize_clash_mode", false);
                TryGetSlotDataValue(ref Core.data.fishRewards, success.SlotData, "fish_rewards", false);
                TryGetSlotDataValue(ref Core.data.cleanRewards, success.SlotData, "cleaning_rewards", false);
                TryGetSlotDataValue(ref Core.data.chessReward, success.SlotData, "chess_reward", false);
                TryGetSlotDataValue(ref Core.data.rocketReward, success.SlotData, "rocket_race_reward", false);
                TryGetSlotDataValue(ref Core.data.randomizeFire2, success.SlotData, "randomize_secondary_fire", false);
                TryGetSlotDataValue(ref Core.data.revForm, success.SlotData, "revolver_form", WeaponForm.Standard);
                TryGetSlotDataValue(ref Core.data.shoForm, success.SlotData, "shotgun_form", WeaponForm.Standard);
                TryGetSlotDataValue(ref Core.data.naiForm, success.SlotData, "nailgun_form", WeaponForm.Standard);
                TryGetSlotDataValue(ref Core.data.randomizeSkulls, success.SlotData, "randomize_skulls", false);
                TryGetSlotDataValue(ref Core.data.l1switch, success.SlotData, "randomize_limbo_switches", false);
                TryGetSlotDataValue(ref Core.data.l7switch, success.SlotData, "randomize_violence_switches", false);

                if (Core.data.randomizeSkulls && !UIManager.createdSkullIcons) UIManager.CreateMenuSkullIcons();
                if ((Core.data.l1switch || Core.data.l7switch) && !UIManager.createdSwitchIcons) UIManager.CreateMenuSwitchIcons();

                if (!Core.DataExists())
                {
                    if (Core.data.revForm == WeaponForm.Standard)
                    {
                        Core.data.revstd = true;
                        Core.data.revalt = false;
                    }
                    else
                    {
                        Core.data.revstd = false;
                        Core.data.revalt = true;
                        GameProgressSaver.AddGear("revalt");
                    }

                    if (Core.data.shoForm == WeaponForm.Standard)
                    {
                        Core.data.shostd = true;
                        Core.data.shoalt = false;
                    }
                    else
                    {
                        Core.data.shostd = false;
                        Core.data.shoalt = true;
                        GameProgressSaver.AddGear("shoalt");
                    }

                    if (Core.data.naiForm == WeaponForm.Standard)
                    {
                        Core.data.naistd = true;
                        Core.data.naialt = false;
                    }
                    else
                    {
                        Core.data.naistd = false;
                        Core.data.naialt = true;
                        GameProgressSaver.AddGear("naialt");
                    }

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

                    PrefsManager.Instance.SetInt("difficulty", 4);
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

                LocationManager.locations = ((JObject)success.SlotData["locations"]).ToObject<Dictionary<string, long>>();

                ConfigManager.LoadStats();
                Core.Logger.LogInfo("Successfully connected to server as player \"" + Core.data.slot_name + "\".");
                ConfigManager.connectionInfo.text = "Successfully connected to server as player \"" + Core.data.slot_name + "\".";
                UIManager.menuIcon.GetComponent<Image>().color = Colors.Green;
                string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
                UIManager.menuText.text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n" + Core.data.@checked.Count + "/" + totalLocations;

                foreach (string weapon in Core.shopPrices.Keys)
                {
                    LocationInfoPacket info = Session.Locations.ScoutLocationsAsync(false, LocationManager.locations[$"shop_{weapon}"]).Result;
                    long itemId = info.Locations[0].Item;
                    string itemName = Session.Items.GetItemName(itemId);
                    string playerName = Session.Players.GetPlayerName(info.Locations[0].Player);
                    UKType? type = LocationManager.GetTypeFromName(itemName);
                    if (type.HasValue)
                    {
                        LocationManager.shopScouts.Add($"shop_{weapon}", new UKItem()
                        {
                            itemName = itemName,
                            playerName = playerName,
                            type = type.Value
                        });
                    }
                    else
                    {
                        LocationManager.shopScouts.Add($"shop_{weapon}", new APItem()
                        {
                            itemName = itemName,
                            playerName = playerName,
                            type = info.Locations[0].Flags
                        });
                    }
                }

                foreach (string loc in Core.data.@checked)
                {
                    LocationManager.CheckLocation(loc);
                }
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
                UIManager.menuText.text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n<color=yellow>Hint Mode</color>";
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
            UIManager.SetLogText("");
            messages.Clear();
            LocationManager.shopScouts = new Dictionary<string, AItem>();
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

                        if (p.Data[0].Type == JsonMessagePartType.PlayerId
                            && Session.Players.GetPlayerName(int.Parse(p.Data[0].Text)) == Core.data.slot_name
                            && p.Data[1].Text == " sent ")
                        {
                            string itemName = Session.Items.GetItemName(long.Parse(p.Data[2].Text));
                            string forPlayer = Session.Players.GetPlayerAlias(int.Parse(p.Data[4].Text));
                            Color messageColor = Color.white;

                            UKType? type = LocationManager.GetTypeFromName(itemName);
                            if (type.HasValue)
                            {
                                messageColor = LocationManager.GetUKMessageColor(itemName);
                                LocationManager.messages.Add(new Message()
                                {
                                    image = LocationManager.GetUKMessageImage(itemName),
                                    color = Colors.Gray,
                                    message = $"FOUND: <color=#{ColorUtility.ToHtmlStringRGBA(messageColor)}>{itemName.ToUpper()}</color> (<color=#{ColorUtility.ToHtmlStringRGBA(Colors.PlayerOther)}>{forPlayer}</color>)"
                                });
                            }
                            else
                            {
                                messageColor = LocationManager.GetAPMessageColor(p.Data[2].Flags.Value);
                                LocationManager.messages.Add(new Message()
                                {
                                    image = "archipelago",
                                    color = LocationManager.GetAPMessageColor(p.Data[2].Flags.Value),
                                    message = $"FOUND: <color=#{ColorUtility.ToHtmlStringRGBA(messageColor)}>{itemName.ToUpper()}</color> (<color=#{ColorUtility.ToHtmlStringRGBA(Colors.PlayerOther)}>{forPlayer}</color>)"
                                });
                            }
                        }

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
                                    if (long.TryParse(messagePart.Text, out long itemId))
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
                                    if (long.TryParse(messagePart.Text, out long locationId))
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
                        UIManager.SetLogText(string.Join("\n", messages.ToArray()));
                        break;
                    }
            }
        }

        public static void ItemReceived(ReceivedItemsHelper helper)
        {
            bool shouldGetItemAgain = LocationManager.ShouldGetItemAgain(LocationManager.GetTypeFromName(helper.PeekItemName()).Value);
            bool silent = !(helper.Index > Core.data.index) && shouldGetItemAgain;
            if (helper.Index > Core.data.index || shouldGetItemAgain)
            {
                string name = helper.PeekItemName();
                string player = Session.Players.GetPlayerName(helper.PeekItem().Player);
                if (player == Core.data.slot_name) player = null;
                else player = (Session.Players.GetPlayerAlias(helper.PeekItem().Player) == "") ? "?" : Session.Players.GetPlayerAlias(helper.PeekItem().Player);
                if (helper.Index > Core.data.index) Core.Logger.LogInfo("Name: \"" + name + "\" | Type: " + LocationManager.GetTypeFromName(name) + " | Player: \"" + player + "\"");

                UKItem item = new UKItem()
                {
                    itemName = name,
                    type = LocationManager.GetTypeFromName(name).Value,
                    playerName = Core.data.slot_name
                };

                LocationManager.itemQueue.Add(new QueuedItem(item, player, silent));
                if (helper.Index > Core.data.index) Core.data.index++;
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
