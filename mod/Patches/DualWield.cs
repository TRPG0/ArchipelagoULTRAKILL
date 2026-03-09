using ArchipelagoULTRAKILL.Components;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(DualWield), "EndPowerUp")]
    class DualWield_EndPowerUp_Patch
    {
        public static void Postfix()
        {
            PlayerHelper.Instance.EndPowerup();
        }
    }
}
