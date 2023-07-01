using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // prevent pumping
    [HarmonyPatch(typeof(Shotgun), "Pump")]
    class Pump_Patch
    {
        public static bool Prefix(Shotgun __instance)
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("sho1") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to pump shotgun. Locked");
                Traverse.Create(__instance).Field<bool>("gunReady").Value = true;
                return false;
            }
            else return true;
        }
    }

    // prevent core eject
    [HarmonyPatch(typeof(Shotgun), "ShootSinks")]
    class ShootSinks_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("sho0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                //Debug.Log("Attempted to eject core. Locked");
                return false;
            }
            else return true;
        }
    }

    // prevent charge meter from filling
    [HarmonyPatch(typeof(Shotgun), "UpdateMeter")]
    class UpdateMeter_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && !Core.data.unlockedFire2.Contains("sho0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
            {
                return false;
            }
            else return true;
        }
    }
}
