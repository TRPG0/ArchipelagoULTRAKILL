using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public class SinglePreloadFromManagerMusic : PreloadMusic
    {
        public override bool Ready => audioClip;

        public AudioClip audioClip;

        public SinglePreloadFromManagerMusic(string levelNames, string scene, string songName, AssetReferenceSprite icon)
        {
            this.levelNames = levelNames;
            this.artistName = "Heaven Pierce Her";
            this.scene = scene;
            this.songName = songName;
            this.icon = icon;
        }

        public SinglePreloadFromManagerMusic(string levelNames, string artistName, string scene, string songName, AssetReferenceSprite icon)
        {
            this.levelNames = levelNames;
            this.artistName = artistName;
            this.scene = scene;
            this.songName = songName;
            this.icon = icon;
        }

        public override void Reset()
        {
            base.Reset();
            audioClip = null;
        }
    }
}
