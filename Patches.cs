using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL
{
    [HarmonyPatch(typeof(FinalRank), "SetTime")]
    class SetTime_Patch
    {
        public static void Prefix(FinalRank __instance)
        {
            __instance.targetLevelName = "Main Menu";
        }
    }

    [HarmonyPatch(typeof(FinalPit), "SendInfo")]
    class SendInfo_Patch
    {
        public static void Prefix()
        {
            if (AssistController.Instance.cheatsEnabled)
            {
                Debug.Log("Cheats enabled! Completion not counted.");
            }
            else
            {
                if (APULTRAKILL.idToLevel.ContainsKey(StatsManager.Instance.levelNumber))
                {
                    Debug.Log("Completed level: " + APULTRAKILL.idToLevel[StatsManager.Instance.levelNumber]);
                    if (!APULTRAKILL.data.completedLevels.Contains(APULTRAKILL.idToLevel[StatsManager.Instance.levelNumber]))
                    {
                        APULTRAKILL.data.completedLevels.Add(APULTRAKILL.idToLevel[StatsManager.Instance.levelNumber]);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(ShopZone), "TurnOn")]
    class TurnOn_Patch
    {
        public static void Prefix()
        {
            APULTRAKILL.UpdateShopObjects();
        }
    }

    [HarmonyPatch(typeof(ShopButton), "OnPointerClick")]
    class OnPointerClick_Patch
    {
        public static bool Prefix(ShopButton __instance)
        {
            if (__instance.variationInfo != null)
            {
                //GameProgressMoneyAndGear generalProgress = GameProgressSaver.GetGeneralProgress();
                //FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(__instance.variationInfo.weaponName, BindingFlags.Instance | BindingFlags.Public);
                //if (int.Parse(field.GetValue(generalProgress).ToString()) != 0 && !__instance.variationInfo.weaponName.Contains("0") && !APULTRAKILL.data.purchasedItems.Contains(__instance.variationInfo.weaponName))
                //{
                    if (GameProgressSaver.GetMoney() >= APULTRAKILL.shopPrices[__instance.variationInfo.weaponName] && !__instance.deactivated)
                    {
                        __instance.deactivated = true;
                        __instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = "ALREADY OWNED";
                        __instance.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                        __instance.variationInfo.costText.text = "ALREADY OWNED";
                        APULTRAKILL.data.purchasedItems.Add(__instance.variationInfo.weaponName);
                        Debug.Log("Bought " + __instance.variationInfo.weaponName + " from shop.");
                    }
                    else Debug.Log("Tried to buy " + __instance.variationInfo.weaponName + " from shop. Can't afford.");
                    return false;
                //}
                //else return true;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(VariationInfo), "Start")]
    class Start_Patch
    {
        public static void Postfix(VariationInfo __instance)
        {
            APULTRAKILL.UpdateShopObjects();
        }
    }

    [HarmonyPatch(typeof(VariationInfo), "UpdateMoney")]
    class UpdateMoney_Patch
    {
        public static bool Prefix(VariationInfo __instance)
        {
            GameProgressMoneyAndGear generalProgress = GameProgressSaver.GetGeneralProgress();
            FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(__instance.weaponName, BindingFlags.Instance | BindingFlags.Public);
            if (int.Parse(field.GetValue(generalProgress).ToString()) != 0 && !__instance.weaponName.Contains("0") && !APULTRAKILL.data.purchasedItems.Contains(__instance.weaponName))
            {
                return false;
            }
            else if (int.Parse(field.GetValue(generalProgress).ToString()) == 0 && !__instance.weaponName.Contains("0") && APULTRAKILL.data.purchasedItems.Contains(__instance.weaponName))
            {
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(NewMovement), "StartSlide")]
    class StartSlide_Patch
    {
        public static bool Prefix(NewMovement __instance)
        {
            if (!APULTRAKILL.data.canSlide)
            {
                //Debug.Log("Attempted to slide, canSlide is false.");
                __instance.StopSlide();
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(PlayerActivatorRelay), "Activate")]
    class Activate_Patch
    {
        public static void Postfix()
        {
            APULTRAKILL.data.playerActivated = true;
        }
    }

    [HarmonyPatch(typeof(Shotgun), "Pump")]
    class Pump_Patch
    {
        public static bool Prefix(Shotgun __instance)
        {
            if (APULTRAKILL.data.lockFire2 && !APULTRAKILL.data.unlockedFire2.Contains("sho1") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to pump shotgun. Locked");
                __instance.ReadyGun();
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(Shotgun), "ShootSinks")]
    class ShootSinks_Patch
    {
        public static bool Prefix()
        {
            if (APULTRAKILL.data.lockFire2 && !APULTRAKILL.data.unlockedFire2.Contains("sho0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to eject core. Locked");
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(Shotgun), "UpdateMeter")]
    class UpdateMeter_Patch
    {
        public static bool Prefix()
        {
            if (APULTRAKILL.data.lockFire2 && !APULTRAKILL.data.unlockedFire2.Contains("sho0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(RocketLauncher), "FreezeRockets")]
    class FreezeRockets_Patch
    {
        public static bool Prefix()
        {
            if (APULTRAKILL.data.lockFire2 && !APULTRAKILL.data.unlockedFire2.Contains("rock0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to freeze rockets. Locked");
                return false;
            }
            else return true;
        }
    }
}
