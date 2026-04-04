using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public class MultiPreloadFromManagerMusic : BaseMusic
    {
        public bool Preloaded { get; internal set; } = false;
        public bool Ready => clean && battle;

        public string scene;
        public string songName;
        public AssetReferenceSprite icon;
        public AudioClip clean;
        public AudioClip battle;

        public MultiPreloadFromManagerMusic(string levelNames, string scene, string songName, AssetReferenceSprite icon)
        {
            this.levelNames = levelNames;
            this.artistName = "Heaven Pierce Her";
            this.scene = scene;
            this.songName = songName;
            this.icon = icon;
        }

        public MultiPreloadFromManagerMusic(string levelNames, string artistName, string scene, string songName, AssetReferenceSprite icon)
        {
            this.levelNames = levelNames;
            this.artistName = artistName;
            this.scene = scene;
            this.songName = songName;
            this.icon = icon;
        }

        public override string ToString()
        {
            return Ready ? $"{songName} (Ready!)" : $"{songName} (Not ready)";
        }
    }
}
