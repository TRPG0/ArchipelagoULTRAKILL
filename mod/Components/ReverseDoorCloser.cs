using HarmonyLib;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class ReverseDoorCloser : MonoBehaviour
    {
        private void Start()
        {

            foreach (ItemPlaceZone ipz in gameObject.GetComponentsInChildren<ItemPlaceZone>())
            {
                if (ipz.reverseDoors.Count() > 0)
                {
                    for (int i = 0; i < ipz.reverseDoors.Count(); i++)
                    {
                        StartCoroutine(Close(ipz.reverseDoors[i]));
                    }
                }

                if (SceneHelper.CurrentScene == "Level 1-2" && !Core.data.unlockedSkulls.Contains("7_b"))
                {
                    ipz.activateOnFailure = ipz.activateOnSuccess;
                    ipz.activateOnSuccess = null;
                    foreach (GameObject go in ipz.activateOnFailure)
                    {
                        go.SetActive(true);
                    }
                }
            }
        }

        private IEnumerator Close(Door door)
        {
            yield return new WaitForSeconds(0.1f);
            door.Close(true);
        }
    }
}
