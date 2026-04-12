namespace ArchipelagoULTRAKILL.Music
{
    public class MultiSoundtrackMusic : BaseMusic
    {
        public AssetReferenceSoundtrackSong soundtrackSong;
        public int cleanIndex;
        public int battleIndex;

        public MultiSoundtrackMusic(string levelNames, AssetReferenceSoundtrackSong soundtrack, int cleanIndex, int battleIndex)
        {
            this.levelNames = levelNames;
            this.artistName = "Heaven Pierce Her";
            this.soundtrackSong = soundtrack;
            this.cleanIndex = cleanIndex;
            this.battleIndex = battleIndex;
        }

        public MultiSoundtrackMusic(string levelNames, string artistName, AssetReferenceSoundtrackSong soundtrack, int cleanIndex, int battleIndex)
        {
            this.levelNames = levelNames;
            this.artistName = artistName;
            this.soundtrackSong = soundtrack;
            this.cleanIndex = cleanIndex;
            this.battleIndex = battleIndex;
        }

        public override void Reset()
        {
            if (soundtrackSong.Asset != null) soundtrackSong.ReleaseAsset();
        }

        public override string ToString()
        {
            return soundtrackSong.AssetGUID;
        }
    }
}
