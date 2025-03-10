using System.Collections;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class FirstLevelSetup : MonoBehaviour
    {
        public void OnEnable()
        {
            StartCoroutine(DoSetup());
        }

        public IEnumerator DoSetup()
        {
            yield return new WaitUntil(() => GunSetter.Instance != null && GunControl.Instance != null && FistControl.Instance != null);
            GunSetter.Instance.ResetWeapons();
            GunControl.Instance.UpdateWeaponList();
            GunControl.Instance.YesWeapon();
            FistControl.Instance.ResetFists();
            LevelManager.AddGlassComponents();
            Component.Destroy(this);
        }
    }
}
