using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace ArchipelagoULTRAKILL.Music
{
    public static class MusicRandomizer
    {
        public static readonly Dictionary<string, BaseMusic> Music = new Dictionary<string, BaseMusic>()
        {
            ["1"] = new MultiClipMusic(
                "0-1, 0-3",
                "Into the Fire",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-1 Into the Fire.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1.wav")),

            ["2"] = new MultiClipMusic(
                "0-2, 0-4",
                "Unstoppable Force",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-2 The Meatgrinder.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2.wav")),

            ["3"] = new MultiClipMusic(
                "0-1, 0-3",
                "Into the Fire",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-3 Double Down.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1.wav")),

            ["4"] = new MultiClipMusic(
                "0-2, 0-4",
                "Unstoppable Force",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-4 A One-Machine Army.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2.wav")),

            ["5"] = new MultiSoundtrackMusic(
                "0-5",
                "Cerberus",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Prelude/Cerberus.asset"),
                0,
                1),

            ["6A"] = new SingleSoundtrackMusic(
                "1-1",
                "A Thousand Greetings",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Limbo/A Thousand Greetings.asset")),

            ["6B"] = new MultiClipMusic(
                "1-1",
                "A Shattered Illusion",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-1 Heart of the Sunrise.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-1.wav")),

            ["7A"] = new MultiClipMusic(
                "1-2",
                "A Complete and Utter Destruction of the Senses (Part A)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-2 The Burning World.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-2 Dark Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-2 Dark Battle.wav")),

            ["7B"] = new MultiClipMusic(
                "1-2",
                "A Complete and Utter Destruction of the Senses (Part B)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-2 The Burning World.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-2 Noise Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-2 Noise Battle.wav")),

            ["8"] = new MultiClipMusic(
                "1-3",
                "Castle Vein",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-3 Halls of Sacred Remains.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-3.wav")),

            ["9A"] = new SingleSoundtrackMusic(
                "1-4",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Limbo/Clair de Lune.asset")),

            ["9B"] = new SingleSoundtrackMusic(
                "1-4",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Limbo/Versus.asset")),

            ["10"] = new MultiClipMusic(
                "2-1",
                "Cold Winds",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/2-1 In the Air Tonight.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-1.wav")),

            ["11"] = new MultiClipMusic(
                "2-2",
                "Requiem",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/2-2 Death at 20,000 Volts.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-2.wav")),

            ["12"] = new MultiClipMusic(
                "2-3",
                "Panic Betrayer",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/2-3 Sheer Heart Attack.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-3.wav")),

            ["13"] = new SingleSoundtrackMusic(
                "2-4",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Lust/In the Presence of a King.asset"),
                1),

            ["14A"] = new MultiClipMusic(
                "3-1",
                "Guts",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/3-1 Belly of the Beast.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Guts Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Guts.wav")),

            ["14B"] = new MultiClipMusic(
                "3-1",
                "Glory",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/3-1 Belly of the Beast.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Glory Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Glory.wav")),

            ["15A"] = new SingleSoundtrackMusic(
                "3-2",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Gluttony/Bach BWV 639.asset")),

            ["15B"] = new SingleSoundtrackMusic(
                "3-2",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Gluttony/Divine Intervention.asset")),

            ["16"] = new MultiClipAndSoundtrackMusic(
                "4-1",
                new AssetReferenceT<AudioClip>("Assets/Music/4-1 Clean.wav"),
                true,
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Greed/Dune Eternal.asset"),
                false),

            ["17"] = new MultiClipMusic(
                "4-2",
                "Sands of Tide",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-2 God Damn the Sun.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-2.wav")),

            ["18A"] = new MultiClipMusic(
                "4-3",
                "Dancer in the Darkness (Part A)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 1.wav")),

            ["18B"] = new MultiClipMusic(
                "4-3",
                "Dancer in the Darkness (Part B)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 2.wav")),

            ["18C"] = new SingleClipMusic(
                "4-3",
                "Dancer in the Darkness (Part C)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 3.wav")),

            ["19"] = new SingleSoundtrackMusic(
                "4-4",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Greed/Duel (Versus Reprise).asset")),

            ["20"] = new MultiPreloadFromManagerMusic(
                "5-1",
                "Level 5-1",
                "Deep Blue",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/5-1 In the Wake of Poseidon.png")),

            ["22A"] = new MultiClipMusic(
                "5-3",
                "Death Odyssey",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/5-3 Ship of Fools.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3.wav")),

            ["22B"] = new MultiClipMusic(
                "5-3",
                "Death Odyssey Aftermath",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/5-3 Ship of Fools.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3 Aftermath Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3 Clean.wav")),

            ["24A"] = new MultiClipMusic(
                "6-1",
                "Altars of Apostasy",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/6-1 Cry for the Weeper.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/6-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/6-1.wav")),

            ["24B"] = new SingleClipMusic(
                "6-1",
                "Hall of Sacreligious Remains",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/6-1 Cry for the Weeper.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/6-1 Hall of Sacreligious Remains.wav")),

            ["25A"] = new SingleSoundtrackMusic(
                "6-2",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Heresy/Fallen Angel.asset")),

            ["25B"] = new SingleSoundtrackMusic(
                "6-2",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Heresy/The Death of Gods Will.asset")),

            ["26A"] = new SingleClipMusic(
                "7-1",
                "The World Looks White",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-1 Garden of Forking Paths.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-1 Intro.wav")),

            ["26B"] = new MultiClipMusic(
                "7-1",
                "The World Looks Red",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-1 Garden of Forking Paths.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-1.wav")),

            ["26C"] = new SingleSoundtrackMusic(
                "7-1",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Violence/Bull of Hell.asset"),
                0),

            ["26D"] = new SingleSoundtrackMusic(
                "7-1",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Violence/Bull of Hell.asset"),
                1),

            ["27A"] = new MultiClipMusic(
                "7-2",
                "Do Robots Dream of Eternal Sleep?",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-2 Light Up the Night.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2 Intro Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2 Intro Battle.wav")),

            ["27B"] = new MultiClipMusic(
                "7-2",
                "Hear! The Siren Song Call of Death",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-2 Light Up the Night.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2.wav")),

            ["28A"] = new SingleClipMusic(
                "7-3",
                "Suffering Leaves Suffering Leaves",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-3 No Sound No Memory.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-3 Intro Clean.wav")),

            ["28B"] = new MultiClipMusic(
                "7-3",
                "Danse Macabre",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-3 No Sound No Memory.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-3.wav")),

            ["30A"] = new MultiClipMusic(
                "8-1",
                "In Absentia Logos",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-1 Hurtbreak Wonderland.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-1 Intro Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-1 Intro.wav")),

            ["30B"] = new MultiClipAndSoundtrackMusic(
                "8-1",
                new AssetReferenceT<AudioClip>("Assets/Music/8-1 Clean.wav"),
                true,
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Fraud/Spiral Out (Keep Going).asset"),
                false),

            ["31A"] = new MultiClipMusic(
                "8-2",
                "Never Odd Or Even",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Intro B.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Intro A.wav")),

            ["31B"] = new MultiClipMusic(
                "8-2",
                "No Devil Lived On",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2.wav")),

            ["31C"] = new SingleClipMusic(
                "8-2",
                "No Devil Lived On (Ambient)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Ambient.wav")),

            ["31D"] = new MultiClipMusic(
                "8-2",
                "No Devil Lived On (Shopping)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Shopping Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Shopping.wav")),

            ["31E"] = new SingleSoundtrackMusic(
                "8-2",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Fraud/Mirror Rim.asset")),

            ["32A"] = new MultiClipMusic(
                "8-3",
                "The Break (Crimson Glass deComposition)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-3 Disintegration Loop.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3 Intro Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3 Intro.wav")),

            ["32B"] = new MultiClipMusic(
                "8-3",
                "The Shattering Circle, or: A Charade of Shadeless Ones and Zeroes Rearranged ad Nihilum",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-3 Disintegration Loop.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3.wav")),

            /*["32C"] = new MultiClipMusic(
                ),*/

            ["33"] = new SingleSoundtrackMusic(
                "8-4",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Fraud/The Fall.asset")),

            ["100A"] = new MultiClipMusic(
                "0-E",
                "A Heart of Cold",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-E This Heat This Evil Heat.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Cold Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Cold.wav")),

            ["100B"] = new MultiClipMusic(
                "0-E",
                "Dead Heat Pulse (A Heart of Cold)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-E This Heat This Evil Heat.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Hot Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Hot.wav")),

            ["101A"] = new SingleClipMusic(
                "1-E",
                "An Absence",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-E And Then Fell the Ashes.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/1-E Ambiance.wav")),

            ["101B"] = new MultiClipMusic(
                "1-E",
                "A Part Falling",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-E And Then Fell the Ashes.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/1-E Jazz Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/1-E Jazz.wav")),

            ["666A"] = new SingleSoundtrackMusic(
                "P-1",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/Chaos.asset")),

            ["666B"] = new SingleSoundtrackMusic(
                "P-1",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/Order.asset")),

            ["667A"] = new MultiClipMusic(
                "P-2",
                "Tenebre Rosso Sangue",
                "KEYGEN CHURCH",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/P-2 Wait of the World.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/P-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/P-2.wav")),

            ["667B"] = new SingleSoundtrackMusic(
                "P-2",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/Pandemonium.asset")),

            ["667C"] = new SingleSoundtrackMusic(
                "P-2",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/War.asset"))
        };

        // missing music:
        // A Thousand Greetings (Alternate) (1-2)
        // Deep Blue (5-1)
        // themeofcancer.wav (5-3, 7-1)

        private static List<object> TestLoadAll()
        {
            List<object> list = new List<object>();
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (BaseMusic music in Music.Values)
            {
                if (music is SingleClipMusic scm)
                {
                    list.Add(scm.icon.LoadAssetAsync<Sprite>().WaitForCompletion());
                    scm.icon.ReleaseAsset();
                    list.Add(scm.audioClip.LoadAssetAsync<AudioClip>().WaitForCompletion());
                    scm.audioClip.ReleaseAsset();
                }
                else if (music is SingleSoundtrackMusic ssm)
                {
                    list.Add(ssm.soundtrackSong.LoadAssetAsync<SoundtrackSong>().WaitForCompletion());
                    ssm.soundtrackSong.ReleaseAsset();
                }
                else if (music is MultiClipMusic mcm)
                {
                    list.Add(mcm.icon.LoadAssetAsync<Sprite>().WaitForCompletion());
                    mcm.icon.ReleaseAsset();
                    list.Add(mcm.audioClipClean.LoadAssetAsync<AudioClip>().WaitForCompletion());
                    mcm.audioClipClean.ReleaseAsset();
                    list.Add(mcm.audioClipBattle.LoadAssetAsync<AudioClip>().WaitForCompletion());
                    mcm.audioClipBattle.ReleaseAsset();
                }
                else if (music is MultiSoundtrackMusic msm)
                {
                    list.Add(msm.soundtrackSong.LoadAssetAsync<SoundtrackSong>().WaitForCompletion());
                    msm.soundtrackSong.ReleaseAsset();
                }
                else if (music is MultiClipAndSoundtrackMusic mcasm)
                {
                    list.Add(mcasm.audioClip.LoadAssetAsync<AudioClip>().WaitForCompletion());
                    mcasm.audioClip.ReleaseAsset();
                    list.Add(mcasm.soundtrackSong.LoadAssetAsync<SoundtrackSong>().WaitForCompletion());
                    mcasm.soundtrackSong.ReleaseAsset();
                }
            }

            stopwatch.Stop();
            Core.Logger.LogInfo($"Time: {stopwatch.ElapsedMilliseconds} ms");
            return list;
        }

        /*
        public static IEnumerator PreloadFromManager(MultiPreloadFromManagerMusic multiPreloadFromManagerMusic)
        {
            yield return SceneHelper.LoadSceneAsync(multiPreloadFromManagerMusic.scene);
            yield return new WaitUntil(() => MusicManager.Instance.cleanTheme != null && MusicManager.Instance.battleTheme != null);

            GameObject cleanObj = MusicManager.Instance.cleanTheme.gameObject;
            cleanObj.transform.SetParent(null);
            cleanObj.hideFlags = HideFlags.HideAndDontSave;
            //Object.DontDestroyOnLoad(cleanObj);
            multiPreloadFromManagerMusic.clean = cleanObj.GetComponent<AudioSource>().clip;

            GameObject battleObj = MusicManager.Instance.battleTheme.gameObject;
            battleObj.transform.SetParent(null);
            battleObj.hideFlags = HideFlags.HideAndDontSave;
            //Object.DontDestroyOnLoad(battleObj);
            multiPreloadFromManagerMusic.battle = battleObj.GetComponent<AudioSource>().clip;

            multiPreloadFromManagerMusic.Preloaded = true;

            SceneHelper.LoadScene("Main Menu");
        }
        */
    }
}
