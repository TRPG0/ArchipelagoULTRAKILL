using UnityEngine;

namespace ArchipelagoULTRAKILL
{
    public class OptionsMenuState : MonoBehaviour
    {
        private void OnDisable()
        {
            if (Core.DataExists() && UIManager.log != null) UIManager.AdjustLogBounds();
        }
    }
}
