using GameConsole;
using plog.Models;

namespace ArchipelagoULTRAKILL.Commands
{
    public class Connect : ICommand
    {
        public string Name => "Connect";
        public string Description => "Connect to an Archipelago multiworld.";
        public string Command => "connect";

        public void Execute(Console con, string[] args)
        {
            if (args.Length < 2)
            {
                if (args.Length != 0)
                {
                    Core.PLogger.Info("Usage: connect <address:port> <name> <password> | connect");
                    return;

                }
                else
                {
                    if (Core.data.host_name == "" || Core.data.slot_name == "" || !Core.DataExists())
                    {
                        Core.PLogger.Info("No saved connection info found. Usage: connect <address:port> <name> <password>");
                        return;
                    }
                    else
                    {
                        Core.mw.StartCoroutine("Connect");
                        UIManager.CreateGoalCounter();
                    }
                }
            }
            else
            {
                if (SceneHelper.CurrentScene != "Main Menu")
                {
                    Core.PLogger.Info("Can't do that right now. Can only connect to an Archipelago server on the main menu.");
                    return;
                }
                else if ((GameProgressSaver.GetTutorial() || GameProgressSaver.GetIntro()) && !Core.DataExists())
                {
                    Core.PLogger.Info("No Archipelago data found. Please start a new save file before connecting.");
                    return;
                }
                else
                {
                    Core.data.host_name = args[0];

                    if (args.Length == 3) Core.data.password = args[2];

                    string full = string.Join(" ", args);
                    if (full.Contains("\""))
                    {
                        string name = full.Substring(full.IndexOf("\""), (full.LastIndexOf("\"") - full.IndexOf("\"")));
                        name = name.Substring(1, name.Length - 1);
                        Core.data.slot_name = name;
                        if (args[args.Length - 1].Contains("\"")) Core.data.password = args[args.Length - 1];
                    }
                    else
                    {
                        Core.data.slot_name = args[1];
                    }

                    Core.mw.StartCoroutine("Connect");
                    UIManager.CreateGoalCounter();
                }
            }
        }
    }
}
