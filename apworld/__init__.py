from typing import Dict, List, Any
from BaseClasses import Region, Location, Item, Tutorial, ItemClassification
from worlds.AutoWorld import World, WebWorld
from .Items import base_id, item_table, group_table
from .Locations import location_table, event_table, ext_bosses, limbo_switches, violence_switches
from .Regions import region_table, secret_levels
from .Rules import rules
from .Options import UltrakillOptions, Goal, UnlockType, StartingWeapon
from .Music import multilayer_music, singlelayer_music, ordered_list


class UltrakillWeb(WebWorld):
    # theme = ""
    tutorials = [Tutorial(
        "Multiworld Setup Guide",
        "A guide to setting up ULTRAKILL randomizer and connecting to an Archipelago Multiworld",
        "English",
        "setup_en.md",
        "setup/en",
        ["TRPG"]
    )]


class UltrakillWorld(World):
    """MANKIND IS DEAD. BLOOD IS FUEL. HELL IS FULL."""

    game = "ULTRAKILL"
    web = UltrakillWeb()

    item_name_to_id = {item["name"]: (base_id + index) for index, item in enumerate(item_table)}
    location_name_to_id = {loc["name"]: (base_id + index) for index, loc in enumerate(location_table)}
    location_name_to_game_id = {loc["name"]: loc["game_id"] for loc in location_table}

    item_name_groups = group_table
    options_dataclass = UltrakillOptions
    options: UltrakillOptions

    goal_name = "6-2"
    start_weapon = "Revolver - Piercer"
    music: Dict[str, str] = {}

    def set_rules(self):    
        rules(self)


    def create_item(self, name: str) -> "UltrakillItem":
        item_id: int = self.item_name_to_id[name]
        id = item_id - base_id
        classification = item_table[id]["classification"]
        if name == "Blue Skull (1-4)" and self.options.hank_rewards:
            classification = ItemClassification.progression

        return UltrakillItem(name, classification, item_id, self.player)
    

    def create_event(self, event: str):
        return UltrakillItem(event, ItemClassification.progression_skip_balancing, None, self.player)
    

    def generate_early(self):
        if not self.options.include_secret_mission_completion and self.options.goal_requirement.value > 28:
            print(f"[ULTRAKILL - '{self.multiworld.get_player_name(self.player)}'] "
                  "Secret mission completion is disabled. Goal requirement lowered to 28.")
            self.options.goal_requirement.value = 28

        if self.options.starting_weapon != StartingWeapon.option_revolver:
            weapons: List[str] = []
            
            if self.options.starting_weapon != StartingWeapon.option_any_arm:
                weapons = list(group_table["start_weapons"])
            else:
                weapons = [
                    "Feedbacker",
                    "Knuckleblaster"
                ]

            if self.options.starting_weapon != StartingWeapon.option_revolver and not self.options.start_with_arm:
                if "Railcannon - Electric" in weapons:
                    weapons.remove("Railcannon - Electric")
                if "Railcannon - Screwdriver" in weapons:
                    weapons.remove("Railcannon - Screwdriver")
                if "Railcannon - Malicious" in weapons:
                    weapons.remove("Railcannon - Malicious")
                if "Rocket Launcher - Freezeframe" in weapons:
                    weapons.remove("Rocket Launcher - Freezeframe")
                if "Rocket Launcher - Firestarter" in weapons:
                    weapons.remove("Rocket Launcher - Firestarter")

            if self.options.start_with_arm:
                if "Feedbacker" in weapons:
                    weapons.remove("Feedbacker")

            if not self.options.start_with_arm and self.options.starting_weapon == StartingWeapon.option_any_weapon:
                if "Feedbacker" in weapons:
                    weapons.remove("Feedbacker")

            if self.options.starting_weapon == StartingWeapon.option_any_weapon:
                if "Knuckleblaster" in weapons:
                    weapons.remove("Knuckleblaster")

            if self.options.randomize_secondary_fire:
                if "Rocket Launcher - S.R.S. Cannon" in weapons:
                    weapons.remove("Rocket Launcher - S.R.S. Cannon")

                if self.options.nailgun_form and "Nailgun - Overheat" in weapons:
                    weapons.remove("Nailgun - Overheat")
            
            self.start_weapon = self.random.choice(weapons)

        if self.options.music_randomizer:
            tempDict: Dict[str, str] = {}
            multi1 = []
            multi1.extend(multilayer_music.keys())
            multi2 = [] 
            multi2.extend(multilayer_music.keys())

            while len(multi1) > 0:
                id1 = self.random.choice(multi1)
                id2 = self.random.choice(multi2)

                tempDict[id1] = id2
                multi1.remove(id1)
                multi2.remove(id2)

            single1 = []
            single1.extend(singlelayer_music.keys())
            single2 = []
            single2.extend(singlelayer_music.keys())

            while len(single1) > 0:
                id1 = self.random.choice(single1)
                id2 = self.random.choice(single2)

                tempDict[id1] = id2
                single1.remove(id1)
                single2.remove(id2)

            self.music = {i: tempDict[i] for i in ordered_list}
            #print(self.music)

    
    def write_spoiler_header(self, spoiler_handle):
        if self.options.music_randomizer:
            spoiler_handle.write("\nMusic:\n")
            for i, j in self.music.items():
                original: str
                changed: str
                if i in multilayer_music.keys():
                    original = multilayer_music[i]
                elif i in singlelayer_music.keys():
                    original = singlelayer_music[i]
                if j in multilayer_music.keys():
                    changed = multilayer_music[j]
                elif j in singlelayer_music.keys():
                    changed = singlelayer_music[j]
                spoiler_handle.write(f"({i}) {original} -> {changed}\n")


    def create_items(self):
        pool = []

        for item in item_table:
            count = 1

            if item["name"] == self.start_weapon:
                continue
            if self.options.unlock_type == UnlockType.option_levels and \
                item["name"] in group_table["layers"] or \
                    (self.goal_name in item["name"] and not "Skull" in item["name"]):
                        continue
            elif self.options.unlock_type == UnlockType.option_layers and item["name"] in group_table["levels"]:
                continue
            if not self.options.randomize_skulls and "Skull" in item["name"]:
                continue
            if not self.options.randomize_secondary_fire and "Secondary Fire" in item["name"]:
                continue
            if self.options.start_with_arm and item["name"] == "Feedbacker":
                continue
            if self.options.start_with_slide and item["name"] == "Slide":
                continue
            if self.options.start_with_slam and item["name"] == "Slam":
                continue
            if self.options.revolver_form == 0 and item["name"] == "Revolver - Standard":
                continue
            if self.options.revolver_form == 1 and item["name"] == "Revolver - Alternate":
                continue
            if self.options.shotgun_form == 0 and item["name"] == "Shotgun - Standard":
                continue
            if self.options.shotgun_form == 1 and item["name"] == "Shotgun - Alternate":
                continue
            if self.options.nailgun_form == 0 and item["name"] == "Nailgun - Standard":
                continue
            if self.options.nailgun_form == 1 and item["name"] == "Nailgun - Alternate":
                continue
            if not self.options.randomize_limbo_switches and "Limbo Switch" in item["name"]:
                continue
            if not self.options.randomize_violence_switches and "Violence Switch" in item["name"]:
                continue
            if not self.options.randomize_clash_mode and item["name"] == "Clash Mode":
                continue
            if item["name"] in group_table["junk"]:
                continue

            if item["name"] == "Stamina Bar":
                count = 3 - self.options.starting_stamina.value
            if item["name"] == "Wall Jump":
                count = 3 - self.options.starting_walljumps.value
            if self.options.randomize_skulls and item["name"] == "Blue Skull (1-4)":
                count = 4
            if self.options.randomize_skulls and item["name"] == "Blue Skull (5-1)":
                count = 3

            if count <= 0:
                continue
            else:
                for _ in range(count):
                    pool.append(self.create_item(item["name"]))
        
        junk: int = len(self.multiworld.get_unfilled_locations(self.player)) - (len(pool) + 1)
        #print("unfilled = " + str(len(self.multiworld.get_unfilled_locations(self.player))))
        #print("junk = " + str(junk))

        trap: int = round(junk * (self.options.trap_percent / 100))
        filler: int = junk - trap
        #print("trap = " + str(trap))
        #print("filler = " + str(filler))

        for _ in range(trap):
            pool.append(self.create_item(self.random.choice(list(group_table["trap"]))))

        for _ in range(filler):
            pool.append(self.create_item(self.random.choice(list(group_table["filler"]))))
            
        self.multiworld.itempool += pool


    def pre_fill(self):
        world = self.multiworld
        player = self.player

        first_loc = world.get_location("0-1: Weapon", player)

        if first_loc.item != None:
            if not first_loc.item.name in group_table["start_weapons"]:
                raise Exception(f"[ULTRAKILL - {world.get_player_name(player)}] "
                                f"'{first_loc.item.name}' is not a valid starting weapon.")
            
            if first_loc.item.name != self.start_weapon:
                print(f"[ULTRAKILL - '{world.get_player_name(player)}'] An item already exists at \"0-1: Weapon\". "
                    "Selected starting weapon is being returned to the item pool.")
                
                world.itempool.append(self.create_item(self.start_weapon))
        else:
            first_loc.place_locked_item(self.create_item(self.start_weapon))


    def create_regions(self):
        
        player = self.player
        multiworld = self.multiworld

        menu = Region("Menu", player, multiworld)

        for number, name in region_table.items():
            multiworld.regions += [Region(name, player, multiworld)]
            if "S" in number:
                multiworld.get_region(region_table[secret_levels[number]], player).add_exits({name})
            else:
                menu.add_exits({name})

        multiworld.regions.append(menu)

        if self.options.goal == Goal.option_1_4:
            self.goal_name = "1-4"
        elif self.options.goal == Goal.option_2_4:
            self.goal_name = "2-4"
        elif self.options.goal == Goal.option_3_2:
            self.goal_name = "3-2"
        elif self.options.goal == Goal.option_4_4:
            self.goal_name = "4-4"
        elif self.options.goal == Goal.option_5_4:
            self.goal_name = "5-4"
        elif self.options.goal == Goal.option_6_2:
            self.goal_name = "6-2"
        elif self.options.goal == Goal.option_7_4:
            self.goal_name = "7-4"
        elif self.options.goal == Goal.option_P_1:
            self.goal_name = "P-1"
        elif self.options.goal == Goal.option_P_2:
            self.goal_name = "P-2"

        for loc in location_table:
            if self.goal_name in loc["name"] and not "_w" in loc["game_id"]:
                continue
            elif "_b" in loc["game_id"] and self.options.boss_rewards.value == 0:
                continue
            elif loc["name"] in ext_bosses and self.options.boss_rewards.value < 2:
                continue
            elif "_c" in loc["game_id"] and not self.options.challenge_rewards:
                continue
            elif "_p" in loc["game_id"] and not self.options.p_rank_rewards:
                continue
            elif "fish" in loc["game_id"] and not self.options.fish_rewards:
                continue
            elif loc["name"] in limbo_switches and not self.options.randomize_limbo_switches:
                continue
            elif loc["name"] in violence_switches and not self.options.randomize_violence_switches:
                continue
            elif loc["game_id"] == "clash" and not self.options.randomize_clash_mode:
                continue
            elif loc["game_id"] == "chess" and not self.options.chess_reward:
                continue
            elif loc["game_id"] == "rr" and not self.options.rocket_race_reward:
                continue
            elif "_ha" in loc["game_id"] and not self.options.hank_rewards:
                continue
            else:
                id = base_id + location_table.index(loc)
                region: Region = multiworld.get_region(region_table[loc["region"]], player)
                location: UltrakillLocation = UltrakillLocation(player, loc["name"], id, region)
                #print(location.name + ", " + region.name)
                region.locations.append(location)

        for loc in event_table:
            if "P-" in loc["name"] and not self.goal_name in loc["name"]:
                continue

            if "-S" in loc["name"] and not self.options.include_secret_mission_completion:
                continue

            region: Region = multiworld.get_region(region_table[loc["region"]], player)

            location: UltrakillLocation = UltrakillLocation(player, loc["name"], None, region)
            if not self.goal_name in loc["name"]:
                location.place_locked_item(self.create_event("Level Completed"))
            region.locations.append(location)

        victory: Location = multiworld.get_location("Cleared " + self.goal_name, player)
        victory.place_locked_item(self.create_event("Victory"))

        multiworld.completion_condition[player] = lambda state: state.has("Victory", player)


    def fill_slot_data(self) -> Dict[str, Any]:
        locations = {}
        for loc in self.multiworld.get_locations(self.player):
            if loc.address != None:
                locations[self.location_name_to_game_id[loc.name]] = self.location_name_to_id[loc.name]


        slot_data: Dict[str, Any] = {
            "locations": locations,
            "goal": self.options.goal.value,
            "goal_requirement": self.options.goal_requirement.value,
            "boss_rewards": self.options.boss_rewards.value,
            "challenge_rewards": bool(self.options.challenge_rewards),
            "p_rank_rewards": bool(self.options.p_rank_rewards),
            "hank_rewards": bool(self.options.hank_rewards),
            "randomize_clash_mode": bool(self.options.randomize_clash_mode),
            "fish_rewards": bool(self.options.fish_rewards),
            "cleaning_rewards": bool(self.options.cleaning_rewards),
            "chess_reward": bool(self.options.chess_reward),
            "rocket_race_reward": bool(self.options.rocket_race_reward),
            "randomize_secondary_fire": bool(self.options.randomize_secondary_fire),
            "start_with_arm": bool(self.options.start_with_arm),
            "starting_stamina": self.options.starting_stamina.value,
            "starting_walljumps": self.options.starting_walljumps.value,
            "start_with_slide": bool(self.options.start_with_slide),
            "start_with_slam": bool(self.options.start_with_slam),
            "revolver_form": self.options.revolver_form.value,
            "shotgun_form": self.options.shotgun_form.value,
            "nailgun_form": self.options.nailgun_form.value,
            "randomize_skulls": bool(self.options.randomize_skulls),
            "randomize_limbo_switches": bool(self.options.randomize_limbo_switches),
            "randomize_violence_switches": bool(self.options.randomize_violence_switches),
            "point_multiplier": self.options.point_multiplier.value,
            "ui_color_randomizer": self.options.ui_color_randomizer.value,
            "gun_color_randomizer": self.options.gun_color_randomizer.value,
            "music_randomizer": bool(self.options.music_randomizer),
            "music": self.music,
            "cybergrind_hints": bool(self.options.cybergrind_hints),
            "death_link": bool(self.options.death_link),
        }
        return slot_data


class UltrakillItem(Item):
    game: str = "ULTRAKILL"


class UltrakillLocation(Location):
    game: str = "ULTRAKILL"

