using System.Collections.Generic;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Music
{
    public class NowPlayingChanger : MonoBehaviour
    {
        public AudioSource audioSource;
        public Sprite sprite;
        public string text;
        public List<NowPlayingChanger> links = new List<NowPlayingChanger>();

        public void Init(Sprite sprite, string text, AudioSource audioSource = null)
        {
            this.audioSource = audioSource;
            this.sprite = sprite;
            this.text = text;
        }

        public void Set()
        {
            if (NowPlaying.Instance)
            {
                NowPlaying.Instance.SetIconAndText(sprite, text, true);
                if (links.Count > 0) DestroyLinks();
                else Destroy(this);
            }
        }

        public void DestroyLinks()
        {
            foreach (NowPlayingChanger changer in links)
            {
                Destroy(changer);
            }
        }

        public void Update()
        {
            if (audioSource)
            {
                if (audioSource.isPlaying)
                {
                    Set();
                    DestroyLinks();
                }
            }
        }
    }
}
