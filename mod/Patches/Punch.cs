using ArchipelagoULTRAKILL.Components;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(Punch), "PunchStart")]
    public class Punch_PunchStart_Patch
    {
        public static bool Prefix(Punch __instance)
        {
            if (PlayerHelper.CurrentPowerup == Structures.Powerup.NoArms
                || !Core.data.hasArm && __instance.type == FistType.Standard && SceneHelper.CurrentScene != "Level 5-S")
                return false;
            return true;
        }
    }
}
