using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public class MultiPreloadFromManagerMusic : PreloadMusic
    {
        public override bool Ready => clean && battle;

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

        public override void Reset()
        {
            base.Reset();
            clean = null;
            battle = null;
        }
    }
}
