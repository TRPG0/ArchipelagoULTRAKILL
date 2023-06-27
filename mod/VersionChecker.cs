using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Core.logger.LogError("Web request was null.");
                    yield break;
                }
                if (www.isNetworkError)
                {
                    Core.logger.LogError("Couldn't get version from url: " + www.error);
                    yield break;
                }
                string text = www.downloadHandler.text;
                JArray jObjects = JArray.Parse(text);
                string latest = jObjects[0].Value<string>("name");
                if (Core.ModVersion != latest)
                {
                    if (Core.ModVersion.CompareTo(latest) < 0)
                    {
                        Core.logger.LogWarning("A new version of Archipelago is available: " + latest + " | Current version: " + Core.ModVersion);
                        GameConsole.Console.Instance.PrintLine("[Archipelago] A new version of Archipelago is available: " + latest + " | Current version: " + Core.ModVersion, GameConsole.ConsoleLogType.Warning);
                    }
                    else
                    {
                        Core.logger.LogMessage("The current version (" + Core.ModVersion + ") is newer than the latest release. (" + latest + ")");
                        GameConsole.Console.Instance.PrintLine("[Archipelago] The current version (" + Core.ModVersion + ") is newer than the latest release. (" + latest + ")");
                    }
                }
                else
                {
                    Core.logger.LogMessage("Archipelago is up to date.");
                    GameConsole.Console.Instance.PrintLine("[Archipelago] Archipelago is up to date.");
                }
                yield break;
            }
        }
    }
}
