using ArchipelagoULTRAKILL.Music;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(MusicChanger), "Change")]
    public class MusicChanger_Change_Patch
    {
        public static void Prefix(MusicChanger __instance)
        {
            if (__instance.TryGetComponent<NowPlayingChanger>(out NowPlayingChanger npc)) npc.Set();
        }
    }
}
