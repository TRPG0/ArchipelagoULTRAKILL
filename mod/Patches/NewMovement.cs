using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // prevent sliding
    [HarmonyPatch(typeof(NewMovement), "StartSlide")]
    class NewMovement_StartSlide_Patch
    {
        public static bool Prefix(NewMovement __instance)
        {
            if (Core.DataExists() && Core.IsPlaying && !Core.data.canSlide)
            {
                //Debug.Log("Attempted to slide, canSlide is false.");
                __instance.StopSlide();
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(NewMovement), "Respawn")]
    class NewMovement_Respawn_Patch
    {
        public static void Postfix()
        {
            Multiworld.DeathLinkKilling = false;
            if (Core.uim.deathLinkMessage != null) Core.uim.deathLinkMessage.text.text = "";
        }
    }
}
