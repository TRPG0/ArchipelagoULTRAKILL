using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class ChapterSelectState : MonoBehaviour
    {
        private void OnDisable()
        {
            if (Core.DataExists())
            {
                Core.Logger.LogInfo("Chapter Select inactive. Updating levels.");
                UIManager.UpdateLevels();
            }
            UIManager.actStats?.gameObject.SetActive(false);
        }
    }
}
