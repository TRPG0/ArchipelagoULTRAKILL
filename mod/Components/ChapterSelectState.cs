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
                if (Core.data.randomizeSkulls)
                {
                    Core.Logger.LogInfo("Skulls randomized. Updating skull icons.");
                    UIManager.UpdateSkullIcons();
                }
            }
            if (UIManager.actStats != null) UIManager.actStats.gameObject.SetActive(false);
        }
    }
}
