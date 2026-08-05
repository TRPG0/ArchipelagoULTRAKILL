using ArchipelagoULTRAKILL.Config;
using GameConsole;

namespace ArchipelagoULTRAKILL.Commands
{
    public class Walljumps : ICommand
    {
        public string Name => "Walljumps";
        public string Description => "Adjust the number of walljumps the player can use while in test mode.";
        public string Command => "walljumps";

        public void Execute(Console con, string[] args)
        {
            if (!(Core.TestMode ?? false))
            {
                Core.PLogger.Info("Save slot is not in test mode.");
                return;
            }

            if (args.Length != 1)
            {
                Core.PLogger.Info("Usage: walljumps <count>");
            }
            else
            {
                if (int.TryParse(args[0], out int value))
                {
                    if (value > 3) value = 3;
                    if (value < 0) value = 0;
                    Core.data.walljumps = value;
                    TestConfig.walljumps.value = value;
                    Core.PLogger.Info($"Set walljumps to {value}");
                }
                else
                {
                    Core.PLogger.Info("Count does not appear to be a valid integer.");
                }
            }
        }
    }
}
