using ArchipelagoULTRAKILL.Music;
using System.Collections.Generic;
using UnityEngine;

namespace ArchipelagoULTRAKILL
{
    public abstract class PreloadMusic : BaseMusic
    {
        public bool Preloaded { get; internal set; } = false;

        public abstract bool Ready { get; }

        public string scene;
        public string songName;
        public List<GameObject> gameObjects = new List<GameObject>();

        public virtual void Reset()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                GameObject.Destroy(gameObject);
            }
            gameObjects.Clear();
            Preloaded = false;
        }

        public override string ToString()
        {
            return Ready ? $"{songName} (Ready!)" : $"{songName} (Not ready)";
        }
    }
}
