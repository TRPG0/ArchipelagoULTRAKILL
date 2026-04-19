using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Music
{
    public class ShoppingTarget : BaseTarget
    {

        public List<string> musicChangerPaths;
        public List<string> audioSourcePaths;
        public List<string> objectActivatorPaths;

        public bool clean;
        public bool battle;
        public bool boss;

        public ShoppingTarget(string musicChangerPath, List<string> audioSourcePaths, List<string> objectActivatorPaths, bool clean = true, bool battle = true, bool boss = true)
        {
            this.musicChangerPaths = new List<string>() { musicChangerPath };
            this.audioSourcePaths = audioSourcePaths;
            this.objectActivatorPaths = objectActivatorPaths;
            this.clean = clean;
            this.battle = battle;
            this.boss = boss;
        }
    }
}
