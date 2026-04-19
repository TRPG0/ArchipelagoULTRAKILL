using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Music
{
    public class SoundChangerTarget : BaseTarget
    {
        public List<string> audioSourcePaths;
        public List<string> soundChangerPaths;

        public SoundChangerTarget(string audioSourcePath, string soundChangerPath)
        {
            this.audioSourcePaths = new List<string>() { audioSourcePath };
            this.soundChangerPaths = new List<string>() { soundChangerPath };
        }

        public SoundChangerTarget(List<string> audioSourcePaths, List<string> soundChangerPaths)
        {
            this.audioSourcePaths = audioSourcePaths;
            this.soundChangerPaths = soundChangerPaths;
        }
    }
}
