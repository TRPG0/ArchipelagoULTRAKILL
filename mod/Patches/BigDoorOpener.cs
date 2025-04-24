using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(BigDoorOpener), "OnEnable")]
    public class BigDoorOpener_OnEnable_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists() && !Multiworld.ServerVersionIsAtLeast("3.2.0") && Core.data.enemyRewards > EnemyOptions.Disabled && SceneHelper.CurrentLevelNumber == 5)
            {
                LocationManager.CheckLocation("5_b");
            }
        }
    }
}
