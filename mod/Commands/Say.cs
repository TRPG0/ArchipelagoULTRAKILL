using Archipelago.MultiClient.Net.Packets;
using GameConsole;
using plog.Models;

namespace ArchipelagoULTRAKILL.Commands
{
    public class Say : ICommand
    {
        public string Name => "Say";
        public string Description => "Send a messaage to an Archipelago server.";
        public string Command => "say";

        public void Execute(Console con, string[] args)
        {
            if (!Multiworld.Authenticated)
            {
                Core.PLogger.Log("You aren't connected to an Archipelago server.", Level.Info);
                return;
            }
            else
            {
                string message = string.Join(" ", args);
                Multiworld.Session.Say(message);
                return;
            }
        }
    }
}
