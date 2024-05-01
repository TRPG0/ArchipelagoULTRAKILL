from worlds.generic.Rules import set_rule, add_rule
from BaseClasses import CollectionState
from .Regions import region_table


def dashes(state: CollectionState, player: int, has: int, needs: int) -> bool:
    dashes: int = needs - has
    return True if dashes <= 0 else state.has("Stamina Bar", player, dashes)


def walljumps(state: CollectionState, player: int, has: int, needs: int) -> bool:
    walljumps: int = needs - has
    return True if walljumps <= 0 else state.has("Wall Jump", player, walljumps)


def can_slide(state: CollectionState, player: int, option: bool) -> bool:
    return True if option else state.has("Slide", player)


def can_slam(state: CollectionState, player: int, option: bool) -> bool:
    return True if option else state.has("Slam", player)


def has_arm(state: CollectionState, player: int, option: bool) -> bool:
    return True if option else state.has("Feedbacker", player)



def revstd0(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Revolver - Piercer"""
    return state.has("Revolver - Piercer", player) if not alt_form \
        else state.has_all({"Revolver - Piercer", "Revolver - Alternate"}, player)


def revalt0(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Revolver - Piercer"""
    return state.has("Revolver - Piercer", player) if alt_form \
        else state.has_all({"Revolver - Piercer", "Revolver - Alternate"}, player)


def revany0(state: CollectionState, player: int) -> bool:
    """Revolver - Piercer"""
    return state.has("Revolver - Piercer", player)


def revstd0_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Revolver - Piercer"""
    return (
        revstd0(state, player, alt_form)
        and state.has("Secondary Fire - Piercer", player)
    ) if fire2 else revstd0(state, player, alt_form)


def revalt0_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Revolver - Piercer"""
    return (
        revalt0(state, player, alt_form)
        and state.has("Secondary Fire - Piercer", player)
    ) if fire2 else revalt0(state, player, alt_form)


def revany0_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Revolver - Piercer"""
    return (
        revany0(state, player)
        and state.has("Secondary Fire - Piercer", player)
    ) if fire2 else revany0(state, player)



def revstd1(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Revolver - Sharpshooter"""
    return state.has("Revolver - Sharpshooter", player) if not alt_form \
        else state.has_all({"Revolver - Sharpshooter", "Revolver - Alternate"}, player)


def revalt1(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Revolver - Sharpshooter"""
    return state.has("Revolver - Sharpshooter", player) if alt_form \
        else state.has_all({"Revolver - Sharpshooter", "Revolver - Alternate"}, player)


def revany1(state: CollectionState, player: int) -> bool:
    """Revolver - Sharpshooter"""
    return state.has("Revolver - Sharpshooter", player)


def revstd1_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Revolver - Sharpshooter"""
    return (
        revstd1(state, player, alt_form)
        and state.has("Secondary Fire - Sharpshooter", player)
    ) if fire2 else revstd1(state, player, alt_form)


def revalt1_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Revolver - Sharpshooter"""
    return (
        revalt1(state, player, alt_form)
        and state.has("Secondary Fire - Sharpshooter", player)
    ) if fire2 else revalt1(state, player, alt_form)


def revany1_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Revolver - Sharpshooter"""
    return (
        revany1(state, player)
        and state.has("Secondary Fire - Sharpshooter", player)
    ) if fire2 else revany1(state, player)



def revstd2(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Revolver - Marksman"""
    return state.has("Revolver - Marksman", player) if not alt_form \
        else state.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player)


def revalt2(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Revolver - Marksman"""
    return state.has("Revolver - Marksman", player) if alt_form \
        else state.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player)


def revany2(state: CollectionState, player: int) -> bool:
    """Revolver - Marksman"""
    return state.has("Revolver - Marksman", player)


def revstd2_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Revolver - Marksman"""
    return (
        revstd2(state, player, alt_form)
        and state.has("Secondary Fire - Marksman", player)
    ) if fire2 else revstd2(state, player, alt_form)


def revalt2_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Revolver - Marksman"""
    return (
        revalt2(state, player, alt_form)
        and state.has("Secondary Fire - Marksman", player)
    ) if fire2 else revalt2(state, player, alt_form)


def revany2_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Revolver - Marksman"""
    return (
        revany2(state, player)
        and state.has("Secondary Fire - Marksman", player)
    ) if fire2 else revany2(state, player)



def revstd_any(state: CollectionState, player: int, alt_form: bool) -> bool:
    return state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player) if not alt_form \
        else (
            state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player)
            and state.has("Revolver - Alternate", player)
        )


def revalt_any(state: CollectionState, player: int, alt_form: bool) -> bool:
    return state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player) if alt_form \
        else (
            state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player)
            and state.has("Revolver - Alternate", player)
        )


def rev_any(state: CollectionState, player: int) -> bool:
    return state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player)



def shostd0(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Shotgun - Core Eject"""
    return state.has("Shotgun - Core Eject", player) if not alt_form \
        else state.has_all({"Shotgun - Core Eject", "Shotgun - Alternate"}, player)


def shoalt0(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Shotgun - Core Eject"""
    return state.has("Shotgun - Core Eject", player) if not alt_form \
        else state.has_all({"Shotgun - Core Eject", "Shotgun - Alternate"}, player)


def shoany0(state: CollectionState, player: int) -> bool:
    """Shotgun - Core Eject"""
    return state.has("Shotgun - Core Eject", player)


def shostd0_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Shotgun - Core Eject"""
    return (
        shostd0(state, player, alt_form)
        and state.has("Secondary Fire - Core Eject", player)
    ) if fire2 else shostd0(state, player, alt_form)


def shoalt0_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Shotgun - Core Eject"""
    return (
        shoalt0(state, player, alt_form)
        and state.has("Secondary Fire - Core Eject", player)
    ) if fire2 else shoalt0(state, player, alt_form)


def shoany0_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Shotgun - Core Eject"""
    return (
        shoany0(state, player)
        and state.has("Secondary Fire - Core Eject", player)
    ) if fire2 else shoany0(state, player)



def shostd1(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Shotgun - Pump Charge"""
    return state.has("Shotgun - Pump Charge", player) if not alt_form \
        else state.has_all({"Shotgun - Pump Charge", "Shotgun - Alternate"}, player)


def shoalt1(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Shotgun - Pump Charge"""
    return state.has("Shotgun - Pump Charge", player) if not alt_form \
        else state.has_all({"Shotgun - Pump Charge", "Shotgun - Alternate"}, player)


def shoany1(state: CollectionState, player: int) -> bool:
    """Shotgun - Pump Charge"""
    return state.has("Shotgun - Pump Charge", player)


def shostd1_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Shotgun - Pump Charge"""
    return (
        shostd1(state, player, alt_form)
        and state.has("Secondary Fire - Pump Charge", player)
    ) if fire2 else shostd1(state, player, alt_form)


def shoalt1_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Shotgun - Pump Charge"""
    return (
        shoalt1(state, player, alt_form)
        and state.has("Secondary Fire - Pump Charge", player)
    ) if fire2 else shoalt1(state, player, alt_form)


def shoany1_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Shotgun - Pump Charge"""
    return (
        shoany1(state, player)
        and state.has("Secondary Fire - Pump Charge", player)
    ) if fire2 else shoany1(state, player)



def shostd2(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Shotgun - Sawed-On"""
    return state.has("Shotgun - Sawed-On", player) if not alt_form \
        else state.has_all({"Shotgun - Sawed-On", "Shotgun - Alternate"}, player)


def shoalt2(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Shotgun - Sawed-On"""
    return state.has("Shotgun - Sawed-On", player) if not alt_form \
        else state.has_all({"Shotgun - Sawed-On", "Shotgun - Alternate"}, player)


def shoany2(state: CollectionState, player: int) -> bool:
    """Shotgun - Sawed-On"""
    return state.has("Shotgun - Sawed-On", player)


def shostd2_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Shotgun - Sawed-On"""
    return (
        shostd2(state, player, alt_form)
        and state.has("Secondary Fire - Sawed-On", player)
    ) if fire2 else shostd2(state, player, alt_form)


def shoalt2_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Shotgun - Sawed-On"""
    return (
        shoalt2(state, player, alt_form)
        and state.has("Secondary Fire - Sawed-On", player)
    ) if fire2 else shoalt2(state, player, alt_form)


def shoany2_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Shotgun - Sawed-On"""
    return (
        shoany2(state, player)
        and state.has("Secondary Fire - Sawed-On", player)
    ) if fire2 else shoany2(state, player)



def shostd_any(state: CollectionState, player: int, alt_form: bool) -> bool:
    return state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player) if not alt_form \
        else (
            state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player)
            and state.has("Shotgun - Alternate", player)
        )


def shoalt_any(state: CollectionState, player: int, alt_form: bool) -> bool:
    return state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player) if alt_form \
        else (
            state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player)
            and state.has("Shotgun - Alternate", player)
        )


def sho_any(state: CollectionState, player: int) -> bool:
    return state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player)



def naistd0(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Nailgun - Attractor"""
    return state.has("Nailgun - Attractor", player) if not alt_form \
        else state.has_all({"Nailgun - Attractor", "Nailgun - Alternate"}, player)


def naialt0(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Nailgun - Attractor"""
    return state.has("Nailgun - Attractor", player) if not alt_form \
        else state.has_all({"Nailgun - Attractor", "Nailgun - Alternate"}, player)


def naiany0(state: CollectionState, player: int) -> bool:
    """Nailgun - Attractor"""
    return state.has("Nailgun - Attractor", player)


def naistd0_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Nailgun - Attractor"""
    return (
        naistd0(state, player, alt_form)
        and state.has("Secondary Fire - Attractor", player)
    ) if fire2 else naistd0(state, player, alt_form)


def naialt0_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Nailgun - Attractor"""
    return (
        naialt0(state, player, alt_form)
        and state.has("Secondary Fire - Attractor", player)
    ) if fire2 else naialt0(state, player, alt_form)


def naiany0_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Nailgun - Attractor"""
    return (
        naiany0(state, player)
        and state.has("Secondary Fire - Attractor", player)
    ) if fire2 else naiany0(state, player)



def naistd1(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Nailgun - Overheat"""
    return state.has("Nailgun - Overheat", player) if not alt_form \
        else state.has_all({"Nailgun - Overheat", "Nailgun - Alternate"}, player)


def naialt1(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Nailgun - Overheat"""
    return state.has("Nailgun - Overheat", player) if not alt_form \
        else state.has_all({"Nailgun - Overheat", "Nailgun - Alternate"}, player)


def naiany1(state: CollectionState, player: int) -> bool:
    """Nailgun - Overheat"""
    return state.has("Nailgun - Overheat", player)


def naistd1_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Nailgun - Overheat"""
    return (
        naistd1(state, player, alt_form)
        and state.has("Secondary Fire - Overheat", player)
    ) if fire2 else naistd1(state, player, alt_form)


def naialt1_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Nailgun - Overheat"""
    return (
        naialt1(state, player, alt_form)
        and state.has("Secondary Fire - Overheat", player)
    ) if fire2 else naialt1(state, player, alt_form)


def naiany1_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Nailgun - Overheat"""
    return (
        naiany1(state, player)
        and state.has("Secondary Fire - Overheat", player)
    ) if fire2 else naiany1(state, player)



def naistd2(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Nailgun - JumpStart"""
    return state.has("Nailgun - JumpStart", player) if not alt_form \
        else state.has_all({"Nailgun - JumpStart", "Nailgun - Alternate"}, player)


def naialt2(state: CollectionState, player: int, alt_form: bool) -> bool:
    """Nailgun - JumpStart"""
    return state.has("Nailgun - JumpStart", player) if not alt_form \
        else state.has_all({"Nailgun - JumpStart", "Nailgun - Alternate"}, player)


def naiany2(state: CollectionState, player: int) -> bool:
    """Nailgun - JumpStart"""
    return state.has("Nailgun - JumpStart", player)


def naistd2_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Nailgun - JumpStart"""
    return (
        naistd2(state, player, alt_form)
        and state.has("Secondary Fire - JumpStart", player)
    ) if fire2 else naistd2(state, player, alt_form)


def naialt2_fire2(state: CollectionState, player: int, alt_form: bool, fire2: bool) -> bool:
    """Nailgun - JumpStart"""
    return (
        naialt2(state, player, alt_form)
        and state.has("Secondary Fire - JumpStart", player)
    ) if fire2 else naialt2(state, player, alt_form)


def naiany2_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Nailgun - JumpStart"""
    return (
        naiany2(state, player)
        and state.has("Secondary Fire - JumpStart", player)
    ) if fire2 else naiany2(state, player)



def naistd_any(state: CollectionState, player: int, alt_form: bool) -> bool:
    return state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player) if not alt_form \
        else (
            state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player)
            and state.has("Nailgun - Alternate", player)
        )


def naialt_any(state: CollectionState, player: int, alt_form: bool) -> bool:
    return state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player) if alt_form \
        else (
            state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player)
            and state.has("Nailgun - Alternate", player)
        )


def nai_any(state: CollectionState, player: int) -> bool:
    return state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player)



def rai0(state: CollectionState, player: int) -> bool:
    """Railgun - Electric"""
    return state.has("Railgun - Electric", player)


def rai1(state: CollectionState, player: int) -> bool:
    """Railgun - Screwdriver"""
    return state.has("Railgun - Screwdriver", player)


def rai2(state: CollectionState, player: int) -> bool:
    """Railgun - Malicious"""
    return state.has("Railgun - Malicious", player)


def rai_any(state: CollectionState, player: int) -> bool:
    return state.has_any({"Railcannon - Electric", "Railcannon - Screwdriver", "Railcannon - Malicious"}, player)



def rock0(state: CollectionState, player: int) -> bool:
    """Rocket Launcher - Freezeframe"""
    return state.has("Rocket Launcher - Freezeframe", player)


def rock0_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Rocket Launcher - Freezeframe"""
    return state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) if fire2 else rock0(state, player)


def rock1(state: CollectionState, player: int) -> bool:
    """Rocket Launcher - S.R.S. Cannon"""
    return state.has("Rocket Launcher - S.R.S. Cannon", player)


def rock1_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Rocket Launcher - S.R.S. Cannon"""
    return state.has_all({"Rocket Launcher - S.R.S. Cannon", "Secondary Fire - S.R.S. Cannon"}, player) if fire2 else rock1(state, player)


def rock2(state: CollectionState, player: int) -> bool:
    """Rocket Launcher - Firestarter"""
    return state.has("Rocket Launcher - Firestarter", player)


def rock2_fire2(state: CollectionState, player: int, fire2: bool) -> bool:
    """Rocket Launcher - Firestarter"""
    return state.has_all({"Rocket Launcher - Firestarter", "Secondary Fire - Firestarter"}, player) if fire2 else rock2(state, player)


def rock_any(state: CollectionState, player: int) -> bool:
    return state.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Rocket Launcher - Firestarter"}, player)



def arm0(state: CollectionState, player: int) -> bool:
    """Feedbacker"""
    return state.has("Feedbacker", player)


def arm1(state: CollectionState, player: int) -> bool:
    """Knuckleblaster"""
    return state.has("Knuckleblaster", player)


def arm2(state: CollectionState, player: int) -> bool:
    """Whiplash"""
    return state.has("Whiplash", player)


def can_punch(state: CollectionState, player: int, option: bool) -> bool:
    return has_arm(state, player, option) or arm1(state, player)


def can_break_idol(state: CollectionState, player: int, arm: bool, shoalt: bool) -> bool:
    return can_punch(state, player, arm) or shoalt_any(state, player, shoalt)


def grab_item(state: CollectionState, player: int, option: bool) -> bool:
    return has_arm(state, player, option) or arm1(state, player) or arm2(state, player)


def can_zap(state: CollectionState, player: int, fire2: bool) -> bool:
    return (
        naiany2_fire2(state, player, fire2)
        or rai0(state, player)
    )


def can_proj_boost(state: CollectionState, player: int, arm: bool, shoalt: bool) -> bool:
    if arm:
        return shostd_any(state, player, shoalt)
    else:
        return (
            shostd_any(state, player, shoalt)
            and arm0(state, player)
        )


def slam_storage(state: CollectionState, player: int, slam: bool, has: int) -> bool:
    return (
        can_slam(state, player, slam)
        and walljumps(state, player, has, 1)
    )


def good_weapon(state: CollectionState, player: int, fire2: bool, arm: bool, slide: bool, dash: int, shoalt: bool, naialt: bool) -> bool:
    return (
        (
            rev_any(state, player)
            or shoany0_fire2(state, player, fire2)
            or shoany1_fire2(state, player, fire2)
            or naistd2(state, player, naialt)
            or can_proj_boost(state, player, arm, shoalt)
        )
        and (
            can_slide(state, player, slide)
            or dashes(state, player, dash, 1)
        )
    )


def can_break_glass(state: CollectionState, player: int, fire2: bool, arm: bool, revalt: bool, shoalt: bool) -> bool:
    return (
        rai0(state, player)
        or rai2(state, player)
        or rock_any(state, player)
        or arm1(state, player)
        or revstd0_fire2(state, player, revalt, fire2)
        or revstd1_fire2(state, player, revalt, fire2)
        or revstd2_fire2(state, player, revalt, fire2)
        or revalt_any(state, player, revalt)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
    )


def can_break_walls(state: CollectionState, player: int, fire2: bool, arm: bool, revalt: bool, shoalt: bool, naialt: bool) -> bool:
    return (
        rai0(state, player)
        or rai2(state, player)
        or rock_any(state, player)
        or arm1(state, player)
        or naistd1_fire2(state, player, naialt, fire2)
        or revalt_any(state, player, revalt)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
    )


def can_break_wall_cancerous_rodent(state: CollectionState, player: int, fire2: bool, arm: bool, revalt: bool, shoalt: bool) -> bool:
    return (
        rai0(state, player)
        or rai2(state, player)
        or rock_any(state, player)
        or arm1(state, player)
        or revalt_any(state, player, revalt)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
    )


def can_break_glass_or_walls(state: CollectionState, player: int, fire2: bool, arm: bool, revalt: bool, shoalt: bool, naialt: bool) -> bool:
    return (
        rai0(state, player)
        or rai2(state, player)
        or rock_any(state, player)
        or arm1(state, player)
        or naistd1_fire2(state, player, naialt, fire2)
        or revstd0_fire2(state, player, revalt, fire2)
        or revstd1_fire2(state, player, revalt, fire2)
        or revstd2_fire2(state, player, revalt, fire2)
        or revalt_any(state, player, revalt)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
    )


def jump_general(state: CollectionState, player: int, slam: bool, fire2: bool, arm: bool, has: int, shoalt: bool, needs: int) -> bool:
    return (
        can_slam(state, player, slam)
        or walljumps(state, player, has, needs)
        or rock0(state, player)
        or rock1(state, player)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
        or rai2(state, player)
    )


def secret_0_2(state: CollectionState, player: int, slam: bool, fire2: bool, arm: bool, has: int, shoalt: bool) -> bool:
    return (
        walljumps(state, player, has, 3)
        or slam_storage(state, player, slam, has)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
        or rai2(state, player)
        or rock_any(state, player)
    )


def challenge_0_3(state: CollectionState, player: int, slam: bool, fire2: bool, arm: bool, has: int, revalt: bool, shoalt: bool) -> bool:
    return (
        (
            slam_storage(state, player, slam, has)
            and can_break_glass(state, player, fire2, arm, revalt, shoalt)
        )
        or (
            shoalt1_fire2(state, player, shoalt, fire2)
            and (
                rai0(state, player)
                or rai2(state, player)
                or rock_any(state, player)
                or arm1(state, player)
                or revstd0_fire2(state, player, revalt, fire2)
                or revstd1_fire2(state, player, revalt, fire2)
                or revstd2_fire2(state, player, revalt, fire2)
                or revalt_any(state, player, revalt)
                or shostd0_fire2(state, player, fire2)
                or shostd1_fire2(state, player, fire2)
                or can_proj_boost(state, player, arm, shoalt)
            )
        )
        or (
            (
                shostd0_fire2(state, player, shoalt, fire2)
                or can_proj_boost(state, player, arm, shoalt)
            )
            and walljumps(state, player, has, 2)
        )
        or (
            shostd1_fire2(state, player, shoalt, fire2)
            and can_proj_boost(state, player, arm, shoalt)
        )
        or shoalt0_fire2(state, player, shoalt, fire2)
        or rock0_fire2(state, player, fire2)
        or rai2(state, player)
    )


def level_0_5(state: CollectionState, player: int, slide: bool, fire2: bool, has_walljumps: int, has_dashes: int, arm: bool, shoalt: bool) -> bool:
    return (
        (
            can_slide(state, player, slide)
            and (
                walljumps(state, player, has_walljumps, 1)
                or dashes(state, player, has_dashes, 1)
            )
        )
        or rock_any(state, player)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
        or rai2(state, player)
    )


def jump_1_1(state: CollectionState, player: int, slam: bool, fire2: bool, arm: bool, shoalt: bool) -> bool:
    return (
        can_slam(state, player, slam)
        or rock_any(state, player)
        or shoany0_fire2(state, player, fire2)
        or shoany1_fire2(state, player, fire2)
        or can_proj_boost(state, player, arm, shoalt)
        or rai2(state, player)
    )


def secret1_2_1(state: CollectionState, player: int, fire2: bool, arm: bool, has_dashes: int, has_walljumps: int, slide: bool, shoalt: bool) -> bool:
    return (
        dashes(state, player, has_dashes, 1)
        or shoany0_fire2(state, player, fire2)
        or shoany1_fire2(state, player, fire2)
        or can_proj_boost(state, player, arm, shoalt)
        or rai2(state, player)
        or rock_any(state, player)
        or can_slide(state, player, slide)
        or walljumps(state, player, has_walljumps, 3)
    )


def secret3_2_1(state: CollectionState, player: int, fire2: bool, has_walljumps: int, has_dashes: int, shoalt: bool) -> bool:
    return (
        (
            (
                dashes(state, player, has_dashes, 2)
                or (
                    walljumps(state, player, has_walljumps, 1)
                    and dashes(state, player, has_dashes, 1)
                )
            )
            and (
                shoalt0_fire2(state, player, shoalt, fire2)
                or shoany1_fire2(state, player, fire2)
                or rai2(state, player)
            )
        )
        or rock0_fire2(state, player, fire2)
    )


def bridge_and_tower_2_1(state: CollectionState, player: int, slam: bool, fire2: bool, arm: bool, has_walljumps: int, has_dashes: int, shoalt: bool) -> bool:
    return (
        can_slam(state, player, slam)
        or rai2(state, player)
        or rock_any(state, player)
        or (
            walljumps(state, player, has_walljumps, 1)
            and dashes(state, player, has_dashes, 1)
        )
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
    )


def challenge_2_1(state:CollectionState, player: int, slam: bool, fire2: bool, arm: bool, has_walljumps: int, has_dashes: int, slide: bool, revalt: bool, shoalt: bool, naialt: bool):
    # reach secret #1 without dying
    if not (
        (
            rai0(state, player)
            or rai2(state, player)
            or rock_any(state, player)
            or arm1(state, player)
            or naistd1_fire2(state, player, naialt, fire2)
            or revalt_any(state, player, revalt)
            or shostd0_fire2(state, player, shoalt, fire2)
            or can_proj_boost(state, player, arm, shoalt)
        ) and (
            dashes(state, player, has_dashes, 1)
            or shostd0_fire2(state, player, shoalt, fire2)
            or can_proj_boost(state, player, arm, shoalt)
            or rai2(state, player)
            or rock_any(state, player)
            or can_slide(state, player, slide)
            or walljumps(state, player, has_walljumps, 3)
        )
        # shostd1_fire2 without dash can be used to do either but not both
        or shostd1_fire2(state, player, shoalt, fire2) and (
            rai0(state, player)
            or arm1(state, player)
            or naistd1_fire2(state, player, fire2)
            or revalt_any(state, player, revalt)
            or dashes(state, player, has_dashes, 1)
            or can_slide(state, player, slide)
            or walljumps(state, player, has_walljumps, 3)
        )
    ):
        return False

    # reach end of level
    return (
        rock0_fire2(state, player, fire2)
        or slam_storage(state, player, slam, has_walljumps)
        or (
            walljumps(state, player, has_walljumps, 3)
            and dashes(state, player, has_dashes, 2)
            and (
                shoany1_fire2(state, player, fire2)
                or rai2(state, player)
            )
        )
    )


def challenge_2_2(state: CollectionState, player: int, has_dashes: int, slide: bool, fire2: bool, arm: bool, shoalt: bool, naialt: bool) -> bool:
    # pass corridor
    if not (
        rev_any(state, player)
        or shostd_any(state, player, shoalt)
        or nai_any(state, player)
        or rai0(state, player)
        or can_slide(state, player, slide)
        or dashes(state, player, has_dashes, 1)
        or can_punch(state, player, arm)
    ):
        return False

    return (
        (
            (
                rev_any(state, player)
                or naiany0(state, player)
                or naistd1_fire2(state, player, naialt, fire2)
                or naistd2(state, player, naialt)
                or can_proj_boost(state, player, arm, shoalt)
                or arm2(state, player)
                or rock_any(state, player)
            ) and (
                can_slide(state, player, slide)
                or dashes(state, player, has_dashes, 1)
            )
        ) or (
            shostd_any(state, player, shoalt)
            and can_slide(state, player, slide)
            and dashes(state, player, has_dashes, 1)
        )
        or rock0_fire2(state, player, fire2)
    )


def secret3_2_3(state: CollectionState, player: int, slam: bool, fire2: bool, has_walljumps: int, arm: bool, shoalt: bool) -> bool:
    return (
        can_slam(state, player, slam)
        or rai2(state, player)
        or walljumps(state, player, has_walljumps, 1)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or rock_any(state, player)
        or can_proj_boost(state, player, arm, shoalt)
    )


def jump_3_2(state: CollectionState, player: int, slam: bool, fire2: bool, arm: bool, has_walljumps: int, has_dashes: int, shoalt: bool) -> bool:
    return (
        can_slam(state, player, slam)
        or rai2(state, player)
        or rock_any(state, player)
        or walljumps(state, player, has_walljumps, 1)
        or dashes(state, player, has_dashes, 1)
        or shostd0_fire2(state, player, shoalt, fire2)
        or shostd1_fire2(state, player, shoalt, fire2)
        or shoalt_any(state, player, shoalt)
        or can_proj_boost(state, player, arm, shoalt)
    )


def challenge_4_1(state: CollectionState, player: int, slam: bool, fire2: bool, has_walljumps: int, has_dashes: int, shoalt: bool) -> bool:
    return (
        rock0_fire2(state, player, fire2)
        or (
            walljumps(state, player, has_walljumps, 2)
            and dashes(state, player, has_dashes, 2)
            and can_slam(state, player, slam)
        )
        or (
            (
                rai2(state, player)
                or shoany1_fire2(state, player, fire2)
                or shoalt0_fire2(state, player, shoalt, fire2)
            )
            and (
                walljumps(state, player, has_walljumps, 1)
                or can_slam(state, player, slam)
            )
        )
    )


def level_4_3(state: CollectionState, player: int, fire2: bool, arm: bool, shoalt: bool) -> bool:
    return (
        has_arm(state, player, arm)
        or arm1(state, player)
        or rai2(state, player)
        or shoany0_fire2(state, player, fire2)
        or can_proj_boost(state, player, arm, shoalt)
    )


def level_4_4(state: CollectionState, player: int, slam: bool, fire2: bool, skulls: bool, has_walljumps: int, has_dashes: int) -> bool:
    if skulls:
        return (
            (
                arm2(state, player)
                and state.has("Blue Skull (4-4)", player)
            )
            or (
                (
                    dashes(state, player, has_dashes, 1)
                    and walljumps(state, player, has_walljumps, 1)
                )
                or walljumps(state, player, has_walljumps, 2)
            )
            or can_slam(state, player, slam)
            or rock0_fire2(state, player, fire2)
        )
    else:
        return (
            arm2(state, player)
            or (
                (
                    dashes(state, player, has_dashes, 1)
                    and walljumps(state, player, has_walljumps, 1)
                )
                or walljumps(state, player, has_walljumps, 2)
            )
            or can_slam(state, player, slam)
            or rock0_fire2(state, player, fire2)
    )


def level_5_1(state: CollectionState, player: int, slam: bool, fire2: bool, has_walljumps: int, has_dashes: int) -> bool:
    return (
        (
            can_slam(state, player, slam)
            and walljumps(state, player, has_walljumps, 3)
            and dashes(state, player, has_dashes, 2)
        )
        or rock0_fire2(state, player, fire2)
        or arm2(state, player)
    )


def rules(ultrakillworld):
    multiworld = ultrakillworld.multiworld
    options = ultrakillworld.options
    player = ultrakillworld.player

    fire2 = options.randomize_secondary_fire
    arm = options.start_with_arm
    slam = options.start_with_slam
    slide = options.start_with_slide
    revalt = options.revolver_form
    shoalt = options.shotgun_form
    naialt = options.nailgun_form
    skulls = options.randomize_skulls
    l1switch = options.randomize_limbo_switches
    l7switch = options.randomize_violence_switches
    boss = options.boss_rewards
    challenge = options.challenge_rewards
    prank = options.p_rank_rewards
    hank = options.hank_rewards
    rocket_race = options.rocket_race_reward
    clean = options.cleaning_rewards
    secretcompletion = options.include_secret_mission_completion
    goal = options.goal

    dash: int = options.starting_stamina.value
    walljump: int = options.starting_walljumps.value

    # goal
    set_rule(multiworld.get_entrance("Menu -> " + region_table[ultrakillworld.goal_name], player), \
        lambda state: state.has("Level Completed", player, options.goal_requirement.value))


    # level entrances
    set_rule(multiworld.get_entrance("Menu -> Shop", player),
        lambda state: (
            state.has_group("levels", player)
            or state.has_group("layers", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 0-2: THE MEATGRINDER", player),
        lambda state: (
            state.has("0-2: THE MEATGRINDER", player)
            or state.has("OVERTURE: THE MOUTH OF HELL", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 0-3: DOUBLE DOWN", player),
        lambda state: (
            state.has("0-3: DOUBLE DOWN", player)
            or state.has("OVERTURE: THE MOUTH OF HELL", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 0-4: A ONE-MACHINE ARMY", player),
        lambda state: (
            state.has("0-4: A ONE-MACHINE ARMY", player)
            or state.has("OVERTURE: THE MOUTH OF HELL", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 0-5: CERBERUS", player),
        lambda state: (
            state.has("0-5: CERBERUS", player)
            or state.has("OVERTURE: THE MOUTH OF HELL", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 1-1: HEART OF THE SUNRISE", player),
        lambda state: (
            state.has("1-1: HEART OF THE SUNRISE", player)
            or state.has("LAYER 1: LIMBO", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 1-2: THE BURNING WORLD", player),
        lambda state: (
            state.has("1-2: THE BURNING WORLD", player)
            or state.has("LAYER 1: LIMBO", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 1-3: HALLS OF SACRED REMAINS", player),
        lambda state: (
            state.has("1-3: HALLS OF SACRED REMAINS", player)
            or state.has("LAYER 1: LIMBO", player)
        ))
    if goal.value != 0:
        set_rule(multiworld.get_entrance("Menu -> 1-4: CLAIR DE LUNE", player),
            lambda state: (
                state.has("1-4: CLAIR DE LUNE", player)
                or state.has("LAYER 1: LIMBO", player)
            ))
    set_rule(multiworld.get_entrance("Menu -> 2-1: BRIDGEBURNER", player),
        lambda state: (
            state.has("2-1: BRIDGEBURNER", player)
            or state.has("LAYER 2: LUST", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 2-2: DEATH AT 20,000 VOLTS", player),
        lambda state: (
            state.has("2-2: DEATH AT 20,000 VOLTS", player)
            or state.has("LAYER 2: LUST", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 2-3: SHEER HEART ATTACK", player),
        lambda state: (
            state.has("2-3: SHEER HEART ATTACK", player)
            or state.has("LAYER 2: LUST", player)
        ))
    if goal.value != 1:
        set_rule(multiworld.get_entrance("Menu -> 2-4: COURT OF THE CORPSE KING", player),
            lambda state: (
                state.has("2-4: COURT OF THE CORPSE KING", player)
                or state.has("LAYER 2: LUST", player)
            ))
    set_rule(multiworld.get_entrance("Menu -> 3-1: BELLY OF THE BEAST", player),
        lambda state: (
            state.has("3-1: BELLY OF THE BEAST", player)
            or state.has("LAYER 3: GLUTTONY", player)
        ))
    if goal.value != 2:
        set_rule(multiworld.get_entrance("Menu -> 3-2: IN THE FLESH", player),
            lambda state: (
                state.has("3-2: IN THE FLESH", player)
                or state.has("LAYER 3: GLUTTONY", player)
            ))
    set_rule(multiworld.get_entrance("Menu -> 4-1: SLAVES TO POWER", player),
        lambda state: (
            state.has("4-1: SLAVES TO POWER", player)
            or state.has("LAYER 4: GREED", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 4-2: GOD DAMN THE SUN", player),
        lambda state: (
            state.has("4-2: GOD DAMN THE SUN", player)
            or state.has("LAYER 4: GREED", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 4-3: A SHOT IN THE DARK", player),
        lambda state: (
            state.has("4-3: A SHOT IN THE DARK", player)
            or state.has("LAYER 4: GREED", player)
        ))
    if goal.value != 3:
        set_rule(multiworld.get_entrance("Menu -> 4-4: CLAIR DE SOLEIL", player),
            lambda state: (
                state.has("4-4: CLAIR DE SOLEIL", player)
                or state.has("LAYER 4: GREED", player)
            ))
    set_rule(multiworld.get_entrance("Menu -> 5-1: IN THE WAKE OF POSEIDON", player),
        lambda state: (
            state.has("5-1: IN THE WAKE OF POSEIDON", player)
            or state.has("LAYER 5: WRATH", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 5-2: WAVES OF THE STARLESS SEA", player),
        lambda state: (
            state.has("5-2: WAVES OF THE STARLESS SEA", player)
            or state.has("LAYER 5: WRATH", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 5-3: SHIP OF FOOLS", player),
        lambda state: (
            state.has("5-3: SHIP OF FOOLS", player)
            or state.has("LAYER 5: WRATH", player)
        ))
    if goal.value != 4:
        set_rule(multiworld.get_entrance("Menu -> 5-4: LEVIATHAN", player),
            lambda state: (
                state.has("5-4: LEVIATHAN", player)
                or state.has("LAYER 5: WRATH", player)
            ))
    set_rule(multiworld.get_entrance("Menu -> 6-1: CRY FOR THE WEEPER", player),
        lambda state: (
            state.has("6-1: CRY FOR THE WEEPER", player)
            or state.has("LAYER 6: HERESY", player)
        ))
    if goal.value != 5:
        set_rule(multiworld.get_entrance("Menu -> 6-2: AESTHETICS OF HATE", player),
            lambda state: (
                state.has("6-2: AESTHETICS OF HATE", player)
                or state.has("LAYER 6: HERESY", player)
            ))
    set_rule(multiworld.get_entrance("Menu -> 7-1: GARDEN OF FORKING PATHS", player),
        lambda state: (
            state.has("7-1: GARDEN OF FORKING PATHS", player)
            or state.has("LAYER 7: VIOLENCE", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 7-2: LIGHT UP THE NIGHT", player),
        lambda state: (
            state.has("7-2: LIGHT UP THE NIGHT", player)
            or state.has("LAYER 7: VIOLENCE", player)
        ))
    set_rule(multiworld.get_entrance("Menu -> 7-3: NO SOUND, NO MEMORY", player),
        lambda state: (
            state.has("7-3: NO SOUND, NO MEMORY", player)
            or state.has("LAYER 7: VIOLENCE", player)
        ))
    if goal.value != 8:
        set_rule(multiworld.get_entrance("Menu -> 7-4: ...LIKE ANTENNAS TO HEAVEN", player),
            lambda state: (
                state.has("7-4: ...LIKE ANTENNAS TO HEAVEN", player)
                or state.has("LAYER 7: VIOLENCE", player)
            ))

    # secret mission entrances
    set_rule(multiworld.get_entrance("0-2: THE MEATGRINDER -> 0-S: SOMETHING WICKED", player),
        lambda state: (
            secret_0_2(state, player, slam, fire2, arm, walljump, shoalt)
            and grab_item(state, player, arm)
        ))
    if skulls:
        add_rule(multiworld.get_entrance("0-2: THE MEATGRINDER -> 0-S: SOMETHING WICKED", player),
            lambda state: state.has("Blue Skull (0-2)", player))
    set_rule(multiworld.get_entrance("1-1: HEART OF THE SUNRISE -> 1-S: THE WITLESS", player),
        lambda state: revany2_fire2(state, player, fire2))
    set_rule(multiworld.get_entrance("2-3: SHEER HEART ATTACK -> 2-S: ALL-IMPERFECT LOVE SONG", player),
        lambda state: (
            secret3_2_3(state, player, slam, fire2, walljump, arm, shoalt)
            and can_slide(state, player, slide)
        ))
    if skulls:
        add_rule(multiworld.get_entrance("2-3: SHEER HEART ATTACK -> 2-S: ALL-IMPERFECT LOVE SONG", player),
            lambda state: state.has("Blue Skull (2-3)", player))
    set_rule(multiworld.get_entrance("4-2: GOD DAMN THE SUN -> 4-S: CLASH OF THE BRANDICOOT", player),
        lambda state: (
            (
                jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
                or revany1_fire2(state, player, fire2)
            )
            and grab_item(state, player, arm)
        ))
    set_rule(multiworld.get_entrance("5-1: IN THE WAKE OF POSEIDON -> 5-S: I ONLY SAY MORNING", player),
        lambda state: (
            can_slide(state, player, slide)
            and level_5_1(state, player, slam, fire2, walljump, dash)
            and grab_item(state, player, arm)
        ))
    if skulls:
        add_rule(multiworld.get_entrance("5-1: IN THE WAKE OF POSEIDON -> 5-S: I ONLY SAY MORNING", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
    set_rule(multiworld.get_entrance("7-3: NO SOUND, NO MEMORY -> 7-S: HELL BATH NO FURY", player),
        lambda state: (
            good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            and can_break_idol(state, player, arm, shoalt)
            and (
                arm2(state, player)
                or walljumps(state, player, walljump, 3)
                or (
                    walljumps(state, player, walljump, 2)
                    and dashes(state, player, dash, 1)
                )
                or (
                    shoalt0_fire2(state, player, shoalt, fire2)
                    or shoany1_fire2(state, player, fire2)
                    or rai2(state, player)
                )
            )
        ))



    # 0-1
    set_rule(multiworld.get_location("0-1: Secret #1", player),
        lambda state: can_break_glass_or_walls(state, player, fire2, arm, revalt, shoalt, naialt))

    set_rule(multiworld.get_location("0-1: Secret #3", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))
    set_rule(multiworld.get_location("0-1: Secret #4", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))

    if challenge:
        set_rule(multiworld.get_location("0-1: Get 5 kills with a single glass panel", player),
            lambda state: can_break_glass(state, player, fire2, arm, revalt, shoalt))

    if prank:
        set_rule(multiworld.get_location("0-1: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt) or \
                state.has("Knuckleblaster", player))


    # 0-2
    set_rule(multiworld.get_location("0-2: Secret #3", player),
        lambda state: (
            rock_any(state, player)
            or rai2(state, player)
            or shoany0_fire2(state, player, fire2)
            or can_proj_boost(state, player, arm, shoalt)
            or (
                walljumps(state, player, walljump, 1)
                and dashes(state, player, dash, 1)
            )
            or walljumps(state, player, walljump, 2)
            or (
                slam_storage(state, player, slam, walljump)
                and can_slide(state, player, slide)
            )
        ))


    set_rule(multiworld.get_location("0-2: Secret #4", player),
        lambda state: can_slide(state, player, slide))
    if challenge:
        set_rule(multiworld.get_location("0-2: Beat the secret encounter", player),
            lambda state: (
                can_slide(state, player, slide)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if prank:
        add_rule(multiworld.get_location("0-2: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))


    # 0-S
    if skulls and secretcompletion:
        add_rule(multiworld.get_location("Cleared 0-S", player),
            lambda state: state.has_all({"Blue Skull (0-2)", "Blue Skull (0-S)", "Red Skull (0-S)"}, player))


    # 0-3
    set_rule(multiworld.get_location("0-3: Secret #1", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))
    set_rule(multiworld.get_location("0-3: Secret #2", player),
        lambda state: (
            can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
            or challenge_0_3(state, player, slam, fire2, arm, walljump, revalt, shoalt)
        ))

    set_rule(multiworld.get_location("0-3: Secret #3", player),
        lambda state: can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt))
    set_rule(multiworld.get_location("Cleared 0-3", player),
        lambda state: (
            (
                can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
                or challenge_0_3(state, player, slam, fire2, arm, walljump, revalt, shoalt)
            )
            and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
        ))

    set_rule(multiworld.get_location("0-3: Weapon", player),
        lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))

    if challenge:
        set_rule(multiworld.get_location("0-3: Kill only 1 enemy", player),
            lambda state: (
                challenge_0_3(state, player, slam, fire2, arm, walljump, revalt, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if prank:
        add_rule(multiworld.get_location("0-3: Perfect Rank", player),
            lambda state: (
                can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    # 0-4
    set_rule(multiworld.get_location("0-4: Secret #1", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))

    set_rule(multiworld.get_location("0-4: Secret #2", player),
        lambda state: can_break_glass(state, player, fire2, arm, revalt, shoalt))

    set_rule(multiworld.get_location("0-4: Secret #3", player),
        lambda state: can_slide(state, player, slide))
    if challenge:
        set_rule(multiworld.get_location("0-4: Slide uninterrupted for 17 seconds", player),
            lambda state: can_slide(state, player, slide))

    if prank:
        set_rule(multiworld.get_location("0-4: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))


    # 0-5
    set_rule(multiworld.get_location("Cleared 0-5", player),
        lambda state: level_0_5(state, player, slide, fire2, walljump, dash, arm, shoalt))
    
    if boss > 0:
        set_rule(multiworld.get_location("0-5: Defeat the Cerberi", player),
            lambda state: level_0_5(state, player, slide, fire2, walljump, dash, arm, shoalt))

    if challenge:
        set_rule(multiworld.get_location("0-5: Don't inflict fatal damage to any enemy", player),
            lambda state: (
                level_0_5(state, player, slide, fire2, walljump, dash, arm, shoalt)
                and dashes(state, player, dash, 1)
            ))

    if prank:
        set_rule(multiworld.get_location("0-5: Perfect Rank", player),
            lambda state: (
                level_0_5(state, player, slide, fire2, walljump, dash, arm, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))


    # 1-1
    set_rule(multiworld.get_location("1-1: Secret #5", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))

    set_rule(multiworld.get_location("1-1: Weapon", player),
        lambda state: grab_item(state, player, arm))
    set_rule(multiworld.get_location("1-1: Secret #3", player),
        lambda state: grab_item(state, player, arm))
    set_rule(multiworld.get_location("1-1: Secret #4", player),
        lambda state: grab_item(state, player, arm))
    add_rule(multiworld.get_location("1-1: Secret #5", player),
        lambda state: grab_item(state, player, arm))
    if l1switch:
        set_rule(multiworld.get_location("1-1: Switch", player),
            lambda state: (
                jump_1_1(state, player, slam, fire2, arm, shoalt)
                and grab_item(state, player, arm)
            ))
    if prank:
        set_rule(multiworld.get_location("1-1: Perfect Rank", player),
            lambda state: (
                good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                and grab_item(state, player, arm)
            ))

    if skulls:
        set_rule(multiworld.get_location("Cleared 1-1", player),
            lambda state: (
                (
                    state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player)
                    and grab_item(state, player, arm)
                )
                or revany2_fire2(state, player, fire2)
            ))

        add_rule(multiworld.get_location("1-1: Weapon", player),
            lambda state: state.has("Red Skull (1-1)", player))
        add_rule(multiworld.get_location("1-1: Secret #3", player),
            lambda state: state.has("Red Skull (1-1)", player))
        add_rule(multiworld.get_location("1-1: Secret #4", player),
            lambda state: state.has("Red Skull (1-1)", player))
        add_rule(multiworld.get_location("1-1: Secret #5", player),
            lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
        if l1switch:
            add_rule(multiworld.get_location("1-1: Switch", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
        if prank:
            add_rule(multiworld.get_location("1-1: Perfect Rank", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
    else:
        set_rule(multiworld.get_location("Cleared 1-1", player),
            lambda state: (
                grab_item(state, player, arm)
                or revany2_fire2(state, player, fire2)
            ))

    if challenge:
        set_rule(multiworld.get_location("1-1: Complete the level in under 10 seconds", player),
            lambda state: revany2_fire2(state, player, fire2))


    # 1-2
    if challenge:
        set_rule(multiworld.get_location("1-2: Do not pick up any skulls", player),
            lambda state: can_zap(state, player, fire2))

    set_rule(multiworld.get_location("1-2: Secret #3", player),
        lambda state: grab_item(state, player, arm))

    if skulls:
        add_rule(multiworld.get_location("1-2: Secret #3", player),
            lambda state: state.has("Blue Skull (1-2)", player))
        set_rule(multiworld.get_location("1-2: Secret #4", player),
            lambda state: (
                state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player)
                and grab_item(state, player, arm)
                or can_zap(state, player, fire2)
            ))
        set_rule(multiworld.get_location("1-2: Secret #5", player),
            lambda state: (
                state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player)
                and grab_item(state, player, arm)
                or can_zap(state, player, fire2)
            ))
        set_rule(multiworld.get_location("Cleared 1-2", player),
            lambda state: (
                state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player)
                and grab_item(state, player, arm)
                or can_zap(state, player, fire2)
            ))
        if l1switch:
            set_rule(multiworld.get_location("1-2: Switch", player),
                lambda state: (
                    (
                        state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player)
                        and grab_item(state, player, arm)
                        or can_zap(state, player, fire2)
                    )
                    and can_break_wall_cancerous_rodent(state, player, fire2, arm, revalt, shoalt)
                ))
        if boss == 2:
            set_rule(multiworld.get_location("1-2: Defeat the Very Cancerous Rodent", player),
                lambda state: (
                    (
                        state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player)
                        and grab_item(state, player, arm)
                        or can_zap(state, player, fire2)
                    )
                    and can_break_wall_cancerous_rodent(state, player, fire2, arm, revalt, shoalt)
                ))
        if prank:
            set_rule(multiworld.get_location("1-2: Perfect Rank", player),
                lambda state: (
                    state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player)
                    and grab_item(state, player, arm)
                ))
    else:
        set_rule(multiworld.get_location("1-2: Secret #4", player),
            lambda state: (
                grab_item(state, player, arm)
                or can_zap(state, player, fire2)
            ))
        set_rule(multiworld.get_location("1-2: Secret #5", player),
            lambda state: (
                grab_item(state, player, arm)
                or can_zap(state, player, fire2)
            ))
        set_rule(multiworld.get_location("Cleared 1-2", player),
            lambda state: (
                grab_item(state, player, arm)
                or can_zap(state, player, fire2)
            ))
        if l1switch:
            set_rule(multiworld.get_location("1-2: Switch", player),
                lambda state: (
                    (
                        grab_item(state, player, arm)
                        or can_zap(state, player, fire2)
                    )
                    and can_break_wall_cancerous_rodent(state, player, fire2, arm, revalt, shoalt)
                ))
        if boss == 2:
            set_rule(multiworld.get_location("1-2: Defeat the Very Cancerous Rodent", player),
                lambda state: (
                    (
                        grab_item(state, player, arm)
                        or can_zap(state, player, fire2)
                    )
                    and can_break_wall_cancerous_rodent(state, player, fire2, arm, revalt, shoalt)
                ))
        if prank:
            set_rule(multiworld.get_location("1-2: Perfect Rank", player),
                lambda state: (
                    grab_item(state, player, arm)
                    or can_zap(state, player, fire2)
                ))

    if prank:
        add_rule(multiworld.get_location("1-2: Perfect Rank", player),
            lambda state: (
                good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                and grab_item(state, player, arm)
            ))


    # 1-3        
    set_rule(multiworld.get_location("1-3: Secret #1", player),
        lambda state: can_break_glass(state, player, fire2, arm, revalt, shoalt))

    set_rule(multiworld.get_location("1-3: Secret #4", player),
        lambda state: can_slide(state, player, slide))
    set_rule(multiworld.get_location("1-3: Secret #5", player),
        lambda state: can_slide(state, player, slide))
    
    if l1switch:
        set_rule(multiworld.get_location("1-3: Switch", player),
            lambda state: (
                grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if skulls:
        set_rule(multiworld.get_location("Cleared 1-3", player),
            lambda state: state.has_any({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))
        if l1switch:
            set_rule(multiworld.get_location("1-3: Switch", player),
                lambda state: state.has_all({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))
        if prank:
            set_rule(multiworld.get_location("1-3: Perfect Rank", player),
                lambda state: state.has_any({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))
        if challenge:
            set_rule(multiworld.get_location("1-3: Beat the secret encounter", player),
                lambda state: state.has_all({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))

    add_rule(multiworld.get_location("Cleared 1-3", player),
        lambda state: grab_item(state, player, arm))
    if prank:
        add_rule(multiworld.get_location("1-3: Perfect Rank", player),
            lambda state: (
                grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))
    if challenge:
        add_rule(multiworld.get_location("1-3: Beat the secret encounter", player),
            lambda state: (
                grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))


    # 1-4
    if not l1switch:
        set_rule(multiworld.get_location("1-4: Secret Weapon", player),
            lambda state: state.can_reach(multiworld.get_region("1-1: HEART OF THE SUNRISE", player)) and \
                state.can_reach(multiworld.get_region("1-2: THE BURNING WORLD", player)) and \
                    state.can_reach(multiworld.get_region("1-3: HALLS OF SACRED REMAINS", player)))

        add_rule(multiworld.get_location("1-4: Secret Weapon", player),
            lambda state: (
                jump_1_1(state, player, slam, fire2, arm, shoalt)
                and can_break_glass(state, player, fire2, arm, revalt, shoalt)
                and can_break_wall_cancerous_rodent(state, player, fire2, arm, revalt, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                and grab_item(state, player, arm)
            ))
    else:
        set_rule(multiworld.get_location("1-4: Secret Weapon", player),
            lambda state: state.has_all({"Limbo Switch I", "Limbo Switch II", "Limbo Switch III", "Limbo Switch IV"}, player))

    if skulls:
        add_rule(multiworld.get_location("1-4: Secret Weapon", player),
            lambda state: (
                state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)", "Blue Skull (1-3)", "Red Skull (1-3)"}, player)
                and (
                    state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) 
                    or can_zap(state, player, fire2)
                )
            ))
        if hank:
            set_rule(multiworld.get_location("1-4: Assemble Hank", player),
                lambda state: (
                    state.has("Blue Skull (1-4)", player)
                    and grab_item(state, player, arm)
                ))

    if boss > 0:
        set_rule(multiworld.get_location("1-4: Defeat V2", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))

    if challenge and goal != 0:
        add_rule(multiworld.get_location("1-4: Do not pick up any skulls", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))

    if prank and goal != 0:
        add_rule(multiworld.get_location("1-4: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))


    # 2-1
    set_rule(multiworld.get_location("2-1: Secret #1", player),
        lambda state: (
            secret1_2_1(state, player, fire2, arm, dash, walljump, slide, shoalt)
            and can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
            or slam_storage(state, player, slam, walljump)
        ))

    set_rule(multiworld.get_location("2-1: Secret #3", player),
        lambda state: secret3_2_1(state, player, fire2, walljump, dash, shoalt))

    set_rule(multiworld.get_location("2-1: Secret #5", player),
        lambda state: bridge_and_tower_2_1(state, player, slam, fire2, arm, walljump, dash, shoalt))
    set_rule(multiworld.get_location("Cleared 2-1", player),
        lambda state: bridge_and_tower_2_1(state, player, slam, fire2, arm, walljump, dash, shoalt))

    if challenge:
        set_rule(multiworld.get_location("2-1: Don't open any normal doors", player),
            lambda state: challenge_2_1(state, player, slam, fire2, arm, walljump, dash, slide, revalt, shoalt, naialt))

    if prank:
        set_rule(multiworld.get_location("2-1: Perfect Rank", player),
            lambda state: (
                bridge_and_tower_2_1(state, player, slam, fire2, arm, walljump, dash, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))            

    # 2-2
    set_rule(multiworld.get_location("2-2: Secret #1", player),
        lambda state: (
            rev_any(state, player)
            or sho_any(state, player)
            or nai_any(state, player)
            or rai0(state, player)
            or rai2(state, player)
            or rock_any(state, player)
            or grab_item(state, player, arm)
            or can_slam(state, player, slam)
        ))

    set_rule(multiworld.get_location("2-2: Secret #2", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))
    set_rule(multiworld.get_location("2-2: Secret #3", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2))
    set_rule(multiworld.get_location("2-2: Secret #4", player),
        lambda state: can_slide(state, player, slide))
    set_rule(multiworld.get_location("2-2: Secret #5", player),
        lambda state: can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt))

    if challenge:
        set_rule(multiworld.get_location("2-2: Beat the level in under 60 seconds", player),
            lambda state: challenge_2_2(state, player, dash, slide, fire2, arm, shoalt, naialt))

    if prank:
        add_rule(multiworld.get_location("2-2: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))


    # 2-3
    set_rule(multiworld.get_location("2-3: Secret #2", player),
        lambda state: can_slide(state, player, slide))
    set_rule(multiworld.get_location("2-3: Secret #3", player),
        lambda state: (
            grab_item(state, player, arm)
            and secret3_2_3(state, player, slam, fire2, walljump, arm, shoalt)
        ))
    set_rule(multiworld.get_location("2-3: Secret #4", player),
        lambda state: grab_item(state, player, arm))
    set_rule(multiworld.get_location("2-3: Secret #5", player),
        lambda state: can_slide(state, player, slide))
    set_rule(multiworld.get_location("Cleared 2-3", player),
        lambda state: grab_item(state, player, arm))

    if skulls:
        add_rule(multiworld.get_location("2-3: Secret #3", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        add_rule(multiworld.get_location("2-3: Secret #4", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        add_rule(multiworld.get_location("Cleared 2-3", player),
            lambda state: (
                (
                    secret3_2_3(state, player, slam, fire2, walljump, arm, shoalt)
                    and state.has("Blue Skull (2-3)", player)
                    and can_slide(state, player, slide)
                )
                or state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player)
            ))
        if challenge:
            set_rule(multiworld.get_location("2-3: Don't touch any water", player),
                lambda state: state.has("Blue Skull (2-3)", player))
        if prank:
            set_rule(multiworld.get_location("2-3: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))

    if challenge:
        add_rule(multiworld.get_location("2-3: Don't touch any water", player),
            lambda state: (
                secret3_2_3(state, player, slam, fire2, walljump, arm, shoalt)
                and can_slide(state, player, slide)
                and grab_item(state, player, arm)
                and can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
            ))

    if prank:
        add_rule(multiworld.get_location("2-3: Perfect Rank", player),
            lambda state: (
                good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                and grab_item(state, player, arm)
            ))


    # 2-4
    if skulls:
        set_rule(multiworld.get_location("Cleared 2-4", player),
            lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        if boss > 0 and goal != 1:
            set_rule(multiworld.get_location("2-4: Defeat the Corpse of King Minos", player),
                lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        if challenge and goal != 1:
            add_rule(multiworld.get_location("2-4: Parry a punch", player),
                lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        if prank and goal != 1:
            add_rule(multiworld.get_location("2-4: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))

    add_rule(multiworld.get_location("Cleared 2-4", player),
        lambda state: grab_item(state, player, arm))
    if boss > 0 and goal != 1:
        add_rule(multiworld.get_location("2-4: Defeat the Corpse of King Minos", player),
            lambda state: (
                grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))
    if challenge and goal != 1:
        add_rule(multiworld.get_location("2-4: Parry a punch", player),
            lambda state: (
                grab_item(state, player, arm)
                and has_arm(state, player, arm)
            ))
    if prank and goal != 1:
        add_rule(multiworld.get_location("2-4: Perfect Rank", player),
            lambda state: (
                grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))


    # 3-1
    set_rule(multiworld.get_location("3-1: Secret #4", player),
        lambda state: can_slide(state, player, slide))

    if prank:
        add_rule(multiworld.get_location("3-1: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))


    # 3-2
    set_rule(multiworld.get_location("Cleared 3-2", player),
        lambda state: (
            can_slide(state, player, slide)
            and jump_3_2(state, player, slam, fire2, arm, walljump, dash, shoalt)
        ))
    
    if boss > 0 and goal != 2:
        set_rule(multiworld.get_location("3-2: Defeat Gabriel", player),
            lambda state: (
                can_slide(state, player, slide)
                and jump_3_2(state, player, slam, fire2, arm, walljump, dash, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if challenge and goal != 2:
        set_rule(multiworld.get_location("3-2: Drop Gabriel in a pit", player),
            lambda state: (
                can_slide(state, player, slide)
                and jump_3_2(state, player, slam, fire2, arm, walljump, dash, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if prank and goal != 2:
        set_rule(multiworld.get_location("3-2: Perfect Rank", player),
            lambda state: (
                can_slide(state, player, slide)
                and jump_3_2(state, player, slam, fire2, arm, walljump, dash, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))


    # 4-1
    set_rule(multiworld.get_location("4-1: Secret #1", player),
        lambda state: (
            dashes(state, player, dash, 1)
            or rock0_fire2(state, player, fire2)
        ))

    set_rule(multiworld.get_location("4-1: Secret #4", player),
        lambda state: (
            (
                walljumps(state, player, walljump, 2) and \
                state.has("Slam", player)
            )
            or rock0_fire2(state, player, fire2)
            or shoalt0_fire2(state, player, shoalt, fire2)
            or shoany1_fire2(state, player, fire2)
            or rai2(state, player)
        ))

    set_rule(multiworld.get_location("4-1: Secret #2", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))
    set_rule(multiworld.get_location("4-1: Secret #3", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1))
    set_rule(multiworld.get_location("4-1: Secret #5", player),
        lambda state: (
            (
                jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
                and can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
            )
            or (
                can_slam(state, player, slam)
                or shostd0_fire2(state, player, shoalt, fire2)
                or shostd1_fire2(state, player, shoalt, fire2)
                or shoalt_any(state, player, shoalt)
                or can_proj_boost(state, player, arm, shoalt)
                or rock_any(state, player)
                or rai2(state, player)
            )
        ))

    if challenge:
        set_rule(multiworld.get_location("4-1: Don't activate any enemies", player),
            lambda state: challenge_4_1(state, player, slam, fire2, walljump, dash, shoalt))

    if prank:
        add_rule(multiworld.get_location("4-1: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))


    # 4-2
    set_rule(multiworld.get_location("4-2: Secret #4", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
            or revany1_fire2(state, player, fire2)
        ))

    if skulls:
        set_rule(multiworld.get_location("Cleared 4-2", player),
            lambda state: (
                jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
                or revany1_fire2(state, player, fire2)
                or (
                    state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player)
                    and grab_item(state, player, arm)
                )
            ))
        if challenge:
            set_rule(multiworld.get_location("4-2: Kill the Insurrectionist in under 10 seconds", player),
                lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        if prank:
            set_rule(multiworld.get_location("4-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
    else:
        set_rule(multiworld.get_location("Cleared 4-2", player),
            lambda state: (
                jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
                or revany1_fire2(state, player, fire2)
                or grab_item(state, player, arm)
            ))

    if challenge:
        add_rule(multiworld.get_location("4-2: Kill the Insurrectionist in under 10 seconds", player),
            lambda state: grab_item(state, player, arm))

    if prank:
        add_rule(multiworld.get_location("4-2: Perfect Rank", player),
            lambda state: (
                grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))


    # 4-3
    if challenge:
        add_rule(multiworld.get_location("4-3: Don't pick up the torch", player),
        lambda state: (
            (
                shoany0_fire2(state, player, fire2)
                or can_proj_boost(state, player, arm, shoalt)
            )
            and grab_item(state, player, arm)
        ))

    add_rule(multiworld.get_location("4-3: Secret #1", player),
        lambda state: level_4_3(state, player, fire2, arm, shoalt))
    add_rule(multiworld.get_location("4-3: Secret #2", player),
        lambda state: (
            level_4_3(state, player, fire2, arm, shoalt)
            and can_slide(state, player, slide)
        ))
    add_rule(multiworld.get_location("4-3: Secret #3", player),
        lambda state: level_4_3(state, player, fire2, arm, shoalt))
    add_rule(multiworld.get_location("4-3: Secret #4", player),
        lambda state: (
            level_4_3(state, player, fire2, arm, shoalt)
            and can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
        ))
    add_rule(multiworld.get_location("4-3: Secret #5", player),
        lambda state: (
            level_4_3(state, player, fire2, arm, shoalt)
            and can_slide(state, player, slide)
        ))
    add_rule(multiworld.get_location("Cleared 4-3", player),
        lambda state: (
            level_4_3(state, player, fire2, arm, shoalt)
            and grab_item(state, player, arm)
        ))
    
    if boss == 2:
        set_rule(multiworld.get_location("4-3: Defeat the Mysterious Druid Knight (& Owl)", player),
            lambda state: (
                level_4_3(state, player, fire2, arm, shoalt)
                and can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
                and can_punch(state, player, arm)
            ))
    
    if prank:
        add_rule(multiworld.get_location("4-3: Perfect Rank", player),
            lambda state: (
                level_4_3(state, player, fire2, arm, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                and grab_item(state, player, arm)
            ))

    if skulls:
        if boss == 2:
            add_rule(multiworld.get_location("4-3: Defeat the Mysterious Druid Knight (& Owl)", player),
                lambda state: state.has("Blue Skull (4-3)", player))
        if challenge:
            add_rule(multiworld.get_location("4-3: Don't pick up the torch", player),
                lambda state: state.has("Blue Skull (4-3)", player))


    # 4-4
    set_rule(multiworld.get_location("Cleared 4-4", player),
        lambda state: (
            arm2(state, player)
            and level_4_4(state, player, slam, fire2, skulls, walljump, dash)
        ))
    
    set_rule(multiworld.get_location("4-4: V2's Other Arm", player),
        lambda state: (
            level_4_4(state, player, slam, fire2, skulls, walljump, dash)
            and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
        ))
    
    if boss > 0 and goal != 3:
        set_rule(multiworld.get_location("4-4: Defeat V2", player),
            lambda state: (
                level_4_4(state, player, slam, fire2, skulls, walljump, dash)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))
    
    if prank and goal != 3:
        set_rule(multiworld.get_location("4-4: Perfect Rank", player),
            lambda state: (
                arm2(state, player)
                and level_4_4(state, player, slam, fire2, skulls, walljump, dash)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if challenge and goal != 3:
        set_rule(multiworld.get_location("4-4: Reach the boss room in 18 seconds", player),
            lambda state: (
                arm2(state, player)
                and dashes(state, player, dash, 3)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    set_rule(multiworld.get_location("4-4: Secret Weapon", player),
        lambda state: (
            arm2(state, player)
            and can_zap(state, player, fire2)
        ))

    if skulls:
        add_rule(multiworld.get_location("4-4: Secret Weapon", player),
            lambda state: state.has("Blue Skull (4-4)", player))
        if challenge and goal != 3:
            add_rule(multiworld.get_location("4-4: Reach the boss room in 18 seconds", player),
                lambda state: state.has("Blue Skull (4-4)", player))


    # 5-1
    if challenge:
        add_rule(multiworld.get_location("5-1: Don't touch any water", player),
            lambda state: (
                arm2(state, player)
                or rock0_fire2(state, player, fire2)
            ))

    add_rule(multiworld.get_location("5-1: Secret #1", player),
        lambda state: (
            can_slide(state, player, slide)
            and level_5_1(state, player, slam, fire2, walljump, dash)
        ))
    add_rule(multiworld.get_location("5-1: Secret #2", player),
        lambda state: (
            can_slide(state, player, slide)
            and level_5_1(state, player, slam, fire2, walljump, dash)
        ))
    add_rule(multiworld.get_location("5-1: Secret #3", player),
        lambda state: (
            can_slide(state, player, slide)
            and level_5_1(state, player, slam, fire2, walljump, dash)
        ))
    add_rule(multiworld.get_location("5-1: Secret #4", player),
        lambda state: (
            can_slide(state, player, slide)
            and level_5_1(state, player, slam, fire2, walljump, dash)
        ))
    add_rule(multiworld.get_location("5-1: Secret #5", player),
        lambda state: (
            can_slide(state, player, slide)
            and level_5_1(state, player, slam, fire2, walljump, dash)
            and grab_item(state, player, arm)
        ))
    add_rule(multiworld.get_location("Cleared 5-1", player),
        lambda state: (
            can_slide(state, player, slide)
            and level_5_1(state, player, slam, fire2, walljump, dash)
            and grab_item(state, player, arm)
        ))
    if challenge:
        add_rule(multiworld.get_location("5-1: Don't touch any water", player),
            lambda state: (
                can_slide(state, player, slide)
                and level_5_1(state, player, slam, fire2, walljump, dash)
                and grab_item(state, player, arm)
            ))
    if prank:
        add_rule(multiworld.get_location("5-1: Perfect Rank", player),
            lambda state: (
                can_slide(state, player, slide)
                and level_5_1(state, player, slam, fire2, walljump, dash)
                and grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if skulls:
        add_rule(multiworld.get_location("5-1: Secret #5", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        add_rule(multiworld.get_location("Cleared 5-1", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        if challenge:
            add_rule(multiworld.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Blue Skull (5-1)", player, 3))
        if prank:
            add_rule(multiworld.get_location("5-1: Perfect Rank", player),
                lambda state: state.has("Blue Skull (5-1)", player, 3))


    # 5-2
    add_rule(multiworld.get_location("5-2: Secret #1", player),
        lambda state: (
            can_slide(state, player, slide)
            or slam_storage(state, player, slam, walljump)
            or rock0_fire2(state, player, fire2)
        ))

    add_rule(multiworld.get_location("5-2: Secret #2", player),
        lambda state: (
            can_slam(state, player, slam)
            or dashes(state, player, dash, 1)
        ))
    add_rule(multiworld.get_location("5-2: Secret #3", player),
        lambda state: (
            (
                can_slam(state, player, slam)
                or dashes(state, player, dash, 1)
            )
            and jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
        ))
    add_rule(multiworld.get_location("5-2: Secret #4", player),
        lambda state: (
            (
                can_slam(state, player, slam)
                or dashes(state, player, dash, 1)
            )
            and jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
        ))
    add_rule(multiworld.get_location("5-2: Secret #5", player),
        lambda state: (
            (
                can_slam(state, player, slam)
                or dashes(state, player, dash, 1)
            )
            and grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    add_rule(multiworld.get_location("Cleared 5-2", player),
        lambda state: (
            (
                can_slam(state, player, slam)
                or dashes(state, player, dash, 1)
            )
            and grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    if challenge:
        add_rule(multiworld.get_location("5-2: Don't fight the ferryman", player),
            lambda state: (
                (
                    can_slam(state, player, slam)
                    or dashes(state, player, dash, 1)
                )
                and revany2_fire2(state, player, fire2)
                and grab_item(state, player, arm)
                and can_break_idol(state, player, arm, shoalt)
            ))
    if prank:
        add_rule(multiworld.get_location("5-2: Perfect Rank", player),
            lambda state: (
                (
                    can_slam(state, player, slam)
                    or dashes(state, player, dash, 1)
                )
                and grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                and can_break_idol(state, player, arm, shoalt)
            ))

    if skulls:
        add_rule(multiworld.get_location("5-2: Secret #5", player),
            lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        add_rule(multiworld.get_location("Cleared 5-2", player),
            lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        if challenge:
            add_rule(multiworld.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        if prank:
            add_rule(multiworld.get_location("5-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))


    # 5-3
    if skulls:
        set_rule(multiworld.get_location("5-3: Secret #1", player),
            lambda state: state.has("Blue Skull (5-3)", player))
        set_rule(multiworld.get_location("5-3: Secret #3", player),
            lambda state: state.has_all({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(multiworld.get_location("5-3: Weapon", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(multiworld.get_location("5-3: Secret #4", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(multiworld.get_location("5-3: Secret #5", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(multiworld.get_location("Cleared 5-3", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        if hank:
            set_rule(multiworld.get_location("5-3: Assemble Hank Jr.", player),
                lambda state: (
                    state.has_all({"Blue Skull (5-3)", "Red Skull (5-3)"}, player)
                    and grab_item(state, player, arm)
                ))
        if challenge:
            set_rule(multiworld.get_location("5-3: Don't touch any water", player),
                lambda state: state.has_all({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        if prank:
            set_rule(multiworld.get_location("5-3: Perfect Rank", player),
                lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))

    add_rule(multiworld.get_location("5-3: Secret #1", player),
        lambda state: grab_item(state, player, arm))
    add_rule(multiworld.get_location("5-3: Secret #2", player),
        lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))
    add_rule(multiworld.get_location("5-3: Secret #3", player),
        lambda state: grab_item(state, player, arm))
    add_rule(multiworld.get_location("5-3: Weapon", player),
        lambda state: (
            grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    add_rule(multiworld.get_location("5-3: Secret #4", player),
        lambda state: (
            grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    add_rule(multiworld.get_location("5-3: Secret #5", player),
        lambda state: (
            grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    add_rule(multiworld.get_location("Cleared 5-3", player),
        lambda state: (
            grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    if challenge:
        add_rule(multiworld.get_location("5-3: Don't touch any water", player),
            lambda state: (
                grab_item(state, player, arm)
                and can_break_idol(state, player, arm, shoalt)
                and can_slide(state, player, slide)
                and dashes(state, player, dash, 3)
                and jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
            ))
    if prank:
        add_rule(multiworld.get_location("5-3: Perfect Rank", player),
            lambda state: (
                grab_item(state, player, arm)
                and can_break_idol(state, player, arm, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))


    # 5-4
    if boss > 0 and goal != 4:
        set_rule(multiworld.get_location("5-4: Defeat the Leviathan", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))
    if challenge and goal != 4:
        set_rule(multiworld.get_location("5-4: Reach the surface in under 10 seconds", player),
            lambda state: (
                rock0_fire2(state, player, fire2)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))
    if prank and goal != 4:
        set_rule(multiworld.get_location("5-4: Perfect Rank", player),
            lambda state: (
                rock0_fire2(state, player, fire2)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))


    # 6-1
    set_rule(multiworld.get_location("6-1: Secret #2", player),
        lambda state: grab_item(state, player, arm))
    set_rule(multiworld.get_location("6-1: Secret #3", player),
        lambda state: grab_item(state, player, arm))
    set_rule(multiworld.get_location("6-1: Secret #4", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
            and grab_item(state, player, arm)
        ))
    set_rule(multiworld.get_location("6-1: Secret #5", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
            and grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    set_rule(multiworld.get_location("Cleared 6-1", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
            and grab_item(state, player, arm)
            and can_break_idol(state, player, arm, shoalt)
        ))
    if challenge:
        set_rule(multiworld.get_location("6-1: Beat the secret encounter", player),
            lambda state: (
                jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
                and grab_item(state, player, arm)
            ))
    if prank:
        set_rule(multiworld.get_location("6-1: Perfect Rank", player),
            lambda state: (
                jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
                and grab_item(state, player, arm)
                and can_break_idol(state, player, arm, shoalt)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))

    if skulls:
        add_rule(multiworld.get_location("6-1: Secret #2", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(multiworld.get_location("6-1: Secret #3", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(multiworld.get_location("6-1: Secret #4", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(multiworld.get_location("6-1: Secret #5", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(multiworld.get_location("Cleared 6-1", player),
            lambda state: state.has("Red Skull (6-1)", player))
        if challenge:
            add_rule(multiworld.get_location("6-1: Beat the secret encounter", player),
                lambda state: state.has("Red Skull (6-1)", player))
        if prank:
            add_rule(multiworld.get_location("6-1: Perfect Rank", player),
                lambda state: state.has("Red Skull (6-1)", player))


    # 6-2                
    set_rule(multiworld.get_location("Cleared 6-2", player),
        lambda state: (
            (
                can_slam(state, player, slam)
                or walljumps(state, player, walljump, 2)
                or shoalt0_fire2(state, player, shoalt, fire2)
                or shoany1_fire2(state, player, fire2)
                or rai2(state, player)
                or rock0_fire2(state, player, fire2)
            )
            and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
        ))
    if goal != 5:
        if boss > 0:
            set_rule(multiworld.get_location("6-2: Defeat Gabriel", player),
                lambda state: (
                    (
                        can_slam(state, player, slam)
                        or walljumps(state, player, walljump, 2)
                        or shoalt0_fire2(state, player, shoalt, fire2)
                        or shoany1_fire2(state, player, fire2)
                        or rai2(state, player)
                        or rock0_fire2(state, player, fire2)
                    )
                    and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                ))
        if challenge:
            set_rule(multiworld.get_location("6-2: Hit Gabriel into the ceiling", player),
                lambda state: (
                    (
                        can_slam(state, player, slam)
                        or walljumps(state, player, walljump, 2)
                        or shoalt0_fire2(state, player, shoalt, fire2)
                        or shoany1_fire2(state, player, fire2)
                        or rai2(state, player)
                        or rock0_fire2(state, player, fire2)
                    )
                    and rock_any(state, player)
                    and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                ))
        if prank:
            set_rule(multiworld.get_location("6-2: Perfect Rank", player),
                lambda state: (
                    (
                        can_slam(state, player, slam)
                        or walljumps(state, player, walljump, 2)
                        or shoalt0_fire2(state, player, shoalt, fire2)
                        or shoany1_fire2(state, player, fire2)
                        or rai2(state, player)
                        or rock0_fire2(state, player, fire2)
                    )
                    and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
                ))
            

    # 7-1
    set_rule(multiworld.get_location("7-1: Secret #1", player),
        lambda state: jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2))
    set_rule(multiworld.get_location("7-1: Secret #2", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
            and can_slide(state, player, slide)
        ))
    set_rule(multiworld.get_location("7-1: Secret #3", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
            and grab_item(state, player, arm)
        ))
    set_rule(multiworld.get_location("7-1: Secret #4", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
            and grab_item(state, player, arm)
        ))
    set_rule(multiworld.get_location("7-1: Secret #5", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
            and grab_item(state, player, arm)
        ))
    set_rule(multiworld.get_location("Cleared 7-1", player),
        lambda state: (
            jump_general(state, player, slam, fire2, arm, walljump, shoalt, 1)
            and grab_item(state, player, arm)
        ))
    if challenge:
        set_rule(multiworld.get_location("7-1: Beat the secret encounter", player),
            lambda state: grab_item(state, player, arm))
    if prank:
        set_rule(multiworld.get_location("7-1: Perfect Rank", player),
            lambda state: (
                jump_general(state, player, slam, fire2, arm, walljump, shoalt, 2)
                and grab_item(state, player, arm)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))
    if skulls:
        add_rule(multiworld.get_location("7-1: Secret #3", player),
            lambda state: (
                state.has("Red Skull (7-1)", player)
                and state.has("Blue Skull (7-1)", player)
            ))
        add_rule(multiworld.get_location("7-1: Secret #4", player),
            lambda state: (
                state.has("Red Skull (7-1)", player)
                and state.has("Blue Skull (7-1)", player)
            ))
        add_rule(multiworld.get_location("7-1: Secret #5", player),
            lambda state: (
                state.has("Red Skull (7-1)", player)
                and state.has("Blue Skull (7-1)", player)
            ))
        add_rule(multiworld.get_location("Cleared 7-1", player),
            lambda state: (
                state.has("Red Skull (7-1)", player)
                and state.has("Blue Skull (7-1)", player)
            ))
        if challenge:
            add_rule(multiworld.get_location("7-1: Beat the secret encounter", player),
                lambda state: (
                    state.has("Red Skull (7-1)", player)
                    and state.has("Blue Skull (7-1)", player)
                ))
        if prank:
            add_rule(multiworld.get_location("7-1: Perfect Rank", player),
                lambda state: (
                    state.has("Red Skull (7-1)", player)
                    and state.has("Blue Skull (7-1)", player)
                ))


    # 7-2
    set_rule(multiworld.get_location("7-2: Secret #1", player),
        lambda state: (
            arm2(state, player)
            and (
                walljumps(state, player, walljump, 1)
                or dashes(state, player, dash, 1)
            )
        ))
    set_rule(multiworld.get_location("7-2: Secret #2", player),
        lambda state: arm2(state, player))
    set_rule(multiworld.get_location("7-2: Secret #3", player),
        lambda state: arm2(state, player))
    set_rule(multiworld.get_location("7-2: Secret #4", player),
        lambda state: arm2(state, player))
    set_rule(multiworld.get_location("7-2: Secret #5", player),
        lambda state: arm2(state, player))
    
    if not l7switch:
        set_rule(multiworld.get_location("7-2: Secret Weapon", player),
            lambda state: (
                arm2(state, player)
                and dashes(state, player, dash, 1)
            ))
    else:
        set_rule(multiworld.get_location("7-2: Switch #1", player),
            lambda state: (
                arm2(state, player)
                and dashes(state, player, dash, 1)
            ))
        set_rule(multiworld.get_location("7-2: Switch #2", player),
            lambda state: (
                arm2(state, player)
                and dashes(state, player, dash, 1)
            ))
        set_rule(multiworld.get_location("7-2: Switch #3", player),
            lambda state: (
                arm2(state, player)
                and dashes(state, player, dash, 1)
            ))
        if skulls:
            add_rule(multiworld.get_location("7-2: Switch #3", player),
                lambda state: state.has("Red Skull (7-2)", player))
        set_rule(multiworld.get_location("7-2: Secret Weapon", player),
            lambda state: state.has_all({"Violence Switch I", "Violence Switch II", "Violence Switch III"}, player))

    set_rule(multiworld.get_location("Cleared 7-2", player),
        lambda state: arm2(state, player))
    if challenge:
        set_rule(multiworld.get_location("7-2: Don't kill any enemies", player),
            lambda state: (
                arm2(state, player)
                and can_break_walls(state, player, fire2, arm, revalt, shoalt, naialt)
            ))
    if prank:
        set_rule(multiworld.get_location("7-2: Perfect Rank", player),
            lambda state: (
                arm2(state, player)
                and good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt)
            ))
    if skulls:
        add_rule(multiworld.get_location("Cleared 7-2", player),
            lambda state: state.has("Red Skull (7-2)", player))
        if challenge:
            add_rule(multiworld.get_location("7-2: Don't kill any enemies", player),
                lambda state: state.has("Red Skull (7-2)", player))
        if prank:
            add_rule(multiworld.get_location("7-2: Perfect Rank", player),
                lambda state: state.has("Red Skull (7-2)", player))
            
    
    # 7-3
    set_rule(multiworld.get_location("7-3: Secret #5", player),
        lambda state: can_slide(state, player, slide))
    #if challenge: <- can cheese by going thru secret exit now
    #    set_rule(multiworld.get_location("7-3: Become marked for death", player),
    #        lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))
    if prank:
        set_rule(multiworld.get_location("7-3: Perfect Rank", player),
            lambda state: good_weapon(state, player, fire2, arm, slide, dash, shoalt, naialt))
        

    # 7-S
    if secretcompletion:
        set_rule(multiworld.get_location("Cleared 7-S", player),
            lambda state: grab_item(state, player, arm))
    
    if clean:
        set_rule(multiworld.get_location("7-S: Cleaned Courtyard", player),
            lambda state: grab_item(state, player, arm))
        set_rule(multiworld.get_location("7-S: Cleaned Library", player),
            lambda state: grab_item(state, player, arm))
        set_rule(multiworld.get_location("7-S: Cleaned Lobby", player),
            lambda state: grab_item(state, player, arm))
        set_rule(multiworld.get_location("7-S: Cleaned Lounge", player),
            lambda state: grab_item(state, player, arm))
        set_rule(multiworld.get_location("7-S: Cleaned Side Room", player),
            lambda state: grab_item(state, player, arm))

    if skulls:
        if secretcompletion:
            add_rule(multiworld.get_location("Cleared 7-S", player),
                lambda state: state.has_all({"Red Skull (7-S)", "Blue Skull (7-S)"}, player))
        
        if clean:
            add_rule(multiworld.get_location("7-S: Cleaned Courtyard", player),
                lambda state: state.has_all({"Red Skull (7-S)", "Blue Skull (7-S)"}, player))
            add_rule(multiworld.get_location("7-S: Cleaned Library", player),
                lambda state: state.has_all({"Red Skull (7-S)", "Blue Skull (7-S)"}, player))
            add_rule(multiworld.get_location("7-S: Cleaned Lobby", player),
                lambda state: state.has_all({"Red Skull (7-S)", "Blue Skull (7-S)"}, player))
            add_rule(multiworld.get_location("7-S: Cleaned Lounge", player),
                lambda state: state.has_all({"Red Skull (7-S)", "Blue Skull (7-S)"}, player))
            add_rule(multiworld.get_location("7-S: Cleaned Side Room", player),
                lambda state: state.has_all({"Red Skull (7-S)", "Blue Skull (7-S)"}, player))
        

    # 7-4
    set_rule(multiworld.get_location("Cleared 7-4", player),
        lambda state: (
            can_slide(state, player, slide)
            and (
                arm2(state, player)
                or slam_storage(state, player, slam, walljump)
            )
            and can_break_idol(state, player, arm, shoalt)
        ))
    if goal != 8:
        if boss > 0:
            set_rule(multiworld.get_location('7-4: Defeat 1000-THR "Earthmover"', player),
                lambda state: (
                    can_slide(state, player, slide)
                    and (
                        arm2(state, player)
                        or slam_storage(state, player, slam, walljump)
                    )
                    and can_break_idol(state, player, arm, shoalt)
                ))
        if challenge:
            set_rule(multiworld.get_location("7-4: Don't fight the security system", player),
                lambda state: (
                    can_slide(state, player, slide)
                    and (
                        arm2(state, player)
                        or slam_storage(state, player, slam, walljump)
                    )
                    and can_break_idol(state, player, arm, shoalt)
                    and can_zap(state, player, fire2)
                ))
        if prank:
            set_rule(multiworld.get_location("7-4: Perfect Rank", player),
                lambda state: (
                    can_slide(state, player, slide)
                    and arm2(state, player)
                    and can_break_idol(state, player, arm, shoalt)
                ))


    # shop
    set_rule(multiworld.get_location("Shop: Buy Revolver Variant 1", player),
        lambda state: rev_any(state, player))

    set_rule(multiworld.get_location("Shop: Buy Revolver Variant 2", player),
        lambda state: rev_any(state, player))

    set_rule(multiworld.get_location("Shop: Buy Shotgun Variant 1", player),
        lambda state: sho_any(state, player))
    
    set_rule(multiworld.get_location("Shop: Buy Shotgun Variant 2", player),
        lambda state: sho_any(state, player))

    set_rule(multiworld.get_location("Shop: Buy Nailgun Variant 1", player),
        lambda state: nai_any(state, player))
    
    set_rule(multiworld.get_location("Shop: Buy Nailgun Variant 2", player),
        lambda state: nai_any(state, player))

    set_rule(multiworld.get_location("Shop: Buy Railcannon Variant 1", player),
        lambda state: rai_any(state, player))

    set_rule(multiworld.get_location("Shop: Buy Railcannon Variant 2", player),
        lambda state: rai_any(state, player))

    set_rule(multiworld.get_location("Shop: Buy Rocket Launcher Variant 1", player),
        lambda state: rock_any(state, player))
    
    set_rule(multiworld.get_location("Shop: Buy Rocket Launcher Variant 2", player),
        lambda state: rock_any(state, player))
    

    # Museum
    if rocket_race:
        set_rule(multiworld.get_location("Museum: Win rocket race", player),
            lambda state: rock0_fire2(state, player, fire2))