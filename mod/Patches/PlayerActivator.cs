using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(PlayerActivator), "Activate")]
    class PlayerActivator_Activate_Patch
    {
        public static void Postfix()
        {
            if (Multiworld.HintMode && SceneHelper.CurrentScene != "Endless") 
                HudMessageReceiver.Instance?.SendHudMessage("<color=orange>[Archipelago]</color> Hint mode is enabled!\nGo to The Cyber Grind to unlock hints!");
        }
    }
}
