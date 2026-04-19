using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Music
{
    public class AudioSourceTarget : BaseTarget
    {
        public List<string> gameObjectPaths;
        public List<string> doNotLink;

        public AudioSourceTarget(string gameObjectPath)
        {
            this.gameObjectPaths = new List<string>() { gameObjectPath };
            this.doNotLink = new List<string>();
        }

        public AudioSourceTarget(List<string> gameObjectPaths)
        {
            this.gameObjectPaths = gameObjectPaths;
            this.doNotLink = new List<string>();
        }

        public AudioSourceTarget(List<string> gameObjectPaths, List<string> doNotLink)
        {
            this.gameObjectPaths = gameObjectPaths;
            this.doNotLink = doNotLink;
        }
    }
}
