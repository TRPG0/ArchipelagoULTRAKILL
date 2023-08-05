using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(LevelStats), "Start")]
    public class LevelStats_Start_Patch
    {
        public static void Postfix(LevelStats __instance)
        {
            if (Core.DataExists())
            {
                if (StockMapInfo.Instance != null) __instance.levelName.text = StockMapInfo.Instance.assets.LargeText;
                Traverse.Create(__instance).Field<bool>("ready").Value = true;
            }
        }
    }
}
