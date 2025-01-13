using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // prevent freezing rockets
    [HarmonyPatch(typeof(RocketLauncher), "FreezeRockets")]
    class RocketLauncher_FreezeRockets_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && (Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("rock0") || PlayerHelper.CurrentPowerup == Powerup.EmptyAmmo) 
                && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to freeze rockets. Locked");
                return false;
            }
            else return true;
        }
    }
}