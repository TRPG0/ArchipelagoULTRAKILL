using ArchipelagoULTRAKILL.Structures;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Components
{
    public class SwitchIcon : MonoBehaviour
    {
        public int Id { get; private set; }

        public bool Limbo { get; private set; }

        public bool Reposition { get; private set; }

        public const int yLeaderboard = 38;
        public const int yNormal = -32;

        public void SetId(int id, bool limbo = true, bool reposition = false)
        {
            Id = id;
            Limbo = limbo;
            Reposition = reposition;
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
            if (Reposition)
            {
                if (PrefsManager.Instance.GetBool("levelLeaderboards", true)) transform.localPosition = new Vector3(transform.localPosition.x, yLeaderboard, transform.localPosition.z);
                else transform.localPosition = new Vector3(transform.localPosition.x, yNormal, transform.localPosition.z);
            }
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
