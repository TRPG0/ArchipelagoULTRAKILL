using System.Collections.Generic;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class LinkedDisabler : MonoBehaviour
    {
        public List<GameObject> objects = new List<GameObject>();

        public void OnDisable()
        {
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }
}
