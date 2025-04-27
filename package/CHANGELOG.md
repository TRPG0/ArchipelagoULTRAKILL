## 3.2.2

- Fixed a bug that would cause The Cyber Grind to freeze when getting a hint with an item from a different game.
- Fixed a logic error for Enemy: Cerberus.

## 3.2.1

- Fixed a disconnect that could happen if specific items were sent by other players before connecting for the first time.

## 3.2.0

Happy 2 years of ULTRAKILL randomizer! :)

- Revamped many item icons.
- Added an option to require the goal to be completed with a perfect rank.
- Added an option for secondary fires to unlock progressively.
- Added an option to unlock rewards when killing each enemy listed in the terminal for the first time.
	- Removed the "boss rewards" option.
- Added lists of recent locations and recent items to the pause menu. Both can be disabled in the options.
- The opacity and font used for the log at the bottom of the screen can now be changed.
- Shops now have icons of the item to be purchased.
- Shop items will now be automatically hinted when viewed for the first time.
- Weapons are no longer re-equipped every time when reconnecting.
- Fixed soap sometimes not spawning properly.
- Fixed main menu text not displaying during a holiday.
- Save slots with randomizer data but no actual save data can now be properly deleted in game.

## 3.1.5

- Fixed encore item colors not being assigned correctly.
- Removed in game hint menu.

## 3.1.4

- Fixed shop items becoming permanently inaccessible if purchased while cheats are enabled.
- Fixed the wrong number of secrets being displayed in Layer 0.
- Added unique item colors for 0-E and 1-E.
- Midair jumps with the Air Jump powerup no longer cancel all momentum.

## 3.1.3

- Re-added the "fake" Feedbacker arm because the shotgun parry bug wasn't actually fixed against Malicious Faces.
- Fixed some logic mistakes in 0-1 and 1-2.
- Added more item groups for easier hinting.

## 3.1.2

- Fixed a bug where death link could repeatedly kill the player if no checkpoints were reached before being killed.
- Fixed the rocket race message in the museum not displaying correctly.
- Adjusted logic for prime sanctums and encore levels.

## 3.1.1

- Reverted the check to prevent loading data since it wasn't working properly.
- Items that are specific to excluded levels will now be classed as filler instead of progression.
- Fixed some logic mistakes in 1-2, 2-1, and 3-2.
- Added an in-game link to the PopTracker pack.

## 3.1.0

- Music randomizer is temporarily disabled.
- Added support for new secrets in 0-3 and 0-4.
- Added support for 0-E and 1-E, including perfect ranks, skulls, and making both levels selectable as a goal.
	- Both encore levels are excluded by default.
- Removed 2-1 from starting levels.
- Adjusted logic to match new level geometry.
- Air Jump will now give the player all 3 wall jumps for the duration of the powerup.
- The player will no longer start with a "fake" Feedbacker.
- Added a check to prevent loading data when a save slot is deleted but Archipelago data is left over. (Usually happens when deleting a save file without Archipelago loaded)
- Fixed a logic error related to the challenge in 4-1 and breaking walls. [#24](https://github.com/TRPG0/ArchipelagoULTRAKILL/pull/24)
- Fixed the starting weapon appearing twice in the item pool.
- Fixed item messages and powerups not being cleared when switching save slots.
- Fixed death link frequently causing a crash if the player has not died at least once in the current level.
- Added a new logo to fit the style of the revamped title screen.
- Updated `Archipelago.MultiClient.Net` to `6.6.0`.

## 3.0.1

- Re-added a missing location. (2-2: Weapon)
- Fixed prime sanctums being inaccessible if unlock type is set to layers.
- Fixed incorrect logic in 5-3.

## 3.0.0

- Multiple levels can be chosen to start in, rather than just 0-1.
    - Full list: 0-1, 0-2, 1-1, 1-2, 1-3, 2-1, 2-3, 3-1, 4-2
- Any level can be chosen to be the goal, including secret missions and prime sanctums.
- Any level and its locations can now easily be excluded from randomizer progression.
    - In addition, prime sanctums are no longer always excluded.
- Weights can now be specified for how often each type of filler and trap item will appear.
- The odds of each starting weapon being chosen can now be customized.
    - Depending on other options, some weapons may never be chosen.
- Added new powerup item: "Confusing Aura" - For 15 seconds, enemies will ignore the player and attack each other instead.
- Added new powerup item: "Quick Charge" - For 15 seconds, weapon ammo regeneration and charge rate is tripled.
- The "Empty Ammuntion" trap item has been renamed to "Weapon Malfunction", and now has the same effect of removing all ammo/charges for weapons, but for 15 seconds instead of just once.
- Added new trap item: "Hands-Free Mode" - Prevents the use of any arms for 15 seconds.
- Added new trap item: "Short-Term Sandstorm" - Covers all enemies in sand for 15 seconds.
- Randomizing skulls will now also include the Blue Skull in P-2.
- Fixed various logic issues. (I forgot most of them ¯\\_(ツ)_/¯)
- Fixed the shop not updating correctly when viewed for the first time in a level.
- Fixed soap sometimes not spawning properly.
- The version a multiworld was generated with will now be displayed on the file select screen.
- Updated `Archipelago.MultiClient.Net` to `6.5.0`.

## 2.2.2

- Fixed a logic error for 5-2: Secret #1.
- Fixed The Cyber Grind soft locking when using Hint Mode to unlock hints for other game slots.

## 2.2.1

- Updated `Archipelago.MultiClient.Net` to `6.0.0-rc5`.
- Fixed a logic error for 7-2: Secret Weapon.
- Fixed secret mission locations not being colored correctly in hint popups.

## 2.2.0

- Fixed item notifications not working on patch 15c.
- Fixed item notifications for other players not having colored text.
- Fixed shop item descriptions for other players accidentally being partially cut off at the beginning.
- Fixed skulls for 1-4 and 5-1 being duplicated when reconnecting to a server.
- Added "Switches pressed" and "Assembled Hank" to act stats.
- Skull icons on level select are slightly smaller.
- Added skull icons to pause menu.
- Added switch icons to level select and pause menu.
- Switches now use their own unique color.
- Firestarter can now be selected as a starting weapon in certain specific cases.

## 2.1.1

- Fixed "7-2: Secret Weapon" not being checked properly.
- Fixed incorrect logic for switches in 7-2.
- Fixed secondary fire indicator being visible for weapons that don't have one when using Classic HUD.
- Fixed text log not repositioning itself when using Classic HUD.

## 2.1.0

It has been over a year since the first version of Archipelago for ULTRAKILL! Thanks for playing :)
Note that games started on 2.0.7 or older **will not** be playable on this version. Finish your games in progress before updating.

- Various adjustments/improvements for logic in Prelude and Act I. [#13](https://github.com/TRPG0/ArchipelagoULTRAKILL/pull/13) [#16](https://github.com/TRPG0/ArchipelagoULTRAKILL/pull/16)
- All items are now remote.
- Added the Sawed-On, JumpStart, Firestarter, and alternate shotgun to randomizer.
- Added a visual for whether the secondary fire is unlocked for the currently held weapon.
- Added custom graphics for the secondary fire of the Piercer, Core Eject, Attractor, and Freezeframe.
- Secondary fire items now have unique icons.
- You can now choose for the revolver, shotgun, and nailgun to be in their default or alternate forms. The form that isn't chosen must be unlocked.
- The switches in the Limbo layer and in 7-2 can now be randomized.
- Clash Mode can now be randomized.
- Added an option for rewards for every room cleaned in 7-S.
- Added an option for rewards for giving Hank a head in 1-4 and 5-3.
- Added an option for a reward for winning chess against a bot in the Developer Museum.
- Added an option for a reward for winning the rocket race in the Developer Museum.
- Powerups will now wait until the timer starts before activating.
- Powerups will no longer activate in secret missions, the sandbox, or the Developer Museum.
- Added skull icons for secret missions.
- Fixed secret missions not ending properly.
- Fixed sometimes being able to use the Core Eject and Pump Charge even when they are not unlocked.
- Fixed soap immediately being held even when no arms are unlocked.
- Fixed not having any arms equipped in 5-S.
- The save slot menu will now display the name and number of locations checked for randomized slots.
- Added new death link messages.
- Added links to Thunderstore, Github, and the AP After Dark Discord in the options menu. 

## 2.0.7

- Fixed to work on patch 15.
- Fixed sounds not playing when buying weapons from the shop.

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