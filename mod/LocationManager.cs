using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using Color = UnityEngine.Color;
using System.Reflection;

namespace ArchipelagoULTRAKILL
{
    public static class LocationManager
    {
        public static GameProgressMoneyAndGear generalProgress;

        public static List<ItemDefinition> ItemDefinitions { get; private set; }

        public static Dictionary<string, long> locations = new Dictionary<string, long>();
        public static Dictionary<string, AItem> shopScouts = new Dictionary<string, AItem>();

        public static List<Message> messages = new List<Message>();
        public static List<Powerup> powerupQueue = new List<Powerup>();
        public static int soapWaiting = 0;

        public static List<QueuedItem> itemQueue = new List<QueuedItem>();

        public static readonly List<string> fire2Weapons = new List<string>()
        {
            "Revolver - Piercer",
            "Revolver - Marksman",
            "Revolver - Sharpshooter",
            "Shotgun - Core Eject",
            "Shotgun - Pump Charge",
            "Shotgun - Sawed-On",
            "Nailgun - Attractor",
            "Nailgun - Overheat",
            "Nailgun - JumpStart",
            "Rocket Launcher - Freezeframe",
            "Rocket Launcher - S.R.S. Cannon",
            "Rocket Launcher - Firestarter"
        };

        public static void RegenerateItemDefinitions()
        {
            ItemDefinitions = new List<ItemDefinition>()
            {
                new ItemDefinition("Revolver - Piercer", UKType.Weapon, () => { return Colors.VariationBlue; }, Core.data.revForm == WeaponForm.Standard ? "rev" : "revalt"),
                new ItemDefinition("Revolver - Marksman", UKType.Weapon, () => { return Colors.VariationGreen; }, Core.data.revForm == WeaponForm.Standard ? "rev" : "revalt"),
                new ItemDefinition("Revolver - Sharpshooter", UKType.Weapon, () => { return Colors.VariationRed; }, Core.data.revForm == WeaponForm.Standard ? "rev" : "revalt"),
                new ItemDefinition("Revolver - Standard", UKType.WeaponAlt, () => { return Colors.WeaponAlt; }, "rev"),
                new ItemDefinition("Revolver - Alternate", UKType.WeaponAlt, () => { return Colors.WeaponAlt; }, "revalt"),

                new ItemDefinition("Shotgun - Core Eject", UKType.Weapon, () => { return Colors.VariationBlue; }, Core.data.shoForm == WeaponForm.Standard ? "sho" : "shoalt"),
                new ItemDefinition("Shotgun - Pump Charge", UKType.Weapon, () => { return Colors.VariationGreen; }, Core.data.shoForm == WeaponForm.Standard ? "sho" : "shoalt"),
                new ItemDefinition("Shotgun - Sawed-On", UKType.Weapon, () => { return Colors.VariationRed; }, Core.data.shoForm == WeaponForm.Standard ? "sho" : "shoalt"),
                new ItemDefinition("Shotgun - Standard", UKType.WeaponAlt, () => { return Colors.WeaponAlt; }, "sho"),
                new ItemDefinition("Shotgun - Alternate", UKType.WeaponAlt, () => { return Colors.WeaponAlt; }, "shoalt"),

                new ItemDefinition("Nailgun - Attractor", UKType.Weapon, () => { return Colors.VariationBlue; }, Core.data.naiForm == WeaponForm.Standard ? "nai" : "naialt"),
                new ItemDefinition("Nailgun - Overheat", UKType.Weapon, () => { return Colors.VariationGreen; }, Core.data.naiForm == WeaponForm.Standard ? "nai" : "naialt"),
                new ItemDefinition("Nailgun - JumpStart", UKType.Weapon, () => { return Colors.VariationRed; }, Core.data.naiForm == WeaponForm.Standard ? "nai" : "naialt"),
                new ItemDefinition("Nailgun - Standard", UKType.WeaponAlt, () => { return Colors.WeaponAlt; }, "nai"),
                new ItemDefinition("Nailgun - Alternate", UKType.WeaponAlt, () => { return Colors.WeaponAlt; }, "naialt"),

                new ItemDefinition("Railcannon - Electric", UKType.Weapon, () => { return Colors.VariationBlue; }, "rai"),
                new ItemDefinition("Railcannon - Screwdriver", UKType.Weapon, () => { return Colors.VariationGreen; }, "rai"),
                new ItemDefinition("Railcannon - Malicious", UKType.Weapon, () => { return Colors.VariationRed; }, "rai"),

                new ItemDefinition("Rocket Launcher - Freezeframe", UKType.Weapon, () => { return Colors.VariationBlue; }, "rock"),
                new ItemDefinition("Rocket Launcher - S.R.S. Cannon", UKType.Weapon, () => { return Colors.VariationGreen; }, "rock"),
                new ItemDefinition("Rocket Launcher - Firestarter", UKType.Weapon, () => { return Colors.VariationRed; }, "rock"),

                new ItemDefinition("Secondary Fire - Piercer", UKType.Fire2, () => { return Colors.VariationBlue; }, "rev0_fire2"),
                new ItemDefinition("Secondary Fire - Marksman", UKType.Fire2, () => { return Colors.VariationGreen; }, "rev2_fire2"),
                new ItemDefinition("Secondary Fire - Sharpshooter", UKType.Fire2, () => { return Colors.VariationRed; }, "rev1_fire2"),
                new ItemDefinition("Secondary Fire - Core Eject", UKType.Fire2, () => { return Colors.VariationBlue; }, "sho0_fire2"),
                new ItemDefinition("Secondary Fire - Pump Charge", UKType.Fire2, () => { return Colors.VariationGreen; }, "sho1_fire2"),
                new ItemDefinition("Secondary Fire - Sawed-On", UKType.Fire2, () => { return Colors.VariationRed; }, "sho2_fire2"),
                new ItemDefinition("Secondary Fire - Attractor", UKType.Fire2, () => { return Colors.VariationBlue; }, "nai0_fire2"),
                new ItemDefinition("Secondary Fire - Overheat", UKType.Fire2, () => { return Colors.VariationGreen; }, Core.data.naiForm == WeaponForm.Standard ? "naistd1_fire2" : "naialt1_fire2"),
                new ItemDefinition("Secondary Fire - JumpStart", UKType.Fire2, () => { return Colors.VariationRed; }, "nai2_fire2"),
                new ItemDefinition("Secondary Fire - Freezeframe", UKType.Fire2, () => { return Colors.VariationBlue; }, "rock0_fire2"),
                new ItemDefinition("Secondary Fire - S.R.S. Cannon", UKType.Fire2, () => { return Colors.VariationGreen; }, "rock1_fire2"),
                new ItemDefinition("Secondary Fire - Firestarter", UKType.Fire2, () => { return Colors.VariationRed; }, "rock2_fire2"),

                new ItemDefinition("Feedbacker", UKType.Arm, () => { return Colors.VariationBlue; }, "arm0"),
                new ItemDefinition("Knuckleblaster", UKType.Arm, () => { return Colors.VariationRed; }, "arm1"),
                new ItemDefinition("Whiplash", UKType.Arm, () => { return Colors.VariationGreen; }, "arm2"),

                new ItemDefinition("Stamina Bar", UKType.Ability, () => { return Colors.Stamina; }, "dash"),
                new ItemDefinition("Wall Jump", UKType.Ability, () => { return Colors.Stamina; }, "walljump"),
                new ItemDefinition("Slide", UKType.Ability, () => { return Colors.Stamina; }, "slide"),
                new ItemDefinition("Slam", UKType.Ability, () => { return Colors.Stamina; }, "slam"),

                new ItemDefinition("Blue Skull (0-2)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (0-S)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (0-S)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Red Skull (1-1)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (1-1)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (1-2)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (1-2)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (1-3)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (1-3)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (1-4)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (2-3)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (2-3)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (2-4)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (2-4)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (4-2)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (4-2)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (4-3)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (4-4)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (5-1)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (5-2)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (5-2)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (5-3)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (5-3)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Red Skull (6-1)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Red Skull (7-1)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (7-1)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (7-2)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Red Skull (7-S)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (7-S)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (0-E)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Red Skull (0-E)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Red Skull (1-E)", UKType.Skull, () => { return Colors.RedSkull; }, "skullr"),
                new ItemDefinition("Blue Skull (1-E)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),
                new ItemDefinition("Blue Skull (P-2)", UKType.Skull, () => { return Colors.BlueSkull; }, "skullb"),

                new ItemDefinition("0-1: INTO THE FIRE", UKType.Level, () => { return Colors.Layer0; }, "layer0"),
                new ItemDefinition("0-2: THE MEATGRINDER", UKType.Level, () => { return Colors.Layer0; }, "layer0"),
                new ItemDefinition("0-3: DOUBLE DOWN", UKType.Level, () => { return Colors.Layer0; }, "layer0"),
                new ItemDefinition("0-4: A ONE-MACHINE ARMY", UKType.Level, () => { return Colors.Layer0; }, "layer0"),
                new ItemDefinition("0-5: CERBERUS", UKType.Level, () => { return Colors.Layer0; }, "layer0"),
                new ItemDefinition("1-1: HEART OF THE SUNRISE", UKType.Level, () => { return Colors.Layer1; }, "layer1"),
                new ItemDefinition("1-2: THE BURNING WORLD", UKType.Level, () => { return Colors.Layer1; }, "layer1"),
                new ItemDefinition("1-3: HALLS OF SACRED REMAINS", UKType.Level, () => { return Colors.Layer1; }, "layer1"),
                new ItemDefinition("1-4: CLAIR DE LUNE", UKType.Level, () => { return Colors.Layer1; }, "layer1"),
                new ItemDefinition("2-1: BRIDGEBURNER", UKType.Level, () => { return Colors.Layer2; }, "layer2"),
                new ItemDefinition("2-2: DEATH AT 20,000 VOLTS", UKType.Level, () => { return Colors.Layer2; }, "layer2"),
                new ItemDefinition("2-3: SHEER HEART ATTACK", UKType.Level, () => { return Colors.Layer2; }, "layer2"),
                new ItemDefinition("2-4: COURT OF THE CORPSE KING", UKType.Level, () => { return Colors.Layer2; }, "layer2"),
                new ItemDefinition("3-1: BELLY OF THE BEAST", UKType.Level, () => { return Colors.Layer3; }, "layer3"),
                new ItemDefinition("3-2: IN THE FLESH", UKType.Level, () => { return Colors.Layer3; }, "layer3"),
                new ItemDefinition("4-1: SLAVES TO POWER", UKType.Level, () => { return Colors.Layer4; }, "layer4"),
                new ItemDefinition("4-2: GOD DAMN THE SUN", UKType.Level, () => { return Colors.Layer4; }, "layer4"),
                new ItemDefinition("4-3: A SHOT IN THE DARK", UKType.Level, () => { return Colors.Layer4; }, "layer4"),
                new ItemDefinition("4-4: CLAIR DE SOLEIL", UKType.Level, () => { return Colors.Layer4; }, "layer4"),
                new ItemDefinition("5-1: IN THE WAKE OF POSEIDON", UKType.Level, () => { return Colors.Layer5; }, "layer5"),
                new ItemDefinition("5-2: WAVES OF THE STARLESS SEA", UKType.Level, () => { return Colors.Layer5; }, "layer5"),
                new ItemDefinition("5-3: SHIP OF FOOLS", UKType.Level, () => { return Colors.Layer5; }, "layer5"),
                new ItemDefinition("5-4: LEVIATHAN", UKType.Level, () => { return Colors.Layer5; }, "layer5"),
                new ItemDefinition("6-1: CRY FOR THE WEEPER", UKType.Level, () => { return Colors.Layer6; }, "layer6"),
                new ItemDefinition("6-2: AESTHETICS OF HATE", UKType.Level, () => { return Colors.Layer6; }, "layer6"),
                new ItemDefinition("7-1: GARDEN OF FORKING PATHS", UKType.Level, () => { return Colors.Layer7; }, "layer7"),
                new ItemDefinition("7-2: LIGHT UP THE NIGHT", UKType.Level, () => { return Colors.Layer7; }, "layer7"),
                new ItemDefinition("7-3: NO SOUND, NO MEMORY", UKType.Level, () => { return Colors.Layer7; }, "layer7"),
                new ItemDefinition("7-4: ...LIKE ANTENNAS TO HEAVEN", UKType.Level, () => { return Colors.Layer7; }, "layer7"),
                new ItemDefinition("0-E: THIS HEAT, AN EVIL HEAT", UKType.Level, () => { return Colors.Encore0; }, "layer0"),
                new ItemDefinition("1-E: ...THEN FELL THE ASHES", UKType.Level, () => { return Colors.Encore1; }, "layer1"),
                new ItemDefinition("P-1: SOUL SURVIVOR", UKType.Level, () => { return Colors.Prime; }, "layer3"),
                new ItemDefinition("P-2: WAIT OF THE WORLD", UKType.Level, () => { return Colors.Prime; }, "layer6"),

                new ItemDefinition("OVERTURE: THE MOUTH OF HELL", UKType.Layer, () => { return Colors.Layer0; }, "layer0"),
                new ItemDefinition("LAYER 1: LIMBO", UKType.Layer, () => { return Colors.Layer1; }, "layer1"),
                new ItemDefinition("LAYER 2: LUST", UKType.Layer, () => { return Colors.Layer2; }, "layer2"),
                new ItemDefinition("LAYER 3: GLUTTONY", UKType.Layer, () => { return Colors.Layer3; }, "layer3"),
                new ItemDefinition("LAYER 4: GREED", UKType.Layer, () => { return Colors.Layer4; }, "layer4"),
                new ItemDefinition("LAYER 5: WRATH", UKType.Layer, () => { return Colors.Layer5; }, "layer5"),
                new ItemDefinition("LAYER 6: HERESY", UKType.Layer, () => { return Colors.Layer6; }, "layer6"),
                new ItemDefinition("LAYER 7: VIOLENCE", UKType.Layer, () => { return Colors.Layer7; }, "layer7"),

                new ItemDefinition("+10,000P", UKType.Points, () => { return Colors.Points; }, "points"),
                new ItemDefinition("Overheal", UKType.Powerup, () => { return Colors.Overheal; }, "overheal"),
                new ItemDefinition("Dual Wield", UKType.Powerup, () => { return Colors.DualWield; }, "dualwield"),
                new ItemDefinition("Infinite Stamina", UKType.Powerup, () => { return Colors.Stamina; }, "infinitestamina"),
                new ItemDefinition("Air Jump", UKType.Powerup, () => { return Colors.DoubleJump; }, "doublejump"),
                new ItemDefinition("Soap", UKType.Soap, () => { return Colors.White; }, "soap"),
                new ItemDefinition("Confusing Aura", UKType.Powerup, () => { return Colors.Confusion; }, "confusion"),
                new ItemDefinition("Quick Charge", UKType.Powerup, () => { return Colors.VariationBlue; }, "quickcharge"),

                new ItemDefinition("Hard Damage", UKType.Trap, () => { return Colors.Trap; }, "overheal"),
                new ItemDefinition("Stamina Limiter", UKType.Trap, () => { return Colors.Trap; }, "infinitestamina"),
                new ItemDefinition("Wall Jump Limiter", UKType.Trap, () => { return Colors.Trap; }, "walljumptrap"),
                new ItemDefinition("Weapon Malfunction", UKType.Trap, () => { return Colors.Trap; }, "quickcharge"),
                new ItemDefinition("Empty Ammunition", UKType.Trap, () => { return Colors.Trap; }, "quickcharge"),
                new ItemDefinition("Radiant Aura", UKType.Trap, () => { return Colors.Trap; }, "radiance"),
                new ItemDefinition("Hands-Free Mode", UKType.Trap, () => { return Colors.Trap; }, "noarms"),
                new ItemDefinition("Short-Term Sandstorm", UKType.Trap, () => { return Colors.Trap; }, "sandstorm"),

                new ItemDefinition("Limbo Switch I", UKType.LimboSwitch, () => { return Colors.Switch; }, "switch1"),
                new ItemDefinition("Limbo Switch II", UKType.LimboSwitch, () => { return Colors.Switch; }, "switch2"),
                new ItemDefinition("Limbo Switch III", UKType.LimboSwitch, () => { return Colors.Switch; }, "switch3"),
                new ItemDefinition("Limbo Switch IV", UKType.LimboSwitch, () => { return Colors.Switch; }, "switch4"),
                new ItemDefinition("Violence Switch I", UKType.ShotgunSwitch, () => { return Colors.Switch; }, "switch1"),
                new ItemDefinition("Violence Switch II", UKType.ShotgunSwitch, () => { return Colors.Switch; }, "switch2"),
                new ItemDefinition("Violence Switch III", UKType.ShotgunSwitch, () => { return Colors.Switch; }, "switch3"),

                new ItemDefinition("Clash Mode", UKType.ClashMode, () => { return Colors.Layer4; }, "clash")
            };
        }

        public static ItemDefinition GetItemDefinition(string name)
        {
            foreach (ItemDefinition itemDefinition in ItemDefinitions)
            {
                if (itemDefinition.Name == name) return itemDefinition;
            }
            Core.Logger.LogWarning($"No item definition for name \"{name}\"");
            return null;
        }

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
            string itemColor = ColorUtility.ToHtmlStringRGB(GetItemDefinition(item.itemName).Color.Invoke());
            string playerColor = ColorUtility.ToHtmlStringRGB(Colors.PlayerOther);
            string text = "";

            if (item.playerName == Core.data.slot_name)
            {
                if (item.type == UKType.Weapon && Core.data.randomizeFire2 == Fire2Options.Progressive && LocationManager.fire2Weapons.Contains(item.itemName))
                {
                    // avoiding a file sharing violation lol
                    FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(LocationManager.GetWeaponIdFromName(item.itemName), BindingFlags.Public | BindingFlags.Instance);
                    if ((int)field.GetValue(LocationManager.generalProgress) > 0)
                    {
                        item.itemName = "Secondary Fire -" + item.itemName.Split(new char[] { '-' }, 2)[1];
                        item.type = UKType.Fire2;
                    }
                }

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
                        generalProgress = GameProgressSaver.GetGeneralProgress();
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
                        if (Core.IsInLevel) HudController.Instance.armIcon.SetActive(true);
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
                            if (SceneHelper.CurrentScene == "Level 1-4") LevelManager.ActivateSkull(LevelManager.skulls.ElementAt(Core.data.unlockedSkulls1_4 - 1).Value);
                        }
                        else if (item.itemName.Contains("5-1"))
                        {
                            if (Core.data.unlockedSkulls5_1 == 3) return;
                            Core.data.unlockedSkulls5_1++;
                            if (SceneHelper.CurrentScene == "Level 5-1") LevelManager.ActivateSkull(LevelManager.skulls.ElementAt(Core.data.unlockedSkulls5_1 - 1).Value);
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
                            if (item.itemName == "Blue Skull (0-S)") LevelManager.ActivateSkull(LevelManager.skulls["SkullBlue"]);
                            else if (item.itemName == "Red Skull (0-S)") LevelManager.ActivateSkull(LevelManager.skulls["SkullRed"]);
                        }
                        else if (SceneHelper.CurrentScene == "Level 7-S")
                        {
                            if (item.itemName == "Blue Skull (7-S)") LevelManager.ActivateSkull(LevelManager.skulls["SkullBlue"]);
                            else if (item.itemName == "Red Skull (7-S)") LevelManager.ActivateSkull(LevelManager.skulls["SkullRed"]);
                        }
                        else if (StatsManager.Instance.levelNumber != 0)
                        {
                            if (Core.IsInLevel && item.itemName.Contains(Core.GetLevelNameFromId(StatsManager.Instance.levelNumber)))
                            {
                                if (item.itemName.Contains("Blue")) LevelManager.ActivateSkull(LevelManager.skulls["SkullBlue"]);
                                else if (item.itemName.Contains("Red"))
                                {
                                    if (Core.CurrentLevelInfo.Id == 101) LevelManager.redDoor.Open();
                                    else LevelManager.ActivateSkull(LevelManager.skulls["SkullRed"]);
                                }
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
                        switch (item.itemName)
                        {
                            case "Overheal":
                                powerupQueue.Add(Powerup.Overheal);
                                break;
                            case "Dual Wield":
                                powerupQueue.Add(Powerup.DualWield);
                                break;
                            case "Infinite Stamina":
                                powerupQueue.Add(Powerup.InfiniteStamina);
                                break;
                            case "Air Jump":
                                powerupQueue.Add(Powerup.DoubleJump);
                                break;
                            case "Confusing Aura":
                                powerupQueue.Add(Powerup.Confusion);
                                break;
                            case "Quick Charge":
                                powerupQueue.Add(Powerup.QuickCharge);
                                break;
                            default:
                                Core.Logger.LogWarning($"Couldn't identify powerup: \"{item.itemName}\"");
                                break;
                        }
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case UKType.Trap:
                        switch (item.itemName)
                        {
                            case "Hard Damage":
                                powerupQueue.Add(Powerup.HardDamage);
                                break;
                            case "Stamina Limiter":
                                powerupQueue.Add(Powerup.StaminaLimiter);
                                break;
                            case "Wall Jump Limiter":
                                powerupQueue.Add(Powerup.WalljumpLimiter);
                                break;
                            case "Empty Ammunition":
                            case "Weapon Malfunction":
                                powerupQueue.Add(Powerup.EmptyAmmo);
                                break;
                            case "Radiant Aura":
                                powerupQueue.Add(Powerup.Radiance);
                                break;
                            case "Hands-Free Mode":
                                powerupQueue.Add(Powerup.NoArms);
                                break;
                            case "Short-Term Sandstorm":
                                powerupQueue.Add(Powerup.Sandstorm);
                                break;
                            default:
                                Core.Logger.LogWarning($"Couldn't identify trap: \"{item.itemName}\"");
                                break;
                        }
                        if (sendingPlayer == null) text = "FOUND: ";
                        else text = "GOT: ";
                        break;

                    case UKType.Soap:
                        soapWaiting++;
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
                        image = GetItemDefinition(item.itemName).Image,
                        color = GetItemDefinition(item.itemName).Color.Invoke(),
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
                        image = GetItemDefinition(item.itemName).Image,
                        color = Colors.Gray,
                        message = text
                    });
                }
            }
            if (save) Core.SaveData();
        }

        public static void GetRandomHint()
        {
            if (Multiworld.Session == null) return;

            var missing = Multiworld.Session.Locations.AllMissingLocations;
            var alreadyHinted = Multiworld.Session.DataStorage.GetHints()
                .Where(h => h.FindingPlayer == Multiworld.Session.ConnectionInfo.Slot)
                .Select(h => h.LocationId);
            var available = missing.Except(alreadyHinted).ToArray();

            if (available.Any())
            {
                var locationId = available[Random.Range(0, available.Length)];

                ScoutedItemInfo info = Multiworld.Session.Locations.ScoutLocationsAsync(true, locationId).Result[locationId];
                string locationName = Multiworld.Session.Locations.GetLocationNameFromId(info.LocationId, Multiworld.Session.Players.GetPlayerInfo(Multiworld.Session.ConnectionInfo.Slot).Game);

                string itemColor = "";
                Color color = Color.white;
                string itemImage = "archipelago";

                if (info.ItemGame == "ULTRAKILL" && GetItemDefinition(info.ItemName) != null)
                {
                    itemColor = ColorUtility.ToHtmlStringRGB(GetItemDefinition(info.ItemName).Color.Invoke());
                    color = GetItemDefinition(info.ItemName).Color.Invoke();
                    itemImage = GetItemDefinition(info.ItemName).Image;
                }
                else
                {
                    itemColor = ColorUtility.ToHtmlStringRGB(GetAPMessageColor(info.Flags));
                    color = GetAPMessageColor(info.Flags);
                }

                string playerColor = ColorUtility.ToHtmlStringRGB(Colors.PlayerOther);
                string locationColor = ColorUtility.ToHtmlStringRGB(GetLocationColor(locationName.Substring(0, 3)));

                string hint = "HINT: <color=#" + itemColor + "FF>";
                hint += info.ItemName.ToUpper() + "</color> ";
                if (Multiworld.Session.Players.GetPlayerName(info.Player) != Core.data.slot_name) 
                    hint += "(<color=#" + playerColor + "FF>" + Multiworld.Session.Players.GetPlayerAlias(info.Player) + "</color>) ";
                hint += "at <color=#" + locationColor + "FF>" + info.LocationName + "</color>";

                messages.Add(new Message
                {
                    image = itemImage,
                    color = color,
                    message = hint
                });
                if (!UIManager.displayingMessage && (Core.IsPlaying || SceneHelper.CurrentScene == "Endless")) Core.uim.StartCoroutine("DisplayMessage");
            }
            else
            {
                Core.Logger.LogWarning("No locations available to hint.");
            }
        }

        public static Color GetLocationColor(string locationName)
        {
            if (locationName.StartsWith("0-E")) return Colors.Encore0;
            if (locationName.StartsWith("1-E")) return Colors.Encore1;
            else if (locationName.StartsWith("0-")) return Colors.Layer0;
            else if (locationName.StartsWith("1-")) return Colors.Layer1;
            else if (locationName.StartsWith("2-")) return Colors.Layer2;
            else if (locationName.StartsWith("3-")) return Colors.Layer3;
            else if (locationName.StartsWith("4-")) return Colors.Layer4;
            else if (locationName.StartsWith("5-")) return Colors.Layer5;
            else if (locationName.StartsWith("6-")) return Colors.Layer6;
            else if (locationName.StartsWith("7-")) return Colors.Layer7;
            else if (locationName.StartsWith("P-")) return Colors.Prime;
            else if (locationName.StartsWith("Shop")) return Colors.Points;
            else return Colors.White;
        }

        public static string GetLocationImage(string locationName)
        {
            if (locationName.StartsWith("0-")) return "layer0";
            else if (locationName.StartsWith("1-")) return "layer1";
            else if (locationName.StartsWith("2-")) return "layer2";
            else if (locationName.StartsWith("3-")) return "layer3";
            else if (locationName.StartsWith("4-")) return "layer4";
            else if (locationName.StartsWith("5-")) return "layer5";
            else if (locationName.StartsWith("6-")) return "layer6";
            else if (locationName.StartsWith("7-")) return "layer7";
            else if (locationName.StartsWith("P-1")) return "layer3";
            else if (locationName.StartsWith("P-2")) return "layer6";
            else if (locationName.Contains("Buy Revolver")) return Core.data.revForm == WeaponForm.Standard ? "rev" : "revalt";
            else if (locationName.Contains("Buy Shotgun")) return Core.data.shoForm == WeaponForm.Standard ? "sho" : "shoalt";
            else if (locationName.Contains("Buy Nailgun")) return Core.data.naiForm == WeaponForm.Standard ? "nai" : "naialt";
            else if (locationName.Contains("Buy Railcannon")) return "rai";
            else if (locationName.Contains("Buy Rocket")) return "rock";
            else if (locationName.StartsWith("Museum")) return "layer1";
            else return "archipelago";
        }

        public static string GetCurrentLevelImage()
        {
            if (Core.CurrentLevelHasInfo)
            {
                switch (Core.CurrentLevelInfo.Layer)
                {
                    case 0: return "layer0";
                    case 1: return "layer1";
                    case 2: return "layer2";
                    case 3: return "layer3";
                    case 4: return "layer4";
                    case 5: return "layer5";
                    case 6: return "layer6";
                    case 7: return "layer7";
                    default: return "confusion";
                }
            }
            else return "confusion";
        }

        public static Color GetAPMessageColor(ItemFlags flag)
        {
            if (flag.HasFlag(ItemFlags.Advancement)) return Colors.ItemAdvancement;
            else if (flag.HasFlag(ItemFlags.NeverExclude)) return Colors.ItemNeverExclude;
            else if (flag.HasFlag(ItemFlags.Trap)) return Colors.ItemTrap;
            else return Colors.ItemFiller;
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
