namespace ArchipelagoULTRAKILL.Structures
{
    public class RecentItem
    {
        public string itemName;
        public string fromPlayer;
        public int index;

        public RecentItem(string itemName, string fromPlayer, int index)
        {
            this.itemName = itemName;
            this.fromPlayer = fromPlayer;
            this.index = index;
        }
    }
}
