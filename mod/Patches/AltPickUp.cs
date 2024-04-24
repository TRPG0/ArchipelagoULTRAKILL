using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;

namespace ArchipelagoULTRAKILL.Patches
{
    // send check when picking up an alternate weapon
    [HarmonyPatch(typeof(AltPickUp), "GotActivated")]
    class AltPickUp_GotActivated_Patch
    {
        public static bool Prefix(AltPickUp __instance)
        {
            if (Core.DataExists() && StatsManager.Instance.levelNumber != 27)
            {
                UnityEvent unityEvent = __instance.onPickUp;
                if (unityEvent != null) unityEvent.Invoke();
                Object.Destroy(__instance.gameObject);

                LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_w2");

                return false;
            }
            else return true;
        }
    }
}
