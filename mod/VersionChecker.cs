using Newtonsoft.Json.Linq;
using PluginConfig.API.Decorators;
using System.Collections;
using UnityEngine.Networking;

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
                        Core.Logger.LogWarning("A new version of Archipelago is available: " + latest + " | Current version: " + Core.PluginVersion);
                        GameConsole.Console.Instance.PrintLine("[Archipelago] A new version of Archipelago is available: " + latest + " | Current version: " + Core.PluginVersion, GameConsole.ConsoleLogType.Warning);
                        new ConfigHeader(ConfigManager.config.rootPanel, "A new version of Archipelago is available!") { textColor = UnityEngine.Color.yellow };
                    }
                    else
                    {
                        Core.Logger.LogMessage("The current version (" + Core.PluginVersion + ") is newer than the latest release. (" + latest + ")");
                        GameConsole.Console.Instance.PrintLine("[Archipelago] The current version (" + Core.PluginVersion + ") is newer than the latest release. (" + latest + ")");
                    }
                }
                else
                {
                    Core.Logger.LogMessage("Archipelago is up to date.");
                    GameConsole.Console.Instance.PrintLine("[Archipelago] Archipelago is up to date.");
                }
                yield break;
            }
        }
    }
}
