using HarmonyLib;
using UnityEngine;
using ArchipelagoULTRAKILL.Structures;
using ArchipelagoULTRAKILL.Powerups;

namespace ArchipelagoULTRAKILL.Components
{
    public class PlayerHelper : MonoBehaviour
    {
        public static PlayerHelper Instance { get; private set; }

        public NewMovement nm;
        public Traverse nmT;

        public static Powerup CurrentPowerup { get; private set; }

        public void Init(NewMovement nm)
        {
            Instance = this;
            this.nm = nm;
            nmT = Traverse.Create(nm);
            CurrentPowerup = Powerup.None;
        }

        public void OnDestroy()
        {
            Instance = null;
        }

        public void Update()
        {
            if (Core.IsPlaying)
            {
                GetQueuedItems();
                DisplayMessage();
                UpdateStats();
                UpdateWeapons();
                if (CurrentPowerup == Powerup.None && PowerUpMeter.Instance != null) GetQueuedPowerups();
            }
        }

        public void GetQueuedItems()
        {
            if (LocationManager.itemQueue.Count > 0)
            {
                QueuedItem qItem = LocationManager.itemQueue[0];
                LocationManager.GetUKItem(qItem.item, qItem.sendingPlayer, qItem.silent);
                LocationManager.itemQueue.RemoveAt(0);
            }
        }

        public void DisplayMessage()
        {
            if (LocationManager.messages.Count > 0 && !UIManager.displayingMessage) Core.uim.StartCoroutine("DisplayMessage");
        }

        public void GetQueuedPowerups()
        {
            if (LocationManager.powerupQueue.Count > 0)
            {
                Powerup powerup = LocationManager.powerupQueue[0];
                LocationManager.powerupQueue.RemoveAt(0);
                GameObject gameObject = new GameObject();
                CurrentPowerup = powerup;
                switch (powerup)
                {
                    case Powerup.Overheal:
                        nm.SuperCharge();
                        CurrentPowerup = Powerup.None;
                        break;
                    case Powerup.HardDamage:
                        nm.ForceAntiHP(75);
                        CurrentPowerup = Powerup.None;
                        break;
                    case Powerup.DualWield:
                        gameObject = Instantiate(AssetHelper.LoadPrefab("Assets/Prefabs/Levels/DualWieldPowerup.prefab"), NewMovement.Instance.transform);
                        gameObject.transform.position = NewMovement.Instance.transform.position;
                        Traverse.Create(gameObject.GetComponent<DualWieldPickup>()).Method("PickedUp").GetValue();
                        //Destroy(gameObject);
                        break;
                    case Powerup.InfiniteStamina:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<StaminaPowerup>();
                        break;
                    case Powerup.DoubleJump:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<DoubleJumpPowerup>();
                        break;
                    case Powerup.StaminaLimiter:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<DashTrap>();
                        break;
                    case Powerup.WalljumpLimiter:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<WallJumpTrap>();
                        break;
                    case Powerup.Radiance:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<RadianceTrap>();
                        break;
                    case Powerup.EmptyAmmo:
                        WeaponCharges.Instance.rev0charge = 0;
                        WeaponCharges.Instance.rev1charge = 0;
                        WeaponCharges.Instance.naiMagnetCharge = 0;
                        WeaponCharges.Instance.naiHeatsinks = 0;
                        WeaponCharges.Instance.naiSawHeatsinks = 0;
                        WeaponCharges.Instance.raicharge = 0;
                        WeaponCharges.Instance.rocketFreezeTime = 0;
                        WeaponCharges.Instance.rocketCannonballCharge = 0;
                        CurrentPowerup = Powerup.None;
                        if (LocationManager.powerupQueue.Count > 0) GetQueuedPowerups();
                        break;
                    default: break;
                }
            }
        }

        public void EndPowerup()
        {
            CurrentPowerup = Powerup.None;
        }

        public void UpdateStats()
        {
            // wall jumps
            if (CurrentPowerup == Powerup.WalljumpLimiter) nm.currentWallJumps = 3;
            else
            {
                if (Core.data.walljumps < 3 && nm.currentWallJumps < (3 - Core.data.walljumps)) nm.currentWallJumps = 3 - Core.data.walljumps;
            }

            // stamina
            if (CurrentPowerup == Powerup.InfiniteStamina) nm.boostCharge = 300;
            else if (CurrentPowerup == Powerup.StaminaLimiter) nm.boostCharge = 0;
            else
            {
                if (Core.data.dashes < 3 && nm.boostCharge > (Core.data.dashes * 100)) nm.boostCharge = Core.data.dashes * 100;
            }

            // slam
            if (!Core.data.canSlam && nmT.Field<float>("fallTime").Value > 0.3)
            {
                nmT.Field<float>("fallTime").Value = 0.3f;
                if (!NewMovement.Instance.gc.onGround) nmT.Field<bool>("falling").Value = true;
            }
        }

        public void UpdateWeapons()
        {
            // feedbacker
            if (!Core.data.hasArm && FistControl.Instance.currentPunch.type == FistType.Standard && SceneHelper.CurrentScene != "Level 5-S")
            {
                FistControl.Instance.currentPunch.ready = false;
            }

            // piercer
            if (!CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown") && Core.data.randomizeFire2)
            {
                if (!Core.data.unlockedFire2.Contains("rev0") && GetHeldWeapon() == "rev0")
                {
                    if (GameProgressSaver.GetGeneralProgress().rev0 == 1)
                    {
                        Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Revolver>()).Field<bool>("pierceReady").Value = false;
                        GunControl.Instance.currentWeapon.GetComponent<Revolver>().pierceCharge = 0;

                        if (CurrentPowerup == Powerup.DualWield)
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

                // marksman
                if (!Core.data.unlockedFire2.Contains("rev2"))
                {
                    WeaponCharges.Instance.rev1charge = 0;
                }

                // sharpshooter
                if (!Core.data.unlockedFire2.Contains("rev1"))
                {
                    WeaponCharges.Instance.rev2charge = 0;
                }

                // core eject || pump charge
                if ((!Core.data.unlockedFire2.Contains("sho0") && GetHeldWeapon() == "sho0") || (!Core.data.unlockedFire2.Contains("sho1") && GetHeldWeapon() == "sho1"))
                {
                    Traverse.Create(InputManager.Instance.InputSource.Fire2).Property("IsPressed").SetValue(false);
                }

                // attractor
                if (!Core.data.unlockedFire2.Contains("nai0"))
                {
                    WeaponCharges.Instance.naiMagnetCharge = 0;
                }

                // overheat
                if (!Core.data.unlockedFire2.Contains("nai1") && GetHeldWeapon() == "nai1")
                {
                    Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Nailgun>()).Field<float>("heatSinks").Value = 0;
                    WeaponCharges.Instance.naiHeatsinks = 0;
                    WeaponCharges.Instance.naiSawHeatsinks = 0;
                }

                // s.r.s. cannon
                if (!Core.data.unlockedFire2.Contains("rock1"))
                {
                    WeaponCharges.Instance.rocketCannonballCharge = 0;
                }
            }
        }

        public static string GetHeldWeapon()
        {
            if (GunControl.Instance.currentWeapon == null) return "?";

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
            else if (GunControl.Instance.currentWeapon.GetComponent<Nailgun>())
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
    }
}
