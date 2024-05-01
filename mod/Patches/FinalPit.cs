using ArchipelagoULTRAKILL.Components;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // load main menu instead of next level
    [HarmonyPatch(typeof(FinalPit), "OnTriggerEnter")]
    class FinalPit_OnTriggerEnter_Patch
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
    class FinalPit_SendInfo_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists())
            {
                if (PlayerHelper.Instance)
                {
                    PlayerHelper.Instance.EndPowerup();
                    PlayerHelper.Instance.CanGetPowerup = false;
                }
                
                if (AssistController.Instance.cheatsEnabled)
                {
                    Core.Logger.LogWarning("Cheats enabled! Completion not counted.");
                }
                else
                {
                    if (Core.CurrentLevelInfo != null)
                    {
                        if (Core.CurrentLevelInfo.Name == Core.data.goal && Multiworld.Authenticated)
                        {
                            Multiworld.SendCompletion();
                        }
                        else
                        {
                            Core.Logger.LogInfo($"Completed level: {Core.CurrentLevelInfo.Name} | {Core.CurrentLevelInfo.Id}");
                            if (!Core.data.completedLevels.Contains(Core.CurrentLevelInfo.Name))
                            {
                                Core.data.completedLevels.Add(Core.CurrentLevelInfo.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
