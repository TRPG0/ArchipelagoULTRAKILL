# ULTRAKILL Multiworld Setup Guide

1. Download the Archipelago mod by using a Thunderstore Mod Manager, or manually by extracting the contents of the zip file into the `UMM Mods` folder.

2. Download and install [Archipelago](https://github.com/ArchipelagoMW/Archipelago/releases). Then, download `ultrakill.apworld` from the mod's [releases page](https://github.com/TRPG0/ArchipelagoULTRAKILL/releases) and add it to your `worlds` folder. (Default path: `C:\ProgramData\Archipelago\lib\worlds`)

3. Set up all `.yaml` files for each player in the multiworld, then add them to the `Players` folder. A template can be downloaded for ULTRAKILL from the mod's [releases page.](https://github.com/TRPG0/ArchipelagoULTRAKILL/releases)

4. Run `ArchipelagoGenerate.exe` to generate a game, which will appear in the `output` folder.

5. Host a game, either manually, or by [uploading](https://archipelago.gg/uploads) it to the Archipelago website.

6. Enable Archipelago in game, then restart ULTRAKILL.

7. To connect to the server, first select a new save file, then press `F8` and use the command `connect [address:port] [player] [password]`. The port and password are both optional - if no port is given, then the default port of `38281` will be used.

There are some additional commands that can be used:

- `disconnect`: Disconnect from an Archipelago server.
- `say [message]`: Send messages or commands to the server.
- `log`: Adjust the messages that appear at the bottom of the screen while connected to a server.
    - `log size [int]`: Change the font size.
    - `log lines [int]`: Change the number of lines that will be displayed.
    - `log clear`: Clears all messages.
    - `log toggle`: Shows/hides the messages.