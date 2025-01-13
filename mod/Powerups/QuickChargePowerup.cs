using ArchipelagoULTRAKILL.Components;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Powerups
{
    public class QuickChargePowerup : MonoBehaviour
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
            meter.powerUpColor = ColorBlindSettings.Instance.staminaColor;
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
