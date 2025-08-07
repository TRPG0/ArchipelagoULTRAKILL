using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // check P rank location if necessary
    [HarmonyPatch(typeof(FinalRank), "SetRank")]
    class FinalRank_SetRank_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists()) {
                if (Core.data.pRankRewards && !AssistController.Instance.cheatsEnabled && StatsManager.Instance.rankScore == 12)
                    LocationManager.CheckLocation(StatsManager.Instance.levelNumber + "_p");
                if (Core.data.perfectGoal && Core.CurrentLevelInfo.Name == Core.data.goal && StatsManager.Instance.rankScore == 12)
                    Multiworld.SendCompletion();
            }
        }
    }
}
