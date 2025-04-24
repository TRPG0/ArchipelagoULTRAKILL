using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // disconnect when loading a different save file
    [HarmonyPatch(typeof(GameProgressSaver), "SetSlot")]
    class GameProgressSaver_SetSlot_Patch
    {
        public static void Postfix()
        {
            if (Multiworld.Authenticated) Multiworld.Disconnect();
            LocationManager.messages.Clear();
            LocationManager.powerupQueue.Clear();
            Multiworld.recentItems.Clear();
            if (Core.DataExists()) Core.firstTimeLoad = false;
            else Core.data = new Data();

        }
    }

    // save multiworld data when game is saving
    [HarmonyPatch(typeof(GameProgressSaver), "WriteFile")]
    class GameProgressSaver_WriteFile_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists()) Core.SaveData();
        }
    }

    // delete data when wiping save file
    [HarmonyPatch(typeof(GameProgressSaver), "WipeSlot")]
    class GameProgressSaver_WipeSlot_Patch
    {
        public static void Postfix(int slot)
        {
            if (Multiworld.Authenticated) Multiworld.Disconnect();
            LocationManager.messages.Clear();
            LocationManager.powerupQueue.Clear();
            Multiworld.recentItems.Clear();
            Core.DeleteData(slot);
            Core.data = new Data();
            Core.firstTimeLoad = false;
        }
    }

    // multiply points
    [HarmonyPatch(typeof(GameProgressSaver), "AddMoney")]
    class GameProgressSaver_AddMoney_Patch
    {
        public static void Prefix(ref int money)
        {
            if (Core.DataExists() && money > 0)
            {
                money *= Core.data.multiplier;
            }
        }
    }

    [HarmonyPatch(typeof(GameProgressSaver), "GetLimboSwitch")]
    class GameProgressSaver_GetLimboSwitch_Patch
    {
        public static bool Prefix(int switchNum, ref bool __result)
        {
            if (Core.DataExists() && Core.data.l1switch)
            {
                __result = Core.data.limboSwitches[switchNum];
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(GameProgressSaver), "GetShotgunSwitch")]
    class GameProgressSaver_GetShotgunSwitch_Patch
    {
        public static bool Prefix(int switchNum, ref bool __result)
        {
            if (Core.DataExists() && Core.data.l7switch)
            {
                __result = Core.data.shotgunSwitches[switchNum];
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(GameProgressSaver), "SetClashModeUnlocked")]
    class GameProgressSaver_SetClashModeUnlocked_Patch
    {
        public static bool Prefix()
        {
            if (Core.DataExists() && Core.data.clashReward)
            {
                LocationManager.CheckLocation("clash");
                return false;
            }
            return true;
        }
    }
}
