using HarmonyLib;
using System.Reflection;

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
                    if ((unlocked && !__instance.weaponName.Contains("0") && !Core.data.purchasedItems.Contains(__instance.weaponName))
                        || (!unlocked && !__instance.weaponName.Contains("0") && Core.data.purchasedItems.Contains(__instance.weaponName))
                        || (!unlocked && __instance.weaponName.Contains("0")))
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
