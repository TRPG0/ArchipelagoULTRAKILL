# Archipelago

## What is this?

[Archipelago](https://archipelago.gg) is a multiworld, multi-game randomizer. By connecting to an Archipelago server, any of the [supported games](https://archipelago.gg/games) can play together in a multiworld.

## What does randomization do to ULTRAKILL?

All weapons and arms are randomized. Extra abilities can also be randomized, including the ability to dash, walljump, slide, slam, and use a weapon's alternate fire.

Items are found by collecting secrets, and optionally by completing level challenges and getting Perfect Ranks.

Levels are unlocked by finding access to them within the multiworld. Levels can either be unlocked individually, or whole layers at once.

One level must be chosen as a goal for the randomizer, and a number of levels must be completed to unlock the goal. Completing secret missions and Prime Sanctums will also count. Prime Sanctums will never be considered in logic, unless chosen as the goal level.

Levels will be unlocked for all difficulties, and you can switch difficulties at any time.

Skulls can also be randomized, meaning they will not appear in any levels until found in the multiworld.

Note that the logic for the randomizer assumes that the player is willing to take intentional damage to get items.

# Setup

1. Download and install the Archipelago mod with r2modman, or manually by extracting the contents of the zip file into the `UMM Mods` folder. If you decide to install manually, you will also need to install [PluginConfigurator](https://thunderstore.io/c/ultrakill/p/EternalsTeam/PluginConfigurator/).

*Note that only one player in the multiworld will need to complete steps 2-5.*

2. Download and install [Archipelago](https://github.com/ArchipelagoMW/Archipelago/releases). Then, download `ultrakill.apworld` from the mod's [releases page](https://github.com/TRPG0/ArchipelagoULTRAKILL/releases) and add it to your `worlds` folder. (Default path: `C:\ProgramData\Archipelago\lib\worlds`)

3. Set up all `.yaml` files for each player in the multiworld, then add them to the `Players` folder. A template can be downloaded for ULTRAKILL from the mod's [releases page.](https://github.com/TRPG0/ArchipelagoULTRAKILL/releases)

4. Run `ArchipelagoGenerate.exe` to generate a game, which will appear in the `output` folder.

5. Host a game, either manually, or by [uploading](https://archipelago.gg/uploads) it to the Archipelago website.

6. Enable Archipelago in game, then restart ULTRAKILL.

7. To connect to the server, first select a new save file. Then open the options menu, click the PLUGIN CONFIG button, click Configure next to Archipelago, and open the PLAYER SETTINGS menu. Enter your name, the server's address in the form of `address:port`, and a password if necessary, then click the Connect button.

There are some additional commands that can be used by pressing `F8` to open the console:

- `connect [address:port] [player] [password]` - Connect to an Archipelago server.
- `disconnect`: Disconnect from an Archipelago server.
- `say [message]`: Send messages or commands to the server.

# Can I play ULTRAKILL randomizer without Archipelago?

Not yet. I would like to make a standalone version of the randomizer eventually, but since the Archipelago version already works well enough, I'm releasing it now.

# Something's not working!

If you have trouble setting up ULTRAKILL randomizer or have found any bugs that need fixing, feel free to ask about it on the [GitHub discussions](https://github.com/TRPG0/ArchipelagoULTRAKILL/discussions) page, or you can DM me on Discord at `TRPG#8501` / `@trpg`.