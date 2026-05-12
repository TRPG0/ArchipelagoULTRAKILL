using ArchipelagoULTRAKILL.Structures;
using Discord;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Components
{
    public class SkullIcon : MonoBehaviour
    {
        public string Id { get; private set; }

        public void SetId(string id)
        {
            Id = id;
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
