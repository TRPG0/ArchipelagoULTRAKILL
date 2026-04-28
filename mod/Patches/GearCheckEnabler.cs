using HarmonyLib;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(GearCheckEnabler), "Start")]
    public class GearCheckEnabler_Start_Patch
    {
        public static bool Prefix(GearCheckEnabler __instance)
        {
            if (Core.DataExists())
            {
                if (__instance.gear == "revalt") return false;
                if (SceneHelper.CurrentScene == "Level 0-1" && Core.data.completedLevels.Contains("0-1"))
                {
                    foreach (GameObject gameObject in __instance.toActivate) gameObject.SetActive(true);
                    foreach (GameObject gameObject in __instance.toDisactivate) gameObject.SetActive(false);
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
