using UnityEngine;
using UnityEngine.EventSystems;

namespace ArchipelagoULTRAKILL.Components
{
    public class EnemyCountPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public EnemyList enemyList;

        public void OnPointerEnter(PointerEventData eventData) => enemyList.gameObject.SetActive(true);
        public void OnPointerExit(PointerEventData eventData) => enemyList.gameObject.SetActive(false);
    }
}
