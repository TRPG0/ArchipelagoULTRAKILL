using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using Newtonsoft.Json;
using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Config
{
    public static class TestConfig
    {
        public static bool Done { get; private set; } = false;

        public static ConfigPanel testPanel;
        public static ButtonField activateButton;
        public static ConfigHeader buttonResult;
        public static IntField dashes;
        public static IntField walljumps;
        public static BoolField slide;
        public static BoolField slam;
        public static EnumField<Fire2WeaponId> weaponEnum;
        public static ButtonArrayField fire2Buttons;

        public enum Fire2WeaponId
        {
            rev0,
            rev2,
            rev1,
            sho0,
            sho1,
            sho2,
            nai0,
            nai1,
            nai2,
            rock0,
            rock1,
            rock2
        }

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            testPanel = new ConfigPanel(config.rootPanel, "TEST MODE", "testPanel");

            string header = "Enable test mode to easily change the player's abilities at any time for accurately testing strategies." +
                "\n\nTest mode cannot be enabled on a save slot that has Archipelago save data or regular save data." +
                "\n\nAfter enabling test mode, this save slot cannot be used to connect to a multiworld unless deleted.";
            new ConfigHeader(testPanel, header, 14);
            activateButton = new ButtonField(testPanel, "ENABLE TEST MODE", "activateButton");
            activateButton.onClick += () =>
            {
                string result = SaveTestModeData();
                buttonResult.text = result;
                buttonResult.hidden = false;
            };

            buttonResult = new ConfigHeader(testPanel, "");
            buttonResult.hidden = true;

            new ConfigHeader(testPanel, "In addition to this config menu, these values can also be changed by typing commands into the in-game console, which can be opened by pressing F8.", 14);
            dashes = new IntField(testPanel, "dashes", "dashes", 0, 0, 3, true, false);
            dashes.postValueChangeEvent += (int i) =>
            {
                if (Core.TestMode ?? false) Core.data.dashes = i;
            };

            walljumps = new IntField(testPanel, "walljumps", "walljumps", 0, 0, 3, true, false);
            walljumps.postValueChangeEvent += (int i) =>
            {
                if (Core.TestMode ?? false) Core.data.walljumps = i;
            };

            slide = new BoolField(testPanel, "slide", "slide", false, false);
            slide.postValueChangeEvent += (bool b) =>
            {
                if (Core.TestMode ?? false) Core.data.canSlide = b;
            };

            slam = new BoolField(testPanel, "slam", "slam", false, false);
            slam.postValueChangeEvent += (bool b) =>
            {
                if (Core.TestMode ?? false) Core.data.canSlam = b;
            };

            new ConfigHeader(testPanel, "SECONDARY FIRE");
            weaponEnum = new EnumField<Fire2WeaponId>(testPanel, "SELECTED WEAPON", "weaponEnum", Fire2WeaponId.rev0, false);
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.rev0, "Revolver - Piercer");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.rev2, "Revolver - Marksman");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.rev1, "Revolver - Sharpshooter");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.sho0, "Shotgun - Core Eject");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.sho1, "Shotgun - Pump Charge");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.sho2, "Shotgun - Sawed-On");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.nai0, "Nailgun - Attractor");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.nai1, "Nailgun - Overheat");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.nai2, "Nailgun - JumpStart");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.rock0, "Rocket Launcher - Freezeframe");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.rock1, "Rocket Launcher - S.R.S. Cannon");
            weaponEnum.SetEnumDisplayName(Fire2WeaponId.rock2, "Rocket Launcher - Firestarter");

            fire2Buttons = new ButtonArrayField(testPanel, "fire2Buttons", 2, new float[] { 0.5f, 0.5f }, new string[] { "UNLOCK", "LOCK" });
            fire2Buttons.OnClickEventHandler(0).onClick += () =>
            {
                if (Core.TestMode ?? false)
                {
                    string weapon = weaponEnum.value.ToString();
                    if (!Core.data.unlockedFire2.Contains(weapon))
                    {
                        Core.data.unlockedFire2.Add(weapon);
                        Fire2HUD.Instance?.UpdateCurrentWeapon();
                    }
                }
            };
            fire2Buttons.OnClickEventHandler(1).onClick += () =>
            {
                if (Core.TestMode ?? false)
                {
                    string weapon = weaponEnum.value.ToString();
                    if (Core.data.unlockedFire2.Contains(weapon))
                    {
                        Core.data.unlockedFire2.Remove(weapon);
                        Fire2HUD.Instance?.UpdateCurrentWeapon();
                    }
                }
            };
        }

        public static void SetTestModeActive(bool? active)
        {
            if (active.HasValue)
            {
                if (active.Value)
                {
                    testPanel.interactable = true;
                    activateButton.interactable = false;
                    buttonResult.text = "";
                    buttonResult.hidden = true;
                    dashes.interactable = true;
                    walljumps.interactable = true;
                    slide.interactable = true;
                    slam.interactable = true;
                    weaponEnum.interactable = true;
                    fire2Buttons.interactable = true;
                    Core.TestMode = true;
                }
                else
                {
                    testPanel.interactable = false;
                    activateButton.interactable = false;
                    buttonResult.text = "Can't activate test mode for current slot.";
                    buttonResult.hidden = false;
                    dashes.interactable = false;
                    walljumps.interactable = false;
                    slide.interactable = false;
                    slam.interactable = false;
                    weaponEnum.interactable = false;
                    fire2Buttons.interactable = false;
                    Core.TestMode = false;
                }
            }
            else
            {
                testPanel.interactable = true;
                activateButton.interactable = true;
                buttonResult.text = "";
                buttonResult.hidden = true;
                dashes.interactable = false;
                walljumps.interactable = false;
                slide.interactable = false;
                slam.interactable = false;
                weaponEnum.interactable = false;
                fire2Buttons.interactable = false;
                Core.TestMode = null;
            }
        }

        public static string SaveTestModeData()
        {
            if (GameProgressSaver.GetTutorial() || GameProgressSaver.GetIntro()) return "Save data already exists in current slot.";
            int slot = GameProgressSaver.currentSlot + 1;
            string filePath = Path.Combine(GameProgressSaver.BaseSavePath, string.Format("Slot{0}", slot)) + "\\archipelago.json";
            if (File.Exists(filePath)) return $"JSON data already exists at file path for slot {slot}.";

            Core.data = new Data();
            Core.data.start = "0-1";
            Core.data.goal = "8-4";
            Core.data.goalRequirement = 0;
            Core.data.hasArm = true;
            Core.data.dashes = 3;
            Core.data.walljumps = 3;
            Core.data.canSlide = true;
            Core.data.canSlam = true;

            UnlockAllLevels();
            UnlockAllWeapons(true);

            Data save = Core.data;

            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(save));
            File.WriteAllBytes(filePath, bytes);

            SetTestModeActive(true);
            if (UIManager.menuIcon != null) UIManager.menuIcon.GetComponent<Image>().color = Color.magenta;
            return $"Save slot {slot} is now in test mode.";
        }

        public static void UnlockAllLevels()
        {
            PrefsManager.Instance.SetInt("difficulty", 4);
            GameProgressSaver.SetIntro(true);
            GameProgressSaver.SetTutorial(true);
            GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Revolver);
            GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Shotgun);
            GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Nailgun);
            GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.Railcannon);
            GameProgressSaver.UnlockWeaponCustomization(GameProgressSaver.WeaponCustomizationType.RocketLauncher);
            GameProgressSaver.SaveProgress(34);
            GameProgressSaver.SetEncoreProgress(101);
            GameProgressSaver.SetPrime(1, 1);
            GameProgressSaver.SetPrime(2, 1);

            foreach (LevelInfo info in Core.levelInfos) Core.data.unlockedLevels.Add(info.Name);
            foreach (LevelInfo info in Core.secretMissionInfos) Core.data.unlockedLevels.Add(info.Name);
        }

        public static void UnlockAllWeapons(bool fire2)
        {
            GameProgressSaver.AddGear("rev0");
            GameProgressSaver.AddGear("rev1");
            GameProgressSaver.AddGear("rev2");
            GameProgressSaver.AddGear("sho0");
            GameProgressSaver.AddGear("sho1");
            GameProgressSaver.AddGear("sho2");
            GameProgressSaver.AddGear("nai0");
            GameProgressSaver.AddGear("nai1");
            GameProgressSaver.AddGear("nai2");
            GameProgressSaver.AddGear("rai0");
            GameProgressSaver.AddGear("rai1");
            GameProgressSaver.AddGear("rai2");
            GameProgressSaver.AddGear("rock0");
            GameProgressSaver.AddGear("rock1");
            GameProgressSaver.AddGear("rock2");
            GameProgressSaver.AddGear("arm0");
            GameProgressSaver.AddGear("arm1");
            GameProgressSaver.AddGear("arm2");

            Core.data.revstd = true;
            Core.data.revalt = true;
            Core.data.shostd = true;
            Core.data.shoalt = true;
            Core.data.naistd = true;
            Core.data.naialt = true;

            Core.data.randomizeFire2 = Fire2Options.Split;
            if (fire2)
            {
                Core.data.unlockedFire2.Add("rev0");
                Core.data.unlockedFire2.Add("rev1");
                Core.data.unlockedFire2.Add("rev2");
                Core.data.unlockedFire2.Add("sho0");
                Core.data.unlockedFire2.Add("sho1");
                Core.data.unlockedFire2.Add("sho2");
                Core.data.unlockedFire2.Add("nai0");
                Core.data.unlockedFire2.Add("nai1");
                Core.data.unlockedFire2.Add("nai2");
                Core.data.unlockedFire2.Add("rock0");
                Core.data.unlockedFire2.Add("rock1");
                Core.data.unlockedFire2.Add("rock2");
            }
        }
    }
}
