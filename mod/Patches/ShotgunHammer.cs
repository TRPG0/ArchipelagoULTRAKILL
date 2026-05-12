using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(ShotgunHammer), "ThrowNade")]
    public class ShotgunHammer_ThrowNade_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && !Core.IsFire2Unlocked("sho0")) return false;
            return true;
        }
    }

    [HarmonyPatch(typeof(ShotgunHammer), "Pump")]
    public class ShotgunHammer_Pump_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && !Core.IsFire2Unlocked("sho1")) return false;
            return true;
        }
    }
}
