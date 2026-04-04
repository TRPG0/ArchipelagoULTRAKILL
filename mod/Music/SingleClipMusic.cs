using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public class SingleClipMusic : BaseClipMusic
    {
        public AssetReferenceT<AudioClip> audioClip;

        public SingleClipMusic(string levelNames, string songName, AssetReferenceSprite icon, AssetReferenceT<AudioClip> clip)
        {
            this.levelNames = levelNames;
            this.songName = songName;
            this.artistName = "Heaven Pierce Her";
            this.icon = icon;
            this.audioClip = clip;
        }

        public SingleClipMusic(string levelNames, string songName, string artistName, AssetReferenceSprite icon, AssetReferenceT<AudioClip> clip)
        {
            this.levelNames = levelNames;
            this.songName = songName;
            this.artistName = artistName;
            this.icon = icon;
            this.audioClip = clip;
        }

        public override string ToString()
        {
            return audioClip.AssetGUID;
        }
    }
}
