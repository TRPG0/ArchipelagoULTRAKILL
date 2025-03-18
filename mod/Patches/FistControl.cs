using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(FistControl), "UpdateFistIcon")]
    public class FistControl_UpdateFistIcon_Patch
    {
        public static void Postfix()
        {
            if (Core.HasNoArms) HudController.Instance?.armIcon.SetActive(false);
        }
    }
}
