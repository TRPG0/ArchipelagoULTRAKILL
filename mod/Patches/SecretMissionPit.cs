using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // add to list of completed levels
    [HarmonyPatch(typeof(SecretMissionPit), "OnTriggerEnter")]
    class OnTriggerEnter_Patch2
    {
        public static void Prefix(SecretMissionPit __instance)
        {
            if (Core.DataExists())
            {
                string level = __instance.missionNumber + "-S";
                if (!Core.data.completedLevels.Contains(level)) Core.data.completedLevels.Add(level);
            }
        }
    }
}
