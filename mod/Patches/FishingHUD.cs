using HarmonyLib;
using System.Linq;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(FishingHUD), "ShowFishCaught")]
    public class FishingHUD_ShowFishCaught_Patch
    {
        public static void Prefix(ref bool show, ref FishObject fish)
        {
            if (Core.DataExists() && Core.data.fishRewards && fish != null)
            {
                int id = FishManager.Instance.recognizedFishes.Keys.ToList().IndexOf(fish);
                LocationManager.CheckLocation($"fish{id}");
            }
        }
    }
}
