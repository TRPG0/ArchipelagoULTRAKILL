using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArchipelagoULTRAKILL.Music
{
    public static class MusicList
    {
        public static readonly Dictionary<string, BaseMusic> Music = new Dictionary<string, BaseMusic>()
        {
            ["1"] = new MultiClipMusic(
                "0-1: INTO THE FIRE",
                "Into the Fire",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-1 Into the Fire.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1.wav")),

            ["2"] = new MultiClipMusic(
                "0-2: THE MEATGRINDER",
                "Unstoppable Force",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-2 The Meatgrinder.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2.wav")),

            ["3"] = new MultiClipMusic(
                "0-3: DOUBLE DOWN",
                "Into the Fire",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-3 Double Down.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-1.wav")),

            ["4"] = new MultiClipMusic(
                "0-4: A ONE-MACHINE ARMY",
                "Unstoppable Force",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-4 A One-Machine Army.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/0-2.wav")),

            ["5"] = new MultiSoundtrackMusic(
                "0-5: CERBERUS",
                "Cerberus",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Prelude/Cerberus.asset"),
                0,
                1),

            ["6A"] = new SingleSoundtrackMusic(
                "1-1: HEART OF THE SUNRISE",
                "A Thousand Greetings",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Limbo/A Thousand Greetings.asset")),

            ["6B"] = new MultiClipMusic(
                "1-1: HEART OF THE SUNRISE",
                "A Shattered Illusion",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-1 Heart of the Sunrise.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-1.wav")),

            ["7A"] = new SinglePreloadFromManagerMusic(
                "1-2: THE BURNING WORLD",
                "Level 1-2",
                "A Thousand Greetings (Alternate)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-2 The Burning World.png")),

            ["7B"] = new MultiClipMusic(
                "1-2: THE BURNING WORLD",
                "A Complete and Utter Destruction of the Senses",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-2 The Burning World.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-2 Noise Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-2 Noise Battle.wav")),

            ["8"] = new MultiClipMusic(
                "1-3: HALLS OF SACRED REMAINS",
                "Castle Vein",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-3 Halls of Sacred Remains.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/1-3.wav")),

            ["9A"] = new SingleSoundtrackMusic(
                "1-4: CLAIR DE LUNE",
                "Claude Debussy",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Limbo/Clair de Lune.asset")),

            ["9B"] = new SingleSoundtrackMusic(
                "1-4: CLAIR DE LUNE",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Limbo/Versus.asset")),

            ["10"] = new MultiClipMusic(
                "2-1: BRIDGEBURNER",
                "Cold Winds",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/2-1 In the Air Tonight.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-1.wav")),

            ["11"] = new MultiClipMusic(
                "2-2: DEATH AT 20,000 VOLTS",
                "Requiem",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/2-2 Death at 20,000 Volts.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-2.wav")),

            ["12"] = new MultiClipMusic(
                "2-3: SHEER HEART ATTACK",
                "Panic Betrayer",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/2-3 Sheer Heart Attack.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/2-3.wav")),

            ["13"] = new SingleSoundtrackMusic(
                "2-4: COURT OF THE CORPSE KING",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Lust/In the Presence of a King.asset"),
                1),

            ["14A"] = new MultiClipMusic(
                "3-1: BELLY OF THE BEAST",
                "Guts",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/3-1 Belly of the Beast.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Guts Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Guts.wav")),

            ["14B"] = new MultiClipMusic(
                "3-1: BELLY OF THE BEAST",
                "Glory",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/3-1 Belly of the Beast.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Glory Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/3-1 Glory.wav")),

            ["15A"] = new SingleSoundtrackMusic(
                "3-2: IN THE FLESH",
                "Johann Sebastian Bach",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Gluttony/Bach BWV 639.asset")),

            ["15B"] = new SingleSoundtrackMusic(
                "3-2: IN THE FLESH",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 1/Gluttony/Divine Intervention.asset")),

            ["16"] = new MultiClipAndSoundtrackMusic(
                "4-1: SLAVES TO POWER",
                new AssetReferenceT<AudioClip>("Assets/Music/4-1 Clean.wav"),
                true,
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Greed/Dune Eternal.asset"),
                false),

            ["17"] = new MultiClipMusic(
                "4-2: GOD DAMN THE SUN",
                "Sands of Tide",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-2 God Damn the Sun.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-2.wav")),

            ["18A"] = new MultiClipMusic(
                "4-3: A SHOT IN THE DARK",
                "Dancer in the Darkness (Phase 1)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 1.wav")),

            ["18B"] = new MultiClipMusic(
                "4-3: A SHOT IN THE DARK",
                "Dancer in the Darkness (Phase 2)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 2.wav")),

            ["18C"] = new SingleClipMusic(
                "4-3: A SHOT IN THE DARK",
                "Dancer in the Darkness (Phase 3)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/4-3 A Shot in the Dark.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/4-3 Phase 3.wav")),

            ["19"] = new SingleSoundtrackMusic(
                "4-4: CLAIR DE SOLEIL",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Greed/Duel (Versus Reprise).asset")),

            ["20"] = new MultiPreloadFromManagerMusic(
                "5-1: IN THE WAKE OF POSEIDON",
                "Level 5-1",
                "Deep Blue",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/5-1 In the Wake of Poseidon.png")),

            ["22A"] = new MultiClipMusic(
                "5-3: SHIP OF FOOLS",
                "Death Odyssey",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/5-3 Ship of Fools.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3.wav")),

            ["22B"] = new MultiClipMusic(
                "5-3: SHIP OF FOOLS",
                "Death Odyssey Aftermath",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/5-3 Ship of Fools.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3 Aftermath Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/5-3 Aftermath.wav")),

            ["24A"] = new MultiClipMusic(
                "6-1: CRY FOR THE WEEPER",
                "Altars of Apostasy",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/6-1 Cry for the Weeper.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/6-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/6-1.wav")),

            ["24B"] = new SingleClipMusic(
                "6-1: CRY FOR THE WEEPER",
                "Hall of Sacreligious Remains",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/6-1 Cry for the Weeper.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/6-1 Hall of Sacreligious Remains.wav")),

            ["25A"] = new SingleSoundtrackMusic(
                "6-2: AESTHETICS OF HATE",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Heresy/Fallen Angel.asset")),

            ["25B"] = new SingleSoundtrackMusic(
                "6-2: AESTHETICS OF HATE",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 2/Heresy/The Death of Gods Will.asset")),

            ["26A"] = new SingleClipMusic(
                "7-1: GARDEN OF FORKING PATHS",
                "The World Looks White",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-1 Garden of Forking Paths.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-1 Intro.wav")),

            ["26B"] = new MultiClipMusic(
                "7-1: GARDEN OF FORKING PATHS",
                "The World Looks Red",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-1 Garden of Forking Paths.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-1 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-1.wav")),

            ["26C"] = new SingleSoundtrackMusic(
                "7-1: GARDEN OF FORKING PATHS",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Violence/Bull of Hell.asset"),
                0),

            ["26D"] = new SingleSoundtrackMusic(
                "7-1: GARDEN OF FORKING PATHS",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Violence/Bull of Hell.asset"),
                1),

            ["27A"] = new MultiClipMusic(
                "7-2: LIGHT UP THE NIGHT",
                "Do Robots Dream of Eternal Sleep?",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-2 Light Up the Night.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2 Intro Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2 Intro Battle.wav")),

            ["27B"] = new MultiClipMusic(
                "7-2: LIGHT UP THE NIGHT",
                "Hear! The Siren Song Call of Death",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-2 Light Up the Night.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-2.wav")),

            ["28A"] = new SingleClipMusic(
                "7-3: NO SOUND, NO MEMORY",
                "Suffering Leaves Suffering Leaves",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-3 No Sound No Memory.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-3 Intro Clean.wav")),

            ["28B"] = new MultiClipMusic(
                "7-3: NO SOUND, NO MEMORY",
                "Danse Macabre",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/7-3 No Sound No Memory.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/7-3.wav")),

            ["30A"] = new MultiClipMusic(
                "8-1: HURTBREAK WONDERLAND",
                "In Absentia Logos",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-1 Hurtbreak Wonderland.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-1 Intro Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-1 Intro.wav")),

            ["30B"] = new MultiClipAndSoundtrackMusic(
                "8-1: HURTBREAK WONDERLAND",
                new AssetReferenceT<AudioClip>("Assets/Music/8-1 Clean.wav"),
                true,
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Fraud/Spiral Out (Keep Going).asset"),
                false),

            ["31A"] = new MultiClipMusic(
                "8-2: THROUGH THE MIRROR",
                "Never Odd Or Even",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Intro B.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Intro A.wav")),

            ["31B"] = new MultiClipMusic(
                "8-2: THROUGH THE MIRROR",
                "No Devil Lived On",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2.wav")),

            ["31C"] = new SingleClipMusic(
                "8-2: THROUGH THE MIRROR",
                "No Devil Lived On (Ambient)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Ambient.wav")),

            ["31D"] = new MultiClipMusic(
                "8-2: THROUGH THE MIRROR",
                "No Devil Lived On (Shopping)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-2 Through the Mirror.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Shopping Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-2 Shopping.wav")),

            ["31E"] = new SingleSoundtrackMusic(
                "8-2: THROUGH THE MIRROR",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Fraud/Mirror Rim.asset")),

            ["32A"] = new MultiClipMusic(
                "8-3: DISINTEGRATION LOOP",
                "The Break (Crimson Glass deComposition)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-3 Disintegration Loop.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3 Intro Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3 Intro.wav")),

            ["32B"] = new MultiClipMusic(
                "8-3: DISINTEGRATION LOOP",
                "The Shattering Circle, or: A Charade of Shadeless Ones and Zeroes Rearranged ad Nihilum",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-3 Disintegration Loop.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/8-3.wav")),

            ["32C"] = new MultiPreloadFromChangerMusic(
                "8-3: DISINTEGRATION LOOP",
                "Level 8-3",
                "Event Horizon (Reach for the Sun and Burn! Burn! Burn!)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/8-3 Disintegration Loop.png"),
                "Musics/Third"),

            ["33"] = new SingleSoundtrackMusic(
                "8-4: FINAL FLIGHT",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Levels/Act 3/Fraud/The Fall.asset")),

            ["100A"] = new MultiClipMusic(
                "0-E: THIS HEAT, AN EVIL HEAT",
                "A Heart of Cold",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-E This Heat This Evil Heat.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Cold Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Cold.wav")),

            ["100B"] = new MultiClipMusic(
                "0-E: THIS HEAT, AN EVIL HEAT",
                "Dead Heat Pulse (A Heart of Cold)",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/0-E This Heat This Evil Heat.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Hot Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/0-E Hot.wav")),

            ["101"] = new MultiClipMusic(
                "1-E: ...THEN FELL THE ASHES",
                "A Part Falling",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/1-E And Then Fell the Ashes.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/1-E Jazz Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/Encores/1-E Jazz.wav")),

            ["666A"] = new SingleSoundtrackMusic(
                "P-1: SOUL SURVIVOR",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/Chaos.asset")),

            ["666B"] = new SingleSoundtrackMusic(
                "P-1: SOUL SURVIVOR",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/Order.asset")),

            ["667A"] = new MultiClipMusic(
                "P-2: WAIT OF THE WORLD",
                "Tenebre Rosso Sangue",
                "KEYGEN CHURCH",
                new AssetReferenceSprite("Assets/Textures/UI/Level Thumbnails/P-2 Wait of the World.png"),
                new AssetReferenceT<AudioClip>("Assets/Music/P-2 Clean.wav"),
                new AssetReferenceT<AudioClip>("Assets/Music/P-2.wav")),

            ["667B"] = new SingleSoundtrackMusic(
                "P-2: WAIT OF THE WORLD",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/Pandemonium.asset")),

            ["667C"] = new SingleSoundtrackMusic(
                "P-2: WAIT OF THE WORLD",
                new AssetReferenceSoundtrackSong("Assets/Data/Soundtrack/Prime Sanctums/War.asset"))
        };

        public static readonly Dictionary<string, BaseTarget> Targets = new Dictionary<string, BaseTarget>()
        {
            ["1"] = null,
            ["2"] = null,
            ["3"] = null,
            ["4"] = null,
            ["5"] = new SoundChangerTarget(new List<string>() { "4 - Cerberus Arena/4 Contents/Music", "4 - Cerberus Arena/4 Contents(Clone)/Music" }, new List<string>() { "4 - Cerberus Arena/4 Contents/Enemies/StatueFake (1)/Cerberus", "4 - Cerberus Arena/4 Contents(Clone)/Enemies/StatueFake (1)/Cerberus" }),
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
            ["13"] = new AudioSourceTarget(new List<string>() { "4 - Second Encounter/4 Stuff/BossMusic", "4 - Second Encounter/4 Stuff(Clone)/BossMusic" }),
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
            ["31A"] = new AudioSourceSplitTarget("Intro Music/Intro A", "Intro Music/Intro B"),
            ["31B"] = new MusicChangerTarget(new List<string>() { "2 - Reception/2 Nonstuff/EnableOnBreak", "Main Music Parent/Main Music" }),
            ["31C"] = new AudioSourceTarget(new List<string>() { "11B - Parking Garage/11B Nonstuff/AmbianceZone (1)/Ambiance", "12 - Island/12 Nonstuff/1/AmbianceZone/Ambiance", "12 - Island/12 Nonstuff/2/AmbianceZone/Ambiance", "12B - Supermarket Entrance/Ambiance (Fake Propagation)", "Exteriors/4B - Upper Exterior/4B Nonstuff (1)/AmbianceZone (2)/Ambiance", "15 - Second Deathcatcher/15 Stuff/Ambiance (Fake Propagation)" }, new List<string>() { "Exteriors/4B - Upper Exterior/4B Nonstuff (1)/AmbianceZone (2)/Ambiance", "15 - Second Deathcatcher/15 Stuff/Ambiance (Fake Propagation)" }),
            ["31D"] = new ShoppingTarget("Main Music Parent/Mall Music", new List<string>() { "12B - Supermarket Entrance/12B Nonstuff/Audio Source", "12B - Supermarket Entrance/12B Nonstuff/Audio Source (3)", "13 - Supermarket Escalators/13 Nonstuff/Audio Source (1)", "13 - Supermarket Escalators/13 Nonstuff/Audio Source (2)", "13 - Supermarket Escalators/13 Nonstuff/Audio Source (3)", "16 - Supermarket Shutters/16 Nonstuff/Audio Source (1)", "16 - Supermarket Shutters/16 Nonstuff/Audio Source (2)" }, new List<string>() { "12B - Supermarket Entrance/12B Stuff/MR Trigger", "12C - Fake Sky Room/12C Stuff/Trigger" }, false, true, true),
            ["31E"] = new AudioSourceTarget("Boss Music"),
            ["32A"] = null,
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
    }
}
