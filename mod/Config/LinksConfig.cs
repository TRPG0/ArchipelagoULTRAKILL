using PluginConfig.API;
using PluginConfig.API.Functionals;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Config
{
    public static class LinksConfig
    {
        public static bool Done { get; private set; } = false;

        public static ConfigPanel linksPanel;
        public static ButtonField thunderstoreButton;
        public static ButtonField githubButton;
        public static ButtonField discordButton;
        public static ButtonField poptrackerButton;

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            linksPanel = new ConfigPanel(config.rootPanel, "LINKS", "linksPanel");

            thunderstoreButton = new ButtonField(linksPanel, "THUNDERSTORE", "thunderstoreButton");
            thunderstoreButton.onClick += () => { Application.OpenURL("https://thunderstore.io/c/ultrakill/p/TRPG/Archipelago/"); };
            githubButton = new ButtonField(linksPanel, "GITHUB", "githubButton");
            githubButton.onClick += () => { Application.OpenURL("https://github.com/TRPG0/ArchipelagoULTRAKILL"); };
            discordButton = new ButtonField(linksPanel, "AP AFTER DARK DISCORD", "discordButton");
            discordButton.onClick += () => { Application.OpenURL("https://discord.gg/Sbhy4ykUKn"); };
            poptrackerButton = new ButtonField(linksPanel, "POPTRACKER PACK", "poptrackerButton");
            poptrackerButton.onClick += () => { Application.OpenURL("https://github.com/BowserCrusher/UltrakillPoptracker/releases/"); };

            Done = true;
        }
    }
}
