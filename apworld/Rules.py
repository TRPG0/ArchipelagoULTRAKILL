from worlds.generic.Rules import add_rule
from BaseClasses import CollectionState
from typing import Callable, Dict, TYPE_CHECKING
from .Regions import Regions, SecretRegion

if TYPE_CHECKING:
    from . import UltrakillWorld
else:
    UltrakillWorld = object


# print nonexistent locations/entrances during generate
DEBUG: bool = False


class UltrakillRules:
    def __init__(self, world: "UltrakillWorld") -> None:
        self.world = world

    def set_rules(self) -> None:
        player = self.world.player
        options = self.world.options

        def stamina(state: CollectionState, needs: int) -> bool:
            bars: int = needs - options.starting_stamina.value
            return True if bars <= 0 else state.has("Stamina Bar", player, bars)
        
        def walljumps(state: CollectionState, needs: int) -> bool:
            jumps: int = needs - options.starting_walljumps.value
            return True if jumps <= 0 else state.has("Wall Jump", player, jumps)
        
        def slide(state: CollectionState) -> bool:
            return True if options.start_with_slide else state.has("Slide", player)

        def slam(state: CollectionState) -> bool:
            return True if options.start_with_slam else state.has("Slam", player)

        def revstd0(state: CollectionState) -> bool:
            """Revolver - Piercer"""
            return state.has("Revolver - Piercer", player) if not options.revolver_form else state.has_all({"Revolver - Piercer", "Revolver - Standard"}, player)

        def revalt0(state: CollectionState) -> bool:
            """Revolver - Piercer"""
            return state.has("Revolver - Piercer", player) if options.revolver_form else state.has_all({"Revolver - Piercer", "Revolver - Alternate"}, player)
        
        def revany0(state: CollectionState) -> bool:
            """Revolver - Piercer"""
            return state.has("Revolver - Piercer", player)
        
        def revstd0_fire2(state: CollectionState) -> bool:
            """Revolver - Piercer"""
            return (
                revstd0(state)
                and state.has("Secondary Fire - Piercer", player)
            ) if options.randomize_secondary_fire else revstd0(state)
        
        def revalt0_fire2(state: CollectionState) -> bool:
            """Revolver - Piercer"""
            return (
                revalt0(state)
                and state.has("Secondary Fire - Piercer", player)
            ) if options.randomize_secondary_fire else revalt0(state)
        
        def revany0_fire2(state: CollectionState) -> bool:
            """Revolver - Piercer"""
            return (
                revany0(state)
                and state.has("Secondary Fire - Piercer", player)
            ) if options.randomize_secondary_fire else revany0(state)
        
        def revstd1(state: CollectionState) -> bool:
            """Revolver - Sharpshooter"""
            return state.has("Revolver - Sharpshooter", player) if not options.revolver_form else state.has_all({"Revolver - Sharpshooter", "Revolver - Standard"}, player)
        
        def revalt1(state: CollectionState) -> bool:
            """Revolver - Sharpshooter"""
            return state.has("Revolver - Sharpshooter", player) if options.revolver_form else state.has_all({"Revolver - Sharpshooter", "Revolver - Alternate"}, player)

        def revany1(state: CollectionState) -> bool:
            """Revolver - Sharpshooter"""
            return state.has("Revolver - Sharpshooter", player)
        
        def revstd1_fire2(state: CollectionState) -> bool:
            """Revolver - Sharpshooter"""
            return (
                revstd1(state)
                and state.has("Secondary Fire - Sharpshooter", player)
            ) if options.randomize_secondary_fire else revstd1(state)

        def revalt1_fire2(state: CollectionState) -> bool:
            """Revolver - Sharpshooter"""
            return (
                revalt1(state)
                and state.has("Secondary Fire - Sharpshooter", player)
            ) if options.randomize_secondary_fire else revalt1(state)

        def revany1_fire2(state: CollectionState) -> bool:
            """Revolver - Sharpshooter"""
            return (
                revany1(state)
                and state.has("Secondary Fire - Sharpshooter", player)
            ) if options.randomize_secondary_fire else revany1(state)

        def revstd2(state: CollectionState) -> bool:
            """Revolver - Marksman"""
            return state.has("Revolver - Marksman", player) if not options.revolver_form else state.has_all({"Revolver - Marksman", "Revolver - Standard"}, player)
        
        def revalt2(state: CollectionState) -> bool:
            """Revolver - Marksman"""
            return state.has("Revolver - Marksman", player) if options.revolver_form else state.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player)

        def revany2(state: CollectionState) -> bool:
            """Revolver - Marksman"""
            return state.has("Revolver - Marksman", player)
        
        def revstd2_fire2(state: CollectionState) -> bool:
            """Revolver - Marksman"""
            return (
                revstd2(state)
                and state.has("Secondary Fire - Marksman", player)
            ) if options.randomize_secondary_fire else revstd2(state)
        
        def revalt2_fire2(state: CollectionState) -> bool:
            """Revolver - Marksman"""
            return (
                revalt2(state)
                and state.has("Secondary Fire - Marksman", player)
            ) if options.randomize_secondary_fire else revalt2(state)
        
        def revany2_fire2(state: CollectionState) -> bool:
            """Revolver - Marksman"""
            return (
                revany2(state)
                and state.has("Secondary Fire - Marksman", player)
            ) if options.randomize_secondary_fire else revany2(state)

        def revstd_any(state: CollectionState) -> bool:
            return state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player) if not options.revolver_form else (
                state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player)
                and state.has("Revolver - Standard", player)
            )
        
        def revalt_any(state: CollectionState) -> bool:
            return state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player) if options.revolver_form else (
                state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player)
                and state.has("Revolver - Alternate", player)
            )
        
        def rev_any(state: CollectionState) -> bool:
            return state.has_any({"Revolver - Piercer", "Revolver - Sharpshooter", "Revolver - Marksman"}, player)
        
        def shostd0(state: CollectionState) -> bool:
            """Shotgun - Core Eject"""
            return state.has("Shotgun - Core Eject", player) if not options.shotgun_form else state.has_all({"Shotgun - Core Eject", "Shotgun - Standard"}, player)
        
        def shoalt0(state: CollectionState) -> bool:
            """Shotgun - Core Eject"""
            return state.has("Shotgun - Core Eject", player) if options.shotgun_form else state.has_all({"Shotgun - Core Eject", "Shotgun - Alternate"}, player)
        
        def shoany0(state: CollectionState) -> bool:
            """Shotgun - Core Eject"""
            return state.has("Shotgun - Core Eject", player)

        def shostd0_fire2(state: CollectionState) -> bool:
            """Shotgun - Core Eject"""
            return (
                shostd0(state)
                and state.has("Secondary Fire - Core Eject", player)
            ) if options.randomize_secondary_fire else shostd0(state)

        def shoalt0_fire2(state: CollectionState) -> bool:
            """Shotgun - Core Eject"""
            return (
                shoalt0(state)
                and state.has("Secondary Fire - Core Eject", player)
            ) if options.randomize_secondary_fire else shoalt0(state)
        
        def shoany0_fire2(state: CollectionState) -> bool:
            """Shotgun - Core Eject"""
            return (
                shoany0(state)
                and state.has("Secondary Fire - Core Eject", player)
            ) if options.randomize_secondary_fire else shoany0(state)

        def shostd1(state: CollectionState) -> bool:
            """Shotgun - Pump Charge"""
            return state.has("Shotgun - Pump Charge", player) if not options.shotgun_form else state.has_all({"Shotgun - Pump Charge", "Shotgun - Standard"}, player)
        
        def shoalt1(state: CollectionState) -> bool:
            """Shotgun - Pump Charge"""
            return state.has("Shotgun - Pump Charge", player) if options.shotgun_form else state.has_all({"Shotgun - Pump Charge", "Shotgun - Alternate"}, player)
        
        def shoany1(state: CollectionState) -> bool:
            """Shotgun - Pump Charge"""
            return state.has("Shotgun - Pump Charge", player)

        def shostd1_fire2(state: CollectionState) -> bool:
            """Shotgun - Pump Charge"""
            return (
                shostd1(state)
                and state.has("Secondary Fire - Pump Charge", player)
            ) if options.randomize_secondary_fire else shostd1(state)
        
        def shoalt1_fire2(state: CollectionState) -> bool:
            """Shotgun - Pump Charge"""
            return (
                shoalt1(state)
                and state.has("Secondary Fire - Pump Charge", player)
            ) if options.randomize_secondary_fire else shoalt1(state)
        
        def shoany1_fire2(state: CollectionState) -> bool:
            """Shotgun - Pump Charge"""
            return (
                shoany1(state)
                and state.has("Secondary Fire - Pump Charge", player)
            ) if options.randomize_secondary_fire else shoany1(state)

        def shostd2(state: CollectionState) -> bool:
            """Shotgun - Sawed-On"""
            return state.has("Shotgun - Sawed-On", player) if not options.shotgun_form else state.has_all({"Shotgun - Sawed-On", "Shotgun - Standard"}, player)
        
        def shoalt2(state: CollectionState) -> bool:
            """Shotgun - Sawed-On"""
            return state.has("Shotgun - Sawed-On", player) if options.shotgun_form else state.has_all({"Shotgun - Sawed-On", "Shotgun - Alternate"}, player)

        def shoany2(state: CollectionState) -> bool:
            """Shotgun - Sawed-On"""
            return state.has("Shotgun - Sawed-On", player)

        def shostd2_fire2(state: CollectionState) -> bool:
            """Shotgun - Sawed-On"""
            return (
                shostd2(state)
                and state.has("Secondary Fire - Sawed-On", player)
            ) if options.randomize_secondary_fire else shostd2(state)
        
        def shoalt2_fire2(state: CollectionState) -> bool:
            """Shotgun - Sawed-On"""
            return (
                shoalt2(state)
                and state.has("Secondary Fire - Sawed-On", player)
            ) if options.randomize_secondary_fire else shoalt2(state)

        def shoany2_fire2(state: CollectionState) -> bool:
            """Shotgun - Sawed-On"""
            return (
                shoany2(state)
                and state.has("Secondary Fire - Sawed-On", player)
            ) if options.randomize_secondary_fire else shoany2(state)
        
        def shostd_any(state: CollectionState) -> bool:
            return state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player) if not options.shotgun_form else (
                state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player)
                and state.has("Shotgun - Standard", player)
            )
        
        def shoalt_any(state: CollectionState) -> bool:
            return state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player) if options.shotgun_form else (
                state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player)
                and state.has("Shotgun - Alternate", player)
            )
        
        def sho_any(state: CollectionState) -> bool:
            return state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Shotgun - Sawed-On"}, player)

        def naistd0(state: CollectionState) -> bool:
            """Nailgun - Attractor"""
            return state.has("Nailgun - Attractor", player) if not options.nailgun_form else state.has_all({"Nailgun - Attractor", "Nailgun - Standard"}, player)
        
        def naialt0(state: CollectionState) -> bool:
            """Nailgun - Attractor"""
            return state.has("Nailgun - Attractor", player) if options.nailgun_form else state.has_all({"Nailgun - Attractor", "Nailgun - Alternate"}, player)
        
        def naiany0(state: CollectionState) -> bool:
            """Nailgun - Attractor"""
            return state.has("Nailgun - Attractor", player)
        
        def naistd0_fire2(state: CollectionState) -> bool:
            """Nailgun - Attractor"""
            return (
                naistd0(state)
                and state.has("Secondary Fire - Attractor", player)
            ) if options.randomize_secondary_fire else naistd0(state)
        
        def naialt0_fire2(state: CollectionState) -> bool:
            """Nailgun - Attractor"""
            return (
                naialt0(state)
                and state.has("Secondary Fire - Attractor", player)
            ) if options.randomize_secondary_fire else naialt0(state)
        
        def naiany0_fire2(state: CollectionState) -> bool:
            """Nailgun - Attractor"""
            return (
                naiany0(state)
                and state.has("Secondary Fire - Attractor", player)
            ) if options.randomize_secondary_fire else naiany0(state)

        def naistd1(state: CollectionState) -> bool:
            """Nailgun - Overheat"""
            return state.has("Nailgun - Overheat", player) if not options.nailgun_form else state.has_all({"Nailgun - Overheat", "Nailgun - Standard"}, player)
        
        def naialt1(state: CollectionState) -> bool:
            """Nailgun - Overheat"""
            return state.has("Nailgun - Overheat", player) if options.nailgun_form else state.has_all({"Nailgun - Overheat", "Nailgun - Alternate"}, player)
        
        def naiany1(state: CollectionState) -> bool:
            """Nailgun - Overheat"""
            return state.has("Nailgun - Overheat", player)
        
        def naistd1_fire2(state: CollectionState) -> bool:
            """Nailgun - Overheat"""
            return (
                naistd1(state)
                and state.has("Secondary Fire - Overheat", player)
            ) if options.randomize_secondary_fire else naistd1(state)
        
        def naialt1_fire2(state: CollectionState) -> bool:
            """Nailgun - Overheat"""
            return (
                naialt1(state)
                and state.has("Secondary Fire - Overheat", player)
            ) if options.randomize_secondary_fire else naialt1(state)
        
        def naiany1_fire2(state: CollectionState) -> bool:
            """Nailgun - Overheat"""
            return (
                naiany1(state)
                and state.has("Secondary Fire - Overheat", player)
            ) if options.randomize_secondary_fire else naiany1(state)

        def naistd2(state: CollectionState) -> bool:
            """Nailgun - JumpStart"""
            return state.has("Nailgun - JumpStart", player) if not options.nailgun_form else state.has_all({"Nailgun - JumpStart", "Nailgun - Standard"}, player)
        
        def naialt2(state: CollectionState) -> bool:
            """Nailgun - JumpStart"""
            return state.has("Nailgun - JumpStart", player) if options.nailgun_form else state.has_all({"Nailgun - JumpStart", "Nailgun - Alternate"}, player)
        
        def naiany2(state: CollectionState) -> bool:
            """Nailgun - JumpStart"""
            return state.has("Nailgun - JumpStart", player)
        
        def naistd2_fire2(state: CollectionState) -> bool:
            """Nailgun - JumpStart"""
            return (
                naistd2(state)
                and state.has("Secondary Fire - JumpStart", player)
            ) if options.randomize_secondary_fire else naistd2(state)
        
        def naialt2_fire2(state: CollectionState) -> bool:
            """Nailgun - JumpStart"""
            return (
                naialt2(state)
                and state.has("Secondary Fire - JumpStart", player)
            ) if options.randomize_secondary_fire else naialt2(state)
        
        def naiany2_fire2(state: CollectionState) -> bool:
            """Nailgun - JumpStart"""
            return (
                naiany2(state)
                and state.has("Secondary Fire - JumpStart", player)
            ) if options.randomize_secondary_fire else naiany2(state)
        
        def naistd_any(state: CollectionState) -> bool:
            return state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player) if not options.nailgun_form else (
                state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player)
                and state.has("Nailgun - Standard", player)
            )
        
        def naialt_any(state: CollectionState) -> bool:
            return state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player) if options.nailgun_form else (
                state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player)
                and state.has("Nailgun - Alternate", player)
            )
        
        def nai_any(state: CollectionState) -> bool:
            return state.has_any({"Nailgun - Attractor", "Nailgun - Overheat", "Nailgun - JumpStart"}, player)
        
        def rai0(state: CollectionState) -> bool:
            """Railcannon - Electric"""
            return state.has("Railcannon - Electric", player)
        
        def rai1(state: CollectionState) -> bool:
            """Railcannon - Screwdriver"""
            return state.has("Railcannon - Screwdriver", player)
        
        def rai2(state: CollectionState) -> bool:
            """Railcannon - Malicious"""
            return state.has("Railcannon - Malicious", player)
        
        def rai_any(state: CollectionState) -> bool:
            return state.has_any({"Railcannon - Electric", "Railcannon - Screwdriver", "Railcannon - Malicious"}, player)
        
        def rock0(state: CollectionState) -> bool:
            """Rocket Launcher - Freezeframe"""
            return state.has("Rocket Launcher - Freezeframe", player)
        
        def rock0_fire2(state: CollectionState) -> bool:
            """Rocket Launcher - Freezeframe"""
            return (
                rock0(state)
                and state.has("Secondary Fire - Freezeframe", player)
            ) if options.randomize_secondary_fire else rock0(state)
        
        def rock1(state: CollectionState) -> bool:
            """Rocket Launcher - S.R.S. Cannon"""
            return state.has("Rocket Launcher - S.R.S. Cannon", player)
        
        def rock1_fire2(state: CollectionState) -> bool:
            """Rocket Launcher - S.R.S. Cannon"""
            return (
                rock1(state)
                and state.has("Secondary Fire - S.R.S. Cannon", player)
            ) if options.randomize_secondary_fire else rock1(state)
        
        def rock2(state: CollectionState) -> bool:
            """Rocket Launcher - Firestarter"""
            return state.has("Rocket Launcher - Firestarter", player)
        
        def rock2_fire2(state: CollectionState) -> bool:
            """Rocket Launcher - Firestarter"""
            return (
                rock2(state)
                and state.has("Secondary Fire - Firestarter", player)
            ) if options.randomize_secondary_fire else rock2(state)
        
        def rock_any(state: CollectionState) -> bool:
            return state.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Rocket Launcher - Firestarter"}, player)
        
        def arm0(state: CollectionState) -> bool:
            """Feedbacker"""
            return state.has("Feedbacker", player) if not options.start_with_arm else True
        
        def arm1(state: CollectionState) -> bool:
            """Knuckleblaster"""
            return state.has("Knuckleblaster", player)
        
        def arm2(state: CollectionState) -> bool:
            """Whiplash"""
            return state.has("Whiplash", player)
        
        def arm_any(state: CollectionState) -> bool:
            return arm0(state) or arm1(state) or arm2(state)
        
        def can_punch(state: CollectionState) -> bool:
            return arm0(state) or arm1(state)
        
        def can_break_idol(state: CollectionState) -> bool:
            return can_punch(state) or shoalt_any(state)
        
        def grab_item(state: CollectionState) -> bool:
            return arm0(state) or arm1(state)
        
        def skull(state: CollectionState, level: str, color: str, count: int = 1) -> bool:
            return state.has(f"{color.capitalize()} Skull ({level})", player, count) if options.randomize_skulls else True
        
        def can_zap(state: CollectionState) -> bool:
            return naiany2_fire2(state) or rai0(state)
        
        def can_proj_boost(state: CollectionState) -> bool:
            return shostd_any(state) and arm0(state)
        
        def slam_storage(state: CollectionState) -> bool:
            return slam(state) and walljumps(state, 1)
        
        def good_weapon(state: CollectionState) -> bool:
            return (
                (
                    rev_any(state)
                    or shoany0_fire2(state)
                    or shoany1_fire2(state)
                    or naistd2(state)
                    or can_proj_boost(state)
                )
                and (
                    slide(state)
                    or stamina(state, 1)
                )
            )
        
        def has_weapon_types(state: CollectionState, count: int) -> bool:
            total: int = 0

            if revany0_fire2(state) or revany1_fire2(state) or revany2_fire2(state):
                total += 1

            if shoany0_fire2(state) or shoany1_fire2(state) or shoany2_fire2(state):
                total += 1

            if naiany0_fire2(state) or naiany1_fire2(state) or naiany2_fire2(state):
                total += 1

            if rock0_fire2(state) or rock1_fire2(state) or rock2_fire2(state):
                total += 1

            return total >= count
        
        def can_break_glass(state: CollectionState) -> bool:
            return (
                rai0(state)
                or rai2(state)
                or rock_any(state)
                or arm1(state)
                or revstd0_fire2(state)
                or revstd1_fire2(state)
                or revstd2_fire2(state)
                or revalt_any(state)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or can_proj_boost(state)
            )
        
        def can_break_far_glass(state: CollectionState) -> bool:
            return (
                rai0(state)
                or rai2(state)
                or rock_any(state)
                or revstd0_fire2(state)
                or revstd1_fire2(state)
                or revstd2_fire2(state)
                or revalt_any(state)
                or shoany0_fire2(state)
                or can_proj_boost(state)
            )
        
        def can_break_walls(state: CollectionState) -> bool:
            return (
                rai0(state)
                or rai2(state)
                or rock_any(state)
                or arm1(state)
                or revalt_any(state)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or naistd1_fire2(state)
                or can_proj_boost(state)
            )
        
        def can_break_wall_cancerous_rodent(state: CollectionState) -> bool:
            return (
                rai0(state)
                or rai2(state)
                or rock_any(state)
                or arm1(state)
                or revalt_any(state)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or can_proj_boost(state)
            )
        
        def can_break_glass_or_walls(state: CollectionState) -> bool:
            return (
                rai0(state)
                or rai2(state)
                or rock_any(state)
                or arm1(state)
                or revstd0_fire2(state)
                or revstd1_fire2(state)
                or revstd2_fire2(state)
                or revalt_any(state)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or naistd1_fire2(state)
                or can_proj_boost(state)
            )
        
        def jump_general(state: CollectionState, needs_jumps: int) -> bool:
            return (
                slam(state)
                or walljumps(state, needs_jumps)
                or rock_any(state)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or can_proj_boost(state)
                or rai2(state)
            )
        
        def l2_secret_exit(state: CollectionState) -> bool:
            """0-2: Secret exit"""
            return (
                walljumps(state, 3)
                or slam_storage(state)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or can_proj_boost(state)
                or rai2(state)
                or rock_any(state)
            )
        
        def l3_challenge(state: CollectionState) -> bool:
            """0-3: Challenge (Kill only 1 enemy)"""
            return (
                can_break_far_glass(state)
                and (
                    slam_storage(state)
                    or shoalt0_fire2(state)
                    or shoany1_fire2(state)
                    or (
                        shostd0_fire2(state)
                        and walljumps(state, 2)
                    )
                    or rock0_fire2(state)
                    or rai2(state)
                )
            )
        
        def l5_general(state: CollectionState) -> bool:
            """0-5: Cross the gap"""
            return (
                (
                    slide(state)
                    and (
                        walljumps(state, 1)
                        or stamina(state, 1)
                    )
                    or rock_any(state)
                    or shostd0_fire2(state)
                    or shostd1_fire2(state)
                    or shoalt_any(state)
                    or can_proj_boost(state)
                    or rai2(state)
                )
            )
        
        def l6_switch(state: CollectionState) -> bool:
            """1-1: Jump to Limbo Switch"""
            return (
                slam(state)
                or rock_any(state)
                or shoany0_fire2(state)
                or shoany1_fire2(state)
                or can_proj_boost(state)
                or rai2(state)
            )

        
        def l10_exit(state: CollectionState) -> bool:
            """2-1: Exit first tower"""
            return (
                slam(state)
                or rai2(state)
                or rock_any(state)
                or walljumps(state, 1)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or can_proj_boost(state)
            )
        
        def l10_tower(state: CollectionState) -> bool:
            """2-1: Reach and climb the tower"""
            return (
                slam(state)
                or rai2(state)
                or rock_any(state)
                or (
                    walljumps(state, 1)
                    and stamina(state, 1)
                )
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or can_proj_boost(state)
            )
        
        def l10_challenge(state: CollectionState) -> bool:
            """2-1: Challenge (Don't open any normal doors)"""
            return (
                can_break_walls(state)
                and (
                    # reach end of level
                    rock0_fire2(state)
                    or slam_storage(state)
                    or (
                        walljumps(state, 3)
                        and stamina(state, 2)
                        and (
                            shoalt0_fire2(state)
                            or shoany1_fire2(state)
                            or rai2(state)
                        )
                    )
                )
            )

        def l11_challenge(state: CollectionState) -> bool:
            """2-2: Challenge (Beat the level in under 60 seconds)"""
            return (
                (
                    # get through corridor after railcannon relatively quick
                    rev_any(state)
                    or shostd_any(state)
                    or nai_any(state)
                    or rai0(state)
                    or slide(state)
                    or stamina(state, 1)
                    or arm2(state)
                )
                and (
                    # hit targets reliably
                    rev_any(state)
                    or naiany0(state)
                    or naistd1_fire2(state)
                    or naistd2(state)
                    or can_proj_boost(state)
                    or rock_any(state)
                )
                and (
                    # movement
                    slide(state)
                    or stamina(state, 1)
                    or rock0_fire2(state)
                )
            )

        def l12_s3(state: CollectionState) -> bool:
            """2-3: Secret #3"""
            return (
                slam(state)
                or rai2(state)
                or walljumps(state, 1)
                or shostd0_fire2(state)
                or shostd1_fire2(state)
                or shoalt_any(state)
                or rock_any(state)
                or can_proj_boost(state)
            )

        def l16_challenge(state: CollectionState) -> bool:
            """4-1: Challenge (Don't activate any enemies)"""
            return (
                rock0_fire2(state)
                or (
                    slam_storage(state)
                    and can_break_walls(state)
                )
                or (
                    (
                        rai2(state)
                        or shoany1_fire2(state)
                        or shoalt0_fire2(state)
                    )
                    and (
                        walljumps(state, 1)
                        or slam(state)
                    )
                )
            )

        def l18_general(state: CollectionState) -> bool:
            """4-3: Light torches"""
            return (
                grab_item(state)
                or rai2(state)
                or shoany0_fire2(state)
                or can_proj_boost(state)
            )
        
        def l19_general(state: CollectionState) -> bool:
            """4-4: Cross gap or use shortcut"""
            return (
                (
                    arm2(state)
                    and skull(state, "4-4", "Blue")
                )
                or (
                    stamina(state, 1)
                    and walljumps(state, 1)
                )
                or walljumps(state, 2)
                or slam(state)
                or rock0_fire2(state)
            )
        
        def l20_general(state: CollectionState) -> bool:
            """5-1: Movement"""
            return (
                slide(state)
                and (
                    (
                        slam(state)
                        and walljumps(state, 3)
                        and stamina(state, 2)
                    )
                    or rock0_fire2(state)
                    or arm2(state)
                )
            )
        
        level_rules: Dict[str, Callable[[CollectionState], bool]] = {
            "shop": 
                lambda state: (
                    (
                        state.has_group("levels", player)
                        or state.has_group("layers", player)
                    )
                    or self.world.start_level.short_name != "0-1"
                ),
            
            "0-1":
                lambda state: (
                    state.has("0-1: INTO THE FIRE", player)
                    or state.has("OVERTURE: THE MOUTH OF HELL", player)
                ),

            "0-2":
                lambda state: (
                    state.has("0-2: THE MEATGRINDER", player)
                    or state.has("OVERTURE: THE MOUTH OF HELL", player)
                ),

            "0-3":
                lambda state: (
                    state.has("0-3: DOUBLE DOWN", player)
                    or state.has("OVERTURE: THE MOUTH OF HELL", player)
                ),

            "0-4":
                lambda state: (
                    state.has("0-4: A ONE-MACHINE ARMY", player)
                    or state.has("OVERTURE: THE MOUTH OF HELL", player)
                ),

            "0-5":
                lambda state: (
                    state.has("0-5: CERBERUS", player)
                    or state.has("OVERTURE: THE MOUTH OF HELL", player)
                ),

            "1-1":
                lambda state: (
                    state.has("1-1: HEART OF THE SUNRISE", player)
                    or state.has("LAYER 1: LIMBO", player)
                ),
            
            "1-2":
                lambda state: (
                    state.has("1-2: THE BURNING WORLD", player)
                    or state.has("LAYER 1: LIMBO", player)
                ),

            "1-3":
                lambda state: (
                    state.has("1-3: HALLS OF SACRED REMAINS", player)
                    or state.has("LAYER 1: LIMBO", player)
                ),

            "1-4":
                lambda state: (
                    state.has("1-4: CLAIR DE LUNE", player)
                    or state.has("LAYER 1: LIMBO", player)
                ),

            "2-1":
                lambda state: (
                    state.has("2-1: BRIDGEBURNER", player)
                    or state.has("LAYER 2: LUST", player)
                ),

            "2-2":
                lambda state: (
                    state.has("2-2: DEATH AT 20,000 VOLTS", player)
                    or state.has("LAYER 2: LUST", player)
                ),

            "2-3":
                lambda state: (
                    state.has("2-3: SHEER HEART ATTACK", player)
                    or state.has("LAYER 2: LUST", player)
                ),

            "2-4":
                lambda state: (
                    state.has("2-4: COURT OF THE CORPSE KING", player)
                    or state.has("LAYER 2: LUST", player)
                ),

            "3-1":
                lambda state: (
                    state.has("3-1: BELLY OF THE BEAST", player)
                    or state.has("LAYER 3: GLUTTONY", player)
                ),

            "3-2":
                lambda state: (
                    state.has("3-2: IN THE FLESH", player)
                    or state.has("LAYER 3: GLUTTONY", player)
                ),

            "4-1":
                lambda state: (
                    state.has("4-1: SLAVES TO POWER", player)
                    or state.has("LAYER 4: GREED", player)
                ),

            "4-2":
                lambda state: (
                    state.has("4-2: GOD DAMN THE SUN", player)
                    or state.has("LAYER 4: GREED", player)
                ),

            "4-3":
                lambda state: (
                    state.has("4-3: A SHOT IN THE DARK", player)
                    or state.has("LAYER 4: GREED", player)
                ),

            "4-4":
                lambda state: (
                    state.has("4-4: CLAIR DE SOLEIL", player)
                    or state.has("LAYER 4: GREED", player)
                ),

            "5-1":
                lambda state: (
                    state.has("5-1: IN THE WAKE OF POSEIDON", player)
                    or state.has("LAYER 5: WRATH", player)
                ),

            "5-2":
                lambda state: (
                    state.has("5-2: WAVES OF THE STARLESS SEA", player)
                    or state.has("LAYER 5: WRATH", player)
                ),
            
            "5-3":
                lambda state: (
                    state.has("5-3: SHIP OF FOOLS", player)
                    or state.has("LAYER 5: WRATH", player)
                ),

            "5-4":
                lambda state: (
                    state.has("5-4: LEVIATHAN", player)
                    or state.has("LAYER 5: WRATH", player)
                ),

            "6-1":
                lambda state: (
                    state.has("6-1: CRY FOR THE WEEPER", player)
                    or state.has("LAYER 6: HERESY", player)
                ),

            "6-2":
                lambda state: (
                    state.has("6-2: AESTHETICS OF HATE", player)
                    or state.has("LAYER 6: HERESY", player)
                ),

            "7-1":
                lambda state: (
                    state.has("7-1: GARDEN OF FORKING PATHS", player)
                    or state.has("LAYER 7: VIOLENCE", player)
                ),

            "7-2":
                lambda state: (
                    state.has("7-2: LIGHT UP THE NIGHT", player)
                    or state.has("LAYER 7: VIOLENCE", player)
                ),

            "7-3":
                lambda state: (
                    state.has("7-3: NO SOUND, NO MEMORY", player)
                    or state.has("LAYER 7: VIOLENCE", player)
                ),

            "7-4":
                lambda state: (
                    state.has("7-4: ...LIKE ANTENNAS TO HEAVEN", player)
                    or state.has("LAYER 7: VIOLENCE", player)
                ),

            "0-E":
                lambda state: (
                    state.has("0-E: THIS HEAT, AN EVIL HEAT", player)
                    or state.has("OVERTURE: THE MOUTH OF HELL", player)
                ),

            "1-E":
                lambda state: (
                    state.has("1-E: ...THEN FELL THE ASHES", player)
                    or state.has("LAYER 1: LIMBO", player)
                ),

            "P-1":
                lambda state: (
                    state.has("P-1: SOUL SURVIVOR", player)
                    or state.has("LAYER 3: GLUTTONY", player)
                ),

            "P-2":
                lambda state: (
                    state.has("P-2: WAIT OF THE WORLD", player)
                    or state.has("LAYER 6: HERESY", player)
                ),

            "0-S":
                lambda state: (
                    l2_secret_exit(state)
                    and grab_item(state)
                    and skull(state, "0-2", "Blue")
                ),

            "1-S":
                revany2_fire2,

            "2-S":
                lambda state: (
                    l12_s3(state)
                    and slide(state)
                    and skull(state, "2-3", "Blue")
                ),

            "4-S":
                lambda state: (
                    (
                        slam(state)
                        or walljumps(state, 3)
                        or rock_any(state)
                        or shoany0_fire2(state)
                        or shoany1_fire2(state)
                        or can_proj_boost(state)
                        or rai2(state)
                        or revany1_fire2(state)
                        or (
                            shoalt_any(state)
                            and walljumps(state, 2)
                        )
                    )
                    and grab_item(state)
                ),

            "5-S":
                lambda state: (
                    slide(state)
                    and l20_general(state)
                    and grab_item(state)
                    and skull(state, "5-1", "Blue", 3)
                    and jump_general(state, 1)
                ),

            "7-S":
                lambda state: (
                    good_weapon(state)
                    and can_break_idol(state)
                    and (
                        arm2(state)
                        or walljumps(state, 3)
                        or (
                            walljumps(state, 2)
                            and stamina(state, 1)
                        )
                        or (
                            shoalt0_fire2(state)
                            or shoany1_fire2(state)
                            or rai2(state)
                        )
                    )
                )
        }
        
        location_rules: Dict[str, Callable[[CollectionState], bool]] = {
            # 0-1
            "0-1: Secret #1":
                can_break_glass_or_walls,

            "0-1: Secret #3":
                lambda state: jump_general(state, 1),

            "0-1: Secret #4":
                lambda state: jump_general(state, 1),

            "0-1: Get 5 kills with a single glass panel":
                can_break_glass,

            "0-1: Perfect Rank":
                lambda state: (
                    good_weapon(state)
                    or arm1(state)
                ),

            # 0-2
            "0-2: Secret #3":
                lambda state: (
                    slide(state)
                    and (
                        rock_any(state)
                        or rai2(state)
                        or shoany0_fire2(state)
                        or can_proj_boost(state)
                        or (
                            walljumps(state, 1)
                            and stamina(state, 1)
                        )
                        or walljumps(state, 2)
                        or slam_storage(state)
                    )
                ),

            "0-2: Secret #4":
                slide,

            "0-2: Beat the secret encounter":
                lambda state: (
                    slide(state)
                    and good_weapon(state)
                ),

            "0-2: Perfect Rank":
                good_weapon,

            # 0-S
            "Cleared 0-S":
                lambda state: (
                    skull(state, "0-2", "Blue")
                    and skull(state, "0-S", "Blue")
                    and skull(state, "0-S", "Red")
                ),

            # 0-3
            "0-3: Secret #1":
                slide,

            "0-3: Secret #2":
                lambda state: jump_general(state, 1),

            "0-3: Secret #3":
                lambda state: (
                    can_break_walls(state)
                    or l3_challenge(state)
                ),

            "0-3: Secret #4":
                can_break_glass,

            "0-3: Secret #5":
                can_break_walls,

            "0-3: Weapon":
                good_weapon,

            "Cleared 0-3":
                lambda state: (
                    (
                        can_break_walls(state)
                        or l3_challenge(state)
                    )
                    and good_weapon(state)
                ),

            "0-3: Kill only 1 enemy":
                lambda state: (
                    l3_challenge(state)
                    and good_weapon(state)
                ),

            "0-3: Perfect Rank":
                lambda state: (
                    can_break_walls(state)
                    and good_weapon(state)
                ),

            # 0-4
            "0-4: Secret #1":
                lambda state: jump_general(state, 1),

            "0-4: Secret #2":
                can_break_glass,

            "0-4: Secret #4":
                slide,

            "0-4: Slide uninterrupted for 17 seconds":
                slide,

            "0-4: Perfect Rank":
                good_weapon,

            # 0-5
            "Cleared 0-5":
                l5_general,

            "0-5: Defeat the Cerberi":
                l5_general,

            "0-5: Don't inflict fatal damage to any enemy":
                lambda state: (
                    l5_general(state)
                    and stamina(state, 1)
                ),

            "0-5: Perfect Rank":
                lambda state: (
                    l5_general(state)
                    and good_weapon(state)
                ),

            # 1-1
            "1-1: Weapon":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-1", "Red")
                ),

            "1-1: Secret #3":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-1", "Red")
                ),

            "1-1: Secret #4":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-1", "Red")
                ),

            "1-1: Secret #5":
                lambda state: (
                    grab_item(state)
                    and jump_general(state, 1)
                    and skull(state, "1-1", "Red")
                    and skull(state, "1-1", "Blue")
                ),

            "1-1: Switch":
                lambda state: (
                    l6_switch(state)
                    and grab_item(state)
                    and skull(state, "1-1", "Red")
                    and skull(state, "1-1", "Blue")
                ),

            "1-1: Complete the level in under 10 seconds":
                revany2_fire2,

            "1-1: Perfect Rank":
                lambda state: (
                    good_weapon(state)
                    and grab_item(state)
                    and skull(state, "1-1", "Red")
                    and skull(state, "1-1", "Blue")
                ),

            "Cleared 1-1":
                lambda state: (
                    (
                        grab_item(state)
                        and skull(state, "1-1", "Red")
                        and skull(state, "1-1", "Blue")
                    )
                    or revany2_fire2(state)
                ),

            # 1-2
            "1-2: Do not pick up any skulls":
                lambda state: (
                    can_break_walls(state)
                    and can_zap(state)
                ),

            "1-2: Secret #2":
                can_break_walls,

            "1-2: Secret #3":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-2", "Blue")
                ),

            "1-2: Secret #4":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-2", "Blue")
                    and skull(state, "1-2", "Red")
                    or can_break_walls(state)
                    and can_zap(state)
                ),

            "1-2: Secret #5":
                lambda state: (
                    jump_general(state, 1)
                    and (
                        grab_item(state)
                        and skull(state, "1-2", "Blue")
                        and skull(state, "1-2", "Red")
                        or can_break_walls(state)
                        and can_zap(state)
                    )
                ),

            "Cleared 1-2":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-2", "Blue")
                    and skull(state, "1-2", "Red")
                    or can_break_walls(state)
                    and can_zap(state)
                ),

            "1-2: Switch":
                lambda state: (
                    (
                        grab_item(state)
                        and skull(state, "1-2", "Blue")
                        and skull(state, "1-2", "Red")
                        or can_break_walls(state)
                        and can_zap(state)
                    )
                    and can_break_wall_cancerous_rodent(state)
                ),

            "1-2: Defeat the Very Cancerous Rodent":
                lambda state: (
                    (
                        grab_item(state)
                        and skull(state, "1-2", "Blue")
                        and skull(state, "1-2", "Red")
                        or can_break_walls(state)
                        and can_zap(state)
                    )
                    and can_break_wall_cancerous_rodent(state)
                ),

            "1-2: Perfect Rank":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-2", "Blue")
                    and skull(state, "1-2", "Red")
                    and good_weapon(state)
                ),

            # 1-3
            "1-3: Secret #1":
                can_break_glass,

            "1-3: Secret #4":
                slide,

            "1-3: Secret #5":
                slide,

            "1-3: Switch":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-3", "Red")
                    and skull(state, "1-3", "Blue")
                    and good_weapon(state)
                ),

            "Cleared 1-3":
                lambda state: (
                    grab_item(state)
                    and (
                        skull(state, "1-3", "Red")
                        or skull(state, "1-3", "Blue")
                    )
                ),

            "1-3: Perfect Rank":
                lambda state: (
                    grab_item(state)
                    and (
                        skull(state, "1-3", "Red")
                        or skull(state, "1-3", "Blue")
                    )
                    and good_weapon(state)
                ),

            "1-3: Beat the secret encounter":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-3", "Red")
                    and skull(state, "1-3", "Blue")
                    and good_weapon(state)
                ),

            # 1-4
            "1-4: Secret Weapon":
                lambda state: (
                    (
                        not options.randomize_limbo_switches
                        and state.can_reach(self.world.get_region("1-1: HEART OF THE SUNRISE"), player=player)
                        and state.can_reach(self.world.get_region("1-2: THE BURNING WORLD"), player=player)
                        and state.can_reach(self.world.get_region("1-3: HALLS OF SACRED REMAINS"), player=player)
                        and l6_switch(state)
                        and can_break_glass(state)
                        and can_break_wall_cancerous_rodent(state)
                        and good_weapon(state)
                        and grab_item(state)
                        and skull(state, "1-1", "Red")
                        and skull(state, "1-1", "Blue")
                        and skull(state, "1-3", "Blue")
                        and skull(state, "1-3", "Red")
                        and (
                            skull(state, "1-2", "Blue")
                            and skull(state, "1-2", "Red")
                            or can_zap(state)
                        )
                    )
                    or (
                        options.randomize_limbo_switches
                        and state.has_all({"Limbo Switch I", "Limbo Switch II", "Limbo Switch III", "Limbo Switch IV"}, player)
                    )
                ),

            "1-4: Assemble Hank":
                lambda state: (
                    grab_item(state)
                    and skull(state, "1-4", "Blue")
                ),

            "1-4: Defeat V2":
                good_weapon,

            "1-4: Do not pick up any skulls":
                good_weapon,

            "1-4: Perfect Rank":
                good_weapon,

            # 2-1
            "2-1: Secret #1":
                can_break_walls,

            "2-1: Secret #2":
                l10_exit,

            "2-1: Secret #3":
                lambda state: (
                    l10_exit(state)
                    and (
                        shoalt0_fire2(state)
                        or shoany1_fire2(state)
                        or rai2(state)
                        or rock0_fire2(state)
                    )
                ),

            "2-1: Secret #4":
                l10_exit,

            "2-1: Secret #5":
                l10_tower,

            "Cleared 2-1":
                l10_tower,

            "2-1: Don't open any normal doors":
                l10_challenge,

            "2-1: Perfect Rank":
                lambda state: (
                    l10_tower(state)
                    and good_weapon(state)
                ),

            # 2-2
            "2-2: Secret #1":
                lambda state: (
                    rev_any(state)
                    or sho_any(state)
                    or nai_any(state)
                    or rai0(state)
                    or rai2(state)
                    or rock_any(state)
                    or grab_item(state)
                    or slam(state)
                ),

            "2-2: Secret #2":
                lambda state: jump_general(state, 1),

            "2-2: Secret #3":
                lambda state: jump_general(state, 2),

            "2-2: Secret #4":
                slide,

            "2-2: Secret #5":
                can_break_walls,

            "2-2: Beat the level in under 60 seconds":
                l11_challenge,

            "2-2: Perfect Rank":
                good_weapon,

            # 2-3
            "2-3: Secret #2":
                slide,

            "2-3: Secret #3":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-3", "Blue")
                    and l12_s3(state)
                ),

            "2-3: Secret #4":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-3", "Blue")
                ),

            "2-3: Secret #5":
                slide,

            "Cleared 2-3":
                lambda state: (
                    grab_item(state)
                    and (
                        (
                            l12_s3(state)
                            and skull(state, "2-3", "Blue")
                            and slide
                        )
                        or (
                            skull(state, "2-3", "Blue")
                            and skull(state, "2-3", "Red")
                        )
                    )
                ),

            "2-3: Don't touch any water":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-3", "Blue")
                    and l12_s3(state)
                    and slide(state)
                    and can_break_walls(state)
                ),

            "2-3: Perfect Rank":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-3", "Blue")
                    and skull(state, "2-3", "Red")
                    and good_weapon(state)
                ),

            # 2-4
            "Cleared 2-4":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-4", "Blue")
                    and skull(state, "2-4", "Red")
                ),

            "2-4: Defeat the Corpse of King Minos":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-4", "Blue")
                    and skull(state, "2-4", "Red")
                    and good_weapon(state)
                ),

            "2-4: Parry a punch":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-4", "Blue")
                    and skull(state, "2-4", "Red")
                    and arm0(state)
                ),

            "2-4: Perfect Rank":
                lambda state: (
                    grab_item(state)
                    and skull(state, "2-4", "Blue")
                    and skull(state, "2-4", "Red")
                    and good_weapon(state)
                ),

            # 3-1
            "3-1: Secret #4":
                slide,

            "3-1: Perfect Rank":
                good_weapon,

            # 3-2
            "Cleared 3-2":
                lambda state: (
                    slide(state)
                    or naialt_any(state)
                ),

            "3-2: Defeat Gabriel":
                lambda state: (
                    (
                        slide(state)
                        or naialt_any(state)
                    )
                    and good_weapon(state)
                ),

            "3-2: Drop Gabriel in a pit":
                lambda state: (
                    (
                        slide(state)
                        or naialt_any(state)
                    )
                    and good_weapon(state)
                    and walljumps(state, 1)
                ),

            "3-2: Perfect Rank":
                lambda state: (
                    (
                        slide(state)
                        or naialt_any(state)
                    )
                    and good_weapon(state)
                ),

            # 4-1
            "4-1: Secret #1":
                lambda state: (
                    stamina(state, 1)
                    or rock0_fire2(state)
                ),

            "4-1: Secret #2":
                lambda state: jump_general(state, 1),

            "4-1: Secret #3":
                lambda state: jump_general(state, 1),

            "4-1: Secret #4":
                lambda state: (
                    slam_storage(state)
                    or rock0_fire2(state)
                    or shoalt0_fire2(state)
                    or shoany1_fire2(state)
                    or rai2(state)
                ),

            "4-1: Secret #5":
                lambda state: (
                    (
                        jump_general(state, 1)
                        and can_break_walls(state)
                    )
                    or (
                        slam(state)
                        or shostd0_fire2(state)
                        or shostd1_fire2(state)
                        or shoalt_any(state)
                        or can_proj_boost(state)
                        or rock_any(state)
                        or rai2(state)
                    )
                ),

            "4-1: Don't activate any enemies":
                l16_challenge,

            "4-1: Perfect Rank":
                good_weapon,

            # 4-2
            "4-2: Secret #4":
                lambda state: (
                    jump_general(state, 2)
                    or revany1_fire2(state)
                ),

            "Cleared 4-2":
                lambda state: (
                    grab_item(state)
                    and (
                        jump_general(state, 2)
                        or revany1_fire2(state)
                        or (
                            skull(state, "4-2", "Blue")
                            and skull(state, "4-2", "Red")
                        )
                    )
                ),

            "4-2: Kill the Insurrectionist in under 10 seconds":
                lambda state: (
                    grab_item(state)
                    and skull(state, "4-2", "Blue")
                    and skull(state, "4-2", "Red")
                    and good_weapon(state)
                ),

            "4-2: Perfect Rank":
                lambda state: (
                    grab_item(state)
                    and skull(state, "4-2", "Blue")
                    and skull(state, "4-2", "Red")
                    and good_weapon(state)
                ),

            # 4-3
            "4-3: Don't pick up the torch":
                lambda state: (
                    grab_item(state)
                    and skull(state, "4-3", "Blue")
                    and (
                        shoany0_fire2(state)
                        or can_proj_boost(state)
                    )
                ),

            "4-3: Secret #1":
                l18_general,

            "4-3: Secret #2":
                lambda state: (
                    l18_general(state)
                    and slide(state)
                ),

            "4-3: Secret #3":
                l18_general,

            "4-3: Secret #4":
                lambda state: (
                    l18_general(state)
                    and can_break_walls(state)
                ),
            
            "4-3: Secret #5":
                lambda state: (
                    l18_general(state)
                    and slide(state)
                ),

            "Cleared 4-3":
                lambda state: (
                    l18_general(state)
                    and grab_item(state)
                ),

            "4-3: Defeat the Mysterious Druid Knight (& Owl)":
                lambda state: (
                    l18_general(state)
                    and can_break_walls(state)
                    and can_punch(state)
                    and skull(state, "4-3", "Blue")
                ),

            "4-3: Perfect Rank":
                lambda state: (
                    l18_general(state)
                    and grab_item(state)
                    and good_weapon(state)
                ),

            # 4-4
            "Cleared 4-4":
                lambda state: (
                    arm2(state)
                    and l19_general(state)
                    and good_weapon(state)
                ),

            "4-4: V2's Other Arm":
                lambda state: (
                    l19_general(state)
                    and good_weapon(state)
                ),

            "4-4: Defeat V2":
                lambda state: (
                    l19_general(state)
                    and good_weapon(state)
                ),

            "4-4: Perfect Rank":
                lambda state: (
                    arm2(state)
                    and l19_general(state)
                    and good_weapon(state)
                ),

            "4-4: Reach the boss room in 18 seconds":
                lambda state: (
                    grab_item(state)
                    and skull(state, "4-4", "Blue")
                    and arm2(state)
                    and stamina(state, 3)
                    and good_weapon(state)
                ),

            "4-4: Secret Weapon":
                lambda state: (
                    grab_item(state)
                    and skull(state, "4-4", "Blue")
                    and arm2(state)
                    and can_zap(state)
                ),

            # 5-1
            "5-1: Don't touch any water":
                lambda state: (
                    (
                        arm2(state)
                        or rock0_fire2(state)
                    )
                    and l20_general(state)
                    and grab_item(state)
                    and skull(state, "5-1", "Blue", 3)
                ),

            "5-1: Secret #1":
                l20_general,

            "5-1: Secret #2":
                l20_general,

            "5-1: Secret #3":
                l20_general,

            "5-1: Secret #4":
                l20_general,

            "5-1: Secret #5":
                lambda state: (
                    l20_general(state)
                    and grab_item(state)
                    and skull(state, "5-1", "Blue", 3)
                ),

            "Cleared 5-1":
                lambda state: (
                    l20_general(state)
                    and grab_item(state)
                    and skull(state, "5-1", "Blue", 3)
                ),

            "5-1: Perfect Rank":
                lambda state: (
                    l20_general(state)
                    and grab_item(state)
                    and skull(state, "5-1", "Blue", 3)
                ),

            # 5-2
            "5-2: Secret #1":
                lambda state: (
                    slide(state)
                    or rock0_fire2(state)
                ),

            "5-2: Secret #2":
                lambda state: (
                    slam(state)
                    or stamina(state, 1)
                ),

            "5-2: Secret #3":
                lambda state: (
                    (
                        slam(state)
                        or stamina(state, 1)
                    )
                    and jump_general(state, 2)
                ),

            "5-2: Secret #4":
                lambda state: (
                    (
                        slam(state)
                        or stamina(state, 1)
                    )
                    and jump_general(state, 2)
                ),

            "5-2: Secret #5":
                lambda state: (
                    (
                        slam(state)
                        or stamina(state, 1)
                    )
                    and grab_item(state)
                    and skull(state, "5-2", "Blue")
                    and skull(state, "5-2", "Red")
                    and can_break_idol(state)
                ),

            "Cleared 5-2":
                lambda state: (
                    (
                        slam(state)
                        or stamina(state, 1)
                    )
                    and grab_item(state)
                    and skull(state, "5-2", "Blue")
                    and skull(state, "5-2", "Red")
                    and can_break_idol(state)
                ),

            "5-2: Don't fight the ferryman":
                lambda state: (
                    (
                        slam(state)
                        or stamina(state, 1)
                    )
                    and grab_item(state)
                    and skull(state, "5-2", "Blue")
                    and skull(state, "5-2", "Red")
                    and can_break_idol(state)
                    and revany2_fire2(state)
                ),

            "5-2: Perfect Rank":
                lambda state: (
                    (
                        slam(state)
                        or stamina(state, 1)
                    )
                    and grab_item(state)
                    and skull(state, "5-2", "Blue")
                    and skull(state, "5-2", "Red")
                    and can_break_idol(state)
                    and good_weapon(state)
                ),

            # 5-3
            "5-3: Secret #1":
                lambda state: (
                    grab_item(state)
                    and skull(state, "5-3", "Blue")
                ),

            "5-3: Secret #2":
                lambda state: (
                    #revany1_fire2(state)
                    shoany0_fire2(state)
                    or can_proj_boost(state)
                    or naialt_any(state)
                    or rai2(state)
                    or rock_any(state)
                ),

            "5-3: Secret #3":
                lambda state: (
                    grab_item(state)
                    and skull(state, "5-3", "Blue")
                    and skull(state, "5-3", "Red")
                ),

            "5-3: Weapon":
                lambda state: (
                    grab_item(state)
                    and (
                        skull(state, "5-3", "Blue")
                        or skull(state, "5-3", "Red")
                    )
                    and can_break_idol(state)
                ),

            "5-3: Secret #4":
                lambda state: (
                    grab_item(state)
                    and (
                        skull(state, "5-3", "Blue")
                        or skull(state, "5-3", "Red")
                    )
                    and can_break_idol(state)
                ),

            "5-3: Secret #5":
                lambda state: (
                    grab_item(state)
                    and (
                        skull(state, "5-3", "Blue")
                        or skull(state, "5-3", "Red")
                    )
                    and can_break_idol(state)
                ),

            "5-3: Assemble Hank Jr.":
                lambda state: (
                    grab_item(state)
                    and skull(state, "5-3", "Blue")
                    and skull(state, "5-3", "Red")
                    and can_break_idol(state)
                ),

            "Cleared 5-3":
                lambda state: (
                    grab_item(state)
                    and (
                        skull(state, "5-3", "Blue")
                        or skull(state, "5-3", "Red")
                    )
                    and can_break_idol(state)
                ),

            "5-3: Don't touch any water":
                lambda state: (
                    grab_item(state)
                    and skull(state, "5-3", "Blue")
                    and skull(state, "5-3", "Red")
                    and can_break_idol(state)
                    and slide(state)
                    and stamina(state, 3)
                    and jump_general(state, 2)
                ),

            "5-3: Perfect Rank":
                lambda state: (
                    grab_item(state)
                    and (
                        skull(state, "5-3", "Blue")
                        or skull(state, "5-3", "Red")
                    )
                    and can_break_idol(state)
                    and good_weapon(state)
                ),

            # 5-4
            "5-4: Defeat the Leviathan":
                good_weapon,

            "5-4: Reach the surface in under 10 seconds":
                lambda state: (
                    rock0_fire2(state)
                    and good_weapon(state)
                ),

            "5-4: Perfect Rank":
                lambda state: (
                    rock0_fire2(state)
                    and good_weapon(state)
                ),

            # 6-1
            "6-1: Secret #2":
                lambda state: (
                    grab_item(state)
                    and skull(state, "6-1", "Red")
                ),

            "6-1: Secret #3":
                lambda state: (
                    grab_item(state)
                    and skull(state, "6-1", "Red")
                ),

            "6-1: Secret #4":
                lambda state: (
                    grab_item(state)
                    and skull(state, "6-1", "Red")
                    and jump_general(state, 1)
                ),

            "6-1: Secret #5":
                lambda state: (
                    grab_item(state)
                    and skull(state, "6-1", "Red")
                    and jump_general(state, 2)
                    and can_break_idol(state)
                ),

            "Cleared 6-1":
                lambda state: (
                    grab_item(state)
                    and skull(state, "6-1", "Red")
                    and jump_general(state, 1)
                    and can_break_idol(state)
                ),

            "6-1: Beat the secret encounter":
                lambda state: (
                    grab_item(state)
                    and skull(state, "6-1", "Red")
                    and (
                        shoany0_fire2(state)
                        or shoany1_fire2(state)
                        or can_proj_boost(state)
                        or rai2(state)
                        or rock0_fire2(state)
                    )
                ),

            "6-1: Perfect Rank":
                lambda state: (
                    grab_item(state)
                    and skull(state, "6-1", "Red")
                    and jump_general(state, 1)
                    and can_break_idol(state)
                    and good_weapon(state)
                ),

            # 6-2
            "Cleared 6-2":
                lambda state: (
                    (
                        slam(state)
                        or slam_storage(state)
                        or walljumps(state, 2)
                        or shoalt0_fire2(state)
                        or shoany1_fire2(state)
                        or rai2(state)
                        or rock0_fire2(state)
                    )
                    and good_weapon(state)
                ),

            "6-2: Defeat Gabriel":
                lambda state: (
                    (
                        slam(state)
                        or slam_storage(state)
                        or walljumps(state, 2)
                        or shoalt0_fire2(state)
                        or shoany1_fire2(state)
                        or rai2(state)
                        or rock0_fire2(state)
                    )
                    and good_weapon(state)
                ),

            "6-2: Hit Gabriel into the ceiling":
                lambda state: (
                    (
                        slam(state)
                        or slam_storage(state)
                        or walljumps(state, 2)
                        or shoalt0_fire2(state)
                        or shoany1_fire2(state)
                        or rai2(state)
                        or rock0_fire2(state)
                    )
                    and good_weapon(state)
                    and rock_any(state)
                ),

            "6-2: Perfect Rank":
                lambda state: (
                    (
                        slam(state)
                        or slam_storage(state)
                        or walljumps(state, 2)
                        or shoalt0_fire2(state)
                        or shoany1_fire2(state)
                        or rai2(state)
                        or rock0_fire2(state)
                    )
                    and good_weapon(state)
                ),

            # 7-1
            "7-1: Secret #1":
                lambda state: jump_general(state, 2),

            "7-1: Secret #2":
                lambda state: (
                    jump_general(state, 1)
                    and slide(state)
                ),

            "7-1: Secret #3":
                lambda state: (
                    jump_general(state, 1)
                    and grab_item(state)
                    and skull(state, "7-1", "Red")
                    and skull(state, "7-1", "Blue")
                ),

            "7-1: Secret #4":
                lambda state: (
                    jump_general(state, 2)
                    and grab_item(state)
                    and skull(state, "7-1", "Red")
                    and skull(state, "7-1", "Blue")
                ),

            "7-1: Secret #5":
                lambda state: (
                    jump_general(state, 2)
                    and grab_item(state)
                    and skull(state, "7-1", "Red")
                    and skull(state, "7-1", "Blue")
                ),

            "Cleared 7-1":
                lambda state: (
                    jump_general(state, 1)
                    and grab_item(state)
                    and skull(state, "7-1", "Red")
                    and skull(state, "7-1", "Blue")
                ),

            "7-1: Beat the secret encounter":
                lambda state: (
                    grab_item(state)
                    and skull(state, "7-1", "Red")
                    and skull(state, "7-1", "Blue")
                ),

            "7-1: Perfect Rank":
                lambda state: (
                    jump_general(state, 2)
                    and grab_item(state)
                    and skull(state, "7-1", "Red")
                    and skull(state, "7-1", "Blue")
                    and good_weapon(state)
                ),

            # 7-2
            "7-2: Secret #1":
                lambda state: (
                    arm2(state)
                    and (
                        walljumps(state, 1)
                        or stamina (state, 1)
                        or rock0_fire2(state)
                    )
                ),

            "7-2: Secret #2":
                arm2,

            "7-2: Secret #3":
                arm2,

            "7-2: Secret #4":
                arm2,

            "7-2: Secret #5":
                arm2,

            "7-2: Secret Weapon":
                lambda state: (
                    (
                        not options.randomize_violence_switches
                        and arm2(state)
                        and stamina(state, 1)
                        and grab_item(state)
                        and skull(state, "7-2", "Red")
                    )
                    or (
                        options.randomize_violence_switches
                        and arm2(state)
                        and state.has_all({"Violence Switch I", "Violence Switch II", "Violence Switch III"}, player)
                    )
                ),

            "7-2: Switch #1":
                lambda state: (
                    arm2(state)
                    and stamina(state, 1)
                ),

            "7-2: Switch #2":
                lambda state: (
                    arm2(state)
                    and stamina(state, 1)
                ),

            "7-2: Switch #3":
                lambda state: (
                    arm2(state)
                    and stamina(state, 1)
                    and grab_item(state)
                    and skull(state, "7-2", "Red")
                ),

            "Cleared 7-2":
                lambda state: (
                    arm2(state)
                    and grab_item(state)
                    and skull(state, "7-2", "Red")
                ),

            "7-2: Don't kill any enemies":
                lambda state: (
                    arm2(state)
                    and can_break_walls(state)
                    and grab_item(state)
                    and skull(state, "7-2", "Red")
                ),

            "7-2: Perfect Rank":
                lambda state: (
                    arm2(state)
                    and good_weapon(state)
                    and grab_item(state)
                    and skull(state, "7-2", "Red")
                ),

            # 7-3
            "7-3: Perfect Rank":
                good_weapon,

            # 7-S
            "Cleared 7-S":
                lambda state: (
                    grab_item(state)
                    and skull(state, "7-S", "Red")
                    and skull(state, "7-S", "Blue")
                ),

            "7-S: Cleaned Courtyard":
                lambda state: (
                    grab_item(state)
                    and skull(state, "7-S", "Red")
                    and skull(state, "7-S", "Blue")
                ),

            "7-S: Cleaned Library":
                lambda state: (
                    grab_item(state)
                    and skull(state, "7-S", "Red")
                    and skull(state, "7-S", "Blue")
                ),

            "7-S: Cleaned Lobby":
                lambda state: (
                    grab_item(state)
                    and skull(state, "7-S", "Red")
                    and skull(state, "7-S", "Blue")
                ),

            "7-S: Cleaned Lounge":
                lambda state: (
                    grab_item(state)
                    and skull(state, "7-S", "Red")
                    and skull(state, "7-S", "Blue")
                ),

            "7-S: Cleaned Side Room":
                lambda state: (
                    grab_item(state)
                    and skull(state, "7-S", "Red")
                    and skull(state, "7-S", "Blue")
                ),

            # 7-4
            "Cleared 7-4":
                lambda state: (
                    slide(state)
                    and (
                        arm2(state)
                        or slam_storage(state)
                    )
                    and can_break_idol(state)
                    and good_weapon(state)
                ),

            '7-4: Defeat 1000-THR "Earthmover"':
                lambda state: (
                    slide(state)
                    and (
                        arm2(state)
                        or slam_storage(state)
                    )
                    and can_break_idol(state)
                    and good_weapon(state)
                ),

            "7-4: Don't fight the security system":
                lambda state: (
                    slide(state)
                    and (
                        arm2(state)
                        or slam_storage(state)
                    )
                    and can_break_idol(state)
                    and good_weapon(state)
                    and can_zap(state)
                ),

            "7-4: Perfect Rank":
                lambda state: (
                    slide(state)
                    and arm2(state)
                    and can_break_idol(state)
                    and good_weapon(state)
                ),

            # Encores
            "Cleared 0-E":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and arm2(state)
                    and has_weapon_types(state, 3)
                    and can_break_glass(state)
                    and slide(state)
                    and stamina(state, 2)
                    and skull(state, "0-E", "Blue")
                    and skull(state, "0-E", "Red")
                ),

            "0-E: Perfect Rank":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and arm2(state)
                    and has_weapon_types(state, 3)
                    and can_break_glass(state)
                    and slide(state)
                    and stamina(state, 3)
                    and skull(state, "0-E", "Blue")
                    and skull(state, "0-E", "Red")
                ),

            "Cleared 1-E":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and arm2(state)
                    and has_weapon_types(state, 3)
                    and can_break_walls(state)
                    and slide(state)
                    and stamina(state, 2)
                    and skull(state, "1-E", "Blue")
                    and skull(state, "1-E", "Red")
                ),

            "1-E: Perfect Rank":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and arm2(state)
                    and has_weapon_types(state, 3)
                    and can_break_walls(state)
                    and slide(state)
                    and stamina(state, 3)
                    and skull(state, "1-E", "Blue")
                    and skull(state, "1-E", "Red")
                ),

            # Primes
            "Cleared P-1":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and has_weapon_types(state, 2)
                    and slide(state)
                    and stamina(state, 2)
                ),

            "P-1: Perfect Rank":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and has_weapon_types(state, 2)
                    and slide(state)
                    and stamina(state, 3)
                ),

            "Cleared P-2":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and arm2(state)
                    and has_weapon_types(state, 3)
                    and slide(state)
                    and stamina(state, 2)
                    and skull(state, "P-2", "Blue")
                ),

            "P-2: Perfect Rank":
                lambda state: (
                    arm0(state)
                    and arm1(state)
                    and arm2(state)
                    and has_weapon_types(state, 3)
                    and slide(state)
                    and stamina(state, 3)
                    and skull(state, "P-2", "Blue")
                ),

            # Shop
            "Shop: Buy Revolver Variant 1":
                rev_any,

            "Shop: Buy Revolver Variant 2":
                rev_any,

            "Shop: Buy Shotgun Variant 1":
                sho_any,

            "Shop: Buy Shotgun Variant 2":
                sho_any,

            "Shop: Buy Nailgun Variant 1":
                nai_any,

            "Shop: Buy Nailgun Variant 2":
                nai_any,

            "Shop: Buy Railcannon Variant 1":
                rai_any,

            "Shop: Buy Railcannon Variant 2":
                rai_any,

            "Shop: Buy Rocket Launcher Variant 1":
                rock_any,

            "Shop: Buy Rocket Launcher Variant 2":
                rock_any,

            # Museum
            "Museum: Win rocket race":
                rock0_fire2
        }

        def add_entrance_rule(level_name: str, rule: Callable[[CollectionState], bool], combine: str = "and") -> None:
            if level_name == self.world.start_level.short_name:
                if DEBUG:
                    print(f"[P{player} - {self.world.player_name}] "
                          f"Start is {level_name}, skipping entrance rule")
            elif level_name == self.world.goal_level.short_name:
                if DEBUG:
                    print(f"[P{player} - {self.world.player_name}] "
                          f"Goal is {level_name}, skipping entrance rule")
            else:
                try:
                    level = Regions.get_from_short_name(level_name)
                    if isinstance(level, SecretRegion):
                        add_rule(self.world.get_entrance(f"{level.parent_level.full_name} -> {level.full_name}"), rule, combine)
                    else:
                        add_rule(self.world.get_entrance(f"Menu -> {level.full_name}"), rule, combine)
                except KeyError as e:
                    raise KeyError(f"No entrance found for level {level_name}.\n{e}")

        def add_location_rule(loc_name: str, rule: Callable[[CollectionState], bool], combine: str = "and") -> None:
            if loc_name not in self.world.location_names and loc_name not in self.world.event_names:
                raise KeyError(f"\"{loc_name}\" is not a valid location name.")
            try:
                add_rule(self.world.get_location(loc_name), rule, combine)
            except KeyError:
                if DEBUG:
                    print(f"[P{player} - {self.world.player_name}] "
                          f"No location found for name \"{loc_name}\".")

        if isinstance(self.world.goal_level, SecretRegion):
            add_rule(self.world.get_entrance(f"{self.world.goal_level.parent_level.full_name} -> {self.world.goal_level.full_name}"), \
                lambda state: state.has("Level Completed", player, options.goal_requirement.value))
        else:
            add_rule(self.world.get_entrance(f"Menu -> {self.world.goal_level.full_name}"), \
                lambda state: state.has("Level Completed", player, options.goal_requirement.value))
                    
        for level, rule in level_rules.items():
            add_entrance_rule(level, rule)

        for location, rule in location_rules.items():
            add_location_rule(location, rule)
