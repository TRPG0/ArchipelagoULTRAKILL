using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(BestiaryData), "SetEnemy")]
    public class BestiaryData_SetEnemy_Patch
    {
        public static List<EnemyType> standard = new List<EnemyType>()
        {
            EnemyType.V2,
            EnemyType.Minos,
            EnemyType.Gabriel,
            EnemyType.V2Second,
            EnemyType.Leviathan,
            EnemyType.GabrielSecond,
            EnemyType.Centaur
        };

        public static List<EnemyType> extended = new List<EnemyType>() 
        { 
            EnemyType.VeryCancerousRodent,
            EnemyType.Mandalore
        };

        public static void Prefix(ref EnemyType enemy, ref int newState)
        {
            if (Core.DataExists() && newState >= 2)
            {
                if (Multiworld.ServerVersionIsAtLeast("3.2.0") && Core.data.enemyRewards > EnemyOptions.Disabled)
                {
                    if ((enemy == EnemyType.V2 && StatsManager.Instance.levelNumber != 9)
                        || (enemy == EnemyType.Gabriel && StatsManager.Instance.levelNumber != 15)
                        || (enemy == EnemyType.Centaur && StatsManager.Instance.levelNumber != 29))
                        return;

                    string location = $"e_{enemy.ToString().ToLower()}";
                    if (!Core.data.@checked.Contains(location)) LocationManager.CheckLocation(location);
                }
                else
                {
                    if (standard.Contains(enemy) && Core.data.enemyRewards > EnemyOptions.Disabled) LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_b");
                    if (extended.Contains(enemy) && Core.data.enemyRewards >= EnemyOptions.Extra) LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_b");
                }
            }
        }
    }
}
