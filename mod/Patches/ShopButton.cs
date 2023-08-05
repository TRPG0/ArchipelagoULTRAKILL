using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Patches
{
    // prevent shop from giving the player weapons
    [HarmonyPatch(typeof(ShopButton), "OnPointerClick")]
    class ShopButton_OnPointerClick_Patch
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
}
