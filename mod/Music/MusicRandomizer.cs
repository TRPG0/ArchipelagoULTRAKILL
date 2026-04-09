using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ArchipelagoULTRAKILL.Music
{
    public class MusicRandomizer : MonoBehaviour
    {
        public static MusicRandomizer Instance;

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

            ["7A"] = new SinglePreloadFromManagerMusic(
                "1-2",
                "Level 1-2",
                "A Thousand Greetings (Alternate)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-2 The Burning World.png")),

            ["7B"] = new MultiClipMusic(
                "1-2",
                "A Complete and Utter Destruction of the Senses",
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
                "Dancer in the Darkness (Phase 1)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 1.wav")),

            ["18B"] = new MultiClipMusic(
                "4-3",
                "Dancer in the Darkness (Phase 2)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 2.wav")),

            ["18C"] = new SingleClipMusic(
                "4-3",
                "Dancer in the Darkness (Phase 3)",
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

            ["32C"] = new MultiPreloadFromChangerMusic(
                "8-3",
                "Level 8-3",
                "Event Horizon (Reach for the Sun and Burn! Burn! Burn!)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-3 Disintegration Loop.png"),
                "Musics/Third"),

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

            ["101"] = new MultiClipMusic(
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

        public static readonly Dictionary<string, BaseTarget> Targets = new Dictionary<string, BaseTarget>()
        {
            ["1"] = null,
            ["2"] = null,
            ["3"] = null,
            ["4"] = null,
            ["5"] = new AudioSourceTarget(new List<string>() { "4 - Cerberus Arena/4 Contents/Music", "4 - Cerberus Arena/4 Contents(Clone)/Music" }),
            ["6A"] = null,
            ["6B"] = new MusicChangerTarget("6 - Waterfall Arena/6 Nonstuff/MusicChanger"),
            ["7A"] = null,
            ["7B"] = new MusicChangerTarget("7 - Castle Entrance/7 Nonstuff/MusicActivator"),
            ["8"] = null,
            ["9A"] = new AudioSourceTarget("Music - Clair de Lune"),
            ["9B"] = new AudioSourceTarget("Music - Versus"),
            ["10"] = new MusicChangerTarget("MusicStart"),
            ["11"] = null,
            ["12"] = null,
            ["13"] = new AudioSourceTarget("4 - Second Encounter/4 Stuff/BossMusic"),
            ["14A"] = null,
            ["14B"] = new MusicChangerTarget(new List<string>() { "6 - Stairways/6 Nonstuff/Music Restart", "6S - P Door/6S Nonstuff/Music Restart", "6 - Stairways/6 Nonstuff/MusicChanger" }),
            ["15A"] = new AudioSourceTarget("Music 2"),
            ["15B"] = new AudioSourceTarget("Music 3"),
            ["16"] = null,
            ["17"] = null,
            ["18A"] = new MusicChangerTarget("1 - First Chambers/1 Stuff/Torches/GreedTorch/OnLight"),
            ["18B"] = new MusicChangerTarget(new List<string>() { "2 - Torches Arena/2 Nonstuff/Music Changer", "3 - Traitor Hallway/3B - Tomb of Kings/3B Nonstuff/Hall/Music Changer (Normal)" }),
            ["18C"] = new MusicChangerTarget("7 - Generator Room/7 Nonstuff/Altar/MusicStopper/Music"),
            //["18D"] = new MusicChangerTarget("3 - Traitor Hallway/3B - Tomb of Kings/3B Stuff/Hall/Trigger (Fight)"),
            ["19"] = new AudioSourceTarget("Versus 2"),
            ["20"] = null,
            ["22A"] = null,
            ["22B"] = new MusicChangerTarget(new List<string>() { "Rotated/FogMachine (Sunken)/InstantVer", "Rotated/FogMachine (Sunken)/IntroVerDenier/IntroVer/NormalVer" }),
            ["24A"] = new MusicChangerTarget("Exteriors/MusicChanger"),
            ["24B"] = new AudioSourceTarget("ClimaxMusic"),
            ["25A"] = new AudioSourceTarget("IntroSounds/Filler/Organ"),
            ["25B"] = new AudioSourceTarget("BossMusic"),
            ["26A"] = new AudioSourceTarget("IntroMusicFiller/IntroMusic"),
            ["26B"] = new MusicChangerTarget("LevelMusicStart"),
            ["26C"] = new AudioSourceTarget("MinotaurPhase1Music"),
            ["26D"] = new AudioSourceTarget("MinotaurPhase2Music"),
            ["27A"] = null,
            ["27B"] = new MusicChangerTarget("Outdoors/Decorations/LevelNameDelay/MusicActivator"),
            ["28A"] = null,
            ["28B"] = new MusicChangerTarget("SecondTrackStart"),
            ["30A"] = null,
            ["30B"] = new MusicChangerTarget(new List<string>() { "Outside/OnTeleportOutsideMusicChangerParent/OnTeleportOutsideMusicChanger", "Outside/FakeExitOutsideMusicTrigger" }),
            ["31A"] = new AudioSourceSplitTarget("Intro Music/Intro B", "Intro Music/Intro A"),
            ["31B"] = new MusicChangerTarget(new List<string>() { "2 - Reception/2 Nonstuff/EnableOnBreak", "Main Music Parent/Main Music" }),
            ["31C"] = new AudioSourceTarget(new List<string>() { "12 - Island/12 Nonstuff/2/AmbianceZone/Ambiance", "15 - Second Deathcatcher/15 Stuff(Clone)(Clone)(Clone)(Clone)(Clone)(Clone)/Ambiance (Fake Propagation)" }),
            ["31D"] = new MusicChangerTarget("Main Music Parent/Mall Music", false, true, true),
            ["31E"] = new AudioSourceTarget("Boss Music"),
            ["32A"] = new MusicChangerTarget("Musics/First"),
            ["32B"] = new MusicChangerTarget("Musics/Second"),
            ["32C"] = new MusicChangerTarget("Musics/Third"),
            ["33"] = new AudioSourceTarget("Main Music"),
            ["100A"] = null,
            ["100B"] = new MusicChangerTarget("9 - Path 1 - Boss Arena/6 Nonstuff/HeatActivation/HeatWarningActivator"),
            ["101"] = new MusicChangerTarget("Music Changer"),
            ["666A"] = new AudioSourceTarget("Music 2/Chaos"),
            ["666B"] = new AudioSourceTarget("Music 3"),
            ["667A"] = new MusicChangerTarget("Rain/PreBoss Musics/LevelMusicActivator/DelayedMusicActivator"),
            ["667B"] = new AudioSourceTarget("BossMusics/FleshPrison"),
            ["667C"] = new AudioSourceTarget("BossMusics/Sisyphus")
        };

        public GameObject preloadParent;

        public bool IsPreloading { get; private set; } = false;
        public List<AsyncOperationHandle> handles = new List<AsyncOperationHandle>();

        private List<object> TestLoadAll()
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

        public void CheckIfPreloadNeededBeforeLevel(string scene, int level)
        {
            List<string> preloadKeys = new List<string>();

            foreach (string key in GetKeysForLevel(level))
            {
                if (Music[key] is PreloadMusic) preloadKeys.Add(key);
            }

            if (preloadKeys.Count > 0) StartCoroutine(PreloadBeforeLevel(preloadKeys, scene));
            else SceneHelper.LoadScene(scene);
        }

        public IEnumerator PreloadBeforeLevel(List<string> preloadKeys, string scene)
        {
            IsPreloading = true;
            SceneHelper.ShowLoadingBlocker();
            foreach (string key in preloadKeys)
            {
                if (Music[key] is SinglePreloadFromManagerMusic spfmm) yield return StartCoroutine(SinglePreloadFromManager(key, spfmm));
                if (Music[key] is MultiPreloadFromManagerMusic mpfmm) yield return StartCoroutine(MultiPreloadFromManager(key, mpfmm));
                if (Music[key] is MultiPreloadFromChangerMusic mpfcm) yield return StartCoroutine(MultiPreloadFromChanger(key, mpfcm));
            }
            IsPreloading = false;
            SceneHelper.DismissBlockers();

            yield return SceneHelper.LoadSceneAsync(scene);
        }

        public void TestPreload(string key, string scene)
        {
            if (!Music.ContainsKey(key))
            {
                Core.Logger.LogWarning($"Music key {key} is not valid.");
                return;
            }

            StartCoroutine(PreloadBeforeLevel(new List<string>() { key }, scene));
        }

        public IEnumerator SinglePreloadFromManager(string key, SinglePreloadFromManagerMusic spfmm)
        {
            if (!spfmm.Preloaded)
            {
                Traverse traverse = Traverse.Create(typeof(SceneHelper));
                traverse.Property<string>("PendingScene").Value = spfmm.scene;

                AsyncOperationHandle handle = Addressables.LoadSceneAsync(spfmm.scene);
                Addressables.ResourceManager.Acquire(handle);

                traverse.Property<string>("PendingScene").Value = null;
                traverse.Property<string>("CurrentScene").Value = spfmm.scene;

                while (!handle.IsDone) yield return null;
                yield return new WaitUntil(() => MusicManager.Instance.cleanTheme != null && MusicManager.Instance.battleTheme != null);

                GameObject keyObj = new GameObject() { name = key };
                Object.DontDestroyOnLoad(keyObj);
                keyObj.transform.SetParent(preloadParent.transform);
                spfmm.gameObjects.Add(keyObj);

                GameObject cleanObj = MusicManager.Instance.cleanTheme.gameObject;
                cleanObj.transform.SetParent(null);
                AudioSource cleanSource = cleanObj.GetComponent<AudioSource>();
                spfmm.audioClip = cleanSource.clip;
                cleanObj.transform.SetParent(keyObj.transform);

                spfmm.Preloaded = true;
            }
        }

        public IEnumerator MultiPreloadFromManager(string key, MultiPreloadFromManagerMusic mpfmm)
        {
            if (!mpfmm.Preloaded)
            {
                Traverse traverse = Traverse.Create(typeof(SceneHelper));
                traverse.Property<string>("PendingScene").Value = mpfmm.scene;

                AsyncOperationHandle handle = Addressables.LoadSceneAsync(mpfmm.scene);
                Addressables.ResourceManager.Acquire(handle);

                traverse.Property<string>("PendingScene").Value = null;
                traverse.Property<string>("CurrentScene").Value = mpfmm.scene;

                while (!handle.IsDone) yield return null;
                yield return new WaitUntil(() => MusicManager.Instance.cleanTheme != null && MusicManager.Instance.battleTheme != null);

                GameObject keyObj = new GameObject() { name = key };
                Object.DontDestroyOnLoad(keyObj);
                keyObj.transform.SetParent(preloadParent.transform);
                mpfmm.gameObjects.Add(keyObj);

                GameObject cleanObj = MusicManager.Instance.cleanTheme.gameObject;
                cleanObj.transform.SetParent(null);
                AudioSource cleanSource = cleanObj.GetComponent<AudioSource>();
                mpfmm.clean = cleanSource.clip;
                cleanObj.transform.SetParent(keyObj.transform);

                GameObject battleObj = MusicManager.Instance.battleTheme.gameObject;
                battleObj.transform.SetParent(null);
                AudioSource battleSource = battleObj.GetComponent<AudioSource>();
                mpfmm.battle = battleSource.clip;
                battleObj.transform.SetParent(keyObj.transform);

                mpfmm.Preloaded = true;
            }
        }

        public IEnumerator MultiPreloadFromChanger(string key, MultiPreloadFromChangerMusic mpfcm)
        {
            if (!mpfcm.Preloaded)
            {
                Traverse traverse = Traverse.Create(typeof(SceneHelper));
                traverse.Property<string>("PendingScene").Value = mpfcm.scene;

                AsyncOperationHandle handle = Addressables.LoadSceneAsync(mpfcm.scene);
                Addressables.ResourceManager.Acquire(handle);

                traverse.Property<string>("PendingScene").Value = null;
                traverse.Property<string>("CurrentScene").Value = mpfcm.scene;

                while (!handle.IsDone) yield return null;

                GameObject keyObj = new GameObject() { name = key };
                Object.DontDestroyOnLoad(keyObj);
                keyObj.transform.SetParent(preloadParent.transform);
                mpfcm.gameObjects.Add(keyObj);

                GameObject changerObj = Core.FindGameObjectFromPathInScene(mpfcm.changerPath);
                changerObj.transform.SetParent(null);
                MusicChanger musicChanger = changerObj.GetComponent<MusicChanger>();
                mpfcm.clean = musicChanger.clean;
                mpfcm.battle = musicChanger.battle;
                changerObj.transform.SetParent(keyObj.transform);

                mpfcm.Preloaded = true;
            }
        }

        public void ReleaseAllHandles()
        {
            foreach (AsyncOperationHandle handle in handles)
            {
                Addressables.Release(handle);
            }
            handles.Clear();
        }

        public void ResetPreloadedMusic()
        {
            foreach (BaseMusic music in Music.Values)
            {
                if (music is PreloadMusic preloadMusic) preloadMusic.Reset();
            }
            ReleaseAllHandles();
        }

        public void ApplySingleMusicToManager(AudioClip clip)
        {
            MusicManager musicManager = MusicManager.Instance;
            musicManager.cleanTheme.clip = clip;
            musicManager.battleTheme.clip = clip;
            musicManager.bossTheme.clip = clip;
        }

        public void ApplyMultiMusicToManager(AudioClip clean, AudioClip battle)
        {
            MusicManager musicManager = MusicManager.Instance;
            musicManager.cleanTheme.clip = clean;
            musicManager.battleTheme.clip = battle;
            musicManager.bossTheme.clip = battle;
        }

        public void ApplySingleMusicToAudioSource(AudioSourceTarget audioSourceTarget, AudioClip clip)
        {
            foreach (string path in audioSourceTarget.gameObjectPaths)
            {
                AudioSource audioSource = Core.FindGameObjectFromPathInScene(path).GetComponent<AudioSource>();
                audioSource.clip = clip;
            }
        }

        public void ApplyMultiMusicToAudioSourceSplit(AudioSourceSplitTarget splitTarget, AudioClip clean, AudioClip battle)
        {
            Core.FindGameObjectFromPathInScene(splitTarget.cleanTarget).GetComponent<AudioSource>().clip = clean;
            Core.FindGameObjectFromPathInScene(splitTarget.battleTarget).GetComponent<AudioSource>().clip = battle;
        }

        public void ApplySingleMusicToChanger(MusicChangerTarget musicChangerTarget, AudioClip clip)
        {
            foreach (string path in musicChangerTarget.gameObjectPaths)
            {
                MusicChanger musicChanger = Core.FindGameObjectFromPathInScene(path).GetComponent<MusicChanger>();
                if (musicChangerTarget.clean) musicChanger.clean = clip;
                if (musicChangerTarget.battle) musicChanger.battle = clip;
                if (musicChangerTarget.boss) musicChanger.boss = clip;
            }
        }

        public void ApplyMultiMusicToChanger(MusicChangerTarget musicChangerTarget, AudioClip clean, AudioClip battle)
        {
            foreach (string path in musicChangerTarget.gameObjectPaths)
            {
                MusicChanger musicChanger = Core.FindGameObjectFromPathInScene(path).GetComponent<MusicChanger>();
                if (musicChangerTarget.clean) musicChanger.clean = clean;
                if (musicChangerTarget.battle) musicChanger.battle = battle;
                if (musicChangerTarget.boss) musicChanger.boss = battle;
            }
        }

        public static List<string> GetKeysForLevel(int level)
        {
            List<string> list = new List<string>();
            string pattern = @"^" + level.ToString() + @"\D?$";

            foreach (string key in Targets.Keys)
            {
                if (Regex.IsMatch(key, pattern)) list.Add(key);
            }

            return list;
        }

        public void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }

            Instance = this;

            preloadParent = new GameObject();
            preloadParent.transform.SetParent(transform);
            preloadParent.name = "Music";
        }
    }
}
