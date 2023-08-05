using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(EndlessGrid), "NextWave")]
    class EndlessGrid_NextWave_Patch
    {
        public static void Prefix(EndlessGrid __instance)
        {
            if (Core.DataExists() && Multiworld.Authenticated)
            {
                if (__instance.currentWave != 0 && __instance.currentWave % 5 == 0) LocationManager.GetRandomHint();
            }
        }
    }
}
