using ArchipelagoULTRAKILL.Structures;
using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using TMPro;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Config
{
    public static class PlayerConfig
    {
        public static bool Done { get; private set; } = false;

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
        public static StringField chat;

        public static StringField start;
        public static StringField goal;
        public static StringField goalProgress;
        public static BoolField perfectGoal;
        public static EnumField<SecretUnlockType> secretExitUnlock;
        public static EnumField<SecretExitType> secretExitComplete;
        public static StringField locationsChecked;
        public static EnumField<EnemyOptions> enemyRewards;
        public static BoolField challengeRewards;
        public static BoolField pRankRewards;
        public static BoolField hankRewards;
        public static BoolField clashReward;
        public static BoolField fishRewards;
        public static BoolField cleanRewards;
        public static BoolField chessReward;
        public static BoolField rocketReward;
        public static EnumField<Fire2Options> randomizeFire2;
        public static EnumField<WeaponForm> revForm;
        public static EnumField<WeaponForm> shoForm;
        public static EnumField<WeaponForm> naiForm;
        public static BoolField randomizeSkulls;
        public static BoolField randomizeLimbo;
        public static BoolField randomizeViolence;
        public static BoolField cybergrindHints;

        public static void Initialize(PluginConfigurator config)
        {
            if (config == null || Done) return;

            playerPanel = new ConfigPanel(config.rootPanel, "PLAYER SETTINGS", "playerPanel");
            dataInfo = new ConfigHeader(config.rootPanel, "", 16);

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
                    else Core.mw.StartCoroutine("Connect");
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
            perfectGoal = new BoolField(playerPanel, "PERFECT GOAL", "perfectGoal", false) { interactable = false };
            secretExitUnlock = new EnumField<SecretUnlockType>(playerPanel, "SECRET MISSION UNLOCK TYPE", "secretExitUnlock", SecretUnlockType.SecretExits, false) { interactable = false };
            secretExitUnlock.SetEnumDisplayName(SecretUnlockType.SecretExits, "SECRET EXITS");
            secretExitUnlock.SetEnumDisplayName(SecretUnlockType.Items, "ITEMS");
            secretExitComplete = new EnumField<SecretExitType>(playerPanel, "SECRET EXIT BEHAVIOR", "secretExitComplete", SecretExitType.Standard, false) { interactable = false };
            secretExitComplete.SetEnumDisplayName(SecretExitType.Standard, "STANDARD");
            secretExitComplete.SetEnumDisplayName(SecretExitType.AddRewards, "ADD REWARDS");
            locationsChecked = new StringField(playerPanel, "LOCATIONS CHECKED", "locationsChecked", "?", false, false) { interactable = false };
            enemyRewards = new EnumField<EnemyOptions>(playerPanel, "ENEMY REWARDS", "enemyRewards", EnemyOptions.Disabled, false) { interactable = false };
            enemyRewards.SetEnumDisplayName(EnemyOptions.Disabled, "DISABLED");
            enemyRewards.SetEnumDisplayName(EnemyOptions.Bosses, "BOSSES");
            enemyRewards.SetEnumDisplayName(EnemyOptions.Extra, "EXTRA");
            enemyRewards.SetEnumDisplayName(EnemyOptions.All, "All");
            challengeRewards = new BoolField(playerPanel, "CHALLENGE REWARDS", "challengeRewards", false, false) { interactable = false };
            pRankRewards = new BoolField(playerPanel, "P RANK REWARDS", "pRankRewards", false, false) { interactable = false };
            hankRewards = new BoolField(playerPanel, "HANK REWARDS", "hankRewards", false, false) { interactable = false };
            clashReward = new BoolField(playerPanel, "RANDOMIZE CLASH MODE", "clashReward", false, false) { interactable = false };
            fishRewards = new BoolField(playerPanel, "FISH REWARDS", "fishRewards", false, false) { interactable = false };
            cleanRewards = new BoolField(playerPanel, "CLEANING REWARDS", "cleanRewards", false, false) { interactable = false };
            chessReward = new BoolField(playerPanel, "CHESS REWARD", "chessReward", false, false) { interactable = false };
            rocketReward = new BoolField(playerPanel, "ROCKET RACE REWARD", "rocketReward", false, false) { interactable = false };
            randomizeFire2 = new EnumField<Fire2Options>(playerPanel, "RANDOMIZE SECONDARY FIRE", "randomizeFire2", Fire2Options.Disabled) { interactable = false };
            randomizeFire2.SetEnumDisplayName(Fire2Options.Disabled, "DISABLED");
            randomizeFire2.SetEnumDisplayName(Fire2Options.Split, "SPLIT");
            randomizeFire2.SetEnumDisplayName(Fire2Options.Progressive, "PROGRESSIVE");
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
            cybergrindHints = new BoolField(playerPanel, "UNLOCK HINTS IN CYBERGRIND", "cybergrindHints", false, false) { interactable = false };

            Done = true;
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
            perfectGoal.value = Core.data.perfectGoal;

            if (Core.data.secretExitUnlock) secretExitUnlock.value = SecretUnlockType.SecretExits;
            else secretExitUnlock.value = SecretUnlockType.Items;

            if (Core.data.secretExitComplete) secretExitComplete.value = SecretExitType.Standard;
            else secretExitComplete.value = SecretExitType.AddRewards;

            string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
            locationsChecked.value = $"{Core.data.@checked.Count} / {totalLocations}";

            enemyRewards.value = Core.data.enemyRewards;
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
            cybergrindHints.value = Core.data.cybergrindHints;
        }

        public static void ResetStatsDefaults()
        {
            if ((GameProgressSaver.GetTutorial() || GameProgressSaver.GetIntro()) && !Core.DataExists()) dataInfo.text = "Current slot is not randomized.";
            else dataInfo.text = Core.data.ToString();

            start.value = "?";
            goal.value = "?";
            goalProgress.value = "?";
            perfectGoal.value = false;
            secretExitUnlock.value = SecretUnlockType.SecretExits;
            secretExitComplete.value = SecretExitType.Standard;
            locationsChecked.value = "?";
            enemyRewards.value = EnemyOptions.Disabled;
            challengeRewards.value = false;
            pRankRewards.value = false;
            hankRewards.value = false;
            clashReward.value = false;
            fishRewards.value = false;
            cleanRewards.value = false;
            chessReward.value = false;
            rocketReward.value = false;
            randomizeFire2.value = Fire2Options.Disabled;
            revForm.value = WeaponForm.Standard;
            shoForm.value = WeaponForm.Standard;
            naiForm.value = WeaponForm.Standard;
            randomizeSkulls.value = false;
            randomizeLimbo.value = false;
            randomizeViolence.value = false;
            cybergrindHints.value = false;
        }
    }
}
