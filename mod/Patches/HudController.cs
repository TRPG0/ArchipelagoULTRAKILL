using ArchipelagoULTRAKILL.Components;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(HudController), "Start")]
    public class HudController_Start_Patch
    {
        public static void Postfix(HudController __instance)
        {
            if (Core.DataExists() && Core.data.randomizeFire2 && Fire2HUD.Instance != null)
            {
                if (__instance.altHud && !__instance.colorless)
                {
                    Fire2HUD.Instance.alt1 = GameObject.Instantiate(__instance.weaponIcon.transform.Find("Image").gameObject, __instance.weaponIcon.transform).GetComponent<Image>();
                    Fire2HUD.Instance.alt1.GetComponent<CopyImage>().imgToCopy = Fire2HUD.Instance.sec;
                    GameObject alt1Clone = GameObject.Instantiate(__instance.weaponIcon.transform.Find("Image (1)").gameObject, __instance.weaponIcon.transform);
                    alt1Clone.GetComponent<CopyImage>().imgToCopy = Fire2HUD.Instance.secGlow;
                    Fire2HUD.Instance.alt1.transform.SetAsLastSibling();
                    if (Fire2HUD.Instance.CurrentWeapon == "?") Fire2HUD.Instance.alt1.gameObject.SetActive(false);
                }
                else if (__instance.altHud && __instance.colorless)
                {
                    Fire2HUD.Instance.alt2 = GameObject.Instantiate(__instance.weaponIcon.transform.Find("Image").gameObject, __instance.weaponIcon.transform).GetComponent<Image>();
                    Fire2HUD.Instance.alt2.GetComponent<CopyImage>().imgToCopy = Fire2HUD.Instance.sec;
                    GameObject alt2Clone = GameObject.Instantiate(__instance.weaponIcon.transform.Find("Image (1)").gameObject, __instance.weaponIcon.transform);
                    alt2Clone.GetComponent<CopyImage>().imgToCopy = Fire2HUD.Instance.secGlow;
                    Fire2HUD.Instance.alt2.transform.SetAsLastSibling();
                    if (Fire2HUD.Instance.CurrentWeapon == "?") Fire2HUD.Instance.alt2.gameObject.SetActive(false);
                }
            }
        }
    }
}
