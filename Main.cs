using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMM;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
using BepInEx.Configuration;

namespace ArchipelagoULTRAKILL
{
    [UKPlugin(ModGUID, ModName, ModVersion, ModDescription, false, true)]
    public class APULTRAKILL : UKMod
    {
        public const string ModGUID = "trpg.archipelagoultrakill";
        public const string ModName = "Archipelago";
        public const string ModVersion = "1.0.0";
        public const string ModDescription = "Connect to an Archipelago server to play ULTRAKILL randomizer.";

        public static GameObject obj;

        public static Scene currentScene;
        public static UKAPI.UKLevelType currentLevelType;
        public static bool inLevel;

        public static AssetBundle bundle = AssetBundle.LoadFromMemory(Properties.Resources.trpg_archipelago);

        public static readonly Dictionary<string, Color> colors = new Dictionary<string, Color>()
        {
            ["white"] = new Color(1, 1, 1),
            ["red"] = new Color(1, 0.2353f, 0.2353f),
            ["green"] = new Color(0.2667f, 1, 0.2706f),
            ["lightblue"] = new Color(0.251f, 0.9059f, 1),
            ["yellow"] = new Color(1, 1, 0.25f, 1),
            ["orange"] = new Color(1, 0.5f, 0.25f),
            ["gold"] = new Color(1, 0.65f, 0),
            ["purple"] = new Color(0.765f, 0.25f, 1),
            ["bone"] = new Color(1, 0.9479f, 0.8566f),
            ["ap_item_advancement"] = new Color(0.69f, 0.6f, 0.94f),
            ["ap_item_neverexclude"] = new Color(0.43f, 0.55f, 0.91f),
            ["ap_item_trap"] = new Color(0.98f, 0.5f, 0.45f),
            ["ap_item_filler"] = new Color(0, 0.93f, 0.93f),
            ["ap_player_self"] = new Color(0.93f, 0, 0.93f),
            ["ap_player_other"] = new Color(0.98f, 0.98f, 0.82f),
            ["ap_location"] = new Color(0, 1, 0.5f),
            ["ap_entrance"] = new Color(0.39f, 0.58f, 0.93f)
        };

        public static APData data = new APData();
        public static Font font;

        public static GameObject canvas;
        public static GameObject apMenuText;
        public static GameObject apMenuLogo;
        public static GameObject chapterSelect;
        public static Dictionary<string, GameObject> chapters = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> layers = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> levels = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> secrets = new Dictionary<string, GameObject>();
        public static GameObject goalCount;
        public static Sprite lockedImage;

        public static GameObject player;
        public static GameObject hud;
        public static GameObject apCanvas;
        public static GameObject apGameText;
        public static GameObject apGameImg;
        public static GameObject shop;
        public static Dictionary<string, GameObject> shopPanels = new Dictionary<string, GameObject>();

        public static readonly List<string> allLevels = new List<string>()
        {
            "0-2",
            "0-3",
            "0-4",
            "0-5",
            "1-1",
            "1-2",
            "1-3",
            "1-4",
            "2-1",
            "2-2",
            "2-3",
            "2-4",
            "3-1",
            "3-2",
            "4-1",
            "4-2",
            "4-3",
            "4-4",
            "5-1",
            "5-2",
            "5-3",
            "5-4",
            "6-1",
            "6-2",
            "P-1",
            "P-2"
        };

        public static readonly List<string> allSecrets = new List<string>()
        {
            "0-S",
            "1-S",
            "2-S",
            "4-S",
            "5-S"
        };

        public static readonly Dictionary<int, string> idToLevel = new Dictionary<int, string>()
        {
            [1] = "0-1",
            [2] = "0-2",
            [3] = "0-3",
            [4] = "0-4",
            [5] = "0-5",
            [6] = "1-1",
            [7] = "1-2",
            [8] = "1-3",
            [9] = "1-4",
            [10] = "2-1",
            [11] = "2-2",
            [12] = "2-3",
            [13] = "2-4",
            [14] = "3-1",
            [15] = "3-2",
            [16] = "4-1",
            [17] = "4-2",
            [18] = "4-3",
            [19] = "4-4",
            [20] = "5-1",
            [21] = "5-2",
            [22] = "5-3",
            [23] = "5-4",
            [24] = "6-1",
            [25] = "6-2",
            [666] = "P-1",
            [667] = "P-2"
        };

        public static readonly Dictionary<string, int> shopPrices = new Dictionary<string, int>()
        {
            ["rev2"] = 7500,
            ["sho1"] = 12500,
            ["nai1"] = 25000,
            ["rai1"] = 100000,
            ["rai2"] = 100000,
            ["rock1"] = 75000
        };

        public override void OnModLoaded()
        {
            Harmony harmony = new Harmony("archipelago");
            harmony.PatchAll();
            obj = FindObjectOfType<APULTRAKILL>().gameObject;
            obj.name = "Archipelago";
            SceneManager.sceneLoaded += OnSceneLoaded;
            UKAPI.OnLevelChanged += ChangedLevel;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //if (UKAPI.CurrentLevelType == UKAPI.UKLevelType.MainMenu) Invoke("FindMenuObjects", 0.2f);
            currentScene = scene;
            currentLevelType = UKAPI.GetUKLevelType(scene.name);

            inLevel = !(currentLevelType == UKAPI.UKLevelType.MainMenu || currentLevelType == UKAPI.UKLevelType.Intro || currentLevelType == UKAPI.UKLevelType.Intermission || currentLevelType == UKAPI.UKLevelType.Unknown);

            Debug.Log("SceneManager.sceneLoaded: " + currentLevelType);
            ChangedLevel(UKAPI.GetUKLevelType(scene.name));
        }

        public static void ChangedLevel(UKAPI.UKLevelType levelType)
        {
            Debug.Log("UKAPI.OnLevelChanged: " + UKAPI.CurrentLevelType.ToString());
            levels.Clear();
            secrets.Clear();
            data.playerActivated = false;
            if (levelType == UKAPI.UKLevelType.MainMenu)
            {
                FindMenuObjects();
            }
            else if (inLevel)
            {
                CreateMessageUI();
                FindShopObjects();
            }
        }

        public static void FindMenuObjects()
        {
            canvas = GameObject.Find("/Canvas");

            foreach (ChapterSelectButton component in canvas.GetComponentsInChildren<ChapterSelectButton>(true))
            {
                //Debug.Log(component.gameObject.name);
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
                //Debug.Log(component.gameObject.transform.parent.name + ", " + component.gameObject.name);
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
                        switch (component.gameObject.transform.parent.name)
                        {
                            case "Layer 1 Limbo":
                                if (!levels.ContainsKey("1-1")) levels["1-1"] = component.gameObject;
                                break;
                            case "Layer 2 Lust":
                                levels["2-1"] = component.gameObject;
                                break;
                            case "Layer 3 Gluttony":
                                levels["3-1"] = component.gameObject;
                                break;
                            case "Layer 4 Greed":
                                levels["4-1"] = component.gameObject;
                                break;
                            case "Layer 5 Wrath":
                                levels["5-1"] = component.gameObject;
                                break;
                            case "Layer 6 Heresy":
                                levels["6-1"] = component.gameObject;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "1-2 Panel":
                        switch (component.gameObject.transform.parent.name)
                        {
                            case "Layer 1 Limbo":
                                if (!levels.ContainsKey("1-2")) levels["1-2"] = component.gameObject;
                                break;
                            case "Layer 2 Lust":
                                levels["2-2"] = component.gameObject;
                                break;
                            case "Layer 3 Gluttony":
                                levels["3-2"] = component.gameObject;
                                break;
                            case "Layer 4 Greed":
                                levels["4-2"] = component.gameObject;
                                break;
                            case "Layer 5 Wrath":
                                levels["5-2"] = component.gameObject;
                                break;
                            case "Layer 6 Heresy":
                                levels["6-2"] = component.gameObject;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "1-3 Panel":
                        switch (component.gameObject.transform.parent.name)
                        {
                            case "Layer 1 Limbo":
                                if (!levels.ContainsKey("1-3")) levels["1-3"] = component.gameObject;
                                break;
                            case "Layer 2 Lust":
                                levels["2-3"] = component.gameObject;
                                break;
                            case "Layer 4 Greed":
                                levels["4-3"] = component.gameObject;
                                break;
                            case "Layer 5 Wrath":
                                levels["5-3"] = component.gameObject;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "1-4 Panel":
                        switch (component.gameObject.transform.parent.name)
                        {
                            case "Layer 1 Limbo":
                                if (!levels.ContainsKey("1-4")) levels["1-4"] = component.gameObject;
                                break;
                            case "Layer 2 Lust":
                                levels["2-4"] = component.gameObject;
                                break;
                            case "Layer 4 Greed":
                                levels["4-4"] = component.gameObject;
                                break;
                            case "Layer 5 Wrath":
                                levels["5-4"] = component.gameObject;
                                break;
                            default:
                                break;
                        }
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
                //Debug.Log(component.gameObject.transform.parent.name + ", " + component.gameObject.name);
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
                    apMenuText = Instantiate(component.gameObject, component.gameObject.transform.parent);
                }
            }
            apMenuText.gameObject.name = "Archipelago Text";
            apMenuText.transform.localPosition = new Vector3(-6, -148, 0);
            apMenuText.GetComponent<Text>().alignment = TextAnchor.UpperRight;
            apMenuText.GetComponent<Text>().fontSize = 24;
            apMenuText.GetComponent<Text>().text = "Archipelago\npre" + ModVersion + "\nSlot " + (GameProgressSaver.currentSlot + 1) + "\nNo data.";
            font = apMenuText.GetComponent<Text>().font;
            apMenuLogo = new GameObject();
            apMenuLogo.gameObject.name = "Archipelago Logo";
            apMenuLogo.transform.SetParent(apMenuText.transform.parent.gameObject.transform);
            apMenuLogo.AddComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/archipelago.png");
            apMenuLogo.transform.localPosition = new Vector3(600, 40, 0);
            apMenuLogo.transform.localScale = new Vector3(0.7f, 0.7f, 1);

            if ((data.goalRequirement - data.completedLevels.Count) > 0)
            {
                goalCount = new GameObject();
                goalCount.transform.SetParent(levels[data.goal].transform);
                goalCount.transform.localPosition = new Vector3(0, 22.5f, 0);
                goalCount.layer = 5;
                goalCount.AddComponent<Text>().text = (data.goalRequirement - data.completedLevels.Count).ToString();
                goalCount.GetComponent<Text>().font = font;
                goalCount.GetComponent<Text>().fontSize = 100;
                goalCount.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                goalCount.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;

                if (data.active) levels[data.goal].transform.GetChild(2).gameObject.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
                else goalCount.SetActive(false);
            }
        }

        public static void CreateMessageUI()
        {
            player = GameObject.Find("/Player");
            foreach (HudController component in player.GetComponentsInChildren<HudController>())
            {
                //Debug.Log(component.gameObject.name);
                hud = component.gameObject;
            }

            apCanvas = Instantiate(hud.transform.GetChild(0).gameObject);
            apCanvas.name = "APCanvas";
            apCanvas.transform.SetParent(hud.transform);
            Traverse hudT = Traverse.Create(apCanvas.GetComponent<HUDPos>());
            hudT.Field<Vector3>("defaultPos").Value = new Vector3(1.125f, -0.53f, 1);
            hudT.Field<Vector3>("defaultRot").Value = new Vector3(0, 30, 0);
            apCanvas.GetComponent<HUDPos>().reversePos = new Vector3(-1.06f, -0.53f, 1);
            apCanvas.GetComponent<HUDPos>().reverseRot = new Vector3(0, 330, 0);
            apCanvas.GetComponent<HUDPos>().active = false;
            apCanvas.GetComponent<HUDPos>().active = true;
            Destroy(apCanvas.transform.GetChild(1).gameObject);
            Destroy(apCanvas.transform.GetChild(0).GetChild(1).gameObject);

            for (int i = apCanvas.transform.GetChild(0).GetChild(0).childCount - 1; i > -1; i--)
            {
                if (apCanvas.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.name == "FistPanel" && apCanvas.transform.GetChild(0).GetChild(0).GetChild(i).childCount == 1)
                {
                    apGameImg = apCanvas.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).gameObject;
                }
                else
                {
                    Destroy(apCanvas.transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
                }
            }

            apCanvas.transform.GetChild(0).gameObject.SetActive(true);
            apCanvas.SetActive(false);
            apGameImg.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/layer4.png");
            apGameImg.GetComponent<Image>().color = colors["yellow"];
            apGameText = new GameObject();
            apGameText.name = "APText";
            apGameText.transform.SetParent(apCanvas.transform.GetChild(0));
            apGameText.layer = 13;
            apGameText.AddComponent<Text>().font = font;
            apGameText.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            apGameText.GetComponent<Text>().resizeTextForBestFit = true;
            apGameText.GetComponent<Text>().resizeTextMaxSize = 120;
            apGameText.GetComponent<Text>().lineSpacing = 1.1f;
            apGameText.GetComponent<Text>().material = HUDOptions.Instance.hudMaterial;
            apGameText.GetComponent<Text>().text = "UNLOCKED: <color=#ffe800ff>4-2 GOD DAMN THE SUN</color> (<color=#fafad2ff>Trev</color>)";
            apGameText.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 150);
            apGameText.transform.localPosition = new Vector3(100.6437f, 23.9347f, 0);
            apGameText.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            apGameText.transform.localRotation = new Quaternion();
            // fix HUDOptions masks
        }

        public static void FindShopObjects()
        {
            foreach (ShopZone sz in Resources.FindObjectsOfTypeAll(typeof(ShopZone)) as ShopZone[])
            {
                if (sz.gameObject.name == "Shop") shop = sz.gameObject;
            }
            foreach (VariationInfo vi in shop.GetComponentsInChildren<VariationInfo>(true))
            {
                shopPanels[vi.weaponName] = vi.gameObject;
            }
        }

        public static void UpdateShopObjects()
        {
            GameProgressMoneyAndGear generalProgress = GameProgressSaver.GetGeneralProgress();
            for (int i = 0; i < shopPanels.Count; i++)
            {
                KeyValuePair<string, GameObject> pair = shopPanels.ElementAt(i);

                if (pair.Key.Contains("arm")) continue;
                else
                {
                    FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(pair.Key, BindingFlags.Instance | BindingFlags.Public);
                    VariationInfo vi = pair.Value.GetComponent<VariationInfo>();

                    Debug.Log(pair.Key + ": " + field.GetValue(generalProgress));
                    if (int.Parse(field.GetValue(generalProgress).ToString()) != 0 && !pair.Key.Contains("0") && !data.purchasedItems.Contains(pair.Key))
                    {
                        bool canAfford = false;
                        string cost;
                        if (GameProgressSaver.GetMoney() >= shopPrices[pair.Key]) canAfford = true;
                        if (canAfford) cost = MoneyText.DivideMoney(shopPrices[pair.Key]) + "<color=orange>P</color>";
                        else cost = "<color=red>" + MoneyText.DivideMoney(shopPrices[pair.Key]) + "P</color>";

                        vi.costText.text = cost;
                        vi.equipButton.transform.GetChild(0).GetComponent<Image>().sprite = vi.equipSprites[PrefsManager.Instance.GetInt("weapon." + pair.Key, 1)];
                        vi.orderButtons.SetActive(true);
                        Traverse.Create(vi).Field<int>("equipStatus").Value = PrefsManager.Instance.GetInt("weapon." + pair.Key, 1);
                        Traverse.Create(vi).Field<int>("money").Value = GameProgressSaver.GetMoney();
                        vi.moneyText.text = MoneyText.DivideMoney(GameProgressSaver.GetMoney()) + "<color=orange>P</color>";

                        vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = cost;
                        if (canAfford) 
                        {
                            vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1);
                            vi.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1); 
                        }
                        else vi.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
                    }
                    else if (int.Parse(field.GetValue(generalProgress).ToString()) == 0 && !pair.Key.Contains("0") && data.purchasedItems.Contains(pair.Key))
                    {
                        vi.costText.text = "ALREADY OWNED";
                        vi.buyButton.deactivated = true;
                        vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "ALREADY OWNED";
                        vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                    }
                }
            }
        }

        public void DisplayMessage(string image, string message)
        {
            if (currentLevelType == UKAPI.UKLevelType.Level || currentLevelType == UKAPI.UKLevelType.Endless || currentLevelType == UKAPI.UKLevelType.Sandbox)
            {
                apGameText.GetComponent<Text>().text = message;

                string color = GetImageColor(image);
                if (color == "?") color = "white";
                apGameImg.GetComponent<Image>().color = colors[color];

                if (image != "revalt" && image != "naialt")
                {
                    if (image.Contains("rev") || image.Contains("sho") || image.Contains("nai") || image.Contains("rai")) image = image.Remove(3, 1);
                    else if (image.Contains("rock")) image = image.Remove(4, 1);
                }

                if (bundle.Contains("assets/" + image + ".png")) apGameImg.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/" + image + ".png");
                else apGameImg.GetComponent<Image>().sprite = bundle.LoadAsset<Sprite>("assets/archipelago.png");

                apCanvas.SetActive(true);
                Invoke("HideMessage", 3f);
            }
            else
            {
                Debug.Log("Can't do DisplayMessage() right now.");
            }
        }

        public static string GetImageColor(string image)
        {
            switch (image)
            {
                case "layer0": 
                    return "orange";
                case "rev2":
                case "sho1":
                case "nai1":
                case "rai1":
                case "rock1":
                case "layer1": 
                    return "green";
                case "layer2": 
                    return "purple";
                case "layer3": 
                    return "bone";
                case "layer4": 
                    return "yellow";
                case "rev0":
                case "sho0":
                case "nai0":
                case "rai0":
                case "rock0":
                case "dash":
                case "walljump":
                case "slide":
                case "slam":
                case "layer5": 
                    return "lightblue";
                case "rai2":
                case "layer6": 
                    return "red";
                case "revalt":
                case "naialt":
                    return "gold";
                default: return "?";
            }
        }

        public void HideMessage()
        {
            apCanvas.SetActive(false);
        }

        public static void GetLockedImage()
        {
            Image[] imageComponents = levels["P-3"].GetComponentsInChildren<Image>();
            foreach (Image component in imageComponents)
            {
                if (component.gameObject.name == "Image") lockedImage = component.sprite;
            }
        }

        public static void UpdateLevels()
        {
            GetLockedImage();
            foreach (string level in allLevels)
            {
                if (!data.unlockedLevels.Contains(level))
                {
                    foreach (Image component in levels[level].GetComponentsInChildren<Image>())
                    {
                        if (component.gameObject.name == "Image") component.sprite = lockedImage;
                    }
                    levels[level].GetComponent<Button>().enabled = false;
                }
            }
        }

        void Update()
        {
            if (inLevel)
            {
                if (data.walljumpTrap)
                {
                    if (data.walljumps > 0 && NewMovement.Instance.currentWallJumps < (3 - (data.walljumps - 1)))
                        NewMovement.Instance.currentWallJumps = (3 - (data.walljumps - 1));
                    else if (data.walljumps == 0) NewMovement.Instance.currentWallJumps = 3;
                }
                else
                {
                    if (data.walljumps < 3 && NewMovement.Instance.currentWallJumps < (3 - data.walljumps))
                        NewMovement.Instance.currentWallJumps = 3 - data.walljumps;
                }

                if (data.staminaPowerup)
                {
                    NewMovement.Instance.boostCharge = 300;
                }
                else if (data.dashTrap)
                {
                    if (data.dashes > 0 && NewMovement.Instance.boostCharge > ((data.dashes - 1) * 100))
                        NewMovement.Instance.boostCharge = (data.dashes * 100) - 100;
                    else if (data.dashes == 0) NewMovement.Instance.boostCharge = 0;
                }
                else
                {
                    if (data.dashes < 3 && NewMovement.Instance.boostCharge > (data.dashes * 100))
                        NewMovement.Instance.boostCharge = data.dashes * 100;
                }

                //if (!data.canSlam && data.playerActivated && NewMovement.Instance.GetFieldValue<float>("fallTime", true) > 0.3)
                if (!data.canSlam && data.playerActivated && Traverse.Create(NewMovement.Instance).Field<float>("fallTime").Value > 0.3)
                {
                    Traverse.Create(NewMovement.Instance).Field<float>("fallTime").Value = 0.3f;
                }

                if (data.lockFire2 && !data.unlockedFire2.Contains("rev0") && GunControl.Instance.currentSlot == 1 && GunControl.Instance.currentVariation == 0 && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Revolver>()).Field<bool>("pierceReady").Value = false;
                    GunControl.Instance.currentWeapon.GetComponent<Revolver>().pierceCharge = 0;
                }

                if (data.lockFire2 && !data.unlockedFire2.Contains("rev2") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.rev1charge = 0;
                }

                if (data.lockFire2 && !data.unlockedFire2.Contains("sho0") && GunControl.Instance.currentSlot == 2 && GunControl.Instance.currentVariation == 0 && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    Traverse shotgun = Traverse.Create(GunControl.Instance.currentWeapon.GetComponent<Shotgun>());
                    shotgun.Field<bool>("charging").Value = false;
                    shotgun.Field<float>("grenadeForce").Value = 0;
                }

                if (data.lockFire2 && !data.unlockedFire2.Contains("rai0") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.naiMagnetCharge = 0;
                }

                if (data.lockFire2 && !data.unlockedFire2.Contains("rai1") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.naiHeatsinks = 0;
                    WeaponCharges.Instance.naiSawHeatsinks = 0;
                }

                if (data.lockFire2 && !data.unlockedFire2.Contains("rock1") && !CheatsManager.Instance.GetCheatState("ultrakill.no-weapon-cooldown"))
                {
                    WeaponCharges.Instance.rocketCannonballCharge = 0;
                }
            }
        }
    }
}
