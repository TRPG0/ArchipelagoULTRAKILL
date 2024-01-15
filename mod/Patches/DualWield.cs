using ArchipelagoULTRAKILL.Components;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // add more powerups after a dual wield powerup, if any
    [HarmonyPatch(typeof(DualWield), "EndPowerUp")]
    class DualWield_EndPowerUp_Patch
    {
        public static void Postfix()
        {
            PlayerHelper.Instance.EndPowerup();
            if (LocationManager.powerupQueue.Count > 0) Core.obj.GetComponent<Core>().Invoke("AddPowerup", 1f);
        }
    }
}
