using ArchipelagoULTRAKILL.Structures;
using Discord;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Components
{
    public class SkullIcon : MonoBehaviour
    {
        public string Id { get; private set; }
        public bool Reposition { get; private set; }

        public const int yLeaderboard = 140;
        public const int yNormal = 65;

        public void SetId(string id, bool reposition = true)
        {
            Id = id;
            Reposition = reposition;
        }

        public void Start()
        {
            if (!GetComponent<Image>())
            {
                Core.Logger.LogWarning($"No image found for skull icon {name}");
                Destroy(this);
            }
        }

        public void OnEnable()
        {
            if (Reposition)
            {
                if (GameStateManager.ShowLeaderboards) transform.localPosition = new Vector3(transform.localPosition.x, yLeaderboard, transform.localPosition.z);
                else transform.localPosition = new Vector3(transform.localPosition.x, yNormal, transform.localPosition.z);
            }
            CheckSkull();
        }

        public void CheckSkull()
        {
            if (Id.Contains("9") && !Id.Contains("19"))
            {
                if (Core.data.unlockedSkulls1_4 >= int.Parse(Id.Substring(Id.Length - 1, 1))) GetComponent<Image>().color = Colors.BlueSkull;
                else GetComponent<Image>().color = Colors.Gray;
            }
            else if (Id.Contains("20"))
            {
                if (Core.data.unlockedSkulls5_1 >= int.Parse(Id.Substring(Id.Length - 1, 1))) GetComponent<Image>().color = Colors.BlueSkull;
                else GetComponent<Image>().color = Colors.Gray;
            }
            else
            {
                if (Core.data.unlockedSkulls.Contains(Id))
                {
                    if (Id.Contains("_b")) GetComponent<Image>().color = Colors.BlueSkull;
                    else if (Id.Contains("_r")) GetComponent<Image>().color = Colors.RedSkull;
                }
                else
                {
                    GetComponent<Image>().color = Colors.Gray;
                }
            }
        }
    }
}
