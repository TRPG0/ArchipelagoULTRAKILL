using ArchipelagoULTRAKILL.Music;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL
{
    public abstract class PreloadMusic : BaseMusic
    {
        public bool Preloaded { get; internal set; } = false;

        public AssetReferenceSprite icon;

        public abstract bool Ready { get; }

        public string scene;
        public string songName;
        public List<GameObject> gameObjects = new List<GameObject>();

        public override void Reset()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                GameObject.Destroy(gameObject);
            }
            gameObjects.Clear();
            if (icon.Asset != null) icon.ReleaseAsset();
            Preloaded = false;
        }

        public override string ToString()
        {
            return Ready ? $"{songName} (Ready!)" : $"{songName} (Not ready)";
        }
    }
}
