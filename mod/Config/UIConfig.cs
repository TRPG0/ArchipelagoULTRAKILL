using ArchipelagoULTRAKILL.Structures;
using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Config
{
    public static class UIConfig
    {
        public static bool Done { get; private set; } = false;

        public static ConfigPanel uiPanel;
        public static BoolField showRecentLocations;
        public static BoolField showRecentItems;
        public static BoolField showLog;
        public static EnumField<LogFont> logFont;
        public static IntField logLines;
        public static IntField logFontSize;
        public static IntField logOpacity;
        public static ButtonField logClear;

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            uiPanel = new ConfigPanel(config.rootPanel, "UI SETTINGS", "uiPanel");

            new ConfigHeader(uiPanel, "PAUSE MENU");
            showRecentLocations = new BoolField(uiPanel, "SHOW RECENT LOCATIONS", "showRecentLocations", true);
            showRecentLocations.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                UIManager.recentLocationContainer?.SetActive(e.value);
            };

            showRecentItems = new BoolField(uiPanel, "SHOW RECENT ITEMS", "showRecentItems", true);
            showRecentItems.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                UIManager.recentItemContainer?.SetActive(e.value);
            };

            new ConfigHeader(uiPanel, "LOG");
            showLog = new BoolField(uiPanel, "SHOW LOG", "showLog", true, true);
            showLog.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                UIManager.log.gameObject.SetActive(e.value);
            };

            logFont = new EnumField<LogFont>(uiPanel, "FONT", "logFont", LogFont.Pixel1);
            logFont.SetEnumDisplayName(LogFont.Pixel1, "fs Tahoma 8px");
            logFont.SetEnumDisplayName(LogFont.Pixel2, "VCR OSD MONO");
            logFont.SetEnumDisplayName(LogFont.SansSerif, "Roboto");
            logFont.onValueChange += (EnumField<LogFont>.EnumValueChangeEvent e) =>
            {
                UIManager.SetLogFont(e.value, true);
            };

            logLines = new IntField(uiPanel, "NUMBER OF MESSAGES", "logLines", 5, 1, 16, true, true);
            logLines.onValueChange += (IntField.IntValueChangeEvent e) =>
            {
                UIManager.lines = e.value;
                while (Multiworld.messages.Count > e.value) Multiworld.messages.RemoveAt(0);
            };

            logFontSize = new IntField(uiPanel, "FONT SIZE", "logFontSize", 20, 1, 32, true, true);
            logFontSize.onValueChange += (IntField.IntValueChangeEvent e) =>
            {
                UIManager.log.fontSize = e.value;
            };

            logOpacity = new IntField(uiPanel, "OPACITY", "logOpacity", 100, 0, 100, true, true);
            logOpacity.onValueChange += (IntField.IntValueChangeEvent e) =>
            {
                UIManager.log.color = new Color(1, 1, 1, (float)e.value / 100);
            };

            logClear = new ButtonField(uiPanel, "CLEAR LOG", "logClear");
            logClear.onClick += () =>
            {
                UIManager.SetLogText("");
                Multiworld.messages.Clear();
            };

            Done = true;
        }
    }
}
