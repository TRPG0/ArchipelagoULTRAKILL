using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public class MultiClipAndSoundtrackMusic : BaseMusic
    {
        public AssetReferenceT<AudioClip> audioClip;
        public AssetReferenceSoundtrackSong soundtrackSong;
        public bool clipIsClean;
        public bool soundtrackIsClean;
        public int soundtrackIndex;

        public MultiClipAndSoundtrackMusic(string levelNames, AssetReferenceT<AudioClip> audioClip, bool clipIsClean, AssetReferenceSoundtrackSong soundtrack, bool soundtrackIsClean, int soundtrackIndex = 0)
        {
            this.levelNames = levelNames;
            this.artistName = "Heaven Pierce Her";
            this.audioClip = audioClip;
            this.soundtrackSong = soundtrack;
            this.clipIsClean = clipIsClean;
            this.soundtrackIsClean = soundtrackIsClean;
            this.soundtrackIndex = soundtrackIndex;
        }

        public MultiClipAndSoundtrackMusic(string levelNames, string artistName, AssetReferenceT<AudioClip> audioClip, bool clipIsClean, AssetReferenceSoundtrackSong soundtrack, bool soundtrackIsClean, int soundtrackIndex = 0)
        {
            this.levelNames = levelNames;
            this.artistName = artistName;
            this.audioClip = audioClip;
            this.soundtrackSong = soundtrack;
            this.clipIsClean = clipIsClean;
            this.soundtrackIsClean = soundtrackIsClean;
            this.soundtrackIndex = soundtrackIndex;
        }

        public override void Reset()
        {
            if (audioClip.Asset != null) audioClip.ReleaseAsset();
            if (soundtrackSong.Asset != null) soundtrackSong.ReleaseAsset();
        }

        public override string ToString()
        {
            return soundtrackSong.AssetGUID;
        }
    }
}
