namespace ArchipelagoULTRAKILL.Music
{
    public class SingleSoundtrackMusic : BaseMusic
    {
        public AssetReferenceSoundtrackSong soundtrackSong;
        public int clipIndex;

        public SingleSoundtrackMusic(string levelNames, AssetReferenceSoundtrackSong soundtrack, int clipIndex = 0)
        {
            this.levelNames = levelNames;
            this.artistName = "Heaven Pierce Her";
            this.soundtrackSong = soundtrack;
            this.clipIndex = clipIndex;
        }

        public SingleSoundtrackMusic(string levelNames, string artistName, AssetReferenceSoundtrackSong soundtrack, int clipIndex = 0)
        {
            this.levelNames = levelNames;
            this.artistName = artistName;
            this.soundtrackSong = soundtrack;
            this.clipIndex = clipIndex;
        }

        public override string ToString()
        {
            return soundtrackSong.AssetGUID;
        }
    }
}
