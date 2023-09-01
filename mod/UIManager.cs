using ArchipelagoULTRAKILL.Structures;
using ArchipelagoULTRAKILL.Components;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL
{
    public class UIManager : MonoBehaviour
    {
        public static AssetBundle bundle = AssetBundle.LoadFromMemory(Properties.Resources.trpg_archipelago);

        public static Font font;
        public static GameObject log;
        public static int lines = 5;

        public static GameObject canvas;
        public static GameObject chapterSelect;
        public static Dictionary<string, GameObject> chapters = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> layers = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> levels = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> secrets = new Dictionary<string, GameObject>();

        public static GameObject menuText;
        public static GameObject menuIcon;
        public static GameObject goalCount;

        public static readonly List<string> skullLevels = new List<string>()
        {
            "Level 0-2",
            "Level 0-S",
            "Level 1-1",
            "Level 1-2",
            "Level 1-3",
            "Level 1-4",
            "Level 2-3",
            "Level 2-4",
            "Level 4-2",
            "Level 4-3",
            "Level 4-4",
            "Level 5-1",
            "Level 5-2",
            "Level 5-3",
            "Level 6-1",
        };

        public static Dictionary<string, List<string>> skullsInLevel = new Dictionary<string, List<string>>();
        public static List<GameObject> skullIcons = new List<GameObject>();

        public static GameObject hud;
        public static GameObject popupCanvas;
        public static GameObject popupText;
        public static GameObject popupImage;
        public static bool displayingMessage = false;

        public DeathLinkMessage deathLinkMessage = null;

        public static void CreateLogObject()
        {
            log = new GameObject();
            log.name = "Message Log";
            log.transform.parent = Core.obj.transform;
            log.transform.localPosition = new Vector3(0, 0, 0);

            Core.obj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            Core.obj.GetComponent<Canvas>().sortingOrder = 256;

            log.AddComponent<Text>().font = font;
            log.GetComponent<Text>().fontSize = ConfigManager.logFontSize.value;
            log.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
            log.GetComponent<Text>().alignByGeometry = true;
            log.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
            log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height - 10);
        }

        public static void AdjustLogBounds()
        {
            if (PrefsManager.Instance.GetInt("hudType") >= 2 && !Core.inLevel)
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
                    case "Prime":
                        chapters["prime"] = component.gameObject;
                        break;
                    default:
                        break;
                }
            }

            chapterSelect = chapters["prelude"].gameObject.transform.parent.gameObject;
            chapterSelect.AddComponent<ChapterSelectState>();

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
                switch (component.gameObject.transform.parent.name)
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
                    default:
                        break;
                }
            }

            CreateMenuUI();
        }
        
        public static void CreateMenuUI()
        {
            foreach (Text component in canvas.GetComponentsInChildren<Text>())
            {
                if (component.text == "-- EARLY ACCESS --")
                {
                    menuText = Instantiate(component.gameObject, component.gameObject.transform.parent);
                }
            }
            menuText.gameObject.name = "Archipelago Text";
            menuText.transform.localPosition = new Vector3(-6, -148, 0);
            menuText.GetComponent<Text>().alignment = TextAnchor.UpperRight;
            menuText.GetComponent<Text>().fontSize = 24;
            string totalLocations = (LocationManager.locations.Count == 0) ? "?" : LocationManager.locations.Count.ToString();
            if (Core.DataExists()) menuText.GetComponent<Text>().text = "Archipelago\n" + Core.ModVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\n" + Core.data.@checked.Count + "/" + totalLocations;
            else menuText.GetComponent<Text>().text = "Archipelago\n" + Core.ModVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\nNo data.";
            font = menuText.GetComponent<Text>().font;
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
            goalCount = new GameObject();
            goalCount.transform.SetParent(levels[Core.data.goal].transform);
            if (Core.data.goal.Contains("P")) goalCount.transform.localPosition = new Vector3(0, 20, 0);
            else if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) goalCount.transform.localPosition = new Vector3(0, 90, 0);
            else goalCount.transform.localPosition = new Vector3(0, 22.5f, 0);
            goalCount.layer = 5;
            goalCount.AddComponent<Text>().text = (Core.data.goalRequirement - Core.data.completedLevels.Count).ToString();
            goalCount.GetComponent<Text>().font = font;
            goalCount.GetComponent<Text>().fontSize = 100;
            goalCount.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            goalCount.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;

            levels[Core.data.goal].transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
            if (!Multiworld.Authenticated) goalCount.SetActive(false);
        }

        public static void UpdateLevels()
        {
            Sprite locked = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/UI/Level Thumbnails/Locked.png").WaitForCompletion();
            foreach (string level in Core.allLevels)
            {
                if ((!Core.data.unlockedLevels.Contains(level) && level != Core.data.goal) || (level == Core.data.goal && Core.data.completedLevels.Count < Core.data.goalRequirement))
                {
                    foreach (Image component in levels[level].GetComponentsInChildren<Image>())
                    {
                        if (component.gameObject.name == "Image") component.sprite = locked;
                    }
                    levels[level].GetComponent<Button>().interactable = false;
                }
            }
        }

        public static void CreateSkullIcons()
        {
            Sprite sprite = bundle.LoadAsset<Sprite>("assets/skull.png");
            foreach (string level in Core.allLevels)
            {
                if (skullsInLevel.ContainsKey(level))
                {
                    foreach (string skull in skullsInLevel[level])
                    {
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(levels[level].transform);
                        go.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = sprite;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) go.transform.localPosition = new Vector3(70 - (38 * skullsInLevel[level].FindIndex(a => a == skull)), 135, 0);
                        else go.transform.localPosition = new Vector3(70 - (38 * skullsInLevel[level].FindIndex(a => a == skull)), 60, 0);
                        skullIcons.Add(go);
                    }
                }
                else if (level == "1-4")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        string skull = "9_b" + (i + 1);
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(levels[level].transform);
                        go.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = sprite;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) go.transform.localPosition = new Vector3(70 - (38 * i), 135, 0);
                        else go.transform.localPosition = new Vector3(70 - (38 * i), 60, 0);

                        skullIcons.Add(go);
                    }
                }
                else if (level == "5-1")
                {
                    for (int i = 0; i < 3; i++)
                    {
                        string skull = "20_b" + (i + 1);
                        GameObject go = new GameObject();
                        go.name = skull;
                        go.transform.SetParent(levels[level].transform);
                        go.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                        go.layer = 5;
                        go.AddComponent<Image>().sprite = sprite;
                        go.AddComponent<Shadow>().effectDistance = new Vector2(2, -2);
                        if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) go.transform.localPosition = new Vector3(70 - (38 * i), 135, 0);
                        else go.transform.localPosition = new Vector3(70 - (38 * i), 60, 0);
                        skullIcons.Add(go);
                    }
                }
            }
        }

        public static void UpdateSkullIcons()
        {
            foreach (GameObject skull in skullIcons)
            {
                if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) skull.transform.localPosition = new Vector3(skull.transform.localPosition.x, 135, skull.transform.localPosition.z);
                else skull.transform.localPosition = new Vector3(skull.transform.localPosition.x, 60, skull.transform.localPosition.z);

                if (skull.name.Contains("9") && !skull.name.Contains("19"))
                {
                    if (Core.data.unlockedSkulls1_4 >= int.Parse(skull.name.Substring(skull.name.Length - 1, 1))) skull.GetComponent<Image>().color = ConfigManager.blueSkullColor.value;
                    else skull.GetComponent<Image>().color = LocationManager.colors["gray"];
                }
                else if (skull.name.Contains("20"))
                {
                    if (Core.data.unlockedSkulls5_1 >= int.Parse(skull.name.Substring(skull.name.Length - 1, 1))) skull.GetComponent<Image>().color = ConfigManager.blueSkullColor.value;
                    else skull.GetComponent<Image>().color = LocationManager.colors["gray"];
                }
                else
                {
                    if (Core.data.unlockedSkulls.Contains(skull.name))
                    {
                        if (skull.name.Contains("_b")) skull.GetComponent<Image>().color = ConfigManager.blueSkullColor.value;
                        else if (skull.name.Contains("_r")) skull.GetComponent<Image>().color = ConfigManager.redSkullColor.value;
                    }
                    else
                    {
                        skull.GetComponent<Image>().color = LocationManager.colors["gray"];
                    }
                }
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
            popupImage.GetComponent<Image>().color = ConfigManager.layer4Color.value;
            popupText = new GameObject();
            popupText.name = "APText";
            popupText.transform.SetParent(popupCanvas.transform.GetChild(0));
            popupText.layer = 13;
            popupText.AddComponent<Text>().font = font;
            popupText.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            popupText.GetComponent<Text>().resizeTextForBestFit = true;
            popupText.GetComponent<Text>().resizeTextMaxSize = 120;
            popupText.GetComponent<Text>().lineSpacing = 1.1f;
            popupText.GetComponent<Text>().material = HUDOptions.Instance.hudMaterial;
            popupText.GetComponent<Text>().text = "UNLOCKED: <color=#ffe800ff>4-2 GOD DAMN THE SUN</color> (<color=#fafad2ff>Trev</color>)";
            popupText.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 150);
            popupText.transform.localPosition = new Vector3(100.6437f, 23.9347f, 0);
            popupText.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            popupText.transform.localRotation = new Quaternion();
            // fix HUDOptions masks
        }

        public IEnumerator DisplayMessage()
        {
            displayingMessage = true;
            Message message = LocationManager.messages[0];
            LocationManager.messages.RemoveAt(0);

            popupText.GetComponent<Text>().text = message.message;
            popupImage.GetComponent<Image>().color = message.color;

            if (bundle.Contains("assets/" + message.image + ".png")) popupImage.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/" + message.image + ".png");
            else popupImage.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/archipelago.png");

            popupCanvas.SetActive(true);
            yield return new WaitForSeconds(3f);
            popupCanvas.SetActive(false);
            if (LocationManager.messages.Count > 0 && Core.inLevel) StartCoroutine(DisplayMessage());
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
    }
}