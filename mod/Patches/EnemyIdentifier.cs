using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Powerups;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(EnemyIdentifier), "Awake")]
    public class EnemyIdentifier_Awake_Patch
    {
        public static void Postfix(EnemyIdentifier __instance)
        {
            if (PlayerHelper.CurrentPowerup == Powerup.Confusion)
            {
                ConfusionPowerup.previousState.Add(__instance, __instance.madness);
                __instance.madness = true;
            }
            else if (PlayerHelper.CurrentPowerup == Powerup.Sandstorm)
            {
                SandstormTrap.previousState.Add(__instance, __instance.sandified);
                __instance.Sandify(true);
            }
        }
    }
}
