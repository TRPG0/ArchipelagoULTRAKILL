using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // disconnect when loading a different save file
    [HarmonyPatch(typeof(GameProgressSaver), "SetSlot")]
    class SetSlot_Patch
    {
        public static void Postfix()
        {
            if (Multiworld.Authenticated)
            {
                Multiworld.Disconnect();
            }
            if (Core.DataExists()) Core.firstTimeLoad = false;
            else Core.data = new Data();

        }
    }

    // save multiworld data when game is saving
    [HarmonyPatch(typeof(GameProgressSaver), "WriteFile")]
    class WriteFile_Patch
    {
        public static void Prefix()
        {
            if (Core.DataExists()) Core.SaveData();
        }
    }

    // delete data when wiping save file
    [HarmonyPatch(typeof(GameProgressSaver), "WipeSlot")]
    class WipeSlot_Patch
    {
        public static void Postfix(int slot)
        {
            if (Multiworld.Authenticated) Multiworld.Disconnect();
            Core.DeleteData(slot);
            Core.data = new Data();
            Core.firstTimeLoad = false;
        }
    }

    // multiply points
    [HarmonyPatch(typeof(GameProgressSaver), "AddMoney")]
    class AddMoney_Patch
    {
        public static void Prefix(ref int money)
        {
            if (Core.DataExists() && money > 0)
            {
                money *= Core.data.multiplier;
            }
        }
    }
}
