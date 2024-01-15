using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Structures
{
    public class LevelInfo
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Layer { get; private set; }
        public bool HasSecrets { get; private set; }
        public MusicType Music { get; private set; }
        public SkullsType Skulls { get; private set; }
        public List<string> SkullsList { get; private set; }

        public LevelInfo(string name, int id, int layer, bool hasSecrets, MusicType music, SkullsType skulls, List<string> skullsList = null)
        {
            Name = name;
            Id = id;
            Layer = layer;
            HasSecrets = hasSecrets;
            Music = music;
            Skulls = skulls;
            SkullsList = skullsList;
        }
    }
}
