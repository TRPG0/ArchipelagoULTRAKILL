using ArchipelagoULTRAKILL.Music;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(MusicManager), "StartMusic")]
    public class MusicManager_StartMusic_Patch
    {
        public static void Postfix(MusicManager __instance)
        {
            if (!__instance.forcedOff) NowPlaying.Instance?.Show();
        }
    }

    [HarmonyPatch(typeof(MusicManager), "ArenaMusicStart")]
    public class MusicManager_ArenaMusicStart_Patch
    {
        public static void Postfix(MusicManager __instance)
        {
            if (!__instance.forcedOff) NowPlaying.Instance?.Show();
        }
    }
}
