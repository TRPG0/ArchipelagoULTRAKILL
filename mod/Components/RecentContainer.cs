using ArchipelagoULTRAKILL.Structures;
using BepInEx;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Components
{
    public enum RecentType
    {
        Location,
        Item
    }

    public class RecentContainer : MonoBehaviour
    {
        public int id;
        public RecentType type;
        public Image image;
        public TextMeshProUGUI text;

        public void Init(int id, RecentType type, Image image, TextMeshProUGUI text)
        {
            this.id = id;
            this.type = type;
            this.image = image;
            this.text = text;
        }

        public void Start()
        {
            text.fontSizeMax = 24;
            text.fontSizeMin = 10;
        }

        public void OnEnable()
        {
            if (!Multiworld.Authenticated)
            {
                image.gameObject.SetActive(false);
                text.gameObject.SetActive(false);

                if (id == 2) transform.Find("Offline Text").gameObject.SetActive(true);
            }
            else if (type == RecentType.Location)
            {
                try
                {
                    int targetId = Core.data.recentLocations.Count - 1 - id;

                    RecentLocation recentLocation = Core.data.recentLocations[targetId];
                    Color locationColor;

                    if (recentLocation.locationColor != null) locationColor = LocationManager.GetLocationColor(recentLocation.locationColor);
                    else locationColor = LocationManager.GetLocationColor(recentLocation.locationName);

                        image.sprite = UIManager.bundle.LoadAsset<Sprite>($"assets/{recentLocation.locationImage}.png");
                    image.color = locationColor;

                    string message = $"<color=#{ColorUtility.ToHtmlStringRGBA(locationColor)}>{recentLocation.locationName}</color>";

                    if (Core.data.playerCount > 1)
                    {
                        Color itemColor = Colors.White;
                        if (LocationManager.GetItemDefinition(recentLocation.itemName) != null) itemColor = LocationManager.GetItemDefinition(recentLocation.itemName).Color.Invoke();
                        else itemColor = LocationManager.GetAPMessageColor(recentLocation.itemFlags);

                        Color playerColor = Colors.PlayerOther;
                        if (recentLocation.targetPlayer == Core.data.slot_name) playerColor = Colors.PlayerSelf;

                        message += $" -> <color=#{ColorUtility.ToHtmlStringRGBA(itemColor)}>{recentLocation.itemName}</color> (<color=#{ColorUtility.ToHtmlStringRGBA(playerColor)}>{recentLocation.targetPlayer}</color>)";
                    }

                    text.text = message;

                    image.gameObject.SetActive(true);
                    text.gameObject.SetActive(true);
                }
                catch (ArgumentOutOfRangeException)
                {
                    image.gameObject.SetActive(false);
                    text.gameObject.SetActive(false);
                }
            }
            else if (type == RecentType.Item)
            {
                try
                {
                    int targetId = Multiworld.recentItems.Count - 1 - id;

                    RecentItem recentItem = Multiworld.recentItems[targetId];
                    ItemDefinition itemDefinition = LocationManager.GetItemDefinition(recentItem.itemName);
                    Color itemColor = itemDefinition.Color.Invoke();

                    image.sprite = UIManager.bundle.LoadAsset<Sprite>($"assets/{itemDefinition.Image}.png");
                    image.color = itemColor;

                    string message = $"<color=#{ColorUtility.ToHtmlStringRGBA(itemColor)}>{recentItem.itemName}</color>";

                    if (Core.data.playerCount > 1 && !recentItem.fromPlayer.IsNullOrWhiteSpace())
                    {
                        Color playerColor = Colors.PlayerOther;
                        if (recentItem.fromPlayer == Core.data.slot_name) playerColor = Colors.PlayerSelf;

                        message += $" (<color=#{ColorUtility.ToHtmlStringRGBA(playerColor)}>{recentItem.fromPlayer}</color>)";
                    }

                    text.text = message;

                    image.gameObject.SetActive(true);
                    text.gameObject.SetActive(true);
                }
                catch (ArgumentOutOfRangeException)
                {
                    image.gameObject.SetActive(false);
                    text.gameObject.SetActive(false);
                }
            }
        }
    }
}
