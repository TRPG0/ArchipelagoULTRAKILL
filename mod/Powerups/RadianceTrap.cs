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
            if (juiceAmount == 0f) juiceAmount = 10f;
            if (meter.juice < juiceAmount)
            {
                meter.latestMaxJuice = juiceAmount;
                meter.juice = juiceAmount;
            }
            meter.powerUpColor = ConfigManager.trapColor.value;
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
            Core.poweredUp = false;
            if (LocationManager.powerupQueue.Count > 0) Core.obj.GetComponent<Core>().Invoke("AddPowerup", 1f);
            Destroy(gameObject);
        }
    }
}
