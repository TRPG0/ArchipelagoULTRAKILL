using ArchipelagoULTRAKILL.Config;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Music
{
    public class NowPlaying : MonoBehaviour
    {
        public static NowPlaying Instance;

        public HorizontalLayoutGroup childLayoutGroup;
        public LayoutElement emptyElement;
        public Image icon;
        public TextMeshProUGUI text;
        public bool done = true;
        public float Timer { get; internal set; } = 0f;
        public const float MaxTime = 7f;

        public static Color transparent = new Color(1, 1, 1, 0);

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }

            Instance = this;
        }

        public void Init(HorizontalLayoutGroup childLayoutGroup, LayoutElement emptyElement, Image icon, TextMeshProUGUI text)
        {
            this.childLayoutGroup = childLayoutGroup;
            this.emptyElement = emptyElement;
            this.icon = icon;
            this.text = text;
        }

        public void SetIconAndText(Sprite sprite, string str, bool resetTime)
        {
            icon.sprite = sprite;
            text.text = str;
            done = false;
            if (resetTime) Show();
        }

        public void Show()
        {
            if (!done && MusicConfig.showNowPlaying.value)
            {
                gameObject.SetActive(true);
                Timer = MaxTime;
                done = true;
            }
        }

        public void OnEnable()
        {
            StartCoroutine(SetDirtyDelay());
        }

        public IEnumerator SetDirtyDelay()
        {
            yield return new WaitForEndOfFrame();
            Traverse.Create(childLayoutGroup).Method("SetDirty").GetValue();
        }

        public void Update()
        {
            if (Timer > 0f)
            {
                Timer -= Time.deltaTime;

                float maxOpacity = GameStateManager.Instance.IsStateActive("pause") ? MusicConfig.nowPlayingMaxOpacity.value / 300f : MusicConfig.nowPlayingMaxOpacity.value / 100f;
                Color opaque = new Color(1, 1, 1, maxOpacity);

                Color color = Color.Lerp(transparent, opaque, Timer);
                icon.color = color;
                text.color = color;

                Dictionary<int, BossHealthBarTemplate> bossBars = Traverse.Create(BossBarManager.Instance).Field<Dictionary<int, BossHealthBarTemplate>>("bossBars").Value;

                switch (bossBars.Count)
                {
                    case 0:
                        emptyElement.flexibleHeight = 20;
                        break;
                    case 1:
                        emptyElement.flexibleHeight = 3.5f;
                        break;
                    case 2:
                        emptyElement.flexibleHeight = 1.5f;
                        break;
                    default:
                        emptyElement.flexibleHeight = -1;
                        break;
                }
            }
            if (Timer < 0f)
            {
                Timer = 0f;
                gameObject.SetActive(false);
            }
        }
    }
}
