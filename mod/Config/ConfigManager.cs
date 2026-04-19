using PluginConfig.API;
using PluginConfig.API.Decorators;
using System.IO;

namespace ArchipelagoULTRAKILL.Config
{
    public static class ConfigManager
    {
        public static PluginConfigurator config;
        public static ConfigHeader versionCheck;

        public static void Initialize()
        {
            if (config != null) return;
            config = PluginConfigurator.Create(Core.PluginName, Core.PluginGUID);

            string iconPath = Path.Combine(Core.workingDir, "icon.png");
            if (File.Exists(iconPath)) config.SetIconWithURL(iconPath);

            new ConfigHeader(config.rootPanel, "ARCHIPELAGO");
            versionCheck = new ConfigHeader(config.rootPanel, "", 16);
            versionCheck.hidden = true;

            PlayerConfig.Initialize(config);
            MusicConfig.Initialize(config);
            DeathLinkConfig.Initialize(config);
            ModifierConfig.Initialize(config);
            new ConfigHeader(config.rootPanel, "---");
            UIConfig.Initialize(config);
            ColorConfig.Initialize(config);
            LinksConfig.Initialize(config);
        }

        public static void LoadConnectionInfo()
        {
            PlayerConfig.LoadConnectionInfo();
        }

        public static void LoadStats()
        {
            PlayerConfig.LoadStats();
            MusicConfig.LoadStats();
            DeathLinkConfig.LoadStats();
        }

        public static void ResetStatsDefaults()
        {
            PlayerConfig.ResetStatsDefaults();
            MusicConfig.ResetStatsDefaults();
            DeathLinkConfig.ResetStatsDefaults();
        }
    }
}
