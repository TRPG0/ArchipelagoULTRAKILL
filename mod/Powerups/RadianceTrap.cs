using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Powerups
{
    public class RadianceTrap : MonoBehaviour
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

            OptionsManager.forceRadiance = true;
            float tier = PrefsManager.Instance.GetInt("difficulty", 0) < 2 ? 0.5f : 1f;
            OptionsManager.radianceTier = tier;
            foreach (EnemyIdentifier ei in FindObjectsOfType<EnemyIdentifier>())
            {
                ei.UpdateBuffs(false);
            }
        }

        private void Update()
        {
            meter.powerUpColor = Color.HSVToRGB(Mathf.Repeat(Time.fixedTime, 1), 0.4f, 1);

            if (juiceGiven && meter.juice <= 0f)
            {
                EndPowerUp();
                return;
            }
        }

        public void EndPowerUp()
        {
            OptionsManager.forceRadiance = false;
            foreach (EnemyIdentifier ei in FindObjectsOfType<EnemyIdentifier>())
            {
                ei.UpdateBuffs(false);
            }
            PlayerHelper.Instance.EndPowerup();
            Destroy(gameObject);
        }
    }
}
