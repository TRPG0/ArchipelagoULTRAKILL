using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(BigDoorOpener), "OnEnable")]
    public class BigDoorOpener_OnEnable_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists() && Core.data.bossRewards > 0 && SceneHelper.CurrentLevelNumber == 5)
            {
                LocationManager.CheckLocation("5_b");
            }
        }
    }
}
