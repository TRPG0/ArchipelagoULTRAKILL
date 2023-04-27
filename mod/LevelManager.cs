using Archipelago.MultiClient.Net.Enums;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL
{
    public static class LevelManager
    {
        public static List<GameObject> shopPanels = new List<GameObject>();

        public static readonly Dictionary<string, int> shopPrices = new Dictionary<string, int>()
        {
            ["rev2"] = 7500,
            ["rev1"] = 12500,
            ["sho1"] = 12500,
            ["nai1"] = 25000,
            ["rai1"] = 100000,
            ["rai2"] = 100000,
            ["rock1"] = 75000
        };

        public static Dictionary<string, GameObject> skulls = new Dictionary<string, GameObject>();

        public static void FindShopObjects()
        {
            foreach (VariationInfo vi in Resources.FindObjectsOfTypeAll<VariationInfo>())
            {
                if (vi.gameObject.scene.name == SceneManager.GetActiveScene().name && vi.equipSprites.Count() > 0) shopPanels.Add(vi.gameObject);
            }
        }

        public static void UpdateShopObjects()
        {
            GameProgressMoneyAndGear generalProgress = GameProgressSaver.GetGeneralProgress();
            for (int i = 0; i < shopPanels.Count; i++)
            {
                GameObject panel = shopPanels[i];
                string weapon = panel.GetComponent<VariationInfo>().weaponName;

                if (weapon == "arm0" && !Core.data.hasArm)
                {
                    VariationInfo info = panel.GetComponent<VariationInfo>();
                    info.costText.text = "<color=red>UNAVAILABLE</color>";
                    info.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "UNAVAILABLE";
                    info.equipButton.gameObject.SetActive(false);
                }
                else if (weapon == "arm0" && Core.data.hasArm) continue;
                else if (weapon == "arm1" || weapon == "arm2") continue;
                else
                {
                    FieldInfo field = typeof(GameProgressMoneyAndGear).GetField(weapon, BindingFlags.Instance | BindingFlags.Public);
                    VariationInfo vi = panel.GetComponent<VariationInfo>();

                    Core.logger.LogInfo(weapon + ": " + field.GetValue(generalProgress));
                    if (!weapon.Contains("0"))
                    {
                        string description = "";

                        if (LocationManager.locations.ContainsKey("shop_" + vi.weaponName))
                        {
                            if (LocationManager.locations["shop_" + vi.weaponName].ukitem)
                            {
                                UKItem item = LocationManager.ukitems["shop_" + vi.weaponName];
                                description = "<color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.GetUKMessageColor(item.item_name)) + "FF>" + item.item_name + "</color>\n";
                                if (item.player_name != Core.data.slot_name) description += "for <color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.colors["ap_player_other"]) + "FF>" + item.player_name + "</color>\n";
                                else description += "\n";
                            }
                            else
                            {
                                APItem item = LocationManager.apitems["shop_" + vi.weaponName];
                                description = "<color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.GetAPMessageColor(item.type)) + "FF>" + item.item_name + "</color>\n";
                                description += "for <color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.colors["ap_player_other"]) + "FF>" + item.player_name + "</color>\n\n";
                                if (item.type == ItemFlags.Advancement) description += "You don't know what this is, but it seems <color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.colors["ap_item_advancement"]) + "FF>important.</color>";
                                else if (item.type == ItemFlags.NeverExclude) description += "You don't know what this is, but it seems like it could be <color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.colors["ap_item_neverexclude"]) + "FF>useful.</color>";
                                else if (item.type == ItemFlags.Trap) description += "You don't know what this is, but it seems like they're probably <color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.colors["ap_item_neverexclude"]) + "FF>better off without it.</color>";
                                else description += "You don't know what this is, but it seems like you could probably <color=#" + ColorUtility.ToHtmlStringRGB(LocationManager.colors["ap_item_filler"]) + "FF>skip this</color> if you wanted to.";
                            }
                        }
                        else description = "???";

                        vi.varPage.transform.GetChild(3).GetComponent<Text>().text = description;
                    }

                    if (int.Parse(field.GetValue(generalProgress).ToString()) != 0 && !weapon.Contains("0") && !Core.data.purchasedItems.Contains(weapon))
                    {
                        bool canAfford = false;
                        string cost;
                        if (GameProgressSaver.GetMoney() >= shopPrices[weapon]) canAfford = true;
                        if (canAfford) cost = MoneyText.DivideMoney(shopPrices[weapon]) + "<color=orange>P</color>";
                        else cost = "<color=red>" + MoneyText.DivideMoney(shopPrices[weapon]) + "P</color>";

                        vi.costText.text = cost;

                        vi.equipButton.transform.GetChild(0).GetComponent<Image>().sprite = vi.equipSprites[PrefsManager.Instance.GetInt("weapon." + weapon, 1)];
                        vi.orderButtons.SetActive(true);
                        Traverse.Create(vi).Field<int>("equipStatus").Value = PrefsManager.Instance.GetInt("weapon." + weapon, 1);
                        Traverse.Create(vi).Field<int>("money").Value = GameProgressSaver.GetMoney();
                        vi.moneyText.text = MoneyText.DivideMoney(GameProgressSaver.GetMoney()) + "<color=orange>P</color>";

                        vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = cost;
                        if (canAfford)
                        {
                            vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1);
                            vi.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
                        }
                        else
                        {
                            vi.buyButton.gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
                        }
                    }
                    else if (int.Parse(field.GetValue(generalProgress).ToString()) == 0 && !weapon.Contains("0") && Core.data.purchasedItems.Contains(weapon))
                    {
                        vi.costText.text = "ALREADY OWNED";
                        vi.buyButton.deactivated = true;
                        vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "ALREADY OWNED";
                        vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(0.5882f, 0.5882f, 0.5882f);
                        vi.equipButton.gameObject.SetActive(false);
                    }
                    else if (int.Parse(field.GetValue(generalProgress).ToString()) == 0 && weapon.Contains("0"))
                    {
                        vi.costText.text = "<color=red>UNAVAILABLE</color>";
                        vi.buyButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "UNAVAILABLE";
                        vi.equipButton.gameObject.SetActive(false);

                    }
                }
            }
        }

        public static void FindSkulls()
        {
            skulls.Clear();
            int i = 0;
            foreach (ItemIdentifier item in Resources.FindObjectsOfTypeAll<ItemIdentifier>())
            {
                if ((item.itemType == ItemType.SkullBlue || item.itemType == ItemType.SkullRed) && item.gameObject.scene.name == SceneManager.GetActiveScene().name)
                {
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
                            if (!Core.data.unlockedSkulls.Contains(StatsManager.Instance.levelNumber.ToString() + "_b")) pair.Value.SetActive(false);
                        }
                        else if (pair.Value.name.Contains("Red"))
                        {
                            if (!Core.data.unlockedSkulls.Contains(StatsManager.Instance.levelNumber.ToString() + "_r")) pair.Value.SetActive(false);
                        }
                        break;
                }
            }
        }

        public static void CloseReverseDoors()
        {
            foreach (ItemPlaceZone ipz in Resources.FindObjectsOfTypeAll<ItemPlaceZone>())
            {
                if (ipz.reverseDoors.Count() > 0)
                {
                    for (int i = 0; i < ipz.reverseDoors.Count(); i++)
                    {
                        ipz.reverseDoors[i].Close(true);
                    }
                }
            }
        }

        public static void DeactivatePlanks()
        {
            GameObject room;
            if (GameObject.Find("1 -  Starting Room"))
            {
                room = GameObject.Find("1 -  Starting Room");
                for (int i = 0; i < room.transform.childCount; i++)
                {
                    GameObject child = room.transform.GetChild(i).gameObject;

                    if (child.name.Contains("Plank")) child.SetActive(false);
                }
            }
            else if (GameObject.Find("1Alt -  Short Starting Room"))
            {
                room = GameObject.Find("1Alt -  Short Starting Room");
                for (int i = 0; i < room.transform.childCount; i++)
                {
                    GameObject child = room.transform.GetChild(i).gameObject;

                    if (child.name.Contains("Plank")) child.SetActive(false);
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

        public static void DeactivateGlass()
        {
            foreach (Glass glass in Resources.FindObjectsOfTypeAll<Glass>())
            {
                //logger.LogInfo(glass.name + " | " + glass.transform.parent.parent.name);
                if (glass.transform.parent.parent.name == "5 Stuff(Clone)" || glass.transform.parent.parent.name == "11 Content(Clone)") glass.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
