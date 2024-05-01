from dataclasses import dataclass
from Options import Choice, Range, Toggle, DefaultOnToggle, DeathLink, PerGameCommonOptions


class Goal(Choice):
    """Choose which level must be completed to finish the game."""
    display_name = "Goal"
    option_1_4 = 0
    option_2_4 = 1
    option_3_2 = 2
    option_4_4 = 3
    option_5_4 = 4
    option_6_2 = 5
    option_7_4 = 8
    option_P_1 = 6
    option_P_2 = 7
    default = 5


class GoalRequirement(Range):
    """Choose the number of levels that must be completed to unlock the goal."""
    display_name = "Goal Requirement"
    range_start = 5
    range_end = 33
    default = 15


class SecretMissionClear(DefaultOnToggle):
    """Choose if completing secret missions will count towards unlocking the goal or not."""
    display_name = "Include Secret Mission Completion"


class UnlockType(Choice):
    """Choose if levels will be unlocked one at a time, or whole layers at once."""
    display_name = "Level Unlock Type"
    option_levels = 0
    option_layers = 1
    default = 0


class TrapPercent(Range):
    """Choose the percentage of trap items that will appear when filling the item pool with junk."""
    display_name = "Trap Item Percentage"
    range_start = 0
    range_end = 100
    default = 25


class BossRewards(Choice):
    """Adds rewards for defeating the bosses in the final level of each layer. 
    \"Extended\" also adds rewards for some optional bosses."""
    display_name = "Boss Rewards"
    option_disabled = 0
    option_standard = 1
    option_extended = 2
    default = 0


class Challenges(Toggle):
    """Adds rewards for completing each level's challenge, except for the goal."""
    display_name = "Challenge Rewards"


class PRanks(Toggle):
    """Adds rewards for completing each level with a Perfect Rank, except for the goal."""
    display_name = "P Rank Rewards"


class HankRewards(Toggle):
    """Adds rewards for giving Hank and Hank Jr. a head in 1-4 and 5-3."""
    display_name = "Hank Rewards"


class ClashReward(Toggle):
    """Randomizes the unlock for destroying all crates in 4-S."""
    display_name = "Randomize Clash Mode"


class FishRewards(Toggle):
    """Adds rewards for catching each fish in 5-S."""
    display_name = "Fish Rewards"


class CleanRewards(Toggle):
    """Adds rewards for cleaning every room in 7-S."""
    display_name = "Cleaning Rewards"


class ChessReward(Toggle):
    """Adds a reward for winning chess against a bot in the Developer Museum."""
    display_name = "Chess Reward"


class RocketRaceReward(Toggle):
    """Adds a reward for winning the rocket race in the Developer Museum."""
    display_name = "Rocket Race Reward"


class StartingWeapon(Choice):
    """Choose what the starting weapon in 0-1 will be."""
    display_name = "Starting Weapon"
    option_revolver = 0
    option_any_weapon = 1
    option_any_arm = 2
    option_any_weapon_or_arm = 3
    default = 1


class RandomizeFire2(Toggle):
    """Locks the ability to use a weapon's alternate fire behind items that must be found in the multiworld."""
    display_name = "Randomize Secondary Fire"


class StartWithArm(DefaultOnToggle):
    """Choose whether or not to start the game with the Feedbacker arm."""
    display_name = "Start with Feedbacker"


class StartingStamina(Range):
    """Choose how many bars of stamina to start with."""
    display_name = "Starting Stamina"
    range_start = 0
    range_end = 3
    default = 3


class StartingWalljumps(Range):
    """Choose how many wall jumps to start with."""
    display_name = "Starting Wall Jumps"
    range_start = 0
    range_end = 3
    default = 3


class StartWithSlide(DefaultOnToggle):
    """Choose whether or not to start the game with the ability to slide."""
    display_name = "Start with Slide"


class StartWithSlam(DefaultOnToggle):
    """Choose whether or not to start the game with the ability to slam."""
    display_name = "Start with Slam"


class RevForm(Choice):
    """Choose whether the revolver should be in its standard or alternate form initially.
    The other form must be unlocked."""
    display_name = "Revolver Form"
    option_standard = 0
    option_alternate = 1
    default = 0


class ShoForm(Choice):
    """Choose whether the shotgun should be in its standard or alternate form initially.
    The other form must be unlocked."""
    display_name = "Shotgun Form"
    option_standard = 0
    option_alternate = 1
    default = 0


class NaiForm(Choice):
    """Choose whether the nailgun should be in its standard or alternate form initially.
    The other form must be unlocked."""
    display_name = "Nailgun Form"
    option_standard = 0
    option_alternate = 1
    default = 0


class RandomizeSkulls(Toggle):
    """Turns the red and blue skulls into items that must be found before they will appear in levels."""
    display_name = "Randomize Skulls"


class LimboSwitch(Toggle):
    """Randomizes the four switches in the Limbo layer."""
    display_name = "Randomize Limbo Switches"


class ViolenceSwitch(Toggle):
    """Randomizes the three switches in 7-2."""
    display_name = "Randomize Violence Switches"


class PointMultiplier(Range):
    """Multiplies the amount of points earned to reduce grind."""
    display_name = "Point Multiplier"
    range_start = 1
    range_end = 10
    default = 1


class UIColorRando(Choice):
    """Randomizes UI colors, either once at the beginning of a run, or every time a level is loaded.
    Does not affect item popup colors.
    This option can be changed later."""
    display_name = "UI Color Randomizer"
    option_disabled = 0
    option_once = 1
    option_every_load = 2
    default = 0


class GunColorRando(Choice):
    """Randomizes gun colors, either once at the beginning of a run, or every time a level is loaded.
    This option can be changed later."""
    display_name = "Gun Color Randomizer"
    option_disabled = 0
    option_once = 1
    option_every_load = 2
    default = 0


class MusicRando(Toggle):
    """Randomizes the music that plays during the game.
    Some music is never randomized."""
    display_name = "Music Randomizer"


class CybergrindHints(DefaultOnToggle):
    """Every 5 waves cleared in The Cyber Grind will unlock a hint.
    This option can NOT be changed later."""
    display_name = "Cyber Grind Hints"


class UltrakillDeathLink(DeathLink):
    """When you die, everyone dies. The reverse is also true.
    This option can be changed later."""


@dataclass
class UltrakillOptions(PerGameCommonOptions):
    goal: Goal
    goal_requirement: GoalRequirement
    include_secret_mission_completion: SecretMissionClear
    unlock_type: UnlockType
    trap_percent: TrapPercent
    boss_rewards: BossRewards
    challenge_rewards: Challenges
    p_rank_rewards: PRanks
    hank_rewards: HankRewards
    randomize_clash_mode: ClashReward
    fish_rewards: FishRewards
    cleaning_rewards: CleanRewards
    chess_reward: ChessReward
    rocket_race_reward: RocketRaceReward
    starting_weapon: StartingWeapon
    randomize_secondary_fire: RandomizeFire2
    start_with_arm: StartWithArm
    starting_stamina: StartingStamina
    starting_walljumps: StartingWalljumps
    start_with_slide: StartWithSlide
    start_with_slam: StartWithSlam
    revolver_form: RevForm
    shotgun_form: ShoForm
    nailgun_form: NaiForm
    randomize_skulls: RandomizeSkulls
    randomize_limbo_switches: LimboSwitch
    randomize_violence_switches: ViolenceSwitch
    point_multiplier: PointMultiplier
    ui_color_randomizer: UIColorRando
    gun_color_randomizer: GunColorRando
    music_randomizer: MusicRando
    cybergrind_hints: CybergrindHints
    death_link: UltrakillDeathLink