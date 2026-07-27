using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using ULTRAKILL.Cheats;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(Revolver), "Update")]
    public class Revolver_Update_Patch
    {
        public static void Postfix(Revolver __instance)
        {
            if (Core.DataExists())
            {
                if (!NoWeaponCooldown.NoCooldown && Core.data.randomizeFire2 > Fire2Options.Disabled)
                {
                    if (__instance.gunVariation == 0 && !Core.IsFire2Unlocked("rev0"))
                    {   
                        Traverse.Create(__instance).Field<bool>("pierceReady").Value = false;
                        __instance.pierceCharge = 0;
                        __instance.pierceShotCharge = 0;
                    }
                }
            }
        }
    }
}
