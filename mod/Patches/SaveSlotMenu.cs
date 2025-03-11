using HarmonyLib;
using Newtonsoft.Json;
using System.IO;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(SaveSlotMenu), "UpdateSlotState")]
    public class SaveSlotMenu_UpdateSlotState_Patch
    {
        public static void Postfix(SlotRowPanel targetPanel, SaveSlotMenu.SlotData data)
        {
            if (Core.DataExists(targetPanel.slotIndex + 1))
            {
                string filePath = Path.Combine(GameProgressSaver.BaseSavePath, string.Format("Slot{0}", targetPanel.slotIndex + 1)) + "\\archipelago.json";
                Data fileData = new Data();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    fileData = JsonConvert.DeserializeObject<Data>(reader.ReadToEnd());
                }
                if (fileData.version == string.Empty) targetPanel.stateLabel.text = $"AP ({fileData.slot_name} | {fileData.@checked.Count} | UNKNOWN VERSION)"; 
                else targetPanel.stateLabel.text = $"AP ({fileData.slot_name} | {fileData.@checked.Count} | {fileData.version})";
            }
        }
    }
}
