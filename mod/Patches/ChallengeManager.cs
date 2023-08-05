using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // check challenge location if necessary
    [HarmonyPatch(typeof(ChallengeManager), "ChallengeDone")]
    class ChallengeManager_ChallengeDone_Patch
    {
        public static void Prefix(ChallengeManager __instance)
        {
            if (Core.DataExists() && Core.data.challengeRewards && !__instance.challengeFailed)
                LocationManager.CheckLocation(StatsManager.Instance.levelNumber + "_c");
        }
    }
}
