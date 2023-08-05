using HarmonyLib;
using System.Reflection;

namespace ArchipelagoULTRAKILL.Patches
{
    // update item descriptions in shop
    [HarmonyPatch(typeof(VariationInfo), "Start")]
    class VariationInfo_Start_Patch
    {
        public static void Postfix(VariationInfo __instance)
        {
            if (Core.DataExists()) LevelManager.UpdateShopObjects();
        }
    }

    // update item prices in shop
    [HarmonyPatch(typeof(VariationInfo), "UpdateMoney")]
    class VariationInfo_UpdateMoney_Patch
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
}
