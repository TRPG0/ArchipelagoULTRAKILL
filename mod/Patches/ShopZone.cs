using HarmonyLib;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(ShopZone), "TurnOff")]
    public class ShopZone_TurnOff_Patch
    {
        public static void Postfix()
        {
            if (Core.DataExists()) Core.ValidateArms();
        }
    }
}
