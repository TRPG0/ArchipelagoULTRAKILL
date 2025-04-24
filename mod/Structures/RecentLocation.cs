using Archipelago.MultiClient.Net.Enums;
using System;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Structures
{
    public class RecentLocation
    {
        public string locationName;
        public string locationImage;
        public string locationColor;
        public string itemName;
        public ItemFlags itemFlags;
        public string targetPlayer;

        public RecentLocation(string locationName, string locationImage, string locationColor, string itemName, ItemFlags itemFlags, string targetPlayer)
        {
            this.locationName = locationName;
            this.locationImage = locationImage;
            this.locationColor = locationColor;
            this.itemName = itemName;
            this.itemFlags = itemFlags;
            this.targetPlayer = targetPlayer;
        }
    }
}
