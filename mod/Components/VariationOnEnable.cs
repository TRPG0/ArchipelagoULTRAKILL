using Archipelago.MultiClient.Net.Enums;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Components
{
    public class VariationOnEnable : MonoBehaviour
    {
        public VariationInfo variationInfo;
        public TextMeshProUGUI text;
        public Image itemIcon;

        public void Awake()
        {
            if (TryGetComponent<VariationInfo>(out var vi))
            {
                variationInfo = vi;

                if (!(vi.weaponName.Contains("0") || vi.weaponName.Contains("arm")))
                {
                    if (Multiworld.Authenticated) vi.varPage.AddComponent<ShopAutoHint>().id = LocationManager.locations[$"shop_{vi.weaponName}"];

                    Transform iconInset = vi.varPage.transform.Find("Panel").Find("Icon Inset");
                    GameObject textObject = new GameObject() { name = "Text" };
                    textObject.transform.SetParent(iconInset);
                    textObject.transform.localScale = Vector3.one;
                    text = textObject.AddComponent<TextMeshProUGUI>();
                    text.font = UIManager.fontMain;
                    text.fontSize = 72;
                    text.text = "->";
                    GameObject itemObject = new GameObject() { name = "Item" };
                    itemObject.transform.SetParent(iconInset);
                    itemObject.transform.localScale = Vector3.one;
                    itemIcon = itemObject.AddComponent<Image>();
                    iconInset.Find("Icon").transform.localPosition = new Vector3(125, 55, 0);
                }
            }
            else
            {
                Core.Logger.LogWarning($"No variation info found for object \"{gameObject.name}\"");
                DestroyImmediate(this);
            }
        }

        public void OnEnable()
        {
            if (text)
            {
                text.transform.localPosition = new Vector3(75, -75, 0);
                text.transform.localRotation = Quaternion.identity;
            }
            if (itemIcon)
            {
                itemIcon.transform.localPosition = new Vector3(185, -60, 0);
                itemIcon.transform.localRotation = Quaternion.identity;
            }
            StartCoroutine(DelayedUpdate());
        }

        public void UpdateShopVariation(VariationInfo variation)
        {
            string red = "#FF4343";

            // Set Feedbacker text to unavailable if not owned
            if (variation.weaponName == "arm0" && !Core.data.hasArm)
            {
                variation.costText.text = "<color=" + red + ">Unavailable</color>";
                variation.buyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = "Unavailable";
                variation.buyButton.gameObject.GetComponent<Button>().interactable = false;
                variation.equipButtons.SetActive(false);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is not unlocked");
                return;
            }
            // Set Feedbacker text back to default if owned
            else if (variation.weaponName == "arm0" && Core.data.hasArm)
            {
                variation.costText.text = "Already Owned";
                variation.buyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = "Already Owned";
                variation.equipButtons.SetActive(true);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is unlocked");
                return;
            }
            // Do nothing for other arms
            if (variation.weaponName == "arm1" || variation.weaponName == "arm2" || variation.weaponName == "arm3")
            {
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Skipping");
                return;
            }

            // Set shop item description (Skip blue variations)
            if (!variation.weaponName.Contains("0"))
            {
                string description = "[Purchase to unlock: ";
                string itemSprite = "archipelago";
                Color itemColor = Colors.White;

                if (LocationManager.locations.ContainsKey("shop_" + variation.weaponName))
                {
                    if (LocationManager.shopScouts["shop_" + variation.weaponName] is UKItem ukitem)
                    {
                        description += "<color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.GetItemDefinition(ukitem.itemName).Color.Invoke()) + "FF>" + ukitem.itemName + "</color>";
                        if (ukitem.playerName != Core.data.slot_name) description += " for <color=#" + ColorUtility.ToHtmlStringRGB(Colors.PlayerOther) + "FF>" + ukitem.playerName + "</color>";
                        description += "]\n\n";
                        itemSprite = LocationManager.GetItemDefinition(ukitem.itemName).Image;
                        if (ukitem.playerName == Core.data.slot_name) itemColor = LocationManager.GetItemDefinition(ukitem.itemName).Color.Invoke();
                        else itemColor = Colors.Gray;
                    }
                    else if (LocationManager.shopScouts["shop_" + variation.weaponName] is APItem apitem)
                    {
                        description += "<color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.GetAPMessageColor(apitem.type)) + "FF>" + apitem.itemName + "</color>";
                        description += " for <color=#" + ColorUtility.ToHtmlStringRGB(Colors.PlayerOther) + "FF>" + apitem.playerName + "</color>";
                        description += "]\n\n";

                        itemSprite = LocationManager.GetAPImage(apitem.type);
                        itemColor = LocationManager.GetAPMessageColor(apitem.type);

                        if (apitem.type.HasFlag(ItemFlags.Advancement)) description += "You don't know what this is, but it seems <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemAdvancement) + "FF>important.</color>";
                        else if (apitem.type.HasFlag(ItemFlags.NeverExclude)) description += "You don't know what this is, but it seems like it could be <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemNeverExclude) + "FF>useful.</color>";
                        else if (apitem.type.HasFlag(ItemFlags.Trap)) description += "You don't know what this is, but it seems like they're probably <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemTrap) + "FF>better off without it.</color>";
                        else description += "You don't know what this is, but it seems like you could probably <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemFiller) + "FF>skip this</color> if you wanted to.";
                    }
                }
                else
                {
                    description = $"[<color=#{ColorUtility.ToHtmlStringRGBA(Colors.Gray)}>OFFLINE</color>]";
                    text.text = $"-> <color=#{ColorUtility.ToHtmlStringRGBA(Colors.Gray)}>?</color>";
                    itemIcon.gameObject.SetActive(false);
                }

                variation.varPage.transform.Find("Panel").Find("Description").GetComponent<TextMeshProUGUI>().text = description;
                itemIcon.sprite = UIManager.bundle.LoadAsset<Sprite>($"assets/{itemSprite}.png");
                itemIcon.color = itemColor;
            }

            GameProgressMoneyAndGear generalProgress = GameProgressSaver.GetGeneralProgress();
            FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(variation.weaponName, BindingFlags.Instance | BindingFlags.Public);
            bool unlocked = int.Parse(field.GetValue(generalProgress).ToString()) == 1;

            // Weapon is unlocked, weapon is not blue variation, weapon has not been purchased
            if (unlocked && !variation.weaponName.Contains("0") && !Core.data.purchasedItems.Contains(variation.weaponName))
            {
                bool canAfford = GameProgressSaver.GetMoney() >= Core.shopPrices[variation.weaponName];
                string cost = "<color=red>" + MoneyText.DivideMoney(Core.shopPrices[variation.weaponName]) + " P</color>";
                if (canAfford) cost = MoneyText.DivideMoney(Core.shopPrices[variation.weaponName]) + " <color=" + red + ">P</color>";
                variation.costText.text = cost;

                //variation.equipButton.transform.GetChild(0).GetComponent<Image>().sprite = variation.equipSprites[PrefsManager.Instance.GetInt("weapon." + variation.weaponName, 1)];
                Traverse variationT = Traverse.Create(variation);
                int equipStatus = PrefsManager.Instance.GetInt("weapon." + variation.weaponName, 1);
                variationT.Field<int>("equipStatus").Value = equipStatus;
                if (equipStatus != 0)
                {
                    variation.orderButtons.SetActive(true);
                    variation.icon.rectTransform.anchoredPosition = new Vector2(25, 0);
                    variation.icon.rectTransform.sizeDelta = new Vector2(75, 75);
                }
                else
                {
                    variation.orderButtons.SetActive(false);
                    variation.icon.rectTransform.anchoredPosition = new Vector2(0, 0);
                    variation.icon.rectTransform.sizeDelta = new Vector2(100, 100);
                }
                variationT.Method("SetEquipStatusText", new object[] { equipStatus }).GetValue();
                variationT.Field<int>("money").Value = GameProgressSaver.GetMoney();

                if (!AssistController.Instance.cheatsEnabled) variation.buyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = cost;
                else variation.buyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = "Cheats enabled!";

                if (canAfford && !AssistController.Instance.cheatsEnabled)
                {
                    variation.buyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).color = new Color(1, 1, 1);
                    variation.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                    variation.buyButton.gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    variation.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
                    variation.buyButton.gameObject.GetComponent<Button>().interactable = false;
                }
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is unlocked, is not purchased");
            }
            // Weapon is not unlocked, weapon is not blue variation, weapon has been purchased
            else if (!unlocked && !variation.weaponName.Contains("0") && Core.data.purchasedItems.Contains(variation.weaponName))
            {
                variation.costText.text = "Already Owned";
                variation.buyButton.deactivated = true;
                variation.buyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Already Owned";

                AsyncOperationHandle<Sprite> asyncHandle = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/UI/smileOS 2 wide button disabled.png");
                variation.buyButton.gameObject.GetComponent<Image>().sprite = asyncHandle.WaitForCompletion();
                //variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                variation.equipButtons.SetActive(false);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is not unlocked, is purchased");
                Addressables.Release(asyncHandle);
            }
            // Weapon is not unlocked, weapon is blue variation
            else if (!unlocked && variation.weaponName.Contains("0"))
            {
                variation.costText.text = "<color=" + red + ">Unavailable</color>";
                variation.buyButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Unavailable";
                variation.equipButtons.SetActive(false);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Blue variation, is not unlocked");
            }
        }

        public IEnumerator DelayedUpdate()
        {
            yield return new WaitForEndOfFrame();
            UpdateShopVariation(variationInfo);
        }
    }
}
