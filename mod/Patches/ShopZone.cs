﻿using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // update shop objects when entering zone
    [HarmonyPatch(typeof(ShopZone), "TurnOn")]
    class TurnOn_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists()) LevelManager.UpdateShopObjects();
        }
    }
}
