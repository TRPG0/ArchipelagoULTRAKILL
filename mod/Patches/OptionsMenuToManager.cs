using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(OptionsMenuToManager), "SetPauseMenu")]
    public class OptionsMenuToManager_SetPauseMenu_Patch
    {
        public static void Postfix(OptionsMenuToManager __instance)
        {
            if (Core.DataExists() && Core.data.randomizeSkulls && Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Skulls > Structures.SkullsType.None)
            {
                bool createdSwitches = false;
                UIManager.CreatePauseSwitchIcons(__instance.pauseMenu, ref createdSwitches);
                UIManager.CreatePauseSkullIcons(__instance.pauseMenu, createdSwitches);
            }
        }
    }
}
