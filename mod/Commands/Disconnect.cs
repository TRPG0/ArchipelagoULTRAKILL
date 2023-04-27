using GameConsole;
using UnityEngine.UI;

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
                con.PrintLine("Usage: disconnect");
                return;
            }
            else
            {
                if (!Multiworld.Authenticated)
                {
                    con.PrintLine("You aren't connected to an Archipelago server.");
                    return;
                }
                else
                {
                    Multiworld.Disconnect();

                    if (SceneHelper.CurrentScene == "Main Menu")
                    {
                        UIManager.menuIcon.GetComponent<Image>().color = LocationManager.colors["red"];
                    }

                    return;
                }
            }
        }
    }
}
