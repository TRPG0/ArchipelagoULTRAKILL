using System.Collections.Generic;

namespace ArchipelagoULTRAKILL
{
    public class APData
    {
        public bool active = false;

        public List<string> unlockedLevels = new List<string>();

        public List<string> unlockedSecrets = new List<string>();

        public List<string> completedLevels = new List<string>();

        public string goal = "6-2";
        public int goalRequirement = 20;

        public int dashes = 3;
        public int walljumps = 3;
        public bool canSlide = true;
        public bool canSlam = true;
        public bool playerActivated = false;

        public bool dashTrap = false;
        public bool walljumpTrap = false;
        public bool staminaPowerup = false;

        public bool lockFire2 = false;
        public List<string> unlockedFire2 = new List<string>();

        public List<string> purchasedItems = new List<string>();

    }
}
