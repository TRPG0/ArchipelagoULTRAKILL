using Archipelago.MultiClient.Net.Packets;
using GameConsole;

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
                con.PrintLine("You aren't connected to an Archipelago server.");
                return;
            }
            else
            {
                string message = string.Join(" ", args);
                Multiworld.Session.Socket.SendPacket(new SayPacket() { Text = message });
                return;
            }
        }
    }
}
