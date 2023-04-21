from worlds.generic.Rules import set_rule, add_rule
from ..AutoWorld import LogicMixin


class UltrakillLogic(LogicMixin):
    def _ultrakill_good_weapon(self, player):
        return self.has_any({"Revolver - Piercer", "Revolver - Marksman", "Shotgun - Core Eject", \
            "Shotgun - Pump Charge"}, player)
    
    def _ultrakill_good_weapon_fire2_noarm(self, player):
        return self.has_any({"Revolver - Piercer", "Revolver - Marksman"}, player) or \
            (self.has("Shotgun - Core Eject", player) and \
                self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                    (self.has("Shotgun - Pump Charge", player) and \
                        self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player))

    def _ultrakill_break_glass(self, player):
        return self.has_any({"Revolver - Piercer", "Revolver - Marksman", "Shotgun - Core Eject", \
            "Shotgun - Pump Charge", "Railcannon - Electric", "Railcannon - Malicious", \
                "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player)
    
    def _ultrakill_break_glass_fire2_noarm(self, player):
        return self.has_any({"Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
            "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                (self.has("Revolver - Piercer", player) and \
                    self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                        (self.has("Revolver - Marksman", player) and \
                            self.has_any({"Revolver - Alternate", "Secondary Fire - Marksman"}, player)) or \
                                (self.has("Shotgun - Core Eject", player) and \
                                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                        (self.has("Shotgun - Pump Charge", player) and \
                                            self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player))
    
    def _ultrakill_break_glass_fire2_yesarm(self, player):
        return self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Electric", \
            "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                "Knuckleblaster"}, player) or \
                    (self.has("Revolver - Piercer", player) and \
                        self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                            (self.has("Revolver - Marksman", player) and \
                                self.has_any({"Revolver - Alternate", "Secondary Fire - Marksman"}, player))
    
    def _ultrakill_0_1c(self, player):
        return self.has_any({"Revolver - Piercer", "Shotgun - Core Eject", "Shotgun - Pump Charge", \
            "Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                    self.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player)
    
    def _ultrakill_0_1c_fire2_noarm(self, player):
        return self.has_any({"Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
            "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                (self.has("Revolver - Piercer", player) and \
                    self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                        self.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player) or \
                            (self.has("Shotgun - Core Eject", player) and \
                                self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                    self.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)
    
    def _ultrakill_0_1c_fire2_yesarm(self, player):
        return self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Electric", \
            "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon" \
                "Knuckleblaster"}, player) or \
                    (self.has("Revolver - Piercer", player) and \
                        self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                            self.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player)
    
    def _ultrakill_break_walls(self, player):
        return self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Nailgun - Overheat", \
            "Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                    (self.has_any({"Revolver - Piercer", "Revolver - Marksman"}, player) and \
                        self.has("Revolver - Alternate", player))
    
    def _ultrakill_break_walls_fire2_noarm(self, player):
        return self.has_any({"Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
            "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                self.has_all({"Nailgun - Overheat", "Secondary Fire - Overheat"}, player) or \
                    (self.has("Shotgun - Core Eject", player) and \
                        self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                            (self.has("Shotgun - Pump Charge", player) and \
                                self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                                    (self.has_any({"Revolver - Piercer", "Revolver - Marksman"}, player) and \
                                        self.has("Revolver - Alternate", player))
    
    def _ultrakill_break_walls_fire2_yesarm(self, player):
        return self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Electric", \
            "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                "Knuckleblaster"}, player) or \
                    self.has_all({"Nailgun - Overheat", "Secondary Fire - Overheat"}, player) or \
                        (self.has_any({"Revolver - Piercer", "Revolver - Marksman"}, player) and \
                            self.has("Revolver - Alternate", player))
        
    def _ultrakill_jump_noslam_nofire2(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps) or \
            self.has_any({"Slam", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)
    
    def _ultrakill_jump_noslam_yesfire2_noarm(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps) or \
            self.has("Slam", player) or \
                self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                    self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                        self.has_all({"Shotgun - Core Eject", "Secondary Fire - Core Eject"}, player) or \
                            (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                                self.has("Feedbacker", player)) or \
                                    self.has("Railcannon - Malicious", player)
    
    def _ultrakill_0_2_jump_noslam_nofire2(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps) or \
            (self.has("Wall Jump", player, walljumps-1) and \
                self.has("Slam", player)) or \
                    self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                        "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)

    def _ultrakill_0_2_jump_yesslam_nofire2(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps) or \
            self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Shotgun - Core Eject", \
                "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)
    
    def _ultrakill_0_2_jump_noslam_yesfire2_noarm(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps) or \
            (self.has("Wall Jump", player, walljumps-1) and \
                self.has("Slam", player)) or \
                    (self.has("Shotgun - Core Eject", player) and \
                        self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                            (self.has("Shotgun - Pump Charge", player) and \
                                self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                                    self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                                        "Railcannon - Malicious"}, player)
    
    def _ultrakill_0_2_jump_yesslam_yesfire2_noarm(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps) or \
            (self.has("Shotgun - Core Eject", player) and \
                self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                    (self.has("Shotgun - Pump Charge", player) and \
                        self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                            self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                                "Railcannon - Malicious"}, player)
    
    def _ultrakill_0_3c_noslam_nofire2(self, player, walljumps):
        return (self.has("Slam", player) and \
            self.has("Wall Jump", player, walljumps-2)) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has("Wall Jump", player, walljumps)) or \
                        self.has_any({"Shotgun - Pump Charge", "Railcannon - Malicious", \
                            "Rocket Launcher - Freezeframe"}, player)
    
    def _ultrakill_0_3c_yesslam_nofire2(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps-2) or \
            (self.has("Shotgun - Core Eject", player) and \
                self.has("Wall Jump", player, walljumps)) or \
                    self.has_any({"Shotgun - Pump Charge", "Railcannon - Malicious", \
                        "Rocket Launcher - Freezeframe"}, player)
    
    def _ultrakill_0_3c_noslam_yesfire2_noarm(self, player, walljumps):
        return (self.has("Slam", player) and \
            self.has("Wall Jump", player, walljumps-2)) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has("Wall Jump", player, walljumps) and \
                        self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                            (self.has("Shotgun - Pump Charge", player) and \
                                self.has("Wall Jump", player, walljumps) and \
                                    self.has("Feedbacker", player)) or \
                                        self.has_all({"Shotgun - Pump Charge", \
                                            "Secondary Fire - Pump Charge", "Feedbacker"}, player) or \
                                                self.has_all({"Rocket Launcher - Freezeframe", \
                                                    "Secondary Fire - Freezeframe"}, player) or \
                                                        self.has("Railcannon - Malicious", player)
    
    def _ultrakill_0_3c_noslam_yesfire2_yesarm(self, player, walljumps):
        return (self.has("Slam", player) and \
            self.has("Wall Jump", player, walljumps-2)) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has("Wall Jump", player, walljumps)) or \
                        (self.has("Shotgun - Pump Charge", player) and \
                            self.has("Wall Jump", player, walljumps)) or \
                                self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                    self.has_all({"Rocket Launcher - Freezeframe", \
                                        "Secondary Fire - Freezeframe"}, player) or \
                                            self.has("Railcannon - Malicious", player)
    
    def _ultrakill_0_3c_yesslam_yesfire2_noarm(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps-2) or \
            (self.has("Shotgun - Core Eject", player) and \
                self.has("Wall Jump", player, walljumps) and \
                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                        (self.has("Shotgun - Pump Charge", player) and \
                            ((self.has("Wall Jump", player, walljumps) and \
                                self.has("Feedbacker", player)) or \
                                    self.has("Secondary Fire - Pump Charge", player))) or \
                                        self.has_all({"Rocket Launcher - Freezeframe", \
                                            "Secondary Fire - Freezeframe"}, player) or \
                                                self.has("Railcannon - Malicious", player)
    
    def _ultrakill_0_3c_yesslam_yesfire2_yesarm(self, player, walljumps):
        return self.has("Wall Jump", player, walljumps-2) or \
            (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                self.has("Wall Jump", player, walljumps)) or \
                    self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                        self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                            self.has("Railcannon - Malicious", player)
    
    def _ultrakill_1_1_jump_noslam_nofire2(self, player):
        return self.has_any({"Slam", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
            "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)
    
    def _ultrakill_1_1_jump_noslam_yesfire2_noarm(self, player):
        return self.has("Slam", player) or \
            self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                    self.has_all({"Shotgun - Core Eject", "Secondary Fire - Core Eject"}, player) or \
                        (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                            self.has("Feedbacker", player)) or \
                                self.has("Railcannon - Malicious", player)
    
    def _ultrakill_2_3_jump_noslam_nofire2(self, player, dashes, walljumps):
        return self.has_any({"Slam", "Shotgun - Pump Charge", "Railcannon - Malicious", \
            "Rocket Launcher - Freezeframe"}, player) or \
                (self.has("Wall Jump", player, walljumps) or \
                    (self.has("Wall Jump", player, walljumps-1) and \
                        self.has("Stamina Bar", player, dashes)))
    
    def _ultrakill_2_3_jump_noslam_yesfire2(self, player, dashes, walljumps):
        return self.has_any({"Slam", "Railcannon - Malicious"}, player) or \
            (self.has("Wall Jump", player, walljumps) or \
                (self.has("Wall Jump", player, walljumps-1) and \
                    self.has("Stamina Bar", player, dashes))) or \
                        self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                            self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player)

    def _ultrakill_3_2_jump_noslam_nofire2(self, player, dashes, walljumps):
        return self.has_any({"Slam", "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious", \
            "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                self.has("Wall Jump", player, walljumps) or \
                    self.has("Stamina Bar", player, dashes)
    
    def _ultrakill_3_2_jump_noslam_yesfire2_noarm(self, player, dashes, walljumps):
        return self.has_any({"Slam", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
            "Rocket Launcher - S.R.S. Cannon"}, player) or \
                self.has("Wall Jump", player, walljumps) or \
                    self.has("Stamina Bar", player, dashes) or \
                        (self.has("Shotgun - Core Eject", player) and \
                            self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                (self.has("Shotgun - Pump Charge", player) and \
                                    self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player))
    
    def _ultrakill_4_1c_noslam_nofire2(self, player, dashes, walljumps):
        return self.has("Rocket Launcher - Freezeframe") or \
            (self.has("Wall Jump", player, walljumps) and \
                self.has("Stamina Bar", player, dashes) and \
                    self.has("Slam", player))
    
    def _ultrakill_4_1c_noslam_yesfire2(self, player, dashes, walljumps):
        return self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
            (self.has("Wall Jump", player, walljumps) and \
                self.has("Stamina Bar", player, dashes) and \
                    self.has("Slam", player))
    
    def _ultrakill_4_1c_yesslam_nofire2(self, player, dashes, walljumps):
        return self.has("Rocket Launcher - Freezeframe") or \
            (self.has("Wall Jump", player, walljumps) and \
                self.has("Stamina Bar", player, dashes))
    
    def _ultrakill_4_1c_yesslam_yesfire2(self, player, dashes, walljumps):
        return self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
            (self.has("Wall Jump", player, walljumps) and \
                self.has("Stamina Bar", player, dashes))
    
    def _ultrakill_4_3_nofire2_noarm(self, player):
        return self.has_any({"Feedbacker", "Knuckleblaster", "Shotgun - Core Eject", \
            "Railcannon - Malicious"}, player) or \
                (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                    self.has("Feedbacker", player))

    def _ultrakill_4_3_yesfire2_noarm(self, player):
        return self.has_any({"Feedbacker", "Knuckleblaster", "Railcannon - Malicious"}, player) or \
            (self.has("Shotgun - Core Eject", player) and \
                self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                    self.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)
    

def rules(ultrakillworld):
    world = ultrakillworld.multiworld
    player = ultrakillworld.player

    dash1: int = 1 - world.starting_stamina[player].value
    dash2: int = 2 - world.starting_stamina[player].value
    dash3: int = 3 - world.starting_stamina[player].value
    walljump1: int = 1 - world.starting_walljumps[player].value
    walljump2: int = 2 - world.starting_walljumps[player].value
    walljump3: int = 3 - world.starting_walljumps[player].value

    # goal
    set_rule(world.get_entrance("To " + ultrakillworld.goal_name, player), \
        lambda state: state.has("Level Completed", player, \
            world.goal_requirement[player].value))


    # level entrances
    set_rule(world.get_entrance("To shop", player),
        lambda state: state.has_group("levels", player) or \
        state.has_group("layers", player))
    set_rule(world.get_entrance("To 0-2", player),
        lambda state: state.has("0-2: THE MEATGRINDER", player) or \
        state.has("OVERTURE: THE MOUTH OF HELL", player))
    set_rule(world.get_entrance("To 0-3", player),
        lambda state: state.has("0-3: DOUBLE DOWN", player) or \
            state.has("OVERTURE: THE MOUTH OF HELL", player))
    set_rule(world.get_entrance("To 0-4", player),
        lambda state: state.has("0-4: A ONE-MACHINE ARMY", player) or \
            state.has("OVERTURE: THE MOUTH OF HELL", player))
    set_rule(world.get_entrance("To 0-5", player),
        lambda state: state.has("0-5: CERBERUS", player) or \
            state.has("OVERTURE: THE MOUTH OF HELL", player))
    set_rule(world.get_entrance("To 1-1", player),
        lambda state: state.has("1-1: HEART OF THE SUNRISE", player) or \
            state.has("LAYER 1: LIMBO", player))
    set_rule(world.get_entrance("To 1-2", player),
        lambda state: state.has("1-2: THE BURNING WORLD", player) or \
            state.has("LAYER 1: LIMBO", player))
    set_rule(world.get_entrance("To 1-3", player),
        lambda state: state.has("1-3: HALLS OF SACRED REMAINS", player) or \
            state.has("LAYER 1: LIMBO", player))
    if world.goal[player].value != 0:
        set_rule(world.get_entrance("To 1-4", player),
            lambda state: state.has("1-4: CLAIR DE LUNE", player) or \
                state.has("LAYER 1: LIMBO", player))
    set_rule(world.get_entrance("To 2-1", player),
        lambda state: state.has("2-1: BRIDGEBURNER", player) or \
            state.has("LAYER 2: LUST", player))
    set_rule(world.get_entrance("To 2-2", player),
        lambda state: state.has("2-2: DEATH AT 20,000 VOLTS", player) or \
            state.has("LAYER 2: LUST", player))
    set_rule(world.get_entrance("To 2-3", player),
        lambda state: state.has("2-3: SHEER HEART ATTACK", player) or \
            state.has("LAYER 2: LUST", player))
    if world.goal[player].value != 1:
        set_rule(world.get_entrance("To 2-4", player),
            lambda state: state.has("2-4: COURT OF THE CORPSE KING", player) or \
                state.has("LAYER 2: LUST", player))
    set_rule(world.get_entrance("To 3-1", player),
        lambda state: state.has("3-1: BELLY OF THE BEAST", player) or \
            state.has("LAYER 3: GLUTTONY", player))
    if world.goal[player].value != 2:
        set_rule(world.get_entrance("To 3-2", player),
            lambda state: state.has("3-2: IN THE FLESH", player) or \
                state.has("LAYER 3: GLUTTONY", player))
    set_rule(world.get_entrance("To 4-1", player),
        lambda state: state.has("4-1: SLAVES TO POWER", player) or \
            state.has("LAYER 4: GREED", player))
    set_rule(world.get_entrance("To 4-2", player),
        lambda state: state.has("4-2: GOD DAMN THE SUN", player) or \
            state.has("LAYER 4: GREED", player))
    set_rule(world.get_entrance("To 4-3", player),
        lambda state: state.has("4-3: A SHOT IN THE DARK", player) or \
            state.has("LAYER 4: GREED", player))
    if world.goal[player].value != 3:
        set_rule(world.get_entrance("To 4-4", player),
            lambda state: state.has("4-4: CLAIR DE SOLEIL", player) or \
                state.has("LAYER 4: GREED", player))
    set_rule(world.get_entrance("To 5-1", player),
        lambda state: state.has("5-1: IN THE WAKE OF POSEIDON", player) or \
            state.has("LAYER 5: WRATH", player))
    set_rule(world.get_entrance("To 5-2", player),
        lambda state: state.has("5-2: WAVES OF THE STARLESS SEA", player) or \
            state.has("LAYER 5: WRATH", player))
    set_rule(world.get_entrance("To 5-3", player),
        lambda state: state.has("5-3: SHIP OF FOOLS", player) or \
            state.has("LAYER 5: WRATH", player))
    if world.goal[player].value != 4:
        set_rule(world.get_entrance("To 5-4", player),
            lambda state: state.has("5-4: LEVIATHAN", player) or \
                state.has("LAYER 5: WRATH", player))
    set_rule(world.get_entrance("To 6-1", player),
        lambda state: state.has("6-1: CRY FOR THE WEEPER", player) or \
            state.has("LAYER 6: HERESY", player))
    if world.goal[player].value != 5:
        set_rule(world.get_entrance("To 6-2", player),
            lambda state: state.has("6-2: AESTHETICS OF HATE", player) or \
                state.has("LAYER 6: HERESY", player))


    # 0-1
    if world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("0-1: Secret #1", player),
                lambda state: state._ultrakill_break_glass_fire2_noarm(player) or \
                    state._ultrakill_break_walls_fire2_noarm(player))
        else:
            set_rule(world.get_location("0-1: Secret #1", player),
                lambda state: state._ultrakill_break_glass_fire2_yesarm(player) or \
                    state._ultrakill_break_walls_fire2_yesarm(player))
    else: 
        set_rule(world.get_location("0-1: Secret #1", player),
            lambda state: state._ultrakill_break_glass(player) or \
                state._ultrakill_break_walls(player))
        
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("0-1: Secret #3", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        set_rule(world.get_location("0-1: Secret #4", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("0-1: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
            set_rule(world.get_location("0-1: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
        else:
            set_rule(world.get_location("0-1: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
            set_rule(world.get_location("0-1: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))

        
    if world.challenge_rewards[player]:
        if world.randomize_secondary_fire[player]:
            if not world.start_with_arm[player]:
                set_rule(world.get_location("0-1: Get 5 kills with a single glass panel", player),
                    lambda state: state._ultrakill_0_1c_fire2_noarm(player))
            else:
                set_rule(world.get_location("0-1: Get 5 kills with a single glass panel", player),
                    lambda state: state._ultrakill_0_1c_fire2_yesarm(player))
        else:
            set_rule(world.get_location("0-1: Get 5 kills with a single glass panel", player),
                lambda state: state._ultrakill_0_1c(player))

    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            set_rule(world.get_location("0-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player) or \
                    state.has("Knuckleblaster", player))
        else:
            set_rule(world.get_location("0-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player) or \
                    state.has("Knuckleblaster", player))


    # 0-2
    if world.randomize_secondary_fire[player]:
        set_rule(world.get_location("0-2: Secret #3", player),
            lambda state: state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                (state.has("Wall Jump", player, walljump1) and \
                    state.has("Stamina Bar", player, dash2)))
        
    if not world.start_with_slide[player]:
        set_rule(world.get_location("0-2: Secret #4", player),
            lambda state: state.has("Slide", player))
        if world.challenge_rewards[player]:
            set_rule(world.get_location("0-2: Beat the secret encounter", player),
                lambda state: state.has("Slide", player))

    if world.challenge_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("0-2: Beat the secret encounter", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("0-2: Beat the secret encounter", player),
                lambda state: state._ultrakill_good_weapon(player))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("0-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("0-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
            
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("Cleared 0-S", player),
            lambda state: state._ultrakill_0_2_jump_noslam_nofire2(player, walljump2))
    elif world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("Cleared 0-S", player),
            lambda state: state._ultrakill_0_2_jump_yesslam_nofire2(player, walljump1))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("Cleared 0-S", player),
                lambda state: state._ultrakill_0_2_jump_noslam_yesfire2_noarm(player, walljump2))
        else:
            set_rule(world.get_location("Cleared 0-S", player),
                lambda state: state._ultrakill_0_2_jump_noslam_nofire2(player, walljump2))
    elif world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("Cleared 0-S", player),
                lambda state: state._ultrakill_0_2_jump_yesslam_yesfire2_noarm(player, walljump1))
        else:
            set_rule(world.get_location("Cleared 0-S", player),
                lambda state: state._ultrakill_0_2_jump_noslam_nofire2(player, walljump2))
        
    if world.randomize_skulls[player]:
        add_rule(world.get_location("Cleared 0-S", player),
            lambda state: state.has_all({"Blue Skull (0-2)", "Blue Skull (0-S)", "Red Skull (0-S)"}, player))
         
    if not world.start_with_arm[player]:
        add_rule(world.get_location("Cleared 0-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))


    # 0-3
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("0-3: Secret #1", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        set_rule(world.get_location("0-3: Secret #2", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("0-3: Secret #1", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
            set_rule(world.get_location("0-3: Secret #2", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
        else:
            set_rule(world.get_location("0-3: Secret #1", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
            set_rule(world.get_location("0-3: Secret #2", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
        
    if world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("0-3: Secret #3", player),
                lambda state: state._ultrakill_break_walls_fire2_noarm(player))
            set_rule(world.get_location("Cleared 0-3", player),
                lambda state: state._ultrakill_break_walls_fire2_noarm(player))
            set_rule(world.get_location("0-3: Weapon", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
            if world.p_rank_rewards[player]:
                set_rule(world.get_location("0-3: Perfect Rank", player),
                    lambda state: state._ultrakill_break_walls_fire2_noarm(player))
        else:
            set_rule(world.get_location("0-3: Secret #3", player),
                lambda state: state._ultrakill_break_walls_fire2_yesarm(player))
            set_rule(world.get_location("Cleared 0-3", player),
                lambda state: state._ultrakill_break_walls_fire2_yesarm(player))
            set_rule(world.get_location("0-3: Weapon", player),
                lambda state: state._ultrakill_good_weapon(player))
            if world.p_rank_rewards[player]:
                set_rule(world.get_location("0-3: Perfect Rank", player),
                    lambda state: state._ultrakill_break_walls_fire2_yesarm(player))
    else:
        set_rule(world.get_location("0-3: Secret #3", player),
            lambda state: state._ultrakill_break_walls(player))
        set_rule(world.get_location("Cleared 0-3", player),
            lambda state: state._ultrakill_break_walls(player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("0-3: Perfect Rank", player),
                lambda state: state._ultrakill_break_walls(player))

    if world.challenge_rewards[player]:
        if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
            set_rule(world.get_location("0-3: Kill only 1 enemy", player),
                lambda state: state._ultrakill_0_3c_noslam_nofire2(player, walljump3))
        elif world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
            set_rule(world.get_location("0-3: Kill only 1 enemy", player),
                lambda state: state._ultrakill_0_3c_yesslam_nofire2(player, walljump3))
        elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
            if not world.start_with_arm[player]:
                set_rule(world.get_location("0-3: Kill only 1 enemy", player),
                    lambda state: state._ultrakill_0_3c_noslam_yesfire2_noarm(player, walljump3))
            else:
                set_rule(world.get_location("0-3: Kill only 1 enemy", player),
                    lambda state: state._ultrakill_0_3c_noslam_yesfire2_yesarm(player, walljump3))
        elif world.start_with_slam[player] and world.randomize_secondary_fire[player]:
            if not world.start_with_arm[player]:
                set_rule(world.get_location("0-3: Kill only 1 enemy", player),
                    lambda state: state._ultrakill_0_3c_yesslam_yesfire2_noarm(player, walljump3))
            else:
                set_rule(world.get_location("0-3: Kill only 1 enemy", player),
                    lambda state: state._ultrakill_0_3c_yesslam_yesfire2_yesarm(player, walljump3))
                
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("0-3: Kill only 1 enemy", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("0-3: Kill only 1 enemy", player),
                lambda state: state._ultrakill_good_weapon(player))

    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("0-3: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("0-3: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))

    # 0-4
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("0-4: Secret #1", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("0-4: Secret #1", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
        else:
            set_rule(world.get_location("0-4: Secret #1", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        
    if world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("0-4: Secret #2", player),
                lambda state: state._ultrakill_break_glass_fire2_noarm(player))
        else:
            set_rule(world.get_location("0-4: Secret #2", player),
                lambda state: state._ultrakill_break_glass_fire2_yesarm(player))
    else:
        set_rule(world.get_location("0-4: Secret #2", player),
            lambda state: state._ultrakill_break_glass(player))
        
    if not world.start_with_slide[player]:
        set_rule(world.get_location("0-4: Secret #3", player),
            lambda state: state.has("Slide", player))
        if world.challenge_rewards[player]:
            set_rule(world.get_location("0-4: Slide uninterrupted for 17 seconds", player),
                lambda state: state.has("Slide", player))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            set_rule(world.get_location("0-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            set_rule(world.get_location("0-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 0-5
    if not world.start_with_slide[player]:
        if not world.randomize_secondary_fire[player]:
            set_rule(world.get_location("Cleared 0-5", player),
                lambda state: (state.has("Slide", player) and \
                    (state.has("Wall Jump", player, walljump2) or \
                        state.has("Stamina Bar", player, dash2))) or \
                            state.has_any({"Rocket Launcher - Freezeframe", "Railcannon - Malicious", \
                                "Shotgun - Pump Charge"}, player))
            if world.challenge_rewards[player]:
                set_rule(world.get_location("0-5: Don't inflict fatal damage to any enemy", player),
                    lambda state: (state.has("Slide", player) and \
                    (state.has("Wall Jump", player, walljump2) or \
                        state.has("Stamina Bar", player, dash2))) or \
                            state.has_any({"Rocket Launcher - Freezeframe", "Railcannon - Malicious", \
                                "Shotgun - Pump Charge"}, player))
            if world.p_rank_rewards[player]:
                set_rule(world.get_location("0-5: Perfect Rank", player),
                    lambda state: (state.has("Slide", player) and \
                    (state.has("Wall Jump", player, walljump2) or \
                        state.has("Stamina Bar", player, dash2))) or \
                            state.has_any({"Rocket Launcher - Freezeframe", "Railcannon - Malicious", \
                                "Shotgun - Pump Charge"}, player))
        else:
            set_rule(world.get_location("Cleared 0-5", player),
                lambda state: (state.has("Slide", player) and \
                    (state.has("Wall Jump", player, walljump2) or \
                        state.has("Stamina Bar", player, dash2))) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                                state.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                    state.has("Railcannon - Malicious", player))
            if world.challenge_rewards[player]:
                set_rule(world.get_location("0-5: Don't inflict fatal damage to any enemy", player),
                    lambda state: (state.has("Slide", player) and \
                        (state.has("Wall Jump", player, walljump2) or \
                            state.has("Stamina Bar", player, dash2))) or \
                                state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                                    state.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                        state.has("Railcannon - Malicious", player))
            if world.p_rank_rewards[player]:
                set_rule(world.get_location("0-5: Perfect Rank", player),
                    lambda state: (state.has("Slide", player) and \
                        (state.has("Wall Jump", player, walljump2) or \
                            state.has("Stamina Bar", player, dash2))) or \
                                state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                                    state.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                        state.has("Railcannon - Malicious", player))
    
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("0-5: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("0-5: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
            
    
    # 1-1
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:

        set_rule(world.get_location("1-1: Secret #5", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("1-1: Secret #5", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
        else:
            set_rule(world.get_location("1-1: Secret #5", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))

    if world.randomize_skulls[player]:
        if world.randomize_secondary_fire[player]:
            set_rule(world.get_location("Cleared 1-1", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player) or \
                    state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
        else:
            set_rule(world.get_location("Cleared 1-1", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player) or \
                    state.has("Revolver - Marksman", player))
        set_rule(world.get_location("1-1: Weapon", player),
            lambda state: state.has("Red Skull (1-1)", player))
        set_rule(world.get_location("1-1: Secret #3", player),
            lambda state: state.has("Red Skull (1-1)", player))
        set_rule(world.get_location("1-1: Secret #4", player),
            lambda state: state.has("Red Skull (1-1)", player))
        add_rule(world.get_location("1-1: Secret #5", player),
            lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("1-1: Perfect Rank", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
    if not world.start_with_arm[player]:
        add_rule(world.get_location("Cleared 1-1", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("1-1: Perfect Rank", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
            
    if world.challenge_rewards[player]:
        if world.randomize_secondary_fire[player]:
            set_rule(world.get_location("1-1: Complete the level in under 10 seconds", player),
                lambda state: state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
        else:
            set_rule(world.get_location("1-1: Complete the level in under 10 seconds", player),
                lambda state: state.has("Revolver - Marksman", player))
        
    if world.randomize_secondary_fire[player]:
        set_rule(world.get_location("Cleared 1-S", player),
            lambda state: state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
    else:
        set_rule(world.get_location("Cleared 1-S", player),
            lambda state: state.has("Revolver - Marksman", player))

    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("1-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("1-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
        
    
    # 1-2
    if world.challenge_rewards[player]:
        set_rule(world.get_location("1-2: Do not pick up any skulls", player),
            lambda state: state.has("Railcannon - Electric", player))

    if world.randomize_skulls[player]:
        set_rule(world.get_location("1-2: Secret #3", player),
            lambda state: state.has("Blue Skull (1-2)", player))
        set_rule(world.get_location("1-2: Secret #4", player),
            lambda state: state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) or \
                state.has("Railcannon - Electric", player))
        set_rule(world.get_location("1-2: Secret #5", player),
            lambda state: state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) or \
                state.has("Railcannon - Electric", player))
        set_rule(world.get_location("Cleared 1-2", player),
            lambda state: state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) or \
                state.has("Railcannon - Electric", player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("1-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) or \
                    state.has("Railcannon - Electric", player))
        
    if not world.start_with_arm[player]:
        add_rule(world.get_location("1-2: Secret #3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("1-2: Secret #4", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("1-2: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 1-2", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("1-2: Perfect Rank", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))

    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("1-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("1-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 1-3
    if world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("1-3: Secret #1", player),
                lambda state: state._ultrakill_break_glass_fire2_noarm(player))
        else:
            set_rule(world.get_location("1-3: Secret #1", player),
                lambda state: state._ultrakill_break_glass_fire2_yesarm(player))
    else:
        set_rule(world.get_location("1-3: Secret #1", player),
            lambda state: state._ultrakill_break_glass(player))
        
    if not world.start_with_slide[player]:
        set_rule(world.get_location("1-3: Secret #4", player),
            lambda state: state.has("Slide", player))
        set_rule(world.get_location("1-3: Secret #5", player),
            lambda state: state.has("Slide", player))

    if world.randomize_skulls[player]:
        set_rule(world.get_location("Cleared 1-3", player),
            lambda state: state.has_any({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("1-3: Perfect Rank", player),
                lambda state: state.has_any({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))
        if world.challenge_rewards[player]:
            set_rule(world.get_location("1-3: Beat the secret encounter", player),
                lambda state: state.has_all({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))

    if not world.start_with_arm[player]:
        add_rule(world.get_location("Cleared 1-3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("1-3: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            if world.randomize_secondary_fire[player]:
                add_rule(world.get_location("1-3: Perfect Rank", player),
                    lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
            else:
                add_rule(world.get_location("1-3: Perfect Rank", player),
                    lambda state: state._ultrakill_good_weapon(player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("1-3: Beat the secret encounter", player),
                lambda state: state.has_all({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            if world.randomize_secondary_fire[player]:
                add_rule(world.get_location("1-3: Beat the secret encounter", player),
                    lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
            else:
                add_rule(world.get_location("1-3: Beat the secret encounter", player),
                    lambda state: state._ultrakill_good_weapon(player))
    

    # 1-4
    set_rule(world.get_location("1-4: Secret Weapon", player),
        lambda state: state.can_reach(world.get_region("1-1: HEART OF THE SUNRISE", player)) and \
            state.can_reach(world.get_region("1-2: THE BURNING WORLD", player)) and \
                state.can_reach(world.get_region("1-3: HALLS OF SACRED REMAINS", player)))
    
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        add_rule(world.get_location("1-4: Secret Weapon", player),
            lambda state: state._ultrakill_1_1_jump_noslam_nofire2(player) and \
                state._ultrakill_break_glass(player) and \
                    state._ultrakill_break_walls(player))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            add_rule(world.get_location("1-4: Secret Weapon", player),
                lambda state: state._ultrakill_1_1_jump_noslam_yesfire2_noarm(player) and \
                    state._ultrakill_break_glass_fire2_noarm(player) and \
                        state._ultrakill_break_walls_fire2_noarm(player))
        else:
            add_rule(world.get_location("1-4: Secret Weapon", player),
                lambda state: state._ultrakill_1_1_jump_noslam_nofire2(player) and \
                    state._ultrakill_break_glass_fire2_yesarm(player) and \
                        state._ultrakill_break_walls_fire2_yesarm(player))

    if world.randomize_skulls[player]:
        add_rule(world.get_location("1-4: Secret Weapon", player),
            lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)", "Blue Skull (1-3)", \
                "Red Skull (1-3)"}, player) and \
                    (state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) or \
                        state.has("Railcannon - Electric", player)))
    
    if not world.start_with_arm[player]:
        add_rule(world.get_location("1-4: Secret Weapon", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        
    if world.p_rank_rewards[player] and world.goal[player] != 0:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("1-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("1-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 2-1
    if world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("2-1: Secret #1", player),
                lambda state: (state.has("Stamina Bar", player, dash1) or \
                    (state.has("Shotgun - Core Eject", player) and \
                        state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                            (state.has("Shotgun - Pump Charge", player) and \
                                state.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                                    state.has_any({"Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                                        "Rocket Launcher - S.R.S. Cannon"}, player)) and \
                                            state._ultrakill_break_walls_fire2_noarm(player))
            if not world.start_with_slam[player]:
                set_rule(world.get_location("2-1: Secret #5", player),
                    lambda state: state.has_any({"Slam", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                        "Rocket Launcher - S.R.S. Cannon"}, player) or \
                            (state.has("Shotgun - Core Eject", player) and \
                                state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                    (state.has("Shotgun - Pump Charge", player) and \
                                        state.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)))
                set_rule(world.get_location("Cleared 2-1", player),
                    lambda state: state.has_any({"Slam", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                        "Rocket Launcher - S.R.S. Cannon"}, player) or \
                            (state.has("Shotgun - Core Eject", player) and \
                                state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                    (state.has("Shotgun - Pump Charge", player) and \
                                        state.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)))
                if world.p_rank_rewards[player]:
                    set_rule(world.get_location("2-1: Perfect Rank", player),
                        lambda state: state.has_any({"Slam", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                            "Rocket Launcher - S.R.S. Cannon"}, player) or \
                                (state.has("Shotgun - Core Eject", player) and \
                                    state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                        (state.has("Shotgun - Pump Charge", player) and \
                                            state.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)))
            else:
                set_rule(world.get_location("2-1: Secret #5", player),
                    lambda state: state.has_any({"Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                        "Rocket Launcher - S.R.S. Cannon"}, player) or \
                            (state.has("Shotgun - Core Eject", player) and \
                                state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                    (state.has("Shotgun - Pump Charge", player) and \
                                        state.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)))
                set_rule(world.get_location("Cleared 2-1", player),
                    lambda state: state.has_any({"Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                        "Rocket Launcher - S.R.S. Cannon"}, player) or \
                            (state.has("Shotgun - Core Eject", player) and \
                                state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                    (state.has("Shotgun - Pump Charge", player) and \
                                        state.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)))
                if world.p_rank_rewards[player]:
                    set_rule(world.get_location("2-1: Perfect Rank", player),
                        lambda state: state.has_any({"Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                            "Rocket Launcher - S.R.S. Cannon"}, player) or \
                                (state.has("Shotgun - Core Eject", player) and \
                                    state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                        (state.has("Shotgun - Pump Charge", player) and \
                                            state.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)))
        else:
            set_rule(world.get_location("2-1: Secret #1", player),
                lambda state: (state.has("Stamina Bar", player, dash1) or \
                    state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious", \
                        "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player)) and \
                            state._ultrakill_break_walls_fire2_yesarm(player))
        set_rule(world.get_location("2-1: Secret #3", player),
            lambda state: ((state.has("Stamina Bar", player, dash2) or \
                (state.has("Wall Jump", player, walljump1) and \
                    state.has("Stamina Bar", player, dash1))) and \
                        (state.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                            state.has("Railcannon - Malicious", player))) or \
                                state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        if world.challenge_rewards[player]:
            if not world.start_with_slam[player]:
                if not world.start_with_arm[player]:
                    set_rule(world.get_location("2-1: Don't open any normal doors", player),
                        lambda state: ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1) and \
                                state.has("Slam", player)) or \
                                    state.has_all({"Rocket Launcher - Freezeframe", \
                                        "Secondary Fire - Freezeframe"}, player)) and \
                                            state._ultrakill_break_walls_fire2_noarm(player))
                else:
                    set_rule(world.get_location("2-1: Don't open any normal doors", player),
                        lambda state: ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1) and \
                                state.has("Slam", player)) or \
                                    state.has_all({"Rocket Launcher - Freezeframe", \
                                        "Secondary Fire - Freezeframe"}, player)) and \
                                            state._ultrakill_break_walls_fire2_yesarm(player))
            else:
                if not world.start_with_arm[player]:
                    set_rule(world.get_location("2-1: Don't open any normal doors", player),
                        lambda state: ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has_all({"Rocket Launcher - Freezeframe", \
                                    "Secondary Fire - Freezeframe"}, player)) and \
                                        state._ultrakill_break_walls_fire2_noarm(player))
                else:
                    set_rule(world.get_location("2-1: Don't open any normal doors", player),
                        lambda state: ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has_all({"Rocket Launcher - Freezeframe", \
                                    "Secondary Fire - Freezeframe"}, player)) and \
                                        state._ultrakill_break_walls_fire2_yesarm(player))
    else:
        set_rule(world.get_location("2-1: Secret #1", player),
            lambda state: (state.has("Stamina Bar", player, dash1) or \
                state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious" \
                    "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player)) and \
                        state._ultrakill_break_walls(player))
        set_rule(world.get_location("2-1: Secret #3", player),
            lambda state: (state.has("Stamina Bar", player, dash2) or \
                (state.has("Wall Jump", player, walljump2) and \
                    state.has("Stamina Bar", player, dash1))) and \
                        state.has_any({"Shotgun - Pump Charge", "Railcannon - Malicious", "Rocket Launcher - Freezeframe"}, player))
        if world.challenge_rewards[player]:
            if not world.start_with_slam[player]:
                set_rule(world.get_location("2-1: Don't open any normal doors", player),
                    lambda state: ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1) and \
                            state.has("Slam", player)) or \
                                state.has("Rocket Launcher - Freezeframe", player)) and \
                                    state._ultrakill_break_walls(player))
            else:
                set_rule(world.get_location("2-1: Don't open any normal doors", player),
                    lambda state: ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has_all("Rocket Launcher - Freezeframe", player)) and \
                                    state._ultrakill_break_walls(player))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("2-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("2-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
            

    # 2-2
    if not world.start_with_slide[player]:
        set_rule(world.get_location("2-2: Secret #4", player),
            lambda state: state.has("Slide", player))
        
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("2-2: Secret #2", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        set_rule(world.get_location("2-2: Secret #3", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("2-2: Secret #2", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
            set_rule(world.get_location("2-2: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
        else:
            set_rule(world.get_location("2-2: Secret #2", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
            set_rule(world.get_location("2-2: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))

    add_rule(world.get_location("2-2: Secret #3", player),
        lambda state: state.has("Stamina Bar", player, dash1))
        
    if world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("2-2: Secret #5", player),
                lambda state: state._ultrakill_break_walls_fire2_noarm(player))
        else:
            set_rule(world.get_location("2-2: Secret #5", player),
                lambda state: state._ultrakill_break_walls_fire2_yesarm(player))
    else:
        set_rule(world.get_location("2-2: Secret #5", player),
            lambda state: state._ultrakill_break_walls(player))

    if world.challenge_rewards[player]:
        if not world.start_with_slide[player]:
            set_rule(world.get_location("2-2: Beat the level in under 60 seconds", player),
                lambda state: state.has("Stamina Bar", player, dash2))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("2-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("2-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 2-3
    if not world.start_with_slide[player]:
        set_rule(world.get_location("2-3: Secret #2", player),
            lambda state: state.has("Slide", player))
        set_rule(world.get_location("2-3: Secret #5", player),
            lambda state: state.has("Slide", player))

    if world.randomize_skulls[player]:
        add_rule(world.get_location("2-3: Secret #3", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        set_rule(world.get_location("2-3: Secret #4", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        #set_rule(world.get_location("Cleared 2-3", player),
            #lambda state: state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("2-3: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
    
    if not world.start_with_arm[player]:
        add_rule(world.get_location("Cleared 2-3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 2-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("2-3: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        
    if not world.start_with_slide[player]:
        add_rule(world.get_location("Cleared 2-S", player),
            lambda state: state.has("Slide", player))
    if world.randomize_skulls[player]:
        add_rule(world.get_location("2-3: Secret #3", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        add_rule(world.get_location("Cleared 2-S", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        
    if not world.start_with_slam[player]:
        if world.randomize_secondary_fire[player]:
            add_rule(world.get_location("2-3: Secret #3", player),
                lambda state: state._ultrakill_2_3_jump_noslam_nofire2(player, dash1, walljump2))
            add_rule(world.get_location("Cleared 2-S", player),
                lambda state: state._ultrakill_2_3_jump_noslam_nofire2(player, dash1, walljump2))
            if world.challenge_rewards[player]:
                add_rule(world.get_location("2-3: Don't touch any water", player),
                    lambda state: state._ultrakill_2_3_jump_noslam_nofire2(player, dash1, walljump2))
        else:
            add_rule(world.get_location("2-3: Secret #3", player),
                lambda state: state._ultrakill_2_3_jump_noslam_yesfire2(player, dash1, walljump2))
            add_rule(world.get_location("Cleared 2-S", player),
                lambda state: state._ultrakill_2_3_jump_noslam_yesfire2(player, dash1, walljump2))
            if world.challenge_rewards[player]:
                add_rule(world.get_location("2-3: Don't touch any water", player),
                    lambda state: state._ultrakill_2_3_jump_noslam_yesfire2(player, dash1, walljump2))
            
    if world.challenge_rewards[player] and not world.start_with_slide[player]:
        add_rule(world.get_location("2-3: Don't touch any water", player),
            lambda state: state.has("Slide", player))

    # either normal exit or secret exit
    if world.randomize_skulls[player]:
        if not world.start_with_slam[player]:
            if world.randomize_secondary_fire[player]:
                if not world.start_with_slide[player]:
                    add_rule(world.get_location("Cleared 2-3", player),
                        lambda state: (state._ultrakill_2_3_jump_noslam_nofire2(player, dash1, walljump2) and \
                            state.has_all({"Blue Skull (2-3)", "Slide"}, player)) or \
                                state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
                else:
                    add_rule(world.get_location("Cleared 2-3", player),
                        lambda state: (state._ultrakill_2_3_jump_noslam_nofire2(player, dash1, walljump2) and \
                            state.has("Blue Skull (2-3)", player)) or \
                                state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
            else:
                if not world.start_with_slide[player]:
                    add_rule(world.get_location("Cleared 2-3", player),
                        lambda state: (state._ultrakill_2_3_jump_noslam_yesfire2(player, dash1, walljump2) and \
                            state.has_all({"Blue Skull (2-3)", "Slide"}, player)) or \
                                state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
                else:
                    add_rule(world.get_location("Cleared 2-3", player),
                        lambda state: (state._ultrakill_2_3_jump_noslam_yesfire2(player, dash1, walljump2) and \
                            state.has("Blue Skull (2-3)", player)) or \
                                state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
        else:
            if not world.start_with_slide[player]:
                add_rule(world.get_location("Cleared 2-3", player),
                    lambda state: state.has_all({"Blue Skull (2-3)", "Slide"}, player) or \
                            state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
            else:
                add_rule(world.get_location("Cleared 2-3", player),
                    lambda state: state.has("Blue Skull (2-3)", player) or \
                            state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
        
    
    # 2-4
    if world.randomize_skulls[player]:
        set_rule(world.get_location("Cleared 2-4", player),
            lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        if world.challenge_rewards[player] and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Parry a punch", player),
                lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        if world.p_rank_rewards[player] and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        
    if not world.start_with_arm[player]:
        add_rule(world.get_location("Cleared 2-4", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.challenge_rewards[player] and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Parry a punch", player),
                lambda state: state.has("Feedbacker", player))
        if world.p_rank_rewards[player] and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if world.p_rank_rewards[player] and world.goal[player] != 1:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("2-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("2-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 3-1
    if not world.start_with_slide[player]:
        set_rule(world.get_location("3-1: Secret #4", player),
            lambda state: state.has("Slide", player))
        
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("3-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("3-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 3-2
    if not world.start_with_slide[player]:
        add_rule(world.get_location("Cleared 3-2", player),
            lambda state: state.has("Slide", player))
        if world.challenge_rewards[player] and world.goal[player] != 2:
            add_rule(world.get_location("3-2: Drop Gabriel in a pit", player),
                lambda state: state.has("Slide", player))
        if world.p_rank_rewards[player] and world.goal[player] != 2:
            add_rule(world.get_location("3-2: Perfect Rank", player),
                lambda state: state.has("Slide", player))
    if not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            add_rule(world.get_location("Cleared 3-2", player),
                lambda state: state._ultrakill_3_2_jump_noslam_yesfire2_noarm(player, dash1, walljump1))
        else:
            add_rule(world.get_location("Cleared 3-2", player),
                lambda state: state._ultrakill_3_2_jump_noslam_nofire2(player, dash1, walljump1))
        if world.challenge_rewards[player] and world.goal[player] != 2:
            if not world.start_with_arm[player]:
                add_rule(world.get_location("3-2: Drop Gabriel in a pit", player),
                    lambda state: state._ultrakill_3_2_jump_noslam_yesfire2_noarm(player, dash1, walljump1))
            else:
                add_rule(world.get_location("3-2: Drop Gabriel in a pit", player),
                    lambda state: state._ultrakill_3_2_jump_noslam_nofire2(player, dash1, walljump1))
        if world.p_rank_rewards[player] and world.goal[player] != 2:
            if not world.start_with_arm[player]:
                add_rule(world.get_location("3-2: Perfect Rank", player),
                    lambda state: state._ultrakill_3_2_jump_noslam_yesfire2_noarm(player, dash1, walljump1))
            else:
                add_rule(world.get_location("3-2: Perfect Rank", player),
                    lambda state: state._ultrakill_3_2_jump_noslam_nofire2(player, dash1, walljump1))
    elif not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        add_rule(world.get_location("Cleared 3-2", player),
            lambda state: state._ultrakill_3_2_jump_noslam_nofire2(player, dash1, walljump1))
        if world.challenge_rewards[player] and world.goal[player] != 2:
            add_rule(world.get_location("3-2: Drop Gabriel in a pit", player),
                lambda state: state._ultrakill_3_2_jump_noslam_nofire2(player, dash1, walljump1))
        if world.p_rank_rewards[player] and world.goal[player] != 2:
            add_rule(world.get_location("3-2: Perfect Rank", player),
                lambda state: state._ultrakill_3_2_jump_noslam_nofire2(player, dash1, walljump1))
            
    if world.p_rank_rewards[player] and world.goal[player] != 2:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("3-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("3-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 4-1
    if world.randomize_secondary_fire[player]:
        set_rule(world.get_location("4-1: Secret #1", player),
            lambda state: state.has("Stamina Bar", player, dash1) or \
                state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
    else:
        set_rule(world.get_location("4-1: Secret #1", player),
            lambda state: state.has("Stamina Bar", player, dash1) or \
                state.has("Rocket Launcher - Freezeframe", player))

    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("4-1: Secret #2", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        set_rule(world.get_location("4-1: Secret #3", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        set_rule(world.get_location("4-1: Secret #4", player),
            lambda state: (state.has("Wall Jump", player, walljump2) and \
                state.has("Slam", player)) or \
                    state.has_any({"Rocket Launcher - Freezeframe", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player))
        set_rule(world.get_location("4-1: Secret #5", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("4-1: Secret #2", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
            set_rule(world.get_location("4-1: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
            set_rule(world.get_location("4-1: Secret #5", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
        else:
            set_rule(world.get_location("4-1: Secret #2", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
            set_rule(world.get_location("4-1: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
            set_rule(world.get_location("4-1: Secret #5", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))

        set_rule(world.get_location("4-1: Secret #4", player),
            lambda state: (state.has("Wall Jump", player, walljump2) and \
                state.has("Slam", player)) or \
                    state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                        state.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                            state.has("Railcannon - Malicious", player))
    else:
        set_rule(world.get_location("4-1: Secret #4", player),
            lambda state: state.has("Wall Jump", player, walljump2) or \
                state.has_any({"Rocket Launcher - Freezeframe", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player))
        
    if world.challenge_rewards[player]:
        if not world.start_with_slam[player]:
            if world.randomize_secondary_fire[player]:
                set_rule(world.get_location("4-1: Don't activate any enemies", player),
                    lambda state: state._ultrakill_4_1c_noslam_yesfire2(player, dash2, walljump2))
            else:
                set_rule(world.get_location("4-1: Don't activate any enemies", player),
                    lambda state: state._ultrakill_4_1c_noslam_nofire2(player, dash2, walljump2))
        else:
            if world.randomize_secondary_fire[player]:
                set_rule(world.get_location("4-1: Don't activate any enemies", player),
                    lambda state: state._ultrakill_4_1c_yesslam_yesfire2(player, dash2, walljump2))
            else:
                set_rule(world.get_location("4-1: Don't activate any enemies", player),
                    lambda state: state._ultrakill_4_1c_yesslam_nofire2(player, dash2, walljump2))
                
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("4-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("4-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 4-2
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("4-2: Secret #4", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
        set_rule(world.get_location("Cleared 4-S", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("4-2: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
            set_rule(world.get_location("Cleared 4-S", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
        else:
            set_rule(world.get_location("4-2: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
            set_rule(world.get_location("Cleared 4-S", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))

    if world.randomize_skulls[player]:
        #set_rule(world.get_location("Cleared 4-2", player),
            #lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        if world.challenge_rewards[player]:
            set_rule(world.get_location("4-2: Kill the Insurrectionist in under 10 seconds", player),
                lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("4-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        
    if not world.start_with_arm[player]:
        add_rule(world.get_location("Cleared 4-2", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 4-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("4-2: Kill the Insurrectionist in under 10 seconds", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("4-2: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("4-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("4-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
            
    # either normal exit or secret exit
    if world.randomize_skulls[player]:
        if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
            set_rule(world.get_location("Cleared 4-2", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2) or \
                    state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
            if not world.start_with_arm[player]:
                set_rule(world.get_location("Cleared 4-2", player),
                    lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2) or \
                        state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
            else:
                set_rule(world.get_location("Cleared 4-2", player),
                    lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2) or \
                        state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))


    # 4-3
    if not world.start_with_arm[player] and not world.randomize_secondary_fire[player]:
        add_rule(world.get_location("4-3: Secret #1", player),
            lambda state: state._ultrakill_4_3_nofire2_noarm(player))
        add_rule(world.get_location("4-3: Secret #2", player),
            lambda state: state._ultrakill_4_3_nofire2_noarm(player))
        add_rule(world.get_location("4-3: Secret #3", player),
            lambda state: state._ultrakill_4_3_nofire2_noarm(player))
        add_rule(world.get_location("4-3: Secret #4", player),
            lambda state: state._ultrakill_4_3_nofire2_noarm(player) and \
                    state._ultrakill_break_walls(player))
        add_rule(world.get_location("4-3: Secret #5", player),
            lambda state: state._ultrakill_4_3_nofire2_noarm(player))
        add_rule(world.get_location("Cleared 4-3", player),
            lambda state: state._ultrakill_4_3_nofire2_noarm(player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("4-3: Don't pick up the torch", player),
            lambda state: (state.has("Shotgun - Core Eject", player) or \
                state.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)) and \
                    state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("4-3: Perfect Rank", player),
                lambda state: state._ultrakill_4_3_nofire2_noarm(player) and \
                    state.has_any({"Feedbacker", "Knuckleblaster"}, player))
    elif not world.start_with_arm[player] and world.randomize_secondary_fire[player]:
        add_rule(world.get_location("4-3: Secret #1", player),
            lambda state: state._ultrakill_4_3_yesfire2_noarm(player))
        add_rule(world.get_location("4-3: Secret #2", player),
            lambda state: state._ultrakill_4_3_yesfire2_noarm(player))
        add_rule(world.get_location("4-3: Secret #3", player),
            lambda state: state._ultrakill_4_3_yesfire2_noarm(player))
        add_rule(world.get_location("4-3: Secret #4", player),
            lambda state: state._ultrakill_4_3_yesfire2_noarm(player) and \
                    state._ultrakill_break_walls_fire2_noarm(player))
        add_rule(world.get_location("4-3: Secret #5", player),
            lambda state: state._ultrakill_4_3_yesfire2_noarm(player))
        add_rule(world.get_location("Cleared 4-3", player),
            lambda state: state._ultrakill_4_3_yesfire2_noarm(player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("4-3: Don't pick up the torch", player),
            lambda state: ((state.has("Shotgun - Core Eject", player) and \
                state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                    state.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)) and \
                        state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("4-3: Perfect Rank", player),
                lambda state: state._ultrakill_4_3_yesfire2_noarm(player) and \
                    state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        
    if not world.start_with_slide[player]:
        add_rule(world.get_location("4-3: Secret #2", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("4-3: Secret #5", player),
            lambda state: state.has("Slide", player))
        
    if world.randomize_skulls[player] and world.challenge_rewards[player]:
        add_rule(world.get_location("4-3: Don't pick up the torch", player),
            lambda state: state.has("Blue Skull (4-3)", player))
        
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("4-3: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("4-3: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
    

    # 4-4
    set_rule(world.get_location("Cleared 4-4", player),
        lambda state: state.has("Whiplash", player))
    if world.p_rank_rewards[player] and world.goal[player] != 3:
        set_rule(world.get_location("4-4: Perfect Rank", player),
            lambda state: state.has_any("Whiplash", player))
            
    if world.challenge_rewards[player] and world.goal[player] != 3:
        set_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
            lambda state: state.has("Whiplash", player) and \
                state.has("Stamina Bar", player, dash3))
        
    set_rule(world.get_location("4-4: Secret Weapon", player),
        lambda state: state.has_all({"Whiplash", "Railcannon - Electric"}, player))
        
    if world.randomize_skulls[player]:
        add_rule(world.get_location("4-4: Secret Weapon", player),
            lambda state: state.has("Blue Skull(4-4)", player))
        if world.challenge_rewards[player] and world.goal[player] != 3:
            add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                lambda state: state.has("Blue Skull (4-4)", player))
        if not world.randomize_secondary_fire[player]:
            add_rule(world.get_location("Cleared 4-4", player),
                lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                    ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has("Wall Jump", player, walljump2) or \
                                state.has("Rocket Launcher - Freezeframe", player)))
            add_rule(world.get_location("4-4: V2's Other Arm", player),
                lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                    ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has("Wall Jump", player, walljump2) or \
                                state.has("Rocket Launcher - Freezeframe", player)))
            if world.p_rank_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Perfect Rank", player),
                    lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has("Wall Jump", player, walljump2) or \
                                    state.has("Rocket Launcher - Freezeframe", player)))
            if world.challenge_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                    lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has("Wall Jump", player, walljump2) or \
                                    state.has("Rocket Launcher - Freezeframe", player)))
        else:
            add_rule(world.get_location("Cleared 4-4", player),
                lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                    ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has("Wall Jump", player, walljump2) or \
                                state.has_all({"Rocket Launcher - Freezeframe", \
                                    "Secondary Fire - Freezeframe"}, player)))
            add_rule(world.get_location("4-4: V2's Other Arm", player),
                lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                    ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has("Wall Jump", player, walljump2) or \
                                state.has_all({"Rocket Launcher - Freezeframe", \
                                    "Secondary Fire - Freezeframe"}, player)))
            if world.p_rank_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Perfect Rank", player),
                    lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has("Wall Jump", player, walljump2) or \
                                    state.has_all({"Rocket Launcher - Freezeframe", \
                                        "Secondary Fire - Freezeframe"}, player)))
            if world.challenge_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                    lambda state: state.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has("Wall Jump", player, walljump2) or \
                                    state.has_all({"Rocket Launcher - Freezeframe", \
                                        "Secondary Fire - Freezeframe"}, player)))
    elif not world.randomize_skulls[player]:
        if not world.randomize_secondary_fire[player]:
            add_rule(world.get_location("Cleared 4-4", player),
                lambda state: state.has("Whiplash", player))
            add_rule(world.get_location("4-4: V2's Other Arm", player),
                lambda state: state.has("Whiplash", player) or \
                    ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has("Wall Jump", player, walljump2) or \
                                state.has("Rocket Launcher - Freezeframe", player)))
            if world.p_rank_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Perfect Rank", player),
                    lambda state: state.has("Whiplash", player))
            if world.challenge_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                    lambda state: state.has("Whiplash", player) and \
                        state.has("Stamina Bar", player, dash3))
        else:
            add_rule(world.get_location("Cleared 4-4", player),
                lambda state: state.has("Whiplash", player) or \
                    ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has("Wall Jump", player, walljump2) or \
                                state.has_all({"Rocket Launcher - Freezeframe", \
                                    "Secondary Fire - Freezeframe"}, player)))
            add_rule(world.get_location("4-4: V2's Other Arm", player),
                lambda state: state.has("Whiplash", player) or \
                    ((state.has("Stamina Bar", player, dash1) and \
                        state.has("Wall Jump", player, walljump1)) or \
                            state.has("Wall Jump", player, walljump2) or \
                                state.has_all({"Rocket Launcher - Freezeframe", \
                                    "Secondary Fire - Freezeframe"}, player)))
            if world.p_rank_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Perfect Rank", player),
                    lambda state: state.has("Whiplash", player) or \
                        ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has("Wall Jump", player, walljump2) or \
                                    state.has_all({"Rocket Launcher - Freezeframe", \
                                        "Secondary Fire - Freezeframe"}, player)))
            if world.challenge_rewards[player] and world.goal[player] != 3:
                add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                    lambda state: state.has("Whiplash", player) or \
                        ((state.has("Stamina Bar", player, dash1) and \
                            state.has("Wall Jump", player, walljump1)) or \
                                state.has("Wall Jump", player, walljump2) or \
                                    state.has_all({"Rocket Launcher - Freezeframe", \
                                        "Secondary Fire - Freezeframe"}, player)))
                
    if world.p_rank_rewards[player] and world.goal[player] != 3:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("4-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("4-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
            
    if world.challenge_rewards[player] and world.goal[player] != 3:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 5-1
    if world.challenge_rewards[player]:
        if world.randomize_secondary_fire[player]:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Whiplash", player) or \
                    state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        else:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Whiplash", player) or \
                    state.has("Rocket Launcher - Freezeframe", player))

    if not world.start_with_slide[player]:
        add_rule(world.get_location("5-1: Secret #1", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("5-1: Secret #2", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("5-1: Secret #3", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("5-1: Secret #4", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: state.has("Slide", player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Slide", player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state.has("Slide", player))
        
    if world.randomize_skulls[player]:
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Blue Skull (5-1)", player, 3))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state.has("Blue Skull (5-1)", player, 3))

    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        add_rule(world.get_location("5-1: Secret #1", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #2", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #3", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #4", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        add_rule(world.get_location("5-1: Secret #1", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #2", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #3", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #4", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: (state.has("Slam", player) and \
                state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
    elif world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        add_rule(world.get_location("5-1: Secret #1", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #2", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #3", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #4", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has("Rocket Launcher - Freezeframe", player))
    elif world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        add_rule(world.get_location("5-1: Secret #1", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #2", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #3", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #4", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: (state.has("Wall Jump", player, walljump3) and \
                    state.has("Stamina Bar", player, dash2)) or \
                        state.has("Whiplash", player) or \
                            state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        
    if not world.start_with_arm[player]:
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 5-2
    if not world.start_with_slide[player]:
        if world.randomize_secondary_fire[player]:
            add_rule(world.get_location("5-2: Secret #1", player),
                lambda state: state.has("Slide", player) or \
                    state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        else:
            add_rule(world.get_location("5-2: Secret #1", player),
                lambda state: state.has("Slide", player) or \
                    state.has("Rocket Launcher - Freezeframe", player))

    if not world.start_with_slam[player]:
        add_rule(world.get_location("5-2: Secret #2", player),
            lambda state: state.has("Slam", player) or \
                state.has("Stamina Bar", player, dash1))
        add_rule(world.get_location("5-2: Secret #3", player),
            lambda state: state.has("Slam", player) or \
                state.has("Stamina Bar", player, dash1))
        add_rule(world.get_location("5-2: Secret #4", player),
            lambda state: state.has("Slam", player) or \
                state.has("Stamina Bar", player, dash1))
        add_rule(world.get_location("5-2: Secret #5", player),
            lambda state: state.has("Slam", player) or \
                state.has("Stamina Bar", player, dash1))
        add_rule(world.get_location("Cleared 5-2", player),
            lambda state: state.has("Slam", player) or \
                state.has("Stamina Bar", player, dash1))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has("Slam", player) or \
                    state.has("Stamina Bar", player, dash1))
        
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        add_rule(world.get_location("5-2: Secret #3", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
        add_rule(world.get_location("5-2: Secret #4", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            add_rule(world.get_location("5-2: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
            add_rule(world.get_location("5-2: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
        else:
            add_rule(world.get_location("5-2: Secret #3", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
            add_rule(world.get_location("5-2: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
        
    if world.challenge_rewards[player]:
        if world.randomize_secondary_fire[player]:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
        else:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has("Revolver - Marksman", player))
        
        
    if world.randomize_skulls[player]:
        add_rule(world.get_location("5-2: Secret #5", player),
            lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        add_rule(world.get_location("Cleared 5-2", player),
            lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("5-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
            
    if not world.start_with_arm[player]:
        add_rule(world.get_location("5-2: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        add_rule(world.get_location("Cleared 5-2", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("5-2: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("5-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("5-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 5-3
    if world.randomize_skulls[player]:
        set_rule(world.get_location("5-3: Secret #1", player),
            lambda state: state.has("Blue Skull (5-3)", player))
        set_rule(world.get_location("5-3: Secret #3", player),
            lambda state: state.has_all({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(world.get_location("5-3: Weapon", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(world.get_location("5-3: Secret #4", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(world.get_location("5-3: Secret #5", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        set_rule(world.get_location("Cleared 5-3", player),
            lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        if world.challenge_rewards[player]:
            set_rule(world.get_location("5-3: Don't touch any water", player),
                lambda state: state.has_all({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("5-3: Perfect Rank", player),
                lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        
    if not world.start_with_arm[player]:
        add_rule(world.get_location("5-3: Secret #1", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("5-3: Secret #3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("5-3: Weapon", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("5-3: Secret #4", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("5-3: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 5-3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("5-3: Don't touch any water", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("5-3: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if world.challenge_rewards[player]:
        if not world.start_with_slam[player]:
            add_rule(world.get_location("5-3: Don't touch any water", player),
                lambda state: state.has("Slide", player))
            
        if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
            set_rule(world.get_location("5-3: Don't touch any water", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
        elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
            if not world.start_with_arm[player]:
                set_rule(world.get_location("5-3: Don't touch any water", player),
                    lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump2))
            else:
                set_rule(world.get_location("5-3: Don't touch any water", player),
                    lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump2))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("5-3: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("5-3: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 5-4
    if world.challenge_rewards[player] and world.goal[player] != 4:
        if world.randomize_secondary_fire[player]:
            set_rule(world.get_location("5-4: Reach the surface in under 10 seconds", player),
                lambda state: state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
            if world.p_rank_rewards[player]:
                set_rule(world.get_location("5-4: Perfect Rank", player),
                    lambda state: state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        else:
            set_rule(world.get_location("5-4: Reach the surface in under 10 seconds", player),
                lambda state: state.has("Rocket Launcher - Freezeframe", player))
            if world.p_rank_rewards[player]:
                set_rule(world.get_location("5-4: Perfect Rank", player),
                    lambda state: state.has("Rocket Launcher - Freezeframe", player))
            
    if world.p_rank_rewards[player] and world.goal[player] != 4:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("5-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("5-4: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))


    # 6-1
    if not world.start_with_slam[player] and not world.randomize_secondary_fire[player]:
        set_rule(world.get_location("6-1: Secret #4", player),
            lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        if world.challenge_rewards[player]:
            set_rule(world.get_location("6-1: Beat the secret encounter", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        if world.p_rank_rewards[player]:
            set_rule(world.get_location("6-1: Perfect Rank", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
    elif not world.start_with_slam[player] and world.randomize_secondary_fire[player]:
        if not world.start_with_arm[player]:
            set_rule(world.get_location("6-1: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
        else:
            set_rule(world.get_location("6-1: Secret #4", player),
                lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        if world.challenge_rewards[player]:
            if not world.start_with_arm[player]:
                set_rule(world.get_location("6-1: Beat the secret encounter", player),
                    lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
            else:
                set_rule(world.get_location("6-1: Beat the secret encounter", player),
                    lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))
        if world.p_rank_rewards[player]:
            if not world.start_with_arm[player]:
                set_rule(world.get_location("6-1: Perfect Rank", player),
                    lambda state: state._ultrakill_jump_noslam_yesfire2_noarm(player, walljump1))
            else:
                set_rule(world.get_location("6-1: Perfect Rank", player),
                    lambda state: state._ultrakill_jump_noslam_nofire2(player, walljump1))

    if world.randomize_skulls[player]:
        add_rule(world.get_location("6-1: Secret #2", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(world.get_location("6-1: Secret #3", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(world.get_location("6-1: Secret #4", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(world.get_location("6-1: Secret #5", player),
            lambda state: state.has("Red Skull (6-1)", player))
        add_rule(world.get_location("Cleared 6-1", player),
            lambda state: state.has("Red Skull (6-1)", player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("6-1: Beat the secret encounter", player),
                lambda state: state.has("Red Skull (6-1)", player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("6-1: Perfect Rank", player),
                lambda state: state.has("Red Skull (6-1)", player))

    if not world.start_with_arm[player]:
        add_rule(world.get_location("6-1: Secret #2", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("6-1: Secret #3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("6-1: Secret #4", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("6-1: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 6-1", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.challenge_rewards[player]:
            add_rule(world.get_location("6-1: Beat the secret encounter", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if world.p_rank_rewards[player]:
            add_rule(world.get_location("6-1: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if world.p_rank_rewards[player]:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("6-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("6-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))
        
    
    # 6-2
    if not world.start_with_slam[player]:
        if world.randomize_secondary_fire[player]:
            set_rule(world.get_location("Cleared 6-2", player),
                lambda state: state.has_any({"Slam", "Railcannon - Malicious"}, player) or \
                    state.has("Wall Jump", player, walljump2) or \
                        state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                            state.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player))
        else:
            set_rule(world.get_location("Cleared 6-2", player),
                lambda state: state.has_any({"Slam", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                    "Shotgun - Pump Charge"}, player) or \
                        state.has("Wall Jump", player, walljump2))
        if world.goal[player] != 5:
            if world.challenge_rewards[player]:
                set_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
                    lambda state: state.has("Slam", player) or \
                        state.has("Wall Jump", player, walljump2))
            if world.p_rank_rewards[player]:
                set_rule(world.get_location("6-2: Perfect Rank", player),
                    lambda state: state.has("Slam", player) or \
                        state.has("Wall Jump", player, walljump2))
            
    if world.challenge_rewards[player] and world.goal[player] != 5:
        add_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
            lambda state: state.has("Rocket Launcher - Freezeframe", player) or \
                state.has("Rocket Launcher - S.R.S. Cannon", player))
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
                lambda state: state._ultrakill_good_weapon(player))
        
    if world.p_rank_rewards[player] and world.goal[player] != 5:
        if world.randomize_secondary_fire[player] and not world.start_with_arm[player]:
            add_rule(world.get_location("6-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon_fire2_noarm(player))
        else:
            add_rule(world.get_location("6-2: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player))

    add_rule(world.get_location("Cleared 6-2", player),
        lambda state: state.has("Stamina Bar", player, dash1))
    if world.p_rank_rewards[player] and world.goal[player] != 5:
        add_rule(world.get_location("6-2: Perfect Rank", player),
            lambda state: state.has("Stamina Bar", player, dash1))
    if world.challenge_rewards[player] and world.goal[player] != 5:
        add_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
            lambda state: state.has("Stamina Bar", player, dash1))

    # shop
    set_rule(world.get_location("Shop: Buy Revolver Variant", player),
        lambda state: state.has_any({"Revolver - Piercer", "Revolver - Marksman"}, player))
    
    set_rule(world.get_location("Shop: Buy Shotgun Variant", player),
        lambda state: state.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player))
    
    set_rule(world.get_location("Shop: Buy Nailgun Variant", player),
        lambda state: state.has_any({"Nailgun - Attractor", "Nailgun - Overheat"}, player))
    
    set_rule(world.get_location("Shop: Buy Railcannon Variant 1", player),
        lambda state: state.has_any({"Railcannon - Electric", "Railcannon - Screwdriver", "Railcannon - Malicious"}, player))
    
    set_rule(world.get_location("Shop: Buy Railcannon Variant 2", player),
        lambda state: state.has_any({"Railcannon - Electric", "Railcannon - Screwdriver", "Railcannon - Malicious"}, player))
    
    set_rule(world.get_location("Shop: Buy Rocket Launcher Variant", player),
        lambda state: state.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player))