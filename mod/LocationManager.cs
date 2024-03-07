using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using ArchipelagoULTRAKILL.Structures;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArchipelagoULTRAKILL
{
    public static class LocationManager
    {
        public static Dictionary<string, Location> locations = new Dictionary<string, Location>();

        public static List<Message> messages = new List<Message>();
        public static List<Powerup> powerupQueue = new List<Powerup>();
        public static bool soapWaiting = false;

        public static List<QueuedItem> itemQueue = new List<QueuedItem>();

        public static void CheckLocation(string loc)
        {
            if (!AssistController.Instance.cheatsEnabled)
            {
                Core.Logger.LogInfo("Checking location \"" + loc + "\"");
                if (!Core.data.@checked.Contains(loc)) Core.data.@checked.Add(loc);

                if (locations.ContainsKey(loc))
                {
                    if (Multiworld.Authenticated) Multiworld.Session.Locations.CompleteLocationChecks(locations[loc].ap_id);
                    if (locations[loc].item is UKItem ukitem && !locations[loc].@checked)
                    {
                        if (CanGetItemTypeWhileNotPlaying(ukitem.type)) GetUKItem(ukitem);
                        else itemQueue.Add(new QueuedItem(ukitem, null));
                    }
                    else if (locations[loc].item is APItem apitem && !locations[loc].@checked) AddAPItemMessage(apitem);
                    locations[loc].@checked = true;
                }
                else Core.Logger.LogWarning("Location \"" + loc + "\" does not exist.");
            }
            else Core.Logger.LogWarning("Tried to check location \"" + loc + "\", but cheats are enabled!");
        }

        public static bool CanGetItemTypeWhileNotPlaying(UKType type)
        {
            if (!Core.IsInLevel || (Core.IsInLevel && Core.IsPitFalling))
            {
                if (type == UKType.Level || type == UKType.Layer || type == UKType.Skull) return true;
            }
            return false;
        }

        public static bool ShouldGetItemAgain(UKType type)
        {
            List<UKType> types = new List<UKType>()
            {
                UKType.Weapon,
                UKType.WeaponAlt,
                UKType.Arm,
                UKType.Skull,
                UKType.Level,
                UKType.Layer,
                UKType.Fire2
            };

            if (types.Contains(type)) return true;
            return false;
        }

        public static void GetUKItem(UKItem item, string sendingPlayer = null, bool silent = false)
        {
            string itemColor = ColorUtility.ToHtmlStringRGB(GetUKMessageColor(item.itemName));
            string playerColor = ColorUtility.ToHtmlStringRGB(Colors.PlayerOther);
            string text = "";

            if (item.playerName == Core.data.slot_name)
            {
                switch (item.type)
                {
                    case UKType.Weapon:
                        GameProgressSaver.AddGear(GetWeaponIdFromName(item.itemName));
                        PrefsManager.Instance.SetInt("weapon." + GetWeaponIdFromName(item.itemName), 1);
                        //Core.logger.LogInfo($"CanGetWeapon = {Core.CanGetWeapon}");
                        if (Core.CanGetWeapon)
                        {
                            GunSetter.Instance.ResetWeapons();
                            GunSetter.Instance.ForceWeapon(GetWeaponIdFromName(item.itemName));
                            if (FistControl.Instance.shopping) GunControl.Instance.NoWeapon();
                        }
                        text = "WEAPON: ";
                        break;

                    case UKType.WeaponAlt:
                        //Core.logger.LogInfo("WeaponAlt: " + GetWeaponIdFromName(item.item_name));
                        GameProgressSaver.AddGear(GetWeaponIdFromName(item.itemName));
                        text = "WEAPON: ";
                        break;

                    case UKType.Arm:
                        if (item.itemName == "Feedbacker")
                        {
                            Core.data.hasArm = true;
                            PrefsManager.Instance.SetInt("weapon.arm0", 1);
                            if (Core.CanGetWeapon)
                            {
                                FistControl.Instance.ResetFists();
                                FistControl.Instance.ForceArm(int.Parse(GetWeaponIdFromName(item.itemName).Substring(3, 1)), false);
                            }
                        }
                        else
                        {
                            if (!Core.data.hasArm && item.itemName == "Knuckleblaster") PrefsManager.Instance.SetInt("weapon.arm0", 0);
                            GameProgressSaver.AddGear(GetWeaponIdFromName(item.itemName));
                            PrefsManager.Instance.SetInt("weapon." + GetWeaponIdFromName(item.itemName), 1);
                            if (Core.CanGetWeapon)
                            {
                                FistControl.Instance.ResetFists();
                                FistControl.Instance.ForceArm(int.Parse(GetWeaponIdFromName(item.itemName).Substring(3, 1)), false);
                            }
                        }
                        text = "ARM: ";
                        break;

                    case UKType.Ability:
                        if (item.itemName == "Stamina Bar" && Core.data.dashes < 3) Core.data.dashes++;
                        else if (item.itemName == "Wall Jump" && Core.data.walljumps < 3) Core.data.walljumps++;
                        else if (item.itemName == "Slide") Core.data.canSlide = true;
                        else if (item.itemName == "Slam") Core.data.canSlam = true;
                        text = "ABILITY: ";
                        break;

                    case UKType.Skull:
                        if (!Core.data.randomizeSkulls) return;
                        if (item.itemName.Contains("1-4"))
                        {
                            if (Core.data.unlockedSkulls1_4 == 4) return;
                            Core.data.unlockedSkulls1_4++;
                        }
                        else if (item.itemName.Contains("5-1"))
                        {
                            if (Core.data.unlockedSkulls5_1 == 3) return;
                            Core.data.unlockedSkulls5_1++;
                        }
                        else if (item.itemName.Contains("0-S"))
                        {
                            if (item.itemName.Contains("Blue")) Core.data.unlockedSkulls.Add("S_b");
                            else if (item.itemName.Contains("Red")) Core.data.unlockedSkulls.Add("S_r");
                        }
                        else
                        {
                            Core.Logger.LogInfo(item.itemName.Substring(item.itemName.Length - 4, 3));
                            string skull = Core.GetLevelIdFromName(item.itemName.Substring(item.itemName.Length - 4, 3)).ToString();
                            if (item.itemName.Contains("Blue")) skull += "_b";
                            else if (item.itemName.Contains("Red")) skull += "_r";
                            Core.Logger.LogInfo("Skull: " + skull);
                            Core.data.unlockedSkulls.Add(skull);
                        }
                        if (SceneHelper.CurrentScene == "Level 1-4") LevelManager.skulls.ElementAt(Core.data.unlockedSkulls1_4 - 1).Value.SetActive(true);
                        else if (SceneHelper.CurrentScene == "Level 5-1") LevelManager.skulls.ElementAt(Core.data.unlockedSkulls5_1 - 1).Value.SetActive(true);
                        else if (SceneHelper.CurrentScene == "Level 0-S")
                        {
                            if (item.itemName == "Blue Skull (0-S)") LevelManager.skulls["SkullBlue"].SetActive(true);
                            else if (item.itemName == "Red Skull (0-S)") LevelManager.skulls["SkullRed"].SetActive(true);
                        }
                        else if (StatsManager.Instance.levelNumber != 0)
                        {
                            if (Core.IsInLevel && item.itemName.Contains(Core.GetLevelNameFromId(StatsManager.Instance.levelNumber)))
                            {
                                if (item.itemName.Contains("Blue")) LevelManager.skulls["SkullBlue"].SetActive(true);
                                else if (item.itemName.Contains("Red")) LevelManager.skulls["SkullRed"].SetActive(true);
                            }
                        }
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case UKType.Level:
                        Core.data.unlockedLevels.Add(item.itemName.Substring(0, 3));
                        text = "UNLOCKED: ";
                        break;

                    case UKType.Layer:
                        int layer;
                        if (item.itemName.Contains("OVERTURE")) layer = 0;
                        else layer = int.Parse(item.itemName[6].ToString());
                        //Core.Logger.LogInfo(layer);

                        foreach (LevelInfo info in Core.levelInfos)
                        {
                            if (info.Layer == layer) Core.data.unlockedLevels.Add(info.Name);
                        }

                        text = "UNLOCKED: ";
                        break;

                    case UKType.Points:
                        GameProgressSaver.AddMoney(10000);
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case UKType.Powerup:
                        if (item.itemName == "Overheal") powerupQueue.Add(Powerup.Overheal);
                        else if (item.itemName == "Dual Wield") powerupQueue.Add(Powerup.DualWield);
                        else if (item.itemName == "Infinite Stamina") powerupQueue.Add(Powerup.InfiniteStamina);
                        else if (item.itemName == "Air Jump") powerupQueue.Add(Powerup.DoubleJump);
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case UKType.Trap:
                        if (item.itemName == "Hard Damage") powerupQueue.Add(Powerup.HardDamage);
                        else if (item.itemName == "Stamina Limiter") powerupQueue.Add(Powerup.StaminaLimiter);
                        else if (item.itemName == "Wall Jump Limiter") powerupQueue.Add(Powerup.WalljumpLimiter);
                        else if (item.itemName == "Empty Ammunition") powerupQueue.Add(Powerup.EmptyAmmo);
                        else if (item.itemName == "Radiant Aura") powerupQueue.Add(Powerup.Radiance);
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case UKType.Soap:
                        if (Core.IsPlaying) Core.SpawnSoap();
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case UKType.Fire2:
                        Core.data.unlockedFire2.Add(GetWeaponIdFromName(item.itemName));
                        text = "UNLOCKED: ";
                        break;

                    default: break;
                }

                text += "<color=#" + itemColor + "FF>" + item.itemName.ToUpper() + "</color>";
                if (sendingPlayer != null) text += " (<color=#" + playerColor + "FF>" + sendingPlayer + "</color>)";
                if (!silent)
                {
                    messages.Add(new Message
                    {
                        image = GetUKMessageImage(item.itemName),
                        color = GetUKMessageColor(item.itemName),
                        message = text
                    });
                }
            }
            else
            {
                text = "FOUND: <color=#" + itemColor + "FF>" + item.itemName.ToUpper() + "</color> (<color=#" + playerColor + "FF>" + item.playerName + "</color>)";
                if (!silent)
                {
                    messages.Add(new Message
                    {
                        image = GetUKMessageImage(item.itemName),
                        color = Colors.Gray,
                        message = text
                    });
                }
            }
            Core.SaveData();
        }

        public static void GetRandomHint()
        {
            if (!Multiworld.Authenticated) return;

            var missing = Multiworld.Session.Locations.AllMissingLocations;
            var alreadyHinted = Multiworld.Session.DataStorage.GetHints()
                .Where(h => h.FindingPlayer == Multiworld.Session.ConnectionInfo.Slot)
                .Select(h => h.LocationId);
            var available = missing.Except(alreadyHinted).ToArray();

            if (available.Any())
            {
                var locationId = available[Random.Range(0, available.Length)];

                Multiworld.Session.Locations.ScoutLocationsAsync(true, locationId);
                LocationInfoPacket info = Multiworld.Session.Locations.ScoutLocationsAsync(false, locationId).Result;

                string itemColor = ColorUtility.ToHtmlStringRGB(GetUKMessageColor(Multiworld.Session.Items.GetItemName(info.Locations[0].Item)));
                Color color = GetUKMessageColor(Multiworld.Session.Items.GetItemName(info.Locations[0].Item));
                if (itemColor == "FFFFFF")
                {
                    itemColor = ColorUtility.ToHtmlStringRGB(GetAPMessageColor(info.Locations[0].Flags));
                    color = GetAPMessageColor(info.Locations[0].Flags);
                }
                string playerColor = ColorUtility.ToHtmlStringRGB(Colors.PlayerOther);
                string locationColor = ColorUtility.ToHtmlStringRGB(GetUKMessageColor(Multiworld.Session.Locations.GetLocationNameFromId(info.Locations[0].Location).Substring(0, 3)));

                string hint = "HINT: <color=#" + itemColor + "FF>";
                hint += Multiworld.Session.Items.GetItemName(info.Locations[0].Item) + "</color> ";
                if (Multiworld.Session.Players.GetPlayerName(info.Locations[0].Player) != Core.data.slot_name) 
                    hint += "(<color=#" + playerColor + "FF>" + Multiworld.Session.Players.GetPlayerAlias(info.Locations[0].Player) + "</color>) ";
                hint += "at <color=#" + locationColor + "FF>" + Multiworld.Session.Locations.GetLocationNameFromId(info.Locations[0].Location) + "</color>";

                messages.Add(new Message
                {
                    image = GetUKMessageImage(Multiworld.Session.Items.GetItemName(info.Locations[0].Item)),
                    color = color,
                    message = hint
                });
                if (!UIManager.displayingMessage && Core.IsPlaying) Core.uim.StartCoroutine("DisplayMessage");
            }
            else
            {
                Core.Logger.LogWarning("No locations available to hint.");
            }
        }

        public static void AddAPItemMessage(APItem item)
        {
            string itemColor = ColorUtility.ToHtmlStringRGB(GetAPMessageColor(item.type));
            string playerColor = ColorUtility.ToHtmlStringRGB(Colors.PlayerOther);
            string text = "FOUND: <color=#" + itemColor + "FF>" + item.itemName + "</color> (<color=#" + playerColor + "FF>" + item.playerName + "</color>)";

            messages.Add(new Message
            {
                image = "archipelago",
                color = GetAPMessageColor(item.type),
                message = text
            });
            if (!UIManager.displayingMessage && Core.IsPlaying) Core.uim.StartCoroutine("DisplayMessage");
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
                case "Red Skull (7-1)":
                case "Blue Skull (7-1)":
                case "Red Skull (7-2)":
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
                case "7-1: GARDEN OF FORKING PATHS":
                case "7-2: LIGHT UP THE NIGHT":
                case "7-3: NO SOUND, NO MEMORY":
                case "7-4: ...LIKE ANTENNAS TO HEAVEN":
                case "LAYER 7: VIOLENCE":
                    return "layer7";
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
                case "Air Jump":
                    return "doublejump";
                case "Radiant Aura":
                    return "radiance";
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
                    return Colors.WeaponAlt;
                case "Stamina Bar":
                case "Wall Jump":
                case "Slide":
                case "Slam":
                case "Infinite Stamina":
                    return ColorBlindSettings.Instance.staminaColor;
                case "Feedbacker":
                    return Colors.Arm0;
                case "5-1: IN THE WAKE OF POSEIDON":
                case "5-2: WAVES OF THE STARLESS SEA":
                case "5-3: SHIP OF FOOLS":
                case "5-4: LEVIATHAN":
                case "5-1":
                case "5-2":
                case "5-3":
                case "5-4":
                case "LAYER 5: WRATH":
                    return Colors.Layer5;
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
                case "Blue Skull (7-1)":
                    return Colors.BlueSkull;
                case "Whiplash":
                    return Colors.Arm2;
                case "1-1: HEART OF THE SUNRISE":
                case "1-2: THE BURNING WORLD":
                case "1-3: HALLS OF SACRED REMAINS":
                case "1-4: CLAIR DE LUNE":
                case "1-1":
                case "1-2":
                case "1-3":
                case "1-4":
                case "LAYER 1: LIMBO":
                    return Colors.Layer1;
                case "Knuckleblaster":
                    return Colors.Arm1;
                case "6-1: CRY FOR THE WEEPER":
                case "6-2: AESTHETICS OF HATE":
                case "6-1":
                case "6-2":
                case "LAYER 6: HERESY":
                    return Colors.Layer6;
                case "7-1: GARDEN OF FORKING PATHS":
                case "7-2: LIGHT UP THE NIGHT":
                case "7-3: NO SOUND, NO MEMORY":
                case "7-4: ...LIKE ANTENNAS TO HEAVEN":
                case "7-1":
                case "7-2":
                case "7-3":
                case "7-4":
                case "LAYER 7: VIOLENCE":
                    return Colors.Layer7;
                case "P-1: SOUL SURVIVOR":
                case "P-2: WAIT OF THE WORLD":
                    return Colors.Prime;
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
                case "Red Skull (7-1)":
                case "Red Skull (7-2)":
                    return Colors.RedSkull;
                case "0-2: THE MEATGRINDER":
                case "0-3: DOUBLE DOWN":
                case "0-4: A ONE-MACHINE ARMY":
                case "0-5: CERBERUS":
                case "OVERTURE: THE MOUTH OF HELL":
                case "0-1":
                case "0-2":
                case "0-3":
                case "0-4":
                case "0-5":
                    return Colors.Layer0;
                case "2-1: BRIDGEBURNER":
                case "2-2: DEATH AT 20,000 VOLTS":
                case "2-3: SHEER HEART ATTACK":
                case "2-4: COURT OF THE CORPSE KING":
                case "LAYER 2: LUST":
                case "2-1":
                case "2-2":
                case "2-3":
                case "2-4":
                    return Colors.Layer2;
                case "3-1: BELLY OF THE BEAST":
                case "3-2: IN THE FLESH":
                case "3-1":
                case "3-2":
                case "LAYER 3: GLUTTONY":
                    return Colors.Layer3;
                case "4-1: SLAVES TO POWER":
                case "4-2: GOD DAMN THE SUN":
                case "4-3: A SHOT IN THE DARK":
                case "4-4: CLAIR DE SOLEIL":
                case "LAYER 4: GREED":
                case "4-1":
                case "4-2":
                case "4-3":
                case "4-4":
                    return Colors.Layer4;
                case "Dual Wield":
                    return Colors.DualWield;
                case "Air Jump":
                    return Colors.DoubleJump;
                case "Overheal":
                    return ColorBlindSettings.Instance.overHealColor;
                case "+10,000P":
                case "Sho":
                    return Colors.Points;
                case "Hard Damage":
                case "Stamina Limiter":
                case "Wall Jump Limiter":
                case "Empty Ammunition":
                case "Radiant Aura":
                    return Colors.Trap;
                default:
                    return Colors.White;
            }
        }

        public static Color GetAPMessageColor(ItemFlags flag)
        {
            switch (flag)
            {
                case ItemFlags.Advancement:
                    return Colors.ItemAdvancement;
                case ItemFlags.NeverExclude:
                    return Colors.ItemNeverExclude;
                case ItemFlags.Trap:
                    return Colors.ItemTrap;
                default:
                    return Colors.ItemFiller;
            }
        }

        public static UKType GetTypeFromName(string name)
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
                    return UKType.Weapon;
                case "Revolver - Alternate":
                case "Nailgun - Alternate":
                    return UKType.WeaponAlt;
                case "Feedbacker":
                case "Knuckleblaster":
                case "Whiplash":
                    return UKType.Arm;
                case "Stamina Bar":
                case "Wall Jump":
                case "Slide":
                case "Slam":
                    return UKType.Ability;
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
                case "Red Skull (7-1)":
                case "Blue Skull (7-1)":
                case "Red Skull (7-2)":
                    return UKType.Skull;
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
                case "7-1: GARDEN OF FORKING PATHS":
                case "7-2: LIGHT UP THE NIGHT":
                case "7-3: NO SOUND, NO MEMORY":
                case "7-4: ...LIKE ANTENNAS TO HEAVEN":
                case "P-1: SOUL SURVIVOR":
                case "P-2: WAIT OF THE WORLD":
                    return UKType.Level;
                case "OVERTURE: THE MOUTH OF HELL":
                case "LAYER 1: LIMBO":
                case "LAYER 2: LUST":
                case "LAYER 3: GLUTTONY":
                case "LAYER 4: GREED":
                case "LAYER 5: WRATH":
                case "LAYER 6: HERESY":
                case "LAYER 7: VIOLENCE":
                    return UKType.Layer;
                case "+10,000P":
                    return UKType.Points;
                case "Overheal":
                case "Dual Wield":
                case "Infinite Stamina":
                case "Air Jump":
                    return UKType.Powerup;
                case "Hard Damage":
                case "Stamina Limiter":
                case "Wall Jump Limiter":
                case "Empty Ammunition":
                case "Radiant Aura":
                    return UKType.Trap;
                case "Soap":
                    return UKType.Soap;
                case "Secondary Fire - Piercer":
                case "Secondary Fire - Marksman":
                case "Secondary Fire - Sharpshooter":
                case "Secondary Fire - Core Eject":
                case "Secondary Fire - Pump Charge":
                case "Secondary Fire - Attractor":
                case "Secondary Fire - Overheat":
                case "Secondary Fire - Freezeframe":
                case "Secondary Fire - S.R.S. Cannon":
                    return UKType.Fire2;
                default: 
                    return UKType.Points;
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
