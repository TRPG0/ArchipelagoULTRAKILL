using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(FinalDoorOpener), "GoTime")]
    public class FinalDoorOpener_GoTime_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists() && Core.data.musicRandomizer && SceneHelper.CurrentScene == "Level 0-5") AudioManager.ChangeMusic();
        }
    }
}
