from Options import Choice, Range, Toggle, DefaultOnToggle, DeathLink


class Goal(Choice):
    """Choose which level must be completed to finish the game."""
    display_name = "Goal"
    option_1_4 = 0
    option_2_4 = 1
    option_3_2 = 2
    option_4_4 = 3
    option_5_4 = 4
    option_6_2 = 5
    option_P_1 = 6
    option_P_2 = 7
    default = 5


class GoalRequirement(Range):
    """Choose the number of levels that must be completed to unlock the goal."""
    display_name = "Goal Requirement"
    range_start = 5
    range_end = 29
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


class Challenges(Toggle):
    """Adds rewards for completing each level's challenge, except for the goal."""
    display_name = "Challenge Rewards"


class PRanks(Toggle):
    """Adds rewards for completing each level with a Perfect Rank, except for the goal."""
    display_name = "P Rank Rewards"


class FishRewards(Toggle):
    """Adds rewards for catching each fish in 5-S."""
    display_name = "Fish Rewards"


class StartingWeapon(Choice):
    """Choose what the starting weapon in 0-1 will be."""
    display_name = "Starting Weapon"
    option_revolver = 0
    option_any_weapon = 1
    option_any_weapon_or_arm = 2
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


class RandomizeSkulls(Toggle):
    """Turns the red and blue skulls into items that must be found before they will appear in levels."""
    display_name = "Randomize Skulls"


class PointMultiplier(Range):
    """Multiplies the amount of points earned to reduce grind."""
    display_name = "Point Multiplier"
    range_start = 1
    range_end = 10
    default = 1


class MusicRando(Toggle):
    """Randomizes the music that plays in each level.
    Some music is never randomized."""
    display_name = "Music Randomizer"


class UltrakillDeathLink(DeathLink):
    """When you die, everyone dies. The reverse is also true."""


ultrakill_options = {
    "goal": Goal,
    "goal_requirement": GoalRequirement,
    "include_secret_mission_completion": SecretMissionClear,
    "unlock_type": UnlockType,
    "trap_percent": TrapPercent,
    "challenge_rewards": Challenges,
    "p_rank_rewards": PRanks,
    "fish_rewards": FishRewards,
    "starting_weapon": StartingWeapon,
    "randomize_secondary_fire": RandomizeFire2,
    "start_with_arm": StartWithArm,
    "starting_stamina": StartingStamina,
    "starting_walljumps": StartingWalljumps,
    "start_with_slide": StartWithSlide,
    "start_with_slam": StartWithSlam,
    "randomize_skulls": RandomizeSkulls,
    "point_multiplier": PointMultiplier,
    "music_randomizer": MusicRando,
    "death_link": UltrakillDeathLink
}