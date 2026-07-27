using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using ArchipelagoULTRAKILL.Config;
using System.Collections.Generic;
using BepInEx.Bootstrap;
using BepInEx;
using System;

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
                        ConfigManager.versionCheck.hidden = false;
                        ConfigManager.versionCheck.text = "A new version of Archipelago is available!";
                        ConfigManager.versionCheck.textColor = Color.yellow;
                    }
                    else
                    {
                        Core.PLogger.Info($"The current version ({Core.PluginVersion}) is newer than the latest release. ({latest})");
                        ConfigManager.versionCheck.hidden = false;
                        ConfigManager.versionCheck.text = $"The current version ({Core.PluginVersion}) is newer than the latest release. ({latest}) Have fun! :)";
                    }
                }
                else
                {
                    Core.PLogger.Info("Archipelago is up to date.");
                }
                yield break;
            }
        }

        public static void CheckPluginIncompatibilities()
        {
            List<string> incompatibleGUIDs = new List<string>()
            {
                "duviz.UltraNet",
                "xzxADIxzx.Jaket",
                "com.d1g1tal.polarite",
                "waffle.ultrakill.ultratweaker"
            };

            List<string> incompatibleNames = new List<string>();

            foreach (PluginInfo plugin in Chainloader.PluginInfos.Values)
            {
                if (incompatibleGUIDs.Contains(plugin.Metadata.GUID)) incompatibleNames.Add(plugin.Metadata.Name);
            }

            if (incompatibleNames.Count > 0)
            {
                string message = $"{incompatibleNames.Count} mod{(incompatibleNames.Count > 1 ? "s are" : " is")} installed that {(incompatibleNames.Count > 1 ? "are" : "is")} known to cause issues while playing Archipelago:";
                Core.Logger.LogWarning($"{message} {String.Join(", ", incompatibleNames)}");
                ConfigManager.incompatibilityCheck.text = $"{message}\n{String.Join(", ", incompatibleNames)}";
                ConfigManager.incompatibilityCheck.hidden = false;
            }
        }
    }
}
