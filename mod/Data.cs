using ArchipelagoULTRAKILL.Structures;
using BepInEx;
using System.Collections.Generic;

namespace ArchipelagoULTRAKILL
{
    public class Data
    {
        public string seed = string.Empty;
        public string version = string.Empty;
        public string serverVersion = string.Empty;
        public long index;
        public string host_name;
        public string slot_name;
        public string password;
        public HashSet<string> @checked = new HashSet<string>();
        public int playerCount = 1;
        public List<RecentLocation> recentLocations = new List<RecentLocation>();

        public HashSet<string> unlockedLevels = new HashSet<string>();
        public HashSet<string> unlockedSecrets = new HashSet<string>();
        public HashSet<string> completedLevels = new HashSet<string>();
        public HashSet<string> purchasedItems = new HashSet<string>();

        public string start = "0-1";
        public string goal = "6-2";
        public int goalRequirement = 20;
        public bool perfectGoal = false;
        public bool secretExitUnlock = true;
        public bool secretExitComplete = true;
        public EnemyOptions enemyRewards = EnemyOptions.Disabled;
        public bool challengeRewards = false;
        public bool pRankRewards = false;
        public bool hankRewards = false;
        public bool clashReward = false;
        public bool fishRewards = false;
        public bool cleanRewards = false;
        public bool chessReward = false;
        public bool rocketReward = false;
        public bool hasArm = true;
        public int dashes = 3;
        public int walljumps = 3;
        public bool canSlide = true;
        public bool canSlam = true;
        public int multiplier = 1;

        public WeaponForm revForm = WeaponForm.Standard;
        public bool revstd = true;
        public bool revalt = false;

        public WeaponForm shoForm = WeaponForm.Standard;
        public bool shostd = true;
        public bool shoalt = false;

        public WeaponForm naiForm = WeaponForm.Standard;
        public bool naistd = true;
        public bool naialt = false;

        public Fire2Options randomizeFire2 = Fire2Options.Disabled;
        public HashSet<string> unlockedFire2 = new HashSet<string>();

        public bool randomizeSkulls = false;
        public HashSet<string> unlockedSkulls = new HashSet<string>();
        public int unlockedSkulls1_4 = 0;
        public int unlockedSkulls5_1 = 0;

        public bool l1switch = false;
        public bool l7switch = false;
        public bool[] limboSwitches = new bool[4];
        public bool[] shotgunSwitches = new bool[3];

        public bool musicRandomizer = false;
        public Dictionary<string, string> music = new Dictionary<string, string>();

        public bool cybergrindHints = true;

        public bool deathLink = false;
        public int deathLinkAmnesty = 1;

        public override string ToString()
        {
            if (!slot_name.IsNullOrWhiteSpace()) return @checked.Count == 1
                    ? $"Slot {GameProgressSaver.currentSlot + 1} | {slot_name} | {@checked.Count} location checked."
                    : $"Slot {GameProgressSaver.currentSlot + 1} | {slot_name} | {@checked.Count} locations checked.";
            return "No data for current slot.";
        }
    }
}
