using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;

namespace ArchipelagoULTRAKILL.Config
{
    public static class DeathLinkConfig
    {
        public static bool Done { get; private set; } = false;

        public static ConfigPanel deathLinkPanel;
        public static ButtonArrayField deathLinkButtons;
        public static ConfigHeader deathLinkStatus;
        public static IntField deathLinkAmnesty;
        public static ConfigHeader deathLinkCount;

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            deathLinkPanel = new ConfigPanel(config.rootPanel, "DEATH LINK", "deathLinkPanel");

            deathLinkButtons = new ButtonArrayField(deathLinkPanel, "deathLinkButtons", 2, new float[] { 0.5f, 0.5f }, new string[] { "ENABLE", "DISABLE" });
            deathLinkButtons.OnClickEventHandler(0).onClick += () =>
            {
                if (Multiworld.Authenticated)
                {
                    Multiworld.EnableDeathLink();
                }
            };
            deathLinkButtons.OnClickEventHandler(1).onClick += () =>
            {
                if (Multiworld.Authenticated)
                {
                    Multiworld.DisableDeathLink();
                }
            };
            deathLinkStatus = new ConfigHeader(deathLinkPanel, "Not connected.");
            deathLinkAmnesty = new IntField(deathLinkPanel, "DEATH LINK AMNESTY", "deathLinkAmnesty", 1, 1, 10, true, true);
            deathLinkAmnesty.postValueChangeEvent += (int value) =>
            {
                Core.data.deathLinkAmnesty = value;
                Multiworld.currentDeathCount = 0;
                UpdateDeathLinkCount();
            };
            new ConfigHeader(deathLinkPanel, "Changing the amnesty value will reset the current death count.", 12);
            deathLinkCount = new ConfigHeader(deathLinkPanel, "Death count: 0/1");

            Done = true;
        }

        public static void UpdateDeathLinkCount()
        {
            if (Core.data.deathLink && Core.data.deathLinkAmnesty > 1) deathLinkCount.text = $"Death count: {Multiworld.currentDeathCount}/{Core.data.deathLinkAmnesty}";
            else deathLinkCount.text = "";
        }

        public static void LoadStats()
        {
            deathLinkAmnesty.value = Core.data.deathLinkAmnesty;
            if (!Multiworld.Authenticated) deathLinkStatus.text = "Not connected.";
            UpdateDeathLinkCount();
        }

        public static void ResetStatsDefaults()
        {
            deathLinkStatus.text = "Not connected.";
            deathLinkCount.text = "";
        }
    }
}
