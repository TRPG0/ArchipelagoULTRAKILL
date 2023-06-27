using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class ChapterSelectState : MonoBehaviour
    {
        private void OnDisable()
        {
            if (Core.DataExists())
            {
                Core.logger.LogInfo("Chapter Select inactive. Updating levels.");
                UIManager.UpdateLevels();
                if (Core.data.randomizeSkulls)
                {
                    Core.logger.LogInfo("Skulls randomized. Updating skull icons.");
                    UIManager.UpdateSkullIcons();
                }
            }
        }
    }
}
