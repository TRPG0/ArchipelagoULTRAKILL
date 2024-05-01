using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(LimboSwitch), "Start")]
    public class LimboSwitch_Start_Patch
    {
        public static bool Prefix(LimboSwitch __instance)
        {
            if (Core.DataExists())
            {
                Traverse traverse = Traverse.Create(__instance);
                if (__instance.type == SwitchLockType.Limbo && Core.data.l1switch && StatsManager.Instance.levelNumber >= 6 && StatsManager.Instance.levelNumber <= 9)
                {
                    bool pressed = Core.data.@checked.Contains(StatsManager.Instance.levelNumber.ToString() + "_sw");
                    traverse.Field<MeshRenderer>("mr").Value = __instance.GetComponent<MeshRenderer>();
                    traverse.Field<MaterialPropertyBlock>("block").Value = new MaterialPropertyBlock();
                    if (pressed)
                    {
                        __instance.beenPressed = true;
                        UnityEvent unityEvent = __instance.onAlreadyPressed;
                        if (unityEvent != null) unityEvent.Invoke();
                        traverse.Field<float>("fadeAmount").Value = 2f;
                        traverse.Field<MeshRenderer>("mr").Value.GetPropertyBlock(traverse.Field<MaterialPropertyBlock>("block").Value);
                        traverse.Field<MaterialPropertyBlock>("block").Value.SetFloat(UKShaderProperties.EmissiveIntensity, 2f);
                        traverse.Field<MeshRenderer>("mr").Value.SetPropertyBlock(traverse.Field<MaterialPropertyBlock>("block").Value);
                    }
                    return false;
                }
                else if (__instance.type == SwitchLockType.Shotgun && Core.data.l7switch && StatsManager.Instance.levelNumber == 27)
                {
                    bool pressed = Core.data.@checked.Contains(StatsManager.Instance.levelNumber.ToString() + "_sw" + __instance.switchNumber.ToString());
                    traverse.Field<MeshRenderer>("mr").Value = __instance.GetComponent<MeshRenderer>();
                    traverse.Field<MaterialPropertyBlock>("block").Value = new MaterialPropertyBlock();
                    if (pressed)
                    {
                        __instance.beenPressed = true;
                        UnityEvent unityEvent = __instance.onAlreadyPressed;
                        if (unityEvent != null) unityEvent.Invoke();
                        traverse.Field<float>("fadeAmount").Value = 2f;
                        traverse.Field<MeshRenderer>("mr").Value.GetPropertyBlock(traverse.Field<MaterialPropertyBlock>("block").Value);
                        traverse.Field<MaterialPropertyBlock>("block").Value.SetFloat(UKShaderProperties.EmissiveIntensity, 2f);
                        traverse.Field<MeshRenderer>("mr").Value.SetPropertyBlock(traverse.Field<MaterialPropertyBlock>("block").Value);
                    }
                    return false;
                }
                return true;
            }
            return true;
        }
    }


    [HarmonyPatch(typeof(LimboSwitch), "Pressed")]
    public class LimboSwitch_Pressed_Patch
    {
        public static bool Prefix(LimboSwitch __instance)
        {
            if (Core.DataExists())
            {
                if (__instance.type == SwitchLockType.Limbo && Core.data.l1switch && StatsManager.Instance.levelNumber >= 6 && StatsManager.Instance.levelNumber <= 9)
                {
                    if (!__instance.beenPressed)
                    {
                        __instance.beenPressed = true;
                        __instance.GetComponent<AudioSource>().Play();
                        //__instance.Invoke("DelayedEffect", 1f);
                        LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_sw");
                    }
                    return false;
                }
                else if (__instance.type == SwitchLockType.Shotgun && Core.data.l7switch && StatsManager.Instance.levelNumber == 27)
                {
                    if (!__instance.beenPressed)
                    {
                        __instance.beenPressed = true;
                        __instance.GetComponent<AudioSource>().Play();
                        //__instance.Invoke("DelayedEffect", 1f);
                        LocationManager.CheckLocation(StatsManager.Instance.levelNumber.ToString() + "_sw" + __instance.switchNumber.ToString());
                    }
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
