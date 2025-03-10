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
            using (UnityWebRequest uwr = UnityWebRequest.Get("https://api.github.com/repos/TRPG0/ArchipelagoULTRAKILL/tags"))
            {
                yield return uwr.SendWebRequest();
                if (uwr == null)
                {
                    Core.Logger.LogError("Web request was null.");
                    yield break;
                }
                if (uwr.result >= UnityWebRequest.Result.ConnectionError)
                {
                    Core.Logger.LogError("Couldn't get version from url: " + uwr.error);
                    yield break;
                }
                string text = uwr.downloadHandler.text;
                JArray jObjects = JArray.Parse(text);
                string latest = jObjects[0].Value<string>("name");
                if (Core.PluginVersion != latest)
                {
                    if (Core.PluginVersion.CompareTo(latest) < 0)
                    {
                        Core.PLogger.Warning($"A new version of Archipelago is available: {latest} | Current version: {Core.PluginVersion}");
                        new ConfigHeader(ConfigManager.config.rootPanel, "A new version of Archipelago is available!") { textColor = UnityEngine.Color.yellow };
                    }
                    else
                    {
                        Core.PLogger.Info($"The current version ({Core.PluginVersion}) is newer than the latest release. ({latest})");
                        new ConfigHeader(ConfigManager.config.rootPanel, $"The current version ({Core.PluginVersion}) is newer than the latest release. ({latest}) Have fun! :)") { textSize = 16 };
                    }
                }
                else
                {
                    Core.PLogger.Info("Archipelago is up to date.");
                }
                yield break;
            }
        }
    }
}
