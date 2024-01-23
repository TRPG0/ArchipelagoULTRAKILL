using ArchipelagoULTRAKILL.Structures;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL
{
    public static class AudioManager
    {
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
            ["26B"] = "Assets/Music/7-1.wav",
            ["27A"] = "Assets/Music/7-2 Intro Battle.wav",
            ["27B"] = "Assets/Music/7-2.wav",
            ["28B"] = "Assets/Music/7-3.wav",
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
            ["18C"] = "Assets/Music/4-3 Phase 3.wav",
            ["20"] = "Assets/Music/5-1 Clean.wav",
            ["22A"] = "Assets/Music/5-3 Clean.wav",
            ["22B"] = "Assets/Music/5-3 Aftermath Clean.wav",
            ["24A"] = "Assets/Music/6-1 Clean.wav",
            ["26B"] = "Assets/Music/7-1 Clean.wav",
            ["27A"] = "Assets/Music/7-2 Intro Clean.wav",
            ["27B"] = "Assets/Music/7-2 Clean.wav",
            ["28B"] = "Assets/Music/7-3 Clean.wav",
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
            ["19"] = "Assets/Music/Bosses/V2 4-4.wav",
            ["24B"] = "Assets/Music/6-1 Hall of Sacreligious Remains.wav",
            ["25A"] = "Assets/Music/Bosses/Gabriel 6-2 Intro B.wav",
            ["25B"] = "Assets/Music/Bosses/Gabriel 6-2.wav",
            ["26A"] = "Assets/Music/7-1 Intro.wav",
            ["26C"] = "Assets/Music/Misc/themeofcancer.wav",
            ["26D"] = "Assets/Music/Bosses/Minotaur A.wav",
            ["26E"] = "Assets/Music/Bosses/Minotaur B.wav",
            ["28A"] = "Assets/Music/7-3 Intro Clean.wav",
            ["666A"] = "Assets/Music/Bosses/Flesh Prison.wav",
            ["666B"] = "Assets/Music/Bosses/Minos Prime.wav",
            ["667B"] = "Assets/Music/Bosses/Flesh panopticon.wav",
            ["667C"] = "Assets/Music/Bosses/Sisyphus Prime.wav",
        };

        public static AudioClip LoadNewSingleTrack(AudioClip source, string id)
        {
            if (Core.data.music.ContainsKey(id)) id = Core.data.music[id];
            else
            {
                Core.Logger.LogError($"Music dictionary does not contain key {id}. Returning original source.");
                return source;
            }

            if (singleMusic.ContainsKey(id))
            {
                try { return Addressables.LoadAssetAsync<AudioClip>(singleMusic[id]).WaitForCompletion(); }
                catch
                {
                    Core.Logger.LogError($"Failed to load music track. (ID: {id} | Address: {singleMusic[id]}");
                    return source;
                }
            }
            else
            {
                Core.Logger.LogError($"Couldn't find address for key {id}. Returning original source.");
                return source;
            }
        }

        public static AudioClip LoadNewCleanTrack(AudioClip source, string id)
        {
            if (Core.data.music.ContainsKey(id)) id = Core.data.music[id];
            else
            {
                Core.Logger.LogError($"Music dictionary does not contain key {id}. Returning original source.");
                return source;
            }

            if (multiMusicClean.ContainsKey(id))
            {
                try { return Addressables.LoadAssetAsync<AudioClip>(multiMusicClean[id]).WaitForCompletion(); }
                catch
                {
                    Core.Logger.LogError($"Failed to load music track. (ID: {id} | Address: {multiMusicClean[id]}");
                    return source;
                }
            }
            else
            {
                Core.Logger.LogError($"Couldn't find address for key {id}. Returning original source.");
                return source;
            }
        }

        public static AudioClip LoadNewBattleTrack(AudioClip source, string id)
        {
            if (Core.data.music.ContainsKey(id)) id = Core.data.music[id];
            else
            {
                Core.Logger.LogError($"Music dictionary does not contain key {id}. Returning original source.");
                return source;
            }

            if (multiMusicBattle.ContainsKey(id))
            {
                try { return Addressables.LoadAssetAsync<AudioClip>(multiMusicBattle[id]).WaitForCompletion(); }
                catch
                {
                    Core.Logger.LogError($"Failed to load music track. (ID: {id} | Address: {multiMusicBattle[id]}");
                    return source;
                }
            }
            else
            {
                Core.Logger.LogError($"Couldn't find address for key {id}. Returning original source.");
                return source;
            }
        }

        public static void LoadMusicManagerTracks(string id, bool single = false)
        {
            if (single)
            {
                MusicManager.Instance.cleanTheme.clip = LoadNewSingleTrack(MusicManager.Instance.cleanTheme.clip, id);
                MusicManager.Instance.battleTheme.clip = LoadNewSingleTrack(MusicManager.Instance.battleTheme.clip, id);
                MusicManager.Instance.bossTheme.clip = LoadNewSingleTrack(MusicManager.Instance.bossTheme.clip, id);
            }
            else
            {
                MusicManager.Instance.cleanTheme.clip = LoadNewCleanTrack(MusicManager.Instance.cleanTheme.clip, id);
                MusicManager.Instance.battleTheme.clip = LoadNewBattleTrack(MusicManager.Instance.battleTheme.clip, id);
                MusicManager.Instance.bossTheme.clip = LoadNewBattleTrack(MusicManager.Instance.bossTheme.clip, id);
            }
        }

        public static void ChangeMusic()
        {
            if (Core.CurrentLevelInfo.Music == MusicType.Normal) LoadMusicManagerTracks(Core.CurrentLevelInfo.Id.ToString());
            else
            {
                switch (SceneHelper.CurrentScene)
                {
                    case "Level 0-5":
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {

                            if (audio.transform.parent != null && (audio.transform.parent.name == "4 Contents" || audio.transform.parent.name == "4 Contents(Clone)") && audio.transform.name == "Music")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "5");
                                audio.transform.parent.Find("Enemies").Find("StatueEnemy (1)").GetComponent<SoundChanger>().newSound = LoadNewSingleTrack(audio.clip, "5");
                            }

                        }
                        break;
                    case "Level 1-1":
                        LoadMusicManagerTracks("6A", true);
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "MusicChanger")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "6B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "6B");
                            }
                        }
                        break;
                    case "Level 1-2":
                        LoadMusicManagerTracks("7A", true);
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "MusicActivator")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "7B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "7B");
                            }
                        }
                        break;
                    case "Level 1-4":
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "Music - Clair de Lune")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "9A");
                            }
                            else if (audio.gameObject.name == "Music - Versus")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "9B");
                            }
                        }
                        break;
                    case "Level 2-1":
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "Cube (1)")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "10");
                                changer.battle = LoadNewBattleTrack(changer.battle, "10");
                            }
                        }
                        break;
                    case "Level 2-4":
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "BossMusic")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "13");
                            }
                        }
                        break;
                    case "Level 3-1":
                        LoadMusicManagerTracks("14A");
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "MusicChanger")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "14B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "14B");
                            }
                        }
                        break;
                    case "Level 3-2":
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "Music 2")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "15A");
                            }
                            else if (audio.gameObject.name == "Music 3")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "15B");
                            }
                        }
                        break;
                    case "Level 4-3":
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "OnLight") // phase 1
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "18A");
                                changer.battle = LoadNewBattleTrack(changer.battle, "18A");
                            }
                            else if (changer.gameObject.name == "Music Changer") // phase 2
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "18B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "18B");
                            }
                            else if (changer.gameObject.name == "Music Changer (Normal)") // phase 2
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "18B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "18B");
                            }
                            else if (changer.gameObject.name == "Music") // phase 3
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "18C");
                                changer.battle = LoadNewBattleTrack(changer.battle, "18C");
                            }
                            else if (changer.gameObject.name == "Trigger (Fight)") // secret boss
                            {
                                changer.clean = LoadNewSingleTrack(changer.clean, "18D");
                                changer.battle = LoadNewSingleTrack(changer.battle, "18D");
                                changer.boss = LoadNewSingleTrack(changer.boss, "18D");
                            }
                        }
                        break;
                    case "Level 4-4":
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "Versus 2")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "19");
                            }
                        }
                        break;

                    case "Level 5-3":
                        LoadMusicManagerTracks("22A");
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "InstantVer") // aftermath intro
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "22B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "22B");
                            }
                            else if (changer.gameObject.name == "NormalVer") // aftermath
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "22B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "22B");
                            }
                        }
                        break;
                    case "Level 6-1":
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "MusicChanger")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "24A");
                                changer.battle = LoadNewBattleTrack(changer.battle, "24A");
                            }
                        }
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "ClimaxMusic")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "24B");
                            }
                        }
                        break;
                    case "Level 6-2":
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "Organ")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "25A");
                            }
                            else if (audio.gameObject.name == "BossMusic")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "25B");
                            }
                        }
                        break;
                    case "Level 7-1":
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.name == "LevelMusicStart")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "26B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "26B");
                                changer.boss = LoadNewBattleTrack(changer.battle, "26B");
                            }
                        }
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.name == "IntroMusic")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "26A");
                            }
                            else if (audio.name == "BigJohnatronMusic")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "26C");
                            }
                            else if (audio.name == "MinotaurPhase1Music")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "26D");
                            }
                            else if (audio.name == "MinotaurPhase2Music")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "26E");
                            }
                        }
                        break;
                    case "Level 7-2":
                        LoadMusicManagerTracks("27A");
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.name == "MusicActivator")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "27B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "27B");
                                changer.boss = LoadNewBattleTrack(changer.battle, "27B");
                            }
                        }
                        break;
                    case "Level 7-3":
                        LoadMusicManagerTracks("28A", true);
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.name == "SecondTrackStart")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "28B");
                                changer.battle = LoadNewBattleTrack(changer.battle, "28B");
                                changer.boss = LoadNewBattleTrack(changer.battle, "28B");
                            }
                        }
                        break;
                    case "Level P-1":
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "Chaos")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "666A");
                            }
                            else if (audio.gameObject.name == "Music 3")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "666B");
                            }
                        }
                        break;
                    case "Level P-2":
                        foreach (MusicChanger changer in Core.FindAllComponentsInCurrentScene<MusicChanger>())
                        {
                            if (changer.gameObject.name == "DelayedMusicActivator")
                            {
                                changer.clean = LoadNewCleanTrack(changer.clean, "667A");
                                changer.battle = LoadNewBattleTrack(changer.battle, "667A");
                                changer.boss = LoadNewBattleTrack(changer.battle, "667A");
                            }
                        }
                        foreach (AudioSource audio in Core.FindAllComponentsInCurrentScene<AudioSource>())
                        {
                            if (audio.gameObject.name == "FleshPrison")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "667B");
                            }
                            else if (audio.gameObject.name == "Sisyphus")
                            {
                                audio.clip = LoadNewSingleTrack(audio.clip, "667C");
                            }
                        }
                        break;
                    default:
                        return;
                }
            }
            Core.Logger.LogInfo("Music changed successfully.");
        }
    }
}
