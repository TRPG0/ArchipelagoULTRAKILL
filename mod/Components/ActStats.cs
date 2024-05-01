using ArchipelagoULTRAKILL.Structures;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArchipelagoULTRAKILL.Components
{
    public class ActStats : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public int StartLevelId { get; private set; }
        public int EndLevelId { get; private set; }

        public bool Prime { get; private set; }

        public void Init(int start, int end, bool prime = false)
        {
            StartLevelId = start;
            EndLevelId = end;
            Prime = prime;
        }

        public bool ActIncludes(int id)
        {
            if (id >= StartLevelId && id <= EndLevelId) return true;
            return false;
        }

        public int TotalSecretsInLevel(int id)
        {
            if (id == 3 || id == 4) return 3;
            return 5;
        }

        public int? SecretMissionCompleted(int id)
        {
            switch (id)
            {
                case 2:
                case 6:
                case 12:
                case 17:
                case 20:
                case 28:
                    if (GameProgressSaver.GetGeneralProgress().secretMissions[Core.GetLevelInfo(id).Layer] >= 2) return 1;
                    return 0;
                default:
                    return null;
            }
        }

        public int? BossDefeated(int id)
        {
            if (Core.data.bossRewards == BossOptions.Disabled) return null;
            else
            {
                switch (id)
                {
                    case 5:
                    case 9:
                    case 13:
                    case 15:
                    case 19:
                    case 23:
                    case 25:
                    case 29:
                        if (Core.data.@checked.Contains($"{id}_b")) return 1;
                        return 0;
                    case 7:
                    case 18:
                        if (Core.data.bossRewards < BossOptions.Extended) return null;
                        if (Core.data.@checked.Contains($"{id}_b")) return 1;
                        return 0;
                    default: 
                        return null;
                }
            }
        }

        public int FishCaught()
        {
            int total = 0;
            for (int i = 0; i <= 11; i++)
            {
                if (Core.data.@checked.Contains($"fish{i}")) total++;
            }
            return total;
        }

        public int RoomsCleaned()
        {
            int total = 0;
            for (int i = 0; i <= 4; i++)
            {
                if (Core.data.@checked.Contains($"clean{i}")) total++;
            }
            return total;
        }

        public string GetActStats()
        {
            int total = EndLevelId - (StartLevelId - 1);
            int unlocked = 0;
            int completed = 0;
            int missionsCompleted = 0;
            int missionsTotal = 0;
            int secretsFound = 0;
            int secretsTotal = 0;
            int bossesDefeated = 0;
            int bossesTotal = 0;
            int challenges = 0;
            int perfects = 0;

            for (int i = StartLevelId; i <= EndLevelId; i++)
            {
                RankData rank = GameProgressSaver.GetRank(i);
                bool isUnlocked = Core.data.unlockedLevels.Contains(Core.GetLevelNameFromId(i)) || i == 1;
                if (isUnlocked) unlocked++;

                bool isCompleted = false;
                bool isPerfect = false;
                foreach (int grade in rank.ranks)
                {
                    if (grade >= 0)
                    {
                        if (!isCompleted) completed++;
                        isCompleted = true;
                    }
                    if (grade >= 12)
                    {
                        if (!isPerfect) perfects++;
                        isPerfect = true;
                    }
                }

                if (Core.GetLevelInfo(i).HasSecrets)
                {
                    foreach (bool found in rank.secretsFound)
                    {
                        if (found) secretsFound++;
                    }
                    secretsTotal += TotalSecretsInLevel(i);
                }

                if (SecretMissionCompleted(i).HasValue)
                {
                    missionsCompleted += SecretMissionCompleted(i).Value;
                    missionsTotal++;
                }

                if (BossDefeated(i).HasValue)
                {
                    bossesDefeated += BossDefeated(i).Value;
                    bossesTotal++;
                }

                if (rank.challenge) challenges++;
            }

            string result = BuildStringGoal(Core.data.goal, Core.data.completedLevels.Count, Core.data.goalRequirement);
            result += BuildString("Levels unlocked", unlocked, total);
            result += BuildString("\nLevels completed", completed, total);
            result += BuildString("\nSecret missions", missionsCompleted, missionsTotal);
            result += BuildString("\nSecrets", secretsFound, secretsTotal);
            if (Core.data.challengeRewards) result += BuildString("\nChallenges", challenges, total);
            if (Core.data.pRankRewards) result += BuildString("\nPerfect Ranks", perfects, total);
            if (Core.data.bossRewards > BossOptions.Disabled) result += BuildString("\nBosses", bossesDefeated, bossesTotal);
            if (ActIncludes(20) && Core.data.fishRewards) result += BuildString("\nFish", FishCaught(), 12);
            if (ActIncludes(28) && Core.data.cleanRewards) result += BuildString("\nRooms cleaned", RoomsCleaned(), 5);

            return result;
        }

        public string GetPrimeStats()
        {
            int total = EndLevelId - (StartLevelId - 1);
            int unlocked = 0;
            int completed = 0;

            for (int i = StartLevelId; i <= EndLevelId; i++)
            {
                RankData rank = GameProgressSaver.GetRank(i);
                bool isUnlocked = Core.data.unlockedLevels.Contains(Core.GetLevelNameFromId(i));
                if (isUnlocked) unlocked++;

                bool isCompleted = false;
                foreach (int grade in rank.ranks)
                {
                    if (grade >= 0)
                    {
                        if (!isCompleted) completed++;
                        isCompleted = true;
                    }
                }
            }

            string result = BuildStringGoal(Core.data.goal, Core.data.completedLevels.Count, Core.data.goalRequirement);
            result += BuildString("Levels unlocked", unlocked, total);
            result += BuildString("\nLevels completed", completed, total);

            return result;
        }

        public string BuildString(string part, int value, int total)
        {
            string color = "red";
            if (value >= total) color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.Perfect);

            return $"{part}: <color={color}>{value}/{total}</color>";
        }

        public string BuildStringGoal(string goal, int value, int total)
        {
            if (!ActIncludes(Core.GetLevelIdFromName(goal))) return "";

            RankData rank = GameProgressSaver.GetRank(Core.GetLevelIdFromName(goal));
            foreach (int grade in rank.ranks)
            {
                if (grade >= 0)
                {
                    return "Goal: <color=green>Completed</color>\n";
                }
            }

            string color = "red";
            if (value >= total) color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.Perfect);

            return $"Goal: <color={color}>{value}/{total}</color>\n";
        }

        public void ShowStats()
        {
            if (UIManager.actStats == null)
            {
                Core.Logger.LogError("UIManager.actStats is null!");
                return;
            }

            Vector3 actPos = transform.localPosition;
            UIManager.actStats.transform.localPosition = new Vector3(UIManager.actStats.transform.localPosition.x, actPos.y - UIManager.actStats.fontSize * 0.75f, UIManager.actStats.transform.localPosition.z);

            if (Prime) UIManager.actStats.text = GetPrimeStats();
            else UIManager.actStats.text = GetActStats();

            UIManager.actStats.gameObject.SetActive(true);
        }

        public void HideStats()
        {
            if (UIManager.actStats == null)
            {
                Core.Logger.LogError("UIManager.actStats is null!");
                return;
            }
            
            UIManager.actStats.gameObject.SetActive(false);
        }

        public void OnSelect(BaseEventData baseEventData)
        {
            if (Core.DataExists()) ShowStats();
            //Core.logger.LogInfo("OnSelect");
        }

        public void OnDeselect(BaseEventData baseEventData)
        {
            if (Core.DataExists()) HideStats();
            //Core.logger.LogInfo("OnDeselect");
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (Core.DataExists()) ShowStats();
            //Core.logger.LogInfo("OnPointerEnter");
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            if (Core.DataExists()) HideStats();
            //Core.logger.LogInfo("OnPointerExit");
        }
    }
}
