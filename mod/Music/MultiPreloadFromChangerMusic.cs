using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public class MultiPreloadFromChangerMusic : PreloadMusic
    {
        public override bool Ready => clean && battle;

        public AssetReferenceSprite icon;
        public AudioClip clean;
        public AudioClip battle;
        public string changerPath;

        public MultiPreloadFromChangerMusic(string levelNames, string scene, string songName, AssetReferenceSprite icon, string changerPath)
        {
            this.levelNames = levelNames;
            this.artistName = "Heaven Pierce Her";
            this.scene = scene;
            this.songName = songName;
            this.icon = icon;
            this.changerPath = changerPath;
        }

        public MultiPreloadFromChangerMusic(string levelNames, string artistName, string scene, string songName, AssetReferenceSprite icon, string changerPath)
        {
            this.levelNames = levelNames;
            this.artistName = artistName;
            this.scene = scene;
            this.songName = songName;
            this.icon = icon;
            this.changerPath = changerPath;
        }

        public override void Reset()
        {
            base.Reset();
            clean = null;
            battle = null;
        }
    }
}
