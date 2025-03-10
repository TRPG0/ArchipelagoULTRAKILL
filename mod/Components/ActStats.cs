﻿using ArchipelagoULTRAKILL.Structures;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArchipelagoULTRAKILL.Components
{
    public class ActStats : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public int StartLevelId { get; private set; }
        public int EndLevelId { get; private set; }

        public bool Special { get; private set; }

        public void Init(int start, int end, bool special = false)
        {
            StartLevelId = start;
            EndLevelId = end;
            Special = special;
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

        public int LimboSwitchesPressed()
        {
            int total = 0;
            for (int i = 6; i <= 9; i++)
            {
                if (Core.data.@checked.Contains($"{i}_sw")) total++;
            }
            return total;
        }

        public int ShotgunSwitchesPressed()
        {
            int total = 0;
            for (int i = 1; i <= 3; i++)
            {
                if (Core.data.@checked.Contains($"27_sw{i}")) total++;
            }
            return total;
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

        public int HankAssembled()
        {
            int total = 0;
            if (ActIncludes(9) && Core.data.@checked.Contains("9_ha")) total++;
            else if (ActIncludes(22) && Core.data.@checked.Contains("22_ha")) total++;
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
                bool isUnlocked = Core.data.unlockedLevels.Contains(Core.GetLevelNameFromId(i));
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
            if (ActIncludes(9) && Core.data.l1switch) result += BuildString("\nSwitches pressed", LimboSwitchesPressed(), 4);
            if (ActIncludes(27) && Core.data.l7switch) result += BuildString("\nSwitches pressed", ShotgunSwitchesPressed(), 3);
            if (ActIncludes(9) && Core.data.hankRewards) result += BuildString("\nAssembled Hank", HankAssembled(), 1);
            if (ActIncludes(22) && Core.data.hankRewards) result += BuildString("\nAssembled Hank Jr.", HankAssembled(), 1);
            if (ActIncludes(20) && Core.data.fishRewards) result += BuildString("\nFish", FishCaught(), 12);
            if (ActIncludes(28) && Core.data.cleanRewards) result += BuildString("\nRooms cleaned", RoomsCleaned(), 5);

            return result;
        }

        public string GetSpecialStats()
        {
            int total = EndLevelId - (StartLevelId - 1);
            int unlocked = 0;
            int completed = 0;
            int perfects = 0;

            for (int i = StartLevelId; i <= EndLevelId; i++)
            {
                RankData rank = GameProgressSaver.GetRank(i);
                bool isUnlocked = Core.data.unlockedLevels.Contains(Core.GetLevelNameFromId(i));
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
            }

            string result = BuildStringGoal(Core.data.goal, Core.data.completedLevels.Count, Core.data.goalRequirement);
            result += BuildString("Levels unlocked", unlocked, total);
            result += BuildString("\nLevels completed", completed, total);
            if (Core.data.pRankRewards) result += BuildString("\nPerfect Ranks", perfects, total);

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
            if (goal.Contains("-S"))
            {
                int secretMissionLevel = 0;
                switch (goal)
                {
                    case "0-S":
                        secretMissionLevel = 2;
                        break;
                    case "1-S":
                        secretMissionLevel = 6;
                        break;
                    case "2-S":
                        secretMissionLevel = 12;
                        break;
                    case "4-S":
                        secretMissionLevel = 17;
                        break;
                    case "5-S":
                        secretMissionLevel = 20;
                        break;
                    case "7-S":
                        secretMissionLevel = 28;
                        break;
                    default:
                        break;
                }

                if (!ActIncludes(secretMissionLevel)) return "";

                if (GameProgressSaver.GetSecretMission(int.Parse(Core.data.goal.Substring(0, 1))) >= 2) return "Goal: <color=green>Completed</color>\n";

                string color = "red";
                if (value >= total) color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.Perfect);

                return $"Goal: <color={color}>{value}/{total}</color>\n";
            }
            else
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
        }

        public void ShowStats()
        {
            if (UIManager.actStats == null)
            {
                Core.Logger.LogError("UIManager.actStats is null!");
                return;
            }

            Vector3 actPos = transform.position;
            UIManager.actStats.transform.position = new Vector3(UIManager.actStats.transform.position.x, actPos.y - UIManager.actStats.fontSize, UIManager.actStats.transform.position.z);

            if (Special) UIManager.actStats.text = GetSpecialStats();
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
