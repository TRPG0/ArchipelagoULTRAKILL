using ArchipelagoULTRAKILL.Structures;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Components
{
    public class SwitchIcon : MonoBehaviour
    {
        public int Id { get; private set; }

        public bool Limbo { get; private set; }

        public void SetId(int id, bool limbo = true)
        {
            Id = id;
            Limbo = limbo;
        }

        public void Start()
        {
            if (!GetComponent<Image>())
            {
                Core.Logger.LogWarning($"No image found for switch icon {name}");
                Destroy(this);
            }
        }

        public void OnEnable()
        {
            CheckSwitch();
        }

        public void CheckSwitch()
        {
            if (Limbo)
            {
                if (Core.data.limboSwitches[Id]) GetComponent<Image>().color = Colors.Switch;
                else GetComponent<Image>().color = Colors.Gray;
            }
            else
            {
                if (Core.data.shotgunSwitches[Id]) GetComponent<Image>().color = Colors.Switch;
                else GetComponent<Image>().color = Colors.Gray;
            }
        }
    }
}
