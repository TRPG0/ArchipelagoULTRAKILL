using Newtonsoft.Json.Linq;
using PluginConfig.API.Decorators;
using System.Collections;
using UnityEngine.Networking;
using plog.Models;

namespace ArchipelagoULTRAKILL
{
    public class VersionChecker
    {
        public static IEnumerator CheckVersion()
        {
            using (UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/repos/TRPG0/ArchipelagoULTRAKILL/tags"))
            {
                yield return www.SendWebRequest();
                if (www == null)
                {
                    Core.Logger.LogError("Web request was null.");
                    yield break;
                }
                if (www.isNetworkError)
                {
                    Core.Logger.LogError("Couldn't get version from url: " + www.error);
                    yield break;
                }
                string text = www.downloadHandler.text;
                JArray jObjects = JArray.Parse(text);
                string latest = jObjects[0].Value<string>("name");
                if (Core.PluginVersion != latest)
                {
                    if (Core.PluginVersion.CompareTo(latest) < 0)
                    {
                        Core.PLogger.Log($"A new version of Archipelago is available: {latest} | Current version: {Core.PluginVersion}", Level.Warning);
                        new ConfigHeader(ConfigManager.config.rootPanel, "A new version of Archipelago is available!") { textColor = UnityEngine.Color.yellow };
                    }
                    else
                    {
                        Core.PLogger.Log($"The current version ({Core.PluginVersion}) is newer than the latest release. ({latest})", Level.Info);
                    }
                }
                else
                {
                    Core.PLogger.Log("Archipelago is up to date.", Level.Info);
                }
                yield break;
            }
        }
    }
}
