using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Music
{
    public class MusicChangerTarget : BaseTarget
    {
        public List<string> gameObjectPaths;

        public bool clean;
        public bool battle;
        public bool boss;


        public MusicChangerTarget(string gameObjectPath, bool clean = true, bool battle = true, bool boss = true)
        {
            this.gameObjectPaths = new List<string>() { gameObjectPath };
            this.clean = clean;
            this.battle = battle;
            this.boss = boss;
        }

        public MusicChangerTarget(List<string> gameObjectPaths, bool clean = true, bool battle = true, bool boss = true)
        {
            this.gameObjectPaths = gameObjectPaths;
            this.clean = clean;
            this.battle = battle;
            this.boss = boss;
        }
    }
}
