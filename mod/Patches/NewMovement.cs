using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // prevent sliding
    [HarmonyPatch(typeof(NewMovement), "StartSlide")]
    class StartSlide_Patch
    {
        public static bool Prefix(NewMovement __instance)
        {
            if (Core.DataExists() && Core.playerActive && !Core.data.canSlide)
            {
                //Debug.Log("Attempted to slide, canSlide is false.");
                __instance.StopSlide();
                return false;
            }
            else return true;
        }
    }
}
