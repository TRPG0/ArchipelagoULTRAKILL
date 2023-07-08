using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(LeaderboardController), "SubmitLevelScore")]
    class SubmitLevelScore_Patch
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
