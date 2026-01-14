using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(SecretMissionPit), "OnTriggerEnter")]
    class SecretMissionPit_OnTriggerEnter_Patch
    {
        public static bool Prefix(SecretMissionPit __instance)
        {
            if (Core.DataExists())
            {
                string level = __instance.missionNumber + "-S";
                if (__instance.halfUnlock && (level == Core.data.goal || !Core.data.secretExitUnlock)) return false;
                return true;
            }
            return true;
        }
    }
}
