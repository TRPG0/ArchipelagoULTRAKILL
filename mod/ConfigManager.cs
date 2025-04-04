using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Enums;
using HarmonyLib;
using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;
using Archipelago.MultiClient.Net.Packets;
using ArchipelagoULTRAKILL.Structures;
using TMPro;

namespace ArchipelagoULTRAKILL
{
    public static class ConfigManager
    {
        public static PluginConfigurator config = null;

        public static ConfigPanel playerPanel;
        public static ConfigHeader dataInfo;

        public static BoolField isConnected;
        public static StringField playerName;
        public static StringField serverAddress;
        public static StringField serverPassword;
        public static BoolField hintMode;
        public static ButtonField connectButton;
        public static ButtonField disconnectButton;
        public static ConfigHeader connectionInfo;
        public static ButtonField deathLinkOnButton;
        public static ButtonField deathLinkOffButton;
        public static StringField chat;

        public static StringField start;
        public static StringField goal;
        public static StringField goalProgress;
        public static StringField locationsChecked;
        public static EnumField<BossOptions> bossRewards;
        public static BoolField challengeRewards;
        public static BoolField pRankRewards;
        public static BoolField hankRewards;
        public static BoolField clashReward;
        public static BoolField fishRewards;
        public static BoolField cleanRewards;
        public static BoolField chessReward;
        public static BoolField rocketReward;
        public static BoolField randomizeFire2;
        public static EnumField<WeaponForm> revForm;
        public static EnumField<WeaponForm> shoForm;
        public static EnumField<WeaponForm> naiForm;
        public static BoolField randomizeSkulls;
        public static BoolField randomizeLimbo;
        public static BoolField randomizeViolence;
        public static BoolField musicRandomizer;
        public static BoolField cybergrindHints;
        public static BoolField deathLink;

        public static ConfigPanel logPanel;
        public static BoolField showLog;
        public static IntField logLines;
        public static IntField logFontSize;
        public static ButtonField logClear;

        public static ConfigPanel colorPanel;

        public static EnumField<ColorOptions> uiColorRandomizer;
        public static EnumField<ColorOptions> gunColorRandomizer;
        public static ButtonField enableCustomButton;
        public static ButtonField disableCustomButton;

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
        public static ColorField encore0Color;
        public static ColorField encore1Color;
        public static ColorField primeColor;
        public static ColorField altColor;
        public static ColorField arm0Color;
        public static ColorField arm1Color;
        public static ColorField arm2Color;
        public static ColorField blueSkullColor;
        public static ColorField redSkullColor;
        public static ColorField switchColor;
        public static ColorField pointsColor;
        public static ColorField dualwieldColor;
        public static ColorField doublejumpColor;
        public static ColorField confusionColor;
        public static ColorField trapColor;

        public static ConfigPanel linksPanel;
        public static ButtonField thunderstoreButton;
        public static ButtonField githubButton;
        public static ButtonField discordButton;
        public static ButtonField poptrackerButton;

        public static void Initialize()
        {
            if (config != null) return;

            config = PluginConfigurator.Create("Archipelago", Core.PluginGUID);

            string iconPath = Path.Combine(Core.workingDir, "icon.png");
            if (File.Exists(iconPath)) config.SetIconWithURL(iconPath);

            // root
            new ConfigHeader(config.rootPanel, "ARCHIPELAGO");
            playerPanel = new ConfigPanel(config.rootPanel, "PLAYER SETTINGS", "playerPanel");
            dataInfo = new ConfigHeader(config.rootPanel, "", 16);
            new ConfigHeader(config.rootPanel, "---");
            logPanel = new ConfigPanel(config.rootPanel, "LOG SETTINGS", "logPanel");
            colorPanel = new ConfigPanel(config.rootPanel, "COLOR SETTINGS", "colorPanel");
            linksPanel = new ConfigPanel(config.rootPanel, "LINKS", "linksPanel");

            // player settings
            isConnected = new BoolField(playerPanel, "CONNECTED TO SERVER?", "isConnected", false, false);
            isConnected.interactable = false;

            playerName = new StringField(playerPanel, "NAME", "playerName", "V1", false, true);
            serverAddress = new StringField(playerPanel, "ADDRESS", "serverAddress", "archipelago.gg", false, true);
            serverPassword = new StringField(playerPanel, "PASSWORD", "serverPassword", "", true, true);
            hintMode = new BoolField(playerPanel, "HINT MODE", "hintMode", false, false);
            new ConfigHeader(playerPanel, "Hint mode disables all randomization, and allows connecting to other games' slots to unlock hints while playing The Cyber Grind.", 12, TextAlignmentOptions.TopLeft);

            connectionInfo = new ConfigHeader(playerPanel, "", 16, TextAlignmentOptions.Top);

            connectButton = new ButtonField(playerPanel, "CONNECT", "connectButton");
            connectButton.onClick += () =>
            {
                if (Multiworld.Authenticated)
                {
                    connectionInfo.text = "Already connected to server.";
                }
                else if (SceneHelper.CurrentScene != "Main Menu")
                {
                    connectionInfo.text = "Can only connect to an Archipelago server on the main menu.";
                }
                else if ((GameProgressSaver.GetTutorial() || GameProgressSaver.GetIntro()) && !Core.DataExists() && !hintMode.value)
                {
                    connectionInfo.text = "No Archipelago data found. Start a new save file before connecting.";
                }
                else if (Core.DataExists() && hintMode.value)
                {
                    connectionInfo.text = "Can't use hint mode on a save file that already has randomizer data.";
                }
                else if (!Multiworld.Authenticated)
                {
                    Core.data.slot_name = playerName.value;
                    Core.data.host_name = serverAddress.value;
                    Core.data.password = serverPassword.value;
                    if (Core.data.password == "") Core.data.password = null;
                    if (hintMode.value) Multiworld.ConnectBK();
                    else Multiworld.Connect();
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
                        UIManager.menuIcon.GetComponent<Image>().color = Colors.Red;
                    }
                }
            };

            deathLinkOnButton = new ButtonField(playerPanel, "ENABLE DEATH LINK", "deathLinkOnButton");
            deathLinkOnButton.onClick += () =>
            {
                if (Multiworld.Authenticated)
                {
                    Core.data.deathLink = true;
                    Multiworld.EnableDeathLink();
                }
            };
            deathLinkOffButton = new ButtonField(playerPanel, "DISABLE DEATH LINK", "deathLinkOffButton");
            deathLinkOffButton.onClick += () =>
            {
                if (Multiworld.Authenticated)
                {
                    Core.data.deathLink = false;
                    Multiworld.DisableDeathLink();
                }
            };

            chat = new StringField(playerPanel, "CHAT", "chat", "", true, false) { interactable = false };
            chat.postValueChangeEvent += (string value) =>
            {
                if (Multiworld.Authenticated)
                {
                    if (value != "") Multiworld.Session.Say(value);
                    chat.value = "";
                }
            };

            new ConfigHeader(playerPanel, "-----");
            start = new StringField(playerPanel, "START LEVEL", "start", "?", false, false) { interactable = false };
            goal = new StringField(playerPanel, "GOAL LEVEL", "goal", "?", false, false) { interactable = false };
            goalProgress = new StringField(playerPanel, "LEVELS COMPLETED", "goalProgress", "?", false, false) { interactable = false };
            locationsChecked = new StringField(playerPanel, "LOCATIONS CHECKED", "locationsChecked", "?", false, false) { interactable = false };
            bossRewards = new EnumField<BossOptions>(playerPanel, "BOSS REWARDS", "bossRewards", BossOptions.Disabled, false) { interactable = false };
            bossRewards.SetEnumDisplayName(BossOptions.Disabled, "DISABLED");
            bossRewards.SetEnumDisplayName(BossOptions.Standard, "STANDARD");
            bossRewards.SetEnumDisplayName(BossOptions.Extended, "EXTENDED");
            challengeRewards = new BoolField(playerPanel, "CHALLENGE REWARDS", "challengeRewards", false, false) { interactable = false };
            pRankRewards = new BoolField(playerPanel, "P RANK REWARDS", "pRankRewards", false, false) { interactable = false };
            hankRewards = new BoolField(playerPanel, "HANK REWARDS", "hankRewards", false, false) { interactable = false };
            clashReward = new BoolField(playerPanel, "RANDOMIZE CLASH MODE", "clashReward", false, false) { interactable = false };
            fishRewards = new BoolField(playerPanel, "FISH REWARDS", "fishRewards", false, false) { interactable = false };
            cleanRewards = new BoolField(playerPanel, "CLEANING REWARDS", "cleanRewards", false, false) { interactable = false };
            chessReward = new BoolField(playerPanel, "CHESS REWARD", "chessReward", false, false) { interactable = false };
            rocketReward = new BoolField(playerPanel, "ROCKET RACE REWARD", "rocketReward", false, false) { interactable = false };
            randomizeFire2 = new BoolField(playerPanel, "RANDOMIZE SECONDARY FIRE", "randomizeFire2", false, false) { interactable = false };
            revForm = new EnumField<WeaponForm>(playerPanel, "REVOLVER FORM", "revForm", WeaponForm.Standard) { interactable = false };
            revForm.SetEnumDisplayName(WeaponForm.Standard, "STANDARD");
            revForm.SetEnumDisplayName(WeaponForm.Alternate, "ALTERNATE");
            shoForm = new EnumField<WeaponForm>(playerPanel, "SHOTGUN FORM", "shoForm", WeaponForm.Standard) { interactable = false };
            shoForm.SetEnumDisplayName(WeaponForm.Standard, "STANDARD");
            shoForm.SetEnumDisplayName(WeaponForm.Alternate, "ALTERNATE");
            naiForm = new EnumField<WeaponForm>(playerPanel, "NAILGUN FORM", "naiForm", WeaponForm.Standard) { interactable = false };
            naiForm.SetEnumDisplayName(WeaponForm.Standard, "STANDARD");
            naiForm.SetEnumDisplayName(WeaponForm.Alternate, "ALTERNATE");
            randomizeSkulls = new BoolField(playerPanel, "RANDOMIZE SKULLS", "randomizeSkulls", false, false) { interactable = false };
            randomizeLimbo = new BoolField(playerPanel, "RANDOMIZE LIMBO SWITCHES", "randomizeLimbo", false, false) { interactable = false };
            randomizeViolence = new BoolField(playerPanel, "RANDOMIZE VIOLENCE SWITCHES", "randomizeViolence", false, false) { interactable = false };
            musicRandomizer = new BoolField(playerPanel, "MUSIC RANDOMIZER", "musicRandomizer", false, false) { interactable = false };
            cybergrindHints = new BoolField(playerPanel, "UNLOCK HINTS IN CYBERGRIND", "cybergrindHints", false, false) { interactable = false };
            deathLink = new BoolField(playerPanel, "DEATH LINK", "deathLink", false, false) { interactable = false };

            // log settings
            showLog = new BoolField(logPanel, "SHOW LOG", "showLog", true, true);
            showLog.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                UIManager.log.gameObject.SetActive(e.value);
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
                UIManager.log.fontSize = e.value;
            };

            logClear = new ButtonField(logPanel, "CLEAR LOG", "logClear");
            logClear.onClick += () =>
            {
                UIManager.SetLogText("");
                Multiworld.messages.Clear();
            };

            // color settings
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
            layer7Color = new ColorField(colorPanel, "LAYER 7", "layer7Color", new Color(0.8f, 0.8f, 0.8f), true);
            encore0Color = new ColorField(colorPanel, "ENCORE 0", "encore0Color", new Color(0.6431f, 0.8745f, 0.9882f), true);
            encore1Color = new ColorField(colorPanel, "ENCORE 1", "encore1Color", new Color(0.5f, 0.5f, 0.5f), true);
            primeColor = new ColorField(colorPanel, "PRIME SANCTUMS", "primeColor", new Color(1, 0.2353f, 0.2353f), true);
            altColor = new ColorField(colorPanel, "ALTERNATE WEAPON", "altColor", new Color(1, 0.65f, 0), true);
            arm0Color = new ColorField(colorPanel, "FEEDBACKER", "arm0Color", new Color(0.251f, 0.9059f, 1), true);
            arm1Color = new ColorField(colorPanel, "KNUCKLEBLASTER", "arm1Color", new Color(1, 0.2353f, 0.2353f), true);
            arm2Color = new ColorField(colorPanel, "WHIPLASH", "arm2Color", new Color(0.2667f, 1, 0.2706f), true);
            blueSkullColor = new ColorField(colorPanel, "BLUE SKULL", "blueSkullColor", new Color(0.251f, 0.9059f, 1), true);
            redSkullColor = new ColorField(colorPanel, "RED SKULL", "redSkullColor", new Color(1, 0.2353f, 0.2353f), true);
            switchColor = new ColorField(colorPanel, "SWITCH", "switchColor", new Color(0.25f, 0.3f, 1), true);
            pointsColor = new ColorField(colorPanel, "POINTS", "pointsColor", new Color(1, 0.65f, 0), true);
            dualwieldColor = new ColorField(colorPanel, "DUAL WIELD", "dualwieldColor", new Color(1, 1, 0.25f), true);
            doublejumpColor = new ColorField(colorPanel, "AIR JUMP", "doublejumpColor", new Color(1, 1, 0.6f), true);
            confusionColor = new ColorField(colorPanel, "CONFUSING AURA", "confusionColor", new Color(0.8242f, 1, 0.1289f), true);
            trapColor = new ColorField(colorPanel, "TRAP", "trapColor", new Color(0.7f, 0.7f, 0.7f), true);

            // links
            thunderstoreButton = new ButtonField(linksPanel, "THUNDERSTORE", "thunderstoreButton");
            thunderstoreButton.onClick += () => { Application.OpenURL("https://thunderstore.io/c/ultrakill/p/TRPG/Archipelago/"); };
            githubButton = new ButtonField(linksPanel, "GITHUB", "githubButton");
            githubButton.onClick += () => { Application.OpenURL("https://github.com/TRPG0/ArchipelagoULTRAKILL"); };
            discordButton = new ButtonField(linksPanel, "AP AFTER DARK DISCORD", "discordButton");
            discordButton.onClick += () => { Application.OpenURL("https://discord.gg/Sbhy4ykUKn"); };
            poptrackerButton = new ButtonField(linksPanel, "POPTRACKER PACK", "poptrackerButton");
            poptrackerButton.onClick += () => { Application.OpenURL("https://github.com/BowserCrusher/UltrakillPoptracker/releases/"); };
        }

        public static void LoadConnectionInfo()
        {
            if (Core.data.slot_name != null) playerName.value = Core.data.slot_name;
            if (Core.data.host_name != null) serverAddress.value = Core.data.host_name;
            if (Core.data.password != null) serverPassword.value = Core.data.password;
        }

        public static void LoadStats()
        {
            dataInfo.text = Core.data.ToString();

            start.value = Core.data.start;
            goal.value = Core.data.goal;
            goalProgress.value = $"{Core.data.completedLevels.Count} / {Core.data.goalRequirement}";

            string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
            locationsChecked.value = $"{Core.data.@checked.Count} / {totalLocations}";

            bossRewards.value = Core.data.bossRewards;
            challengeRewards.value = Core.data.challengeRewards;
            pRankRewards.value = Core.data.pRankRewards;
            hankRewards.value = Core.data.hankRewards;
            clashReward.value = Core.data.clashReward;
            fishRewards.value = Core.data.fishRewards;
            cleanRewards.value = Core.data.cleanRewards;
            chessReward.value = Core.data.chessReward;
            rocketReward.value = Core.data.rocketReward;
            randomizeFire2.value = Core.data.randomizeFire2;
            revForm.value = Core.data.revForm;
            shoForm.value = Core.data.shoForm;
            naiForm.value = Core.data.naiForm;
            randomizeSkulls.value = Core.data.randomizeSkulls;
            randomizeLimbo.value = Core.data.l1switch;
            randomizeViolence.value = Core.data.l7switch;
            musicRandomizer.value = Core.data.musicRandomizer;
            cybergrindHints.value = Core.data.cybergrindHints;
            deathLink.value = Core.data.deathLink;
        }

        public static void ResetStatsDefaults()
        {
            if ((GameProgressSaver.GetTutorial() || GameProgressSaver.GetIntro()) && !Core.DataExists()) dataInfo.text = "Current slot is not randomized.";
            else dataInfo.text = Core.data.ToString();

            start.value = "?";
            goal.value = "?";
            goalProgress.value = "?";
            locationsChecked.value = "?";
            bossRewards.value = BossOptions.Disabled;
            challengeRewards.value = false;
            pRankRewards.value = false;
            hankRewards.value = false;
            clashReward.value = false;
            fishRewards.value = false;
            cleanRewards.value = false;
            chessReward.value = false;
            rocketReward.value = false;
            randomizeFire2.value = false;
            revForm.value = WeaponForm.Standard;
            shoForm.value = WeaponForm.Standard;
            naiForm.value = WeaponForm.Standard;
            randomizeSkulls.value = false;
            randomizeLimbo.value = false;
            randomizeViolence.value = false;
            musicRandomizer.value = false;
            cybergrindHints.value = false;
            deathLink.value = false;
        }
    }
}
