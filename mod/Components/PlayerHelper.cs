using HarmonyLib;
using UnityEngine;
using ArchipelagoULTRAKILL.Structures;
using ArchipelagoULTRAKILL.Powerups;
using ULTRAKILL.Cheats;
using System.Collections;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using BepInEx;

namespace ArchipelagoULTRAKILL.Components
{
    public class PlayerHelper : MonoBehaviour
    {
        public static PlayerHelper Instance { get; private set; }

        public NewMovement nm;
        public Traverse nmT;

        public bool CanGetPowerup { get; internal set; } = false;
        public static Powerup CurrentPowerup { get; private set; }

        public void Init(NewMovement nm)
        {
            Instance = this;
            this.nm = nm;
            nmT = Traverse.Create(nm);
            CurrentPowerup = Powerup.None;
        }

        public void Start()
        {
            GunControl.Instance.NoWeapon();
            GunControl.Instance.YesWeapon();
        }

        public void OnDestroy()
        {
            Instance = null;
        }

        public void Update()
        {
            if (Core.IsPlaying)
            {
                if (Multiworld.lastDeathLink != null && !nm.dead)
                {
                    string cause = "{0} has died.";
                    if (!Multiworld.lastDeathLink.Cause.IsNullOrWhiteSpace()) cause = Multiworld.lastDeathLink.Cause;
                    else cause = string.Format(cause, Multiworld.lastDeathLink.Source);

                    if (Core.uim.deathLinkMessage != null) Core.uim.deathLinkMessage.SetDeathMessage(cause);
                    NewMovement.Instance.GetHurt(200, false);
                }

                DisplayMessage();
                UpdateStats();
                UpdateWeapons();
                if (CanGetPowerup && CurrentPowerup == Powerup.None && PowerUpMeter.Instance != null) GetQueuedPowerups();
                if (LocationManager.soapWaiting > 0) SpawnSoap();
            }
        }

        public void DisplayMessage()
        {
            if (LocationManager.messages.Count > 0 && !UIManager.displayingMessage) Core.uim.StartCoroutine("DisplayMessage");
        }

        public static void SpawnSoap()
        {
            LocationManager.soapWaiting--;
            GameObject obj = Instantiate(AssetHelper.LoadPrefab("Assets/Prefabs/Items/Soap.prefab"), NewMovement.Instance.transform);
            obj.transform.parent = null;

            if (FistControl.Instance.currentPunch != null)
            {
                if (FistControl.Instance.currentPunch.type == FistType.Standard && !Core.data.hasArm) return;
                if (!FistControl.Instance.currentPunch.holding)
                {
                    FistControl.Instance.currentPunch.ForceHold(obj.GetComponent<ItemIdentifier>());
                }
            }
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
                        gameObject.AddComponent<GenericTrap>();
                        break;
                    case Powerup.WalljumpLimiter:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<GenericTrap>();
                        break;
                    case Powerup.Radiance:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<RadianceTrap>();
                        break;
                    case Powerup.EmptyAmmo:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<GenericTrap>();
                        break;
                    case Powerup.NoArms:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<GenericTrap>();
                        break;
                    case Powerup.Confusion:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<ConfusionPowerup>();
                        break;
                    case Powerup.QuickCharge:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<QuickChargePowerup>();
                        break;
                    case Powerup.Sandstorm:
                        gameObject.transform.SetParent(Core.obj.transform);
                        gameObject.AddComponent<SandstormTrap>();
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
            else if (CurrentPowerup != Powerup.DoubleJump)
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
            // piercer
            if (!NoWeaponCooldown.NoCooldown && Core.data.randomizeFire2 > Fire2Options.Disabled)
            {
                if ((!Core.data.unlockedFire2.Contains("rev0") || CurrentPowerup == Powerup.EmptyAmmo) && GetHeldWeapon() == "rev0")
                {
                    if (GameProgressSaver.GetGeneralProgress().rev0 == 1)
                    {
                        Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Revolver>()).Field<bool>("pierceReady").Value = false;
                        GunControl.Instance.currentWeapon.GetComponent<Revolver>().pierceCharge = 0;
                        GunControl.Instance.currentWeapon.GetComponent<Revolver>().pierceShotCharge = 0;

                        if (CurrentPowerup == Powerup.DualWield)
                        {
                            foreach (DualWield dw in GunControl.Instance.gameObject.GetComponentsInChildren<DualWield>())
                            {
                                Traverse.Create(dw.gameObject.transform.GetChild(0).GetComponent<Revolver>()).Field<bool>("pierceReady").Value = false;
                                dw.gameObject.transform.GetChild(0).GetComponent<Revolver>().pierceCharge = 0;
                                dw.gameObject.transform.GetChild(0).GetComponent<Revolver>().pierceShotCharge = 0;
                            }
                        }
                    }
                    WeaponCharges.Instance.rev0charge = 0;
                }

                // marksman
                if (!Core.data.unlockedFire2.Contains("rev2") || CurrentPowerup == Powerup.EmptyAmmo)
                {
                    WeaponCharges.Instance.rev1charge = 0;
                }

                // sharpshooter
                if (!Core.data.unlockedFire2.Contains("rev1") || CurrentPowerup == Powerup.EmptyAmmo)
                {
                    WeaponCharges.Instance.rev2charge = 0;
                }

                // core eject || pump charge
                if (((!Core.data.unlockedFire2.Contains("sho0") || CurrentPowerup == Powerup.EmptyAmmo) && GetHeldWeapon() == "sho0") 
                    || ((!Core.data.unlockedFire2.Contains("sho1") || CurrentPowerup == Powerup.EmptyAmmo) && GetHeldWeapon() == "sho1"))
                {
                    Traverse.Create(InputManager.Instance.InputSource.Fire2).Property("IsPressed").SetValue(false);
                }

                // sawed-on
                if (!Core.data.unlockedFire2.Contains("sho2") || CurrentPowerup == Powerup.EmptyAmmo)
                {
                    WeaponCharges.Instance.shoSawCharge = 0;
                }

                // attractor
                if (!Core.data.unlockedFire2.Contains("nai0") || CurrentPowerup == Powerup.EmptyAmmo)
                {
                    WeaponCharges.Instance.naiMagnetCharge = 0;
                }

                // overheat
                if ((!Core.data.unlockedFire2.Contains("nai1") || CurrentPowerup == Powerup.EmptyAmmo) && GetHeldWeapon() == "nai1")
                {
                    Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Nailgun>()).Field<float>("heatSinks").Value = 0;
                    WeaponCharges.Instance.naiHeatsinks = 0;
                    WeaponCharges.Instance.naiSawHeatsinks = 0;
                }

                // s.r.s. cannon
                if (!Core.data.unlockedFire2.Contains("rock1") || CurrentPowerup == Powerup.EmptyAmmo)
                {
                    WeaponCharges.Instance.rocketCannonballCharge = 0;
                }

                // firestarter
                if (!Core.data.unlockedFire2.Contains("rock2") || CurrentPowerup == Powerup.EmptyAmmo)
                {
                    WeaponCharges.Instance.rocketNapalmFuel = 0;
                }
            }
            if (CurrentPowerup == Powerup.EmptyAmmo) WeaponCharges.Instance.raicharge = 0;
        }

        public string GetHeldWeapon()
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
            else if (GunControl.Instance.currentWeapon.GetComponent<ShotgunHammer>()) return $"sho{GunControl.Instance.currentWeapon.GetComponent<ShotgunHammer>().variation}";
            else if (GunControl.Instance.currentWeapon.GetComponent<Nailgun>())
            {
                switch (GunControl.Instance.currentWeapon.GetComponent<Nailgun>().variation)
                {
                    case 0:
                    default:
                        return "nai1";
                    case 1:
                        return "nai0";
                    case 2:
                        return "nai2";
                }
            }
            else if (GunControl.Instance.currentWeapon.GetComponent<Railcannon>()) return $"rai{GunControl.Instance.currentWeapon.GetComponent<Railcannon>().variation}";
            else if (GunControl.Instance.currentWeapon.GetComponent<RocketLauncher>()) return $"rock{GunControl.Instance.currentWeapon.GetComponent<RocketLauncher>().variation}";
            else return "?";
        }

        public bool IsWeaponAlternate()
        {
            if (GunControl.Instance.currentWeapon == null) return false;

            if (GunControl.Instance.currentWeapon.GetComponent<Revolver>()) return GunControl.Instance.currentWeapon.GetComponent<Revolver>().altVersion;
            else if (GunControl.Instance.currentWeapon.GetComponent<ShotgunHammer>()) return true;
            else if (GunControl.Instance.currentWeapon.GetComponent<Nailgun>()) return GunControl.Instance.currentWeapon.GetComponent<Nailgun>().altVersion;
            else return false;
        }
    }
}
