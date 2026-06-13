using ArchipelagoULTRAKILL.Structures;
using ArchipelagoULTRAKILL.Components;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using TMPro;
using UnityEngine.ResourceManagement.AsyncOperations;
using ArchipelagoULTRAKILL.Music;
using ArchipelagoULTRAKILL.Config;
using System.Linq;

namespace ArchipelagoULTRAKILL
{
    public class UIManager : MonoBehaviour
    {
        public static AssetBundle bundle = AssetBundle.LoadFromMemory(Properties.Resources.trpg_archipelago);

        public static Sprite menuSprite1;
        public static Sprite menuSprite2;
        public static Texture2D killsTexture;

        public static TMP_FontAsset fontMain;
        public static TMP_FontAsset fontSecondary;
        public static TextMeshProUGUI log;
        public static int lines = 5;

        public static GameObject canvas;
        public static GameObject chapterSelect;
        public static TextMeshProUGUI actStats;
        public static Dictionary<string, GameObject> chapters = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> layers = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> levels = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> secrets = new Dictionary<string, GameObject>();

        public static GameObject menuText;
        public static GameObject menuIcon;
        public static TextMeshProUGUI goalCount;

        public static GameObject pauseContainer;
        public static bool createdMenuIcons = false;
        public static bool createdSkullIcons = false;
        public static bool createdSwitchIcons = false;

        public static GameObject recentItemContainer;
        public static GameObject recentLocationContainer;

        public static GameObject hud;
        public static GameObject popupCanvas;
        public static TextMeshProUGUI popupText;
        public static GameObject popupImage;
        public static bool displayingMessage = false;

        public DeathLinkMessage deathLinkMessage = null;

        public void Update()
        {
            if (LocationManager.itemQueue.Count > 0 && Multiworld.CanGetItems)
            {
                QueuedItem qItem = LocationManager.itemQueue[0];
                LocationManager.GetUKItem(qItem.item, qItem.sendingPlayer, qItem.silent);
                LocationManager.itemQueue.RemoveAt(0);
            }
        }

        public void CreateLogObject()
        {
            StartCoroutine(VersionChecker.CheckVersion());

            lines = UIConfig.logLines.value;

            GameConsole.Console.Instance.RegisterCommand(new Commands.Connect());
            GameConsole.Console.Instance.RegisterCommand(new Commands.Disconnect());
            GameConsole.Console.Instance.RegisterCommand(new Commands.Say());

            GameObject go = new GameObject();
            go.name = "Log";
            go.transform.parent = Core.obj.transform;
            go.transform.localPosition = new Vector3(0, 0, 0);

            Core.obj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            Core.obj.GetComponent<Canvas>().sortingOrder = 256;

            log = go.AddComponent<TextMeshProUGUI>();
            log.fontSize = UIConfig.logFontSize.value;
            log.alignment = TextAlignmentOptions.BottomGeoAligned;
            log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height - 10);
            SetLogFont(UIConfig.logFont.value);
        }

        public static void SetLogFont(LogFont font, bool reset = false)
        {
            TMP_FontAsset fontAsset = null;
            Material fontMat = null;

            if (font == LogFont.Pixel1)
            {
                fontAsset = bundle.LoadAsset<TMP_FontAsset>("assets/fs-tahoma-8px sdf.asset");
                fontMat = bundle.LoadAsset<Material>("assets/fs-tahoma-8px_underlay.mat");
                if (reset)
                {
                    UIConfig.logFontSize.value = 20;
                    log.fontSize = 20;
                }
            }
            else if (font == LogFont.Pixel2)
            {
                fontAsset = bundle.LoadAsset<TMP_FontAsset>("assets/vcr_osd_mono_1.asset");
                fontMat = bundle.LoadAsset<Material>("assets/vcr_osd_mono_underlay.mat");
                if (reset)
                {
                    UIConfig.logFontSize.value = 16;
                    log.fontSize = 16;
                }
            }
            else if (font == LogFont.SansSerif)
            {
                fontAsset = bundle.LoadAsset<TMP_FontAsset>("assets/roboto-regular sdf.asset");
                fontMat = bundle.LoadAsset<Material>("assets/roboto-regular_underlay.mat");
                if (reset)
                {
                    UIConfig.logFontSize.value = 14;
                    log.fontSize = 14;
                }
            }

            if (log == null || fontAsset == null || fontMat == null) return;

            log.font = fontAsset;
            log.fontMaterial = fontMat;
        }

        public static void SetLogText(string text)
        {
            if (log != null) log.text = text;
        }

        public static void AdjustLogBounds()
        {
            if (PrefsManager.Instance.GetInt("hudType") >= 2 && Core.IsInLevel)
            {
                log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, (float)Math.Round(Screen.height * 0.77f));
            }
            else
            {
                log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height - 10);
            }
        }

        public static void FindMenuObjects()
        {
            canvas = GameObject.Find("/Canvas");

            foreach (ChapterSelectButton component in canvas.GetComponentsInChildren<ChapterSelectButton>(true))
            {
                //logger.LogInfo(component.gameObject.name);
                switch (component.gameObject.name)
                {
                    case "Prelude":
                        chapters["prelude"] = component.gameObject;
                        break;
                    case "Act I":
                        chapters["act1"] = component.gameObject;
                        break;
                    case "Act II":
                        chapters["act2"] = component.gameObject;
                        break;
                    case "Act III":
                        chapters["act3"] = component.gameObject;
                        break;
                    case "Prime":
                        chapters["prime"] = component.gameObject;
                        break;
                    default:
                        break;
                }
            }

            chapterSelect = chapters["prelude"].gameObject.transform.parent.gameObject;
            chapterSelect.AddComponent<ChapterSelectState>();

            actStats = GameObject.Instantiate(chapterSelect.transform.Find("Prelude").Find("Name"), chapterSelect.transform.parent).GetComponent<TextMeshProUGUI>();
            Vector3 rankPos = chapterSelect.transform.Find("Prelude").Find("RankPanel").transform.position;
            actStats.transform.position = new Vector3(rankPos.x + 97, rankPos.y, rankPos.z);
            actStats.overflowMode = TextOverflowModes.Overflow;
            actStats.alignment = TextAlignmentOptions.TopLeft;
            actStats.lineSpacing = 1.2f;
            actStats.gameObject.SetActive(false);

            chapterSelect.transform.Find("Prelude").gameObject.AddComponent<ActStats>().Init(1, 5);
            chapterSelect.transform.Find("Act I").gameObject.AddComponent<ActStats>().Init(6, 15);
            chapterSelect.transform.Find("Act II").gameObject.AddComponent<ActStats>().Init(16, 25);
            chapterSelect.transform.Find("Act III").gameObject.AddComponent<ActStats>().Init(26, 33);
            chapterSelect.transform.Find("Encore").gameObject.AddComponent<ActStats>().Init(100, 101, true);
            chapterSelect.transform.Find("Prime").gameObject.AddComponent<ActStats>().Init(666, 667, true);

            Button cyberGrindButton = chapterSelect.transform.Find("The Cyber Grind").GetComponent<Button>();
            cyberGrindButton.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            cyberGrindButton.onClick.AddListener(delegate { SceneHelper.LoadScene("Endless"); });

            Button sandboxButton = chapterSelect.transform.Find("Sandbox").GetComponent<Button>();
            sandboxButton.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            sandboxButton.onClick.AddListener(delegate { SceneHelper.LoadScene("uk_construct"); });

            foreach (LayerSelect component in canvas.GetComponentsInChildren<LayerSelect>(true))
            {
                switch (component.gameObject.name)
                {
                    case "Overture":
                        layers["layer0"] = component.gameObject;
                        break;
                    case "Layer 1 Limbo":
                        if (component.gameObject.transform.parent.transform.parent.transform.parent.name == "Level Select (Prelude)") break;
                        else
                        {
                            layers["layer1"] = component.gameObject;
                            break;
                        }
                    case "Layer 2 Lust":
                        layers["layer2"] = component.gameObject;
                        break;
                    case "Layer 3 Gluttony":
                        layers["layer3"] = component.gameObject;
                        break;
                    case "Layer 4 Greed":
                        layers["layer4"] = component.gameObject;
                        break;
                    case "Layer 5 Wrath":
                        layers["layer5"] = component.gameObject;
                        break;
                    case "Layer 6 Heresy":
                        layers["layer6"] = component.gameObject;
                        break;
                    case "Layer 7 Violence":
                        layers["layer7"] = component.gameObject;
                        break;
                    case "Prime Sanctums":
                        layers["layerP"] = component.gameObject;
                        break;
                    default:
                        break;
                }
            }

            foreach (LevelSelectPanel component in canvas.GetComponentsInChildren<LevelSelectPanel>(true))
            {
                //logger.LogInfo(component.gameObject.transform.parent.name + ", " + component.gameObject.name);
                switch (component.gameObject.name)
                {
                    case "0-1 Panel":
                        levels["0-1"] = component.gameObject;
                        break;
                    case "0-2 Panel":
                        levels["0-2"] = component.gameObject;
                        break;
                    case "0-3 Panel":
                        levels["0-3"] = component.gameObject;
                        break;
                    case "0-4 Panel":
                        levels["0-4"] = component.gameObject;
                        break;
                    case "0-5 Panel":
                        levels["0-5"] = component.gameObject;
                        break;
                    case "1-1 Panel":
                        switch (component.gameObject.transform.parent.parent.name)
                        {
                            case "Layer 1 Limbo":
                                if (!levels.ContainsKey("1-1")) levels["1-1"] = component.gameObject;
                                break;
                            case "Layer 6 Heresy":
                                levels["6-1"] = component.gameObject;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "1-2 Panel":
                        switch (component.gameObject.transform.parent.parent.name)
                        {
                            case "Layer 1 Limbo":
                                if (!levels.ContainsKey("1-2")) levels["1-2"] = component.gameObject;
                                break;
                            case "Layer 6 Heresy":
                                levels["6-2"] = component.gameObject;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "1-3 Panel":
                        levels["1-3"] = component.gameObject;
                        break;
                    case "1-4 Panel":
                        levels["1-4"] = component.gameObject;
                        break;
                    case "2-1 Panel":
                        levels["2-1"] = component.gameObject;
                        break;
                    case "2-2 Panel":
                        levels["2-2"] = component.gameObject;
                        break;
                    case "2-3 Panel":
                        levels["2-3"] = component.gameObject;
                        break;
                    case "2-4 Panel":
                        levels["2-4"] = component.gameObject;
                        break;
                    case "3-1 Panel":
                        levels["3-1"] = component.gameObject;
                        break;
                    case "3-2 Panel":
                        levels["3-2"] = component.gameObject;
                        break;
                    case "4-1 Panel":
                        levels["4-1"] = component.gameObject;
                        break;
                    case "4-2 Panel":
                        levels["4-2"] = component.gameObject;
                        break;
                    case "4-3 Panel":
                        levels["4-3"] = component.gameObject;
                        break;
                    case "4-4 Panel":
                        levels["4-4"] = component.gameObject;
                        break;
                    case "5-1 Panel":
                        levels["5-1"] = component.gameObject;
                        break;
                    case "5-2 Panel":
                        levels["5-2"] = component.gameObject;
                        break;
                    case "5-3 Panel":
                        levels["5-3"] = component.gameObject;
                        break;
                    case "5-4 Panel":
                        levels["5-4"] = component.gameObject;
                        break;
                    case "7-1 Panel":
                        levels["7-1"] = component.gameObject;
                        break;
                    case "7-2 Panel":
                        levels["7-2"] = component.gameObject;
                        break;
                    case "7-3 Panel":
                        levels["7-3"] = component.gameObject;
                        break;
                    case "7-4 Panel":
                        levels["7-4"] = component.gameObject;
                        break;
                    case "8-1 Panel":
                        levels["8-1"] = component.gameObject;
                        break;
                    case "8-2 Panel":
                        levels["8-2"] = component.gameObject;
                        break;
                    case "8-3 Panel":
                        levels["8-3"] = component.gameObject;
                        break;
                    case "8-4 Panel":
                        levels["8-4"] = component.gameObject;
                        break;
                    case "P-1 Panel":
                        levels["P-1"] = component.gameObject;
                        break;
                    case "P-2 Panel":
                        levels["P-2"] = component.gameObject;
                        break;
                    case "P-3 Panel":
                        levels["P-3"] = component.gameObject;
                        break;
                    case "0-E":
                        levels["0-E"] = component.gameObject;
                        break;
                    case "1-E":
                        levels["1-E"] = component.gameObject;
                        break;
                    default:
                        break;
                }
            }

            foreach (SecretMissionPanel component in canvas.GetComponentsInChildren<SecretMissionPanel>(true))
            {
                //logger.LogInfo(component.gameObject.transform.parent.name + ", " + component.gameObject.name);
                switch (component.gameObject.transform.parent.parent.name)
                {
                    case "Overture":
                        secrets["0-S"] = component.gameObject;
                        break;
                    case "Layer 1 Limbo":
                        if (!secrets.ContainsKey("1-S")) secrets["1-S"] = component.gameObject;
                        break;
                    case "Layer 2 Lust":
                        secrets["2-S"] = component.gameObject;
                        break;
                    case "Layer 4 Greed":
                        secrets["4-S"] = component.gameObject;
                        break;
                    case "Layer 5 Wrath":
                        secrets["5-S"] = component.gameObject;
                        break;
                    case "Layer 7 Violence":
                        secrets["7-S"] = component.gameObject;
                        break;
                    default:
                        break;
                }
            }

            if (MusicRandomizer.Instance)
            {
                Button l1Button = Core.FindGameObjectFromPathInScene("Canvas/Level Select (Prelude)/FullIntroPopup/Panel/Button").GetComponent<Button>();
                l1Button.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                l1Button.onClick.AddListener(delegate { MusicRandomizer.Instance.CheckIfPreloadNeededBeforeLevel("Level 0-1", 1); });

                foreach (KeyValuePair<string, GameObject> kvp in levels)
                {
                    if (kvp.Key == "0-1") continue;
                    Button button = kvp.Value.GetComponent<Button>();
                    LevelSelectPanel levelSelectPanel = kvp.Value.GetComponent<LevelSelectPanel>();
                    button.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    button.onClick.AddListener(delegate { MusicRandomizer.Instance.CheckIfPreloadNeededBeforeLevel($"Level {kvp.Key}", levelSelectPanel.levelNumber); });
                }
            }
        }
        
        public static void CreateMenuUI()
        {
            string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();

            canvas.transform.Find("Main Menu (1)/Border").gameObject.AddComponent<MenuBorderCheck>();
            menuText = canvas.transform.Find("Main Menu (1)/LeftSide/Text (3)").gameObject;
            fontMain = menuText.GetComponent<TextMeshProUGUI>().font;
            string line2 = "NO DATA.";
            if (Core.DataExists()) line2 = $"{Core.data.slot_name.ToUpper()} - {Core.data.@checked.Count} / {totalLocations}";
            else if (Multiworld.HintMode) line2 = $"{Core.data.slot_name.ToUpper()} - HINT MODE ({Multiworld.Session.ConnectionInfo.Game.ToUpper()})";
            string full = $" - ARCHIPELAGO {Core.PluginVersion} - SLOT {GameProgressSaver.currentSlot + 1}\n{line2}";
            menuText.GetComponent<TextMeshProUGUI>().text = menuText.GetComponent<TextMeshProUGUI>().text + full;
            menuText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = menuText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text + full;

            GameObject seasonalStuff = GameObject.Find("Seasonal Stuff");
            ObjectActivator christmasObject = seasonalStuff.transform.Find("Christmas").GetComponent<ObjectActivator>();
            christmasObject.events.toDisActivateObjects = new GameObject[1] { christmasObject.events.toDisActivateObjects[0] };
            christmasObject.events.toActivateObjects[0].GetComponent<TextMeshProUGUI>().text = new String(' ', line2.Length + 1) + "MERRY CHRISTMAS";
            christmasObject.events.toActivateObjects[0].transform.Translate(new Vector3(0, -2, 0));
            ObjectActivator halloweenObject = seasonalStuff.transform.Find("Halloween").GetComponent<ObjectActivator>();
            halloweenObject.events.toDisActivateObjects = new GameObject[1] { halloweenObject.events.toDisActivateObjects[0] };
            halloweenObject.events.toActivateObjects[0].GetComponent<TextMeshProUGUI>().text = new String(' ', line2.Length + 1) + "HAPPY HALLOWEEN";
            halloweenObject.events.toActivateObjects[0].transform.Translate(new Vector3(0, -2, 0));
            ObjectActivator easterObject = seasonalStuff.transform.Find("Easter").GetComponent<ObjectActivator>();
            easterObject.events.toDisActivateObjects = new GameObject[1] { easterObject.events.toDisActivateObjects[0] };
            easterObject.events.toActivateObjects[0].GetComponent<TextMeshProUGUI>().text = new String(' ', line2.Length + 1) + "HAPPY EASTER";
            easterObject.events.toActivateObjects[0].transform.Translate(new Vector3(0, -2, 0));

            menuIcon = new GameObject();
            menuIcon.gameObject.name = "Archipelago Logo";
            menuIcon.transform.SetParent(menuText.transform.parent.gameObject.transform);
            menuIcon.AddComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/newlogo1.png");
            menuIcon.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            menuIcon.transform.localPosition = new Vector3(637.5f, -48, 0);
            menuIcon.transform.localScale = new Vector3(0.96f, 0.96f, 0.96f);

            if (Core.DataExists() && Core.data.completedLevels.Count < Core.data.goalRequirement) CreateGoalCounter();
        }

        public static void CreateGoalCounter()
        {
            if (goalCount != null) return;
            if (Core.data.goal.Contains("-S"))
            {
                Transform rankText = secrets[Core.data.goal].transform.parent.Find("RankPanel").Find("RankText");
                goalCount = GameObject.Instantiate(rankText, rankText.parent).GetComponent<TextMeshProUGUI>();
                goalCount.transform.localPosition = new Vector3(-30, -30, 0);
                goalCount.text = (Core.data.goalRequirement - Core.data.completedLevels.Count).ToString();
                if (goalCount.text.Length > 1) goalCount.fontSize = 40;
            }
            else
            {
                GameObject go = new GameObject();
                go.transform.SetParent(levels[Core.data.goal].transform);
                if (Core.data.goal.Contains("P")) go.transform.localPosition = new Vector3(0, 20, 0);
                else if (LeaderboardController.ShowLevelLeaderboards) go.transform.localPosition = new Vector3(0, 90, 0);
                else go.transform.localPosition = new Vector3(0, 22.5f, 0);
                go.layer = 5;
                goalCount = go.AddComponent<TextMeshProUGUI>();
                goalCount.text = (Core.data.goalRequirement - Core.data.completedLevels.Count).ToString();
                goalCount.font = fontMain;
                goalCount.fontSize = 100;
                goalCount.alignment = TextAlignmentOptions.Center;
                goalCount.overflowMode = TextOverflowModes.Overflow;

                levels[Core.data.goal].transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
            }
            
            if (!Core.DataExists()) goalCount.gameObject.SetActive(false);
        }

        public static void UpdateLevels()
        {
            AsyncOperationHandle<Sprite> asyncHandle = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/UI/Level Thumbnails/Locked.png");
            Sprite locked = asyncHandle.WaitForCompletion();
            foreach (string level in Core.AllLevels)
            {
                if ((!Core.data.unlockedLevels.Contains(level) && level != Core.data.goal) || (level == Core.data.goal && Core.data.completedLevels.Count < Core.data.goalRequirement))
                {
                    foreach (Image component in levels[level].GetComponentsInChildren<Image>())
                    {
                        if (component.gameObject.name == "Image") component.sprite = locked;
                    }
                    levels[level].GetComponent<Button>().interactable = false;
                }
                else if (Core.data.unlockedLevels.Contains(level))
                {
                    foreach (Image component in levels[level].GetComponentsInChildren<Image>())
                    {
                        if (component.gameObject.name == "Image" && component.sprite.Equals(locked)) component.sprite = Traverse.Create(component.transform.parent.GetComponent<LevelSelectPanel>()).Field<Sprite>("origSprite").Value;
                    }
                    levels[level].GetComponent<Button>().interactable = true;
                }
            }
            Addressables.Release(asyncHandle);
        }

        public static void CreateMenuIcons()
        {
            foreach (LevelInfo info in Core.levelInfos)
            {
                Vector3 containerPos = new Vector3(90, -2, 0);
                Vector3 containerScale = new Vector3(0.225f, 0.225f, 1);
                Vector3 enemyPos = new Vector3(-90, -136, 0);
                Vector3 enemyScale = new Vector3(0.5f, 0.5f, 1);

                if (info.Id >= 666)
                {
                    containerPos = new Vector3(156, -2, 0);
                    containerScale = new Vector3(0.35f, 0.35f, 1);
                    enemyPos = new Vector3(-158, -239, 0);
                    enemyScale = new Vector3(0.7f, 0.7f, 1);
                }

                //Core.Logger.LogInfo(info.Name);
                GameObject parent = new GameObject();
                parent.name = "AP Icons";
                parent.transform.SetParent(levels[info.Name].transform.Find("Image"));
                parent.transform.localPosition = Vector3.zero;
                parent.transform.localScale = Vector3.one;

                GameObject container = new GameObject();
                container.name = "Container";
                container.transform.SetParent(parent.transform);
                container.transform.localPosition = containerPos;
                container.transform.localScale = containerScale;
                VerticalLayoutGroup containerLayoutGroup = container.AddComponent<VerticalLayoutGroup>();
                containerLayoutGroup.spacing = 10;
                containerLayoutGroup.reverseArrangement = true;
                container.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                container.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                container.GetComponent<RectTransform>().pivot = new Vector2(1, 1);

                if (Core.data.enemyRewards > EnemyOptions.Disabled && info.Enemies.Count > 0)
                {
                    int count = 0;
                    int total = 0;

                    foreach (EnemyType enemyType in info.Enemies)
                    {
                        if ((Core.data.enemyRewards == EnemyOptions.Bosses && Core.enemyBoss.Contains(enemyType))
                            || (Core.data.enemyRewards == EnemyOptions.Extra && (Core.enemyBoss.Contains(enemyType) || Core.enemyExt.Contains(enemyType)))
                            || Core.data.enemyRewards == EnemyOptions.All)
                        {
                            if (Core.data.@checked.Contains("e_" + enemyType.ToString().ToLower())) count++;
                            total++;
                        }
                    }

                    if (total > 0)
                    {
                        EnemyList enemyList = CreateMenuEnemyList(info, levels[info.Name]);
                        GameObject enemies = new GameObject();
                        enemies.name = "Enemies";
                        enemies.transform.SetParent(parent.transform);
                        enemies.transform.localPosition = enemyPos;
                        enemies.transform.localScale = enemyScale;
                        HorizontalLayoutGroup enemiesLayoutGroup = enemies.AddComponent<HorizontalLayoutGroup>();
                        enemiesLayoutGroup.childControlWidth = false;
                        enemiesLayoutGroup.childForceExpandWidth = false;
                        enemiesLayoutGroup.spacing = 4;
                        ContentSizeFitter enemiesFitter = enemies.AddComponent<ContentSizeFitter>();
                        enemiesFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                        enemiesFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                        CreateMenuEnemyCounter(info, enemies, enemyList, count, total);
                        enemies.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
                    }
                }

                if (Core.data.randomizeSkulls && (info.Flags & InfoFlags.HasAnySkulls) != 0)
                {
                    GameObject skulls = new GameObject();
                    skulls.name = "Skulls";
                    skulls.transform.SetParent(container.transform);
                    skulls.transform.localPosition = Vector3.zero;
                    skulls.transform.localScale = Vector3.one;
                    HorizontalLayoutGroup skullLayoutGroup = skulls.AddComponent<HorizontalLayoutGroup>();
                    skullLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
                    skullLayoutGroup.childScaleHeight = true;
                    skullLayoutGroup.childScaleWidth = true;
                    skullLayoutGroup.spacing = -24;
                    skullLayoutGroup.padding.right = -20;
                    skulls.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    skulls.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    CreateMenuSkullIcons(info, skulls);

                    skulls.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
                }

                if (info.Flags.HasFlag(InfoFlags.HasSwitches))
                {
                    GameObject switches = new GameObject();
                    switches.name = "Switches";
                    switches.transform.SetParent(container.transform);
                    switches.transform.localPosition = Vector3.zero;
                    switches.transform.localScale = Vector3.one;
                    HorizontalLayoutGroup switchLayoutGroup = switches.AddComponent<HorizontalLayoutGroup>();
                    switchLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
                    switchLayoutGroup.childScaleHeight = true;
                    switchLayoutGroup.childScaleWidth = true;
                    switchLayoutGroup.reverseArrangement = true;
                    switchLayoutGroup.spacing = 8;
                    switches.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    switches.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    CreateMenuSwitchIcons(info, switches);
                }
            }

            Sprite skullb = bundle.LoadAsset<Sprite>("assets/skullb.png");
            Sprite skullr = bundle.LoadAsset<Sprite>("assets/skullr.png");

            foreach (LevelInfo info in Core.secretMissionInfos)
            {
                if (info.Flags.HasFlag(InfoFlags.HasSkullsNormal))
                {
                    if (info.SkullsList == null) throw new Exception($"Skull list is null for level {info.Name}.");

                    int xPos = 260;
                    if (info.Name == "0-S") xPos = 275;
                    int xOffset = 38;
                    int yPos = 10;

                    GameObject rankPanel = secrets[info.Name].transform.parent.Find("RankPanel").gameObject;
                    Vector3 rankPos = rankPanel.transform.localPosition;
                    rankPos.x = xPos - (34 * info.SkullsList.Count);
                    rankPanel.transform.localPosition = rankPos;

                    foreach (string skull in info.SkullsList)
                    {
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(secrets[info.Name].transform.parent);
                        go.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        go.layer = 5;
                        if (skull.Contains("b")) go.AddComponent<Image>().sprite = skullb;
                        else go.AddComponent<Image>().sprite = skullr;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        go.transform.localPosition = new Vector3(xPos - (xOffset * info.SkullsList.FindIndex(a => a == skull)), yPos, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
                    }
                }
            }

            createdMenuIcons = true;
        }

        public static void CreateMenuEnemyCounter(LevelInfo info, GameObject gameObject, EnemyList enemyList, int count, int total)
        {
            Sprite sprite = Sprite.Create(killsTexture, new Rect(0, 0, 64, 64), new Vector2());

            GameObject icon = new GameObject();
            icon.name = "Icon";
            icon.transform.SetParent(gameObject.transform);
            icon.transform.localScale = Vector3.one;
            Image image = icon.AddComponent<Image>();
            image.sprite = sprite;
            icon.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            icon.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);

            GameObject textObj = new GameObject();
            textObj.name = "Text";
            textObj.transform.SetParent(gameObject.transform);
            textObj.transform.localScale = Vector3.one;
            TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
            text.font = bundle.LoadAsset<TMP_FontAsset>("assets/vcr_osd_mono_1.asset");
            text.fontMaterial = bundle.LoadAsset<Material>("assets/vcr_osd_mono_underlay.mat");
            text.fontSize = 64;
            textObj.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            textObj.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            if (count >= total)
            {
                image.color = Colors.ActComplete;
                text.color = Colors.ActComplete;
            }
            else
            {
                image.color = Colors.ActHighlight;
                text.color = Colors.ActHighlight;
            }

            text.GetComponent<TextMeshProUGUI>().text = $"{count}/{total}";
            gameObject.AddComponent<EnemyCountPointerHandler>().enemyList = enemyList;
            levels[info.Name].AddComponent<EnemyCountGamepadHandler>().enemyList = enemyList;
        }

        public static EnemyList CreateMenuEnemyList(LevelInfo info, GameObject gameObject)
        {
            GameObject list = new GameObject();
            list.name = "Enemy List";
            list.transform.SetParent(gameObject.transform);
            list.transform.localPosition = Vector3.zero;
            list.transform.localScale = Vector3.one;
            Image image = list.AddComponent<Image>();
            image.sprite = menuSprite1;
            image.pixelsPerUnitMultiplier = 4;
            image.type = Image.Type.Sliced;
            image.color = new Color(0, 0, 0, 0.8f);
            Canvas canvas = list.AddComponent<Canvas>();
            canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;

            GameObject layout = new GameObject();
            layout.name = "Layout";
            layout.transform.SetParent(list.transform);
            layout.transform.localPosition = Vector3.zero;
            layout.transform.localScale = Vector3.one;
            VerticalLayoutGroup verticalLayoutGroup = layout.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childControlWidth = false;
            verticalLayoutGroup.padding = new RectOffset(5, 5, 5, 5);
            verticalLayoutGroup.spacing = 1;
            ContentSizeFitter fitter = layout.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            foreach (EnemyType enemyType in info.Enemies)
            {
                if ((Core.data.enemyRewards == EnemyOptions.Bosses && Core.enemyBoss.Contains(enemyType))
                    || (Core.data.enemyRewards == EnemyOptions.Extra && (Core.enemyBoss.Contains(enemyType) || Core.enemyExt.Contains(enemyType)))
                    || Core.data.enemyRewards == EnemyOptions.All)
                {
                    GameObject go = new GameObject();
                    go.name = enemyType.ToString();
                    go.transform.SetParent(layout.transform);
                    go.transform.localScale = Vector3.one;
                    TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
                    text.font = fontMain;
                    text.fontSize = 16;
                    if (enemyType == EnemyType.Minotaur) text.fontStyle = FontStyles.Strikethrough;

                    if (Core.data.@checked.Contains("e_" + enemyType.ToString().ToLower()))
                    {
                        text.text = "X " + enemyType.ToReadableName();
                        text.color = Colors.ActHighlight;
                    }
                    else
                    {
                        text.text = "- " + enemyType.ToReadableName();
                    }
                    ContentSizeFitter csf = go.AddComponent<ContentSizeFitter>();
                    csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
            }

            EnemyList enemyList = list.AddComponent<EnemyList>();
            enemyList.childRect = layout.GetComponent<RectTransform>();
            enemyList.listCanvas = canvas;
            enemyList.id = info.Id;
            return enemyList;
        }

        public static void CreateMenuSkullIcons(LevelInfo info, GameObject gameObject)
        {
            Sprite skullb = bundle.LoadAsset<Sprite>("assets/skullb.png");
            Sprite skullr = bundle.LoadAsset<Sprite>("assets/skullr.png");

            if (info.Flags.HasFlag(InfoFlags.HasSkullsNormal))
            {
                if (info.SkullsList == null) throw new Exception($"Skull list is null for level {info.Name}.");

                foreach (string skull in info.SkullsList)
                {
                    GameObject go = new GameObject();
                    go.name = skull;
                    go.transform.SetParent(gameObject.transform);
                    go.transform.localScale = Vector3.one;
                    go.layer = 5;
                    if (skull.Contains("b")) go.AddComponent<Image>().sprite = skullb;
                    else go.AddComponent<Image>().sprite = skullr;
                    go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                    go.AddComponent<SkullIcon>().SetId(skull);
                    go.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                }
            }
            else if (info.Name == "1-4")
            {
                for (int i = 0; i < 4; i++)
                {
                    string skull = "9_b" + (i + 1);
                    GameObject go = new GameObject();
                    go.name = skull;
                    go.transform.SetParent(gameObject.transform);
                    go.transform.localScale = Vector3.one;
                    go.layer = 5;
                    go.AddComponent<Image>().sprite = skullb;
                    go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                    go.AddComponent<SkullIcon>().SetId(skull);
                    go.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                }
            }
            else if (info.Name == "5-1")
            {
                for (int i = 0; i < 3; i++)
                {
                    string skull = "20_b" + (i + 1);
                    GameObject go = new GameObject();
                    go.name = skull;
                    go.transform.SetParent(gameObject.transform);
                    go.transform.localScale = Vector3.one;
                    go.layer = 5;
                    go.AddComponent<Image>().sprite = skullb;
                    go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                    go.AddComponent<SkullIcon>().SetId(skull);
                    go.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                }
            }
            createdSkullIcons = true;
        }

        public static void CreateMenuSwitchIcons(LevelInfo info, GameObject gameObject)
        {
            if (Core.data.l1switch && info.Id == 9)
            {
                for (int i = 4; i > 0; i--)
                {
                    Sprite sprite = bundle.LoadAsset<Sprite>($"assets/switch{i}.png");
                    GameObject go = new GameObject();
                    go.name = i.ToString();
                    go.transform.SetParent(gameObject.transform);
                    go.transform.localScale = Vector3.one;
                    go.layer = 5;
                    go.AddComponent<Image>().sprite = sprite;
                    go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                    go.AddComponent<SwitchIcon>().SetId(i - 1, true);
                    go.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                }
            }
            if (Core.data.l7switch && info.Id == 27)
            {
                for (int i = 3; i > 0; i--)
                {
                    Sprite sprite = bundle.LoadAsset<Sprite>($"assets/switch{i}.png");
                    GameObject go = new GameObject();
                    go.name = i.ToString();
                    go.transform.SetParent(gameObject.transform);
                    go.transform.localScale = Vector3.one;
                    go.layer = 5;
                    go.AddComponent<Image>().sprite = sprite;
                    go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                    go.AddComponent<SwitchIcon>().SetId(i - 1, false);
                    go.AddComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                }
            }
            createdSwitchIcons = true;
        }

        public static void CreateChapterRecents(GameObject chapterSelect)
        {
            GameObject container = new GameObject();
            container.name = "Recent Stuff";
            container.transform.SetParent(chapterSelect.transform);
            container.transform.localPosition = new Vector3(-330, 50, 0);
            container.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

            chapterSelect.GetComponent<ObjectActivateInSequence>().objectsToActivate = chapterSelect.GetComponent<ObjectActivateInSequence>().objectsToActivate.AddItem(container).ToArray();

            if (recentLocationContainer == null)
            {
                recentLocationContainer = new GameObject();
                recentLocationContainer.name = "Locations";
                recentLocationContainer.transform.SetParent(container.transform);
                if (UIConfig.showRecentLocationsChapter.value) recentLocationContainer.transform.localPosition = new Vector3(0, 190, 0);
                else recentLocationContainer.transform.localPosition = Vector3.zero;
                recentLocationContainer.transform.localScale = Vector3.one;
                recentLocationContainer.AddComponent<RectTransform>().sizeDelta = new Vector2(250, 400);

                GameObject locationHeader = new GameObject();
                locationHeader.name = "Text";
                locationHeader.transform.SetParent(recentLocationContainer.transform);
                locationHeader.transform.localPosition = new Vector3(0, 160, 0);
                locationHeader.transform.localScale = Vector3.one;
                TextMeshProUGUI locationHeaderText = locationHeader.AddComponent<TextMeshProUGUI>();
                locationHeaderText.font = fontMain;
                locationHeaderText.fontSize = 28;
                locationHeaderText.alignment = TextAlignmentOptions.Center;
                locationHeaderText.text = "RECENT\nLOCATIONS";

                GameObject locationContainer1 = new GameObject();
                locationContainer1.name = "Container 1";
                locationContainer1.transform.SetParent(recentLocationContainer.transform);
                locationContainer1.transform.localPosition = new Vector3(0, 90, 0);
                locationContainer1.transform.localScale = Vector3.one;
                GameObject locationContainer1Image = new GameObject();
                locationContainer1Image.name = "Image";
                locationContainer1Image.transform.SetParent(locationContainer1.transform);
                locationContainer1Image.transform.localPosition = new Vector3(90, 0, 0);
                locationContainer1Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                locationContainer1Image.transform.localRotation = Quaternion.identity;
                Image locationContainer1ImageC = locationContainer1Image.AddComponent<Image>();
                GameObject locationContainer1Text = new GameObject();
                locationContainer1Text.name = "Text";
                locationContainer1Text.transform.SetParent(locationContainer1.transform);
                locationContainer1Text.transform.localPosition = new Vector3(-30, 0, 0);
                locationContainer1Text.transform.localScale = Vector3.one;
                locationContainer1Text.transform.localRotation = Quaternion.identity;
                locationContainer1Text.AddComponent<RectTransform>().sizeDelta = new Vector2(175, 55);
                TextMeshProUGUI locationContainer1TextC = locationContainer1Text.AddComponent<TextMeshProUGUI>();
                locationContainer1TextC.font = fontSecondary;
                locationContainer1TextC.enableAutoSizing = true;
                locationContainer1TextC.margin = new Vector4(0, 0, 5, 0);
                locationContainer1TextC.alignment = TextAlignmentOptions.Right;
                locationContainer1.AddComponent<RecentContainer>().Init(0, RecentType.Location, locationContainer1ImageC, locationContainer1TextC);

                for (int i = 1; i < 5; i++)
                {
                    GameObject locationContainerI = GameObject.Instantiate(locationContainer1, recentLocationContainer.transform);
                    locationContainerI.name = $"Container {i + 1}";
                    locationContainerI.transform.localPosition = new Vector3(0, (90 - (60 * i)), 0);
                    locationContainerI.GetComponent<RecentContainer>().id = i;

                    if (i == 2)
                    {
                        GameObject offlineText = new GameObject();
                        offlineText.name = "Offline Text";
                        offlineText.transform.SetParent(locationContainerI.transform);
                        offlineText.transform.localPosition = Vector3.zero;
                        offlineText.transform.localScale = Vector3.one;
                        TextMeshProUGUI offlineTextC = offlineText.AddComponent<TextMeshProUGUI>();
                        offlineTextC.font = fontMain;
                        offlineTextC.fontSize = 28;
                        offlineTextC.alignment = TextAlignmentOptions.Center;
                        offlineTextC.text = "OFFLINE";
                        offlineTextC.color = new Color(0.5f, 0.5f, 0.5f);
                        offlineText.SetActive(false);
                    }
                }
                if (!UIConfig.showRecentLocationsChapter.value) recentLocationContainer.SetActive(false);
            }

            if (recentItemContainer == null)
            {
                recentItemContainer = new GameObject();
                recentItemContainer.name = "Items";
                recentItemContainer.transform.SetParent(container.transform);
                if (UIConfig.showRecentItemsChapter.value) recentItemContainer.transform.localPosition = new Vector3(0, -190, 0);
                else recentItemContainer.transform.localPosition = Vector3.zero;
                recentItemContainer.transform.localScale = Vector3.one;
                recentItemContainer.AddComponent<RectTransform>().sizeDelta = new Vector2(250, 400);

                GameObject itemHeader = new GameObject();
                itemHeader.name = "Text";
                itemHeader.transform.SetParent(recentItemContainer.transform);
                itemHeader.transform.localPosition = new Vector3(0, 160, 0);
                itemHeader.transform.localScale = Vector3.one;
                TextMeshProUGUI itemHeaderText = itemHeader.AddComponent<TextMeshProUGUI>();
                itemHeaderText.font = fontMain;
                itemHeaderText.fontSize = 28;
                itemHeaderText.alignment = TextAlignmentOptions.Center;
                itemHeaderText.text = "RECENT\nITEMS";

                GameObject itemContainer1 = new GameObject() { name = "Container 1" };
                itemContainer1.transform.SetParent(recentItemContainer.transform);
                itemContainer1.transform.localPosition = new Vector3(0, 90, 0);
                itemContainer1.transform.localScale = Vector3.one;
                GameObject itemContainer1Image = new GameObject() { name = "Image" };
                itemContainer1Image.transform.SetParent(itemContainer1.transform);
                itemContainer1Image.transform.localPosition = new Vector3(90, 0, 0);
                itemContainer1Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                Image itemContainer1ImageC = itemContainer1Image.AddComponent<Image>();
                GameObject itemContainer1Text = new GameObject() { name = "Text" };
                itemContainer1Text.transform.SetParent(itemContainer1.transform);
                itemContainer1Text.transform.localPosition = new Vector3(-30, 0, 0);
                itemContainer1Text.transform.localScale = Vector3.one;
                itemContainer1Text.AddComponent<RectTransform>().sizeDelta = new Vector2(175, 55);
                TextMeshProUGUI itemContainer1TextC = itemContainer1Text.AddComponent<TextMeshProUGUI>();
                itemContainer1TextC.font = fontSecondary;
                itemContainer1TextC.enableAutoSizing = true;
                itemContainer1TextC.margin = new Vector4(5, 0, 0, 0);
                itemContainer1TextC.alignment = TextAlignmentOptions.Right;
                itemContainer1.AddComponent<RecentContainer>().Init(0, RecentType.Item, itemContainer1ImageC, itemContainer1TextC);

                for (int i = 1; i < 5; i++)
                {
                    GameObject itemContainerI = GameObject.Instantiate(itemContainer1, recentItemContainer.transform);
                    itemContainerI.name = $"Container {i + 1}";
                    itemContainerI.transform.localPosition = new Vector3(0, (90 - (60 * i)), 0);
                    itemContainerI.GetComponent<RecentContainer>().id = i;

                    if (i == 2)
                    {
                        GameObject offlineText = new GameObject() { name = "Offline Text" };
                        offlineText.transform.SetParent(itemContainerI.transform);
                        offlineText.transform.localPosition = Vector3.zero;
                        offlineText.transform.localScale = Vector3.one;
                        TextMeshProUGUI offlineTextC = offlineText.AddComponent<TextMeshProUGUI>();
                        offlineTextC.font = fontMain;
                        offlineTextC.fontSize = 28;
                        offlineTextC.alignment = TextAlignmentOptions.Center;
                        offlineTextC.text = "OFFLINE";
                        offlineTextC.color = new Color(0.5f, 0.5f, 0.5f);
                        offlineText.SetActive(false);
                    }
                }
                if (!UIConfig.showRecentItemsChapter.value) recentItemContainer.SetActive(false);
            }
        }

        public static void CreatePauseRecents(GameObject pauseMenu)
        {
            if (recentLocationContainer == null)
            {
                // locations - left side
                recentLocationContainer = new GameObject() { name = "Recent Locations" };
                recentLocationContainer.transform.SetParent(pauseMenu.transform);
                recentLocationContainer.transform.localPosition = new Vector3(-380, 0, 0);
                recentLocationContainer.transform.localScale = Vector3.one;
                recentLocationContainer.AddComponent<RectTransform>().sizeDelta = new Vector2(250, 400);
                Image locationImage = recentLocationContainer.AddComponent<Image>();
                locationImage.sprite = menuSprite1;
                locationImage.pixelsPerUnitMultiplier = 2;
                locationImage.type = Image.Type.Sliced;
                locationImage.color = new Color(0, 0, 0, 0.7843f);

                GameObject locationBorder = new GameObject() { name = "Border" };
                locationBorder.transform.SetParent(recentLocationContainer.transform);
                locationBorder.transform.localPosition = Vector3.zero;
                locationBorder.transform.localScale = Vector3.one;
                RectTransform locationBorderRectTransform = locationBorder.AddComponent<RectTransform>();
                locationBorderRectTransform.anchorMax = new Vector2(1, 1);
                locationBorderRectTransform.anchorMin = new Vector2(0, 0);
                locationBorderRectTransform.sizeDelta = new Vector2(20, 20);
                Image locationBorderImage = locationBorder.AddComponent<Image>();
                locationBorderImage.sprite = menuSprite2;
                locationBorderImage.pixelsPerUnitMultiplier = 2;
                locationBorderImage.type = Image.Type.Sliced;
                locationBorderImage.color = new Color(0, 0, 0, 0.7843f);

                GameObject locationHeader = new GameObject() { name = "Text" };
                locationHeader.transform.SetParent(recentLocationContainer.transform);
                locationHeader.transform.localPosition = new Vector3(0, 160, 0);
                locationHeader.transform.localScale = Vector3.one;
                TextMeshProUGUI locationHeaderText = locationHeader.AddComponent<TextMeshProUGUI>();
                locationHeaderText.font = fontMain;
                locationHeaderText.fontSize = 28;
                locationHeaderText.alignment = TextAlignmentOptions.Center;
                locationHeaderText.text = "RECENT\nLOCATIONS";

                GameObject locationContainer1 = new GameObject() { name = "Container 1" };
                locationContainer1.transform.SetParent(recentLocationContainer.transform);
                locationContainer1.transform.localPosition = new Vector3(0, 90, 0);
                locationContainer1.transform.localScale = Vector3.one;
                GameObject locationContainer1Image = new GameObject() { name = "Image" };
                locationContainer1Image.transform.SetParent(locationContainer1.transform);
                locationContainer1Image.transform.localPosition = new Vector3(90, 0, 0);
                locationContainer1Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                locationContainer1Image.transform.localRotation = Quaternion.identity;
                Image locationContainer1ImageC = locationContainer1Image.AddComponent<Image>();
                GameObject locationContainer1Text = new GameObject() { name = "Text" };
                locationContainer1Text.transform.SetParent(locationContainer1.transform);
                locationContainer1Text.transform.localPosition = new Vector3(-30, 0, 0);
                locationContainer1Text.transform.localScale = Vector3.one;
                locationContainer1Text.transform.localRotation = Quaternion.identity;
                locationContainer1Text.AddComponent<RectTransform>().sizeDelta = new Vector2(175, 55);
                TextMeshProUGUI locationContainer1TextC = locationContainer1Text.AddComponent<TextMeshProUGUI>();
                locationContainer1TextC.font = fontSecondary;
                locationContainer1TextC.enableAutoSizing = true;
                locationContainer1TextC.margin = new Vector4(0, 0, 5, 0);
                locationContainer1TextC.alignment = TextAlignmentOptions.Right;
                locationContainer1.AddComponent<RecentContainer>().Init(0, RecentType.Location, locationContainer1ImageC, locationContainer1TextC);

                for (int i = 1; i < 5; i++)
                {
                    GameObject locationContainerI = GameObject.Instantiate(locationContainer1, recentLocationContainer.transform);
                    locationContainerI.name = $"Container {i + 1}";
                    locationContainerI.transform.localPosition = new Vector3(0, (90 - (60 * i)), 0);
                    locationContainerI.GetComponent<RecentContainer>().id = i;

                    if (i == 2)
                    {
                        GameObject offlineText = new GameObject() { name = "Offline Text" };
                        offlineText.transform.SetParent(locationContainerI.transform);
                        offlineText.transform.localPosition = Vector3.zero;
                        offlineText.transform.localScale = Vector3.one;
                        TextMeshProUGUI offlineTextC = offlineText.AddComponent<TextMeshProUGUI>();
                        offlineTextC.font = fontMain;
                        offlineTextC.fontSize = 28;
                        offlineTextC.alignment = TextAlignmentOptions.Center;
                        offlineTextC.text = "OFFLINE";
                        offlineTextC.color = new Color(0.5f, 0.5f, 0.5f);
                        offlineText.SetActive(false);
                    }
                }
                if (!UIConfig.showRecentLocationsPause.value) recentLocationContainer.SetActive(false);
            }
            if (recentItemContainer == null)
            {
                // items - right side
                recentItemContainer = new GameObject() { name = "Recent Items" };
                recentItemContainer.transform.SetParent(pauseMenu.transform);
                recentItemContainer.transform.localPosition = new Vector3(380, 0, 0);
                recentItemContainer.transform.localScale = Vector3.one;
                recentItemContainer.AddComponent<RectTransform>().sizeDelta = new Vector2(250, 400);
                Image itemImage = recentItemContainer.AddComponent<Image>();
                itemImage.sprite = menuSprite1;
                itemImage.pixelsPerUnitMultiplier = 2;
                itemImage.type = Image.Type.Sliced;
                itemImage.color = new Color(0, 0, 0, 0.7843f);

                GameObject itemBorder = new GameObject() { name = "Border" };
                itemBorder.transform.SetParent(recentItemContainer.transform);
                itemBorder.transform.localPosition = Vector3.zero;
                itemBorder.transform.localScale = Vector3.one;
                RectTransform itemBorderRectTransform = itemBorder.AddComponent<RectTransform>();
                itemBorderRectTransform.anchorMax = new Vector2(1, 1);
                itemBorderRectTransform.anchorMin = new Vector2(0, 0);
                itemBorderRectTransform.sizeDelta = new Vector2(20, 20);
                Image itemBorderImage = itemBorder.AddComponent<Image>();
                itemBorderImage.sprite = menuSprite2;
                itemBorderImage.pixelsPerUnitMultiplier = 2;
                itemBorderImage.type = Image.Type.Sliced;
                itemBorderImage.color = new Color(0, 0, 0, 0.7843f);

                GameObject itemHeader = new GameObject() { name = "Text" };
                itemHeader.transform.SetParent(recentItemContainer.transform);
                itemHeader.transform.localPosition = new Vector3(0, 160, 0);
                itemHeader.transform.localScale = Vector3.one;
                TextMeshProUGUI itemHeaderText = itemHeader.AddComponent<TextMeshProUGUI>();
                itemHeaderText.font = fontMain;
                itemHeaderText.fontSize = 28;
                itemHeaderText.alignment = TextAlignmentOptions.Center;
                itemHeaderText.text = "RECENT\nITEMS";

                GameObject itemContainer1 = new GameObject() { name = "Container 1" };
                itemContainer1.transform.SetParent(recentItemContainer.transform);
                itemContainer1.transform.localPosition = new Vector3(0, 90, 0);
                itemContainer1.transform.localScale = Vector3.one;
                GameObject itemContainer1Image = new GameObject() { name = "Image" };
                itemContainer1Image.transform.SetParent(itemContainer1.transform);
                itemContainer1Image.transform.localPosition = new Vector3(-90, 0, 0);
                itemContainer1Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                Image itemContainer1ImageC = itemContainer1Image.AddComponent<Image>();
                GameObject itemContainer1Text = new GameObject() { name = "Text" };
                itemContainer1Text.transform.SetParent(itemContainer1.transform);
                itemContainer1Text.transform.localPosition = new Vector3(30, 0, 0);
                itemContainer1Text.transform.localScale = Vector3.one;
                itemContainer1Text.AddComponent<RectTransform>().sizeDelta = new Vector2(175, 55);
                TextMeshProUGUI itemContainer1TextC = itemContainer1Text.AddComponent<TextMeshProUGUI>();
                itemContainer1TextC.font = fontSecondary;
                itemContainer1TextC.enableAutoSizing = true;
                itemContainer1TextC.margin = new Vector4(5, 0, 0, 0);
                itemContainer1TextC.alignment = TextAlignmentOptions.Left;
                itemContainer1.AddComponent<RecentContainer>().Init(0, RecentType.Item, itemContainer1ImageC, itemContainer1TextC);

                for (int i = 1; i < 5; i++)
                {
                    GameObject itemContainerI = GameObject.Instantiate(itemContainer1, recentItemContainer.transform);
                    itemContainerI.name = $"Container {i + 1}";
                    itemContainerI.transform.localPosition = new Vector3(0, (90 - (60 * i)), 0);
                    itemContainerI.GetComponent<RecentContainer>().id = i;

                    if (i == 2)
                    {
                        GameObject offlineText = new GameObject() { name = "Offline Text" };
                        offlineText.transform.SetParent(itemContainerI.transform);
                        offlineText.transform.localPosition = Vector3.zero;
                        offlineText.transform.localScale = Vector3.one;
                        TextMeshProUGUI offlineTextC = offlineText.AddComponent<TextMeshProUGUI>();
                        offlineTextC.font = fontMain;
                        offlineTextC.fontSize = 28;
                        offlineTextC.alignment = TextAlignmentOptions.Center;
                        offlineTextC.text = "OFFLINE";
                        offlineTextC.color = new Color(0.5f, 0.5f, 0.5f);
                        offlineText.SetActive(false);
                    }
                }
                if (!UIConfig.showRecentItemsPause.value) recentItemContainer.SetActive(false);
            }
        }

        public static void CreatePauseIconContainer(GameObject pauseMenu)
        {
            if (pauseContainer != null) return;

            float yPos = -240;
            float ySize = 50;
            if (Core.CurrentLevelHasSkulls && Core.CurrentLevelHasSwitches)
            {
                yPos = -260;
                ySize = 90;
            }
            pauseContainer = new GameObject() { name = "Icons BG" };
            pauseContainer.transform.SetParent(pauseMenu.transform);
            pauseContainer.transform.localPosition = new Vector3(0, yPos, 0);
            pauseContainer.transform.localScale = Vector3.one;
            pauseContainer.AddComponent<RectTransform>().sizeDelta = new Vector2(50, ySize);
            Image bgImage = pauseContainer.AddComponent<Image>();
            bgImage.sprite = menuSprite1;
            bgImage.pixelsPerUnitMultiplier = 5;
            bgImage.type = Image.Type.Sliced;
            bgImage.color = new Color(0, 0, 0, 0.7843f);
        }

        public static void CreatePauseSkullIcons(GameObject pauseMenu)
        {
            if (Core.CurrentLevelHasInfo && !createdSkullIcons)
            {
                if (pauseContainer == null) CreatePauseIconContainer(pauseMenu);

                Sprite skullb = bundle.LoadAsset<Sprite>("assets/skullb.png");
                Sprite skullr = bundle.LoadAsset<Sprite>("assets/skullr.png");
                LevelInfo info = Core.CurrentLevelInfo;

                float xOffset = 36;
                float xPos = 0;
                float yPos = 1;
                if (Core.CurrentLevelHasSkulls && Core.CurrentLevelHasSwitches) yPos = 21;
                if (info.Flags.HasFlag(InfoFlags.HasSkullsNormal))
                {
                    if (info.SkullsList == null) throw new Exception($"Skull list is null for level {info.Name}");
                    if (info.SkullsList.Count > 1)
                    {
                        xPos = xOffset * (((float)info.SkullsList.Count - 1) / 2);
                        float xSize = 15 + (35 * info.SkullsList.Count);
                        float ySize = (Core.CurrentLevelHasSkulls && Core.CurrentLevelHasSwitches) ? 90 : 50;
                        pauseContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);
                    }
                    foreach (string skull in info.SkullsList)
                    {
                        if (pauseMenu.transform.Find(skull)) continue;
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(pauseContainer.transform);
                        go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                        go.layer = 5;
                        if (skull.Contains("b")) go.AddComponent<Image>().sprite = skullb;
                        else go.AddComponent<Image>().sprite = skullr;
                        go.transform.localPosition = new Vector3(xPos - (xOffset * info.SkullsList.FindIndex(a => a == skull)), yPos, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
                    }
                }
                else if (info.Name == "1-4")
                {
                    xPos = xOffset * 1.5f;
                    pauseContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(15 + (35 * 4), 50);
                    for (int i = 0; i < 4; i++)
                    {
                        string skull = "9_b" + (i + 1);
                        if (pauseMenu.transform.Find(skull)) continue;
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(pauseContainer.transform);
                        go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = skullb;
                        go.transform.localPosition = new Vector3(-xPos + (xOffset * i), yPos, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
                    }
                }
                else if (info.Name == "5-1")
                {
                    xPos = xOffset;
                    pauseContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(15 + (35 * 3), 50);
                    for (int i = 0; i < 3; i++)
                    {
                        string skull = "20_b" + (i + 1);
                        if (pauseMenu.transform.Find(skull)) continue;
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(pauseContainer.transform);
                        go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = skullb;
                        go.transform.localPosition = new Vector3(-xPos + (xOffset * i), yPos, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
                    }
                }
                createdSkullIcons = true;
            }
        }

        public static void CreatePauseSwitchIcons(GameObject pauseMenu)
        {
            if (Core.CurrentLevelHasInfo && !createdSwitchIcons)
            {
                if (!(Core.CurrentLevelInfo.Id == 9 || Core.CurrentLevelInfo.Id == 27)) return;
                if (pauseContainer == null) CreatePauseIconContainer(pauseMenu);

                float xPos = 0;
                float xOffset = 40;
                float yPos = 0;
                if (Core.CurrentLevelHasSkulls && Core.CurrentLevelHasSwitches) yPos = -20;

                int switches = 0;
                if (Core.CurrentLevelInfo.Id == 9) switches = 4;
                if (Core.CurrentLevelInfo.Id == 27) switches = 3;

                xPos = xOffset * (((float)switches - 1) / 2);
                float xSize = 10 + (40 * switches);
                float ySize = (Core.CurrentLevelHasSkulls && Core.CurrentLevelHasSwitches) ? 90 : 50;
                if (pauseContainer.GetComponent<RectTransform>().sizeDelta.x < xSize) pauseContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);

                bool limbo = true;
                if (Core.CurrentLevelInfo.Id == 27) limbo = false;

                for (int i = switches; i > 0; i--)
                {
                    Sprite sprite = bundle.LoadAsset<Sprite>($"assets/switch{i}.png");
                    if (pauseMenu.transform.Find(i.ToString())) continue;
                    GameObject go = new GameObject();
                    go.name = i.ToString();
                    go.transform.SetParent(pauseContainer.transform);
                    go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                    go.layer = 5;
                    go.AddComponent<Image>().sprite = sprite;
                    go.transform.localPosition = new Vector3(xPos - (xOffset * (switches - i)), yPos, 0);
                    go.AddComponent<SwitchIcon>().SetId(i-1, limbo);
                }
                createdSwitchIcons = true;
            }
        }

        public static void CreateMessageUI()
        {
            GameObject player = GameObject.Find("/Player");
            foreach (HudController component in player.GetComponentsInChildren<HudController>())
            {
                //Core.logger.LogInfo(component.gameObject.name);
                hud = component.gameObject;
            }

            popupCanvas = Instantiate(hud.transform.GetChild(0).gameObject, hud.transform);
            popupCanvas.name = "APCanvas";
            Traverse hudT = Traverse.Create(popupCanvas.GetComponent<HUDPos>());
            hudT.Field<Vector3>("defaultPos").Value = new Vector3(1.125f, -0.53f, 1);
            hudT.Field<Vector3>("defaultRot").Value = new Vector3(0, 30, 0);
            popupCanvas.GetComponent<HUDPos>().reversePos = new Vector3(-1.06f, -0.53f, 1);
            popupCanvas.GetComponent<HUDPos>().reverseRot = new Vector3(0, 330, 0);
            popupCanvas.GetComponent<HUDPos>().active = false;
            popupCanvas.GetComponent<HUDPos>().active = true;
            Destroy(popupCanvas.transform.Find("SpeedometerPanel").gameObject);
            Destroy(popupCanvas.transform.Find("GunPanel").gameObject);
            Destroy(popupCanvas.transform.Find("StatsPanel").Find("RailcannonChargePanel").gameObject);
            Destroy(popupCanvas.transform.Find("StatsPanel").Find("Filler").Find("Panel (2)").gameObject);
            Destroy(popupCanvas.transform.Find("StatsPanel").Find("Filler").Find("Panel (3)").gameObject);
            Destroy(popupCanvas.transform.Find("StatsPanel").Find("Filler").Find("Panel").gameObject);
            Destroy(popupCanvas.transform.Find("StatsPanel").Find("Filler").Find("AltRailcannonPanel").gameObject);

            try
            {
                popupImage = popupCanvas.transform.Find("StatsPanel").Find("Filler").Find("FistPanel").Find("Panel").gameObject;
            }
            catch (NullReferenceException)
            {
                foreach (Image image in popupCanvas.transform.Find("StatsPanel").Find("Filler").GetComponentsInChildren<Image>())
                {
                    if (image.name == "Panel" && image.transform.parent.name == "FistPanel") popupImage = image.gameObject;
                }
            }
            popupImage.transform.parent.gameObject.SetActive(true);
            Destroy(popupImage.transform.parent.Find("Background").gameObject);


            popupCanvas.transform.Find("StatsPanel").gameObject.SetActive(true);
            popupCanvas.SetActive(false);
            popupImage.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/layer4.png");
            popupImage.GetComponent<Image>().color = Colors.Layer4;
            GameObject go = new GameObject();
            go.name = "APText";
            go.transform.SetParent(popupCanvas.transform.Find("StatsPanel"));
            go.layer = 13;
            popupText = go.AddComponent<TextMeshProUGUI>();
            popupText.font = fontMain;
            popupText.alignment = TextAlignmentOptions.Right;
            popupText.enableAutoSizing = true;
            popupText.fontSizeMax = 120;
            popupText.lineSpacing = 1.1f;
            popupText.text = "UNLOCKED: <color=#ffe800ff>4-2 GOD DAMN THE SUN</color> (<color=#fafad2ff>Trev</color>)";
            popupText.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 150);
            popupText.transform.localPosition = new Vector3(100.6437f, 23.9347f, 0);
            popupText.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            popupText.transform.localRotation = new Quaternion();
        }

        public IEnumerator DisplayMessage()
        {
            displayingMessage = true;
            Message message = LocationManager.messages[0];
            LocationManager.messages.RemoveAt(0);

            popupText.text = message.message;
            popupImage.GetComponent<Image>().color = message.color;

            if (bundle.Contains("assets/" + message.image + ".png")) popupImage.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/" + message.image + ".png");
            else popupImage.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/archipelago.png");

            popupCanvas.SetActive(true);
            popupText.isOverlay = true;
            yield return new WaitForSeconds(3f);
            popupCanvas.SetActive(false);
            popupText.isOverlay = false;
            if (LocationManager.messages.Count > 0 && Core.IsPlaying) StartCoroutine(DisplayMessage());
            else displayingMessage = false;
        }

        public void CreateDeathLinkMessage()
        {
            GameObject go = new GameObject()
            {
                name = "APDeathMessage",
                layer = 5,
            };
            GameObject go2 = new GameObject()
            {
                name = "Text",
                layer = 5
            };
            go.transform.parent = Traverse.Create(NewMovement.Instance.deathSequence).Field<GameObject>("deathScreen").Value.transform;
            go.AddComponent<Canvas>();
            go2.transform.parent = go.transform;
            go2.AddComponent<Text>();
            deathLinkMessage = go2.AddComponent<DeathLinkMessage>();
            deathLinkMessage.Initialize();
        }
    }
}