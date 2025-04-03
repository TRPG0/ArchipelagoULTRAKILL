﻿using Archipelago.MultiClient.Net.Enums;
using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Reflection;
using TMPro;
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
                if (AssistController.Instance.cheatsEnabled && !__instance.deactivated)
                {
                    __instance.deactivated = true;
                    __instance.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cheats enabled!";
                    __instance.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                    if (__instance.failSound != null) Object.Instantiate(__instance.failSound);
                }
                else if (GameProgressSaver.GetMoney() >= Core.shopPrices[__instance.variationInfo.weaponName] && !__instance.deactivated)
                {
                    LocationManager.CheckLocation("shop_" + __instance.variationInfo.weaponName);
                    GameProgressSaver.AddMoney(Core.shopPrices[__instance.variationInfo.weaponName] * -1);
                    foreach (MoneyText mt in Object.FindObjectsOfType<MoneyText>()) mt.UpdateMoney();
                    __instance.deactivated = true;
                    __instance.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Already Owned";
                    __instance.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                    __instance.variationInfo.costText.text = "Already Owned";
                    Core.data.purchasedItems.Add(__instance.variationInfo.weaponName);
                    Core.Logger.LogInfo("Bought " + __instance.variationInfo.weaponName + " from shop.");
                    if (__instance.variationInfo.buySound != null) Object.Instantiate(__instance.variationInfo.buySound);
                    if (__instance.clickSound != null) Object.Instantiate(__instance.clickSound);
                }
                else
                {
                    Core.Logger.LogInfo("Tried to buy " + __instance.variationInfo.weaponName + " from shop. Can't afford.");
                    if (__instance.failSound != null) Object.Instantiate(__instance.failSound);
                }
                    return false;
            }
            else return true;
        }

        public static void Postfix(ShopButton __instance)
        {
            if (Core.DataExists() && __instance.TryGetComponent(out ShopCategory sc))
            {
                VariationInfo[] variations = __instance.toActivate[0].transform.GetComponentsInChildren<VariationInfo>(true);
                foreach (VariationInfo variation in variations)
                {
                    variation.GetOrAddComponent<VariationOnEnable>();
                }
            }
        }
    }
}
