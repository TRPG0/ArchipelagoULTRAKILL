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

namespace ArchipelagoULTRAKILL
{
    public class UIManager : MonoBehaviour
    {
        public static AssetBundle bundle = AssetBundle.LoadFromMemory(Properties.Resources.trpg_archipelago);

        public static TMP_FontAsset font;
        public static TextMeshProUGUI log;
        public static TextMeshProUGUI logBlack;
        public static int lines = 5;

        public static GameObject canvas;
        public static GameObject chapterSelect;
        public static TextMeshProUGUI actStats;
        public static Dictionary<string, GameObject> chapters = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> layers = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> levels = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> secrets = new Dictionary<string, GameObject>();

        public static TextMeshProUGUI menuText;
        public static GameObject menuIcon;
        public static TextMeshProUGUI goalCount;

        public static bool createdSkullIcons = false;

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
            log.font = font;
            log.fontSize = ConfigManager.logFontSize.value;
            log.alignment = TextAlignmentOptions.BottomGeoAligned;
            log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height - 10);

            logBlack = GameObject.Instantiate(log.gameObject, log.transform.parent).GetComponent<TextMeshProUGUI>();
            logBlack.gameObject.name = "Shadow";
            logBlack.color = new Color(0, 0, 0, 0.7f);
            logBlack.overrideColorTags = true;
            logBlack.transform.localPosition = new Vector3(0, -1.5f, 0);
            logBlack.transform.SetAsFirstSibling();
        }

        public static void SetLogText(string text)
        {
            if (log != null) log.text = text;
            if (logBlack != null) logBlack.text = text;
        }

        public static void AdjustLogBounds()
        {
            if (PrefsManager.Instance.GetInt("hudType") >= 2 && Core.IsPlaying)
            {
                log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, ((float)Math.Round(Screen.height * 0.77f)));
            }
            else log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height - 10);
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

            actStats = GameObject.Instantiate(chapterSelect.transform.Find("Prelude").Find("Name"), chapterSelect.transform).GetComponent<TextMeshProUGUI>();
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

            CreateMenuUI();
        }
        
        public static void CreateMenuUI()
        {
            foreach (TextMeshProUGUI tmp in canvas.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (tmp.text == "-- EARLY ACCESS --")
                {
                    menuText = Instantiate(tmp.gameObject, tmp.gameObject.transform.parent).GetComponent<TextMeshProUGUI>();
                    break;
                }
            }
            menuText.gameObject.name = "Archipelago Text";
            menuText.transform.localPosition = new Vector3(-6, -148, 0);
            menuText.alignment = TextAlignmentOptions.TopRight;
            menuText.fontSize = 24;
            string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
            if (Core.DataExists()) menuText.text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n" + Core.data.@checked.Count + "/" + totalLocations;
            else if (Multiworld.HintMode) menuText.text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n<color=yellow>Hint Mode</color>";
            else menuText.text = "Archipelago\n" + Core.PluginVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\nNo data.";
            font = menuText.font;
            menuIcon = new GameObject();
            menuIcon.gameObject.name = "Archipelago Logo";
            menuIcon.transform.SetParent(menuText.transform.parent.gameObject.transform);
            menuIcon.AddComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/archipelago.png");
            menuIcon.transform.localPosition = new Vector3(600, 40, 0);
            menuIcon.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            if (Core.DataExists() && Core.data.completedLevels.Count < Core.data.goalRequirement) CreateGoalCounter();
        }

        public static void CreateGoalCounter()
        {
            GameObject go = new GameObject();
            go.transform.SetParent(levels[Core.data.goal].transform);
            if (Core.data.goal.Contains("P")) go.transform.localPosition = new Vector3(0, 20, 0);
            else if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) go.transform.localPosition = new Vector3(0, 90, 0);
            else go.transform.localPosition = new Vector3(0, 22.5f, 0);
            go.layer = 5;
            goalCount = go.AddComponent<TextMeshProUGUI>();
            goalCount.text = (Core.data.goalRequirement - Core.data.completedLevels.Count).ToString();
            goalCount.font = font;
            goalCount.fontSize = 100;
            goalCount.alignment = TextAlignmentOptions.Center;
            goalCount.overflowMode = TextOverflowModes.Overflow;

            levels[Core.data.goal].transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
            if (!Multiworld.Authenticated) goalCount.gameObject.SetActive(false);
        }

        public static void UpdateLevels()
        {
            Sprite locked = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/UI/Level Thumbnails/Locked.png").WaitForCompletion();
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
        }

        public static void CreateSkullIcons()
        {
            Sprite sprite = bundle.LoadAsset<Sprite>("assets/skull.png");

            foreach (LevelInfo info in Core.levelInfos)
            {
                if (info.Skulls == SkullsType.Normal)
                {
                    if (info.SkullsList == null) throw new Exception($"Skull list is null for level {info.Name}.");

                    foreach (string skull in info.SkullsList)
                    {
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(levels[info.Name].transform);
                        go.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = sprite;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) go.transform.localPosition = new Vector3(70 - (38 * info.SkullsList.FindIndex(a => a == skull)), 135, 0);
                        else go.transform.localPosition = new Vector3(70 - (38 * info.SkullsList.FindIndex(a => a == skull)), 60, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
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
                        go.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = sprite;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) go.transform.localPosition = new Vector3(70 - (38 * i), 135, 0);
                        else go.transform.localPosition = new Vector3(70 - (38 * i), 60, 0);
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
                        go.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = sprite;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) go.transform.localPosition = new Vector3(70 - (38 * i), 135, 0);
                        else go.transform.localPosition = new Vector3(70 - (38 * i), 60, 0);
                        go.AddComponent<SkullIcon>().SetId(skull);
                    }
                }
            }

            foreach (LevelInfo info in Core.secretMissionInfos)
            {
                if (info.Skulls == SkullsType.Normal)
                {
                    if (info.SkullsList == null) throw new Exception($"Skull list is null for level {info.Name}.");

                    int xPos = 275;
                    if (info.Name == "0-S") xPos = 290;

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
                        go.AddComponent<Image>().sprite = sprite;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        go.transform.localPosition = new Vector3(xPos - (38 * info.SkullsList.FindIndex(a => a == skull)), 10, 0);
                        go.AddComponent<SkullIcon>().SetId(skull, true);
                    }
                }
            }
            createdSkullIcons = true;
        }

        public static void CreateMessageUI()
        {
            GameObject player = GameObject.Find("/Player");
            foreach (HudController component in player.GetComponentsInChildren<HudController>())
            {
                //Core.logger.LogInfo(component.gameObject.name);
                hud = component.gameObject;
            }

            popupCanvas = Instantiate(hud.transform.GetChild(0).gameObject);
            popupCanvas.name = "APCanvas";
            popupCanvas.transform.SetParent(hud.transform);
            Traverse hudT = Traverse.Create(popupCanvas.GetComponent<HUDPos>());
            hudT.Field<Vector3>("defaultPos").Value = new Vector3(1.125f, -0.53f, 1);
            hudT.Field<Vector3>("defaultRot").Value = new Vector3(0, 30, 0);
            popupCanvas.GetComponent<HUDPos>().reversePos = new Vector3(-1.06f, -0.53f, 1);
            popupCanvas.GetComponent<HUDPos>().reverseRot = new Vector3(0, 330, 0);
            popupCanvas.GetComponent<HUDPos>().active = false;
            popupCanvas.GetComponent<HUDPos>().active = true;
            Destroy(popupCanvas.transform.GetChild(1).gameObject);
            Destroy(popupCanvas.transform.GetChild(0).GetChild(1).gameObject);

            for (int i = popupCanvas.transform.GetChild(0).GetChild(0).childCount - 1; i > -1; i--)
            {
                if (popupCanvas.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.name == "FistPanel" && popupCanvas.transform.GetChild(0).GetChild(0).GetChild(i).childCount == 1)
                {
                    popupImage = popupCanvas.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).gameObject;
                }
                else
                {
                    Destroy(popupCanvas.transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
                }
            }

            popupCanvas.transform.GetChild(0).gameObject.SetActive(true);
            popupCanvas.SetActive(false);
            popupImage.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/layer4.png");
            popupImage.GetComponent<Image>().color = Colors.Layer4;
            GameObject go = new GameObject();
            go.name = "APText";
            go.transform.SetParent(popupCanvas.transform.GetChild(0));
            go.layer = 13;
            popupText = go.AddComponent<TextMeshProUGUI>();
            popupText.font = font;
            popupText.alignment = TextAlignmentOptions.Right;
            popupText.enableAutoSizing = true;
            popupText.fontSizeMax = 120;
            popupText.lineSpacing = 1.1f;
            popupText.material = HUDOptions.Instance.hudMaterial;
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
            go.transform.parent = NewMovement.Instance.blackScreen.gameObject.transform;
            go.AddComponent<Canvas>();
            go2.transform.parent = go.transform;
            go2.AddComponent<Text>();
            deathLinkMessage = go2.AddComponent<DeathLinkMessage>();
            deathLinkMessage.Initialize();
        }

        public static Sprite GetLevelThumbnail(string name)
        {
            string file;

            switch (name)
            {
                case "0-1":
                    file = "0-1 Into the Fire";
                    break;
                case "0-2":
                    file = "0-2 The Meatgrinder";
                    break;
                case "0-3":
                    file = "0-3 Double Down";
                    break;
                case "0-4":
                    file = "0-4 A One-Machine Army";
                    break;
                case "0-5":
                    file = "0-5 Cerberus";
                    break;
                case "1-1":
                    file = "1-1 Heart of the Sunrise";
                    break;
                case "1-2":
                    file = "1-2 The Burning World";
                    break;
                case "1-3":
                    file = "1-3 Halls of Sacred Remains";
                    break;
                case "1-4":
                    file = "1-4 Clair de Lune";
                    break;
                case "2-1":
                    file = "2-1 In the Air Tonight";
                    break;
                case "2-2":
                    file = "2-2 Death at 20,000 Volts";
                    break;
                case "2-3":
                    file = "2-3 Sheer Heart Attack";
                    break;
                case "2-4":
                    file = "2-4 Court of the Corpse King";
                    break;
                case "3-1":
                    file = "3-1 Belly of the Beast";
                    break;
                case "3-2":
                    file = "3-2 In the Flesh";
                    break;
                case "4-1":
                    file = "4-1 Slaves to Power";
                    break;
                case "4-2":
                    file = "4-2 God Damn the Sun";
                    break;
                case "4-3":
                    file = "4-3 A Shot in the Dark";
                    break;
                case "4-4":
                    file = "4-4 Clair de Soleil";
                    break;
                case "5-1":
                    file = "5-1 In the Wake of Poseidon";
                    break;
                case "5-2":
                    file = "5-2 Waves of the Starless Sea";
                    break;
                case "5-3":
                    file = "5-3 Ship of Fools";
                    break;
                case "5-4":
                    file = "5-4 Leviathan";
                    break;
                case "6-1":
                    file = "6-1 Cry for the Weeper";
                    break;
                case "6-2":
                    file = "6-2 Aesthetics of Hate";
                    break;
                case "7-1":
                    file = "7-1 Garden of Forking Paths";
                    break;
                case "7-2":
                    file = "7-2 Light Up the Night";
                    break;
                case "7-3":
                    file = "7-3 No Sound No Memory";
                    break;
                case "7-4":
                    file = "7-4 Like Antennas to Heaven";
                    break;
                case "P-1":
                    file = "P-1 Soul Survivor";
                    break;
                case "P-2":
                    file = "P-2 Wait of the World";
                    break;
                default:
                    file = "Locked";
                    break;
            }

            return Addressables.LoadAssetAsync<Sprite>($"Assets/Textures/UI/Level Thumbnails/{file}.png").WaitForCompletion();
        }

        public static Sprite GetLevelThumbnail(int id)
        {
            return GetLevelThumbnail(Core.GetLevelNameFromId(id));
        }

        public static Sprite GetLevelThumbnail(LevelInfo info)
        {
            return GetLevelThumbnail(info.Name);
        }
    }
}