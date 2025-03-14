using TMPro;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class RocketRaceCheck : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public GameObject button;

        public bool CanRocketRace => GameProgressSaver.GetGeneralProgress().rock0 > 0 && Core.IsFire2Unlocked("rock0");

        public void Awake()
        {
            text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            button = transform.Find("OpenButton").gameObject;
        }

        public void OnEnable()
        {
            if (!CanRocketRace)
            {
                button.SetActive(false);
                text.text = "\n\n\nCAN'T ROCKET RACE WITH NO ROCKET, DUMBASS";
            }
            else
            {
                button.SetActive(true);
                text.text = "ROCKET RACE";
            }
        }
    }
}
