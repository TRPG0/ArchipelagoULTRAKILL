using ArchipelagoULTRAKILL.Config;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(OptionsManager), "Start")]
    public class OptionsManager_Start_Patch
    {
        public static void Postfix()
        {
            OptionsManager.forceRadiance = ModifierConfig.permaRadiance.value;
            OptionsManager.radianceTier = ModifierConfig.radianceLevel.value;
            //OptionsManager.forceSand = ModifierConfig.permaSand.value;
        }
    }
}
