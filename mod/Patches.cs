using ArchipelagoULTRAKILL.Commands;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL
{
    // disconnect when loading a different save file
    [HarmonyPatch(typeof(GameProgressSaver), "SetSlot")]
    class SetSlot_Patch
    {
        public static void Postfix()
        {
            if (Multiworld.Authenticated)
            {
                Multiworld.Disconnect();
            }
            if (Core.DataExists()) Core.firstTimeLoad = false;
            else Core.data = new Data();

        }
    }

    // save multiworld data when game is saving
    [HarmonyPatch(typeof(GameProgressSaver), "WriteFile")]
    class WriteFile_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists()) Core.SaveData();
        }
    }

    // load main menu instead of next level
    [HarmonyPatch(typeof(FinalPit), "OnTriggerEnter")]
    class OnTriggerEnter_Patch
    {
        public static void Prefix(FinalPit __instance)
        {
            if (Core.DataExists())
            {
                if (!__instance.targetLevelName.Contains("-S")) __instance.targetLevelName = "Main Menu";
            }
        }
    }

    // check P rank location if necessary
    [HarmonyPatch(typeof(FinalRank), "SetRank")]
    class SetRank_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists() && Core.data.pRankRewards && !AssistController.Instance.cheatsEnabled && StatsManager.Instance.rankScore == 12)
                LocationManager.CheckLocation(StatsManager.Instance.levelNumber + "_p");
        }
    }

    // check challenge location if necessary
    [HarmonyPatch(typeof(ChallengeManager), "ChallengeDone")]
    class ChallengeDone_Patch
    {
        public static void Prefix(ChallengeManager __instance)
        {
            if (Core.DataExists() && Core.data.challengeRewards && !__instance.challengeFailed)
                LocationManager.CheckLocation(StatsManager.Instance.levelNumber + "_c");
        }
    }

    // add to list of completed levels, check challenge location if necessary
    [HarmonyPatch(typeof(FinalPit), "SendInfo")]
    class SendInfo_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists())
            {
                Core.playerActive = false;
                Core.poweredUp = false;
                if (AssistController.Instance.cheatsEnabled)
                {
                    Core.logger.LogWarning("Cheats enabled! Completion not counted.");
                }
                else
                {
                    if (Core.idToLevel.ContainsKey(StatsManager.Instance.levelNumber))
                    {
                        if (Core.idToLevel[StatsManager.Instance.levelNumber] == Core.data.goal && Multiworld.Authenticated)
                        {
                            Multiworld.SendCompletion();
                        }
                        else
                        {
                            Core.logger.LogInfo("Completed level: " + Core.idToLevel[StatsManager.Instance.levelNumber]);
                            if (!Core.data.completedLevels.Contains(Core.idToLevel[StatsManager.Instance.levelNumber]))
                            {
                                Core.data.completedLevels.Add(Core.idToLevel[StatsManager.Instance.levelNumber]);
                            }
                        }
                    }
                }
            }
        }
    }

    // add to list of completed levels
    [HarmonyPatch(typeof(SecretMissionPit), "OnTriggerEnter")]
    class OnTriggerEnter_Patch2
    {
        public static void Prefix(SecretMissionPit __instance)
        {
            if (Core.DataExists())
            {
                string level = __instance.missionNumber + "-S";
                if (!Core.data.completedLevels.Contains(level)) Core.data.completedLevels.Add(level);
            }
        }
    }

    // update shop objects when entering zone
    [HarmonyPatch(typeof(ShopZone), "TurnOn")]
    class TurnOn_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists()) LevelManager.UpdateShopObjects();
        }
    }

    // prevent shop from giving the player weapons
    [HarmonyPatch(typeof(ShopButton), "OnPointerClick")]
    class OnPointerClick_Patch
    {
        public static bool Prefix(ShopButton __instance)
        {
            if (__instance.variationInfo != null && Core.DataExists())
            {
                if (__instance.variationInfo.weaponName.Contains("0")) return false;
                if (GameProgressSaver.GetMoney() >= LevelManager.shopPrices[__instance.variationInfo.weaponName] && !__instance.deactivated)
                {
                    GameProgressSaver.AddMoney(LevelManager.shopPrices[__instance.variationInfo.weaponName] * -1);
                    LocationManager.CheckLocation("shop_" + __instance.variationInfo.weaponName);
                    __instance.deactivated = true;
                    __instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = "ALREADY OWNED";
                    __instance.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                    __instance.variationInfo.costText.text = "ALREADY OWNED";
                    Core.data.purchasedItems.Add(__instance.variationInfo.weaponName);
                    Core.logger.LogInfo("Bought " + __instance.variationInfo.weaponName + " from shop.");
                }
                else Core.logger.LogInfo("Tried to buy " + __instance.variationInfo.weaponName + " from shop. Can't afford.");
                return false;
            }
            else return true;
        }
    }

    // update item descriptions in shop
    [HarmonyPatch(typeof(VariationInfo), "Start")]
    class Start_Patch
    {
        public static void Postfix(VariationInfo __instance)
        {
            if (Core.DataExists()) LevelManager.UpdateShopObjects();
        }
    }

    // update item prices in shop
    [HarmonyPatch(typeof(VariationInfo), "UpdateMoney")]
    class UpdateMoney_Patch
    {
        public static bool Prefix(VariationInfo __instance)
        {
            if (Core.DataExists())
            {
                if (__instance.weaponName.Contains("arm"))
                {
                    if (__instance.weaponName == "arm0")
                    {
                        if (!Core.data.hasArm) return false;
                        else return true;
                    }
                    else return true;
                }
                else
                {
                    GameProgressMoneyAndGear generalProgress = GameProgressSaver.GetGeneralProgress();
                    FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(__instance.weaponName, BindingFlags.Instance | BindingFlags.Public);
                    if (int.Parse(field.GetValue(generalProgress).ToString()) != 0 && !__instance.weaponName.Contains("0") && !Core.data.purchasedItems.Contains(__instance.weaponName))
                    {
                        return false;
                    }
                    else if (int.Parse(field.GetValue(generalProgress).ToString()) == 0 && !__instance.weaponName.Contains("0") && Core.data.purchasedItems.Contains(__instance.weaponName))
                    {
                        return false;
                    }
                    else return true;
                }
            }
            else return true;
        }
    }

    // prevent sliding
    [HarmonyPatch(typeof(NewMovement), "StartSlide")]
    class StartSlide_Patch
    {
        public static bool Prefix(NewMovement __instance)
        {
            if (Core.DataExists() && Core.playerActive && !Core.data.canSlide)
            {
                //Debug.Log("Attempted to slide, canSlide is false.");
                __instance.StopSlide();
                return false;
            }
            else return true;
        }
    }

    // set playerActive when landing at the beginning of a level
    [HarmonyPatch(typeof(PlayerActivatorRelay), "Activate")]
    class Activate_Patch
    {
        public static void Postfix()
        {
            Core.playerActive = true;
            if (Core.DataExists())
            {
                if (Core.currentScene.name == "Level 0-1")
                {
                    GunSetter.Instance.ResetWeapons();
                    GunControl.Instance.UpdateWeaponList();
                    GunControl.Instance.YesWeapon();
                    FistControl.Instance.ResetFists();
                    if (!Core.CanBreakGlass()) LevelManager.DeactivateGlass();
                }
                else if (UIManager.skullLevels.Contains(Core.currentScene.name))
                {
                    LevelManager.FindSkulls();
                }
            }
            if (LocationManager.messages.Count > 0 && !UIManager.displayingMessage) Core.uim.StartCoroutine("DisplayMessage");
            if (LocationManager.powerupQueue.Count > 0 && !Core.poweredUp)
            {
                Core.obj.GetComponent<Core>().Invoke("AddPowerup", 1f);
                Core.poweredUp = true;
            }
            if (LocationManager.overhealWaiting)
            {
                NewMovement.Instance.SuperCharge();
                LocationManager.overhealWaiting = false;
            }
            if (LocationManager.hardDmgWaiting)
            {
                NewMovement.Instance.ForceAntiHP(50);
                LocationManager.hardDmgWaiting = false;
            }
            if (LocationManager.soapWaiting) Core.SpawnSoap();
        }
    }

    // prevent pumping
    [HarmonyPatch(typeof(Shotgun), "Pump")]
    class Pump_Patch
    {
        public static bool Prefix(Shotgun __instance)
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("sho1") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to pump shotgun. Locked");
                __instance.ReadyGun();
                return false;
            }
            else return true;
        }
    }

    // prevent core eject
    [HarmonyPatch(typeof(Shotgun), "ShootSinks")]
    class ShootSinks_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("sho0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to eject core. Locked");
                return false;
            }
            else return true;
        }
    }

    // prevent charge meter from filling
    [HarmonyPatch(typeof(Shotgun), "UpdateMeter")]
    class UpdateMeter_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("sho0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                return false;
            }
            else return true;
        }
    }

    // prevent freezing rockets
    [HarmonyPatch(typeof(RocketLauncher), "FreezeRockets")]
    class FreezeRockets_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("rock0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to freeze rockets. Locked");
                return false;
            }
            else return true;
        }
    }

    // send check when picking up weapon
    [HarmonyPatch(typeof(WeaponPickUp), "GotActivated")]
    class GotActivated_Patch
    {
        public static bool Prefix(WeaponPickUp __instance)
        {
            if (Core.DataExists())
            {
                Traverse wpu = Traverse.Create(__instance);
                wpu.Field<bool>("activated").Value = true;
                if (__instance.activateOnPickup != null) __instance.activateOnPickup.SetActive(true);
                __instance.gameObject.SetActive(false);

                LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_w1");

                return false;
            }
            else return true;
        }
    }

    // send check when picking up an alternate weapon
    [HarmonyPatch(typeof(AltPickUp), "GotActivated")]
    class GotActivated_Patch2
    {
        public static bool Prefix(AltPickUp __instance)
        {
            if (Core.DataExists())
            {
                UnityEvent unityEvent = __instance.onPickUp;
                if (unityEvent != null) unityEvent.Invoke();
                Object.Destroy(__instance.gameObject);

                LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_w2");

                return false;
            }
            else return true;
        }
    }

    // send check when touching a secret
    [HarmonyPatch(typeof(StatsManager), "SecretFound")]
    class SecretFound_Patch
    {
        public static bool Prefix(int i)
        {
            if (!AssistController.Instance.cheatsEnabled)
            {
                if (Core.DataExists()) LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_s" + (i + 1));
                return true;
            }
            else return false;
        }
    }

    // add more powerups after a dual wield powerup, if any
    [HarmonyPatch(typeof(DualWield), "EndPowerUp")]
    class EndPowerUp_Patch
    {
        public static void Postfix()
        {
            if (LocationManager.powerupQueue.Count > 0) Core.AddPowerup();
        }
    }

    // delete data when wiping save file
    [HarmonyPatch(typeof(GameProgressSaver), "WipeSlot")]
    class WipeSlot_Patch
    {
        public static void Postfix(int slot)
        {
            if (Multiworld.Authenticated) Multiworld.Disconnect();
            Core.DeleteData(slot);
            Core.data = new Data();
            Core.firstTimeLoad = false;
        }
    }

    // multiply points
    [HarmonyPatch(typeof(GameProgressSaver), "AddMoney")]
    class AddMoney_Patch
    {
        public static void Prefix(ref int money)
        {
            if (Core.DataExists() && money > 0)
            {
                money *= Core.data.multiplier;
            }
        }
    }

    // show shop categories if any weapon of type is unlocked
    [HarmonyPatch(typeof(ShopCategory), "CheckGear")]
    class CheckGear_Patch
    {
        public static bool Prefix(ShopCategory __instance)
        {
            if (Core.DataExists() && !__instance.weaponName.Contains("arm"))
            {
                bool hasWeapon = false;
                switch (__instance.weaponName.Substring(0, __instance.weaponName.Length - 1))
                {
                    case "rev":
                        if (GameProgressSaver.CheckGear("rev0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rev2") == 1) hasWeapon = true;
                        break;
                    case "sho":
                        if (GameProgressSaver.CheckGear("sho0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("sho1") == 1) hasWeapon = true;
                        break;
                    case "nai":
                        if (GameProgressSaver.CheckGear("nai0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("nai1") == 1) hasWeapon = true;
                        break;
                    case "rai":
                        if (GameProgressSaver.CheckGear("rai0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rai1") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rai2") == 1) hasWeapon = true;
                        break;
                    case "rock":
                        if (GameProgressSaver.CheckGear("rock0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rock1") == 1) hasWeapon = true;
                        break;
                    default: break;
                }
                if (!hasWeapon) __instance.gameObject.SetActive(false);
                else __instance.gameObject.SetActive(true);

                return false;
            }
            else return true;
        }
    }

    // lazy fix for closing doors with no skull on pedestal
    [HarmonyPatch(typeof(ItemPlaceZone), "Start")]
    class Start_Patch2
    {
        public static void Postfix()
        {
            if (Core.DataExists() && Core.data.randomizeSkulls) LevelManager.CloseReverseDoors();
        }
    }
}
