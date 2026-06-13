using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections.Generic;

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
                if (!__instance.targetLevelName.Contains("-S")
                    || __instance.targetLevelName.Contains("-S") && !Core.data.secretExitUnlock) __instance.targetLevelName = "Main Menu";
            }
        }
    }

    // add to list of completed levels, check challenge location if necessary
    [HarmonyPatch(typeof(FinalPit), "SendInfo")]
    class FinalPit_SendInfo_Patch
    {
        static Dictionary<int, string> secretExits = new Dictionary<int, string>()
        {
            [2] = "5B - Secret Arena/5B Nonstuff/Pit (2)/Cube (1)",
            [6] = "1 - First Field/1 Nonstuff/FinalRoom SecretEntrance/Pit/Cube (1)",
            [12] = "2 - Sewer Arena/2 Nonstuff/Secret Level Entrance/FinalRoom SecretEntrance/Pit/Cube (1)",
            [17] = "FakeMoon/FinalRoom SecretEntrance/Pit/Cube (1)",
            [20] = "2 - Elevator/2B Secret/FinalRoom SecretEntrance/Pit/Cube (1)",
            [28] = "2 - Garden Maze/Secret/FinalRoom SecretEntrance/Pit/Cube (1)"
        };

        public static void Prefix(FinalPit __instance)
        {
            if (Core.DataExists())
            {
                if (Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Flags.HasFlag(InfoFlags.HasSecretExit))
                {
                    int level = StatsManager.Instance.levelNumber;
                    if (FinalPit_SendInfo_Patch.secretExits.ContainsKey(level))
                    {
                        string fullPath = __instance.gameObject.GetFullPath();
                        if (!SceneHelper.CurrentScene.Contains("-S") && !Core.data.secretExitComplete && fullPath == FinalPit_SendInfo_Patch.secretExits[level]) PlayerHelper.IsSecretExiting = true;
                    }
                }

                if (PlayerHelper.Instance)
                {
                    PlayerHelper.Instance.EndPowerup();
                    PlayerHelper.Instance.CanGetPowerup = false;
                }
            }
        }

        public static void Postfix(FinalPit __instance)
        {
            if (Core.DataExists())
            {
                if (AssistController.Instance.cheatsEnabled)
                {
                    Core.Logger.LogWarning("Cheats enabled! Completion not counted.");
                }
                else
                {
                    if (Core.CurrentLevelInfo != null)
                    {
                        if (Core.CurrentLevelInfo.Name == Core.data.goal)
                        {
                            if (!Core.data.perfectGoal
                                || Core.data.perfectGoal && Core.data.goal.Contains("S"))
                                Multiworld.SendCompletion();
                        }
                        else if (PlayerHelper.IsSecretExiting)
                        {
                            LocationManager.CheckLocation($"se_{Core.CurrentLevelInfo.Layer}");
                            return;
                        }
                        else
                        {
                            Core.Logger.LogInfo($"Completed level: {Core.CurrentLevelInfo.Name} | {Core.CurrentLevelInfo.Id}");
                            if (!Core.data.completedLevels.Contains(Core.CurrentLevelInfo.Name))
                            {
                                Core.data.completedLevels.Add(Core.CurrentLevelInfo.Name);
                                Multiworld.UpdateCompletedLevels();
                            }
                        }
                    }
                }
            }
        }
    }
}
