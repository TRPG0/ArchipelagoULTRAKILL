using HarmonyLib;
using System.Collections.Generic;
using System.Text;
using UMM;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json;
using ArchipelagoULTRAKILL.Structures;
using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Powerups;
using BepInEx.Logging;
using System.Reflection;

namespace ArchipelagoULTRAKILL
{
    [UKPlugin(ModGUID, ModName, ModVersion, ModDescription, false, true)]
    public class Core : UKMod
    {
        public const string ModGUID = "trpg.archipelagoultrakill";
        public const string ModName = "Archipelago";
        public const string ModVersion = "1.1.6";
        public const string ModDescription = "Connect to an Archipelago server to play ULTRAKILL randomizer.";

        public static string workingPath;
        public static string workingDir;

        public static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("Archipelago");

        public static GameObject obj;
        public static UIManager uim;

        public static bool inIntro => GameStateManager.Instance.IsStateActive("intro");
        public static bool inLevel => !GameStateManager.Instance.IsStateActive("main-menu") && !GameStateManager.Instance.IsStateActive("intro");

        public static Data data = new Data();
        public static bool firstTimeLoad = false;

        public static readonly List<string> allLevels = new List<string>()
        {
            "0-2",
            "0-3",
            "0-4",
            "0-5",
            "1-1",
            "1-2",
            "1-3",
            "1-4",
            "2-1",
            "2-2",
            "2-3",
            "2-4",
            "3-1",
            "3-2",
            "4-1",
            "4-2",
            "4-3",
            "4-4",
            "5-1",
            "5-2",
            "5-3",
            "5-4",
            "6-1",
            "6-2",
            "P-1",
            "P-2"
        };

        public static readonly List<string> allSecrets = new List<string>()
        {
            "0-S",
            "1-S",
            "2-S",
            "4-S",
            "5-S"
        };

        public static readonly Dictionary<int, string> idToLevel = new Dictionary<int, string>()
        {
            [1] = "0-1",
            [2] = "0-2",
            [3] = "0-3",
            [4] = "0-4",
            [5] = "0-5",
            [6] = "1-1",
            [7] = "1-2",
            [8] = "1-3",
            [9] = "1-4",
            [10] = "2-1",
            [11] = "2-2",
            [12] = "2-3",
            [13] = "2-4",
            [14] = "3-1",
            [15] = "3-2",
            [16] = "4-1",
            [17] = "4-2",
            [18] = "4-3",
            [19] = "4-4",
            [20] = "5-1",
            [21] = "5-2",
            [22] = "5-3",
            [23] = "5-4",
            [24] = "6-1",
            [25] = "6-2",
            [666] = "P-1",
            [667] = "P-2"
        };


        public static bool playerActive = false;
        public static bool poweredUp = false;

        public static bool dashTrap = false;
        public static bool walljumpTrap = false;
        public static bool staminaPowerup = false;

        public override void OnModLoaded()
        {
            Harmony harmony = new Harmony("archipelago");
            harmony.PatchAll();

            workingPath = Assembly.GetExecutingAssembly().Location;
            workingDir = Path.GetDirectoryName(workingPath);
            logger.LogInfo($"Working Path: {workingPath}, Working Dir: {workingDir}");

            ConfigManager.Initialize();

            obj = FindObjectOfType<Core>().gameObject;
            obj.name = "Archipelago";
            obj.transform.localPosition = new Vector3(960, 540, 0);

            uim = obj.AddComponent<UIManager>();

            SceneManager.sceneLoaded += OnSceneLoaded;
            //UKAPI.OnLevelChanged += ChangedLevel;

            UIManager.skullsInLevel["0-2"] = new List<string>{ "2_b" };
            UIManager.skullsInLevel["1-1"] = new List<string>{ "6_b", "6_r" };
            UIManager.skullsInLevel["1-2"] = new List<string> { "7_r", "7_b" };
            UIManager.skullsInLevel["1-3"] = new List<string> { "8_r", "8_b" };
            UIManager.skullsInLevel["2-3"] = new List<string> { "12_r", "12_b" };
            UIManager.skullsInLevel["2-4"] = new List<string> { "13_r", "13_b" };
            UIManager.skullsInLevel["4-2"] = new List<string> { "17_r", "17_b" };
            UIManager.skullsInLevel["4-3"] = new List<string> { "18_b" };
            UIManager.skullsInLevel["4-4"] = new List<string> { "19_b" };
            UIManager.skullsInLevel["5-2"] = new List<string> { "21_r", "21_b" };
            UIManager.skullsInLevel["5-3"] = new List<string> { "22_r", "22_b" };
            UIManager.skullsInLevel["6-1"] = new List<string> { "24_r" };

            GameConsole.Console.Instance.RegisterCommand(new Commands.Connect());
            GameConsole.Console.Instance.RegisterCommand(new Commands.Disconnect());
            GameConsole.Console.Instance.RegisterCommand(new Commands.Say());

            StartCoroutine(VersionChecker.CheckVersion());
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            /*
            foreach (var locator in Addressables.ResourceLocators)
            {
                if (locator is ResourceLocationMap)
                {
                    foreach (string key in locator.Keys)
                    {
                        if (key.ToLower().Contains("assets/music/")) logger.LogInfo(key);
                    }
                }
            }
            */
            /*
            foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
            {
                if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name)
                {
                    logger.LogInfo(changer.gameObject.name);
                    if (changer.transform.parent != null) logger.LogInfo(changer.transform.parent.name);
                }
            }
            */
            if (SceneHelper.CurrentScene == "Intro" || SceneHelper.CurrentScene == null) return;
            obj.GetComponent<Core>().StopCoroutine("DisplayMessage");
            UIManager.displayingMessage = false;
            UIManager.levels.Clear();
            UIManager.secrets.Clear();
            UIManager.skullIcons.Clear();
            LevelManager.skulls.Clear();
            LevelManager.shopPanels.Clear();
            playerActive = false;
            poweredUp = false;
            staminaPowerup = false;
            dashTrap = false;
            walljumpTrap = false;
            if (SceneHelper.CurrentScene == "Main Menu")
            {
                UIManager.FindMenuObjects();

                if (DataExists() && Multiworld.Authenticated) UIManager.menuIcon.GetComponent<Image>().color = LocationManager.colors["green"];
                else if (DataExists() && !Multiworld.Authenticated) UIManager.menuIcon.GetComponent<Image>().color = LocationManager.colors["red"];

                if (UIManager.log == null) UIManager.CreateLogObject();

                if (DataExists() && !firstTimeLoad)
                {
                    LoadData();
                    ConfigManager.LoadConnectionInfo();
                    firstTimeLoad = true;
                }

                if (DataExists() && data.randomizeSkulls) UIManager.CreateSkullIcons();

                if (data.completedLevels.Count >= data.goalRequirement)
                {
                    if (!data.unlockedLevels.Contains(data.goal)) data.unlockedLevels.Add(data.goal);
                }
            }
            else if (inLevel && DataExists())
            {
                UIManager.CreateMessageUI();
                LevelManager.FindShopObjects();
                if (data.musicRandomizer) AudioManager.ChangeMusic();
            }
            if (!inIntro) OptionsManager.Instance.optionsMenu.gameObject.AddComponent<OptionsMenuState>();

            if (DataExists() && UIManager.log != null) UIManager.AdjustLogBounds();
            if (DataExists() && SceneHelper.CurrentScene == "Level 1-2" && GameProgressSaver.GetGeneralProgress().nai0 == 0) LevelManager.DeactivateNailgun();
        }

        public static bool DataExists()
        {
            string filePath = Path.Combine(GameProgressSaver.BaseSavePath, string.Format("Slot{0}", GameProgressSaver.currentSlot + 1)) + "\\archipelago.json";
            return File.Exists(filePath);
        }

        public static void SaveData()
        {
            string filePath = Path.Combine(GameProgressSaver.BaseSavePath, string.Format("Slot{0}", GameProgressSaver.currentSlot + 1)) + "\\archipelago.json";
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            File.WriteAllBytes(filePath, bytes);
        }

        public static void LoadData()
        {
            string filePath = Path.Combine(GameProgressSaver.BaseSavePath, string.Format("Slot{0}", GameProgressSaver.currentSlot + 1)) + "\\archipelago.json";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    data = JsonConvert.DeserializeObject<Data>(reader.ReadToEnd());
                }
            }
            else
            {
                logger.LogError("Archipelago data for slot " + (GameProgressSaver.currentSlot + 1) + " does not exist.");
            }
        }

        public static void DeleteData(int slot)
        {
            string filePath = Path.Combine(GameProgressSaver.BaseSavePath, string.Format("Slot{0}", slot + 1)) + "\\archipelago.json";
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        public void AddPowerup()
        {
            Enums.Powerup powerup = LocationManager.powerupQueue[0];
            LocationManager.powerupQueue.RemoveAt(0);
            GameObject gameObject = new GameObject();
            poweredUp = true;
            switch (powerup)
            {
                case Enums.Powerup.DualWield:
                    gameObject = Instantiate(AssetHelper.LoadPrefab("Assets/Prefabs/Levels/DualWieldPowerup.prefab"), NewMovement.Instance.transform);
                    gameObject.transform.position = NewMovement.Instance.transform.position;
                    Traverse.Create(gameObject.GetComponent<DualWieldPickup>()).Method("PickedUp").GetValue();
                    //Destroy(gameObject);
                    break;
                case Enums.Powerup.InfiniteStamina:
                    gameObject.transform.SetParent(obj.transform);
                    gameObject.AddComponent<StaminaPowerup>();
                    break;
                case Enums.Powerup.StaminaLimiter:
                    gameObject.transform.SetParent(obj.transform);
                    gameObject.AddComponent<DashTrap>();
                    break;
                case Enums.Powerup.WalljumpLimiter:
                    gameObject.transform.SetParent(obj.transform);
                    gameObject.AddComponent<WalljumpTrap>();
                    break;
                case Enums.Powerup.EmptyAmmo:
                    WeaponCharges.Instance.rev0charge = 0;
                    WeaponCharges.Instance.rev1charge = 0;
                    WeaponCharges.Instance.naiMagnetCharge = 0;
                    WeaponCharges.Instance.naiHeatsinks = 0;
                    WeaponCharges.Instance.naiSawHeatsinks = 0;
                    WeaponCharges.Instance.raicharge = 0;
                    WeaponCharges.Instance.rocketFreezeTime = 0;
                    WeaponCharges.Instance.rocketCannonballCharge = 0;
                    if (LocationManager.powerupQueue.Count > 0) AddPowerup();
                    break;
                default: break;
            }
        }

        public static bool HasNoArms()
        {
            bool hasNoArms = true;
            if (data.hasArm || GameProgressSaver.GetGeneralProgress().arm1 == 1) hasNoArms = false;
            return hasNoArms;
        }

        public static bool CanBreakGlass()
        {
            if (LocationManager.ukitems.ContainsKey("1_w1"))
            {
                switch (LocationManager.ukitems["1_w1"].item_name)
                {
                    case "Revolver - Piercer":
                    case "Revolver - Marksman":
                    case "Revolver - Sharpshooter":
                    case "Shotgun - Core Eject":
                    case "Shotgun - Pump Charge":
                        if (data.randomizeFire2) return false;
                        else return true;
                    case "Railcannon - Electric":
                    case "Railcannon - Malicious":
                    case "Rocket Launcher - Freezeframe":
                    case "Rocket Launcher - S.R.S. Cannon":
                    case "Knuckleblaster":
                        return true;
                    case "Nailgun - Attractor":
                    case "Nailgun - Overheat":
                    case "Feedbacker":
                        return false;
                    default:
                        return false;
                }
            }
            else return false;
        }

        public static void SpawnSoap()
        {
            LocationManager.soapWaiting = false;
            GameObject obj = Instantiate(AssetHelper.LoadPrefab("Assets/Prefabs/Items/Soap.prefab"), NewMovement.Instance.transform);
            obj.transform.parent = null;

            if (FistControl.Instance.currentPunch != null)
            {
                if (!FistControl.Instance.currentPunch.holding)
                {
                    FistControl.Instance.currentPunch.ForceHold(obj.GetComponent<ItemIdentifier>());
                }
            }
        }

        public static string GetHeldWeapon()
        {
            if (GunControl.Instance.currentWeapon.GetComponent<Revolver>())
            {
                switch (GunControl.Instance.currentWeapon.GetComponent<Revolver>().gunVariation)
                {
                    case 0:
                    default:
                        return "rev0";
                    case 1:
                        return "rev2";
                    case 2:
                        return "rev1";
                }
            }
            else if (GunControl.Instance.currentWeapon.GetComponent<Shotgun>()) return $"sho{GunControl.Instance.currentWeapon.GetComponent<Shotgun>().variation}";
            else if (GunControl.Instance.currentWeapon.GetComponent <Nailgun>())
            {
                switch (GunControl.Instance.currentWeapon.GetComponent<Nailgun>().variation)
                {
                    case 0:
                    default:
                        return "nai1";
                    case 1:
                        return "nai0";
                }
            }
            else if (GunControl.Instance.currentWeapon.GetComponent<Railcannon>()) return $"rai{GunControl.Instance.currentWeapon.GetComponent<Railcannon>().variation}";
            else if (GunControl.Instance.currentWeapon.GetComponent<RocketLauncher>()) return $"rock{GunControl.Instance.currentWeapon.GetComponent<RocketLauncher>().variation}";
            else return "?";
        }

        void Update()
        {
            if (playerActive && DataExists())
            {
                if (walljumpTrap)
                {
                    if (data.walljumps > 0 && NewMovement.Instance.currentWallJumps < (3 - (data.walljumps - 1)))
                        NewMovement.Instance.currentWallJumps = (3 - (data.walljumps - 1));
                    else if (data.walljumps == 0) NewMovement.Instance.currentWallJumps = 3;
                }
                else
                {
                    if (data.walljumps < 3 && NewMovement.Instance.currentWallJumps < (3 - data.walljumps))
                        NewMovement.Instance.currentWallJumps = 3 - data.walljumps;
                }

                if (staminaPowerup)
                {
                    NewMovement.Instance.boostCharge = 300;
                }
                else if (dashTrap)
                {
                    if (data.dashes > 0 && NewMovement.Instance.boostCharge > ((data.dashes - 1) * 100))
                        NewMovement.Instance.boostCharge = (data.dashes * 100) - 100;
                    else if (data.dashes == 0) NewMovement.Instance.boostCharge = 0;
                }
                else
                {
                    if (data.dashes < 3 && NewMovement.Instance.boostCharge > (data.dashes * 100))
                        NewMovement.Instance.boostCharge = data.dashes * 100;
                }

                //if (!data.canSlam && data.playerActivated && NewMovement.Instance.GetFieldValue<float>("fallTime", true) > 0.3)
                if (!data.canSlam && Traverse.Create(NewMovement.Instance).Field<float>("fallTime").Value > 0.3)
                {
                    Traverse.Create(NewMovement.Instance).Field<float>("fallTime").Value = 0.3f;
                    if (!NewMovement.Instance.gc.onGround) Traverse.Create(NewMovement.Instance).Field<bool>("falling").Value = true;
                }

                if (!data.hasArm && FistControl.Instance.currentPunch.type == FistType.Standard)
                {
                    FistControl.Instance.currentPunch.ready = false;
                }

                if (data.randomizeFire2 && !data.unlockedFire2.Contains("rev0") && GetHeldWeapon() == "rev0" && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    if (GameProgressSaver.GetGeneralProgress().rev0 == 1)
                    {
                        Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Revolver>()).Field<bool>("pierceReady").Value = false;
                        GunControl.Instance.currentWeapon.GetComponent<Revolver>().pierceCharge = 0;

                        if (Traverse.Create(PowerUpMeter.Instance).Field<bool>("hasPowerUp").Value == true)
                        {
                            foreach (DualWield dw in GunControl.Instance.gameObject.GetComponentsInChildren<DualWield>())
                            {
                                Traverse.Create(dw.gameObject.transform.GetChild(0).GetComponent<Revolver>()).Field<bool>("pierceReady").Value = false;
                                dw.gameObject.transform.GetChild(0).GetComponent<Revolver>().pierceCharge = 0;
                            }
                        }
                    }
                    WeaponCharges.Instance.rev0charge = 0;
                }

                if (data.randomizeFire2 && !data.unlockedFire2.Contains("rev2") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.rev1charge = 0;
                }

                if (data.randomizeFire2 && !data.unlockedFire2.Contains("rev1") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.rev2charge = 0;
                }

                if (data.randomizeFire2 && ((!data.unlockedFire2.Contains("sho0") && GetHeldWeapon() == "sho0") || (!data.unlockedFire2.Contains("sho1") && GetHeldWeapon() == "sho1")) && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    Traverse.Create(InputManager.Instance.InputSource.Fire2).Property("IsPressed").SetValue(false);
                }

                if (data.randomizeFire2 && !data.unlockedFire2.Contains("nai0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.naiMagnetCharge = 0;
                }

                if (data.randomizeFire2 && !data.unlockedFire2.Contains("nai1") && GetHeldWeapon() == "nai1" && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Nailgun>()).Field<float>("heatSinks").Value = 0;
                    WeaponCharges.Instance.naiHeatsinks = 0;
                    WeaponCharges.Instance.naiSawHeatsinks = 0;
                }

                if (data.randomizeFire2 && !data.unlockedFire2.Contains("rock1") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.rocketCannonballCharge = 0;
                }
            }
        }

        void FixedUpdate()
        {
            if (LocationManager.tempItems.Count > 0)
            {
                LocationManager.GetUKItem(LocationManager.tempItems[0].Value, LocationManager.tempItems[0].Key);
                LocationManager.tempItems.RemoveAt(0);
            }
        }
    }
}
