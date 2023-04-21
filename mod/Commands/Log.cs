using GameConsole;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Commands
{
    public class Log : ICommand
    {
        public string Name => "Log";
        public string Description => "Adjust the messages from the randomizer.";
        public string Command => "log";

        public void Execute(Console con, string[] args)
        {
            if (args.Length > 2)
            {
                con.PrintLine("Usage: log size <int> | log lines <int> | log clear | log toggle");
                return;
            }
            else
            {
                if (args[0] == "size")
                {
                    bool success = int.TryParse(args[1], out int size);
                    if (!success)
                    {
                        con.PrintLine("Couldn't parse int.");
                        return;
                    }
                    else
                    {
                        if (size < 0) size = 0;
                        UIManager.log.GetComponent<Text>().fontSize = size;
                        UMM.UKMod.SetPersistentModData("message_log_font_size", size.ToString(), "Archipelago");
                        con.PrintLine("Font size set to " + size + ".");
                        return;
                    }
                }
                else if (args[0] == "lines")
                {
                    bool success = int.TryParse(args[1], out int lines);
                    if (!success)
                    {
                        con.PrintLine("Couldn't parse int.");
                        return;
                    }
                    else
                    {
                        if (lines < 1) lines = 1;
                        UIManager.lines = lines;

                        while (Multiworld.messages.Count > lines) Multiworld.messages.RemoveAt(0);

                        UMM.UKMod.SetPersistentModData("message_log_lines", lines.ToString(), "Archipelago");
                        con.PrintLine("Message log will now display " + lines + " lines at once.");
                        return;
                    }
                }
                else if (args[0] == "clear")
                {
                    UIManager.log.GetComponent<Text>().text = "";
                    Multiworld.messages.Clear();
                    con.PrintLine("Messages cleared.");
                    return;
                }
                else if (args[0] == "toggle")
                {
                    if (UIManager.log.activeSelf)
                    {
                        UIManager.log.SetActive(false);
                        con.PrintLine("Messages are hidden.");
                        return;
                    }
                    else
                    {
                        UIManager.log.SetActive(true);
                        con.PrintLine("Messages are no longer hidden");
                        return;
                    }
                }
                else
                {
                    con.PrintLine("Usage: log size <int> | log lines <int> | log clear | log toggle");
                    return;
                }
            }
        }
    }
}
