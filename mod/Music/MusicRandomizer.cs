using ArchipelagoULTRAKILL.Config;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Music
{
    public class MusicRandomizer : MonoBehaviour
    {
        public static MusicRandomizer Instance;

        public static bool ShouldRandomizeMusic => Core.data.musicRandomizer && Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Flags.HasFlag(InfoFlags.HasRandomMusic) && Instance && !Instance.IsPreloading && DictIsValid;
        public static bool DictIsValid { get; private set; } = false;

        private GameObject preloadParent;

        public bool IsPreloading { get; private set; } = false;
        private List<AsyncOperationHandle> handles = new List<AsyncOperationHandle>();
        public float AllHandlesPercentComplete
        {
            get
            {
                if (handles.Count == 0) return 1;

                float current = 0;
                foreach (AsyncOperationHandle handle in handles) if (handle.IsValid()) current += handle.PercentComplete;
                return current / handles.Count;
            }
        }
        public bool AllHandlesDone
        {
            get
            {
                if (handles.Count == 0) return true;
                foreach (AsyncOperationHandle handle in handles) if (handle.IsValid() && !handle.IsDone) return false;
                return true;
            }
        }

        public static void ValidateMusicDictionary()
        {
            List<string> multiLayer = new List<string>();
            List<string> singleLayer = new List<string>();

            foreach (KeyValuePair<string, BaseMusic> kvp in MusicList.Music)
            {
                if (kvp.Value.IsMultiMusic) multiLayer.Add(kvp.Key);
                else singleLayer.Add(kvp.Key);
            }

            foreach (KeyValuePair<string, string> kvp in Core.data.music)
            {
                if ((multiLayer.Contains(kvp.Key) && singleLayer.Contains(kvp.Value)) 
                    || (multiLayer.Contains(kvp.Value) && singleLayer.Contains(kvp.Key))
                    || (!multiLayer.Contains(kvp.Key) && !singleLayer.Contains(kvp.Key)))
                {
                    Core.Logger.LogWarning($"Music dictionary is invalid. {kvp.Key} {kvp.Value}");
                    MusicConfig.invalidMessage.hidden = false;
                    DictIsValid = false;
                    return;
                }
            }
            MusicConfig.invalidMessage.hidden = true;
            DictIsValid = true;
        }

        public static void RerandomizeMusicDictionary()
        {
            List<string> multiLayer1 = new List<string>();
            List<string> singleLayer1 = new List<string>();
            List<string> multiLayer2 = new List<string>();
            List<string> singleLayer2 = new List<string>();

            foreach (KeyValuePair<string, BaseMusic> kvp in MusicList.Music)
            {
                if (kvp.Value.IsMultiMusic)
                {
                    multiLayer1.Add(kvp.Key);
                    multiLayer2.Add(kvp.Key);
                }
                else
                {
                    singleLayer1.Add(kvp.Key);
                    singleLayer2.Add(kvp.Key);
                }
            }

            Dictionary<string, string> temp = new Dictionary<string, string>();
            Dictionary<string, string> final = new Dictionary<string, string>();

            while (multiLayer1.Count > 0)
            {
                string key1 = multiLayer1[Random.Range(0, multiLayer1.Count)];
                string key2 = multiLayer2[Random.Range(0, multiLayer2.Count)];

                temp[key1] = key2;
                multiLayer1.Remove(key1);
                multiLayer2.Remove(key2);
            }

            while (singleLayer1.Count > 0)
            {
                string key1 = singleLayer1[Random.Range(0, singleLayer1.Count)];
                string key2 = singleLayer2[Random.Range(0, singleLayer2.Count)];

                temp[key1] = key2;
                singleLayer1.Remove(key1);
                singleLayer2.Remove(key2);
            }

            foreach (string key in MusicList.Music.Keys)
            {
                final[key] = temp[key];
            }

            Core.data.music = final;
            ValidateMusicDictionary();
        }

        private List<object> TestLoadAll()
        {
            List<object> list = new List<object>();
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (BaseMusic music in MusicList.Music.Values)
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
            if (Core.DataExists() && Core.data.musicRandomizer)
            {
                List<string> preloadKeys = new List<string>();

                foreach (KeyValuePair<string, string> kvp in GetKeysForLevel(level))
                {
                    if (MusicList.Music[kvp.Value] is PreloadMusic pm)
                    {
                        if (pm.scene != scene) preloadKeys.Add(kvp.Value);
                    }
                }

                if (preloadKeys.Count > 0)
                {
                    if (MusicConfig.allowPreload.value)
                    {
                        StartCoroutine(PreloadBeforeLevel(preloadKeys, scene));
                        return;
                    }
                    else Core.Logger.LogWarning($"Preloading is disabled! Skipping preload for {string.Join(", ", preloadKeys)}");
                }
            }
            
            SceneHelper.LoadScene(scene);
        }

        private IEnumerator PreloadBeforeLevel(List<string> preloadKeys, string scene)
        {
            Core.Logger.LogInfo($"Begin preloading music ({preloadKeys.Count})");
            IsPreloading = true;
            SceneHelper.ShowLoadingBlocker();
            foreach (string key in preloadKeys)
            {
                if (MusicList.Music[key] is SinglePreloadFromManagerMusic spfmm) yield return StartCoroutine(SinglePreloadFromManager(key, spfmm));
                else if (MusicList.Music[key] is MultiPreloadFromManagerMusic mpfmm) yield return StartCoroutine(MultiPreloadFromManager(key, mpfmm));
                else if (MusicList.Music[key] is MultiPreloadFromChangerMusic mpfcm) yield return StartCoroutine(MultiPreloadFromChanger(key, mpfcm));
                Core.Logger.LogInfo($"Preloading {key}");
            }
            IsPreloading = false;
            SceneHelper.DismissBlockers();
            Core.Logger.LogInfo("Preloading done!");

            yield return SceneHelper.LoadSceneAsync(scene);
        }

        private void TestPreload(string key, string scene)
        {
            if (!MusicList.Music.ContainsKey(key))
            {
                Core.Logger.LogWarning($"Music key {key} is not valid.");
                return;
            }

            StartCoroutine(PreloadBeforeLevel(new List<string>() { key }, scene));
        }

        private IEnumerator SinglePreloadFromManager(string key, SinglePreloadFromManagerMusic spfmm)
        {
            if (!spfmm.Preloaded)
            {
                Traverse traverse = Traverse.Create(typeof(SceneHelper));
                traverse.Property<string>("PendingScene").Value = spfmm.scene;

                AsyncOperationHandle handle = Addressables.LoadSceneAsync(spfmm.scene);
                handles.Add(handle);
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

                AsyncOperationHandle iconHandle = spfmm.icon.LoadAssetAsync();
                handles.Add(iconHandle);
                while (!iconHandle.IsDone) yield return null; 
            }
        }

        private IEnumerator GetSinglePreloadFromManagerInCurrentScene(string key, SinglePreloadFromManagerMusic spfmm)
        {
            if (!spfmm.Preloaded)
            {
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

                AsyncOperationHandle iconHandle = spfmm.icon.LoadAssetAsync();
                handles.Add(iconHandle);
                while (!iconHandle.IsDone) yield return null;
            }
        }

        private IEnumerator MultiPreloadFromManager(string key, MultiPreloadFromManagerMusic mpfmm)
        {
            if (!mpfmm.Preloaded)
            {
                Traverse traverse = Traverse.Create(typeof(SceneHelper));
                traverse.Property<string>("PendingScene").Value = mpfmm.scene;

                AsyncOperationHandle handle = Addressables.LoadSceneAsync(mpfmm.scene);
                handles.Add(handle);
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

                AsyncOperationHandle iconHandle = mpfmm.icon.LoadAssetAsync();
                handles.Add(iconHandle);
                while (!iconHandle.IsDone) yield return null;
            }
        }

        private IEnumerator GetMultiPreloadFromManagerInCurrentScene(string key, MultiPreloadFromManagerMusic mpfmm)
        {
            if (!mpfmm.Preloaded)
            {
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

                AsyncOperationHandle iconHandle = mpfmm.icon.LoadAssetAsync();
                handles.Add(iconHandle);
                while (!iconHandle.IsDone) yield return null;
            }
        }

        private IEnumerator MultiPreloadFromChanger(string key, MultiPreloadFromChangerMusic mpfcm)
        {
            if (!mpfcm.Preloaded)
            {
                Traverse traverse = Traverse.Create(typeof(SceneHelper));
                traverse.Property<string>("PendingScene").Value = mpfcm.scene;

                AsyncOperationHandle handle = Addressables.LoadSceneAsync(mpfcm.scene);
                handles.Add(handle);
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

                AsyncOperationHandle iconHandle = mpfcm.icon.LoadAssetAsync();
                handles.Add(iconHandle);
                while (!iconHandle.IsDone) yield return null;
            }
        }

        private IEnumerator GetMultiPreloadFromChangerInCurrentScene(string key, MultiPreloadFromChangerMusic mpfcm)
        {
            if (!mpfcm.Preloaded)
            {
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

                AsyncOperationHandle iconHandle = mpfcm.icon.LoadAssetAsync();
                handles.Add(iconHandle);
                while (!iconHandle.IsDone) yield return null;
            }
        }

        public void ReleaseAllHandles()
        {
            foreach (AsyncOperationHandle handle in handles)
            {
                if (handle.IsValid()) Addressables.Release(handle);
            }
            handles.Clear();
        }

        public void ResetAllMusic()
        {
            foreach (BaseMusic music in MusicList.Music.Values) music.Reset();
            ReleaseAllHandles();
        }

        private void LoadMultiClipMusic(MultiClipMusic mcm)
        {
            if (!mcm.audioClipClean.IsValid()) handles.Add(mcm.audioClipClean.LoadAssetAsync());
            if (!mcm.audioClipBattle.IsValid()) handles.Add(mcm.audioClipBattle.LoadAssetAsync());
            if (!mcm.icon.IsValid()) handles.Add(mcm.icon.LoadAssetAsync());
        }

        private void LoadMultiSoundtrackMusic(MultiSoundtrackMusic msm)
        {
            if (!msm.soundtrackSong.IsValid()) handles.Add(msm.soundtrackSong.LoadAssetAsync());
        }

        private void LoadMultiClipAndSoundtrackMusic(MultiClipAndSoundtrackMusic mcasm)
        {
            if (!mcasm.audioClip.IsValid()) handles.Add(mcasm.audioClip.LoadAssetAsync());
            if (!mcasm.soundtrackSong.IsValid()) handles.Add(mcasm.soundtrackSong.LoadAssetAsync());
        }

        private void LoadSingleClipMusic(SingleClipMusic scm)
        {
            if (!scm.audioClip.IsValid()) handles.Add(scm.audioClip.LoadAssetAsync());
            if (!scm.icon.IsValid()) handles.Add(scm.icon.LoadAssetAsync());
        }

        private void LoadSingleSoundtrackMusic(SingleSoundtrackMusic ssm)
        {
            if (!ssm.soundtrackSong.IsValid()) handles.Add(ssm.soundtrackSong.LoadAssetAsync());
        }

        private void ApplySingleMusicToManager(AudioClip clip, Sprite icon, string text)
        {
            MusicManager musicManager = MusicManager.Instance;
            musicManager.cleanTheme.clip = clip;
            musicManager.battleTheme.clip = clip;
            musicManager.bossTheme.clip = clip;
            NowPlaying.Instance?.SetIconAndText(icon, text, false);
        }

        private void ApplyMultiMusicToManager(AudioClip clean, AudioClip battle, Sprite icon, string text)
        {
            MusicManager musicManager = MusicManager.Instance;
            musicManager.cleanTheme.clip = clean;
            musicManager.battleTheme.clip = battle;
            musicManager.bossTheme.clip = battle;
            NowPlaying.Instance?.SetIconAndText(icon, text, false);
        }

        private void ApplySingleMusicToAudioSource(AudioSourceTarget audioSourceTarget, AudioClip clip, Sprite icon, string text)
        {
            List<NowPlayingChanger> changers = new List<NowPlayingChanger>();
            foreach (string path in audioSourceTarget.gameObjectPaths)
            {
                GameObject gameObject = Core.FindGameObjectFromPathInScene(path);
                AudioSource audioSource = Core.FindGameObjectFromPathInScene(path).GetComponent<AudioSource>();
                audioSource.clip = clip;
                if (!audioSourceTarget.doNotLink.Contains(path))
                {
                    NowPlayingChanger changer = gameObject.AddComponent<NowPlayingChanger>();
                    changer.Init(icon, text, audioSource);
                    changers.Add(changer);
                }
            }
            if (changers.Count > 1)
            {
                foreach (NowPlayingChanger changer in changers) changer.links = changers;
            }
        }

        private void ApplyMultiMusicToAudioSourceSplit(AudioSourceSplitTarget splitTarget, AudioClip clean, AudioClip battle, Sprite icon, string text)
        {
            List<NowPlayingChanger> changers = new List<NowPlayingChanger>();

            GameObject cleanTarget = Core.FindGameObjectFromPathInScene(splitTarget.cleanTarget);
            AudioSource cleanSource = cleanTarget.GetComponent<AudioSource>();
            cleanSource.clip = clean;
            NowPlayingChanger cleanChanger = cleanTarget.AddComponent<NowPlayingChanger>();
            cleanChanger.Init(icon, text, cleanSource);
            changers.Add(cleanChanger);

            GameObject battleTarget = Core.FindGameObjectFromPathInScene(splitTarget.battleTarget);
            AudioSource battleSource = battleTarget.GetComponent<AudioSource>();
            battleSource.clip = battle;
            NowPlayingChanger battleChanger = battleTarget.AddComponent<NowPlayingChanger>();
            battleChanger.Init(icon, text, battleSource);
            changers.Add(battleChanger);

            foreach (NowPlayingChanger changer in changers) changer.links = changers;
        }

        private void ApplySingleMusicToMusicChanger(MusicChangerTarget musicChangerTarget, AudioClip clip, Sprite icon, string text)
        {
            List<NowPlayingChanger> changers = new List<NowPlayingChanger>();
            foreach (string path in musicChangerTarget.gameObjectPaths)
            {
                MusicChanger musicChanger = Core.FindGameObjectFromPathInScene(path).GetComponent<MusicChanger>();
                if (musicChangerTarget.clean) musicChanger.clean = clip;
                if (musicChangerTarget.battle) musicChanger.battle = clip;
                if (musicChangerTarget.boss) musicChanger.boss = clip;
                NowPlayingChanger changer = musicChanger.gameObject.AddComponent<NowPlayingChanger>();
                changer.Init(icon, text);
                changers.Add(changer);
            }
            if (changers.Count > 1)
            {
                foreach (NowPlayingChanger changer in changers) changer.links = changers;
            }
        }

        private void ApplyMultiMusicToMusicChanger(MusicChangerTarget musicChangerTarget, AudioClip clean, AudioClip battle, Sprite icon, string text)
        {
            List<NowPlayingChanger> changers = new List<NowPlayingChanger>();
            foreach (string path in musicChangerTarget.gameObjectPaths)
            {
                MusicChanger musicChanger = Core.FindGameObjectFromPathInScene(path).GetComponent<MusicChanger>();
                if (musicChangerTarget.clean) musicChanger.clean = clean;
                if (musicChangerTarget.battle) musicChanger.battle = battle;
                if (musicChangerTarget.boss) musicChanger.boss = battle;
                NowPlayingChanger changer = musicChanger.gameObject.AddComponent<NowPlayingChanger>();
                changer.Init(icon, text);
                changers.Add(changer);
            }
            if (changers.Count > 1)
            {
                foreach (NowPlayingChanger changer in changers) changer.links = changers;
            }
        }

        private void ApplyMultiMusicToShoppingTarget(ShoppingTarget shoppingTarget, AudioClip clean, AudioClip battle, Sprite icon, string text)
        {
            foreach (string path in shoppingTarget.musicChangerPaths)
            {
                MusicChanger musicChanger = Core.FindGameObjectFromPathInScene(path).GetComponent<MusicChanger>();
                if (shoppingTarget.clean) musicChanger.clean = clean;
                if (shoppingTarget.battle) musicChanger.battle = battle;
                if (shoppingTarget.boss) musicChanger.boss = battle;
            }
            foreach (string path in shoppingTarget.audioSourcePaths)
            {
                AudioSource audioSource = Core.FindGameObjectFromPathInScene(path).GetComponent<AudioSource>();
                audioSource.clip = clean;
                audioSource.gameObject.AddComponent<AudioHighPassFilter>().cutoffFrequency = 1500;
            }
            List<NowPlayingChanger> changers = new List<NowPlayingChanger>();
            foreach (string path in shoppingTarget.objectActivatorPaths)
            {
                ObjectActivator objectActivator = Core.FindGameObjectFromPathInScene(path).GetComponent<ObjectActivator>();
                NowPlayingChanger changer = objectActivator.gameObject.AddComponent<NowPlayingChanger>();
                changer.Init(icon, text, objectActivator);
                changers.Add(changer);
            }
            if (changers.Count > 1) foreach (NowPlayingChanger changer in changers) changer.links = changers;
        }

        private void ApplyMultiMusicToSoundChanger(SoundChangerTarget soundChangerTarget, AudioClip clean, AudioClip battle, Sprite icon, string text)
        {
            List<NowPlayingChanger> changers = new List<NowPlayingChanger>();
            foreach (string path in soundChangerTarget.audioSourcePaths)
            {
                AudioSource audioSource = Core.FindGameObjectFromPathInScene(path).GetComponent<AudioSource>();
                audioSource.clip = clean;
                NowPlayingChanger changer = audioSource.gameObject.AddComponent<NowPlayingChanger>();
                changer.Init(icon, text, audioSource);
                changers.Add(changer);
            }
            foreach (string path in soundChangerTarget.soundChangerPaths)
            {
                SoundChanger soundChanger = Core.FindGameObjectFromPathInScene(path).GetComponent<SoundChanger>();
                soundChanger.newSound = battle;
            }
            if (changers.Count > 1)
            {
                foreach (NowPlayingChanger changer in changers) changer.links = changers;
            }
        }

        public static List<KeyValuePair<string, string>> GetKeysForLevel(int level)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            string pattern = @"^" + level.ToString() + @"\D?$";

            foreach (string key in MusicList.Targets.Keys)
            {
                if (Regex.IsMatch(key, pattern))
                {
                    if (!Core.data.music.ContainsKey(key)) Core.Logger.LogWarning($"Music does not contain key: {key}");
                    else list.Add(new KeyValuePair<string, string>(key, Core.data.music[key]));
                }
            }

            return list;
        }

        private IEnumerator RandomizeMusic(List<KeyValuePair<string, string>> kvps)
        {
            Core.Logger.LogInfo("Begin loading music");

            List<KeyValuePair<string, string>> identicalPairs = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> kvp in kvps)
            {
                if (kvp.Key == kvp.Value)
                {
                    Core.Logger.LogInfo($"Music key and value are identical. ({kvp.Key}) Skipping");
                    continue;
                }

                BaseMusic music = MusicList.Music[kvp.Value];
                if (music is MultiClipMusic mcm) LoadMultiClipMusic(mcm);
                else if (music is MultiSoundtrackMusic msm) LoadMultiSoundtrackMusic(msm);
                else if (music is MultiClipAndSoundtrackMusic mcasm) LoadMultiClipAndSoundtrackMusic(mcasm);
                else if (music is SingleClipMusic scm) LoadSingleClipMusic(scm);
                else if (music is SingleSoundtrackMusic ssm) LoadSingleSoundtrackMusic(ssm);
                else if (music is PreloadMusic pm)
                {
                    if (!pm.Ready)
                    {
                        if (pm.scene == SceneHelper.CurrentScene)
                        {
                            if (pm is MultiPreloadFromManagerMusic mpfmm) StartCoroutine(GetMultiPreloadFromManagerInCurrentScene(kvp.Value, mpfmm));
                            else if (pm is MultiPreloadFromChangerMusic mpfcm) StartCoroutine(GetMultiPreloadFromChangerInCurrentScene(kvp.Value, mpfcm));
                            else if (pm is SinglePreloadFromManagerMusic spfmm) StartCoroutine(GetSinglePreloadFromManagerInCurrentScene(kvp.Value, spfmm));
                        }
                        else Core.Logger.LogWarning($"Preload music {kvp.Value} is not ready!");
                    }
                    continue;
                }

                Core.Logger.LogInfo($"Loading {kvp.Value}");
            }

            foreach (KeyValuePair<string, string> kvp in identicalPairs) if (kvps.Contains(kvp)) kvps.Remove(kvp);
            if (kvps.Count == 0) yield break;

            yield return new WaitUntil(() => AllHandlesDone);
            Core.Logger.LogInfo("Finished loading music");

            foreach (KeyValuePair<string, string> kvp in kvps)
            {
                BaseMusic music = MusicList.Music[kvp.Value];
                BaseTarget target = MusicList.Targets[kvp.Key];

                Sprite nowPlayingIcon = null;
                string nowPlayingText = FormatNowPlayingText(music);
                
                if (music.IsMultiMusic)
                {
                    AudioClip clean = null;
                    AudioClip battle = null;

                    if (music is MultiClipMusic mcm)
                    {
                        nowPlayingIcon = (Sprite)mcm.icon.Asset;
                        clean = (AudioClip)mcm.audioClipClean.Asset;
                        battle = (AudioClip)mcm.audioClipBattle.Asset;
                    }
                    else if (music is MultiSoundtrackMusic msm)
                    {
                        SoundtrackSong soundtrackSong = (SoundtrackSong)msm.soundtrackSong.Asset;
                        nowPlayingIcon = soundtrackSong.icon;
                        clean = soundtrackSong.clips[msm.cleanIndex];
                        battle = soundtrackSong.clips[msm.battleIndex];
                    }
                    else if (music is MultiClipAndSoundtrackMusic mcasm)
                    {
                        SoundtrackSong soundtrackSong = (SoundtrackSong)mcasm.soundtrackSong.Asset;
                        nowPlayingIcon = soundtrackSong.icon;

                        if (mcasm.clipIsClean) clean = (AudioClip)mcasm.audioClip.Asset;
                        else battle = (AudioClip)mcasm.audioClip.Asset;

                        if (mcasm.soundtrackIsClean) clean = soundtrackSong.clips[mcasm.soundtrackIndex];
                        else battle = soundtrackSong.clips[mcasm.soundtrackIndex];
                    }
                    else if (music is MultiPreloadFromManagerMusic mpfmm)
                    {
                        nowPlayingIcon = (Sprite)mpfmm.icon.Asset;
                        if (mpfmm.Ready)
                        {
                            clean = mpfmm.clean;
                            battle = mpfmm.battle;
                        }
                    }
                    else if (music is MultiPreloadFromChangerMusic mpfcm)
                    {
                        nowPlayingIcon = (Sprite)mpfcm.icon.Asset;
                        if (mpfcm.Ready)
                        {
                            clean = mpfcm.clean;
                            battle = mpfcm.battle;
                        }
                    }

                    if (clean == null || battle == null)
                    {
                        Core.Logger.LogWarning($"An AudioClip is null. {kvp.Key} {kvp.Value}");
                        continue;
                    }

                    if (target == null) ApplyMultiMusicToManager(clean, battle, nowPlayingIcon, nowPlayingText);
                    else if (target is AudioSourceSplitTarget asst) ApplyMultiMusicToAudioSourceSplit(asst, clean, battle, nowPlayingIcon, nowPlayingText);
                    else if (target is MusicChangerTarget mct) ApplyMultiMusicToMusicChanger(mct, clean, battle, nowPlayingIcon, nowPlayingText);
                    else if (target is ShoppingTarget st) ApplyMultiMusicToShoppingTarget(st, clean, battle, nowPlayingIcon, nowPlayingText);
                    else if (target is SoundChangerTarget sct) ApplyMultiMusicToSoundChanger(sct, clean, battle, nowPlayingIcon, nowPlayingText);
                    Core.Logger.LogInfo($"Set music {kvp.Key} {kvp.Value}");
                }
                else
                {
                    AudioClip clip = null;

                    if (music is SingleClipMusic scm)
                    {
                        nowPlayingIcon = (Sprite)scm.icon.Asset;
                        clip = (AudioClip)scm.audioClip.Asset;
                    }
                    else if (music is SingleSoundtrackMusic ssm)
                    {
                        SoundtrackSong soundtrackSong = (SoundtrackSong)ssm.soundtrackSong.Asset;
                        nowPlayingIcon = soundtrackSong.icon;
                        clip = soundtrackSong.clips[ssm.clipIndex];
                    }
                    else if (music is SinglePreloadFromManagerMusic spfmm)
                    {
                        nowPlayingIcon = (Sprite)spfmm.icon.Asset;
                        if (spfmm.Ready)
                        {
                            clip = spfmm.audioClip;
                        }
                    }

                    if (clip == null)
                    {
                        Core.Logger.LogWarning($"An AudioClip is null. {kvp.Key} {kvp.Value}");
                        continue;
                    }

                    if (target == null) ApplySingleMusicToManager(clip, nowPlayingIcon, nowPlayingText);
                    else if (target is AudioSourceTarget ast) ApplySingleMusicToAudioSource(ast, clip, nowPlayingIcon, nowPlayingText);
                    else if (target is MusicChangerTarget mct) ApplySingleMusicToMusicChanger(mct, clip, nowPlayingIcon, nowPlayingText);
                    Core.Logger.LogInfo($"Set music {kvp.Key} {kvp.Value}");
                }
            }

            Core.Logger.LogInfo("Music randomizer done!");
        }

        public void RandomizeMusicForLevel()
        {
            int level = StatsManager.Instance.levelNumber;
            if (level <= 0) return;
            StartCoroutine(RandomizeMusic(GetKeysForLevel(level)));
        }

        private IEnumerator CreateNowPlaying()
        {
            if (NowPlaying.Instance == null)
            {
                GameObject npObj = new GameObject();
                npObj.name = "Now Playing";
                npObj.transform.SetParent(null);
                DontDestroyOnLoad(npObj);

                GameObject layoutObj = new GameObject();
                layoutObj.name = "Layout";
                layoutObj.transform.SetParent(npObj.transform);

                GameObject iconObj = new GameObject();
                iconObj.name = "Icon";
                iconObj.transform.SetParent(layoutObj.transform);
                Image image = iconObj.AddComponent<Image>();
                image.preserveAspect = true;

                GameObject textObj = new GameObject();
                textObj.name = "Text";
                textObj.transform.SetParent(layoutObj.transform);
                TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
                text.font = UIManager.bundle.LoadAsset<TMP_FontAsset>("assets/vcr_osd_mono_1.asset");
                text.fontMaterial = UIManager.bundle.LoadAsset<Material>("assets/vcr_osd_mono_underlay.mat");

                HorizontalLayoutGroup layout = layoutObj.AddComponent<HorizontalLayoutGroup>();
                layout.childControlHeight = false;
                layout.childControlWidth = false;
                layout.childForceExpandHeight = false;
                layout.childForceExpandWidth = false;
                layout.childScaleHeight = false;
                layout.childScaleWidth = false;
                layout.spacing = 20;
                layout.childAlignment = TextAnchor.MiddleCenter;

                AspectRatioFitter arf = iconObj.AddComponent<AspectRatioFitter>();
                arf.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
                arf.aspectRatio = 1.333f;
                iconObj.GetComponent<RectTransform>().sizeDelta = new Vector2(180, 136);

                ContentSizeFitter csf = textObj.AddComponent<ContentSizeFitter>();
                csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                npObj.transform.localPosition = new Vector3(0, 680, 0);

                AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/UI/Level Thumbnails/Locked.png");
                while (!handle.IsDone) yield return null;
                image.sprite = handle.Result;
                text.text = "test\ntest\ntest";

                Canvas canvas = npObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 100;

                CanvasScaler canvasScaler = npObj.AddComponent<CanvasScaler>();
                canvasScaler.referenceResolution = new Vector2(2560, 1600);
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

                npObj.AddComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;

                GameObject emptyObj = new GameObject();
                emptyObj.name = "Empty";
                emptyObj.transform.SetParent(npObj.transform);
                LayoutElement empty = emptyObj.AddComponent<LayoutElement>();
                empty.flexibleHeight = 20;

                npObj.AddComponent<NowPlaying>().Init(layout, empty, image, text);
                npObj.SetActive(false);
            }
        }

        public static string FormatNowPlayingText(BaseMusic baseMusic)
        {
            string fail = "Assets did not finish loading!";

            if (baseMusic is MultiClipAndSoundtrackMusic mcasm)
            {
                if (mcasm.soundtrackSong.IsDone)
                {
                    SoundtrackSong soundtrackSong = (SoundtrackSong)mcasm.soundtrackSong.Asset;
                    return $"{soundtrackSong.songName}\n{mcasm.levelNames}\n{mcasm.artistName}";
                }
                else return fail;
            }
            else if (baseMusic is BaseClipMusic bcm)
            {
                return $"{bcm.songName}\n{bcm.levelNames}\n{bcm.artistName}";
            }
            else if (baseMusic is PreloadMusic pm)
            {
                return $"{pm.songName}\n{pm.levelNames}\n{pm.artistName}";
            }
            else if (baseMusic is MultiSoundtrackMusic msm)
            {
                if (msm.soundtrackSong.IsDone)
                {
                    SoundtrackSong soundtrackSong = (SoundtrackSong)msm.soundtrackSong.Asset;
                    return $"{soundtrackSong.songName}\n{msm.levelNames}\n{msm.artistName}";
                }
                else return fail;
            }
            else if (baseMusic is SingleSoundtrackMusic ssm)
            {
                if (ssm.soundtrackSong.IsDone)
                {
                    SoundtrackSong soundtrackSong = (SoundtrackSong)ssm.soundtrackSong.Asset;
                    return $"{soundtrackSong.songName}\n{ssm.levelNames}\n{ssm.artistName}";
                }
                else return fail;
            }
            else return "?";
        }

        private void Awake()
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

            StartCoroutine(CreateNowPlaying());
        }
    }
}
