using System.Collections.Generic;

namespace ArchipelagoULTRAKILL.Structures
{
    public class LevelInfo
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Layer { get; private set; }
        public InfoFlags Flags { get; private set; }
        public List<string> SkullsList { get; private set; }

        public LevelInfo(string name, int id, int layer, InfoFlags flags, List<string> skullsList = null)
        {
            Name = name;
            Id = id;
            Layer = layer;
            Flags = flags;
            SkullsList = skullsList;
        }

        public override string ToString() => $"{Name} ({Id} | {Flags.ToString()})";
    }
}
