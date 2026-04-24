using ArchipelagoULTRAKILL.Structures;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArchipelagoULTRAKILL.Components
{
    public class ActStats : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public static List<ActStats> All { get; private set; } = new List<ActStats>();

        public int StartLevelId { get; private set; }
        public int EndLevelId { get; private set; }

        public bool Special { get; private set; }

        private bool dirty = true;
        private string result = "";

        public static void SetAllDirty()
        {
            foreach (ActStats actStats in All) actStats.dirty = true;
        }

        public void Init(int start, int end, bool special = false)
        {
            StartLevelId = start;
            EndLevelId = end;
            Special = special;
            All.Add(this);
        }

        public void OnDestroy()
        {
            All.Remove(this);
        }

        public bool ActIncludes(int id)
        {
            if (id >= StartLevelId && id <= EndLevelId) return true;
            return false;
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
            int exitsCompleted = 0;
            int exitsTotal = 0;
            int secretsFound = 0;
            int secretsTotal = 0;
            int challenges = 0;
            int perfects = 0;
            int weapons = 0;
            int weaponsTotal = 0;
            int secretWeapons = 0;
            int secretWeaponsTotal = 0;

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

                LevelInfo levelInfo = Core.GetLevelInfo(i);

                if (levelInfo.Flags.HasFlag(InfoFlags.HasSecrets))
                {
                    foreach (bool found in rank.secretsFound)
                    {
                        if (found) secretsFound++;
                    }
                    secretsTotal += 5;
                }

                if (SecretMissionCompleted(i).HasValue)
                {
                    missionsCompleted += SecretMissionCompleted(i).Value;
                    missionsTotal++;
                }

                if (levelInfo.Flags.HasFlag(InfoFlags.HasSecretExit))
                {
                    if (Core.data.@checked.Contains($"se_{levelInfo.Layer}")) exitsCompleted++;
                    exitsTotal++;
                }

                if (levelInfo.Flags.HasFlag(InfoFlags.HasWeapon))
                {
                    if (Core.data.@checked.Contains($"{levelInfo.Id}_w1")) weapons++;
                    weaponsTotal++;
                }

                if (levelInfo.Flags.HasFlag(InfoFlags.HasSecretWeapon))
                {
                    if (Core.data.@checked.Contains($"{levelInfo.Id}_w2")) secretWeapons++;
                    secretWeaponsTotal++;
                }

                if (rank.challenge) challenges++;
            }

            string result = BuildStringGoal(Core.data.goal, Core.data.completedLevels.Count, Core.data.goalRequirement);
            result += BuildString("Levels unlocked", unlocked, total);
            result += BuildString("\nLevels completed", completed, total);
            result += BuildString("\nSecret missions", missionsCompleted, missionsTotal);
            if (!Core.data.secretExitComplete) result += BuildString("\nSecret exits", exitsCompleted, exitsTotal);
            result += BuildString("\nSecrets", secretsFound, secretsTotal);
            if (weaponsTotal > 0) result += BuildString("\nWeapons", weapons, weaponsTotal);
            if (secretWeaponsTotal > 0) result += BuildString("\nSecret weapons", secretWeapons, secretWeaponsTotal);
            if (Core.data.challengeRewards) result += BuildString("\nChallenges", challenges, total);
            if (Core.data.pRankRewards) result += BuildString("\nPerfect Ranks", perfects, total);
            if (ActIncludes(9) && Core.data.l1switch) result += BuildString("\nSwitches pressed", LimboSwitchesPressed(), 4);
            if (ActIncludes(27) && Core.data.l7switch) result += BuildString("\nSwitches pressed", ShotgunSwitchesPressed(), 3);
            if (ActIncludes(9) && Core.data.hankRewards) result += BuildString("\nAssembled Hank", HankAssembled(), 1);
            if (ActIncludes(22) && Core.data.hankRewards) result += BuildString("\nAssembled Hank Jr.", HankAssembled(), 1);
            if (ActIncludes(20) && Core.data.fishRewards) result += BuildString("\nFish", FishCaught(), 12);
            if (ActIncludes(28) && Core.data.cleanRewards) result += BuildString("\nRooms cleaned", RoomsCleaned(), 5);

            dirty = false;
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

            dirty = false;
            return result;
        }

        public string BuildString(string part, int value, int total)
        {
            string color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.ActHighlight);
            if (value >= total) color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.ActComplete);

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

                if (GameProgressSaver.GetSecretMission(int.Parse(Core.data.goal.Substring(0, 1))) >= 2) return $"Goal: <color=#{ColorUtility.ToHtmlStringRGBA(Colors.ActGoal)}>Completed</color>\n";

                string color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.ActHighlight);
                if (value >= total) color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.ActComplete);

                return $"Goal: <color={color}>{value}/{total}</color>\n";
            }
            else
            {
                if (!ActIncludes(Core.GetLevelIdFromName(goal))) return "";

                RankData rank = GameProgressSaver.GetRank(Core.GetLevelIdFromName(goal));
                foreach (int grade in rank.ranks)
                {
                    if (Core.data.perfectGoal)
                    {
                        if (grade >= 12)
                        {
                            return $"Goal: <color=#{ColorUtility.ToHtmlStringRGBA(Colors.ActGoal)}>Completed</color>\n";
                        }
                    }
                    else
                    {
                        if (grade >= 0)
                        {
                            return $"Goal: <color=#{ColorUtility.ToHtmlStringRGBA(Colors.ActGoal)}>Completed</color>\n";
                        }
                    }
                }

                string color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.ActHighlight);
                if (value >= total) color = "#" + ColorUtility.ToHtmlStringRGBA(Colors.ActComplete);

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

            if (dirty)
            {
                if (Special) result = GetSpecialStats();
                else result = GetActStats();
            }

            UIManager.actStats.text = result;

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
