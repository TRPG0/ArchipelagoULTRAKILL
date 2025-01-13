from typing import List, Dict, Set
from dataclasses import dataclass
from enum import Enum


base_id = 2009000


class ItemType(Enum):
    Weapon = 0
    Fire2 = 1
    Stamina = 2
    WallJump = 3
    Slide = 4
    Slam = 5
    Skull = 6
    Level = 7
    Layer = 8
    Filler = 9
    Trap = 10
    LimboSwitch = 11
    ViolenceSwitch = 12
    ClashMode = 13
    RevStd = 14
    RevAlt = 15
    ShoStd = 16
    ShoAlt = 17
    NaiStd = 18
    NaiAlt = 19


@dataclass
class UKItem:
    name: str
    type: ItemType
    count: int = 1


item_list: List[UKItem] = [
    # Weapons
    UKItem("Revolver - Piercer", ItemType.Weapon),
    UKItem("Revolver - Marksman", ItemType.Weapon),
    UKItem("Revolver - Sharpshooter", ItemType.Weapon),
    UKItem("Revolver - Standard", ItemType.RevStd),
    UKItem("Revolver - Alternate", ItemType.RevAlt),

    UKItem("Shotgun - Core Eject", ItemType.Weapon),
    UKItem("Shotgun - Pump Charge", ItemType.Weapon),
    UKItem("Shotgun - Sawed-On", ItemType.Weapon),
    UKItem("Shotgun - Standard", ItemType.ShoStd),
    UKItem("Shotgun - Alternate", ItemType.ShoAlt),

    UKItem("Nailgun - Attractor", ItemType.Weapon),
    UKItem("Nailgun - Overheat", ItemType.Weapon),
    UKItem("Nailgun - JumpStart", ItemType.Weapon),
    UKItem("Nailgun - Standard", ItemType.NaiStd),
    UKItem("Nailgun - Alternate", ItemType.NaiAlt),

    UKItem("Railcannon - Electric", ItemType.Weapon),
    UKItem("Railcannon - Screwdriver", ItemType.Weapon),
    UKItem("Railcannon - Malicious", ItemType.Weapon),

    UKItem("Rocket Launcher - Freezeframe", ItemType.Weapon),
    UKItem("Rocket Launcher - S.R.S. Cannon", ItemType.Weapon),
    UKItem("Rocket Launcher - Firestarter", ItemType.Weapon),

    # Secondary Fire
    UKItem("Secondary Fire - Piercer", ItemType.Fire2),
    UKItem("Secondary Fire - Marksman", ItemType.Fire2),
    UKItem("Secondary Fire - Sharpshooter", ItemType.Fire2),
    UKItem("Secondary Fire - Core Eject", ItemType.Fire2),
    UKItem("Secondary Fire - Pump Charge", ItemType.Fire2),
    UKItem("Secondary Fire - Sawed-On", ItemType.Fire2),
    UKItem("Secondary Fire - Attractor", ItemType.Fire2),
    UKItem("Secondary Fire - Overheat", ItemType.Fire2),
    UKItem("Secondary Fire - JumpStart", ItemType.Fire2),
    UKItem("Secondary Fire - Freezeframe", ItemType.Fire2),
    UKItem("Secondary Fire - S.R.S. Cannon", ItemType.Fire2),
    UKItem("Secondary Fire - Firestarter", ItemType.Fire2),

    # Arms
    UKItem("Feedbacker", ItemType.Weapon),
    UKItem("Knuckleblaster", ItemType.Weapon),
    UKItem("Whiplash", ItemType.Weapon),

    # Abilities
    UKItem("Stamina Bar", ItemType.Stamina),
    UKItem("Wall Jump", ItemType.WallJump),
    UKItem("Slide", ItemType.Slide),
    UKItem("Slam", ItemType.Slam),

    # Skulls
    UKItem("Blue Skull (0-2)", ItemType.Skull),
    UKItem("Blue Skull (0-S)", ItemType.Skull),
    UKItem("Red Skull (0-S)", ItemType.Skull),
    UKItem("Red Skull (1-1)", ItemType.Skull),
    UKItem("Blue Skull (1-1)", ItemType.Skull),
    UKItem("Blue Skull (1-2)", ItemType.Skull),
    UKItem("Red Skull (1-2)", ItemType.Skull),
    UKItem("Blue Skull (1-3)", ItemType.Skull),
    UKItem("Red Skull (1-3)", ItemType.Skull),
    UKItem("Blue Skull (1-4)", ItemType.Skull, 4),
    UKItem("Blue Skull (2-3)", ItemType.Skull),
    UKItem("Red Skull (2-3)", ItemType.Skull),
    UKItem("Blue Skull (2-4)", ItemType.Skull),
    UKItem("Red Skull (2-4)", ItemType.Skull),
    UKItem("Blue Skull (4-2)", ItemType.Skull),
    UKItem("Red Skull (4-2)", ItemType.Skull),
    UKItem("Blue Skull (4-3)", ItemType.Skull),
    UKItem("Blue Skull (4-4)", ItemType.Skull),
    UKItem("Blue Skull (5-1)", ItemType.Skull, 3),
    UKItem("Blue Skull (5-2)", ItemType.Skull),
    UKItem("Red Skull (5-2)", ItemType.Skull),
    UKItem("Blue Skull (5-3)", ItemType.Skull),
    UKItem("Red Skull (5-3)", ItemType.Skull),
    UKItem("Red Skull (6-1)", ItemType.Skull),
    UKItem("Red Skull (7-1)", ItemType.Skull),
    UKItem("Blue Skull (7-1)", ItemType.Skull),
    UKItem("Red Skull (7-2)", ItemType.Skull),
    UKItem("Red Skull (7-S)", ItemType.Skull),
    UKItem("Blue Skull (7-S)", ItemType.Skull),
    UKItem("Blue Skull (P-2)", ItemType.Skull),

    # Levels
    UKItem("0-1: INTO THE FIRE", ItemType.Level),
    UKItem("0-2: THE MEATGRINDER", ItemType.Level),
    UKItem("0-3: DOUBLE DOWN", ItemType.Level),
    UKItem("0-4: A ONE-MACHINE ARMY", ItemType.Level),
    UKItem("0-5: CERBERUS", ItemType.Level),
    UKItem("1-1: HEART OF THE SUNRISE", ItemType.Level),
    UKItem("1-2: THE BURNING WORLD", ItemType.Level),
    UKItem("1-3: HALLS OF SACRED REMAINS", ItemType.Level),
    UKItem("1-4: CLAIR DE LUNE", ItemType.Level),
    UKItem("2-1: BRIDGEBURNER", ItemType.Level),
    UKItem("2-2: DEATH AT 20,000 VOLTS", ItemType.Level),
    UKItem("2-3: SHEER HEART ATTACK", ItemType.Level),
    UKItem("2-4: COURT OF THE CORPSE KING", ItemType.Level),
    UKItem("3-1: BELLY OF THE BEAST", ItemType.Level),
    UKItem("3-2: IN THE FLESH", ItemType.Level),
    UKItem("4-1: SLAVES TO POWER", ItemType.Level),
    UKItem("4-2: GOD DAMN THE SUN", ItemType.Level),
    UKItem("4-3: A SHOT IN THE DARK", ItemType.Level),
    UKItem("4-4: CLAIR DE SOLEIL", ItemType.Level),
    UKItem("5-1: IN THE WAKE OF POSEIDON", ItemType.Level),
    UKItem("5-2: WAVES OF THE STARLESS SEA", ItemType.Level),
    UKItem("5-3: SHIP OF FOOLS", ItemType.Level),
    UKItem("5-4: LEVIATHAN", ItemType.Level),
    UKItem("6-1: CRY FOR THE WEEPER", ItemType.Level),
    UKItem("6-2: AESTHETICS OF HATE", ItemType.Level),
    UKItem("7-1: GARDEN OF FORKING PATHS", ItemType.Level),
    UKItem("7-2: LIGHT UP THE NIGHT", ItemType.Level),
    UKItem("7-3: NO SOUND, NO MEMORY", ItemType.Level),
    UKItem("7-4: ...LIKE ANTENNAS TO HEAVEN", ItemType.Level),
    UKItem("P-1: SOUL SURVIVOR", ItemType.Level),
    UKItem("P-2: WAIT OF THE WORLD", ItemType.Level),

    # Layers
    UKItem("OVERTURE: THE MOUTH OF HELL", ItemType.Layer),
    UKItem("LAYER 1: LIMBO", ItemType.Layer),
    UKItem("LAYER 2: LUST", ItemType.Layer),
    UKItem("LAYER 3: GLUTTONY", ItemType.Layer),
    UKItem("LAYER 4: GREED", ItemType.Layer),
    UKItem("LAYER 5: WRATH", ItemType.Layer),
    UKItem("LAYER 6: HERESY", ItemType.Layer),
    UKItem("LAYER 7: VIOLENCE", ItemType.Layer),

    # Filler
    UKItem("+10,000P", ItemType.Filler),
    UKItem("Overheal", ItemType.Filler),
    UKItem("Dual Wield", ItemType.Filler),
    UKItem("Infinite Stamina", ItemType.Filler),
    UKItem("Air Jump", ItemType.Filler),
    UKItem("Soap", ItemType.Filler),
    UKItem("Confusing Aura", ItemType.Filler),
    UKItem("Quick Charge", ItemType.Filler),

    # Traps
    UKItem("Hard Damage", ItemType.Trap),
    UKItem("Stamina Limiter", ItemType.Trap),
    UKItem("Wall Jump Limiter", ItemType.Trap),
    UKItem("Weapon Malfunction", ItemType.Trap),
    UKItem("Radiant Aura", ItemType.Trap),
    UKItem("Hands-Free Mode", ItemType.Trap),
    UKItem("Short-Term Sandstorm", ItemType.Trap),

    # Switches
    UKItem("Limbo Switch I", ItemType.LimboSwitch),
    UKItem("Limbo Switch II", ItemType.LimboSwitch),
    UKItem("Limbo Switch III", ItemType.LimboSwitch),
    UKItem("Limbo Switch IV", ItemType.LimboSwitch),
    UKItem("Violence Switch I", ItemType.ViolenceSwitch),
    UKItem("Violence Switch II", ItemType.ViolenceSwitch),
    UKItem("Violence Switch III", ItemType.ViolenceSwitch),

    UKItem("Clash Mode", ItemType.ClashMode)
]


group_dict: Dict[str, Set[str]] = {
    "rev0": {"Revolver - Piercer"},
    "rev1": {"Revolver - Sharpshooter"},
    "rev2": {"Revolver - Marksman"},
    "revalt": {"Revolver - Alternate",
               "Revolver - Standard"},
    "revstd": {"Revolver - Alternate",
               "Revolver - Standard"},
    "sho0": {"Shotgun - Core Eject"},
    "sho1": {"Shotgun - Pump Charge"},
    "sho2": {"Shotgun - Sawed-On"},
    "shoalt": {"Shotgun - Alternate",
               "Shotgun - Standard"},
    "shostd": {"Shotgun - Alternate",
               "Shotgun - Standard"},
    "nai0": {"Nailgun - Attractor"},
    "nai1": {"Nailgun - Overheat"},
    "nai2": {"Nailgun - JumpStart"},
    "naialt": {"Nailgun - Alternate",
               "Nailgun - Standard"},
    "naistd": {"Nailgun - Alternate",
               "Nailgun - Standard"},
    "rai0": {"Railcannon - Electric"},
    "rai1": {"Railcannon - Screwdriver"},
    "rai2": {"Railcannon - Malicious"},
    "rock0": {"Rocket Launcher - Freezeframe"},
    "rock1": {"Rocket Launcher - S.R.S. Cannon"},
    "rock2": {"Rocket Launcher - Firestarter"},
    "arm0": {"Feedbacker"},
    "arm1": {"Knuckleblaster"},
    "arm2": {"Whiplash"},
    "rev0fire2": {"Secondary Fire - Piercer"},
    "rev1fire2": {"Secondary Fire - Sharpshooter"},
    "rev2fire2": {"Secondary Fire - Marksman"},
    "sho0fire2": {"Secondary Fire - Core Eject"},
    "sho1fire2": {"Secondary Fire - Pump Charge"},
    "sho2fire2": {"Secondary Fire - Sawed-On"},
    "nai0fire2": {"Secondary Fire - Attractor"},
    "nai1fire2": {"Secondary Fire - Overheat"},
    "nai2fire2": {"Secondary Fire - JumpStart"},
    "rock0fire2": {"Secondary Fire - Freezeframe"},
    "rock1fire2": {"Secondary Fire - S.R.S. Cannon"},
    "rock2fire2": {"Secondary Fire - Firestarter"},
    "alt fire - piercer": {"Secondary Fire - Piercer"},
    "alternate fire - piercer": {"Secondary Fire - Piercer"},
    "alt fire - sharpshooter": {"Secondary Fire - Sharpshooter"},
    "alternate fire - sharpshooter": {"Secondary Fire - Sharpshooter"},
    "alt fire - marksman": {"Secondary Fire - Marksman"},
    "alternate fire - marksman": {"Secondary Fire - Marksman"},
    "alt fire - core eject": {"Secondary Fire - Core Eject"},
    "alternate fire - core eject": {"Secondary Fire - Core Eject"},
    "alt fire - pump charge": {"Secondary Fire - Pump Charge"},
    "alternate fire - pump charge": {"Secondary Fire - Pump Charge"},
    "alt fire - sawed-on": {"Secondary Fire - Sawed-On"},
    "alternate fire - sawed-on": {"Secondary Fire - Sawed-On"},
    "alt fire - sawed on": {"Secondary Fire - Sawed-On"},
    "alternate fire - sawed on": {"Secondary Fire - Sawed-On"},
    "alt fire - attractor": {"Secondary Fire - Attractor"},
    "alternate fire - attractor": {"Secondary Fire - Attractor"},
    "alt fire - overheat": {"Secondary Fire - Overheat"},
    "alternate fire - overheat": {"Secondary Fire - Overheat"},
    "alt fire - jumpstart": {"Secondary Fire - JumpStart"},
    "alternate fire - jumpstart": {"Secondary Fire - JumpStart"},
    "alt fire - jump start": {"Secondary Fire - JumpStart"},
    "alternate fire - jump start": {"Secondary Fire - JumpStart"},
    "alt fire - freezeframe": {"Secondary Fire - Freezeframe"},
    "alternate fire - freezeframe": {"Secondary Fire - Freezeframe"},
    "alt fire - s.r.s. cannon": {"Secondary Fire - S.R.S. Cannon"},
    "alternate fire - s.r.s. cannon": {"Secondary Fire - S.R.S. Cannon"},
    "alt fire - srs cannon": {"Secondary Fire - S.R.S. Cannon"},
    "alternate fire - srs cannon": {"Secondary Fire - S.R.S. Cannon"},
    "alt fire - firestarter": {"Secondary Fire - Firestarter"},
    "alternate fire - firestarter": {"Secondary Fire - Firestarter"},
    "revolver": {"Revolver - Piercer",
                 "Revolver - Marksman",
                 "Revolver - Sharpshooter"},
    "pistol": {"Revolver - Piercer",
               "Revolver - Marksman",
               "Revolver - Sharpshooter"},
    "shotgun": {"Shotgun - Core Eject",
                "Shotgun - Pump Charge",
                "Shotgun - Sawed-On"},
    "nailgun": {"Nailgun - Attractor",
                "Nailgun - Overheat",
                "Nailgun - JumpStart"},
    "railcannon": {"Railcannon - Electric",
                   "Railcannon - Screwdriver",
                   "Railcannon - Malicious"},
    "railgun": {"Railcannon - Electric",
                "Railcannon - Screwdriver",
                "Railcannon - Malicious"},
    "rocket": {"Rocket Launcher - Freezeframe",
               "Rocket Launcher - S.R.S. Cannon",
               "Rocket Launcher - Firestarter"},
    "rocket launcher": {"Rocket Launcher - Freezeframe",
                        "Rocket Launcher - S.R.S. Cannon",
                        "Rocket Launcher - Firestarter"},
    "rpg": {"Rocket Launcher - Freezeframe",
            "Rocket Launcher - S.R.S. Cannon",
            "Rocket Launcher - Firestarter"},
    "junk": {"+10,000P",
             "Overheal",
             "Dual Wield",
             "Infinite Stamina",
             "Air Jump",
             "Soap",
             "Hard Damage",
             "Stamina Limiter",
             "Wall Jump Limiter",
             "Weapon Malfunction",
             "Radiant Aura",
             "Confusing Aura",
             "Quick Charge",
             "Hands-Free Mode",
             "Short-Term Sandstorm"},
    "filler": {"+10,000P",
               "Overheal",
               "Dual Wield",
               "Infinite Stamina",
               "Air Jump",
               "Soap", 
               "Confusing Aura",
               "Quick Charge"},
    "trap": {"Hard Damage",
             "Stamina Limiter",
             "Wall Jump Limiter",
             "Weapon Malfunction",
             "Radiant Aura",
             "Hands-Free Mode",
             "Short-Term Sandstorm"},
    "dash": {"Stamina Bar"},
    "walljump": {"Wall Jump"},
    "levels": {"0-1: INTO THE FIRE",
               "0-2: THE MEATGRINDER",
               "0-3: DOUBLE DOWN",
               "0-4: A ONE-MACHINE ARMY",
               "0-5: CERBERUS",
               "1-1: HEART OF THE SUNRISE",
               "1-2: THE BURNING WORLD",
               "1-3: HALLS OF SACRED REMAINS",
               "1-4: CLAIR DE LUNE",
               "2-1: BRIDGEBURNER",
               "2-2: DEATH AT 20,000 VOLTS",
               "2-3: SHEER HEART ATTACK",
               "2-4: COURT OF THE CORPSE KING",
               "3-1: BELLY OF THE BEAST",
               "3-2: IN THE FLESH",
               "4-1: SLAVES TO POWER",
               "4-2: GOD DAMN THE SUN",
               "4-3: A SHOT IN THE DARK",
               "4-4: CLAIR DE SOLEIL",
               "5-1: IN THE WAKE OF POSEIDON",
               "5-2: WAVES OF THE STARLESS SEA",
               "5-3: SHIP OF FOOLS",
               "5-4: LEVIATHAN",
               "6-1: CRY FOR THE WEEPER",
               "6-2: AESTHETICS OF HATE",
               "7-1: GARDEN OF FORKING PATHS",
               "7-2: LIGHT UP THE NIGHT",
               "7-3: NO SOUND, NO MEMORY",
               "7-4: ...LIKE ANTENNAS TO HEAVEN"},
    "layers": {"OVERTURE: THE MOUTH OF HELL",
               "LAYER 1: LIMBO",
               "LAYER 2: LUST",
               "LAYER 3: GLUTTONY",
               "LAYER 4: GREED",
               "LAYER 5: WRATH",
               "LAYER 6: HERESY",
               "LAYER 7: VIOLENCE"},
    "start_weapons": {"Revolver - Piercer",
                      "Revolver - Marksman",
                      "Revolver - Sharpshooter",
                      "Shotgun - Core Eject",
                      "Shotgun - Pump Charge",
                      "Shotgun - Sawed-On",
                      "Nailgun - Attractor",
                      "Nailgun - Overheat",
                      "Nailgun - JumpStart",
                      "Railcannon - Electric",
                      "Railcannon - Screwdriver",
                      "Railcannon - Malicious",
                      "Rocket Launcher - Freezeframe",
                      "Rocket Launcher - S.R.S. Cannon",
                      "Rocket Launcher - Firestarter",
                      "Feedbacker",
                      "Knuckleblaster",
                      "Whiplash"},
    "0-1": {"0-1: INTO THE FIRE"},
    "0-2": {"0-2: THE MEATGRINDER"},
    "0-3": {"0-3: DOUBLE DOWN"},
    "0-4": {"0-4: A ONE-MACHINE ARMY"},
    "0-5": {"0-5: CERBERUS"},
    "1-1": {"1-1: HEART OF THE SUNRISE"},
    "1-2": {"1-2: THE BURNING WORLD"},
    "1-3": {"1-3: HALLS OF SACRED REMAINS"},
    "1-4": {"1-4: CLAIR DE LUNE"},
    "2-1": {"2-1: BRIDGEBURNER"},
    "2-2": {"2-2: DEATH AT 20,000 VOLTS"},
    "2-3": {"2-3: SHEER HEART ATTACK"},
    "2-4": {"2-4: COURT OF THE CORPSE KING"},
    "3-1": {"3-1: BELLY OF THE BEAST"},
    "3-2": {"3-2: IN THE FLESH"},
    "4-1": {"4-1: SLAVES TO POWER"},
    "4-2": {"4-2: GOD DAMN THE SUN"},
    "4-3": {"4-3: A SHOT IN THE DARK"},
    "4-4": {"4-4: CLAIR DE SOLEIL"},
    "5-1": {"5-1: IN THE WAKE OF POSEIDON"},
    "5-2": {"5-2: WAVES OF THE STARLESS SEA"},
    "5-3": {"5-3: SHIP OF FOOLS"},
    "5-4": {"5-4: LEVIATHAN"},
    "6-1": {"6-1: CRY FOR THE WEEPER"},
    "6-2": {"6-2: AESTHETICS OF HATE"},
    "7-1": {"7-1: GARDEN OF FORKING PATHS"},
    "7-2": {"7-2: LIGHT UP THE NIGHT"},
    "7-3": {"7-3: NO SOUND, NO MEMORY"},
    "7-4": {"7-4: ...LIKE ANTENNAS TO HEAVEN"},
    "P-1": {"P-1: SOUL SURVIVOR"},
    "P-2": {"P-2: WAIT OF THE WORLD"},
    "overture": {"OVERTURE: THE MOUTH OF HELL"},
    "prologue": {"OVERTURE: THE MOUTH OF HELL"},
    "prelude": {"OVERTURE: THE MOUTH OF HELL"},
    "layer 0": {"OVERTURE: THE MOUTH OF HELL"},
    "layer 1": {"LAYER 1: LIMBO"},
    "limbo": {"LAYER 1: LIMBO"},
    "layer 2": {"LAYER 2: LUST"},
    "lust": {"LAYER 2: LUST"},
    "layer 3": {"LAYER 3: GLUTTONY"},
    "gluttony": {"LAYER 3: GLUTTONY"},
    "layer 4": {"LAYER 4: GREED"},
    "greed": {"LAYER 4: GREED"},
    "layer 5": {"LAYER 5: WRATH"},
    "wrath": {"LAYER 5: WRATH"},
    "layer 6": {"LAYER 6: HERESY"},
    "heresy": {"LAYER 6: HERESY"},
    "layer 7": {"LAYER 7: VIOLENCE"},
    "violence": {"LAYER 7: VIOLENCE"},
}