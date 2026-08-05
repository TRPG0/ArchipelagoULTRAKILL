using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(SaveSlotMenu), "UpdateSlotState")]
    public class SaveSlotMenu_UpdateSlotState_Patch
    {
        public static void Postfix(SlotRowPanel targetPanel, SaveSlotMenu.SlotData data)
        {
            if (Core.DataExists(targetPanel.slotIndex + 1))
            {
                targetPanel.deleteButton.interactable = true;

                try
                {
                    string filePath = Path.Combine(GameProgressSaver.BaseSavePath, string.Format("Slot{0}", targetPanel.slotIndex + 1)) + "\\archipelago.json";
                    Data fileData = new Data();
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        fileData = JsonConvert.DeserializeObject<Data>(reader.ReadToEnd());
                    }
                    if (fileData.IsTestMode()) targetPanel.stateLabel.text = "AP (TEST MODE)";
                    else targetPanel.stateLabel.text = $"AP ({fileData.slot_name} | {fileData.@checked.Count} | {(fileData.version == string.Empty ? "UNKNOWN VERSION" : fileData.version)})";
                }
                catch (NullReferenceException)
                {
                    targetPanel.stateLabel.color = Color.red;
                    targetPanel.stateLabel.text = "Failed to load Archipelago data. File may be corrupt.";
                }
            }
        }
    }
}
