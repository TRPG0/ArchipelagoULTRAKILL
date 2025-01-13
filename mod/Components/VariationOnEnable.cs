using System.Collections;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class VariationOnEnable : MonoBehaviour
    {
        VariationInfo variationInfo;

        public void Awake()
        {
            if (TryGetComponent<VariationInfo>(out var vi))
            {
                variationInfo = vi;
            }
            else
            {
                Core.Logger.LogWarning($"No variation info found for object \"{gameObject.name}\"");
                DestroyImmediate(this);
            }
        }

        public void OnEnable()
        {
            StartCoroutine(DelayedUpdate());
        }

        public IEnumerator DelayedUpdate()
        {
            yield return new WaitForEndOfFrame();
            LevelManager.UpdateShopVariation(variationInfo);
        }
    }
}
