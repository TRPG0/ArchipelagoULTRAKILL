using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // send check when picking up weapon
    [HarmonyPatch(typeof(WeaponPickUp), "GotActivated")]
    class WeaponPickUp_GotActivated_Patch
    {
        public static bool Prefix(WeaponPickUp __instance)
        {
            if (Core.DataExists())
            {
                Traverse wpu = Traverse.Create(__instance);
                wpu.Field<bool>("activated").Value = true;
                if (__instance.activateOnPickup != null) __instance.activateOnPickup.SetActive(true);
                __instance.gameObject.SetActive(false);

                LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_w1");

                return false;
            }
            else return true;
        }
    }
}
