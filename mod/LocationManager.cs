using Archipelago.MultiClient.Net.Enums;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArchipelagoULTRAKILL
{
    public static class LocationManager
    {
        public static readonly Dictionary<string, Color> colors = new Dictionary<string, Color>()
        {
            ["white"] = new Color(1, 1, 1),
            ["gray"] = new Color (0.7f, 0.7f, 0.7f),
            ["red"] = new Color(1, 0.2353f, 0.2353f),
            ["green"] = new Color(0.2667f, 1, 0.2706f),
            ["lightblue"] = new Color(0.251f, 0.9059f, 1),
            ["yellow"] = new Color(1, 1, 0.25f, 1),
            ["orange"] = new Color(1, 0.5f, 0.25f),
            ["gold"] = new Color(1, 0.65f, 0),
            ["purple"] = new Color(0.765f, 0.25f, 1),
            ["bone"] = new Color(1, 0.9479f, 0.8566f),
            ["ap_item_advancement"] = new Color(0.69f, 0.6f, 0.94f),
            ["ap_item_neverexclude"] = new Color(0.43f, 0.55f, 0.91f),
            ["ap_item_trap"] = new Color(0.98f, 0.5f, 0.45f),
            ["ap_item_filler"] = new Color(0, 0.93f, 0.93f),
            ["ap_player_self"] = new Color(0.93f, 0, 0.93f),
            ["ap_player_other"] = new Color(0.98f, 0.98f, 0.82f),
            ["ap_location"] = new Color(0, 1, 0.5f),
            ["ap_entrance"] = new Color(0.39f, 0.58f, 0.93f)
        };

        public static Dictionary<string, UKLocation> locations = new Dictionary<string, UKLocation>();
        public static Dictionary<string, UKItem> ukitems = new Dictionary<string, UKItem>();
        public static Dictionary<string, APItem> apitems = new Dictionary<string, APItem>();

        public static List<Message> messages = new List<Message>();
        public static List<Enums.Powerup> powerupQueue = new List<Enums.Powerup>();
        public static bool overhealWaiting = false;
        public static bool hardDmgWaiting = false;
        public static bool soapWaiting = false;

        public static List<KeyValuePair<string, UKItem>> tempItems = new List<KeyValuePair<string, UKItem>>();

        public static void CheckLocation(string loc)
        {
            if (!AssistController.Instance.cheatsEnabled)
            {
                Core.logger.LogInfo("Checking location \"" + loc + "\"");
                if (!Core.data.@checked.Contains(loc)) Core.data.@checked.Add(loc);

                if (locations.ContainsKey(loc))
                {
                    if (Multiworld.Authenticated) Multiworld.Session.Locations.CompleteLocationChecks(locations[loc].ap_id);
                    if (locations[loc].ukitem && !locations[loc].@checked) GetUKItem(ukitems[loc]);
                    else if (!locations[loc].ukitem && !locations[loc].@checked) AddAPItemMessage(apitems[loc]);
                    locations[loc].@checked = true;
                }
            }
            else Core.logger.LogWarning("Tried to check location \"" + loc + "\", but cheats are enabled!");
        }

        public static void GetUKItem(UKItem item, string sendingPlayer = null)
        {
            string itemColor = ColorUtility.ToHtmlStringRGB(GetUKMessageColor(item.item_name));
            string playerColor = ColorUtility.ToHtmlStringRGB(colors["ap_player_other"]);
            string text = "";

            if (item.player_name == Core.data.slot_name)
            {
                switch (item.type)
                {
                    case Enums.UKItemType.Weapon:
                        GameProgressSaver.AddGear(GetWeaponIdFromName(item.item_name));
                        PrefsManager.Instance.SetInt("weapon." + GetWeaponIdFromName(item.item_name), 1);
                        if (Core.playerActive && Core.inLevel)
                        {
                            GunSetter.Instance.ResetWeapons();
                            GunSetter.Instance.ForceWeapon(GetWeaponIdFromName(item.item_name));
                        }
                        text = "WEAPON: ";
                        break;

                    case Enums.UKItemType.WeaponAlt:
                        //Core.logger.LogInfo("WeaponAlt: " + GetWeaponIdFromName(item.item_name));
                        GameProgressSaver.AddGear(GetWeaponIdFromName(item.item_name));
                        text = "WEAPON: ";
                        break;

                    case Enums.UKItemType.Arm:
                        if (item.item_name == "Feedbacker")
                        {
                            Core.data.hasArm = true;
                            PrefsManager.Instance.SetInt("weapon.arm0", 1);
                            if (Core.playerActive && Core.inLevel)
                            {
                                FistControl.Instance.ResetFists();
                                FistControl.Instance.ForceArm(int.Parse(GetWeaponIdFromName(item.item_name).Substring(3, 1)), false);
                            }
                        }
                        else
                        {
                            GameProgressSaver.AddGear(GetWeaponIdFromName(item.item_name));
                            PrefsManager.Instance.SetInt("weapon." + GetWeaponIdFromName(item.item_name), 1);
                            if (Core.playerActive && Core.inLevel)
                            {
                                FistControl.Instance.ResetFists();
                                FistControl.Instance.ForceArm(int.Parse(GetWeaponIdFromName(item.item_name).Substring(3, 1)), false);
                            }
                        }
                        text = "ARM: ";
                        break;

                    case Enums.UKItemType.Ability:
                        if (item.item_name == "Stamina Bar" && Core.data.dashes < 3) Core.data.dashes++;
                        else if (item.item_name == "Wall Jump" && Core.data.walljumps < 3) Core.data.walljumps++;
                        else if (item.item_name == "Slide") Core.data.canSlide = true;
                        else if (item.item_name == "Slam") Core.data.canSlam = true;
                        text = "ABILITY: ";
                        break;

                    case Enums.UKItemType.Skull:
                        if (!Core.data.randomizeSkulls) return;
                        if (item.item_name.Contains("1-4"))
                        {
                            if (Core.data.unlockedSkulls1_4 == 4) return;
                            Core.data.unlockedSkulls1_4++;
                        }
                        else if (item.item_name.Contains("5-1"))
                        {
                            if (Core.data.unlockedSkulls5_1 == 3) return;
                            Core.data.unlockedSkulls5_1++;
                        }
                        else if (item.item_name.Contains("0-S"))
                        {
                            if (item.item_name.Contains("Blue")) Core.data.unlockedSkulls.Add("S_b");
                            else if (item.item_name.Contains("Red")) Core.data.unlockedSkulls.Add("S_r");
                        }
                        else
                        {
                            Core.logger.LogInfo(item.item_name.Substring(item.item_name.Length - 4, 3));
                            string skull = Core.idToLevel.FirstOrDefault(x => x.Value == item.item_name.Substring(item.item_name.Length - 4, 3)).Key.ToString();
                            if (item.item_name.Contains("Blue")) skull += "_b";
                            else if (item.item_name.Contains("Red")) skull += "_r";
                            Core.logger.LogInfo("Skull: " + skull);
                            Core.data.unlockedSkulls.Add(skull);
                        }
                        if (SceneHelper.CurrentScene == "Level 1-4") LevelManager.skulls.ElementAt(Core.data.unlockedSkulls1_4 - 1).Value.SetActive(true);
                        else if (SceneHelper.CurrentScene == "Level 5-1") LevelManager.skulls.ElementAt(Core.data.unlockedSkulls5_1 - 1).Value.SetActive(true);
                        else if (SceneHelper.CurrentScene == "Level 0-S")
                        {
                            if (item.item_name.Contains("Blue")) LevelManager.skulls["SkullBlue"].SetActive(true);
                            else if (item.item_name.Contains("Red")) LevelManager.skulls["SkullRed"].SetActive(true);
                        }
                        else if (Core.playerActive && item.item_name.Contains(Core.idToLevel[StatsManager.Instance.levelNumber]))
                        {
                            if (item.item_name.Contains("Blue")) LevelManager.skulls["SkullBlue"].SetActive(true);
                            else if (item.item_name.Contains("Red")) LevelManager.skulls["SkullRed"].SetActive(true);
                        }
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case Enums.UKItemType.Level:
                        //APULTRAKILL.logger.LogInfo(item.item_name.Substring(0, 3));
                        Core.data.unlockedLevels.Add(item.item_name.Substring(0, 3));
                        text = "UNLOCKED: ";
                        break;

                    case Enums.UKItemType.Layer:
                        if (item.item_name.Contains("OVERTURE"))
                        {
                            Core.data.unlockedLevels.Add("0-2");
                            Core.data.unlockedLevels.Add("0-3");
                            Core.data.unlockedLevels.Add("0-4");
                            Core.data.unlockedLevels.Add("0-5");
                        }
                        else if (item.item_name.Contains("1"))
                        {
                            Core.data.unlockedLevels.Add("1-1");
                            Core.data.unlockedLevels.Add("1-2");
                            Core.data.unlockedLevels.Add("1-3");
                            if (Core.data.goal != "1-4") Core.data.unlockedLevels.Add("1-4");
                        }
                        else if (item.item_name.Contains("2"))
                        {
                            Core.data.unlockedLevels.Add("2-1");
                            Core.data.unlockedLevels.Add("2-2");
                            Core.data.unlockedLevels.Add("2-3");
                            if (Core.data.goal != "2-4") Core.data.unlockedLevels.Add("2-4");
                        }
                        else if (item.item_name.Contains("3"))
                        {
                            Core.data.unlockedLevels.Add("3-1");
                            if (Core.data.goal != "3-2") Core.data.unlockedLevels.Add("3-2");
                        }
                        else if (item.item_name.Contains("4"))
                        {
                            Core.data.unlockedLevels.Add("4-1");
                            Core.data.unlockedLevels.Add("4-2");
                            Core.data.unlockedLevels.Add("4-3");
                            if (Core.data.goal != "4-4") Core.data.unlockedLevels.Add("4-4");
                        }
                        else if (item.item_name.Contains("5"))
                        {
                            Core.data.unlockedLevels.Add("5-1");
                            Core.data.unlockedLevels.Add("5-2");
                            Core.data.unlockedLevels.Add("5-3");
                            if (Core.data.goal != "5-4") Core.data.unlockedLevels.Add("5-4");
                        }
                        else if (item.item_name.Contains("6"))
                        {
                            Core.data.unlockedLevels.Add("6-1");
                            if (Core.data.goal != "6-2") Core.data.unlockedLevels.Add("6-2");
                        }
                        text = "UNLOCKED: ";
                        break;

                    case Enums.UKItemType.Points:
                        GameProgressSaver.AddMoney(10000);
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case Enums.UKItemType.Powerup:
                        if (item.item_name == "Overheal")
                        {
                            if (Core.playerActive && Core.inLevel) NewMovement.Instance.SuperCharge();
                            else overhealWaiting = true;
                        }
                        else if (item.item_name == "Dual Wield") powerupQueue.Add(Enums.Powerup.DualWield);
                        else if (item.item_name == "Infinite Stamina") powerupQueue.Add(Enums.Powerup.InfiniteStamina);
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case Enums.UKItemType.Trap:
                        if (item.item_name == "Hard Damage")
                        {
                            if (Core.playerActive && Core.inLevel) NewMovement.Instance.ForceAntiHP(50);
                            else hardDmgWaiting = true;
                        }
                        else if (item.item_name == "Stamina Limiter") powerupQueue.Add(Enums.Powerup.StaminaLimiter);
                        else if (item.item_name == "Wall Jump Limiter") powerupQueue.Add(Enums.Powerup.WalljumpLimiter);
                        else if (item.item_name == "Empty Ammunition") powerupQueue.Add(Enums.Powerup.EmptyAmmo);
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case Enums.UKItemType.Soap:
                        if (Core.playerActive && Core.inLevel) Core.SpawnSoap();
                        else soapWaiting = true;
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case Enums.UKItemType.Fire2:
                        Core.data.unlockedFire2.Add(GetWeaponIdFromName(item.item_name));
                        text = "UNLOCKED: ";
                        break;

                    default: break;
                }

                text += "<color=#" + itemColor + "FF>" + item.item_name.ToUpper() + "</color>";
                if (sendingPlayer != null) text += " (<color=#" + playerColor + "FF>" + sendingPlayer + "</color>)";
                messages.Add(new Message
                {
                    image = GetUKMessageImage(item.item_name),
                    color = GetUKMessageColor(item.item_name),
                    message = text
                });
            }
            else
            {
                text = "FOUND: <color=#" + itemColor + "FF>" + item.item_name.ToUpper() + "</color> (<color=#" + playerColor + "FF>" + item.player_name + "</color>)";
                messages.Add(new Message
                {
                    image = GetUKMessageImage(item.item_name),
                    color = colors["gray"],
                    message = text
                });
            }
            if (!UIManager.displayingMessage && Core.playerActive) Core.uim.StartCoroutine("DisplayMessage");
            if (powerupQueue.Count > 0 && Core.playerActive && Core.inLevel && !Core.poweredUp) Core.AddPowerup();
        }

        public static void AddAPItemMessage(APItem item)
        {
            string itemColor = ColorUtility.ToHtmlStringRGB(GetAPMessageColor(item.type));
            string playerColor = ColorUtility.ToHtmlStringRGB(colors["ap_player_other"]);
            string text = "FOUND: <color=#" + itemColor + "FF>" + item.item_name + "</color> (<color=#" + playerColor + "FF>" + item.player_name + "</color>)";

            messages.Add(new Message
            {
                image = "archipelago",
                color = GetAPMessageColor(item.type),
                message = text
            });
            if (!UIManager.displayingMessage && Core.playerActive) Core.uim.StartCoroutine("DisplayMessage");
        }

        public static string GetUKMessageImage(string itemName)
        {
            switch (itemName)
            {
                case "Revolver - Piercer":
                case "Revolver - Marksman":
                case "Revolver - Sharpshooter":
                case "Secondary Fire - Piercer":
                case "Secondary Fire - Marksman":
                case "Secondary Fire - Sharpshooter":
                    return "rev";
                case "Shotgun - Core Eject":
                case "Shotgun - Pump Charge":
                case "Secondary Fire - Core Eject":
                case "Secondary Fire - Pump Charge":
                    return "sho";
                case "Nailgun - Attractor":
                case "Nailgun - Overheat":
                case "Secondary Fire - Attractor":
                case "Secondary Fire - Overheat":
                    return "nai";
                case "Railcannon - Electric":
                case "Railcannon - Screwdriver":
                case "Railcannon - Malicious":
                    return "rai";
                case "Rocket Launcher - Freezeframe":
                case "Rocket Launcher - S.R.S. Cannon":
                case "Secondary Fire - Freezeframe":
                case "Secondary Fire - S.R.S. Cannon":
                    return "rock";
                case "Revolver - Alternate":
                    return "revalt";
                case "Nailgun - Alternate":
                    return "naialt";
                // TO DO: make arm icons
                case "Feedbacker":
                case "Knuckleblaster":
                case "Whiplash":
                    return "arm";
                case "Stamina Bar":
                    return "dash";
                case "Wall Jump":
                    return "walljump";
                case "Slide":
                    return "slide";
                case "Slam":
                    return "slam";
                case "Blue Skull (0-2)":
                case "Blue Skull (0-S)":
                case "Red Skull (0-S)":
                case "Red Skull (1-1)":
                case "Blue Skull (1-1)":
                case "Blue Skull (1-2)":
                case "Red Skull (1-2)":
                case "Blue Skull (1-3)":
                case "Red Skull (1-3)":
                case "Blue Skull (1-4)":
                case "Blue Skull (2-3)":
                case "Red Skull (2-3)":
                case "Blue Skull (2-4)":
                case "Red Skull (2-4)":
                case "Blue Skull (4-2)":
                case "Red Skull (4-2)":
                case "Blue Skull (4-3)":
                case "Blue Skull (4-4)":
                case "Blue Skull (5-1)":
                case "Blue Skull (5-2)":
                case "Red Skull (5-2)":
                case "Blue Skull (5-3)":
                case "Red Skull (5-3)":
                case "Blue Skull (5-4)":
                case "Red Skull (6-1)":
                    return "skull";
                case "0-2: THE MEATGRINDER":
                case "0-3: DOUBLE DOWN":
                case "0-4: A ONE-MACHINE ARMY":
                case "0-5: CERBERUS":
                case "OVERTURE: THE MOUTH OF HELL":
                    return "layer0";
                case "1-1: HEART OF THE SUNRISE":
                case "1-2: THE BURNING WORLD":
                case "1-3: HALLS OF SACRED REMAINS":
                case "1-4: CLAIR DE LUNE":
                case "LAYER 1: LIMBO":
                    return "layer1";
                case "2-1: BRIDGEBURNER":
                case "2-2: DEATH AT 20,000 VOLTS":
                case "2-3: SHEER HEART ATTACK":
                case "2-4: COURT OF THE CORPSE KING":
                case "LAYER 2: LUST":
                    return "layer2";
                case "3-1: BELLY OF THE BEAST":
                case "3-2: IN THE FLESH":
                case "LAYER 3: GLUTTONY":
                    return "layer3";
                case "4-1: SLAVES TO POWER":
                case "4-2: GOD DAMN THE SUN":
                case "4-3: A SHOT IN THE DARK":
                case "4-4: CLAIR DE SOLEIL":
                case "LAYER 4: GREED":
                    return "layer4";
                case "5-1: IN THE WAKE OF POSEIDON":
                case "5-2: WAVES OF THE STARLESS SEA":
                case "5-3: SHIP OF FOOLS":
                case "5-4: LEVIATHAN":
                case "LAYER 5: WRATH":
                    return "layer5";
                case "6-1: CRY FOR THE WEEPER":
                case "6-2: AESTHETICS OF HATE":
                case "LAYER 6: HERESY":
                    return "layer6";
                // TO DO: make prime sanctum icons
                case "P-1: SOUL SURVIVOR":
                case "P-2: WAIT OF THE WORLD":
                    return "layer3";
                case "+10,000P":
                    return "points";
                case "Overheal":
                case "Hard Damage":
                    return "overheal";
                case "Dual Wield":
                case "Empty Ammunition":
                    return "dualwield";
                case "Infinite Stamina":
                case "Stamina Limiter":
                    return "infinitestamina";
                case "Wall Jump Limiter":
                    return "walljumptrap";
                case "Soap":
                    return "soap";
                default:
                    return "archipelago";
            }
        }

        public static Color GetUKMessageColor(string itemName)
        {
            switch (itemName)
            {
                case "Revolver - Piercer":
                case "Shotgun - Core Eject":
                case "Nailgun - Attractor":
                case "Railcannon - Electric":
                case "Rocket Launcher - Freezeframe":
                case "Secondary Fire - Piercer":
                case "Secondary Fire - Core Eject":
                case "Secondary Fire - Attractor":
                case "Secondary Fire - Freezeframe":
                    return ColorBlindSettings.Instance.variationColors[0]; // blue
                case "Revolver - Marksman":
                case "Shotgun - Pump Charge":
                case "Nailgun - Overheat":
                case "Railcannon - Screwdriver":
                case "Rocket Launcher - S.R.S. Cannon":
                case "Secondary Fire - Marksman":
                case "Secondary Fire - Pump Charge":
                case "Secondary Fire - Overheat":
                case "Secondary Fire - S.R.S. Cannon":
                    return ColorBlindSettings.Instance.variationColors[1]; // green
                case "Revolver - Sharpshooter":
                case "Railcannon - Malicious":
                case "Secondary Fire - Sharpshooter":
                    return ColorBlindSettings.Instance.variationColors[2]; // red
                case "Revolver - Alternate":
                case "Nailgun - Alternate":
                    return colors["gold"];
                case "Stamina Bar":
                case "Wall Jump":
                case "Slide":
                case "Slam":
                case "Infinite Stamina":
                    return ColorBlindSettings.Instance.staminaColor;
                case "Feedbacker":
                case "5-1: IN THE WAKE OF POSEIDON":
                case "5-2: WAVES OF THE STARLESS SEA":
                case "5-3: SHIP OF FOOLS":
                case "5-4: LEVIATHAN":
                case "LAYER 5: WRATH":
                case "Blue Skull (0-2)":
                case "Blue Skull (0-S)":
                case "Blue Skull (1-1)":
                case "Blue Skull (1-2)":
                case "Blue Skull (1-3)":
                case "Blue Skull (1-4)":
                case "Blue Skull (2-3)":
                case "Blue Skull (2-4)":
                case "Blue Skull (4-2)":
                case "Blue Skull (4-3)":
                case "Blue Skull (4-4)":
                case "Blue Skull (5-1)":
                case "Blue Skull (5-2)":
                case "Blue Skull (5-3)":
                case "Blue Skull (5-4)":
                    return colors["lightblue"];
                case "Whiplash":
                case "1-1: HEART OF THE SUNRISE":
                case "1-2: THE BURNING WORLD":
                case "1-3: HALLS OF SACRED REMAINS":
                case "1-4: CLAIR DE LUNE":
                case "LAYER 1: LIMBO":
                    return colors["green"];
                case "Knuckleblaster":
                case "6-1: CRY FOR THE WEEPER":
                case "6-2: AESTHETICS OF HATE":
                case "LAYER 6: HERESY":
                case "P-1: SOUL SURVIVOR":
                case "P-2: WAIT OF THE WORLD":
                case "Red Skull (0-S)":
                case "Red Skull (1-1)":
                case "Red Skull (1-2)":
                case "Red Skull (1-3)":
                case "Red Skull (2-3)":
                case "Red Skull (2-4)":
                case "Red Skull (4-2)":
                case "Red Skull (5-2)":
                case "Red Skull (5-3)":
                case "Red Skull (6-1)":
                    return colors["red"];
                case "0-2: THE MEATGRINDER":
                case "0-3: DOUBLE DOWN":
                case "0-4: A ONE-MACHINE ARMY":
                case "0-5: CERBERUS":
                case "OVERTURE: THE MOUTH OF HELL":
                    return colors["orange"];
                case "2-1: BRIDGEBURNER":
                case "2-2: DEATH AT 20,000 VOLTS":
                case "2-3: SHEER HEART ATTACK":
                case "2-4: COURT OF THE CORPSE KING":
                case "LAYER 2: LUST":
                    return colors["purple"];
                case "3-1: BELLY OF THE BEAST":
                case "3-2: IN THE FLESH":
                case "LAYER 3: GLUTTONY":
                    return colors["bone"];
                case "4-1: SLAVES TO POWER":
                case "4-2: GOD DAMN THE SUN":
                case "4-3: A SHOT IN THE DARK":
                case "4-4: CLAIR DE SOLEIL":
                case "LAYER 4: GREED":
                case "Dual Wield":
                    return colors["yellow"];
                case "Overheal":
                    return ColorBlindSettings.Instance.overHealColor;
                case "+10,000P":
                    return colors["gold"];
                case "Hard Damage":
                case "Stamina Limiter":
                case "Wall Jump Limiter":
                case "Empty Ammunition":
                    return colors["gray"];
                default:
                    return colors["white"];
            }
        }

        public static Color GetAPMessageColor(ItemFlags flag)
        {
            switch (flag)
            {
                case ItemFlags.Advancement:
                    return colors["ap_item_advancement"];
                case ItemFlags.NeverExclude:
                    return colors["ap_item_neverexclude"];
                case ItemFlags.Trap:
                    return colors["ap_item_trap"];
                default:
                    return colors["ap_item_filler"];
            }
        }

        public static Enums.UKItemType GetTypeFromName(string name)
        {
            switch (name)
            {
                case "Revolver - Piercer":
                case "Revolver - Marksman":
                case "Revolver - Sharpshooter":
                case "Shotgun - Core Eject":
                case "Shotgun - Pump Charge":
                case "Nailgun - Attractor":
                case "Nailgun - Overheat":
                case "Railcannon - Electric":
                case "Railcannon - Screwdriver":
                case "Railcannon - Malicious":
                case "Rocket Launcher - Freezeframe":
                case "Rocket Launcher - S.R.S. Cannon":
                    return Enums.UKItemType.Weapon;
                case "Revolver - Alternate":
                case "Nailgun - Alternate":
                    return Enums.UKItemType.WeaponAlt;
                case "Feedbacker":
                case "Knuckleblaster":
                case "Whiplash":
                    return Enums.UKItemType.Arm;
                case "Stamina Bar":
                case "Wall Jump":
                case "Slide":
                case "Slam":
                    return Enums.UKItemType.Ability;
                case "Blue Skull (0-2)":
                case "Blue Skull (0-S)":
                case "Red Skull (0-S)":
                case "Red Skull (1-1)":
                case "Blue Skull (1-1)":
                case "Blue Skull (1-2)":
                case "Red Skull (1-2)":
                case "Blue Skull (1-3)":
                case "Red Skull (1-3)":
                case "Blue Skull (1-4)":
                case "Blue Skull (2-3)":
                case "Red Skull (2-3)":
                case "Blue Skull (2-4)":
                case "Red Skull (2-4)":
                case "Blue Skull (4-2)":
                case "Red Skull (4-2)":
                case "Blue Skull (4-3)":
                case "Blue Skull (4-4)":
                case "Blue Skull (5-1)":
                case "Blue Skull (5-2)":
                case "Red Skull (5-2)":
                case "Blue Skull (5-3)":
                case "Red Skull (5-3)":
                case "Blue Skull (5-4)":
                case "Red Skull (6-1)":
                    return Enums.UKItemType.Skull;
                case "0-2: THE MEATGRINDER":
                case "0-3: DOUBLE DOWN":
                case "0-4: A ONE-MACHINE ARMY":
                case "0-5: CERBERUS":
                case "1-1: HEART OF THE SUNRISE":
                case "1-2: THE BURNING WORLD":
                case "1-3: HALLS OF SACRED REMAINS":
                case "1-4: CLAIR DE LUNE":
                case "2-1: BRIDGEBURNER":
                case "2-2: DEATH AT 20,000 VOLTS":
                case "2-3: SHEER HEART ATTACK":
                case "2-4: COURT OF THE CORPSE KING":
                case "3-1: BELLY OF THE BEAST":
                case "3-2: IN THE FLESH":
                case "4-1: SLAVES TO POWER":
                case "4-2: GOD DAMN THE SUN":
                case "4-3: A SHOT IN THE DARK":
                case "4-4: CLAIR DE SOLEIL":
                case "5-1: IN THE WAKE OF POSEIDON":
                case "5-2: WAVES OF THE STARLESS SEA":
                case "5-3: SHIP OF FOOLS":
                case "5-4: LEVIATHAN":
                case "6-1: CRY FOR THE WEEPER":
                case "6-2: AESTHETICS OF HATE":
                case "P-1: SOUL SURVIVOR":
                case "P-2: WAIT OF THE WORLD":
                    return Enums.UKItemType.Level;
                case "OVERTURE: THE MOUTH OF HELL":
                case "LAYER 1: LIMBO":
                case "LAYER 2: LUST":
                case "LAYER 3: GLUTTONY":
                case "LAYER 4: GREED":
                case "LAYER 5: WRATH":
                case "LAYER 6: HERESY":
                    return Enums.UKItemType.Layer;
                case "+10,000P":
                    return Enums.UKItemType.Points;
                case "Overheal":
                case "Dual Wield":
                case "Infinite Stamina":
                    return Enums.UKItemType.Powerup;
                case "Hard Damage":
                case "Stamina Limiter":
                case "Wall Jump Limiter":
                case "Empty Ammunition":
                    return Enums.UKItemType.Trap;
                case "Soap":
                    return Enums.UKItemType.Soap;
                case "Secondary Fire - Piercer":
                case "Secondary Fire - Marksman":
                case "Secondary Fire - Sharpshooter":
                case "Secondary Fire - Core Eject":
                case "Secondary Fire - Pump Charge":
                case "Secondary Fire - Attractor":
                case "Secondary Fire - Overheat":
                case "Secondary Fire - Freezeframe":
                case "Secondary Fire - S.R.S. Cannon":
                    return Enums.UKItemType.Fire2;
                default: 
                    return Enums.UKItemType.Points;
            }
        }

        public static string GetWeaponIdFromName(string name)
        {
            switch (name)
            {
                case "Revolver - Piercer":
                case "Secondary Fire - Piercer":
                    return "rev0";
                case "Revolver - Marksman":
                case "Secondary Fire - Marksman":
                    return "rev2";
                case "Revolver - Sharpshooter":
                case "Secondary Fire - Sharpshooter":
                    return "rev1";
                case "Shotgun - Core Eject":
                case "Secondary Fire - Core Eject":
                    return "sho0";
                case "Shotgun - Pump Charge":
                case "Secondary Fire - Pump Charge":
                    return "sho1";
                case "Nailgun - Attractor":
                case "Secondary Fire - Attractor":
                    return "nai0";
                case "Nailgun - Overheat":
                case "Secondary Fire - Overheat":
                    return "nai1";
                case "Railcannon - Electric":
                    return "rai0";
                case "Railcannon - Screwdriver":
                    return "rai1";
                case "Railcannon - Malicious":
                    return "rai2";
                case "Rocket Launcher - Freezeframe":
                case "Secondary Fire - Freezeframe":
                    return "rock0";
                case "Rocket Launcher - S.R.S. Cannon":
                case "Secondary Fire - S.R.S. Cannon":
                    return "rock1";
                case "Revolver - Alternate":
                    return "revalt";
                case "Nailgun - Alternate":
                    return "naialt";
                case "Knuckleblaster":
                    return "arm1";
                case "Whiplash":
                    return "arm2";
                default: return "rev0";
            }
        }
    }
}
