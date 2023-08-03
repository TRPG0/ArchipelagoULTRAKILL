﻿using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL
{
    public static class ConfigManager
    {
        public static PluginConfigurator config = null;

        public static ConfigPanel playerPanel;
        public static BoolField isConnected;
        public static StringField playerName;
        public static StringField serverAddress;
        public static StringField serverPassword;
        public static ButtonField connectButton;
        public static ButtonField disconnectButton;
        public static ConfigHeader connectionInfo;

        public static ConfigPanel logPanel;
        public static BoolField showLog;
        public static IntField logLines;
        public static IntField logFontSize;
        public static ButtonField logClear;

        public static ConfigPanel colorPanel;
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
        public static ColorField primeColor;
        public static ColorField altColor;
        public static ColorField arm0Color;
        public static ColorField arm1Color;
        public static ColorField arm2Color;
        public static ColorField blueSkullColor;
        public static ColorField redSkullColor;
        public static ColorField pointsColor;
        public static ColorField dualwieldColor;
        public static ColorField trapColor;

        public static void Initialize()
        {
            if (config != null) return;

            config = PluginConfigurator.Create("Archipelago", Core.ModGUID);

            string iconPath = Path.Combine(Core.workingDir, "icon.png");
            if (File.Exists(iconPath)) config.SetIconWithURL(iconPath);

            // root
            new ConfigHeader(config.rootPanel, "ARCHIPELAGO");
            playerPanel = new ConfigPanel(config.rootPanel, "PLAYER SETTINGS", "playerPanel");
            logPanel = new ConfigPanel(config.rootPanel, "LOG SETTINGS", "logPanel");
            colorPanel = new ConfigPanel(config.rootPanel, "COLOR SETTINGS", "colorPanel");

            // player settings
            isConnected = new BoolField(playerPanel, "CONNECTED TO SERVER?", "isConnected", false, false);
            isConnected.interactable = false;

            playerName = new StringField(playerPanel, "NAME", "playerName", "V1", false, true);
            serverAddress = new StringField(playerPanel, "ADDRESS", "serverAddress", "archipelago.gg", false, true);
            serverPassword = new StringField(playerPanel, "PASSWORD", "serverPassword", "", true, true);

            connectButton = new ButtonField(playerPanel, "CONNECT", "connectButton");
            connectButton.onClick += () =>
            {
                if (!Multiworld.Authenticated)
                {
                    Core.data.slot_name = playerName.value;
                    Core.data.host_name = serverAddress.value;
                    Core.data.password = serverPassword.value;
                    if (Core.data.password == "") Core.data.password = null;
                    Multiworld.Connect();
                }
            };

            disconnectButton = new ButtonField(playerPanel, "DISCONNECT", "disconnectButton");
            disconnectButton.onClick += () =>
            {
                if (Multiworld.Authenticated)
                {
                    Multiworld.Disconnect();
                    connectionInfo.text = "Disconnected from server.";
                    if (SceneHelper.CurrentScene == "Main Menu")
                    {
                        UIManager.menuIcon.GetComponent<Image>().color = LocationManager.colors["red"];
                    }
                }
            };

            connectionInfo = new ConfigHeader(playerPanel, "", 16, TextAnchor.UpperCenter);  

            // log settings
            showLog = new BoolField(logPanel, "SHOW LOG", "showLog", true, true);
            showLog.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                UIManager.log.SetActive(e.value);
            };

            logLines = new IntField(logPanel, "NUMBER OF MESSAGES", "logLines", 5, 1, 16, true, true);
            logLines.onValueChange += (IntField.IntValueChangeEvent e) =>
            {
                UIManager.lines = e.value;
                while (Multiworld.messages.Count > e.value) Multiworld.messages.RemoveAt(0);
            };

            logFontSize = new IntField(logPanel, "FONT SIZE", "logFontSize", 16, 1, 32, true, true);
            logFontSize.onValueChange += (IntField.IntValueChangeEvent e) =>
            {
                UIManager.log.GetComponent<Text>().fontSize = e.value;
            };

            logClear = new ButtonField(logPanel, "CLEAR LOG", "logClear");
            logClear.onClick += () =>
            {
                UIManager.log.GetComponent<Text>().text = "";
                Multiworld.messages.Clear();
            };

            // color settings
            new ConfigHeader(colorPanel, "ARCHIPELAGO COLORS");
            APPlayerSelf = new ColorField(colorPanel, "PLAYER (YOU)", "APPlayerSelf", new Color(0.93f, 0, 0.93f), true);
            APPlayerOther = new ColorField(colorPanel, "PLAYER (OTHERS)", "APPlayerOther", new Color(0.98f, 0.98f, 0.82f), true);
            APItemFiller = new ColorField(colorPanel, "ITEM (FILLER)", "APItemFiller", new Color(0, 0.93f, 0.93f), true);
            APItemNeverExclude = new ColorField(colorPanel, "ITEM (USEFUL)", "APItemNeverExclude", new Color(0.43f, 0.55f, 0.91f), true);
            APItemAdvancement = new ColorField(colorPanel, "ITEM (PROGRESSION)", "APItemAdvancement", new Color(0.69f, 0.6f, 0.94f), true);
            APItemTrap = new ColorField(colorPanel, "ITEM (TRAP)", "APItemTrap", new Color(0.98f, 0.5f, 0.45f), true);
            APLocation = new ColorField(colorPanel, "LOCATION", "APLocation", new Color(0, 1, 0.5f), true);

            new ConfigHeader(colorPanel, "POPUP COLORS");
            layer0Color = new ColorField(colorPanel, "LAYER 0", "layer0Color", new Color(1, 0.5f, 0.25f), true);
            layer1Color = new ColorField(colorPanel, "LAYER 1", "layer1Color", new Color(0.2667f, 1, 0.2706f), true);
            layer2Color = new ColorField(colorPanel, "LAYER 2", "layer2Color", new Color(0.765f, 0.25f, 1), true);
            layer3Color = new ColorField(colorPanel, "LAYER 3", "layer3Color", new Color(1, 0.9479f, 0.8566f), true);
            layer4Color = new ColorField(colorPanel, "LAYER 4", "layer4Color", new Color(1, 1, 0.25f), true);
            layer5Color = new ColorField(colorPanel, "LAYER 5", "layer5Color", new Color(0.251f, 0.9059f, 1), true);
            layer6Color = new ColorField(colorPanel, "LAYER 6", "layer6Color", new Color(1, 0.2353f, 0.2353f), true);
            layer6Color = new ColorField(colorPanel, "PRIME SANCTUMS", "primeColor", new Color(1, 0.2353f, 0.2353f), true);
            altColor = new ColorField(colorPanel, "ALTERNATE WEAPON", "altColor", new Color(1, 0.65f, 0), true);
            arm0Color = new ColorField(colorPanel, "FEEDBACKER", "arm0Color", new Color(0.251f, 0.9059f, 1), true);
            arm1Color = new ColorField(colorPanel, "KNUCKLEBLASTER", "arm1Color", new Color(1, 0.2353f, 0.2353f), true);
            arm2Color = new ColorField(colorPanel, "WHIPLASH", "arm2Color", new Color(0.2667f, 1, 0.2706f), true);
            blueSkullColor = new ColorField(colorPanel, "BLUE SKULL", "blueSkullColor", new Color(0.251f, 0.9059f, 1), true);
            redSkullColor = new ColorField(colorPanel, "RED SKULL", "redSkullColor", new Color(1, 0.2353f, 0.2353f), true);
            pointsColor = new ColorField(colorPanel, "POINTS", "pointsColor", new Color(1, 0.65f, 0), true);
            dualwieldColor = new ColorField(colorPanel, "DUAL WIELD", "dualwieldColor", new Color(1, 1, 0.25f), true);
            trapColor = new ColorField(colorPanel, "TRAP", "trapColor", new Color(0.7f, 0.7f, 0.7f), true);
        }

        public static void LoadConnectionInfo()
        {
            if (Core.data.slot_name != null) playerName.value = Core.data.slot_name;
            if (Core.data.host_name != null) serverAddress.value = Core.data.host_name;
            if (Core.data.password != null) serverPassword.value = Core.data.password;
        }
    }
}