using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using ArchipelagoULTRAKILL.Components;
using HarmonyLib;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(DeathSequence), "OnEnable")]
    public class DeathSequence_OnEnable_Patch
    {
        public static void Postfix()
        {
            if (Multiworld.DeathLinkService != null && Core.uim.deathLinkMessage != null && Multiworld.lastDeathLink == null) 
            {
                int index = 0;

                if (DeathLinkMessage.specialMessages.ContainsKey(SceneHelper.CurrentScene))
                {
                    index = Random.Range(0, DeathLinkMessage.specialMessages[SceneHelper.CurrentScene].Count);
                    Core.Logger.LogWarning($"Sending Death Link with message: {string.Format(DeathLinkMessage.specialMessages[SceneHelper.CurrentScene][index], Core.data.slot_name)}");
                    Multiworld.DeathLinkService.SendDeathLink(new DeathLink(Core.data.slot_name, string.Format(DeathLinkMessage.specialMessages[SceneHelper.CurrentScene][index], Core.data.slot_name)));
                }
                else
                {
                    index = Random.Range(0, DeathLinkMessage.deathMessages.Count);
                    Core.Logger.LogWarning($"Sending Death Link with message: {string.Format(DeathLinkMessage.deathMessages[index], Core.data.slot_name)}");
                    Multiworld.DeathLinkService.SendDeathLink(new DeathLink(Core.data.slot_name, string.Format(DeathLinkMessage.deathMessages[index], Core.data.slot_name)));
                }
            }
        }
    }
}
