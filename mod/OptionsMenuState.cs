using UnityEngine;

namespace ArchipelagoULTRAKILL
{
    public class OptionsMenuState : MonoBehaviour
    {
        private void OnDisable()
        {
            if (Core.DataExists() && UIManager.log != null) UIManager.AdjustLogBounds();
            if (SceneHelper.CurrentScene == "Main Menu" && UIManager.goalCount != null && !Core.data.goal.Contains("P"))
            {
                if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) UIManager.goalCount.transform.localPosition = new Vector3(0, 90, 0);
                else UIManager.goalCount.transform.localPosition = new Vector3(0, 22.5f, 0);
            }
        }
    }
}
