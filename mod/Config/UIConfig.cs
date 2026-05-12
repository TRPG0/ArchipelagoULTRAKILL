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
        public static BoolField showRecentLocationsChapter;
        public static BoolField showRecentItemsChapter;
        public static BoolField showRecentLocationsPause;
        public static BoolField showRecentItemsPause;
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

            showRecentLocationsChapter = new BoolField(uiPanel, "RECENT LOCATIONS IN ACT SELECT", "showRecentLocationsChapter", true);
            showRecentLocationsChapter.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                if (SceneHelper.CurrentScene == "Main Menu")
                {
                    if (UIManager.recentLocationContainer && UIManager.recentItemContainer)
                    {
                        UIManager.recentLocationContainer.SetActive(e.value);

                        if (UIManager.recentLocationContainer.activeSelf) UIManager.recentItemContainer.transform.localPosition = new Vector3(0, -190, 0);
                        else UIManager.recentItemContainer.transform.localPosition = Vector3.zero;
                    }
                }
            };

            showRecentItemsChapter = new BoolField(uiPanel, "RECENT ITEMS IN ACT SELECT", "showRecentItemsChapter", true);
            showRecentItemsChapter.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                if (SceneHelper.CurrentScene == "Main Menu")
                {
                    if (UIManager.recentLocationContainer && UIManager.recentItemContainer)
                    {
                        UIManager.recentItemContainer.SetActive(e.value);

                        if (UIManager.recentItemContainer.activeSelf) UIManager.recentLocationContainer.transform.localPosition = new Vector3(0, 190, 0);
                        else UIManager.recentLocationContainer.transform.localPosition = Vector3.zero;
                    }
                }
            };
            
            showRecentLocationsPause = new BoolField(uiPanel, "RECENT LOCATIONS IN PAUSE MENU", "showRecentLocationsPause", true);
            showRecentLocationsPause.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                if (SceneHelper.CurrentScene != "Main Menu") UIManager.recentLocationContainer?.SetActive(e.value);
            };

            showRecentItemsPause = new BoolField(uiPanel, "RECENT ITEMS IN PAUSE MENU", "showRecentItemsPause", true);
            showRecentItemsPause.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                if (SceneHelper.CurrentScene != "Main Menu") UIManager.recentItemContainer?.SetActive(e.value);
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
