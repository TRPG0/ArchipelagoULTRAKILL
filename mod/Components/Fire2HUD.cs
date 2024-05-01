using ArchipelagoULTRAKILL.Structures;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Components
{
    public class Fire2HUD : MonoBehaviour
    {
        public static Fire2HUD Instance { get; private set; }

        public Image main;
        public Image mainGlow;
        public Image sec;
        public Image secGlow;
        public Image alt1;
        public GameObject alt1shadow;
        public Image alt2;
        public GameObject alt2shadow;

        public float timer = 0f;

        public string CurrentWeapon { get; private set; }
        public bool CurrentIsAlternate { get; private set; }
        public bool CurrentIsUnlocked { get; private set; }

        public void Awake()
        {
            if (!GetComponent<WeaponHUD>() || PlayerHelper.Instance == null || Instance != null) DestroyImmediate(this);
            Instance = this;

            main = GetComponent<Image>();
            mainGlow = transform.GetChild(0).GetComponent<Image>();
            sec = GameObject.Instantiate(mainGlow.gameObject, transform).GetComponent<Image>();
            sec.gameObject.name = "Secondary";
            Component.Destroy(sec.GetComponent<Animator>());
            secGlow = GameObject.Instantiate(mainGlow.gameObject, transform).GetComponent<Image>();
            secGlow.gameObject.name = "Secondary Glow";
            Component.Destroy(secGlow.GetComponent<Animator>());
        }

        public void Start()
        {
            UpdateCurrentWeapon();
            if (Object.FindObjectOfType<WeaponIcon>()) Object.FindObjectOfType<WeaponIcon>().UpdateIcon();
            else Core.Logger.LogWarning("Couldn't find WeaponIcon for Fire2HUD!");
        }

        public void UpdateCurrentWeapon()
        {
            List<string> ignoredWeapons = new List<string>()
            { 
                "rai0",
                "rai1",
                "rai2"
            };

            CurrentWeapon = PlayerHelper.Instance.GetHeldWeapon();
            if (ignoredWeapons.Contains(CurrentWeapon)) CurrentWeapon = "?";

            CurrentIsAlternate = PlayerHelper.Instance.IsWeaponAlternate();
            CurrentIsUnlocked = Core.IsFire2Unlocked(CurrentWeapon);

            if (CurrentWeapon == "?")
            {
                sec.gameObject.SetActive(false);
                secGlow.gameObject.SetActive(false);
                if (alt1 != null) alt1.gameObject.SetActive(false);
                if (alt1shadow != null) alt1shadow.SetActive(false);
                if (alt2 != null) alt2.gameObject.SetActive(false);
                if (alt2shadow != null) alt2shadow.SetActive(false);
                return;
            }
            else
            {
                sec.gameObject.SetActive(true);
                secGlow.gameObject.SetActive(true);
                if (alt1 != null) alt1.gameObject.SetActive(true);
                if (alt1shadow != null) alt1shadow.SetActive(true);
                if (alt2 != null) alt2.gameObject.SetActive(true);
                if (alt2shadow != null) alt2shadow.SetActive(true);
            }

            if (CurrentIsUnlocked)
            {
                sec.color = main.color;
                secGlow.color = mainGlow.color;
                if (alt1 != null) alt1.color = main.color;
                if (alt2 != null) alt2.color = main.color;
            }
            else
            {
                sec.color = Colors.Gray;
                secGlow.color = Colors.Gray;
                if (alt1 != null) alt1.color = Colors.Gray;
                if (alt2 != null) alt2.color = Colors.Gray;
            }
        }

        public void Update()
        {
            Color c = secGlow.color;
            c.a = mainGlow.color.a;
            secGlow.color = c;

            if (!NewMovement.Instance.isActiveAndEnabled) return;

            if (InputManager.Instance.InputSource.Fire2.WasPerformedThisFrame && !FistControl.Instance.shopping && CurrentWeapon != "?" && !CurrentIsUnlocked)
            {
                timer = 1.5f;
            }

            if (timer > 0f)
            {
                Color color1 = Color.Lerp(Colors.Gray, Colors.Red, timer);
                Color color2 = color1;
                color2.a = mainGlow.color.a;
                sec.color = color1;
                alt1.color = color1;
                alt2.color = color2;
                secGlow.color = color2;
                timer -= Time.deltaTime * 2;
            }

            if (timer < 0f)
            {
                timer = 0f;
            }
        }
    }
}
