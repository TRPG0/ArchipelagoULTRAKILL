using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public class MultiClipMusic : BaseClipMusic
    {
        public AssetReferenceT<AudioClip> audioClipClean;
        public AssetReferenceT<AudioClip> audioClipBattle;

        public MultiClipMusic(string levelNames, string songName, AssetReferenceSprite icon, AssetReferenceT<AudioClip> clean, AssetReferenceT<AudioClip> battle)
        {
            this.levelNames = levelNames;
            this.songName = songName;
            this.artistName = "Heaven Pierce Her";
            this.icon = icon;
            this.audioClipClean = clean;
            this.audioClipBattle = battle;
        }

        public MultiClipMusic(string levelNames, string songName, string artistName, AssetReferenceSprite icon, AssetReferenceT<AudioClip> clean, AssetReferenceT<AudioClip> battle)
        {
            this.levelNames = levelNames;
            this.songName = songName;
            this.artistName = artistName;
            this.icon = icon;
            this.audioClipClean = clean;
            this.audioClipBattle = battle;
        }

        public override void Reset()
        {
            if (audioClipClean.Asset != null) audioClipClean.ReleaseAsset();
            if (audioClipBattle.Asset != null) audioClipBattle.ReleaseAsset();
        }

        public override string ToString()
        {
            return audioClipBattle.AssetGUID;
        }
    }
}
