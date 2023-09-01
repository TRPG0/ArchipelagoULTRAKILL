using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // show shop categories if any weapon of type is unlocked
    [HarmonyPatch(typeof(ShopCategory), "CheckGear")]
    class ShopCategory_CheckGear_Patch
    {
        public static bool Prefix(ShopCategory __instance)
        {
            if (Core.DataExists())
            {
                bool hasWeapon = false;
                switch (__instance.weaponName.Substring(0, __instance.weaponName.Length - 1))
                {
                    case "rev":
                        if (GameProgressSaver.CheckGear("rev0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rev2") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rev1") == 1) hasWeapon = true;
                        break;
                    case "sho":
                        if (GameProgressSaver.CheckGear("sho0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("sho1") == 1) hasWeapon = true;
                        break;
                    case "nai":
                        if (GameProgressSaver.CheckGear("nai0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("nai1") == 1) hasWeapon = true;
                        break;
                    case "rai":
                        if (GameProgressSaver.CheckGear("rai0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rai1") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rai2") == 1) hasWeapon = true;
                        break;
                    case "rock":
                        if (GameProgressSaver.CheckGear("rock0") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("rock1") == 1) hasWeapon = true;
                        break;
                    case "arm":
                        if (Core.data.hasArm) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("arm1") == 1) hasWeapon = true;
                        if (GameProgressSaver.CheckGear("arm2") == 1) hasWeapon = true;
                        break;
                    default: break;
                }
                if (!hasWeapon) __instance.gameObject.SetActive(false);
                else __instance.gameObject.SetActive(true);

                return false;
            }
            else return true;
        }
    }
}
