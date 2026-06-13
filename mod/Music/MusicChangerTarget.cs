using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Music
{
    public class MusicChangerTarget : BaseTarget
    {
        public List<string> gameObjectPaths;

        public bool clean;
        public bool battle;
        public bool boss;
        public bool alsoTargetManager;


        public MusicChangerTarget(string gameObjectPath, bool clean = true, bool battle = true, bool boss = true, bool alsoTargetManager = false)
        {
            this.gameObjectPaths = new List<string>() { gameObjectPath };
            this.clean = clean;
            this.battle = battle;
            this.boss = boss;
            this.alsoTargetManager = alsoTargetManager;
        }

        public MusicChangerTarget(List<string> gameObjectPaths, bool clean = true, bool battle = true, bool boss = true, bool alsoTargetManager = false)
        {
            this.gameObjectPaths = gameObjectPaths;
            this.clean = clean;
            this.battle = battle;
            this.boss = boss;
            this.alsoTargetManager = alsoTargetManager;
        }
    }
}
