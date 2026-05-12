using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(Shotgun), "Update")]
    public class Shotgun_Update_Patch
    {
        public static void Prefix(Shotgun __instance)
        {
            if (Core.DataExists())
            {
                if (__instance.variation == 0 && !Core.IsFire2Unlocked("sho0")) Traverse.Create(__instance).Field<TimeSince>("sinceLastCore").Value = 0f;
            }
        }
    }

    [HarmonyPatch(typeof(Shotgun), "ShootSinks")]
    public class Shotgun_ShootSinks_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && !Core.IsFire2Unlocked("sho0")) return false;
            return true;
        }
    }

    [HarmonyPatch(typeof(Shotgun), "Pump")]
    public class Shotgun_Pump_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && !Core.IsFire2Unlocked("sho1")) return false;
            return true;
        }
    }
}
