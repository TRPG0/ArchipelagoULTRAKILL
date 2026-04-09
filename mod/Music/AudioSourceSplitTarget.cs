namespace ArchipelagoULTRAKILL.Music
{
    public class AudioSourceSplitTarget : BaseTarget
    {
        public string cleanTarget;
        public string battleTarget;

        public AudioSourceSplitTarget(string cleanTarget, string battleTarget)
        {
            this.cleanTarget = cleanTarget;
            this.battleTarget = battleTarget;
        }
    }
}
