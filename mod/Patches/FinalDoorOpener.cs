using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(FinalDoorOpener), "GoTime")]
    public class FinalDoorOpener_GoTime_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists())
            {
                if (Core.data.musicRandomizer && Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Music == MusicType.Special2) AudioManager.ChangeMusic();
                if (PlayerHelper.Instance && !SceneHelper.CurrentScene.Contains("-S") && !(SceneHelper.CurrentScene == "CreditsMuseum2" || SceneHelper.CurrentScene == "uk_construct")) PlayerHelper.Instance.CanGetPowerup = true;
            }
        }
    }
}
