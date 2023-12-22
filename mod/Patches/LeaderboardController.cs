using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(LeaderboardController), "SubmitLevelScore")]
    class LeaderboardController_SubmitLevelScore_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists())
            {
                Core.logger.LogInfo("Current save file is randomized. Skipped leaderboard submission.");
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(LeaderboardController), "SubmitCyberGrindScore")]
    class LeaderboardController_SubmitCyberGrindScore_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists())
            {
                Core.logger.LogInfo("Current save file is randomized. Skipped leaderboard submission.");
                return false;
            }
            else return true;
        }
    }
}
