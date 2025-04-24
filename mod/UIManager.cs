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

namespace ArchipelagoULTRAKILL
{
    public class UIManager : MonoBehaviour
    {
        public static AssetBundle bundle = AssetBundle.LoadFromMemory(Properties.Resources.trpg_archipelago);

        public static Sprite menuSprite1;
        public static Sprite menuSprite2;

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
            if (LocationManager.itemQueue.Count > 0)
            {
                QueuedItem qItem = LocationManager.itemQueue[0];
                LocationManager.GetUKItem(qItem.item, qItem.sendingPlayer, qItem.silent);
                LocationManager.itemQueue.RemoveAt(0);
            }
        }

        public void CreateLogObject()
        {
            StartCoroutine(VersionChecker.CheckVersion());

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
            log.fontSize = ConfigManager.logFontSize.value;
            log.alignment = TextAlignmentOptions.BottomGeoAligned;
            log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height - 10);
            SetLogFont(ConfigManager.logFont.value);
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
                    ConfigManager.logFontSize.value = 20;
                    log.fontSize = 20;
                }
            }
            else if (font == LogFont.Pixel2)
            {
                fontAsset = bundle.LoadAsset<TMP_FontAsset>("assets/vcr_osd_mono_1.asset");
                fontMat = bundle.LoadAsset<Material>("assets/vcr_osd_mono_underlay.mat");
                if (reset)
                {
                    ConfigManager.logFontSize.value = 16;
                    log.fontSize = 16;
                }
            }
            else if (font == LogFont.SansSerif)
            {
                fontAsset = bundle.LoadAsset<TMP_FontAsset>("assets/roboto-regular sdf.asset");
                fontMat = bundle.LoadAsset<Material>("assets/roboto-regular_underlay.mat");
                if (reset)
                {
                    ConfigManager.logFontSize.value = 14;
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
            actStats.transform.position = new Vector3(rankPos.x + 77, rankPos.y, rankPos.z);
            actStats.overflowMode = TextOverflowModes.Overflow;
            actStats.alignment = TextAlignmentOptions.TopLeft;
            actStats.lineSpacing = 1.2f;
            actStats.gameObject.SetActive(false);

            chapterSelect.transform.Find("Prelude").gameObject.AddComponent<ActStats>().Init(1, 5);
            chapterSelect.transform.Find("Act I").gameObject.AddComponent<ActStats>().Init(6, 15);
            chapterSelect.transform.Find("Act II").gameObject.AddComponent<ActStats>().Init(16, 25);
            chapterSelect.transform.Find("Act III").gameObject.AddComponent<ActStats>().Init(26, 29);
            chapterSelect.transform.Find("Encore").gameObject.AddComponent<ActStats>().Init(100, 101, true);
            chapterSelect.transform.Find("Prime").gameObject.AddComponent<ActStats>().Init(666, 667, true);

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
                else if (GameStateManager.ShowLeaderboards) go.transform.localPosition = new Vector3(0, 90, 0);
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

        public static void CreateMenuSkullIcons()
        {
            Sprite skullb = bundle.LoadAsset<Sprite>("assets/skullb.png");
            Sprite skullr = bundle.LoadAsset<Sprite>("assets/skullr.png");

            foreach (LevelInfo info in Core.levelInfos)
            {
                int xPos = 74;
                int xOffset = 33;
                int yPos = 65;
                if (info.Skulls == SkullsType.Normal)
                {
                    if (info.SkullsList == null) throw new Exception($"Skull list is null for level {info.Name}.");
                    bool reposition = true;
                    if (info.Name.Contains("P-"))
                    {
                        reposition = false;
                        xPos = 140;
                        yPos = 112;
                    }
                    if (info.Name.Contains("-E"))
                    {
                        reposition = false;
                        yPos = 140;
                    }

                    foreach (string skull in info.SkullsList)
                    {
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(levels[info.Name].transform);
                        go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                        go.layer = 5;
                        if (skull.Contains("b")) go.AddComponent<Image>().sprite = skullb;
                        else go.AddComponent<Image>().sprite = skullr;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        go.transform.localPosition = new Vector3(xPos - (xOffset * info.SkullsList.FindIndex(a => a == skull)), yPos, 0);
                        go.AddComponent<SkullIcon>().SetId(skull, reposition);
                    }
                }
                else if (info.Name == "1-4")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        string skull = "9_b" + (i + 1);
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(levels[info.Name].transform);
                        go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = skullb;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        go.transform.localPosition = new Vector3(xPos - (xOffset * i), yPos, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
                    }
                }
                else if (info.Name == "5-1")
                {
                    for (int i = 0; i < 3; i++)
                    {
                        string skull = "20_b" + (i + 1);
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(levels[info.Name].transform);
                        go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = skullb;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        go.transform.localPosition = new Vector3(xPos - (xOffset * i), yPos, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
                    }
                }
            }

            foreach (LevelInfo info in Core.secretMissionInfos)
            {
                if (info.Skulls == SkullsType.Normal)
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
                        go.AddComponent<SkullIcon>().SetId(skull, false);
                    }
                }
            }
            createdSkullIcons = true;
        }

        public static void CreateMenuSwitchIcons()
        {
            int xPos = 70;
            int xOffset = 40;
            if (Core.data.l1switch)
            {
                for (int i = 4; i > 0; i--)
                {
                    Sprite sprite = bundle.LoadAsset<Sprite>($"assets/switch{i}.png");
                    GameObject go = new GameObject();
                    go.name = i.ToString();
                    go.transform.SetParent(levels["1-4"].transform);
                    go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                    go.layer = 5;
                    go.AddComponent<Image>().sprite = sprite;
                    go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                    go.transform.localPosition = new Vector3(xPos - (xOffset * (4 - i)), -32, 0);
                    go.AddComponent<SwitchIcon>().SetId(i - 1, true, true);
                }
            }
            if (Core.data.l7switch)
            {
                for (int i = 3; i > 0; i--)
                {
                    Sprite sprite = bundle.LoadAsset<Sprite>($"assets/switch{i}.png");
                    GameObject go = new GameObject();
                    go.name = i.ToString();
                    go.transform.SetParent(levels["7-2"].transform);
                    go.transform.localScale = new Vector3(0.35f, 0.35f, 1);
                    go.layer = 5;
                    go.AddComponent<Image>().sprite = sprite;
                    go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                    go.transform.localPosition = new Vector3(xPos - (xOffset * (3 - i)), -32, 0);
                    go.AddComponent<SwitchIcon>().SetId(i - 1, false, true);
                }
            }
            createdSwitchIcons = true;
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
                if (!ConfigManager.showRecentLocations.value) recentLocationContainer.SetActive(false);
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
                if (!ConfigManager.showRecentItems.value) recentItemContainer.SetActive(false);
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
                if (info.Skulls == SkullsType.Normal)
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
                        go.AddComponent<SkullIcon>().SetId(skull, false);
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
                        go.AddComponent<SkullIcon>().SetId(skull, false);
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
                        go.AddComponent<SkullIcon>().SetId(skull, false);
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