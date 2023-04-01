using UnityEngine;

namespace ArchipelagoULTRAKILL
{
    public class ChapterSelectState : MonoBehaviour
    {
        private void OnDisable()
        {
            if (APULTRAKILL.data.active)
            {
                Debug.Log("Chapter Select inactive. Updating levels.");
                APULTRAKILL.UpdateLevels();
            }
        }
    }
}
