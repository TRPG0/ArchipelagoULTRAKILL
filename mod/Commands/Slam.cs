using ArchipelagoULTRAKILL.Config;
using GameConsole;

namespace ArchipelagoULTRAKILL.Commands
{
    public class Slam : ICommand
    {
        public string Name => "Slam";
        public string Description => "Adjust if the player can slam while in test mode.";
        public string Command => "slam";

        public void Execute(Console con, string[] args)
        {
            if (!(Core.TestMode ?? false))
            {
                Core.PLogger.Info("Save slot is not in test mode.");
                return;
            }

            if (args.Length != 1)
            {
                Core.PLogger.Info("Usage: slam <0|1>, or slam <true|false>");
            }
            else
            {
                if (int.TryParse(args[0], out int value))
                {
                    if (value < 0 || value > 1)
                    {
                        Core.PLogger.Info("Input integer was not 0 or 1.");
                    }
                    else
                    {
                        Core.data.canSlam = value == 1;
                        TestConfig.slam.value = value == 1;
                        Core.PLogger.Info($"Set slam to {value == 1}");
                    }
                }
                else
                {
                    string arg = args[0];
                    arg = arg.Substring(0, 1).ToUpperInvariant() + arg.Substring(1).ToLowerInvariant();
                    if (bool.TryParse(arg, out bool val))
                    {
                        Core.data.canSlam = val;
                        TestConfig.slam.value = val;
                        Core.PLogger.Info($"Set slam to {val}");
                    }
                    else
                    {
                        Core.PLogger.Info("Input does not appear to be a valid integer or boolean.");
                    }
                }
            }
        }
    }
}
