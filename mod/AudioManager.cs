using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace ArchipelagoULTRAKILL
{
    public static class AudioManager
    {
        private static readonly List<string> standardLevels = new List<string>()
        {
            "Level 0-1",
            "Level 0-2",
            "Level 0-3",
            "Level 0-4",
            "Level 1-3",
            "Level 2-2",
            "Level 2-3",
            "Level 4-1",
            "Level 4-2",
            "Level 5-1"
        };

        private static readonly Dictionary<string, string> multiMusicBattle = new Dictionary<string, string>()
        {
            ["1"] = "Assets/Music/0-1.wav",
            ["2"] = "Assets/Music/0-2.wav",
            ["3"] = "Assets/Music/0-1.wav",
            ["4"] = "Assets/Music/0-2.wav",
            ["6B"] = "Assets/Music/1-1.wav",
            ["7B"] = "Assets/Music/1-2 Noise Battle.wav",
            ["8"] = "Assets/Music/1-3.wav",
            ["10"] = "Assets/Music/2-1.wav",
            ["11"] = "Assets/Music/2-2.wav",
            ["12"] = "Assets/Music/2-3.wav",
            ["14A"] = "Assets/Music/3-1 Guts.wav",
            ["14B"] = "Assets/Music/3-1 Glory.wav",
            ["16"] = "Assets/Music/4-1.wav",
            ["17"] = "Assets/Music/4-2.wav",
            ["18A"] = "Assets/Music/4-3 Phase 1.wav",
            ["18B"] = "Assets/Music/4-3 Phase 2.wav",
            ["18C"] = "Assets/Music/4-3 Phase 3.wav",
            ["20"] = "Assets/Music/5-1.wav",
            ["22A"] = "Assets/Music/5-3.wav",
            ["22B"] = "Assets/Music/5-3 Aftermath.wav",
            ["24A"] = "Assets/Music/6-1.wav",
            ["667A"] = "Assets/Music/P-2.wav",
        };

        private static readonly Dictionary<string, string> multiMusicClean = new Dictionary<string, string>()
        {
            ["1"] = "Assets/Music/0-1 Clean.wav",
            ["2"] = "Assets/Music/0-2 Clean.wav",
            ["3"] = "Assets/Music/0-1 Clean.wav",
            ["4"] = "Assets/Music/0-2 Clean.wav",
            ["6B"] = "Assets/Music/1-1 Clean.wav",
            ["7B"] = "Assets/Music/1-2 Noise Clean.wav",
            ["8"] = "Assets/Music/1-3 Clean.wav",
            ["10"] = "Assets/Music/2-1 Clean.wav",
            ["11"] = "Assets/Music/2-2 Clean.wav",
            ["12"] = "Assets/Music/2-3 Clean.wav",
            ["14A"] = "Assets/Music/3-1 Guts Clean.wav",
            ["14B"] = "Assets/Music/3-1 Glory Clean.wav",
            ["16"] = "Assets/Music/4-1 Clean.wav",
            ["17"] = "Assets/Music/4-2 Clean.wav",
            ["18A"] = "Assets/Music/4-3 Phase 1 Clean.wav",
            ["18B"] = "Assets/Music/4-3 Phase 2 Clean.wav",
            ["18C"] = "Assets/Music/4-3 Phase 3 Clean.wav",
            ["20"] = "Assets/Music/5-1 Clean.wav",
            ["22A"] = "Assets/Music/5-3 Clean.wav",
            ["22B"] = "Assets/Music/5-3 Aftermath Clean.wav",
            ["24A"] = "Assets/Music/6-1 Clean.wav",
            ["667A"] = "Assets/Music/P-2 Clean.wav",
        };

        private static readonly Dictionary<string, string> singleMusic = new Dictionary<string, string>()
        {
            ["5"] = "Assets/Music/Bosses/Cerberus A.mp3",
            ["6A"] = "Assets/Music/Misc/A Thousand Greetings.wav",
            ["7A"] = "Assets/Music/Misc/A Thousand Greetings.wav",
            ["9A"] = "Assets/Music/Misc/Clair_de_lune_(Claude_Debussy)_Suite_bergamasque (CREATIVE COMMONS).ogg",
            ["9B"] = "Assets/Music/Bosses/V2 1-4.wav",
            ["13"] = "Assets/Music/Bosses/Minos Corpse B.wav",
            ["15A"] = "Assets/Music/Bosses/Gabriel 3-2 Intro.wav",
            ["15B"] = "Assets/Music/Bosses/Gabriel 3-2.wav",
            ["18D"] = "Assets/Music/Misc/themeofcancer.wav",
            ["19"] = "Assets/Music/Misc/V2 4-4.wav",
            ["24B"] = "Assets/Music/6-1 Halls of Sacreligious Remains.wav",
            ["25A"] = "Assets/Music/Bosses/Gabriel 6-2 Intro B.wav",
            ["25B"] = "Assets/Music/Bosses/Gabriel 6-2.wav",
            ["666A"] = "Assets/Music/Bosses/Flesh Prison.wav",
            ["666B"] = "Assets/Music/Bosses/Minos Prime.wav",
            ["667B"] = "Assets/Music/Bosses/Flesh Panopticon.wav",
            ["667C"] = "Assets/Music/Bosses/Sisyphus Prime.wav",
        };

        public static AudioClip LoadNewTrack(AudioClip source, string id, bool clean, bool single = false)
        {
            string id2;
            if (Core.data.music.ContainsKey(id)) id2 = Core.data.music[id];
            else id2 = id;

            if (single)
            {
                if (singleMusic.ContainsKey(id2)) return Addressables.LoadAssetAsync<AudioClip>(singleMusic[id2]).WaitForCompletion();
                else return source;
            }
            else if (clean)
            {
                if (multiMusicClean.ContainsKey(id2)) return Addressables.LoadAssetAsync<AudioClip>(multiMusicClean[id2]).WaitForCompletion();
                else return source;
            }
            else
            {
                if (multiMusicBattle.ContainsKey(id2)) return Addressables.LoadAssetAsync<AudioClip>(multiMusicBattle[id2]).WaitForCompletion();
                else return source;
            }
        }

        public static void ChangeMusic()
        {
            if (standardLevels.Contains(SceneHelper.CurrentScene))
            {
                MusicManager.Instance.cleanTheme.clip = LoadNewTrack(MusicManager.Instance.cleanTheme.clip, StatsManager.Instance.levelNumber.ToString(), true);
                MusicManager.Instance.battleTheme.clip = LoadNewTrack(MusicManager.Instance.battleTheme.clip, StatsManager.Instance.levelNumber.ToString(), false);
            }
            else
            {
                switch (SceneHelper.CurrentScene)
                {
                    case "Level 0-5":
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name && audio.clip.name == "Cerberus A")
                            {
                                audio.clip = LoadNewTrack(audio.clip, "5", true, true);
                            }
                        }
                        break;
                    case "Level 1-1":
                        MusicManager.Instance.cleanTheme.clip = LoadNewTrack(MusicManager.Instance.cleanTheme.clip, "6A", true, true);
                        MusicManager.Instance.battleTheme.clip = LoadNewTrack(MusicManager.Instance.battleTheme.clip, "6A", true, true);

                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name && changer.gameObject.name == "MusicChanger")
                            {
                                changer.clean = LoadNewTrack(changer.clean, "6B", true);
                                changer.battle = LoadNewTrack(changer.battle, "6B", false);
                            }
                        }
                        break;
                    case "Level 1-2":
                        MusicManager.Instance.cleanTheme.clip = LoadNewTrack(MusicManager.Instance.cleanTheme.clip, "7A", true, true);
                        MusicManager.Instance.battleTheme.clip = LoadNewTrack(MusicManager.Instance.battleTheme.clip, "7A", true, true);

                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name && changer.gameObject.name == "MusicActivator")
                            {
                                changer.clean = LoadNewTrack(changer.clean, "7B", true);
                                changer.battle = LoadNewTrack(changer.battle, "7B", false);
                            }
                        }
                        break;
                    case "Level 1-4":
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                if (audio.clip.name == "Clair_de_lune_(Claude_Debussy)_Suite_bergamasque (CREATIVE COMMONS)")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "9A", true, true);
                                }
                                else if (audio.clip.name == "V2 1-4")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "9B", true, true);
                                }
                            }
                        }
                        break;
                    case "Level 2-1":
                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name && changer.gameObject.name == "Cube (1)")
                            {
                                changer.clean = LoadNewTrack(changer.clean, "10", true);
                                changer.battle = LoadNewTrack(changer.battle, "10", false);
                            }
                        }
                        break;
                    case "Level 2-4":
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name && audio.clip.name == "Minos Corpse B")
                            {
                                audio.clip = LoadNewTrack(audio.clip, "13", true, true);
                            }
                        }
                        break;
                    case "Level 3-1":
                        MusicManager.Instance.cleanTheme.clip = LoadNewTrack(MusicManager.Instance.cleanTheme.clip, "14A", true);
                        MusicManager.Instance.battleTheme.clip = LoadNewTrack(MusicManager.Instance.battleTheme.clip, "14A", false);

                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name && changer.gameObject.name == "MusicChanger")
                            {
                                changer.clean = LoadNewTrack(changer.clean, "14B", true);
                                changer.battle = LoadNewTrack(changer.battle, "14B", false);
                            }
                        }
                        break;
                    case "Level 3-2":
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                if (audio.clip.name == "Gabriel 3-2 Intro")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "15A", true, true);
                                }
                                else if (audio.clip.name == "Gabriel 3-2")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "15B", true, true);
                                }
                            }
                        }
                        break;
                    case "Level 4-3":
                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                if (changer.gameObject.name == "OnLight") // phase 1
                                {
                                    changer.clean = LoadNewTrack(changer.clean, "18A", true);
                                    changer.battle = LoadNewTrack(changer.battle, "18A", false);
                                }
                                else if (changer.gameObject.name == "Music Changer") // phase 2
                                {
                                    changer.clean = LoadNewTrack(changer.clean, "18B", true);
                                    changer.battle = LoadNewTrack(changer.battle, "18B", false);
                                }
                                else if (changer.gameObject.name == "Music Changer (Normal)") // phase 2
                                {
                                    changer.clean = LoadNewTrack(changer.clean, "18B", true);
                                    changer.battle = LoadNewTrack(changer.battle, "18B", false);
                                }
                                else if (changer.gameObject.name == "Music") // phase 3
                                {
                                    changer.clean = LoadNewTrack(changer.clean, "18C", true);
                                    changer.battle = LoadNewTrack(changer.battle, "18C", false);
                                }
                                else if (changer.gameObject.name == "Trigger (Fight)") // secret boss
                                {
                                    changer.clean = LoadNewTrack(changer.clean, "18D", true, true);
                                    changer.battle = LoadNewTrack(changer.battle, "18D", true, true);
                                    changer.boss = LoadNewTrack(changer.boss, "18D", true, true);
                                }
                            }
                        }
                        break;
                    case "Level 4-4":
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name && audio.clip.name == "V2 4-4")
                            {
                                audio.clip = LoadNewTrack(audio.clip, "19", true, true);
                            }
                        }
                        break;
                    case "Level 5-2":
                        // maybe some day
                        break;
                    case "Level 5-3":
                        MusicManager.Instance.cleanTheme.clip = LoadNewTrack(MusicManager.Instance.cleanTheme.clip, "22A", true);
                        MusicManager.Instance.battleTheme.clip = LoadNewTrack(MusicManager.Instance.battleTheme.clip, "22A", false);

                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                if (changer.gameObject.name == "InstantVer") // aftermath intro
                                {
                                    changer.clean = LoadNewTrack(changer.clean, "22B", true);
                                    changer.battle = LoadNewTrack(changer.battle, "22B", false);
                                }
                                else if (changer.gameObject.name == "NormalVer") // aftermath
                                {
                                    changer.clean = LoadNewTrack(changer.clean, "22B", true);
                                    changer.battle = LoadNewTrack(changer.battle, "22B", false);
                                }
                            }
                        }
                        break;
                    case "Level 5-4":
                        /*
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                if (audio.clip.name == "Leviathan A")
                                {
                                    audio.clip = Addressables.LoadAssetAsync<AudioClip>("Assets/Music/Bosses/Sisyphus Prime.wav").WaitForCompletion();
                                }
                                else if (audio.clip.name == "Leviathan B")
                                {
                                    audio.clip = Addressables.LoadAssetAsync<AudioClip>("Assets/Music/Bosses/Sisyphus Prime.wav").WaitForCompletion();
                                }
                            }
                        }
                        */
                        break;
                    case "Level 6-1":
                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name && changer.gameObject.name == "MusicChanger")
                            {
                                changer.clean = LoadNewTrack(changer.clean, "24A", true);
                                changer.battle = LoadNewTrack(changer.battle, "24A", false);
                            }
                        }
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name && audio.gameObject.name == "ClimaxMusic")
                            {
                                audio.clip = LoadNewTrack(audio.clip, "24B", true, true);
                            }
                        }
                        break;
                    case "Level 6-2":
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                //Core.logger.LogInfo(audio.gameObject.name);
                                if (audio.gameObject.name == "Organ")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "25A", true, true);
                                }
                                else if (audio.gameObject.name == "BossMusic")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "25B", true, true);
                                }
                            }
                        }
                        break;
                    case "Level P-1":
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                if (audio.clip.name == "Flesh Prison")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "666A", true, true);
                                }
                                else if (audio.clip.name == "Minos Prime")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "666B", true, true);
                                }
                            }
                        }
                        break;
                    case "Level P-2":
                        foreach (MusicChanger changer in Resources.FindObjectsOfTypeAll<MusicChanger>())
                        {
                            if (changer.gameObject.scene.name == SceneManager.GetActiveScene().name && changer.gameObject.name == "DelayedMusicActivator")
                            {
                                changer.clean = LoadNewTrack(changer.clean, "667A", true);
                                changer.battle = LoadNewTrack(changer.battle, "667A", false);
                            }
                        }
                        foreach (AudioSource audio in Resources.FindObjectsOfTypeAll<AudioSource>())
                        {
                            if (audio.gameObject.scene.name == SceneManager.GetActiveScene().name)
                            {
                                if (audio.clip.name == "Flesh Panopticon")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "667B", true, true);
                                }
                                else if (audio.clip.name == "Sisyphus Prime")
                                {
                                    audio.clip = LoadNewTrack(audio.clip, "667C", true, true);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
