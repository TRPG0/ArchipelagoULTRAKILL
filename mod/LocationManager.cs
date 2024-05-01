using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArchipelagoULTRAKILL
{
    public static class LocationManager
    {
        public static Dictionary<string, long> locations = new Dictionary<string, long>();
        public static Dictionary<string, AItem> shopScouts = new Dictionary<string, AItem>();

        public static List<Message> messages = new List<Message>();
        public static List<Powerup> powerupQueue = new List<Powerup>();
        public static bool soapWaiting = false;

        public static List<QueuedItem> itemQueue = new List<QueuedItem>();

        public static void CheckLocation(string loc)
        {
            bool isCheating = AssistController.Instance.cheatsEnabled;
            if (SceneHelper.CurrentScene == "CreditsMuseum2") isCheating = false;

            if (!isCheating)
            {
                if (!Core.data.@checked.Contains(loc)) Core.data.@checked.Add(loc);

                if (locations.ContainsKey(loc) && Multiworld.Authenticated)
                {
                    if (SceneHelper.CurrentScene != "Level 7-S") Core.Logger.LogInfo($"Checking location \"{loc}\" | {locations[loc]}");
                    Multiworld.Session.Locations.CompleteLocationChecks(locations[loc]);
                }
                else Core.Logger.LogWarning("Location \"" + loc + "\" does not exist.");
            }
            else
            {
                if (HudMessageReceiver.Instance) HudMessageReceiver.Instance.SendHudMessage("Skipping location check because cheats are enabled!");
                Core.Logger.LogWarning("Skipping location check \"" + loc + "\" because cheats are enabled!");
            }
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
                UKType.Fire2,
                UKType.LimboSwitch,
                UKType.ShotgunSwitch,
                UKType.ClashMode
            };

            if (types.Contains(type)) return true;
            return false;
        }

        public static void GetUKItem(UKItem item, string sendingPlayer = null, bool silent = false, bool save = true)
        {
            string itemColor = ColorUtility.ToHtmlStringRGB(GetUKMessageColor(item.itemName));
            string playerColor = ColorUtility.ToHtmlStringRGB(Colors.PlayerOther);
            string text = "";

            if (item.playerName == Core.data.slot_name)
            {
                switch (item.type)
                {
                    case UKType.Weapon:
                        string wid = GetWeaponIdFromName(item.itemName);
                        int equip = 1;
                        if ((wid.Contains("rev") && Core.data.revForm == WeaponForm.Alternate)
                            || (wid.Contains("sho") && Core.data.shoForm == WeaponForm.Alternate)
                            || (wid.Contains("nai") && Core.data.naiForm == WeaponForm.Alternate))
                            equip = 2;

                        //Core.Logger.LogInfo(equip);
                        GameProgressSaver.AddGear(wid);
                        PrefsManager.Instance.SetInt("weapon." + wid, equip);
                        //Core.logger.LogInfo($"CanGetWeapon = {Core.CanGetWeapon}");
                        if (Core.CanGetWeapon)
                        {
                            GunSetter.Instance.ResetWeapons();
                            GunSetter.Instance.ForceWeapon(wid);
                            if (FistControl.Instance.shopping) GunControl.Instance.NoWeapon();
                        }
                        text = "WEAPON: ";
                        break;

                    case UKType.WeaponAlt:
                        //Core.logger.LogInfo("WeaponAlt: " + GetWeaponIdFromName(item.item_name));
                        //GameProgressSaver.AddGear(GetWeaponIdFromName(item.itemName));
                        string id = GetWeaponIdFromName(item.itemName);
                        if (id == "revalt")
                        {
                            if (Core.data.revForm == WeaponForm.Standard)
                            {
                                Core.data.revalt = true;
                                GameProgressSaver.AddGear(id);
                            }
                            else Core.data.revstd = true;
                        }
                        else if (id == "shoalt")
                        {
                            if (Core.data.shoForm == WeaponForm.Standard)
                            {
                                Core.data.shoalt = true;
                                GameProgressSaver.AddGear(id);
                            }
                            else Core.data.shostd = true;
                        }
                        else if (id == "naialt")
                        {
                            if (Core.data.naiForm == WeaponForm.Standard)
                            {
                                Core.data.naialt = true;
                                GameProgressSaver.AddGear(id);
                            }
                            else Core.data.naistd = true;
                        }
                        else Core.Logger.LogWarning($"Unknown WeaponAlt: {id}");
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
                            if (SceneHelper.CurrentScene == "Level 1-4") LevelManager.skulls.ElementAt(Core.data.unlockedSkulls1_4 - 1).Value.SetActive(true);
                        }
                        else if (item.itemName.Contains("5-1"))
                        {
                            if (Core.data.unlockedSkulls5_1 == 3) return;
                            Core.data.unlockedSkulls5_1++;
                            if (SceneHelper.CurrentScene == "Level 5-1") LevelManager.skulls.ElementAt(Core.data.unlockedSkulls5_1 - 1).Value.SetActive(true);
                        }
                        else if (item.itemName.Contains("0-S"))
                        {
                            if (item.itemName.Contains("Blue")) Core.data.unlockedSkulls.Add("0S_b");
                            else if (item.itemName.Contains("Red")) Core.data.unlockedSkulls.Add("0S_r");
                        }
                        else if (item.itemName.Contains("7-S"))
                        {
                            if (item.itemName.Contains("Blue")) Core.data.unlockedSkulls.Add("7S_b");
                            else if (item.itemName.Contains("Red")) Core.data.unlockedSkulls.Add("7S_r");
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

                        if (SceneHelper.CurrentScene == "Level 0-S")
                        {
                            if (item.itemName == "Blue Skull (0-S)") LevelManager.skulls["SkullBlue"].SetActive(true);
                            else if (item.itemName == "Red Skull (0-S)") LevelManager.skulls["SkullRed"].SetActive(true);
                        }
                        else if (SceneHelper.CurrentScene == "Level 7-S")
                        {
                            if (item.itemName == "Blue Skull (7-S)") LevelManager.skulls["SkullBlue"].SetActive(true);
                            else if (item.itemName == "Red Skull (7-S)") LevelManager.skulls["SkullRed"].SetActive(true);
                        }
                        else if (StatsManager.Instance.levelNumber != 0)
                        {
                            if (Core.IsInLevel && item.itemName.Contains(Core.GetLevelNameFromId(StatsManager.Instance.levelNumber)))
                            {
                                if (item.itemName.Contains("Blue")) LevelManager.skulls["SkullBlue"].SetActive(true);
                                else if (item.itemName.Contains("Red")) LevelManager.skulls["SkullRed"].SetActive(true);
                            }
                        }

                        if (SceneHelper.CurrentScene == "Main Menu")
                        {
                            foreach (SkullIcon skullIcon in Object.FindObjectsOfType<SkullIcon>())
                            {
                                skullIcon.CheckSkull();
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
                        if (Fire2HUD.Instance != null) Fire2HUD.Instance.UpdateCurrentWeapon();
                        text = "UNLOCKED: ";
                        break;

                    case UKType.LimboSwitch:
                        if (item.itemName == "Limbo Switch I") Core.data.limboSwitches[0] = true;
                        else if (item.itemName == "Limbo Switch II") Core.data.limboSwitches[1] = true;
                        else if (item.itemName == "Limbo Switch III") Core.data.limboSwitches[2] = true;
                        else if (item.itemName == "Limbo Switch IV") Core.data.limboSwitches[3] = true;
                        if (Object.FindObjectOfType<LimboSwitchLock>())
                        {
                            Traverse.Create(Object.FindObjectOfType<LimboSwitchLock>()).Field<int>("openedLocks").Value = 0;
                            Object.FindObjectOfType<LimboSwitchLock>().CheckSaves();
                            Traverse.Create(Object.FindObjectOfType<LimboSwitchLock>()).Method("CheckLocks").GetValue();
                        }
                        text = "ACTIVATED: ";
                        break;

                    case UKType.ShotgunSwitch:
                        if (item.itemName == "Violence Switch I") Core.data.shotgunSwitches[0] = true;
                        else if (item.itemName == "Violence Switch II") Core.data.shotgunSwitches[1] = true;
                        else if (item.itemName == "Violence Switch III") Core.data.shotgunSwitches[2] = true;
                        if (Object.FindObjectOfType<LimboSwitchLock>())
                        {
                            Traverse.Create(Object.FindObjectOfType<LimboSwitchLock>()).Field<int>("openedLocks").Value = 0;
                            Object.FindObjectOfType<LimboSwitchLock>().CheckSaves();
                            Traverse.Create(Object.FindObjectOfType<LimboSwitchLock>()).Method("CheckLocks").GetValue();
                        }
                        text = "ACTIVATED: ";
                        break;

                    case UKType.ClashMode:
                        GameProgressMoneyAndGear gameProgress = GameProgressSaver.GetGeneralProgress();
                        gameProgress.clashModeUnlocked = true;
                        Traverse.Create(typeof(GameProgressSaver)).Method("WriteFile", new object[] { Traverse.Create(typeof(GameProgressSaver)).Property<string>("generalProgressPath").Value, gameProgress }).GetValue();
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
            if (save) Core.SaveData();
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
                hint += Multiworld.Session.Items.GetItemName(info.Locations[0].Item).ToUpper() + "</color> ";
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

        public static string GetUKMessageImage(string itemName)
        {
            switch (itemName)
            {
                case "Revolver - Piercer":
                case "Revolver - Marksman":
                case "Revolver - Sharpshooter":
                    if (Core.data.revForm == WeaponForm.Standard) return "rev";
                    else return "revalt";
                case "Secondary Fire - Piercer":
                    return "rev0_fire2";
                case "Secondary Fire - Marksman":
                    return "rev2_fire2";
                case "Secondary Fire - Sharpshooter":
                    return "rev1_fire2";
                case "Shotgun - Core Eject":
                case "Shotgun - Pump Charge":
                case "Shotgun - Sawed-On":
                    if (Core.data.shoForm == WeaponForm.Standard) return "sho";
                    else return "shoalt";
                case "Secondary Fire - Core Eject":
                    return "sho0_fire2";
                case "Secondary Fire - Pump Charge":
                    return "sho1_fire2";
                case "Secondary Fire - Sawed-On":
                    return "sho2_fire2";
                case "Nailgun - Attractor":
                case "Nailgun - Overheat":
                case "Nailgun - JumpStart":
                    if (Core.data.naiForm == WeaponForm.Standard) return "nai";
                    else return "naialt";
                case "Secondary Fire - Attractor":
                    return "nai0_fire2";
                case "Secondary Fire - Overheat":
                    if (Core.data.naiForm == WeaponForm.Standard) return "naistd1_fire2";
                    else return "naialt1_fire2";
                case "Secondary Fire - JumpStart":
                    return "nai2_fire2";
                case "Railcannon - Electric":
                case "Railcannon - Screwdriver":
                case "Railcannon - Malicious":
                    return "rai";
                case "Rocket Launcher - Freezeframe":
                case "Rocket Launcher - S.R.S. Cannon":
                case "Rocket Launcher - Firestarter":
                    return "rock";
                case "Secondary Fire - Freezeframe":
                    return "rock0_fire2";
                case "Secondary Fire - S.R.S. Cannon":
                    return "rock1_fire2";
                case "Secondary Fire - Firestarter":
                    return "rock2_fire2";
                case "Revolver - Standard":
                case "Revolver - Alternate":
                    if (Core.data.revForm == WeaponForm.Standard) return "revalt";
                    else return "rev";
                case "Shotgun - Standard":
                case "Shotgun - Alternate":
                    if (Core.data.shoForm == WeaponForm.Standard) return "shoalt";
                    else return "sho";
                case "Nailgun - Standard":
                case "Nailgun - Alternate":
                    if (Core.data.naiForm == WeaponForm.Standard) return "naialt";
                    else return "nai";
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
                case "Red Skull (7-S)":
                case "Blue Skull (7-S)":
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
                case "Limbo Switch I":
                case "Violence Switch I":
                    return "switch1";
                case "Limbo Switch II":
                case "Violence Switch II":
                    return "switch2";
                case "Limbo Switch III":
                case "Violence Switch III":
                    return "switch3";
                case "Limbo Switch IV":
                    return "switch4";
                case "Clash Mode":
                    return "clash";
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
                case "Shotgun - Sawed-On":
                case "Nailgun - JumpStart":
                case "Railcannon - Malicious":
                case "Rocket Launcher - Firestarter":
                case "Secondary Fire - Sharpshooter":
                case "Secondary Fire - Sawed-On":
                case "Secondary Fire - JumpStart":
                case "Secondary Fire - Firestarter":
                    return ColorBlindSettings.Instance.variationColors[2]; // red
                case "Revolver - Standard":
                case "Revolver - Alternate":
                case "Shotgun - Standard":
                case "Shotgun - Alternate":
                case "Nailgun - Standard":
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
                case "Blue Skull (7-S)":
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
                case "Limbo Switch I":
                case "Limbo Switch II":
                case "Limbo Switch III":
                case "Limbo Switch IV":
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
                case "Violence Switch I":
                case "Violence Switch II":
                case "Violence Switch III":
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
                case "Red Skull (7-S)":
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
                case "Clash Mode":
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

        public static UKType? GetTypeFromName(string name)
        {
            switch (name)
            {
                case "Revolver - Piercer":
                case "Revolver - Marksman":
                case "Revolver - Sharpshooter":
                case "Shotgun - Core Eject":
                case "Shotgun - Pump Charge":
                case "Shotgun - Sawed-On":
                case "Nailgun - Attractor":
                case "Nailgun - Overheat":
                case "Nailgun - JumpStart":
                case "Railcannon - Electric":
                case "Railcannon - Screwdriver":
                case "Railcannon - Malicious":
                case "Rocket Launcher - Freezeframe":
                case "Rocket Launcher - S.R.S. Cannon":
                case "Rocket Launcher - Firestarter":
                    return UKType.Weapon;
                case "Revolver - Standard":
                case "Revolver - Alternate":
                case "Shotgun - Standard":
                case "Shotgun - Alternate":
                case "Nailgun - Standard":
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
                case "Red Skull (7-S)":
                case "Blue Skull (7-S)":
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
                case "Secondary Fire - Sawed-On":
                case "Secondary Fire - Attractor":
                case "Secondary Fire - Overheat":
                case "Secondary Fire - JumpStart":
                case "Secondary Fire - Freezeframe":
                case "Secondary Fire - S.R.S. Cannon":
                case "Secondary Fire - Firestarter":
                    return UKType.Fire2;
                case "Limbo Switch I":
                case "Limbo Switch II":
                case "Limbo Switch III":
                case "Limbo Switch IV":
                    return UKType.LimboSwitch;
                case "Violence Switch I":
                case "Violence Switch II":
                case "Violence Switch III":
                    return UKType.ShotgunSwitch;
                case "Clash Mode":
                    return UKType.ClashMode;
                default: 
                    return null;
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
                case "Shotgun - Sawed-On":
                case "Secondary Fire - Sawed-On":
                    return "sho2";
                case "Nailgun - Attractor":
                case "Secondary Fire - Attractor":
                    return "nai0";
                case "Nailgun - Overheat":
                case "Secondary Fire - Overheat":
                    return "nai1";
                case "Nailgun - JumpStart":
                case "Secondary Fire - JumpStart":
                    return "nai2";
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
                case "Rocket Launcher - Firestarter":
                case "Secondary Fire - Firestarter":
                    return "rock2";
                case "Revolver - Standard":
                case "Revolver - Alternate":
                    return "revalt";
                case "Shotgun - Standard":
                case "Shotgun - Alternate":
                    return "shoalt";
                case "Nailgun - Standard":
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
