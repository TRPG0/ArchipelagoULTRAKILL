using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Music
{
    public class MusicChangerTarget : BaseTarget
    {
        public List<string> gameObjectPaths;

        public bool clean;
        public bool battle;
        public bool boss;

        public MusicChangerTarget(string gameObjectPath)
        {
            this.gameObjectPaths = new List<string>() { gameObjectPath };
            clean = true;
            battle = true;
            boss = true;
        }

        public MusicChangerTarget(string gameObjectPath, bool clean, bool battle, bool boss)
        {
            this.gameObjectPaths = new List<string>() { gameObjectPath };
            this.clean = clean;
            this.battle = battle;
            this.boss = boss;
        }

        public MusicChangerTarget(List<string> gameObjectPaths)
        {
            this.gameObjectPaths = gameObjectPaths;
            clean = true;
            battle = true;
            boss = true;
        }

        public MusicChangerTarget(List<string> gameObjectPaths, bool clean, bool battle, bool boss)
        {
            this.gameObjectPaths = gameObjectPaths;
            this.clean = clean;
            this.battle = battle;
            this.boss = boss;
        }
    }
}
