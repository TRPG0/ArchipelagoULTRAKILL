using ArchipelagoULTRAKILL.Structures;
using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Config
{
    public static class ColorConfig
    {
        public static bool Done { get; private set; } = false;

        public static ConfigPanel colorPanel;

        public static EnumField<ColorOptions> uiColorRandomizer;
        public static EnumField<ColorOptions> gunColorRandomizer;
        public static ButtonField enableCustomButton;
        public static ButtonField disableCustomButton;

        public static ColorField actHighlightColor;
        public static ColorField actCompleteColor;
        public static ColorField actGoalColor;

        public static ColorField APPlayerSelf;
        public static ColorField APPlayerOther;
        public static ColorField APItemAdvancement;
        public static ColorField APItemNeverExclude;
        public static ColorField APItemFiller;
        public static ColorField APItemTrap;
        public static ColorField APLocation;

        public static ColorField layer0Color;
        public static ColorField layer1Color;
        public static ColorField layer2Color;
        public static ColorField layer3Color;
        public static ColorField layer4Color;
        public static ColorField layer5Color;
        public static ColorField layer6Color;
        public static ColorField layer7Color;
        public static ColorField layer8Color;
        public static ColorField encore0Color;
        public static ColorField encore1Color;
        public static ColorField primeColor;
        public static ColorField altColor;
        public static ColorField blueSkullColor;
        public static ColorField redSkullColor;
        public static ColorField switchColor;
        public static ColorField pointsColor;
        public static ColorField dualwieldColor;
        public static ColorField doublejumpColor;
        public static ColorField confusionColor;
        public static ColorField trapColor;

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            colorPanel = new ConfigPanel(config.rootPanel, "COLOR SETTINGS", "colorPanel");

            uiColorRandomizer = new EnumField<ColorOptions>(colorPanel, "UI COLOR RANDOMIZER", "uiColorRandomizer", ColorOptions.Off, true);
            uiColorRandomizer.SetEnumDisplayName(ColorOptions.Off, "DISABLED");
            uiColorRandomizer.SetEnumDisplayName(ColorOptions.Once, "ONCE");
            uiColorRandomizer.SetEnumDisplayName(ColorOptions.EveryLoad, "EVERY NEW LEVEL LOADED");

            gunColorRandomizer = new EnumField<ColorOptions>(colorPanel, "GUN COLOR RANDOMIZER", "gunColorRandomizer", ColorOptions.Off, true);
            gunColorRandomizer.SetEnumDisplayName(ColorOptions.Off, "DISABLED");
            gunColorRandomizer.SetEnumDisplayName(ColorOptions.Once, "ONCE");
            gunColorRandomizer.SetEnumDisplayName(ColorOptions.EveryLoad, "EVERY NEW LEVEL LOADED");

            enableCustomButton = new ButtonField(colorPanel, "ENABLE ALL CUSTOM WEAPON COLORS", "enableCustomButton");
            enableCustomButton.onClick += () =>
            {
                PrefsManager.Instance.SetBool("gunColorType.1", true);
                PrefsManager.Instance.SetBool("gunColorType.1.a", true);
                PrefsManager.Instance.SetBool("gunColorType.2", true);
                PrefsManager.Instance.SetBool("gunColorType.3", true);
                PrefsManager.Instance.SetBool("gunColorType.3.a", true);
                PrefsManager.Instance.SetBool("gunColorType.4", true);
                PrefsManager.Instance.SetBool("gunColorType.5", true);
            };

            disableCustomButton = new ButtonField(colorPanel, "DISABLE ALL CUSTOM WEAPON COLORS", "disableCustomButton");
            disableCustomButton.onClick += () =>
            {
                PrefsManager.Instance.SetBool("gunColorType.1", false);
                PrefsManager.Instance.SetBool("gunColorType.1.a", false);
                PrefsManager.Instance.SetBool("gunColorType.2", false);
                PrefsManager.Instance.SetBool("gunColorType.3", false);
                PrefsManager.Instance.SetBool("gunColorType.3.a", false);
                PrefsManager.Instance.SetBool("gunColorType.4", false);
                PrefsManager.Instance.SetBool("gunColorType.5", false);
            };

            new ConfigHeader(colorPanel, "ACT SUMMARY COLORS");
            actHighlightColor = new ColorField(colorPanel, "HIGHLIGHTED", "actHighlightColor", Color.red, true);
            actCompleteColor = new ColorField(colorPanel, "COMPLETED", "actCompleteColor", Colors.Perfect, true);
            actGoalColor = new ColorField(colorPanel, "GOAL", "actGoalColor", Color.green, true);

            new ConfigHeader(colorPanel, "ARCHIPELAGO COLORS");
            APPlayerSelf = new ColorField(colorPanel, "PLAYER (YOU)", "APPlayerSelf", new Color(0.93f, 0, 0.93f), true);
            APPlayerOther = new ColorField(colorPanel, "PLAYER (OTHERS)", "APPlayerOther", new Color(0.98f, 0.98f, 0.82f), true);
            APItemFiller = new ColorField(colorPanel, "ITEM (FILLER)", "APItemFiller", new Color(0, 0.93f, 0.93f), true);
            APItemNeverExclude = new ColorField(colorPanel, "ITEM (USEFUL)", "APItemNeverExclude", new Color(0.43f, 0.55f, 0.91f), true);
            APItemAdvancement = new ColorField(colorPanel, "ITEM (PROGRESSION)", "APItemAdvancement", new Color(0.69f, 0.6f, 0.94f), true);
            APItemTrap = new ColorField(colorPanel, "ITEM (TRAP)", "APItemTrap", new Color(0.98f, 0.5f, 0.45f), true);
            APLocation = new ColorField(colorPanel, "LOCATION", "APLocation", new Color(0, 1, 0.5f), true);

            new ConfigHeader(colorPanel, "ITEM / LOCATION COLORS");
            layer0Color = new ColorField(colorPanel, "LAYER 0: PRELUDE", "layer0Color", new Color(1, 0.5f, 0.25f), true);
            layer1Color = new ColorField(colorPanel, "LAYER 1: LIMBO", "layer1Color", new Color(0.2667f, 1, 0.2706f), true);
            layer2Color = new ColorField(colorPanel, "LAYER 2: LUST", "layer2Color", new Color(0.765f, 0.25f, 1), true);
            layer3Color = new ColorField(colorPanel, "LAYER 3: GLUTTONY", "layer3Color", new Color(1, 0.9479f, 0.8566f), true);
            layer4Color = new ColorField(colorPanel, "LAYER 4: GREED", "layer4Color", new Color(1, 1, 0.25f), true);
            layer5Color = new ColorField(colorPanel, "LAYER 5: WRATH", "layer5Color", new Color(0.251f, 0.9059f, 1), true);
            layer6Color = new ColorField(colorPanel, "LAYER 6: HERESY", "layer6Color", new Color(1, 0.2353f, 0.2353f), true);
            layer7Color = new ColorField(colorPanel, "LAYER 7: VIOLENCE", "layer7Color", new Color(0.8f, 0.8f, 0.8f), true);
            layer8Color = new ColorField(colorPanel, "LAYER 8: FRAUD", "layer8Color", new Color(0.5373f, 0.3804f, 0.2706f), true);
            encore0Color = new ColorField(colorPanel, "ENCORE 0", "encore0Color", new Color(0.6431f, 0.8745f, 0.9882f), true);
            encore1Color = new ColorField(colorPanel, "ENCORE 1", "encore1Color", new Color(0.5f, 0.5f, 0.5f), true);
            primeColor = new ColorField(colorPanel, "PRIME SANCTUMS", "primeColor", new Color(1, 0.2353f, 0.2353f), true);
            altColor = new ColorField(colorPanel, "ALTERNATE WEAPON", "altColor", new Color(1, 0.65f, 0), true);
            blueSkullColor = new ColorField(colorPanel, "BLUE SKULL", "blueSkullColor", new Color(0.251f, 0.9059f, 1), true);
            redSkullColor = new ColorField(colorPanel, "RED SKULL", "redSkullColor", new Color(1, 0.2353f, 0.2353f), true);
            switchColor = new ColorField(colorPanel, "SWITCH", "switchColor", new Color(0.25f, 0.3f, 1), true);
            pointsColor = new ColorField(colorPanel, "POINTS", "pointsColor", new Color(1, 0.65f, 0), true);
            dualwieldColor = new ColorField(colorPanel, "DUAL WIELD", "dualwieldColor", new Color(1, 1, 0.25f), true);
            doublejumpColor = new ColorField(colorPanel, "AIR JUMP", "doublejumpColor", new Color(1, 1, 0.6f), true);
            confusionColor = new ColorField(colorPanel, "CONFUSING AURA", "confusionColor", new Color(0.8242f, 1, 0.1289f), true);
            trapColor = new ColorField(colorPanel, "TRAP", "trapColor", new Color(0.7f, 0.7f, 0.7f), true);

            Done = true;
        }
    }
}
