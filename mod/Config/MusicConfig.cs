using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;

namespace ArchipelagoULTRAKILL.Config
{
    public static class MusicConfig
    {
        public static bool Done { get; private set; } = false;

        public static ConfigPanel musicPanel;
        public static BoolField enabled;
        public static BoolField allowPreload;
        public static BoolField showNowPlaying;
        public static IntField nowPlayingMaxOpacity;

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            musicPanel = new ConfigPanel(config.rootPanel, "MUSIC", "musicPanel");

            enabled = new BoolField(musicPanel, "ENABLE RANDOM MUSIC", "musicEnabled", false);
            enabled.onValueChange += (BoolField.BoolValueChangeEvent e) => {
                Core.data.musicRandomizer = e.value;
            };
            new ConfigHeader(musicPanel, "Level must be reloaded for changes to take effect.", 12);

            allowPreload = new BoolField(musicPanel, "ALLOW PRELOADING", "musicAllowPreload", true);
            new ConfigHeader(musicPanel, "Preloading music may affect load times and performance. However, if disabled, some music tracks will not be able to play in levels they do not normally appear in.", 12);

            new ConfigHeader(musicPanel, "---");

            showNowPlaying = new BoolField(musicPanel, "SHOW NOW PLAYING", "musicShowNowPlaying", true);
            nowPlayingMaxOpacity = new IntField(musicPanel, "NOW PLAYING OPACITY", "musicNowPlayingMaxOpacity", 100, 1, 100, true);

            Done = true;
        }

        public static void LoadStats()
        {
            enabled.value = Core.data.musicRandomizer;
        }

        public static void ResetStatsDefaults()
        {
            enabled.value = false;
        }
    }
}
