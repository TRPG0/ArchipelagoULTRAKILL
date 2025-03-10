﻿using GameConsole;
using UnityEngine.UI;
using plog.Models;
using Colors = ArchipelagoULTRAKILL.Structures.Colors;

namespace ArchipelagoULTRAKILL.Commands
{
    public class Disconnect : ICommand
    {
        public string Name => "Disconnect";
        public string Description => "Disconnect from an Archipelago multiworld.";
        public string Command => "disconnect";

        public void Execute(Console con, string[] args)
        {
            if (args.Length != 0)
            {
                Core.PLogger.Info("Usage: disconnect");
                return;
            }
            else
            {
                if (!Multiworld.Authenticated)
                {
                    Core.PLogger.Info("You aren't connected to an Archipelago server.");
                    return;
                }
                else
                {
                    Multiworld.Disconnect();
                    ConfigManager.connectionInfo.text = "Disconnected from server.";
                    if (SceneHelper.CurrentScene == "Main Menu")
                    {
                        UIManager.menuIcon.GetComponent<Image>().color = Colors.Red;
                    }

                    return;
                }
            }
        }
    }
}
