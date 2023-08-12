using HarmonyLib;
using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(BestiaryData), "SetEnemy")]
    public class BestiaryData_SetEnemy_Patch
    {
        public static List<EnemyType> bosses = new List<EnemyType>()
        {
            EnemyType.V2,
            EnemyType.Minos,
            EnemyType.Gabriel,
            EnemyType.V2Second,
            EnemyType.Leviathan,
            EnemyType.GabrielSecond,
            EnemyType.VeryCancerousRodent,
            EnemyType.Mandalore
        };

        public static void Prefix(ref EnemyType enemy, ref int newState)
        {
            if (Core.DataExists() && Core.data.bossRewards && newState >= 2 && bosses.Contains(enemy))
            {
                LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_b");
            }
        }
    }
}
