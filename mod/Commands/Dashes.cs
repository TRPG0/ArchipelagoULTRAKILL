using ArchipelagoULTRAKILL.Config;
using GameConsole;

namespace ArchipelagoULTRAKILL.Commands
{
    public class Dashes : ICommand
    {
        public string Name => "Dashes";
        public string Description => "Adjust the number of dashes the player can use while in test mode.";
        public string Command => "dashes";

        public void Execute(Console con, string[] args)
        {
            if (!(Core.TestMode ?? false))
            {
                Core.PLogger.Info("Save slot is not in test mode.");
                return;
            }

            if (args.Length != 1)
            {
                Core.PLogger.Info("Usage: dashes <count>");
            }
            else
            {
                if (int.TryParse(args[0], out int value))
                {
                    if (value > 3) value = 3;
                    if (value < 0) value = 0;
                    Core.data.dashes = value;
                    TestConfig.dashes.value = value;
                    Core.PLogger.Info($"Set dashes to {value}");
                }
                else
                {
                    Core.PLogger.Info("Count does not appear to be a valid integer.");
                }
            }
        }
    }
}
