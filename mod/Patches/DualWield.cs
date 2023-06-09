﻿using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // add more powerups after a dual wield powerup, if any
    [HarmonyPatch(typeof(DualWield), "EndPowerUp")]
    class EndPowerUp_Patch
    {
        public static void Postfix()
        {
            if (LocationManager.powerupQueue.Count > 0) Core.obj.GetComponent<Core>().Invoke("AddPowerup", 1f);
        }
    }
}
