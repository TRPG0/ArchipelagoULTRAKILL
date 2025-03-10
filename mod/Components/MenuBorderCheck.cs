using System.Collections;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class MenuBorderCheck : MonoBehaviour
    {
        public void OnEnable()
        {
            StartCoroutine(Resize());
        }

        public IEnumerator Resize()
        {
            yield return new WaitForEndOfFrame();
            if (UIManager.log != null) UIManager.log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height * 0.98f - 10);
        }

        public void OnDisable()
        {
            if (UIManager.log != null) UIManager.log.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 10, Screen.height - 10);
        }
    }
}
