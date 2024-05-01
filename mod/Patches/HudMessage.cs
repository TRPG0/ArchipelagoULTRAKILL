using ArchipelagoULTRAKILL.Components;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(HudMessage), "Done")]
    public class HudMessage_Done_Patch
    {
        public static void Postfix(HudMessage __instance)
        {
            if (Core.DataExists() && Core.data.hankRewards)
            {
                if (__instance.GetComponent<Hank>()) LocationManager.CheckLocation(StatsManager.Instance.levelNumber + "_ha");
            }
        }
    }
}
