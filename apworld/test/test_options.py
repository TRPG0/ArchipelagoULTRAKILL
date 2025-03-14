from . import UltrakillTestBase
from ..Items import item_list, group_dict, ItemType
from ..Locations import location_list, LocationType
from typing import List


class TestUnlockLevels(UltrakillTestBase):
    options = { "unlock_type": "levels" }

    def test_unlock_levels(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        for item in [i for i in group_dict["levels"] if not (i == self.world.start_level.full_name or i == self.world.goal_level.full_name)]:
            self.assertIn(item, item_names)

        for item in group_dict["layers"]:
            self.assertNotIn(item, item_names)


class TestUnlockLayers(UltrakillTestBase):
    options = { "unlock_type": "layers" }
    
    def test_unlock_layers(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        for item in group_dict["layers"]:
            self.assertIn(item, item_names)

        for item in group_dict["levels"]:
            self.assertNotIn(item, item_names)


class TestBossDisabled(UltrakillTestBase):
    options = { "boss_rewards": "disabled" }
    
    def test_boss_disabled(self) -> None:
        boss_locations = [l.name for l in location_list if l.type == LocationType.Boss or l.type == LocationType.BossExt]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in boss_locations:
            self.assertNotIn(location, location_names)


class TestBossStandard(UltrakillTestBase):
    options = { "boss_rewards": "standard" }
    
    def test_boss_standard(self) -> None:
        standard_locations = [l.name for l in location_list if l.type == LocationType.Boss]

        ext_locations = [l.name for l in location_list if l.type == LocationType.BossExt]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in standard_locations:
            self.assertIn(location, location_names)

        for location in ext_locations:
            self.assertNotIn(location, location_names)


class TestBossExtended(UltrakillTestBase):
    options = { "boss_rewards": "extended" }
    
    def test_boss_extended(self) -> None:
        boss_locations = [l.name for l in location_list if l.type == LocationType.Boss or l.type == LocationType.BossExt]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in boss_locations:
            self.assertIn(location, location_names)


class TestNotChallenges(UltrakillTestBase):
    options = { "challenge_rewards": "false" }
    
    def test_not_challenges(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Challenge]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)


class TestChallenges(UltrakillTestBase):
    options = { "challenge_rewards": "true" }
    
    def test_challenges(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Challenge]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)


class TestNotPRanks(UltrakillTestBase):
    options = { "p_rank_rewards": "false" }
    
    def test_not_p_ranks(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.PerfectRank]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)


class TestPRanks(UltrakillTestBase):
    options = { 
        "p_rank_rewards": "true",
        "skipped_levels": {}
    }
    
    def test_p_ranks(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.PerfectRank]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)


class TestNotHanks(UltrakillTestBase):
    options = { "hank_rewards": "false" }
    
    def test_not_hanks(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Hank]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)


class TestHanks(UltrakillTestBase):
    options = { "hank_rewards": "true" }
    
    def test_hanks(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Hank]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)


class TestNotClash(UltrakillTestBase):
    options = { "randomize_clash_mode": "false" }
    
    def test_not_clash(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.ClashMode]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)

        self.assertNotIn("Clash Mode", [i.name for i in self.multiworld.get_items()])


class TestClash(UltrakillTestBase):
    options = { "randomize_clash_mode": "true" }
    
    def test_clash(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.ClashMode]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)

        self.assertIn("Clash Mode", [i.name for i in self.multiworld.get_items()])


class TestNotFish(UltrakillTestBase):
    options = { "fish_rewards": "false" }
    
    def test_not_fish(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Fish]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)


class TestFish(UltrakillTestBase):
    options = { "fish_rewards": "true" }
    
    def test_fish(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Fish]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)


class TestNotClean(UltrakillTestBase):
    options = { "cleaning_rewards": "false" }
    
    def test_not_clean(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Clean]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)


class TestClean(UltrakillTestBase):
    options = { "cleaning_rewards": "true" }
    
    def test_clean(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Clean]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)


class TestNotChess(UltrakillTestBase):
    options = { "chess_reward": "false" }
    
    def test_not_chess(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Chess]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)


class TestChess(UltrakillTestBase):
    options = { "chess_reward": "true" }
    
    def test_chess(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Chess]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)


class TestNotRocket(UltrakillTestBase):
    options = { "rocket_race_reward": "false" }
    
    def test_not_rocket(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Rocket]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertNotIn(location, location_names)


class TestRocket(UltrakillTestBase):
    options = { "rocket_race_reward": "true" }
    
    def test_rocket(self) -> None:
        challenge_locations = [l.name for l in location_list if l.type == LocationType.Rocket]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in challenge_locations:
            self.assertIn(location, location_names)


class TestStartWeapon(UltrakillTestBase):
    def test_start_weapon(self) -> None:
        self.assertEqual(sum([i.name == self.world.start_weapon for i in self.multiworld.get_items()]), 1)


class TestNotFire2(UltrakillTestBase):
    options = { "randomize_secondary_fire": "false" }
    
    def test_not_fire2(self) -> None:
        fire2_items = [i.name for i in item_list if i.type == ItemType.Fire2]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in fire2_items:
            self.assertNotIn(item, item_names)


class TestFire2(UltrakillTestBase):
    options = { "randomize_secondary_fire": "true" }
    
    def test_fire2(self) -> None:
        fire2_items = [i.name for i in item_list if i.type == ItemType.Fire2]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in fire2_items:
            self.assertIn(item, item_names)


class TestNotStartArm(UltrakillTestBase):
    options = { "start_with_arm": "false" }
    
    def test_not_start_arm(self) -> None:
        self.assertIn("Feedbacker", [i.name for i in self.multiworld.get_items()])


class TestStartArm(UltrakillTestBase):
    options = { "start_with_arm": "true" }
    
    def test_start_arm(self) -> None:
        self.assertNotIn("Feedbacker", [i.name for i in self.multiworld.get_items()])


class TestStartStamina0(UltrakillTestBase):
    options = { "starting_stamina": 0 }

    def test_start_stamina_0(self) -> None:
        self.assertEqual(len([i.name for i in self.multiworld.get_items() if i.name == "Stamina Bar"]), 3)


class TestStartStamina1(UltrakillTestBase):
    options = { "starting_stamina": 1 }

    def test_start_stamina_1(self) -> None:
        self.assertEqual(len([i.name for i in self.multiworld.get_items() if i.name == "Stamina Bar"]), 2)


class TestStartStamina2(UltrakillTestBase):
    options = { "starting_stamina": 2 }

    def test_start_stamina_2(self) -> None:
        self.assertEqual(len([i.name for i in self.multiworld.get_items() if i.name == "Stamina Bar"]), 1)


class TestStartStamina3(UltrakillTestBase):
    options = { "starting_stamina": 3 }

    def test_start_stamina_3(self) -> None:
        self.assertNotIn("Stamina Bar", [i.name for i in self.multiworld.get_items()])


class TestStartWallJumps0(UltrakillTestBase):
    options = { "starting_walljumps": 0 }

    def test_start_walljumps_0(self) -> None:
        self.assertEqual(len([i.name for i in self.multiworld.get_items() if i.name == "Wall Jump"]), 3)


class TestStartWallJumps1(UltrakillTestBase):
    options = { "starting_walljumps": 1 }

    def test_start_walljumps_1(self) -> None:
        self.assertEqual(len([i.name for i in self.multiworld.get_items() if i.name == "Wall Jump"]), 2)


class TestStartWallJumps2(UltrakillTestBase):
    options = { "starting_walljumps": 2 }

    def test_start_walljumps_2(self) -> None:
        self.assertEqual(len([i.name for i in self.multiworld.get_items() if i.name == "Wall Jump"]), 1)


class TestStartWallJumps3(UltrakillTestBase):
    options = { "starting_walljumps": 3 }

    def test_start_walljumps_3(self) -> None:
        self.assertNotIn("Wall Jump", [i.name for i in self.multiworld.get_items()])


class TestNotStartSlide(UltrakillTestBase):
    options = { "start_with_slide": "false" }

    def test_not_start_with_slide(self) -> None:
        self.assertIn("Slide", [i.name for i in self.multiworld.get_items()])


class TestStartSlide(UltrakillTestBase):
    options = { "start_with_slide": "true" }

    def test_start_with_slide(self) -> None:
        self.assertNotIn("Slide", [i.name for i in self.multiworld.get_items()])


class TestNotStartSlam(UltrakillTestBase):
    options = { "start_with_slam": "false" }

    def test_start_with_slam(self) -> None:
        self.assertIn("Slam", [i.name for i in self.multiworld.get_items()])


class TestStartSlam(UltrakillTestBase):
    options = { "start_with_slam": "true" }

    def test_start_with_slam(self) -> None:
        self.assertNotIn("Slam", [i.name for i in self.multiworld.get_items()])


class TestRevolverStandard(UltrakillTestBase):
    options = { "revolver_form": "standard" }

    def test_revolver_standard(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        self.assertNotIn("Revolver - Standard", item_names)
        self.assertIn("Revolver - Alternate", item_names)


class TestRevolverAlternate(UltrakillTestBase):
    options = { "revolver_form": "alternate" }

    def test_revolver_alternate(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        self.assertIn("Revolver - Standard", item_names)
        self.assertNotIn("Revolver - Alternate", item_names)


class TestShotgunStandard(UltrakillTestBase):
    options = { "shotgun_form": "standard" }

    def test_shotgun_standard(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        self.assertNotIn("Shotgun - Standard", item_names)
        self.assertIn("Shotgun - Alternate", item_names)


class TestShotgunAlternate(UltrakillTestBase):
    options = { "shotgun_form": "alternate" }

    def test_shotgun_alternate(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        self.assertIn("Shotgun - Standard", item_names)
        self.assertNotIn("Shotgun - Alternate", item_names)


class TestNailgunStandard(UltrakillTestBase):
    options = { "nailgun_form": "standard" }

    def test_nailgun_standard(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        self.assertNotIn("Nailgun - Standard", item_names)
        self.assertIn("Nailgun - Alternate", item_names)


class TestNailgunAlternate(UltrakillTestBase):
    options = { "nailgun_form": "alternate" }

    def test_nailgun_alternate(self) -> None:
        item_names = [i.name for i in self.multiworld.get_items()]

        self.assertIn("Nailgun - Standard", item_names)
        self.assertNotIn("Nailgun - Alternate", item_names)


class TestNotSkulls(UltrakillTestBase):
    options = { "randomize_skulls": "false" }

    def test_not_skulls(self) -> None:
        skull_items = [i.name for i in item_list if i.type == ItemType.Skull]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in skull_items:
            self.assertNotIn(item, item_names)


class TestSkulls(UltrakillTestBase):
    options = { "randomize_skulls": "true" }

    def test_skulls(self) -> None:
        skull_items = [i.name for i in item_list if i.type == ItemType.Skull]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in skull_items:
            self.assertIn(item, item_names)


class TestNotLimboSwitches(UltrakillTestBase):
    options = { "randomize_limbo_switches" : "false" }

    def test_not_limbo_switches(self) -> bool:
        switch_items = [i.name for i in item_list if i.type == ItemType.LimboSwitch]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in switch_items:
            self.assertNotIn(item, item_names)

        switch_locations = [l.name for l in location_list if l.type == LocationType.LimboSwitch]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in switch_locations:
            self.assertNotIn(location, location_names)


class TestLimboSwitches(UltrakillTestBase):
    options = { "randomize_limbo_switches" : "true" }

    def test_limbo_switches(self) -> bool:
        switch_items = [i.name for i in item_list if i.type == ItemType.LimboSwitch]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in switch_items:
            self.assertIn(item, item_names)

        switch_locations = [l.name for l in location_list if l.type == LocationType.LimboSwitch]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in switch_locations:
            self.assertIn(location, location_names)


class TestNotViolenceSwitches(UltrakillTestBase):
    options = { "randomize_violence_switches" : "false" }

    def test_violence_switches(self) -> bool:
        switch_items = [i.name for i in item_list if i.type == ItemType.ViolenceSwitch]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in switch_items:
            self.assertNotIn(item, item_names)

        switch_locations = [l.name for l in location_list if l.type == LocationType.ViolenceSwitch]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in switch_locations:
            self.assertNotIn(location, location_names)


class TestViolenceSwitches(UltrakillTestBase):
    options = { "randomize_violence_switches" : "true" }

    def test_violence_switches(self) -> bool:
        switch_items = [i.name for i in item_list if i.type == ItemType.ViolenceSwitch]

        item_names = [i.name for i in self.multiworld.get_items()]

        for item in switch_items:
            self.assertIn(item, item_names)

        switch_locations = [l.name for l in location_list if l.type == LocationType.ViolenceSwitch]

        location_names = [l.name for l in self.multiworld.get_locations()]

        for location in switch_locations:
            self.assertIn(location, location_names)