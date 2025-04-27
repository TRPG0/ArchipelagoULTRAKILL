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
using BepInEx;
using System.Reflection;
using System.Linq;

namespace ArchipelagoULTRAKILL
{
    public static class Multiworld
    {
        public static Version apVersion = new Version(0, 6, 1);
        public static DeathLinkService DeathLinkService = null;
        public static DeathLink lastDeathLink = null;

        public static bool Authenticated;
        public static bool HintMode = false;
        public static ArchipelagoSession Session;
        public static List<string> messages = new List<string>();

        public static List<RecentItem> recentItems = new List<RecentItem>();

        public static void TryGetStart(ref HashSet<string> unlockedLevels, Dictionary<string, object> slotData, string defaultValue)
        {
            try 
            { 
                unlockedLevels.Add(slotData["start"].ToString());
                Core.data.start = slotData["start"].ToString();
            }
            catch (KeyNotFoundException)
            {
                Core.Logger.LogWarning($"No key found for start level. Using default value ({defaultValue})");
                unlockedLevels.Add(defaultValue);
                Core.data.start = defaultValue;
            }
        }

        public static void TryGetGoal(ref string goal, Dictionary<string, object> slotData, string defaultValue)
        {
            if (int.TryParse(slotData["goal"].ToString(), out int goalNum))
            {
                Core.Logger.LogWarning("Using legacy goal option.");
                switch (goalNum)
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
            else
            {
                goal = slotData["goal"].ToString();
            }
        }

        public static void TryGetEnemyOption(ref EnemyOptions option, Dictionary<string, object> slotData, string key, EnemyOptions defaultValue)
        {
            try { option = (EnemyOptions)int.Parse(slotData[key].ToString()); }
            catch (KeyNotFoundException)
            {
                try 
                { 
                    option = (EnemyOptions)int.Parse(slotData["boss_rewards"].ToString());
                    Core.Logger.LogInfo("Using legacy enemy reward option.");
                }
                catch (KeyNotFoundException)
                {
                    Core.Logger.LogWarning($"No key found for enemy rewards. Using default value ({defaultValue})");
                    option = defaultValue;
                }
            }
        }

        public static void TryGetFire2(ref Fire2Options option, Dictionary<string, object> slotData, string key, Fire2Options defaultValue)
        {
            if (bool.TryParse(slotData[key].ToString(), out bool value))
            {
                Core.Logger.LogInfo("Using legacy secondary fire option.");
                if (value) option = Fire2Options.Split;
                else option = Fire2Options.Disabled;
            }
            else
            {
                try { option = (Fire2Options)int.Parse(slotData[key].ToString()); }
                catch (KeyNotFoundException)
                {
                    Core.Logger.LogWarning($"No key found for option \"{key}\". Using default value ({defaultValue})");
                    option = defaultValue;
                }
            }
        }

        public static void TryGetSlotDataValue(ref string option, Dictionary<string, object> slotData, string key, string defaultValue)
        {
            try { option = slotData[key].ToString(); }
            catch (KeyNotFoundException)
            {
                Core.Logger.LogWarning($"No key found for option \"{key}\". Using default value ({defaultValue})");
                option = defaultValue;
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

            WeaponLoadout loadout = WeaponLoadout.Get();
            Core.Logger.LogInfo("Got loadout");
            GameProgressMoneyAndGear gameProgress = GameProgressSaver.GetGeneralProgress();
            Core.LockAllWeapons();

            Core.data.unlockedSkulls1_4 = 0;
            Core.data.unlockedSkulls5_1 = 0;

            Session = ArchipelagoSessionFactory.CreateSession(Core.data.host_name);
            Session.Socket.SocketClosed += SocketClosed;
            Session.Socket.ErrorReceived += ErrorReceived;
            Session.Socket.PacketReceived += PacketReceived;
            Session.Items.ItemReceived += ItemReceived;

            Core.Logger.LogInfo($"Attempting connection ({Core.data.slot_name} - {apVersion} / {Core.PluginVersion})");

            LoginResult loginResult = Session.TryConnectAndLogin(
                "ULTRAKILL",
                Core.data.slot_name,
                ItemsHandlingFlags.AllItems,
                apVersion,
                null,
                null,
                Core.data.password == "" ? null : Core.data.password,
                true
                );

            if (loginResult is LoginSuccessful success)
            {
                Core.Logger.LogInfo("Login successful");
                Authenticated = true;

                ConfigManager.isConnected.value = true;
                ConfigManager.playerName.interactable = false;
                ConfigManager.serverAddress.interactable = false;
                ConfigManager.serverPassword.interactable = false;
                ConfigManager.hintMode.interactable = false;
                ConfigManager.chat.interactable = true;

                TryGetSlotDataValue(ref Core.data.version, success.SlotData, "version", string.Empty);
                Core.Logger.LogInfo($"Server version: {Session.RoomState.Version} / {(Core.data.version == string.Empty ? "?" : Core.data.version)}");
                TryGetStart(ref Core.data.unlockedLevels, success.SlotData, "0-1");
                TryGetGoal(ref Core.data.goal, success.SlotData, "6-2");

                TryGetSlotDataValue(ref Core.data.goalRequirement, success.SlotData, "goal_requirement", 15);
                TryGetSlotDataValue(ref Core.data.perfectGoal, success.SlotData, "perfect_goal", false);
                TryGetEnemyOption(ref Core.data.enemyRewards, success.SlotData, "enemy_rewards", EnemyOptions.Disabled);
                TryGetSlotDataValue(ref Core.data.challengeRewards, success.SlotData, "challenge_rewards", false);
                TryGetSlotDataValue(ref Core.data.pRankRewards, success.SlotData, "p_rank_rewards", false);
                TryGetSlotDataValue(ref Core.data.hankRewards, success.SlotData, "hank_rewards", false);
                TryGetSlotDataValue(ref Core.data.clashReward, success.SlotData, "randomize_clash_mode", false);
                TryGetSlotDataValue(ref Core.data.fishRewards, success.SlotData, "fish_rewards", false);
                TryGetSlotDataValue(ref Core.data.cleanRewards, success.SlotData, "cleaning_rewards", false);
                TryGetSlotDataValue(ref Core.data.chessReward, success.SlotData, "chess_reward", false);
                TryGetSlotDataValue(ref Core.data.rocketReward, success.SlotData, "rocket_race_reward", false);
                TryGetFire2(ref Core.data.randomizeFire2, success.SlotData, "randomize_secondary_fire", Fire2Options.Disabled);
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
                    Core.Logger.LogInfo("Doing first time setup for save file");
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

                    Core.data.playerCount = Session.Players.Players[0].Count - 1;

                    PrefsManager.Instance.SetInt("difficulty", 4);
                    GameProgressSaver.SetIntro(true);
                    GameProgressSaver.SetTutorial(true);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Revolver);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Shotgun);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Nailgun);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Railcannon);
                    GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.RocketLauncher);
                    GameProgressSaver.SaveProgress(30);
                    GameProgressSaver.SetEncoreProgress(101);
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
                LocationManager.RegenerateItemDefinitions();
                Core.Logger.LogInfo("Successfully connected to server as player \"" + Core.data.slot_name + "\".");
                ConfigManager.connectionInfo.text = "Successfully connected to server as player \"" + Core.data.slot_name + "\".";
                UIManager.menuIcon.GetComponent<Image>().color = Colors.Green;
                if (Core.data.completedLevels.Count < Core.data.goalRequirement) UIManager.CreateGoalCounter();
                string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
                //UIManager.menuText.text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n" + Core.data.@checked.Count + "/" + totalLocations;

                Core.Logger.LogInfo("Scouting shop items");
                foreach (string weapon in Core.shopPrices.Keys)
                {
                    long locationId = LocationManager.locations[$"shop_{weapon}"];
                    ScoutedItemInfo info = Session.Locations.ScoutLocationsAsync(false, LocationManager.locations[$"shop_{weapon}"]).Result[locationId];
                    string itemName = info.ItemName;
                    string playerName = Session.Players.GetPlayerName(info.Player);
                    if (LocationManager.GetItemDefinition(itemName) != null)
                    {
                        LocationManager.shopScouts.Add($"shop_{weapon}", new UKItem()
                        {
                            itemName = itemName,
                            playerName = playerName,
                            type = LocationManager.GetItemDefinition(itemName).Type
                        });
                    }
                    else
                    {
                        LocationManager.shopScouts.Add($"shop_{weapon}", new APItem()
                        {
                            itemName = itemName,
                            playerName = playerName,
                            type = info.Flags
                        });
                    }
                }

                foreach (string loc in Core.data.@checked)
                {
                    LocationManager.CheckLocation(loc);
                }

                Core.Instance.StartCoroutine("ApplyLoadout", loadout);
            }
            else if (loginResult is LoginFailure failure)
            {
                Core.Logger.LogInfo("Login failed");
                Authenticated = false;
                //GameConsole.Console.Instance.PrintLine(String.Join("\n", loginFailure.Errors));
                Core.Logger.LogError(String.Join("\n", failure.Errors));
                ConfigManager.connectionInfo.text = String.Join("\n", failure.Errors);
                Session.Socket.DisconnectAsync();
                Session = null;
                UIManager.menuIcon.GetComponent<Image>().color = Colors.Red;
                Core.SaveGeneralProgress(gameProgress);
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
                apVersion,
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
                //UIManager.menuText.text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n<color=yellow>Hint Mode</color>";
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

                        // found item for self
                        if (p.Data[0].Type == JsonMessagePartType.PlayerId
                            && Session.Players.GetPlayerName(int.Parse(p.Data[0].Text)) == Core.data.slot_name
                            && p.Data[1].Text == " found their ")
                        {
                            string locationName = Session.Locations.GetLocationNameFromId(long.Parse(p.Data[4].Text));
                            string itemName = Session.Items.GetItemName(long.Parse(p.Data[2].Text));
                            ItemFlags itemFlags = p.Data[2].Flags.Value;

                            string locationImage = "";
                            string locationColor = null;

                            if (locationName.StartsWith("Enemy:") || locationName.StartsWith("Boss:"))
                            {
                                locationImage = LocationManager.GetCurrentLevelImage();
                                locationColor = Core.CurrentLevelInfo?.Name;
                            }
                            else
                            {
                                locationImage = LocationManager.GetLocationImage(locationName);
                            }

                            Core.data.recentLocations.Add(new RecentLocation(locationName, locationImage, locationColor, itemName, itemFlags, Core.data.slot_name));
                            if (Core.data.recentLocations.Count > 5) Core.data.recentLocations.RemoveAt(0);
                        }
                        // found item for other player
                        else if (p.Data[0].Type == JsonMessagePartType.PlayerId
                            && Session.Players.GetPlayerName(int.Parse(p.Data[0].Text)) == Core.data.slot_name
                            && p.Data[1].Text == " sent ")
                        {
                            string itemName = Session.Items.GetItemName(long.Parse(p.Data[2].Text), Session.Players.GetPlayerInfo(p.Data[2].Player.Value).Game);
                            ItemFlags itemFlags = p.Data[2].Flags.Value;
                            string forPlayer = Session.Players.GetPlayerAlias(int.Parse(p.Data[4].Text));
                            string locationName = Session.Locations.GetLocationNameFromId(long.Parse(p.Data[6].Text));

                            string locationImage = "";
                            string locationColor = null;

                            if (locationName.StartsWith("Enemy:") || locationName.StartsWith("Boss:"))
                            {
                                locationImage = LocationManager.GetCurrentLevelImage();
                                locationColor = Core.CurrentLevelInfo?.Name;
                            }
                            else
                            {
                                locationImage = LocationManager.GetLocationImage(locationName);
                            }

                            Core.data.recentLocations.Add(new RecentLocation(locationName, locationImage, locationColor, itemName, itemFlags, forPlayer));
                            if (Core.data.recentLocations.Count > 5) Core.data.recentLocations.RemoveAt(0);

                            Color messageColor = Color.white;

                            if (LocationManager.GetItemDefinition(itemName) != null)
                            {
                                messageColor = LocationManager.GetItemDefinition(itemName).Color.Invoke();
                                LocationManager.messages.Add(new Message()
                                {
                                    image = LocationManager.GetItemDefinition(itemName).Image,
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
                                        string itemName = Session.Items.GetItemName(itemId, Session.Players.GetPlayerInfo(messagePart.Player.Value).Game) ?? $"Item: {itemId}";
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
                                        string locationName = Session.Locations.GetLocationNameFromId(locationId, Session.Players.GetPlayerInfo(messagePart.Player.Value).Game) ?? $"Location: {locationId}";
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
            bool shouldGetItemAgain = LocationManager.ShouldGetItemAgain(LocationManager.GetItemDefinition(helper.PeekItem().ItemName).Type);
            bool silent = !(helper.Index > Core.data.index) && shouldGetItemAgain;
            if (helper.Index > Core.data.index || shouldGetItemAgain)
            {
                ItemInfo item = helper.PeekItem();
                string name = item.ItemName;
                string player = Session.Players.GetPlayerName(item.Player);
                if (player == Core.data.slot_name) player = null;
                else player = (Session.Players.GetPlayerAlias(item.Player) == "") ? "?" : Session.Players.GetPlayerAlias(item.Player);
                if (helper.Index > Core.data.index) Core.Logger.LogInfo("Name: \"" + name + "\" | Type: " + LocationManager.GetItemDefinition(name).Type + " | Player: \"" + player + "\"");

                UKType type = LocationManager.GetItemDefinition(name).Type;

                if (type == UKType.Weapon && Core.data.randomizeFire2 == Fire2Options.Progressive && LocationManager.fire2Weapons.Contains(name))
                {
                    // avoiding a file sharing violation lol
                    FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(LocationManager.GetWeaponIdFromName(name), BindingFlags.Public | BindingFlags.Instance);
                    if ((int)field.GetValue(LocationManager.generalProgress) > 0)
                    {
                        name = "Secondary Fire -" + name.Split('-')[1];
                        type = UKType.Fire2;
                    }
                }

                UKItem ukitem = new UKItem()
                {
                    itemName = name,
                    type = type,
                    playerName = Core.data.slot_name
                };

                LocationManager.itemQueue.Add(new QueuedItem(ukitem, player, silent));

                recentItems.Add(new RecentItem(name, player, helper.Index));
                if (recentItems.Count > 5) recentItems.RemoveAt(0);

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
            if (Core.IsInLevel && Core.uim.deathLinkMessage == null) Core.uim.CreateDeathLinkMessage();
        }

        public static void DisableDeathLink()
        {
            if (DeathLinkService == null) return;
            else DeathLinkService.DisableDeathLink();
        }

        public static void DeathLinkReceived(DeathLink deathLink)
        {
            if (Core.IsInLevel) lastDeathLink = deathLink;
            else Core.Logger.LogWarning("Received DeathLink, but player cannot be killed right now.");
        }

        public static void SendCompletion()
        {
            Core.Logger.LogInfo("Goal completed!");
            Session?.SetGoalAchieved();
        }

        public static void UpdateCompletedLevels()
        {
            if (Session != null)
            {
                Core.Logger.LogInfo("1");
                Session.DataStorage[Scope.Slot, "completedLevels"] = Core.data.completedLevels.ToArray();
                Core.Logger.LogInfo("2");
                Session.Socket.SendPacketAsync(new BouncePacket() 
                { 
                    Slots = new List<int>() { Session.Players.ActivePlayer.Slot },
                    Tags = new List<string>() { "Tracker" },
                    Data = new Dictionary<string, JToken>() { ["completedLevels"] = null },
                });
                Core.Logger.LogInfo("3");
            }
        }

        public static bool ServerVersionIsAtLeast(string version)
        {
            if (Core.data.version.IsNullOrWhiteSpace() || Core.data.version == "?") return false;
            if (Core.data.version == version) return true;

            if (Core.data.version.CompareTo(version) < 0) return false;
            else return true;
        }
    }
}
