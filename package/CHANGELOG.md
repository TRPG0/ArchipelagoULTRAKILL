## 2.0.6

- Fixed being unable to receive some items from other players after reconnecting to a server.

## 2.0.5

- Fixed generation failing on newer Archipelago versions due to duplicate entrance names.
- Fixed no arm being equipped in 5-S.
- Fixed a bug that could immediately disconnect the player after connecting.

## 2.0.4

- Fixed item notifications not appearing most of the time when receiving items from other players.

## 2.0.3

- Fixed more weird arm bugs. [#15](https://github.com/TRPG0/ArchipelagoULTRAKILL/pull/15)
- Fixed item and location names not being parsed correctly in chat if the IDs were too large.
- Added a failsafe for handling missing items when connecting to a server.

## 2.0.2

- Fixed glass not being removed in 0-1 after restarting from a checkpoint. [#12](https://github.com/TRPG0/ArchipelagoULTRAKILL/pull/12)
- Fixed music reverting to default in 0-5 after restarting from a checkpoint.

## 2.0.1

- Fixed to work on patch 14c.
- Fixed completed levels not being counted in the stats if the final rank was a D.
- Added a goal counter to the stats.
- Added stats for Prime Sanctums.
- Fixed music randomizer not working in P-1 and P-2.

## 2.0.0

- A large portion of the mod has been rewritten.
	- Note that games started on 1.2.5 or older **will not** be playable on this version. Finish your games in progress before updating.
- Added new content from Layer 7.
- Added a summary of levels unlocked or completed, secrets found, and etc. which can be seen when hovering over an act on the act select screen.
- Several trap items have been buffed.
	- Stamina Limiters and Wall Jump Limiters both set stamina or wall jumps to 0, instead of reducing by 1.
	- Hard Damage now adds 75 instead of 50 hard damage.
	- Radiant Aura now lasts for 15 seconds instead of 10, the same as all other trap items.
- Fixed incorrect logic for 4-1 Secret #5 and Challenge.
- Tweaked how items are received, which hopefully fixes instances of not actually getting items when sent by other players.
- Fixed various bugs related to the shop.

## 1.2.5

- Quick update so that randomizer can be played on the current version.
	- This update **does not** contain any new content from Layer 7, that will be coming soon.
- Archipelago no longer depends on UMM.

## 1.2.4

- Added "hint mode", which disables all randomization, and allows connecting to other games' slots to unlock hints while playing The Cyber Grind.
- The chat box in the options menu will now properly clear itself after sending a message.
- Fixed a crash that would occur after Death Link was enabled, then disconnecting, then reconnecting.

## 1.2.3

- Death Link is now supported.
- Fixed more arm related bugs.

## 1.2.2

- Fixed sometimes being unable to use the Feedbacker when starting a new run.
- The "ARMS" panel of the shop is now visible after receiving the Feedbacker.

## 1.2.1

- Randomizer runs that were generated prior to 1.2.0 can now be played on the current version.
- Slightly tweaked the way that the level select is modified. (Should be much less annoying to navigate with a controller)

## 1.2.0

- There are now many options that can be changed in-game thanks to the [PluginConfigurator](https://thunderstore.io/c/ultrakill/p/EternalsTeam/PluginConfigurator/) mod.
    - Connecting to a server can now be done through the options menu. (You can still use the command if you want to, though)
    - Settings for the current run can be viewed in the options menu.
    - The log that appears at the bottom of the screen can be adjusted in the options menu.
    - Every UI color used by Archipelago can now be customized.
    - While connected to a server, a list of all acquired hints can be viewed in the options menu.
- You can now choose to remove secret mission completions from logic, if you would prefer not to do them.
- Unlocking hints while playing The Cyber Grind can now be disabled, if you want.
- The stats screen can now be opened anytime, even while playing levels that haven't been completed yet.
- Custom weapon colors are now always unlocked.
- A new powerup has been added: Air Jump, which allows the player to use their wall jumps as additional midair jumps.
- A new trap has been added: Radiant Aura, which gives all enemies Radiance for 10 seconds.
- You can now enable Boss Rewards, which adds location checks for the boss at the end of each layer.
    - When set to "Extended", location checks will also be added for the Very Cancerous Rodent in 1-2 and the Mysterious Druid Knight (& Owl) in 4-3.
- You can now enable Fish Rewards, which adds location checks for catching each fish in 5-S.
- The music that plays during gameplay can now be randomized. Note that some music is never randomized.
- UI and weapon colors can be randomized, either once at the beginning of each run, or every time a new level is loaded.

## 1.1.6

- Leaderboard times can no longer be submitted while playing randomizer.
- Every 5 waves cleared in The Cyber Grind will now unlock a random hint.

## 1.1.5

- Fixed: The displays on the side of the Core Eject and Pump Charge shotguns wouldn't work.
- Fixed: Shotguns would get jammed after parrying an attack.

## 1.1.4

- Fixed: Various weird things about the blue skull in 1-2.
- Fixed: Plando can now be used to place a specific weapon at the beginning of the game.

## 1.1.3

- New: The mod will now check if a new version is available, and output the result to the console.
- Fixed: The Archipelago logo on the title screen wouldn't retain its color after connecting.
- Fixed: Logic for 0-3 Secret #2 was incorrect.
- Fixed: Some secondary fire unlocks would not work correctly if the order of weapons was changed.
- Fixed: Extra piercer revolvers from picking up Dual Wield powerups could use secondary fire without having it unlocked.
- Fixed: Removing the glass in 0-1 sometimes wouldn't work, softlocking the player.
- Fixed: Some reverse skull doors weren't closing themselves.
- Fixed: Parts of 1-2 would sometimes unload if skulls were randomized, allowing the player to skip most of the level.

## 1.1.2

- Fixed: skulls disappearing even if `randomize_skulls` was disabled.
- Fixed: receiving a skull without having `randomize_skulls` enabled (by using cheats) could cause a lot of lag.


## 1.1.1

- Fixed: skull icons not appearing in the correct position if level leaderboards are enabled.
- Logic: Sharpshooter can no longer break walls
- Logic: Sharpshooter can fly with dual wield powerups to grab Secret #4 and use the secret exit in 4-2


## 1.1.0

- Now playable with the Cyber Grind update, including the new Sharpshooter revolver.


## 1.0.1

- Fix the file formatting cause I've never uploaded a mod to thunderstore before. (lol)


## 1.0.0

- Initial release.