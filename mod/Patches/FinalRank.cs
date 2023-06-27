﻿using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // check P rank location if necessary
    [HarmonyPatch(typeof(FinalRank), "SetRank")]
    class SetRank_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists() && Core.data.pRankRewards && !AssistController.Instance.cheatsEnabled && StatsManager.Instance.rankScore == 12)
                LocationManager.CheckLocation(StatsManager.Instance.levelNumber + "_p");
        }
    }
}
