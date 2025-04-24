using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(Nailgun), "ShootZapper")]
    public class Nailgun_ShootZapper_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && Core.data.randomizeFire2 > Fire2Options.Disabled && !Core.data.unlockedFire2.Contains("nai2")) return false;
            else return true;
        }
    }
}
