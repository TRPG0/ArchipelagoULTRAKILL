using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(OptionsManager), "Start")]
    public class OptionsManager_Start_Patch
    {
        public static void Postfix()
        {
            OptionsManager.forceRadiance = ConfigManager.permaRadiance.value;
            OptionsManager.radianceTier = ConfigManager.radianceLevel.value;
            OptionsManager.forceSand = ConfigManager.permaSand.value;
        }
    }
}
