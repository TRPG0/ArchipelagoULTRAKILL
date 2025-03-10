using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(OptionsMenuToManager), "SetPauseMenu")]
    public class OptionsMenuToManager_SetPauseMenu_Patch
    {
        public static void Postfix(OptionsMenuToManager __instance)
        {
            if (Core.DataExists())
            {
                if (Core.CurrentLevelHasSkulls) UIManager.CreatePauseSkullIcons(__instance.pauseMenu);
                if (Core.CurrentLevelHasSwitches) UIManager.CreatePauseSwitchIcons(__instance.pauseMenu);
            }
        }
    }
}
