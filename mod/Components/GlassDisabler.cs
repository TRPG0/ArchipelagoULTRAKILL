﻿using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class GlassDisabler : MonoBehaviour
    {
        private void OnEnable()
        {
            if (!Core.CanBreakGlass())
            {
                foreach (Glass glass in gameObject.GetComponentsInChildren<Glass>())
                {
                    glass.transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
}
