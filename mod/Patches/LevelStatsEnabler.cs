using HarmonyLib;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(LevelStatsEnabler), "Start")]
    public class LevelStatsEnabler_Start_Patch
    {
        public static void Postfix(LevelStatsEnabler __instance)
        {
            if (Core.DataExists())
            {
                __instance.gameObject.SetActive(true);
            }
        }
    }
}
