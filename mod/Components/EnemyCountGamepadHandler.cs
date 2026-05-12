using UnityEngine;
using UnityEngine.EventSystems;

namespace ArchipelagoULTRAKILL.Components
{
    public class EnemyCountGamepadHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public EnemyList enemyList;

        public void OnDisable() => enemyList.gameObject.SetActive(false);

        public void OnSelect(BaseEventData eventData) => enemyList.gameObject.SetActive(true);
        public void OnDeselect(BaseEventData eventData) => enemyList.gameObject.SetActive(false);
    }
}
