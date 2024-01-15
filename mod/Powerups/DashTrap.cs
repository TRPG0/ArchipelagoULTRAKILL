using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Powerups
{
    public class DashTrap : MonoBehaviour
    {
        private PowerUpMeter meter;
        public float juiceAmount;
        private bool juiceGiven;

        private void Start()
        {
            meter = PowerUpMeter.Instance;
            if (juiceAmount == 0f) juiceAmount = 15f;
            if (meter.juice < juiceAmount)
            {
                meter.latestMaxJuice = juiceAmount;
                meter.juice = juiceAmount;
            }
            meter.powerUpColor = Colors.Trap;
            juiceGiven = true;
        }

        private void Update()
        {
            if (juiceGiven && meter.juice <= 0f)
            {
                EndPowerUp();
                return;
            }
        }

        public void EndPowerUp()
        {
            PlayerHelper.Instance.EndPowerup();
            Destroy(gameObject);
        }
    }
}
