using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(WeaponHUD), "Awake")]
    public class WeaponHUD_Awake_Patch
    {
        public static void Postfix(WeaponHUD __instance)
        {
            if (Core.DataExists() && Core.data.randomizeFire2) __instance.gameObject.AddComponent<Fire2HUD>(); 
        }
    }

    [HarmonyPatch(typeof(WeaponHUD), "UpdateImage")]
    public class WeaponHUD_UpdateImage_Patch
    {
        public static bool Prefix(WeaponHUD __instance, Sprite icon, Sprite glowIcon, int variation)
        {
            if (Fire2HUD.Instance != null)
            {
                Fire2HUD.Instance.UpdateCurrentWeapon();
                if (Fire2HUD.Instance.CurrentWeapon == "?") return true;
                Fire2HUD.Instance.timer = 0f;

                Traverse traverse = Traverse.Create(__instance);
                if (traverse.Field<Image>("img").Value == null)
                {
                    traverse.Field<Image>("img").Value = __instance.GetComponent<Image>();
                }
                if (traverse.Field<Image>("glowImg").Value == null)
                {
                    traverse.Field<Image>("glowImg").Value = __instance.transform.GetChild(0).GetComponent<Image>();
                }

                string weapon = Fire2HUD.Instance.CurrentWeapon;
                if (Fire2HUD.Instance.CurrentIsAlternate) weapon = weapon.Substring(0, weapon.Length - 1) + "alt" + weapon.Substring(weapon.Length - 1);

                traverse.Field<Image>("img").Value.sprite = UIManager.bundle.LoadAsset<Sprite>($"assets/weapons/{weapon}_main.png");
                traverse.Field<Image>("img").Value.color = ColorBlindSettings.Instance.variationColors[variation];
                traverse.Field<Image>("glowImg").Value.sprite = UIManager.bundle.LoadAsset<Sprite>($"assets/weapons/{weapon}_main_glow.png");
                traverse.Field<Image>("glowImg").Value.color = ColorBlindSettings.Instance.variationColors[variation];

                Fire2HUD.Instance.sec.sprite = UIManager.bundle.LoadAsset<Sprite>($"assets/weapons/{weapon}_sec.png");
                if (Fire2HUD.Instance.CurrentIsUnlocked) Fire2HUD.Instance.sec.color = ColorBlindSettings.Instance.variationColors[variation];
                else Fire2HUD.Instance.sec.color = Colors.Gray;
                Fire2HUD.Instance.secGlow.sprite = UIManager.bundle.LoadAsset<Sprite>($"assets/weapons/{weapon}_sec_glow.png");
                if (Fire2HUD.Instance.CurrentIsUnlocked) Fire2HUD.Instance.secGlow.color = ColorBlindSettings.Instance.variationColors[variation];
                else Fire2HUD.Instance.secGlow.color = Colors.Gray;

                return false;
            }
            else return true;
        }
    }
}
