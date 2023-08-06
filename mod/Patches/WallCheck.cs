using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(WallCheck), "Update")]
    public class WallCheck_Update_Patch
    {
        public static bool Prefix(WallCheck __instance)
        {
            if (Core.doublejumpPowerup)
            {
                __instance.onWall = true;
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(WallCheck), "CheckForCols")]
    public class WallCheck_CheckForCols_Patch
    {
        public static void Postfix(WallCheck __instance, ref bool __result)
        {
            if (Core.doublejumpPowerup)
            {
                if (__instance.cols.Count == 0) __instance.poc = __instance.transform.position;
                __result = true;
            }
        }
    }
}
