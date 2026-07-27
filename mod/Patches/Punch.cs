using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(Punch), "PunchStart")]
    public class Punch_PunchStart_Patch
    {
        public static bool Prefix(Punch __instance)
        {
            if (Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Flags.HasFlag(InfoFlags.HasFixedLoadout)) return true;

            if (PlayerHelper.CurrentPowerup == Structures.Powerup.NoArms
                || !Core.data.hasArm && __instance.type == FistType.Standard && SceneHelper.CurrentScene != "Level 5-S")
                return false;

            return true;
        }
    }
}
