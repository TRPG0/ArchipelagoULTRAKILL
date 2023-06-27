using HarmonyLib;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class ReverseDoorCloser : MonoBehaviour
    {
        private void OnEnable()
        {

            foreach (ItemPlaceZone ipz in gameObject.GetComponentsInChildren<ItemPlaceZone>())
            {
                if (ipz.reverseDoors.Count() > 0)
                {
                    for (int i = 0; i < ipz.reverseDoors.Count(); i++)
                    {
                        //ipz.reverseDoors[i].Close(true);
                        StartCoroutine(Close(ipz.reverseDoors[i]));
                    }
                }

                if (SceneHelper.CurrentScene == "Level 1-2" && Traverse.Create(ipz).Field<bool>("acceptedItemPlaced").Value == false)
                {
                    ipz.activateOnFailure = ipz.activateOnSuccess;
                    ipz.activateOnSuccess = null;
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
