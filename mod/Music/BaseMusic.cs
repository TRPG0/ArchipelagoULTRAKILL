namespace ArchipelagoULTRAKILL.Music
{
    public abstract class BaseMusic
    {
        public string levelNames;
        public string artistName;

        public bool IsMultiMusic => this is MultiClipMusic || this is MultiSoundtrackMusic || this is MultiClipAndSoundtrackMusic || this is MultiPreloadFromManagerMusic || this is MultiPreloadFromChangerMusic;

        public abstract void Reset();
    }
}
