using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(FinalDoorOpener), "GoTime")]
    public class FinalDoorOpener_GoTime_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists() && Core.data.musicRandomizer && Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Music == MusicType.Special2) AudioManager.ChangeMusic();
        }
    }
}
