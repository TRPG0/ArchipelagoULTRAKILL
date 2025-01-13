using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(DifficultyTitle), "Check")]
    public class DifficultyTitle_Check_Patch
    {
        public static void Postfix(DifficultyTitle __instance)
        {
            Traverse traverse = Traverse.Create(__instance);
            if (Core.DataExists() && __instance.lines && SceneHelper.CurrentScene == "Main Menu")
            {
                if (traverse.Field<TMP_Text>("txt2").Value != null)
                {
                    traverse.Field<TMP_Text>("txt2").Value.alignment = TextAlignmentOptions.Top;

                    string startColor = ColorUtility.ToHtmlStringRGBA(LocationManager.GetUKMessageColor(Core.data.start));
                    string goalColor = ColorUtility.ToHtmlStringRGBA(LocationManager.GetUKMessageColor(Core.data.goal));

                    traverse.Field<TMP_Text>("txt2").Value.text = traverse.Field<TMP_Text>("txt2").Value.text + $"\nSTART: <color=#{startColor}>{Core.data.start}</color> | GOAL: <color=#{goalColor}>{Core.data.goal}</color>";
                }
            }
            else traverse.Field<TMP_Text>("txt2").Value.alignment = TextAlignmentOptions.Center;
        }
    }
}
