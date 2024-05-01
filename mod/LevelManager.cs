﻿using Archipelago.MultiClient.Net.Enums;
using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL
{
    public static class LevelManager
    {
        public static Dictionary<string, GameObject> skulls = new Dictionary<string, GameObject>();

        public static void FindSkulls()
        {
            skulls.Clear();
            int i = 0;
            foreach (ItemIdentifier item in Resources.FindObjectsOfTypeAll<ItemIdentifier>())
            {
                if ((item.itemType == ItemType.SkullBlue || item.itemType == ItemType.SkullRed) && item.gameObject.scene.name == SceneManager.GetActiveScene().name)
                {
                    if (SceneHelper.CurrentScene == "Level 7-S")
                    {
                        if (item.itemType == ItemType.SkullBlue && !item.transform.parent.parent.parent) continue;
                        else if (item.itemType == ItemType.SkullRed && item.transform.parent.parent.parent.name != "Interactives") continue;
                    }

                    if (skulls.ContainsKey(item.itemType.ToString()))
                    {
                        i++;
                        skulls[item.itemType.ToString() + i] = item.gameObject;
                    }
                    else skulls[item.itemType.ToString()] = item.gameObject;
                }
            }

            for (int j = 0; j < skulls.Count; j++)
            {
                KeyValuePair<string, GameObject> pair = skulls.ElementAt(j);
                string id = Core.CurrentLevelInfo.Id.ToString();
                if (Core.CurrentLevelInfo.Name == "0-S") id = "0S";
                else if (Core.CurrentLevelInfo.Name == "7-S") id = "7S";
                switch (SceneHelper.CurrentScene)
                {
                    case "Level 1-4":
                        if (j + 1 > Core.data.unlockedSkulls1_4) pair.Value.SetActive(false);
                        break;
                    case "Level 5-1":
                        if (j + 1 > Core.data.unlockedSkulls5_1) pair.Value.SetActive(false);
                        break;
                    default:
                        if (pair.Value.name.Contains("Blue"))
                        {
                            if (!Core.data.unlockedSkulls.Contains(id + "_b")) pair.Value.SetActive(false);
                        }
                        else if (pair.Value.name.Contains("Red"))
                        {
                            if (!Core.data.unlockedSkulls.Contains(id + "_r")) pair.Value.SetActive(false);
                        }
                        break;
                }
            }
        }

        public static void UpdateShopVariation(VariationInfo variation)
        {
            // Set Feedbacker text to unavailable if not owned
            if (variation.weaponName == "arm0" && !Core.data.hasArm)
            {
                variation.costText.text = "<color=red>UNAVAILABLE</color>";
                variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "UNAVAILABLE";
                variation.equipButton.gameObject.SetActive(false);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is not unlocked");
                return;
            }
            // Set Feedbacker text back to default if owned
            else if (variation.weaponName == "arm0" && Core.data.hasArm)
            {
                variation.costText.text = "ALREADY OWNED";
                variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ALREADY OWNED";
                variation.equipButton.gameObject.SetActive(true);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is unlocked");
                return;
            }
            // Do nothing for other arms
            if (variation.weaponName == "arm1" || variation.weaponName == "arm2")
            {
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Skipping");
                return;
            }

            // Set shop item description (Skip blue variations)
            if (!variation.weaponName.Contains("0"))
            {
                string description = "[Purchase to unlock: ";

                if (LocationManager.locations.ContainsKey("shop_" + variation.weaponName))
                {
                    if (LocationManager.shopScouts["shop_" + variation.weaponName] is UKItem ukitem)
                    {
                        description += "<color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.GetUKMessageColor(ukitem.itemName)) + "FF>" + ukitem.itemName + "</color>";
                        if (ukitem.playerName != Core.data.slot_name) description += " for <color=#" + ColorUtility.ToHtmlStringRGB(Colors.PlayerOther) + "FF>" + ukitem.playerName + "</color>";
                        description += "]\n\n";
                    }
                    else if (LocationManager.shopScouts["shop_" + variation.weaponName] is APItem apitem)
                    {
                        description = "<color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.GetAPMessageColor(apitem.type)) + "FF>" + apitem.itemName + "</color>";
                        description += " for <color=#" + ColorUtility.ToHtmlStringRGB(Colors.PlayerOther) + "FF>" + apitem.playerName + "</color>";
                        description += "]\n\n";

                        if (apitem.type == ItemFlags.Advancement) description += "You don't know what this is, but it seems <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemAdvancement) + "FF>important.</color>";
                        else if (apitem.type == ItemFlags.NeverExclude) description += "You don't know what this is, but it seems like it could be <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemNeverExclude) + "FF>useful.</color>";
                        else if (apitem.type == ItemFlags.Trap) description += "You don't know what this is, but it seems like they're probably <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemTrap) + "FF>better off without it.</color>";
                        else description += "You don't know what this is, but it seems like you could probably <color=#" + ColorUtility.ToHtmlStringRGB(Colors.ItemFiller) + "FF>skip this</color> if you wanted to.";
                    }
                }
                else description = "???";
                variation.varPage.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = description;
            }

            GameProgressMoneyAndGear generalProgress = GameProgressSaver.GetGeneralProgress();
            FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(variation.weaponName, BindingFlags.Instance | BindingFlags.Public);
            bool unlocked = int.Parse(field.GetValue(generalProgress).ToString()) == 1;

            // Weapon is unlocked, weapon is not blue variation, weapon has not been purchased
            if (unlocked && !variation.weaponName.Contains("0") && !Core.data.purchasedItems.Contains(variation.weaponName))
            {
                bool canAfford = false;
                string cost;
                if (GameProgressSaver.GetMoney() >= Core.shopPrices[variation.weaponName]) canAfford = true;
                if (canAfford) cost = MoneyText.DivideMoney(Core.shopPrices[variation.weaponName]) + "<color=orange>P</color>";
                else cost = "<color=red>" + MoneyText.DivideMoney(Core.shopPrices[variation.weaponName]) + "P</color>";

                variation.costText.text = cost;

                variation.equipButton.transform.GetChild(0).GetComponent<Image>().sprite = variation.equipSprites[PrefsManager.Instance.GetInt("weapon." + variation.weaponName, 1)];
                variation.orderButtons.SetActive(true);
                Traverse variationT = Traverse.Create(variation);
                variationT.Field<int>("equipStatus").Value = PrefsManager.Instance.GetInt("weapon." + variation.weaponName, 1);
                variationT.Field<int>("money").Value = GameProgressSaver.GetMoney();
                variation.varPage.GetComponentInChildren<MoneyText>().UpdateMoney();

                variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cost;
                if (canAfford)
                {
                    variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1);
                    variation.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                else
                {
                    variation.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
                }
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is unlocked, is not purchased");
            }
            // Weapon is not unlocked, weapon is not blue variation, weapon has been purchased
            else if (!unlocked && !variation.weaponName.Contains("0") && Core.data.purchasedItems.Contains(variation.weaponName))
            {
                variation.costText.text = "ALREADY OWNED";
                variation.buyButton.deactivated = true;
                variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ALREADY OWNED";
                variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                variation.equipButton.gameObject.SetActive(false);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is unlocked, is purchased");
            }
            // Weapon is not unlocked, weapon is blue variation
            else if (!unlocked && variation.weaponName.Contains("0"))
            {
                variation.costText.text = "<color=red>UNAVAILABLE</color>";
                variation.buyButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "UNAVAILABLE";
                variation.equipButton.gameObject.SetActive(false);
                Core.Logger.LogInfo($"Shop - Weapon: \"{variation.weaponName}\" - Is not unlocked");
            }
        }

        public static void AddDoorClosers()
        {
            foreach (ItemPlaceZone ipz in Resources.FindObjectsOfTypeAll<ItemPlaceZone>())
            {
                if (SceneHelper.CurrentScene == "Level 1-1")
                {
                    if (ipz.transform.parent.parent != null && ipz.transform.parent.parent.name == "11 Nonstuff")
                    {
                        if (!ipz.transform.parent.parent.parent.gameObject.GetComponent<ReverseDoorCloser>()) ipz.transform.parent.parent.parent.gameObject.AddComponent<ReverseDoorCloser>();
                    }
                }
                else if (SceneHelper.CurrentScene == "Level 1-2")
                {
                    if (ipz.transform.parent.parent != null && ipz.transform.parent.parent.name == "3 Nonstuff")
                    {
                        if (!ipz.transform.parent.parent.parent.gameObject.GetComponent<ReverseDoorCloser>()) ipz.transform.parent.parent.parent.gameObject.AddComponent<ReverseDoorCloser>();
                    }
                }
                else if (SceneHelper.CurrentScene == "Level 2-3")
                {
                    if (ipz.transform.parent != null && (ipz.transform.parent.name == "Altar" || ipz.transform.parent.name == "Altar (1)"))
                    {
                        if (!ipz.transform.parent.gameObject.GetComponent<ReverseDoorCloser>()) ipz.transform.parent.gameObject.AddComponent<ReverseDoorCloser>();
                    }
                }
                else if (SceneHelper.CurrentScene == "Level 4-4")
                {
                    if (ipz.transform.parent.parent != null && ipz.transform.parent.parent.name == "Secret Hall")
                    {
                        if (!ipz.transform.parent.parent.parent.gameObject.GetComponent<ReverseDoorCloser>()) ipz.transform.parent.parent.parent.gameObject.AddComponent<ReverseDoorCloser>();
                    }
                }
                else if (SceneHelper.CurrentScene == "Level 5-2")
                {
                    if (ipz.transform.parent.parent != null && (ipz.transform.parent.parent.name == "6" || ipz.transform.parent.parent.name == "7B"))
                    {
                        if (!ipz.transform.parent.parent.gameObject.GetComponent<ReverseDoorCloser>()) ipz.transform.parent.parent.gameObject.AddComponent<ReverseDoorCloser>();
                    }
                }
                else if (SceneHelper.CurrentScene == "Level 5-3")
                {
                    if (ipz.transform.parent.parent != null && ipz.transform.parent.parent.name == "2A4 - Skullway")
                    {
                        if (!ipz.transform.parent.parent.gameObject.GetComponent<ReverseDoorCloser>()) ipz.transform.parent.parent.gameObject.AddComponent<ReverseDoorCloser>();
                    }
                }
                else if (SceneHelper.CurrentScene == "Level 6-1")
                {
                    if (ipz.transform.parent.parent != null && ipz.transform.parent.parent.name == "3 - Crossroads")
                    {
                        if (!ipz.transform.parent.parent.gameObject.GetComponent<ReverseDoorCloser>()) ipz.transform.parent.parent.gameObject.AddComponent<ReverseDoorCloser>();
                    }
                }
            }
        }

        public static void DeactivateNailgun()
        {
            foreach (GearCheckEnabler gce in Resources.FindObjectsOfTypeAll<GearCheckEnabler>())
            {
                if (gce.transform.parent.name == "1 - First Room") gce.gameObject.SetActive(false);
            }
        }

        public static void ChangeIntro()
        {
            if (!Core.CurrentLevelHasInfo || Core.CurrentLevelInfo.Id != 1) return;

            TextMeshProUGUI text = GameObject.Find("/Canvas").transform.Find("HurtScreen").Find("Text 2 Sound").Find("Text (2)").GetComponent<TextMeshProUGUI>();
            text.text = text.text + "\nARCHIPELAGO BY   <color=#fec24c>TRPG</color>";
            text.lineSpacing = 25;
            TextMeshProUGUI text2 = GameObject.Instantiate(text.gameObject, text.transform.parent).GetComponent<TextMeshProUGUI>();
            text2.text = "and others!";
            text2.fontSize = 32;
            text2.transform.localPosition = new Vector3(270, -70, 0);
            text2.transform.Rotate(new Vector3(0, 0, 6));
            GameObject iconObj = GameObject.Instantiate(text.gameObject, text.transform.parent);
            Component.DestroyImmediate(iconObj.GetComponent<TextMeshProUGUI>());
            Component.DestroyImmediate(iconObj.GetComponent<TMP_SpriteAnimator>());
            Image icon = iconObj.AddComponent<Image>();
            icon.sprite = UIManager.bundle.LoadAsset<Sprite>("assets/trpg.png");
            icon.color = new Color(0.992f, 0.758f, 0.297f);
            icon.preserveAspect = true;
            icon.transform.localPosition = new Vector3(150, -32, 0);
            icon.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            text.gameObject.AddComponent<LinkedDisabler>().objects.Add(text2.gameObject);
            text.gameObject.AddComponent<LinkedDisabler>().objects.Add(icon.gameObject);
        }

        public static void AddGlassComponents()
        {
            if (!Core.CurrentLevelHasInfo || Core.CurrentLevelInfo.Id != 1) return;

            foreach (Glass glass in Resources.FindObjectsOfTypeAll<Glass>())
            {
                string parentName = glass.transform.parent.parent.name;
                if (parentName == "5 Stuff" || parentName == "5 Stuff(Clone)" || parentName == "11 Content" || parentName == "11 Content(Clone)")
                {
                    GameObject room = glass.transform.parent.parent.gameObject;
                    if (!room.GetComponent<GlassDisabler>()) room.AddComponent<GlassDisabler>();
                }
            }
        }

        public static void FindHank()
        {
            if (!Core.CurrentLevelHasInfo) return;
            foreach (HudMessage hm in Resources.FindObjectsOfTypeAll<HudMessage>())
            {
                if ((Core.CurrentLevelInfo.Id == 9 || Core.CurrentLevelInfo.Id == 22) && hm.name == "EasterEgg")
                {
                    string message = hm.message;
                    message = message.Replace("Nothing", "Something");
                    message = message.Replace("but", "and");
                    hm.message = message;
                    hm.gameObject.AddComponent<Hank>();
                }
            }
        }

        public static void ForceBlueArm()
        {
            if (!FistControl.Instance)
            {
                Core.Logger.LogWarning("FistControl.Instance is null!");
                return;
            }
            FistControl.Instance.forcedLoadout.arm.blueVariant = VariantOption.ForceOn;
            FistControl.Instance.ResetFists();
        }

        public static void FindRocketRaceButton()
        {
            if (SceneHelper.CurrentScene != "CreditsMuseum2" || !Core.data.rocketReward) return;
            GameObject.Find("/PuzzleScreen (2)/Canvas/Background/Start").AddComponent<RocketRaceCheck>();
        }
    }
}
