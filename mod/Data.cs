using System.Collections.Generic;

namespace ArchipelagoULTRAKILL
{
    public class Data
    {
        public long index;
        public string host_name;
        public string slot_name;
        public string password;
        public HashSet<string> @checked = new HashSet<string>();

        public HashSet<string> unlockedLevels = new HashSet<string>();
        public HashSet<string> unlockedSecrets = new HashSet<string>();
        public HashSet<string> completedLevels = new HashSet<string>();
        public HashSet<string> purchasedItems = new HashSet<string>();

        public string goal = "6-2";
        public int goalRequirement = 20;
        public bool challengeRewards = false;
        public bool pRankRewards = false;
        public bool hasArm = true;
        public int dashes = 3;
        public int walljumps = 3;
        public bool canSlide = true;
        public bool canSlam = true;
        public int multiplier = 1;

        public bool randomizeFire2 = false;
        public HashSet<string> unlockedFire2 = new HashSet<string>();

        public bool randomizeSkulls = false;
        public HashSet<string> unlockedSkulls = new HashSet<string>();
        public int unlockedSkulls1_4 = 0;
        public int unlockedSkulls5_1 = 0;

        public bool musicRandomizer = false;
        public Dictionary<string, string> music = new Dictionary<string, string>();
    }
}
