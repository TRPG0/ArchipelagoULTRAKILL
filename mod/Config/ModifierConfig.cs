using PluginConfig.API;
using PluginConfig.API.Fields;

namespace ArchipelagoULTRAKILL.Config
{
    public static class ModifierConfig
    {
        public static bool Done { get; private set; } = false;

        public static ConfigPanel modifierPanel;
        public static BoolField permaRadiance;
        public static FloatField radianceLevel;
        //public static BoolField permaSand;

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            modifierPanel = new ConfigPanel(config.rootPanel, "MODIFIERS", "modifierPanel");

            permaRadiance = new BoolField(modifierPanel, "PERMANENT RADIANCE", "permaRadiance", false, true);
            permaRadiance.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                OptionsManager.forceRadiance = e.value;
            };
            radianceLevel = new FloatField(modifierPanel, "RADIANCE TIER", "radianceLevel", 1f, true);
            radianceLevel.onValueChange += (FloatField.FloatValueChangeEvent e) =>
            {
                OptionsManager.radianceTier = e.value;
            };

            /*
            new ConfigHeader(modifierPanel, "-----");
            permaSand = new BoolField(modifierPanel, "PERMANENT SAND", "permaSand", false, true);
            permaSand.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                OptionsManager.forceSand = e.value;
            };
            new ConfigHeader(modifierPanel, "Changes will not affect enemies that have already spawned.", 12);
            */

            Done = true;
        }
    }
}
