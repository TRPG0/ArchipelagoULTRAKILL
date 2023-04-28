from worlds.generic.Rules import set_rule, add_rule
from ..AutoWorld import LogicMixin


class UltrakillLogic(LogicMixin):
    def _ultrakill_good_weapon(self, player, fire2, arm):
        if fire2 and not arm:
            return self.has_any({"Revolver - Piercer", "Revolver - Marksman", "Revolver - Sharpshooter"}, player) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                        (self.has("Shotgun - Pump Charge", player) and \
                            self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player))
        else:
            return self.has_any({"Revolver - Piercer", "Revolver - Marksman", "Revolver - Sharpshooter", \
                "Shotgun - Core Eject", "Shotgun - Pump Charge"}, player)

    def _ultrakill_break_glass(self, player, fire2, arm):
        if fire2 and not arm:
            return self.has_any({"Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                    (self.has("Revolver - Piercer", player) and \
                        self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                            (self.has("Revolver - Marksman", player) and \
                                self.has_any({"Revolver - Alternate", "Secondary Fire - Marksman"}, player)) or \
                                    (self.has("Revolver - Sharpshooter", player) and \
                                        self.has_any({"Revolver - Alternate", "Secondary Fire - Sharpshooter"}, player)) or \
                                            (self.has("Shotgun - Core Eject", player) and \
                                                self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                                    (self.has("Shotgun - Pump Charge", player) and \
                                                        self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player))
        elif fire2 and arm:
            return self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Electric", \
                "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                    "Knuckleblaster"}, player) or \
                        (self.has("Revolver - Piercer", player) and \
                            self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                                (self.has("Revolver - Marksman", player) and \
                                    self.has_any({"Revolver - Alternate", "Secondary Fire - Marksman"}, player)) or \
                                        (self.has("Revolver - Sharpshooter", player) and \
                                            self.has_any({"Revolver - Alternate", "Secondary Fire - Sharpshooter"}, player))
        else:
            return self.has_any({"Revolver - Piercer", "Revolver - Marksman", "Revolver - Sharpshooter", \
                "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Electric", "Railcannon - Malicious", \
                    "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player)

    def _ultrakill_break_walls(self, player, fire2, arm):
        if not fire2:
            return self.has_any({"Revolver - Sharpshooter", "Shotgun - Core Eject", "Shotgun - Pump Charge", "Nailgun - Overheat", "Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                (self.has_any({"Revolver - Piercer", "Revolver - Marksman"}, player) and \
                    self.has("Revolver - Alternate", player))
        elif fire2 and not arm:
            return self.has_any({"Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                self.has_all({"Nailgun - Overheat", "Secondary Fire - Overheat"}, player) or \
                    (self.has("Shotgun - Core Eject", player) and \
                        self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                            (self.has("Shotgun - Pump Charge", player) and \
                                self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                                    (self.has_any({"Revolver - Piercer", "Revolver - Marksman", "Revolver - Sharpshooter"}, player) and \
                                        self.has("Revolver - Alternate", player))
        elif fire2 and arm:
            return self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                self.has_all({"Nailgun - Overheat", "Secondary Fire - Overheat"}, player) or \
                    (self.has_any({"Revolver - Piercer", "Revolver - Marksman", "Revolver - Sharpshooter"}, player) and \
                        self.has("Revolver - Alternate", player))

    def _ultrakill_generic_jump(self, player, walljumps, slam, fire2, arm):
        if slam:
            return True
        elif not fire2 or arm:
            return self.has("Wall Jump", player, walljumps) or \
                self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)
        elif fire2 and not arm:
            return self.has("Wall Jump", player, walljumps) or \
                self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                    self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                        self.has_all({"Shotgun - Core Eject", "Secondary Fire - Core Eject"}, player) or \
                            (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                                self.has("Feedbacker", player)) or \
                                    self.has("Railcannon - Malicious", player) 

    def _ultrakill_rev1_fly(self, player, fire2):
        if fire2:
            return self.has_all({"Revolver - Sharpshooter", "Secondary Fire - Sharpshooter"}, player)
        else:
            return self.has("Revolver - Sharpshooter", player)

    def _ultrakill_0_1c(self, player, fire2, arm):
        if fire2 and not arm:
            return self.has_any({"Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                    (self.has("Revolver - Piercer", player) and \
                        self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                            self.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player) or \
                                (self.has("Shotgun - Core Eject", player) and \
                                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                        self.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)
        elif fire2 and arm:
            return self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Electric", \
                "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon" \
                    "Knuckleblaster"}, player) or \
                        (self.has("Revolver - Piercer", player) and \
                            self.has_any({"Revolver - Alternate", "Secondary Fire - Piercer"}, player)) or \
                                self.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player)
        else:
            return self.has_any({"Revolver - Piercer", "Shotgun - Core Eject", "Shotgun - Pump Charge", \
                "Railcannon - Electric", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                    "Rocket Launcher - S.R.S. Cannon", "Knuckleblaster"}, player) or \
                        self.has_all({"Revolver - Marksman", "Revolver - Alternate"}, player)
    
    def _ultrakill_0_2_secret(self, player, walljumps, slam, fire2, arm):
        if (not slam and not fire2) or (not slam and fire2 and arm):
            return self.has("Wall Jump", player, walljumps) or \
                (self.has("Wall Jump", player, walljumps-1) and \
                    self.has("Slam", player)) or \
                        self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                            "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)
        elif (slam and not fire2) or (slam and fire2 and arm):
            return self.has("Wall Jump", player, walljumps-1) or \
                self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", "Shotgun - Core Eject", \
                    "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)
        elif not slam and fire2 and not arm:
            return self.has("Wall Jump", player, walljumps) or \
                (self.has("Wall Jump", player, walljumps-1) and \
                    self.has("Slam", player)) or \
                        (self.has("Shotgun - Core Eject", player) and \
                            self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                (self.has("Shotgun - Pump Charge", player) and \
                                    self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                                        self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                                            "Railcannon - Malicious"}, player)
        elif slam and fire2 and not arm:
            return self.has("Wall Jump", player, walljumps) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                        (self.has("Shotgun - Pump Charge", player) and \
                            self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                                self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                                    "Railcannon - Malicious"}, player)
    
    def _ultrakill_0_3c(self, player, walljumps, slam, fire2, arm):
        if not slam and not fire2:
            return (self.has("Slam", player) and \
                self.has("Wall Jump", player, walljumps-2)) or \
                    (self.has("Shotgun - Core Eject", player) and \
                        self.has("Wall Jump", player, walljumps)) or \
                            self.has_any({"Shotgun - Pump Charge", "Railcannon - Malicious", \
                                "Rocket Launcher - Freezeframe"}, player)
        elif slam and not fire2:
            return self.has("Wall Jump", player, walljumps-2) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has("Wall Jump", player, walljumps)) or \
                        self.has_any({"Shotgun - Pump Charge", "Railcannon - Malicious", \
                            "Rocket Launcher - Freezeframe"}, player)
        elif not slam and fire2:
            if not arm:
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
            else:
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
        elif slam and fire2:
            if not arm:
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
            else:
                return self.has("Wall Jump", player, walljumps-2) or \
                    (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                        self.has("Wall Jump", player, walljumps)) or \
                            self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                                    self.has("Railcannon - Malicious", player)

    def _ultrakill_0_5(self, player, walljumps, dashes, slide, fire2):
        if not slide:
            if not fire2:
                return (self.has("Slide", player) and \
                    (self.has("Wall Jump", player, walljumps) or \
                        self.has("Stamina Bar", player, dashes))) or \
                            self.has_any({"Rocket Launcher - Freezeframe", "Railcannon - Malicious", "Shotgun - Pump Charge"}, player)
            else:
                return (self.has("Slide", player) and \
                    (self.has("Wall Jump", player, walljumps) or \
                        self.has("Stamina Bar", player, dashes))) or \
                            self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                                self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                    self.has("Railcannon - Malicious", player)
        else:
            if not fire2:
                return self.has("Wall Jump", player, walljumps) or \
                    self.has("Stamina Bar", player, dashes) or \
                        self.has_any({"Rocket Launcher - Freezeframe", "Railcannon - Malicious", "Shotgun - Pump Charge"}, player)
            else:
                return self.has("Wall Jump", player, walljumps) or \
                    self.has("Stamina Bar", player, dashes) or \
                        self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                            self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                self.has("Railcannon - Malicious", player)
    
    def _ultrakill_1_1_jump(self, player, slam, fire2, arm):
        if slam:
            return True
        elif not slam and not fire2:
            return self.has_any({"Slam", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon", \
                "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player)
        elif not slam and fire2:
            return self.has("Slam", player) or \
                self.has_any({"Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                    self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                        self.has_all({"Shotgun - Core Eject", "Secondary Fire - Core Eject"}, player) or \
                            (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                                self.has("Feedbacker", player)) or \
                                    self.has("Railcannon - Malicious", player)
        
    def _ultrakill_2_1_s1(self, player, dashes, fire2, arm):
        if fire2 and not arm:
            return self.has("Stamina Bar", player, dashes) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                        (self.has("Shotgun - Pump Charge", player) and \
                            self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player)) or \
                                self.has_any({"Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player)
        else:
            return self.has("Stamina Bar", player, dashes) or \
                self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player)

    def _ultrakill_2_1_s3(self, player, walljumps, dashes, fire2):
        if fire2:
            return ((self.has("Stamina Bar", player, dashes) or \
                (self.has("Wall Jump", player, walljumps) and \
                    self.has("Stamina Bar", player, dashes-1))) and \
                        (self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                            self.has("Railcannon - Malicious", player))) or \
                                self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player)
        else:
            return ((self.has("Stamina Bar", player, dashes) or \
                (self.has("Wall Jump", player, walljumps) and \
                    self.has("Stamina Bar", player, dashes-1))) and \
                        self.has_any({"Shotgun - Pump Charge", "Railcannon - Malicious"}, player)) or \
                            self.has("Rocket Launcher - Freezeframe", player)

    def _ultrakill_2_1_s5(self, player, walljumps, dashes, slam, fire2, arm):
        if slam:
            return True
        elif not arm and fire2:
            return self.has_any({"Slam", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                (self.has("Wall Jump", player, walljumps) and \
                    self.has("Stamina Bar", player, dashes)) or \
                        (self.has("Shotgun - Core Eject", player) and \
                            self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                (self.has("Shotgun - Pump Charge", player) and \
                                    self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player))
        else:
            return self.has_any({"Slam", "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                (self.has("Wall Jump", player, walljumps) and \
                    self.has("Stamina Bar", player, dashes))

    def _ultrakill_2_1c(self, player, walljumps, dashes, slam, fire2):
        if fire2:
            if not slam:
                return (self.has("Stamina Bar", player, dashes) and \
                    self.has("Wall Jump", player, walljumps) and \
                        self.has("Slam", player)) or \
                            self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player)
            else:
                return (self.has("Stamina Bar", player, dashes) and \
                    self.has("Wall Jump", player, walljumps)) or \
                        self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player)
        else:
            if not slam:
                return (self.has("Stamina Bar", player, dashes) and \
                    self.has("Wall Jump", player, walljumps) and \
                        self.has("Slam", player)) or \
                            self.has("Rocket Launcher - Freezeframe", player)
            else:
                return (self.has("Stamina Bar", player, dashes) and \
                    self.has("Wall Jump", player, walljumps)) or \
                        self.has("Rocket Launcher - Freezeframe", player)
    
    def _ultrakill_2_3_s3(self, player, walljumps, dashes, slam, fire2):
        if slam:
            return True
        elif not slam:
            if fire2:
                return self.has_any({"Slam", "Shotgun - Pump Charge", "Railcannon - Malicious", "Rocket Launcher - Freezeframe"}, player) or \
                    (self.has("Wall Jump", player, walljumps) or \
                        (self.has("Wall Jump", player, walljumps-1) and \
                            self.has("Stamina Bar", player, dashes)))
            else:
                return self.has_any({"Slam", "Railcannon - Malicious"}, player) or \
                    (self.has("Wall Jump", player, walljumps) or \
                        (self.has("Wall Jump", player, walljumps-1) and \
                            self.has("Stamina Bar", player, dashes))) or \
                                self.has_all({"Shotgun - Pump Charge", "Secondary Fire - Pump Charge"}, player) or \
                                    self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player)

    def _ultrakill_3_2_jump(self, player, walljumps, dashes, slam, fire2, arm):
        if slam:
            return True
        elif not slam and fire2:
            if not arm:
                return self.has_any({"Slam", "Railcannon - Malicious", "Rocket Launcher - Freezeframe", \
                    "Rocket Launcher - S.R.S. Cannon"}, player) or \
                        self.has("Wall Jump", player, walljumps) or \
                            self.has("Stamina Bar", player, dashes) or \
                                (self.has("Shotgun - Core Eject", player) and \
                                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                                        (self.has("Shotgun - Pump Charge", player) and \
                                            self.has_any({"Secondary Fire - Pump Charge", "Feedbacker"}, player))
            else:
                return self.has_any({"Slam", "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious", \
                    "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                        self.has("Wall Jump", player, walljumps) or \
                            self.has("Stamina Bar", player, dashes)
        elif not slam and not fire2:
            return self.has_any({"Slam", "Shotgun - Core Eject", "Shotgun - Pump Charge", "Railcannon - Malicious", \
                "Rocket Launcher - Freezeframe", "Rocket Launcher - S.R.S. Cannon"}, player) or \
                    self.has("Wall Jump", player, walljumps) or \
                        self.has("Stamina Bar", player, dashes)

    def _ultrakill_4_1c(self, player, walljumps, dashes, slam, fire2):
        if not slam:
            if fire2:
                return self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                    (self.has("Wall Jump", player, walljumps) and \
                        self.has("Stamina Bar", player, dashes) and \
                            self.has("Slam", player))
            else:
                return self.has("Rocket Launcher - Freezeframe", player) or \
                    (self.has("Wall Jump", player, walljumps) and \
                        self.has("Stamina Bar", player, dashes) and \
                            self.has("Slam", player))
        else:
            if fire2:
                return self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                    (self.has("Wall Jump", player, walljumps) and \
                        self.has("Stamina Bar", player, dashes))
            else:
                return self.has("Rocket Launcher - Freezeframe", player) or \
                    (self.has("Wall Jump", player, walljumps) and \
                        self.has("Stamina Bar", player, dashes))
    
    def _ultrakill_4_3(self, player, fire2, arm):
        if arm:
            return True
        elif not arm and not fire2:
            return self.has_any({"Feedbacker", "Knuckleblaster", "Shotgun - Core Eject", \
                "Railcannon - Malicious"}, player) or \
                    (self.has_any({"Shotgun - Core Eject", "Shotgun - Pump Charge"}, player) and \
                        self.has("Feedbacker", player))
        elif not arm and fire2:
            return self.has_any({"Feedbacker", "Knuckleblaster", "Railcannon - Malicious"}, player) or \
                (self.has("Shotgun - Core Eject", player) and \
                    self.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                        self.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)

    def _ultrakill_4_4(self, player, walljumps, dashes, skulls, slam, fire2):
        if skulls:
            if not fire2:
                if not slam:
                    return self.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has_any({"Slam", "Rocket Launcher - Freezeframe"}, player))
                else:
                    return self.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has("Rocket Launcher - Freezeframe", player))
            else:
                if not slam:
                    return self.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has("Slam", player) or \
                                        self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
                else:
                    return self.has_all({"Whiplash", "Blue Skull (4-4)"}, player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        else:
            if not fire2:
                if not slam:
                    return self.has("Whiplash", player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has_any({"Slam", "Rocket Launcher - Freezeframe"}, player))
                else:
                    return self.has("Whiplash", player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has("Rocket Launcher - Freezeframe", player))
            else:
                if not slam:
                    return self.has("Whiplash", player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has("Slam", player) or \
                                        self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
                else:
                    return self.has("Whiplash", player) or \
                        ((self.has("Stamina Bar", player, dashes) and \
                            self.has("Wall Jump", player, walljumps-1)) or \
                                self.has("Wall Jump", player, walljumps) or \
                                    self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))

    def _ultrakill_5_1(self, player, walljumps, dashes, slam, fire2):
        if not slam and not fire2:
            return (self.has("Slam", player) and \
                self.has("Wall Jump", player, walljumps) and \
                    self.has("Stamina Bar", player, dashes)) or \
                        self.has_any({"Rocket Launcher - Freezeframe", "Whiplash"}, player)
        elif not slam and fire2:
            return (self.has("Slam", player) and \
                self.has("Wall Jump", player, walljumps) and \
                    self.has("Stamina Bar", player, dashes)) or \
                        self.has("Whiplash", player) or \
                            self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player)
        elif slam and not fire2:
            return (self.has("Wall Jump", player, walljumps) and \
                self.has("Stamina Bar", player, dashes)) or \
                    self.has_any({"Rocket Launcher - Freezeframe", "Whiplash"}, player)
        elif slam and fire2:
            return (self.has("Wall Jump", player, walljumps) and \
                self.has("Stamina Bar", player, dashes)) or \
                    self.has("Whiplash", player) or \
                        self.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player)



def rules(ultrakillworld):
    world = ultrakillworld.multiworld
    player = ultrakillworld.player

    fire2 = world.randomize_secondary_fire[player]
    arm = world.start_with_arm[player]
    slam = world.start_with_slam[player]
    slide = world.start_with_slide[player]
    skulls = world.randomize_skulls[player]
    challenge = world.challenge_rewards[player]
    prank = world.p_rank_rewards[player]

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
    set_rule(world.get_location("0-1: Secret #1", player),
        lambda state: state._ultrakill_break_glass(player, fire2, arm))
        
    set_rule(world.get_location("0-1: Secret #3", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
    set_rule(world.get_location("0-1: Secret #4", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))

    if challenge:
        set_rule(world.get_location("0-1: Get 5 kills with a single glass panel", player),
            lambda state: state._ultrakill_0_1c(player, fire2, arm))

    if prank:
        set_rule(world.get_location("0-1: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm) or \
                state.has("Knuckleblaster", player))


    # 0-2
    if fire2:
        set_rule(world.get_location("0-2: Secret #3", player),
            lambda state: state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player) or \
                (state.has("Wall Jump", player, walljump1) and \
                    state.has("Stamina Bar", player, dash2)))
        
    if not slide:
        set_rule(world.get_location("0-2: Secret #4", player),
            lambda state: state.has("Slide", player))
        if challenge:
            set_rule(world.get_location("0-2: Beat the secret encounter", player),
                lambda state: state.has("Slide", player))

    if challenge:
        add_rule(world.get_location("0-2: Beat the secret encounter", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))
            
    if prank:
        add_rule(world.get_location("0-2: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))
            
    set_rule(world.get_location("Cleared 0-S", player),
        lambda state: state._ultrakill_0_2_secret(player, walljump2, slam, fire2, arm))
        
    if skulls:
        add_rule(world.get_location("Cleared 0-S", player),
            lambda state: state.has_all({"Blue Skull (0-2)", "Blue Skull (0-S)", "Red Skull (0-S)"}, player))
         
    if not arm:
        add_rule(world.get_location("Cleared 0-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))


    # 0-3
    set_rule(world.get_location("0-3: Secret #1", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
    set_rule(world.get_location("0-3: Secret #2", player),
        lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm))
        
    set_rule(world.get_location("0-3: Secret #3", player),
        lambda state: state._ultrakill_break_walls(player, fire2, arm))
    set_rule(world.get_location("Cleared 0-3", player),
        lambda state: state._ultrakill_break_walls(player, fire2, arm) or \
            state._ultrakill_0_3c(player, walljump3, slam, fire2, arm))
    

    set_rule(world.get_location("0-3: Weapon", player),
        lambda state: state._ultrakill_good_weapon(player, fire2, arm))

    if challenge:
        set_rule(world.get_location("0-3: Kill only 1 enemy", player),
            lambda state: state._ultrakill_0_3c(player, walljump3, slam, fire2, arm))

    if prank:
        add_rule(world.get_location("0-3: Perfect Rank", player),
            lambda state: state._ultrakill_break_walls(player, fire2, arm) and \
                state._ultrakill_good_weapon(player, fire2, arm))

    # 0-4
    set_rule(world.get_location("0-4: Secret #1", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))

    set_rule(world.get_location("0-4: Secret #2", player),
        lambda state: state._ultrakill_break_glass(player, fire2, arm))
        
    if not slide:
        set_rule(world.get_location("0-4: Secret #3", player),
            lambda state: state.has("Slide", player))
        if challenge:
            set_rule(world.get_location("0-4: Slide uninterrupted for 17 seconds", player),
                lambda state: state.has("Slide", player))
            
    if prank:
        set_rule(world.get_location("0-4: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 0-5
    set_rule(world.get_location("Cleared 0-5", player),
        lambda state: state._ultrakill_0_5(player, walljump2, dash2, slide, fire2))
    
    if challenge:
        set_rule(world.get_location("0-5: Don't inflict fatal damage to any enemy", player),
            lambda state: state._ultrakill_0_5(player, walljump2, dash2, slide, fire2))
    
    if prank:
        set_rule(world.get_location("0-5: Perfect Rank", player),
            lambda state: state._ultrakill_0_5(player, walljump2, dash2, slide, fire2) and \
                state._ultrakill_good_weapon(player, fire2, arm))
            
    
    # 1-1
    set_rule(world.get_location("1-1: Secret #5", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))

    if skulls:
        if fire2:
            if not arm:
                set_rule(world.get_location("Cleared 1-1", player),
                    lambda state: (state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player) and \
                        state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player)) or \
                            state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
            else:
                set_rule(world.get_location("Cleared 1-1", player),
                    lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player) or \
                        state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
        else:
            if not arm:
                set_rule(world.get_location("Cleared 1-1", player),
                    lambda state: (state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player) and \
                        state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player)) or \
                            state.has("Revolver - Marksman", player))
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
        if prank:
            set_rule(world.get_location("1-1: Perfect Rank", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
    if not arm:
        if prank:
            set_rule(world.get_location("1-1: Perfect Rank", player),
                lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)"}, player))
            
    if challenge:
        if fire2:
            set_rule(world.get_location("1-1: Complete the level in under 10 seconds", player),
                lambda state: state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
        else:
            set_rule(world.get_location("1-1: Complete the level in under 10 seconds", player),
                lambda state: state.has("Revolver - Marksman", player))
        
    if fire2:
        set_rule(world.get_location("Cleared 1-S", player),
            lambda state: state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
    else:
        set_rule(world.get_location("Cleared 1-S", player),
            lambda state: state.has("Revolver - Marksman", player))

    if prank:
        add_rule(world.get_location("1-1: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))
        
    
    # 1-2
    if challenge:
        set_rule(world.get_location("1-2: Do not pick up any skulls", player),
            lambda state: state.has("Railcannon - Electric", player))

    if skulls:
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
        if prank:
            set_rule(world.get_location("1-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) or \
                    state.has("Railcannon - Electric", player))
        
    if not arm:
        add_rule(world.get_location("1-2: Secret #3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("1-2: Secret #4", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player) or \
                state.has("Railcannon - Electric", player))
        add_rule(world.get_location("1-2: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player) or \
                state.has("Railcannon - Electric", player))
        add_rule(world.get_location("Cleared 1-2", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player) or \
                state.has("Railcannon - Electric", player))
        if prank:
            add_rule(world.get_location("1-2: Perfect Rank", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))

    if prank:
        add_rule(world.get_location("1-2: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 1-3        
    set_rule(world.get_location("1-3: Secret #1", player),
        lambda state: state._ultrakill_break_glass(player, fire2, arm))
        
    if not slide:
        set_rule(world.get_location("1-3: Secret #4", player),
            lambda state: state.has("Slide", player))
        set_rule(world.get_location("1-3: Secret #5", player),
            lambda state: state.has("Slide", player))

    if skulls:
        set_rule(world.get_location("Cleared 1-3", player),
            lambda state: state.has_any({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))
        if prank:
            set_rule(world.get_location("1-3: Perfect Rank", player),
                lambda state: state.has_any({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))
        if challenge:
            set_rule(world.get_location("1-3: Beat the secret encounter", player),
                lambda state: state.has_all({"Red Skull (1-3)", "Blue Skull (1-3)"}, player))

    if not arm:
        add_rule(world.get_location("Cleared 1-3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if prank:
            add_rule(world.get_location("1-3: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if challenge:
            add_rule(world.get_location("1-3: Beat the secret encounter", player),
                lambda state: state.has_all({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))

    if challenge:
        add_rule(world.get_location("1-3: Beat the secret encounter", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))

    if prank:
        add_rule(world.get_location("1-3: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))    

    # 1-4
    set_rule(world.get_location("1-4: Secret Weapon", player),
        lambda state: state.can_reach(world.get_region("1-1: HEART OF THE SUNRISE", player)) and \
            state.can_reach(world.get_region("1-2: THE BURNING WORLD", player)) and \
                state.can_reach(world.get_region("1-3: HALLS OF SACRED REMAINS", player)))
    
    add_rule(world.get_location("1-4: Secret Weapon", player),
        lambda state: state._ultrakill_1_1_jump(player, slam, fire2, arm) and \
            state._ultrakill_break_glass(player, fire2, arm) and \
                state._ultrakill_break_walls(player, fire2, arm))

    if skulls:
        add_rule(world.get_location("1-4: Secret Weapon", player),
            lambda state: state.has_all({"Red Skull (1-1)", "Blue Skull (1-1)", "Blue Skull (1-3)", \
                "Red Skull (1-3)"}, player) and \
                    (state.has_all({"Blue Skull (1-2)", "Red Skull (1-2)"}, player) or \
                        state.has("Railcannon - Electric", player)))
    
    if not arm:
        add_rule(world.get_location("1-4: Secret Weapon", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))

    if challenge and world.goal[player] != 0:
        add_rule(world.get_location("1-4: Do not pick up any skulls", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))

        
    if prank and world.goal[player] != 0:
        add_rule(world.get_location("1-4: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 2-1
    set_rule(world.get_location("2-1: Secret #1", player),
        lambda state: state._ultrakill_2_1_s1(player, dash1, fire2, arm) and \
            state._ultrakill_break_walls(player, fire2, arm))

    set_rule(world.get_location("2-1: Secret #3", player),
        lambda state: state._ultrakill_2_1_s3(player, walljump1, dash2, fire2))

    set_rule(world.get_location("2-1: Secret #5", player),
        lambda state: state._ultrakill_2_1_s5(player, walljump1, dash1, slam, fire2, arm))
    set_rule(world.get_location("Cleared 2-1", player),
        lambda state: state._ultrakill_2_1_s5(player, walljump1, dash1, slam, fire2, arm))

    if challenge:
        set_rule(world.get_location("2-1: Don't open any normal doors", player),
            lambda state: state._ultrakill_2_1c(player, walljump1, dash1, slam, fire2) and \
                state._ultrakill_break_walls(player, fire2, arm))

    if prank:
        set_rule(world.get_location("2-1: Perfect Rank", player),
            lambda state: state._ultrakill_2_1_s5(player, walljump1, dash1, slam, fire2, arm) and \
                state._ultrakill_good_weapon(player, fire2, arm))            

    # 2-2
    if not slide:
        set_rule(world.get_location("2-2: Secret #4", player),
            lambda state: state.has("Slide", player))
    
    set_rule(world.get_location("2-2: Secret #2", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
    set_rule(world.get_location("2-2: Secret #3", player),
        lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm))
        
    set_rule(world.get_location("2-2: Secret #5", player),
        lambda state: state._ultrakill_break_walls(player, fire2, arm))

    if challenge:
        if not slide:
            set_rule(world.get_location("2-2: Beat the level in under 60 seconds", player),
                lambda state: state.has("Stamina Bar", player, dash2))
            
    if prank:
        add_rule(world.get_location("2-2: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))
        


    # 2-3
    if not slide:
        set_rule(world.get_location("2-3: Secret #2", player),
            lambda state: state.has("Slide", player))
        set_rule(world.get_location("2-3: Secret #5", player),
            lambda state: state.has("Slide", player))

    if skulls:
        add_rule(world.get_location("2-3: Secret #3", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        set_rule(world.get_location("2-3: Secret #4", player),
            lambda state: state.has("Blue Skull (2-3)", player))
        #set_rule(world.get_location("Cleared 2-3", player),
            #lambda state: state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
        if challenge:
            set_rule(world.get_location("2-3: Don't touch any water", player),
                lambda state: state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
        if prank:
            set_rule(world.get_location("2-3: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
    
    if not arm:
        add_rule(world.get_location("2-3: Secret #3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("2-3: Secret #4", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 2-3", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 2-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if challenge:
            add_rule(world.get_location("2-3: Don't touch any water", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if prank:
            add_rule(world.get_location("2-3: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        
    if not slide:
        add_rule(world.get_location("Cleared 2-S", player),
            lambda state: state.has("Slide", player))
    if skulls:
        add_rule(world.get_location("Cleared 2-S", player),
            lambda state: state.has("Blue Skull (2-3)", player))

    add_rule(world.get_location("2-3: Secret #3", player),
        lambda state: state._ultrakill_2_3_s3(player, walljump2, dash1, slam, fire2))
    add_rule(world.get_location("Cleared 2-S", player),
        lambda state: state._ultrakill_2_3_s3(player, walljump2, dash1, slam, fire2))
    if challenge:
        add_rule(world.get_location("2-3: Don't touch any water", player),
            lambda state: state._ultrakill_2_3_s3(player, walljump2, dash1, slam, fire2))
            
    if challenge and not slide:
        add_rule(world.get_location("2-3: Don't touch any water", player),
            lambda state: state.has("Slide", player))

    # either normal exit or secret exit
    if skulls:
        if not slide:
            add_rule(world.get_location("Cleared 2-3", player),
                lambda state: (state._ultrakill_2_3_s3(player, walljump2, dash1, slam, fire2) and \
                    state.has_all({"Blue Skull (2-3)", "Slide"}, player)) or \
                        state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))
        else:
            add_rule(world.get_location("Cleared 2-3", player),
                lambda state: (state._ultrakill_2_3_s3(player, walljump2, dash1, slam, fire2) and \
                    state.has("Blue Skull (2-3)", player)) or \
                        state.has_all({"Blue Skull (2-3)", "Red Skull (2-3)"}, player))

    if prank:
        add_rule(world.get_location("2-3: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))
        
    
    # 2-4
    if skulls:
        set_rule(world.get_location("Cleared 2-4", player),
            lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        if challenge and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Parry a punch", player),
                lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        if prank and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (2-4)", "Red Skull (2-4)"}, player))
        
    if not arm:
        add_rule(world.get_location("Cleared 2-4", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if challenge and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Parry a punch", player),
                lambda state: state.has("Feedbacker", player))
        if prank and world.goal[player] != 1:
            add_rule(world.get_location("2-4: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if prank and world.goal[player] != 1:
        add_rule(world.get_location("2-4: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 3-1
    if not slide:
        set_rule(world.get_location("3-1: Secret #4", player),
            lambda state: state.has("Slide", player))
        
    if prank:
            add_rule(world.get_location("3-1: Perfect Rank", player),
                lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 3-2
    if not slide:
        add_rule(world.get_location("Cleared 3-2", player),
            lambda state: state.has("Slide", player))
        if challenge and world.goal[player] != 2:
            add_rule(world.get_location("3-2: Drop Gabriel in a pit", player),
                lambda state: state.has("Slide", player))
        if prank and world.goal[player] != 2:
            add_rule(world.get_location("3-2: Perfect Rank", player),
                lambda state: state.has("Slide", player))

    add_rule(world.get_location("Cleared 3-2", player),
        lambda state: state._ultrakill_3_2_jump(player, walljump1, dash1, slam, fire2, arm))
    if challenge and world.goal[player] != 2:
        add_rule(world.get_location("3-2: Drop Gabriel in a pit", player),
            lambda state: state._ultrakill_3_2_jump(player, walljump1, dash1, slam, fire2, arm) and \
                state._ultrakill_good_weapon(player, fire2, arm))
    if prank and world.goal[player] != 2:
        add_rule(world.get_location("3-2: Perfect Rank", player),
            lambda state: state._ultrakill_3_2_jump(player, walljump1, dash1, slam, fire2, arm) and \
                state._ultrakill_good_weapon(player, fire2, arm))

    # 4-1
    if fire2:
        set_rule(world.get_location("4-1: Secret #1", player),
            lambda state: state.has("Stamina Bar", player, dash1) or \
                state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
    else:
        set_rule(world.get_location("4-1: Secret #1", player),
            lambda state: state.has("Stamina Bar", player, dash1) or \
                state.has("Rocket Launcher - Freezeframe", player))

    if not slam and not fire2:
        set_rule(world.get_location("4-1: Secret #4", player),
            lambda state: (state.has("Wall Jump", player, walljump2) and \
                state.has("Slam", player)) or \
                    state.has_any({"Rocket Launcher - Freezeframe", "Shotgun - Pump Charge", "Railcannon - Malicious"}, player))
    elif not slam and fire2:
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

    set_rule(world.get_location("4-1: Secret #2", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
    set_rule(world.get_location("4-1: Secret #3", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
    set_rule(world.get_location("4-1: Secret #5", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
        
    if challenge:
        set_rule(world.get_location("4-1: Don't activate any enemies", player),
            lambda state: state._ultrakill_4_1c(player, walljump2, dash2, slam, fire2))
                
    if prank:
        add_rule(world.get_location("4-1: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 4-2            
    set_rule(world.get_location("4-2: Secret #4", player),
        lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm) or \
            state._ultrakill_rev1_fly(player, fire2))
    set_rule(world.get_location("Cleared 4-S", player),
        lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm) or \
            state._ultrakill_rev1_fly(player, fire2)) 

    if skulls:
        #set_rule(world.get_location("Cleared 4-2", player),
            #lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        if challenge:
            set_rule(world.get_location("4-2: Kill the Insurrectionist in under 10 seconds", player),
                lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        if prank:
            set_rule(world.get_location("4-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))
        
    if not arm:
        add_rule(world.get_location("Cleared 4-2", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 4-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if challenge:
            add_rule(world.get_location("4-2: Kill the Insurrectionist in under 10 seconds", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if prank:
            add_rule(world.get_location("4-2: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if prank:
        add_rule(world.get_location("4-2: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))
            
    # either normal exit or secret exit
    if skulls:
        set_rule(world.get_location("Cleared 4-2", player),
            lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm) or \
                state._ultrakill_rev1_fly(player, fire2) or \
                    state.has_all({"Blue Skull (4-2)", "Red Skull (4-2)"}, player))

    # 4-3
    if not arm and not fire2:
        if challenge:
            add_rule(world.get_location("4-3: Don't pick up the torch", player),
            lambda state: (state.has("Shotgun - Core Eject", player) or \
                state.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)) and \
                    state.has_any({"Feedbacker", "Knuckleblaster"}, player))
    elif not arm and fire2:
        if challenge:
            add_rule(world.get_location("4-3: Don't pick up the torch", player),
            lambda state: ((state.has("Shotgun - Core Eject", player) and \
                state.has_any({"Secondary Fire - Core Eject", "Feedbacker"}, player)) or \
                    state.has_all({"Shotgun - Pump Charge", "Feedbacker"}, player)) and \
                        state.has_any({"Feedbacker", "Knuckleblaster"}, player))

    add_rule(world.get_location("4-3: Secret #1", player),
        lambda state: state._ultrakill_4_3(player, fire2, arm))
    add_rule(world.get_location("4-3: Secret #2", player),
        lambda state: state._ultrakill_4_3(player, fire2, arm))
    add_rule(world.get_location("4-3: Secret #3", player),
        lambda state: state._ultrakill_4_3(player, fire2, arm))
    add_rule(world.get_location("4-3: Secret #4", player),
        lambda state: state._ultrakill_4_3(player, fire2, arm) and \
            state._ultrakill_break_walls(player, fire2, arm))
    add_rule(world.get_location("4-3: Secret #5", player),
        lambda state: state._ultrakill_4_3(player, fire2, arm))
    add_rule(world.get_location("Cleared 4-3", player),
        lambda state: state._ultrakill_4_3(player, fire2, arm))
    if prank:
        add_rule(world.get_location("4-3: Perfect Rank", player),
            lambda state: state._ultrakill_4_3(player, fire2, arm) and \
                state._ultrakill_good_weapon(player, fire2, arm))

    if not slide:
        add_rule(world.get_location("4-3: Secret #2", player),
            lambda state: state.has("Slide", player))
        add_rule(world.get_location("4-3: Secret #5", player),
            lambda state: state.has("Slide", player))
        
    if skulls and challenge:
        add_rule(world.get_location("4-3: Don't pick up the torch", player),
            lambda state: state.has("Blue Skull (4-3)", player))
        

    # 4-4
    set_rule(world.get_location("Cleared 4-4", player),
        lambda state: state.has("Whiplash", player))
    if prank and world.goal[player] != 3:
        set_rule(world.get_location("4-4: Perfect Rank", player),
            lambda state: state.has_any("Whiplash", player))
            
    if challenge and world.goal[player] != 3:
        set_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
            lambda state: state.has("Whiplash", player) and \
                state.has("Stamina Bar", player, dash3))
        
    set_rule(world.get_location("4-4: Secret Weapon", player),
        lambda state: state.has_all({"Whiplash", "Railcannon - Electric"}, player))
        
    if skulls:
        add_rule(world.get_location("4-4: Secret Weapon", player),
            lambda state: state.has("Blue Skull(4-4)", player))
        if challenge and world.goal[player] != 3:
            add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
                lambda state: state.has("Blue Skull (4-4)", player))

    add_rule(world.get_location("Cleared 4-4", player),
        lambda state: state._ultrakill_4_4(player, walljump2, dash1, skulls, slam, fire2))
    add_rule(world.get_location("4-4: V2's Other Arm", player),
        lambda state: state._ultrakill_4_4(player, walljump2, dash1, skulls, slam, fire2))
    if prank and world.goal[player] != 3:
        add_rule(world.get_location("4-4: Perfect Rank", player),
            lambda state: state._ultrakill_4_4(player, walljump2, dash1, skulls, slam, fire2) and \
                state._ultrakill_good_weapon(player, fire2, arm))
                            
    if challenge and world.goal[player] != 3:
        add_rule(world.get_location("4-4: Reach the boss room in 18 seconds", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))

    # 5-1
    if challenge:
        if fire2:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Whiplash", player) or \
                    state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        else:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Whiplash", player) or \
                    state.has("Rocket Launcher - Freezeframe", player))

    if not slide:
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
        if challenge:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Slide", player))
        if prank:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state.has("Slide", player))
        
    if skulls:
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: state.has("Blue Skull (5-1)", player, 3))
        if challenge:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has("Blue Skull (5-1)", player, 3))
        if prank:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state.has("Blue Skull (5-1)", player, 3))

    add_rule(world.get_location("5-1: Secret #1", player),
        lambda state: state._ultrakill_5_1(player, walljump3, dash2, slam, fire2))
    add_rule(world.get_location("5-1: Secret #2", player),
        lambda state: state._ultrakill_5_1(player, walljump3, dash2, slam, fire2))
    add_rule(world.get_location("5-1: Secret #3", player),
        lambda state: state._ultrakill_5_1(player, walljump3, dash2, slam, fire2))
    add_rule(world.get_location("5-1: Secret #4", player),
        lambda state: state._ultrakill_5_1(player, walljump3, dash2, slam, fire2))
    add_rule(world.get_location("5-1: Secret #5", player),
        lambda state: state._ultrakill_5_1(player, walljump3, dash2, slam, fire2))
    add_rule(world.get_location("Cleared 5-1", player),
        lambda state: state._ultrakill_5_1(player, walljump3, dash2, slam, fire2))
    add_rule(world.get_location("Cleared 5-S", player),
        lambda state: state._ultrakill_5_1(player, walljump3, dash2, slam, fire2))

    if not arm:
        add_rule(world.get_location("5-1: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 5-1", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        add_rule(world.get_location("Cleared 5-S", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if challenge:
            add_rule(world.get_location("5-1: Don't touch any water", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if prank:
            add_rule(world.get_location("5-1: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        
    if prank:
        add_rule(world.get_location("5-1: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 5-2
    if not slide:
        if fire2:
            add_rule(world.get_location("5-2: Secret #1", player),
                lambda state: state.has("Slide", player) or \
                    state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        else:
            add_rule(world.get_location("5-2: Secret #1", player),
                lambda state: state.has("Slide", player) or \
                    state.has("Rocket Launcher - Freezeframe", player))

    if not slam:
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
        if challenge:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has("Slam", player) or \
                    state.has("Stamina Bar", player, dash1))
                    
    add_rule(world.get_location("5-2: Secret #3", player),
        lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm))
    add_rule(world.get_location("5-2: Secret #4", player),
        lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm))
        
    if challenge:
        if fire2:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has_all({"Revolver - Marksman", "Secondary Fire - Marksman"}, player))
        else:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has("Revolver - Marksman", player))
        
        
    if skulls:
        add_rule(world.get_location("5-2: Secret #5", player),
            lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        add_rule(world.get_location("Cleared 5-2", player),
            lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        if challenge:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
        if prank:
            add_rule(world.get_location("5-2: Perfect Rank", player),
                lambda state: state.has_all({"Blue Skull (5-2)", "Red Skull (5-2)"}, player))
            
    if not arm:
        add_rule(world.get_location("5-2: Secret #5", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        add_rule(world.get_location("Cleared 5-2", player),
            lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        if challenge:
            add_rule(world.get_location("5-2: Don't fight the ferryman", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
        if prank:
            add_rule(world.get_location("5-2: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster"}, player))
            
    if prank:
        add_rule(world.get_location("5-2: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 5-3
    if skulls:
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
        if challenge:
            set_rule(world.get_location("5-3: Don't touch any water", player),
                lambda state: state.has_all({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        if prank:
            set_rule(world.get_location("5-3: Perfect Rank", player),
                lambda state: state.has_any({"Blue Skull (5-3)", "Red Skull (5-3)"}, player))
        
    if not arm:
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
        if challenge:
            add_rule(world.get_location("5-3: Don't touch any water", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if prank:
            add_rule(world.get_location("5-3: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if challenge:
        if not slam:
            add_rule(world.get_location("5-3: Don't touch any water", player),
                lambda state: state.has("Slide", player))

        add_rule(world.get_location("5-3: Don't touch any water", player),
            lambda state: state._ultrakill_generic_jump(player, walljump2, slam, fire2, arm))
            
    if prank:
        add_rule(world.get_location("5-3: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 5-4
    if challenge and world.goal[player] != 4:
        if fire2:
            set_rule(world.get_location("5-4: Reach the surface in under 10 seconds", player),
                lambda state: state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
            if prank:
                set_rule(world.get_location("5-4: Perfect Rank", player),
                    lambda state: state.has_all({"Rocket Launcher - Freezeframe", "Secondary Fire - Freezeframe"}, player))
        else:
            set_rule(world.get_location("5-4: Reach the surface in under 10 seconds", player),
                lambda state: state.has("Rocket Launcher - Freezeframe", player))
            if prank:
                set_rule(world.get_location("5-4: Perfect Rank", player),
                    lambda state: state.has("Rocket Launcher - Freezeframe", player))
            
    if prank and world.goal[player] != 4:
        add_rule(world.get_location("5-4: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))


    # 6-1
    set_rule(world.get_location("6-1: Secret #4", player),
        lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
    if challenge:
        set_rule(world.get_location("6-1: Beat the secret encounter", player),
            lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))
    if prank:
        set_rule(world.get_location("6-1: Perfect Rank", player),
            lambda state: state._ultrakill_generic_jump(player, walljump1, slam, fire2, arm))

    if skulls:
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
        if challenge:
            add_rule(world.get_location("6-1: Beat the secret encounter", player),
                lambda state: state.has("Red Skull (6-1)", player))
        if prank:
            add_rule(world.get_location("6-1: Perfect Rank", player),
                lambda state: state.has("Red Skull (6-1)", player))

    if not arm:
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
        if challenge:
            add_rule(world.get_location("6-1: Beat the secret encounter", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
        if prank:
            add_rule(world.get_location("6-1: Perfect Rank", player),
                lambda state: state.has_any({"Feedbacker", "Knuckleblaster", "Whiplash"}, player))
            
    if prank:
        add_rule(world.get_location("6-1: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))
        
    
    # 6-2
    if not slam:
        if fire2:
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
            if challenge:
                set_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
                    lambda state: state.has("Slam", player) or \
                        state.has("Wall Jump", player, walljump2))
            if prank:
                set_rule(world.get_location("6-2: Perfect Rank", player),
                    lambda state: state.has("Slam", player) or \
                        state.has("Wall Jump", player, walljump2))
            
    if challenge and world.goal[player] != 5:
        add_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
            lambda state: (state.has("Rocket Launcher - Freezeframe", player) or \
                state.has("Rocket Launcher - S.R.S. Cannon", player)) and \
                    state._ultrakill_good_weapon(player, fire2, arm))
        
    if prank and world.goal[player] != 5:
        add_rule(world.get_location("6-2: Perfect Rank", player),
            lambda state: state._ultrakill_good_weapon(player, fire2, arm))

    add_rule(world.get_location("Cleared 6-2", player),
        lambda state: state.has("Stamina Bar", player, dash1))
    if prank and world.goal[player] != 5:
        add_rule(world.get_location("6-2: Perfect Rank", player),
            lambda state: state.has("Stamina Bar", player, dash1))
    if challenge and world.goal[player] != 5:
        add_rule(world.get_location("6-2: Hit Gabriel into the ceiling", player),
            lambda state: state.has("Stamina Bar", player, dash1))

    # shop
    set_rule(world.get_location("Shop: Buy Revolver Variant 1", player),
        lambda state: state.has_any({"Revolver - Piercer", "Revolver - Marksman", "Revolver - Sharpshooter"}, player))

    set_rule(world.get_location("Shop: Buy Revolver Variant 2", player),
        lambda state: state.has_any({"Revolver - Piercer", "Revolver - Marksman", "Revolver - Sharpshooter"}, player))
    
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