using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(GearCheckEnabler), "Start")]
    public class GearCheckEnabler_Start_Patch
    {
        public static bool Prefix(GearCheckEnabler __instance)
        {
            if (Core.DataExists() && __instance.gear == "revalt") return false;
            else return true;
        }
    }
}
