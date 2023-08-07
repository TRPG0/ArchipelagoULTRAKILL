using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(PowerUpMeter), "Start")]
    public class PowerUpMeter_Start_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists() && !Core.poweredUp && LocationManager.powerupQueue.Count > 0) Core.obj.GetComponent<Core>().Invoke("AddPowerup", 1f);
        }
    }
}
