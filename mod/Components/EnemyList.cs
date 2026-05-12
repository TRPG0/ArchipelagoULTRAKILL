using System.Collections;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class EnemyList : MonoBehaviour
    {
        public RectTransform childRect;
        public Canvas listCanvas;
        public int id;

        public void Start()
        {
            StartCoroutine(DelayDeactivate());
        }

        public IEnumerator DelayDeactivate()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            gameObject.SetActive(false);
        }

        public void OnEnable() => StartCoroutine(Adjust());

        public IEnumerator Adjust()
        {
            yield return new WaitForEndOfFrame();
            GetComponent<RectTransform>().sizeDelta = childRect.sizeDelta;

            listCanvas.overrideSorting = true;
            listCanvas.sortingOrder = 200;

            if (transform.parent.position.x >= (Screen.safeArea.width / 2))
            {
                GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
                if (id >= 666) transform.localPosition = new Vector3(-190, 0, 0);
                else transform.localPosition = new Vector3(-120, 0, 0);
            }
            else
            {
                GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
                if (id >= 666) transform.localPosition = new Vector3(190, 0, 0);
                else transform.localPosition = new Vector3(120, 0, 0);
            }
        }
    }
}
