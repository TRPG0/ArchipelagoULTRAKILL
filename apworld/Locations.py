from typing import List, Dict
from enum import Enum
from dataclasses import dataclass
from .Regions import Regions, UKRegion


class LocationType(Enum):
    Normal = 0
    Challenge = 1
    PerfectRank = 2
    Boss = 3
    BossExt = 4
    Fish = 5
    Clean = 6
    LimboSwitch = 7
    ViolenceSwitch = 8
    ClashMode = 9
    Chess = 10
    Rocket = 11
    Hank = 12


@dataclass
class UKLocation:
    name: str
    region: UKRegion
    game_id: str
    type: LocationType


location_list: List[UKLocation] = [
    # 0-1: INTO THE FIRE
    UKLocation("0-1: Weapon", Regions.l1, "1_w1", LocationType.Normal),
    UKLocation("0-1: Secret #1", Regions.l1, "1_s1", LocationType.Normal),
    UKLocation("0-1: Secret #2", Regions.l1, "1_s2", LocationType.Normal),
    UKLocation("0-1: Secret #3", Regions.l1, "1_s3", LocationType.Normal),
    UKLocation("0-1: Secret #4", Regions.l1, "1_s4", LocationType.Normal),
    UKLocation("0-1: Secret #5", Regions.l1, "1_s5", LocationType.Normal),
    UKLocation("0-1: Get 5 kills with a single glass panel", Regions.l1, "1_c", LocationType.Challenge),
    UKLocation("0-1: Perfect Rank", Regions.l1, "1_p", LocationType.PerfectRank),

    # 0-2: THE MEATGRINDER
    UKLocation("0-2: Secret #1", Regions.l2, "2_s1", LocationType.Normal),
    UKLocation("0-2: Secret #2", Regions.l2, "2_s2", LocationType.Normal),
    UKLocation("0-2: Secret #3", Regions.l2, "2_s3", LocationType.Normal),
    UKLocation("0-2: Secret #4", Regions.l2, "2_s4", LocationType.Normal),
    UKLocation("0-2: Secret #5", Regions.l2, "2_s5", LocationType.Normal),
    UKLocation("0-2: Beat the secret encounter", Regions.l2, "2_c", LocationType.Challenge),
    UKLocation("0-2: Perfect Rank", Regions.l2, "2_p", LocationType.PerfectRank),

    # 0-3: DOUBLE DOWN
    UKLocation("0-3: Weapon", Regions.l3, "3_w1", LocationType.Normal),
    UKLocation("0-3: Secret #1", Regions.l3, "3_s1", LocationType.Normal),
    UKLocation("0-3: Secret #2", Regions.l3, "3_s2", LocationType.Normal),
    UKLocation("0-3: Secret #3", Regions.l3, "3_s3", LocationType.Normal),
    UKLocation("0-3: Secret #4", Regions.l3, "3_s4", LocationType.Normal),
    UKLocation("0-3: Secret #5", Regions.l3, "3_s5", LocationType.Normal),
    UKLocation("0-3: Kill only 1 enemy", Regions.l3, "3_c", LocationType.Challenge),
    UKLocation("0-3: Perfect Rank", Regions.l3, "3_p", LocationType.PerfectRank),

    # 0-4: A ONE-MACHINE ARMY
    UKLocation("0-4: Secret #1", Regions.l4, "4_s1", LocationType.Normal),
    UKLocation("0-4: Secret #2", Regions.l4, "4_s2", LocationType.Normal),
    UKLocation("0-4: Secret #3", Regions.l4, "4_s3", LocationType.Normal),
    UKLocation("0-4: Secret #4", Regions.l4, "4_s4", LocationType.Normal),
    UKLocation("0-4: Secret #5", Regions.l4, "4_s5", LocationType.Normal),
    UKLocation("0-4: Slide uninterrupted for 17 seconds", Regions.l4, "4_c", LocationType.Challenge),
    UKLocation("0-4: Perfect Rank", Regions.l4, "4_p", LocationType.PerfectRank),

    # 0-5: CERBERUS
    UKLocation("0-5: Defeat the Cerberi", Regions.l5, "5_b", LocationType.Boss),
    UKLocation("0-5: Don't inflict fatal damage to any enemy", Regions.l5, "5_c", LocationType.Challenge),
    UKLocation("0-5: Perfect Rank", Regions.l5, "5_p", LocationType.PerfectRank),

    # 1-1: HEART OF THE SUNRISE
    UKLocation("1-1: Weapon", Regions.l6, "6_w1", LocationType.Normal),
    UKLocation("1-1: Secret #1", Regions.l6, "6_s1", LocationType.Normal),
    UKLocation("1-1: Secret #2", Regions.l6, "6_s2", LocationType.Normal),
    UKLocation("1-1: Secret #3", Regions.l6, "6_s3", LocationType.Normal),
    UKLocation("1-1: Secret #4", Regions.l6, "6_s4", LocationType.Normal),
    UKLocation("1-1: Secret #5", Regions.l6, "6_s5", LocationType.Normal),
    UKLocation("1-1: Switch", Regions.l6, "6_sw", LocationType.LimboSwitch),
    UKLocation("1-1: Complete the level in under 10 seconds", Regions.l6, "6_c", LocationType.Challenge),
    UKLocation("1-1: Perfect Rank", Regions.l6, "6_p", LocationType.PerfectRank),

    # 1-2: THE BURNING WORLD
    UKLocation("1-2: Secret #1", Regions.l7, "7_s1", LocationType.Normal),
    UKLocation("1-2: Secret #2", Regions.l7, "7_s2", LocationType.Normal),
    UKLocation("1-2: Secret #3", Regions.l7, "7_s3", LocationType.Normal),
    UKLocation("1-2: Secret #4", Regions.l7, "7_s4", LocationType.Normal),
    UKLocation("1-2: Secret #5", Regions.l7, "7_s5", LocationType.Normal),
    UKLocation("1-2: Switch", Regions.l7, "7_sw", LocationType.LimboSwitch),
    UKLocation("1-2: Defeat the Very Cancerous Rodent", Regions.l7, "7_b", LocationType.BossExt),
    UKLocation("1-2: Do not pick up any skulls", Regions.l7, "7_c", LocationType.Challenge),
    UKLocation("1-2: Perfect Rank", Regions.l7, "7_p", LocationType.PerfectRank),

    # 1-3: HALLS OF SACRED REMAINS
    UKLocation("1-3: Secret #1", Regions.l8, "8_s1", LocationType.Normal),
    UKLocation("1-3: Secret #2", Regions.l8, "8_s2", LocationType.Normal),
    UKLocation("1-3: Secret #3", Regions.l8, "8_s3", LocationType.Normal),
    UKLocation("1-3: Secret #4", Regions.l8, "8_s4", LocationType.Normal),
    UKLocation("1-3: Secret #5", Regions.l8, "8_s5", LocationType.Normal),
    UKLocation("1-3: Switch", Regions.l8, "8_sw", LocationType.LimboSwitch),
    UKLocation("1-3: Beat the secret encounter", Regions.l8, "8_c", LocationType.Challenge),
    UKLocation("1-3: Perfect Rank", Regions.l8, "8_p", LocationType.PerfectRank),

    # 1-4: CLAIR DE LUNE
    UKLocation("1-4: Switch", Regions.l9, "9_sw", LocationType.LimboSwitch),
    UKLocation("1-4: Assemble Hank", Regions.l9, "9_ha", LocationType.Hank),
    UKLocation("1-4: V2's Arm", Regions.l9, "9_w1", LocationType.Normal),
    UKLocation("1-4: Secret Weapon", Regions.l9, "9_w2", LocationType.Normal),
    UKLocation("1-4: Defeat V2", Regions.l9, "9_b", LocationType.Boss),
    UKLocation("1-4: Do not pick up any skulls", Regions.l9, "9_c", LocationType.Challenge),
    UKLocation("1-4: Perfect Rank", Regions.l9, "9_p", LocationType.PerfectRank),

    # 2-1: BRIDGEBURNER
    UKLocation("2-1: Secret #1", Regions.l10, "10_s1", LocationType.Normal),
    UKLocation("2-1: Secret #2", Regions.l10, "10_s2", LocationType.Normal),
    UKLocation("2-1: Secret #3", Regions.l10, "10_s3", LocationType.Normal),
    UKLocation("2-1: Secret #4", Regions.l10, "10_s4", LocationType.Normal),
    UKLocation("2-1: Secret #5", Regions.l10, "10_s5", LocationType.Normal),
    UKLocation("2-1: Don't open any normal doors", Regions.l10, "10_c", LocationType.Challenge),
    UKLocation("2-1: Perfect Rank", Regions.l10, "10_p", LocationType.PerfectRank),

    # 2-2: DEATH AT 20,000 VOLTS
    UKLocation("2-2: Weapon", Regions.l11, "11_w1", LocationType.Normal),
    UKLocation("2-2: Secret #1", Regions.l11, "11_s1", LocationType.Normal),
    UKLocation("2-2: Secret #2", Regions.l11, "11_s2", LocationType.Normal),
    UKLocation("2-2: Secret #3", Regions.l11, "11_s3", LocationType.Normal),
    UKLocation("2-2: Secret #4", Regions.l11, "11_s4", LocationType.Normal),
    UKLocation("2-2: Secret #5", Regions.l11, "11_s5", LocationType.Normal),
    UKLocation("2-2: Beat the level in under 60 seconds", Regions.l11, "11_c", LocationType.Challenge),
    UKLocation("2-2: Perfect Rank", Regions.l11, "11_p", LocationType.PerfectRank),

    # 2-3: SHEER HEART ATTACK
    UKLocation("2-3: Secret #1", Regions.l12, "12_s1", LocationType.Normal),
    UKLocation("2-3: Secret #2", Regions.l12, "12_s2", LocationType.Normal),
    UKLocation("2-3: Secret #3", Regions.l12, "12_s3", LocationType.Normal),
    UKLocation("2-3: Secret #4", Regions.l12, "12_s4", LocationType.Normal),
    UKLocation("2-3: Secret #5", Regions.l12, "12_s5", LocationType.Normal),
    UKLocation("2-3: Don't touch any water", Regions.l12, "12_c", LocationType.Challenge),
    UKLocation("2-3: Perfect Rank", Regions.l12, "12_p", LocationType.PerfectRank),

    # 2-4: COURT OF THE CORPSE KING
    UKLocation("2-4: Defeat the Corpse of King Minos", Regions.l13, "13_b", LocationType.Boss),
    UKLocation("2-4: Parry a punch", Regions.l13, "13_c", LocationType.Challenge),
    UKLocation("2-4: Perfect Rank", Regions.l13, "13_p", LocationType.PerfectRank),

    # 3-1: BELLY OF THE BEAST
    UKLocation("3-1: Secret #1", Regions.l14, "14_s1", LocationType.Normal),
    UKLocation("3-1: Secret #2", Regions.l14, "14_s2", LocationType.Normal),
    UKLocation("3-1: Secret #3", Regions.l14, "14_s3", LocationType.Normal),
    UKLocation("3-1: Secret #4", Regions.l14, "14_s4", LocationType.Normal),
    UKLocation("3-1: Secret #5", Regions.l14, "14_s5", LocationType.Normal),
    UKLocation("3-1: Kill a mindflayer with acid", Regions.l14, "14_c", LocationType.Challenge),
    UKLocation("3-1: Perfect Rank", Regions.l14, "14_p", LocationType.PerfectRank),

    # 3-2: IN THE FLESH
    UKLocation("3-2: Defeat Gabriel", Regions.l15, "15_b", LocationType.Boss),
    UKLocation("3-2: Drop Gabriel in a pit", Regions.l15, "15_c", LocationType.Challenge),
    UKLocation("3-2: Perfect Rank", Regions.l15, "15_p", LocationType.PerfectRank),

    # 4-1: SLAVES TO POWER
    UKLocation("4-1: Secret #1", Regions.l16, "16_s1", LocationType.Normal),
    UKLocation("4-1: Secret #2", Regions.l16, "16_s2", LocationType.Normal),
    UKLocation("4-1: Secret #3", Regions.l16, "16_s3", LocationType.Normal),
    UKLocation("4-1: Secret #4", Regions.l16, "16_s4", LocationType.Normal),
    UKLocation("4-1: Secret #5", Regions.l16, "16_s5", LocationType.Normal),
    UKLocation("4-1: Don't activate any enemies", Regions.l16, "16_c", LocationType.Challenge),
    UKLocation("4-1: Perfect Rank", Regions.l16, "16_p", LocationType.PerfectRank),

    # 4-2: GOD DAMN THE SUN
    UKLocation("4-2: Secret #1", Regions.l17, "17_s1", LocationType.Normal),
    UKLocation("4-2: Secret #2", Regions.l17, "17_s2", LocationType.Normal),
    UKLocation("4-2: Secret #3", Regions.l17, "17_s3", LocationType.Normal),
    UKLocation("4-2: Secret #4", Regions.l17, "17_s4", LocationType.Normal),
    UKLocation("4-2: Secret #5", Regions.l17, "17_s5", LocationType.Normal),
    UKLocation("4-2: Kill the Insurrectionist in under 10 seconds", Regions.l17, "17_c", LocationType.Challenge),
    UKLocation("4-2: Perfect Rank", Regions.l17, "17_p", LocationType.PerfectRank),

    # 4-S: CLASH OF THE BRANDICOOT
    UKLocation("4-S: Destroy all crates", Regions.s4, "clash", LocationType.ClashMode),

    # 4-3: A SHOT IN THE DARK
    UKLocation("4-3: Secret #1", Regions.l18, "18_s1", LocationType.Normal),
    UKLocation("4-3: Secret #2", Regions.l18, "18_s2", LocationType.Normal),
    UKLocation("4-3: Secret #3", Regions.l18, "18_s3", LocationType.Normal),
    UKLocation("4-3: Secret #4", Regions.l18, "18_s4", LocationType.Normal),
    UKLocation("4-3: Secret #5", Regions.l18, "18_s5", LocationType.Normal),
    UKLocation("4-3: Defeat the Mysterious Druid Knight (& Owl)", Regions.l18, "18_b", LocationType.BossExt),
    UKLocation("4-3: Don't pick up the torch", Regions.l18, "18_c", LocationType.Challenge),
    UKLocation("4-3: Perfect Rank", Regions.l18, "18_p", LocationType.PerfectRank),

    # 4-4: CLAIR DE SOLEIL
    UKLocation("4-4: V2's Other Arm", Regions.l19, "19_w1", LocationType.Normal),
    UKLocation("4-4: Secret Weapon", Regions.l19, "19_w2", LocationType.Normal),
    UKLocation("4-4: Defeat V2", Regions.l19, "19_b", LocationType.Boss),
    UKLocation("4-4: Reach the boss room in 18 seconds", Regions.l19, "19_c", LocationType.Challenge),
    UKLocation("4-4: Perfect Rank", Regions.l19, "19_p", LocationType.PerfectRank),

    # 5-1: IN THE WAKE OF POSEIDON
    UKLocation("5-1: Secret #1", Regions.l20, "20_s1", LocationType.Normal),
    UKLocation("5-1: Secret #2", Regions.l20, "20_s2", LocationType.Normal),
    UKLocation("5-1: Secret #3", Regions.l20, "20_s3", LocationType.Normal),
    UKLocation("5-1: Secret #4", Regions.l20, "20_s4", LocationType.Normal),
    UKLocation("5-1: Secret #5", Regions.l20, "20_s5", LocationType.Normal),
    UKLocation("5-1: Don't touch any water", Regions.l20, "20_c", LocationType.Challenge),
    UKLocation("5-1: Perfect Rank", Regions.l20, "20_p", LocationType.PerfectRank),

    # 5-S: I ONLY SAY MORNING
    UKLocation("5-S: Funny Stupid Fish (Friend)", Regions.s5, "fish0", LocationType.Fish),
    UKLocation("5-S: PITR Fish", Regions.s5, "fish1", LocationType.Fish),
    UKLocation("5-S: Trout", Regions.s5, "fish2", LocationType.Fish),
    UKLocation("5-S: Metal Fish", Regions.s5, "fish3", LocationType.Fish),
    UKLocation("5-S: Chomper", Regions.s5, "fish4", LocationType.Fish),
    UKLocation("5-S: Bomb Fish", Regions.s5, "fish5", LocationType.Fish),
    UKLocation("5-S: Eyeball", Regions.s5, "fish6", LocationType.Fish),
    UKLocation("5-S: Frog (?)", Regions.s5, "fish7", LocationType.Fish),
    UKLocation("5-S: Dope Fish", Regions.s5, "fish8", LocationType.Fish),
    UKLocation("5-S: Stickfish", Regions.s5, "fish9", LocationType.Fish),
    UKLocation("5-S: Cooked Fish", Regions.s5, "fish10", LocationType.Fish),
    UKLocation("5-S: Shark", Regions.s5, "fish11", LocationType.Fish),

    # 5-2: WAVES OF THE STARLESS SEA
    UKLocation("5-2: Secret #1", Regions.l21, "21_s1", LocationType.Normal),
    UKLocation("5-2: Secret #2", Regions.l21, "21_s2", LocationType.Normal),
    UKLocation("5-2: Secret #3", Regions.l21, "21_s3", LocationType.Normal),
    UKLocation("5-2: Secret #4", Regions.l21, "21_s4", LocationType.Normal),
    UKLocation("5-2: Secret #5", Regions.l21, "21_s5", LocationType.Normal),
    UKLocation("5-2: Don't fight the ferryman", Regions.l21, "21_c", LocationType.Challenge),
    UKLocation("5-2: Perfect Rank", Regions.l21, "21_p", LocationType.PerfectRank),

    # 5-3: SHIP OF FOOLS
    UKLocation("5-3: Weapon", Regions.l22, "22_w1", LocationType.Normal),
    UKLocation("5-3: Secret #1", Regions.l22, "22_s1", LocationType.Normal),
    UKLocation("5-3: Secret #2", Regions.l22, "22_s2", LocationType.Normal),
    UKLocation("5-3: Secret #3", Regions.l22, "22_s3", LocationType.Normal),
    UKLocation("5-3: Secret #4", Regions.l22, "22_s4", LocationType.Normal),
    UKLocation("5-3: Secret #5", Regions.l22, "22_s5", LocationType.Normal),
    UKLocation("5-3: Assemble Hank Jr.", Regions.l22, "22_ha", LocationType.Hank),
    UKLocation("5-3: Don't touch any water", Regions.l22, "22_c", LocationType.Challenge),
    UKLocation("5-3: Perfect Rank", Regions.l22, "22_p", LocationType.PerfectRank),

    # 5-4: LEVIATHAN
    UKLocation("5-4: Defeat the Leviathan", Regions.l23, "23_b", LocationType.Boss),
    UKLocation("5-4: Reach the surface in under 10 seconds", Regions.l23, "23_c", LocationType.Challenge),
    UKLocation("5-4: Perfect Rank", Regions.l22, "23_p", LocationType.PerfectRank),

    # 6-1: CRY FOR THE WEEPER
    UKLocation("6-1: Secret #1", Regions.l24, "24_s1", LocationType.Normal),
    UKLocation("6-1: Secret #2", Regions.l24, "24_s2", LocationType.Normal),
    UKLocation("6-1: Secret #3", Regions.l24, "24_s3", LocationType.Normal),
    UKLocation("6-1: Secret #4", Regions.l24, "24_s4", LocationType.Normal),
    UKLocation("6-1: Secret #5", Regions.l24, "24_s5", LocationType.Normal),
    UKLocation("6-1: Beat the secret encounter", Regions.l24, "24_c", LocationType.Challenge),
    UKLocation("6-1: Perfect Rank", Regions.l24, "24_p", LocationType.PerfectRank),

    # 6-2: AESTHETICS OF HATE
    UKLocation("6-2: Defeat Gabriel", Regions.l25, "25_b", LocationType.Boss),
    UKLocation("6-2: Hit Gabriel into the ceiling", Regions.l25, "25_c", LocationType.Challenge),
    UKLocation("6-2: Perfect Rank", Regions.l25, "25_p", LocationType.PerfectRank),

    # 7-1: GARDEN OF FORKING PATHS
    UKLocation("7-1: Secret #1", Regions.l26, "26_s1", LocationType.Normal),
    UKLocation("7-1: Secret #2", Regions.l26, "26_s2", LocationType.Normal),
    UKLocation("7-1: Secret #3", Regions.l26, "26_s3", LocationType.Normal),
    UKLocation("7-1: Secret #4", Regions.l26, "26_s4", LocationType.Normal),
    UKLocation("7-1: Secret #5", Regions.l26, "26_s5", LocationType.Normal),
    UKLocation("7-1: Beat the secret encounter", Regions.l26, "26_c", LocationType.Challenge),
    UKLocation("7-1: Perfect Rank", Regions.l26, "26_p", LocationType.PerfectRank),

    # 7-2: LIGHT UP THE NIGHT
    UKLocation("7-2: Secret #1", Regions.l27, "27_s1", LocationType.Normal),
    UKLocation("7-2: Secret #2", Regions.l27, "27_s2", LocationType.Normal),
    UKLocation("7-2: Secret #3", Regions.l27, "27_s3", LocationType.Normal),
    UKLocation("7-2: Secret #4", Regions.l27, "27_s4", LocationType.Normal),
    UKLocation("7-2: Secret #5", Regions.l27, "27_s5", LocationType.Normal),
    UKLocation("7-2: Switch #1", Regions.l27, "27_sw1", LocationType.ViolenceSwitch),
    UKLocation("7-2: Switch #2", Regions.l27, "27_sw2", LocationType.ViolenceSwitch),
    UKLocation("7-2: Switch #3", Regions.l27, "27_sw3", LocationType.ViolenceSwitch),
    UKLocation("7-2: Secret Weapon", Regions.l27, "27_w2", LocationType.Normal),
    UKLocation("7-2: Don't kill any enemies", Regions.l27, "27_c", LocationType.Challenge),
    UKLocation("7-2: Perfect Rank", Regions.l27, "27_p", LocationType.PerfectRank),

    # 7-3: NO SOUND, NO MEMORY
    UKLocation("7-3: Secret #1", Regions.l28, "28_s1", LocationType.Normal),
    UKLocation("7-3: Secret #2", Regions.l28, "28_s2", LocationType.Normal),
    UKLocation("7-3: Secret #3", Regions.l28, "28_s3", LocationType.Normal),
    UKLocation("7-3: Secret #4", Regions.l28, "28_s4", LocationType.Normal),
    UKLocation("7-3: Secret #5", Regions.l28, "28_s5", LocationType.Normal),
    UKLocation("7-3: Become marked for death", Regions.l28, "28_c", LocationType.Challenge),
    UKLocation("7-3: Perfect Rank", Regions.l28, "28_p", LocationType.PerfectRank),

    # 7-S: HELL BATH NO FURY
    UKLocation("7-S: Cleaned Courtyard", Regions.s7, "clean0", LocationType.Clean),
    UKLocation("7-S: Cleaned Library", Regions.s7, "clean1", LocationType.Clean),
    UKLocation("7-S: Cleaned Lobby", Regions.s7, "clean2", LocationType.Clean),
    UKLocation("7-S: Cleaned Lounge", Regions.s7, "clean3", LocationType.Clean),
    UKLocation("7-S: Cleaned Side Room", Regions.s7, "clean4", LocationType.Clean),

    # 7-4: ...LIKE ANTENNAS TO HEAVEN
    UKLocation('7-4: Defeat 1000-THR "Earthmover"', Regions.l29, "29_b", LocationType.Boss),
    UKLocation("7-4: Don't fight the security system", Regions.l29, "29_c", LocationType.Challenge),
    UKLocation("7-4: Perfect Rank", Regions.l29, "29_p", LocationType.PerfectRank),

    # Encores
    UKLocation("0-E: Perfect Rank", Regions.e0, "100_p", LocationType.PerfectRank),
    UKLocation("1-E: Perfect Rank", Regions.e1, "101_p", LocationType.PerfectRank),

    # Primes
    UKLocation("P-1: Perfect Rank", Regions.p1, "666_p", LocationType.PerfectRank),
    UKLocation("P-2: Perfect Rank", Regions.p2, "667_p", LocationType.PerfectRank),

    # Shop
    UKLocation("Shop: Buy Revolver Variant 1", Regions.shop, "shop_rev2", LocationType.Normal),
    UKLocation("Shop: Buy Revolver Variant 2", Regions.shop, "shop_rev1", LocationType.Normal),
    UKLocation("Shop: Buy Shotgun Variant 1", Regions.shop, "shop_sho1", LocationType.Normal),
    UKLocation("Shop: Buy Shotgun Variant 2", Regions.shop, "shop_sho2", LocationType.Normal),
    UKLocation("Shop: Buy Nailgun Variant 1", Regions.shop, "shop_nai1", LocationType.Normal),
    UKLocation("Shop: Buy Nailgun Variant 2", Regions.shop, "shop_nai2", LocationType.Normal),
    UKLocation("Shop: Buy Railcannon Variant 1", Regions.shop, "shop_rai1", LocationType.Normal),
    UKLocation("Shop: Buy Railcannon Variant 2", Regions.shop, "shop_rai2", LocationType.Normal),
    UKLocation("Shop: Buy Rocket Launcher Variant 1", Regions.shop, "shop_rock1", LocationType.Normal),
    UKLocation("Shop: Buy Rocket Launcher Variant 2", Regions.shop, "shop_rock2", LocationType.Normal),

    # Misc
    UKLocation("Museum: Win chess", Regions.museum, "chess", LocationType.Chess),
    UKLocation("Museum: Win rocket race", Regions.museum, "rr", LocationType.Rocket)
]


start_weapon_locations: Dict[str, List[str]] = {
    "0-1": ["0-1: Weapon"],
    "0-2": ["0-2: Secret #1",
            "0-2: Secret #2"],
    "1-1": ["1-1: Secret #1",
            "1-1: Secret #2"],
    "1-2": ["1-2: Secret #1"],
    "1-3": ["1-3: Secret #2",
            "1-3: Secret #3"],
    "2-3": ["2-3: Secret #1"],
    "3-1": ["3-1: Secret #1"],
    "4-2": ["4-2: Secret #1",
            "4-2: Secret #2",
            "4-2: Secret #3",
            "4-2: Secret #5"]
}