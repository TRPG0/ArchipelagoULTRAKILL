using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Music
{
    public class AudioSourceTarget : BaseTarget
    {
        public List<string> gameObjectPaths;

        public AudioSourceTarget(string gameObjectPath)
        {
            this.gameObjectPaths = new List<string>() { gameObjectPath };
        }

        public AudioSourceTarget(List<string> gameObjectPaths)
        {
            this.gameObjectPaths = gameObjectPaths;
        }
    }
}
