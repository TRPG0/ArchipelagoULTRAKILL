﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ArchipelagoULTRAKILL.Components
{
    public class DeathLinkMessage : MonoBehaviour
    {
        public Text text;
        public Text youDiedText;
        public RectTransform selfRT;
        public RectTransform parentRT;
        public RectTransform skullRT;

        public static string DifficultyName
        {
            get
            {
                int difficulty = PrefsManager.Instance.GetInt("difficulty");
                switch (difficulty)
                {
                    default:
                    case 0: 
                        return "Harmless";
                    case 1: 
                        return "Lenient";
                    case 2: 
                        return "Standard";
                    case 3: 
                        return "Violent";
                    case 4: 
                        return "Brutal";
                }
            }
        }

        internal static List<string> deathMessages = new List<string>()
            {
                "{0} wasn't skilled enough.",
                "{0} ran out of blood.",
                "{0} forgot how to aim.",
                "{0} got ULTRAKILLed.",
                "Things got too " + DifficultyName.ToLower() + " for {0}."
            };

        internal static Dictionary<string, List<string>> specialMessages = new Dictionary<string, List<string>>();

        private void Awake()
        {
            specialMessages["uk_construct"] = new List<string>()
            {
                "{0} did that on purpose.",
                "{0} has some explaining to do."
            };
            specialMessages["CreditsMuseum2"] = specialMessages["uk_construct"];
            specialMessages["Level 0-S"] = new List<string>()
            {
                "{0} got caught by something.",
                "Something wicked this way came for {0}."
            };
            specialMessages["Level 1-4"] = new List<string>()
            {
                "V2 was too quick for {0}.",
                "Blue {0} couldn't beat red {0}."
            };
            specialMessages["Level 2-4"] = new List<string>()
            {
                "{0} got crushed even though they're just a little guy.",
                "{0} couldn't parry a punch."
            };
            specialMessages["Level 3-2"] = new List<string>()
            {
                "{0} crossed the will of God.",
                "{0} was rended apart.",
                "{0} beheld the power of an angel."
            };
            specialMessages["Level 4-4"] = new List<string>()
            {
                "{0} had sand in their eye.",
                "Blue {0} couldn't beat red {0}."
            };
            specialMessages["Level 5-4"] = new List<string>()
            {
                "{0} got water damaged."
            };
            specialMessages["Level 6-2"] = new List<string>()
            {
                "{0} experienced perfect hatred.",
                "{0} was ended, there and then."
            };
            specialMessages["Level 7-4"] = new List<string>()
            {
                "{0} 0 - Earthmover 1000",
                "Security threat \"{0}\" eliminated."
            };
            specialMessages["Level P-1"] = new List<string>()
            {
                "{0}'s punishment is death."
            };
            specialMessages["Level P-2"] = new List<string>()
            {
                "One must imagine {0} happy.",
            };
        }

        public void Initialize()
        {
            if (TryGetComponent(out Text component)) text = component;
            if (TryGetComponent(out RectTransform component2)) selfRT = component2;

            if (transform.parent.parent.GetChild(0).TryGetComponent(out Text component3)) youDiedText = component3;
            if (transform.parent.parent.GetChild(1).TryGetComponent(out RectTransform component4)) skullRT = component4;
            if (transform.parent.TryGetComponent(out RectTransform component5)) parentRT = component5;

            text.font = text.transform.parent.parent.Find("YouDiedText").GetComponent<Text>().font;
            text.resizeTextForBestFit = true;
            text.resizeTextMaxSize = 200;
            text.alignment = TextAnchor.UpperCenter;
        }

        private void OnEnable()
        {
            StartCoroutine(DelayAdjustPosSize());
        }

        private IEnumerator DelayAdjustPosSize()
        {
            yield return new WaitForEndOfFrame();
            parentRT.sizeDelta = new Vector2(Screen.width, Screen.height);
            parentRT.transform.localPosition = new Vector3(0, 0, 0);
            parentRT.transform.localScale = new Vector3(1, 1, 1);
            transform.localPosition = new Vector3(0, (skullRT.rect.height / 2 + 35), 0);
            transform.localScale = new Vector3(1, 1, 1);
            selfRT.sizeDelta = new Vector2(Mathf.Round((float)(Screen.width * 0.9)), Mathf.Round((float)(Screen.height * 0.05)));
        }

        private void Update()
        {
            text.color = youDiedText.color;
        }

        public void SetDeathMessage(string str)
        {
            text.text = $"because\n{str}";
        }
    }
}
