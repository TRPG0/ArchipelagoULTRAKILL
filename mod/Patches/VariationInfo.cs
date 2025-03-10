using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Patches
{
    // update item prices in shop
    [HarmonyPatch(typeof(VariationInfo), "UpdateMoney")]
    class VariationInfo_UpdateMoney_Patch
    {
        public static bool Prefix(VariationInfo __instance)
        {
            if (Core.DataExists() && !(__instance.weaponName == "sho2" || __instance.weaponName == "nai2" || __instance.weaponName == "rock2"))
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
                    bool unlocked = int.Parse(field.GetValue(generalProgress).ToString()) == 1;
                    if ((unlocked && !__instance.weaponName.Contains("0") && !Core.data.purchasedItems.Contains(__instance.weaponName)) // unlocked, not blue variation, not purchased
                        || (!unlocked && !__instance.weaponName.Contains("0") && Core.data.purchasedItems.Contains(__instance.weaponName)) // unlocked, not blue variation, purchased
                        || (!unlocked && __instance.weaponName.Contains("0"))) // not unlocked, blue variation
                    {
                        return false;
                    }
                    else return true;
                }
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(VariationInfo), "ChangeEquipment")]
    class VariationInfo_ChangeEquipment_Patch
    {
        public static bool Prefix(VariationInfo __instance, int value)
        {
            if (Core.DataExists())
            {
                bool def, alt;
                if (__instance.weaponName.Contains("rev"))
                {
                    def = Core.data.revstd;
                    alt = Core.data.revalt;
                }
                else if (__instance.weaponName.Contains("sho"))
                {
                    def = Core.data.shostd;
                    alt = Core.data.shoalt;
                }
                else if (__instance.weaponName.Contains("nai"))
                {
                    def = Core.data.naistd;
                    alt = Core.data.naialt;
                }
                else return true;

                Traverse traverse = Traverse.Create(__instance);
                int equipStatus = traverse.Field<int>("equipStatus").Value;

                if (value > 0) equipStatus++;
                else equipStatus--;

                int state = equipStatus;
                if (equipStatus < 0)
                {
                    if (alt) state = 2;
                    else if (def) state = 1;
                    else state = 0;
                }
                else if (equipStatus == 1)
                {
                    if (!def)
                    {
                        if (alt && value > 0) state = 2;
                        else state = 0;
                    }
                    else state = 1;
                }
                else if (equipStatus == 2)
                {
                    if (alt) state = 2;
                    else if (def && value < 0) state = 1;
                    else state = 0;
                }
                else if (equipStatus > 2) state = 0;

                traverse.Field<int>("equipStatus").Value = state;
                Core.Logger.LogInfo($"{__instance.weaponName} {state}");
                traverse.Method("SetEquipStatusText", new object[] { state }).GetValue();
                PrefsManager.Instance.SetInt("weapon." + __instance.weaponName, state);
                if (__instance.orderButtons)
                {
                    if (state != 0)
                    {
                        __instance.orderButtons.SetActive(true);
                        __instance.icon.rectTransform.anchoredPosition = new Vector2(25, 0);
                        __instance.icon.rectTransform.sizeDelta = new Vector2(75, 75);
                    }
                    else
                    {
                        __instance.orderButtons.SetActive(false);
                        __instance.icon.rectTransform.anchoredPosition = new Vector2(0, 0);
                        __instance.icon.rectTransform.sizeDelta = new Vector2(100, 100);
                    }
                }
                GunSetter.Instance?.ResetWeapons(false);
                FistControl.Instance?.ResetFists();
                return false;
            }
            else return true;
        }
    }
}
