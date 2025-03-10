import typing, random
from dataclasses import dataclass
from Options import T, Choice, Range, OptionSet, Toggle, DefaultOnToggle, ItemDict, DeathLink, PerGameCommonOptions, Removed
from .Regions import Regions
from .Items import group_dict


class UKLevelChoice(Choice):
    randomized: bool

    def __init__(self, value: int, randomized: bool = False):
        super().__init__(value)
        self.randomized = randomized

    @classmethod
    def get_option_name(cls, value: T) -> str:
        return cls.name_lookup[value].replace("_", "-")
    
    @classmethod
    def from_text(cls, text: str) -> Choice:
        text = text.lower()
        if text == "random":
            return cls(random.choice(list(cls.name_lookup)), True)
        for option_name, value in cls.options.items():
            if option_name == text:
                return cls(value)
        raise KeyError(
            f'Could not find option "{text}" for "{cls.__name__}", '
            f'known options are {", ".join(f"{option}" for option in cls.name_lookup.values())}')
    

class ItemWeights(ItemDict):
    def __init__(self, value: typing.Dict[str, int]):
        if any(item_count < 0 for item_count in value.values()):
            raise Exception("Cannot have negative item counts.")
        if all(item_count == 0 for item_count in value.values()):
            raise Exception("At least one item count must be positive.")
        super(ItemDict, self).__init__(value)


class StartLevel(UKLevelChoice):
    """
    Choose which level to start in.
    """
    display_name = "Start Level"
    option_0_1 = 1
    option_0_2 = 2
    option_1_1 = 6
    option_1_2 = 7
    option_1_3 = 8
    option_2_3 = 12
    option_3_1 = 14
    option_4_2 = 17
    default = 1


class GoalLevel(UKLevelChoice):
    """
    Choose which level must be completed to finish the game.
    """
    display_name = "Goal Level"
    option_0_1 = 1
    option_0_2 = 2
    option_0_3 = 3
    option_0_4 = 4
    option_0_5 = 5
    option_0_S = 0
    option_1_1 = 6
    option_1_2 = 7
    option_1_3 = 8
    option_1_4 = 9
    option_1_S = -1
    option_2_1 = 10
    option_2_2 = 11
    option_2_3 = 12
    option_2_4 = 13
    option_2_S = -2
    option_3_1 = 14
    option_3_2 = 15
    option_4_1 = 16
    option_4_2 = 17
    option_4_3 = 18
    option_4_4 = 19
    option_4_S = -4
    option_5_1 = 20
    option_5_2 = 21
    option_5_3 = 22
    option_5_4 = 23
    option_5_S = -5
    option_6_1 = 24
    option_6_2 = 25
    option_7_1 = 26
    option_7_2 = 27
    option_7_3 = 28
    option_7_4 = 29
    option_7_S = -7
    option_0_E = 100
    option_1_E = 101
    option_P_1 = 666
    option_P_2 = 667
    default = 29


class GoalRequirement(Range):
    """
    Choose the number of levels that must be completed to unlock the goal.
    """
    display_name = "Goal Requirement"
    range_start = 5
    range_end = 38
    default = 15


class SkipLevels(OptionSet):
    """
    List as many levels as you would like to skip completing for the goal.
    """
    display_name = "Skipped Levels"
    valid_keys = {r.short_name for r in Regions.all_regions if not (r.short_name == "shop" or r.short_name == "museum")}
    default = {"0-E", "1-E", "P-1", "P-2"}


class AutoExcludeSkip(DefaultOnToggle):
    """
    Choose if skipped levels should automatically have all of their locations excluded.
    """
    display_name = "Auto Exclude Skipped Level Locations"


class UnlockType(Choice):
    """
    Choose if levels will be unlocked one at a time, or whole layers at once.
    """
    display_name = "Level Unlock Type"
    option_levels = 0
    option_layers = 1
    default = 0


class TrapPercent(Range):
    """
    Choose the percentage of trap items that will appear when filling the item pool with junk.
    """
    display_name = "Trap Item Percentage"
    range_start = 0
    range_end = 100
    default = 25


class FillerWeights(ItemWeights):
    """Choose the odds of each filler item being created when filling the item pool with junk."""
    display_name = "Filler Item Weights"
    valid_keys = group_dict["filler"]
    default = {item: 50 for item in group_dict["filler"]}


class TrapWeights(ItemWeights):
    """Choose the odds of each trap item being created when filling the item pool with traps."""
    display_name = "Trap Item Weights"
    valid_keys = group_dict["trap"]
    default = {item: 50 for item in group_dict["trap"]}


class BossRewards(Choice):
    """
    Adds rewards for defeating the bosses in the final level of each layer.

    \"Extended\" also adds rewards for some optional bosses.
    """
    display_name = "Boss Rewards"
    option_disabled = 0
    option_standard = 1
    option_extended = 2
    default = 0


class Challenges(Toggle):
    """
    Adds rewards for completing each level's challenge, except for the goal.
    """
    display_name = "Challenge Rewards"


class PRanks(Toggle):
    """
    Adds rewards for completing each level with a Perfect Rank, except for the goal.
    """
    display_name = "P Rank Rewards"


class HankRewards(Toggle):
    """
    Adds rewards for giving Hank and Hank Jr. a head in 1-4 and 5-3.
    """
    display_name = "Hank Rewards"


class ClashReward(Toggle):
    """
    Randomizes the unlock for destroying all crates in 4-S.
    """
    display_name = "Randomize Clash Mode"


class FishRewards(Toggle):
    """
    Adds rewards for catching each fish in 5-S.
    """
    display_name = "Fish Rewards"


class CleanRewards(Toggle):
    """
    Adds rewards for cleaning every room in 7-S.
    """
    display_name = "Cleaning Rewards"


class ChessReward(Toggle):
    """
    Adds a reward for winning chess against a bot in the Developer Museum.
    """
    display_name = "Chess Reward"


class RocketRaceReward(Toggle):
    """
    Adds a reward for winning the rocket race in the Developer Museum.
    """
    display_name = "Rocket Race Reward"


class StartingWeaponPool(ItemWeights):
    """
    Choose a pool of possible starting weapons and their weights of being selected.

    Some weapons will not be considered valid depending on other options:

    If choosing to start with Feedbacker, it will be removed from the pool.

    If choosing not to start with Feedbacker, the Whiplash, Freezeframe, and all Railcannons are removed from the pool.

    If choosing to randomize secondary fire and not to start with Feedbacker, the S.R.S. Cannon and Firestarter are removed from the pool.
    
    If choosing to randomize secondary fire, not to start with Feedbacker, and for the Nailgun to be in its Alternate form, the Overheat is removed from the pool.
    """
    display_name = "Starting Weapon Pool"
    valid_keys = group_dict["start_weapons"]
    default = {item: 50 for item in group_dict["start_weapons"]}


class RandomizeFire2(Toggle):
    """
    Locks the ability to use a weapon's alternate fire behind items that must be found in the multiworld.
    """
    display_name = "Randomize Secondary Fire"


class StartWithArm(DefaultOnToggle):
    """
    Choose whether or not to start the game with the Feedbacker arm.
    """
    display_name = "Start with Feedbacker"


class StartingStamina(Range):
    """
    Choose how many bars of stamina to start with.
    """
    display_name = "Starting Stamina"
    range_start = 0
    range_end = 3
    default = 3


class StartingWalljumps(Range):
    """
    Choose how many wall jumps to start with.
    """
    display_name = "Starting Wall Jumps"
    range_start = 0
    range_end = 3
    default = 3


class StartWithSlide(DefaultOnToggle):
    """
    Choose whether or not to start the game with the ability to slide.
    """
    display_name = "Start with Slide"


class StartWithSlam(DefaultOnToggle):
    """
    Choose whether or not to start the game with the ability to slam.
    """
    display_name = "Start with Slam"


class RevForm(Choice):
    """
    Choose whether the revolver should be in its standard or alternate form initially.

    The other form must be unlocked.
    """
    display_name = "Revolver Form"
    option_standard = 0
    option_alternate = 1
    default = 0


class ShoForm(Choice):
    """
    Choose whether the shotgun should be in its standard or alternate form initially.

    The other form must be unlocked.
    """
    display_name = "Shotgun Form"
    option_standard = 0
    option_alternate = 1
    default = 0


class NaiForm(Choice):
    """
    Choose whether the nailgun should be in its standard or alternate form initially.

    The other form must be unlocked.
    """
    display_name = "Nailgun Form"
    option_standard = 0
    option_alternate = 1
    default = 0


class RandomizeSkulls(Toggle):
    """
    Turns the red and blue skulls into items that must be found before they will appear in levels.
    """
    display_name = "Randomize Skulls"


class LimboSwitch(Toggle):
    """
    Randomizes the four switches in the Limbo layer.
    """
    display_name = "Randomize Limbo Switches"


class ViolenceSwitch(Toggle):
    """
    Randomizes the three switches in 7-2.
    """
    display_name = "Randomize Violence Switches"


class PointMultiplier(Range):
    """
    Multiplies the amount of points earned to reduce grind.
    """
    display_name = "Point Multiplier"
    range_start = 1
    range_end = 10
    default = 1


class UIColorRando(Choice):
    """
    Randomizes UI colors, either once at the beginning of a run, or every time a level is loaded.

    Does not affect item popup colors.

    This option can be changed later.
    """
    display_name = "UI Color Randomizer"
    option_disabled = 0
    option_once = 1
    option_every_load = 2
    default = 0


class GunColorRando(Choice):
    """
    Randomizes gun colors, either once at the beginning of a run, or every time a level is loaded.

    This option can be changed later.
    """
    display_name = "Gun Color Randomizer"
    option_disabled = 0
    option_once = 1
    option_every_load = 2
    default = 0


class MusicRando(Toggle):
    """
    Randomizes the music that plays during the game.

    Music randomizer is temporarily disabled.
    """
    display_name = "Music Randomizer"


class CybergrindHints(DefaultOnToggle):
    """
    Every 5 waves cleared in The Cyber Grind will unlock a hint.

    This option can NOT be changed later.
    """
    display_name = "Cyber Grind Hints"


class UltrakillDeathLink(DeathLink):
    """
    When you die, everyone dies. The reverse is also true.

    This option can be changed later.
    """


@dataclass
class UltrakillOptions(PerGameCommonOptions):
    start_level: StartLevel
    goal_level: GoalLevel
    goal_requirement: GoalRequirement
    skipped_levels: SkipLevels
    auto_exclude_skipped_locations: AutoExcludeSkip
    unlock_type: UnlockType
    trap_percent: TrapPercent
    filler_weights: FillerWeights
    trap_weights: TrapWeights
    boss_rewards: BossRewards
    challenge_rewards: Challenges
    p_rank_rewards: PRanks
    hank_rewards: HankRewards
    randomize_clash_mode: ClashReward
    fish_rewards: FishRewards
    cleaning_rewards: CleanRewards
    chess_reward: ChessReward
    rocket_race_reward: RocketRaceReward
    starting_weapon_pool: StartingWeaponPool
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

    goal: Removed
    include_secret_mission_completion: Removed
    starting_weapon: Removed
