using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using System.Collections.Generic;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Powerups
{
    public class ConfusionPowerup : MonoBehaviour
    {
        private PowerUpMeter meter;
        public float juiceAmount;
        private bool juiceGiven;
        internal static Dictionary<EnemyIdentifier, bool> previousState = new Dictionary<EnemyIdentifier, bool>();

        private void Start()
        {
            meter = PowerUpMeter.Instance;
            if (juiceAmount == 0f) juiceAmount = 15f;
            if (meter.juice < juiceAmount)
            {
                meter.latestMaxJuice = juiceAmount;
                meter.juice = juiceAmount;
            }
            meter.powerUpColor = Colors.Confusion;
            juiceGiven = true;

            foreach (EnemyIdentifier ei in FindObjectsOfType<EnemyIdentifier>())
            {
                previousState.Add(ei, ei.madness);
                ei.madness = true;
            }
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
            foreach (EnemyIdentifier ei in FindObjectsOfType<EnemyIdentifier>())
            {
                if (previousState.ContainsKey(ei)) ei.madness = previousState[ei];
                else Core.Logger.LogWarning($"Couldn't find previous state for enemy with name {ei.gameObject.name}");
            }
            PlayerHelper.Instance.EndPowerup();
            previousState.Clear();
            Destroy(gameObject);
        }
    }
}
