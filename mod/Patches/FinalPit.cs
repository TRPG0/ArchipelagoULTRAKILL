using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // load main menu instead of next level
    [HarmonyPatch(typeof(FinalPit), "OnTriggerEnter")]
    class OnTriggerEnter_Patch
    {
        public static void Prefix(FinalPit __instance)
        {
            if (Core.DataExists())
            {
                if (!__instance.targetLevelName.Contains("-S")) __instance.targetLevelName = "Main Menu";
            }
        }
    }

    // add to list of completed levels, check challenge location if necessary
    [HarmonyPatch(typeof(FinalPit), "SendInfo")]
    class SendInfo_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists())
            {
                Core.playerActive = false;
                Core.poweredUp = false;
                if (AssistController.Instance.cheatsEnabled)
                {
                    Core.logger.LogWarning("Cheats enabled! Completion not counted.");
                }
                else
                {
                    if (Core.idToLevel.ContainsKey(StatsManager.Instance.levelNumber))
                    {
                        if (Core.idToLevel[StatsManager.Instance.levelNumber] == Core.data.goal && Multiworld.Authenticated)
                        {
                            Multiworld.SendCompletion();
                        }
                        else
                        {
                            Core.logger.LogInfo("Completed level: " + Core.idToLevel[StatsManager.Instance.levelNumber]);
                            if (!Core.data.completedLevels.Contains(Core.idToLevel[StatsManager.Instance.levelNumber]))
                            {
                                Core.data.completedLevels.Add(Core.idToLevel[StatsManager.Instance.levelNumber]);
                            }
                        }
                    }
                }
            }
        }
    }
}
