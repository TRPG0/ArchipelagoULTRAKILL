using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Patches
{
    [HarmonyPatch(typeof(PlayerActivatorRelay), "Activate")]
    class PlayerActivatorRelay_Activate_Patch
    {
        public static void Postfix()
        {
            if (Core.DataExists() && PlayerHelper.Instance == null)
            {
                NewMovement.Instance.gameObject.AddComponent<PlayerHelper>().Init(NewMovement.Instance);

                if (SceneHelper.CurrentScene == "Level 0-1") Core.obj.AddComponent<FirstLevelSetup>();

                if ((SceneHelper.CurrentScene == "Level 1-1" || SceneHelper.CurrentScene == "Level 1-2" || SceneHelper.CurrentScene == "Level 2-3"
                    || SceneHelper.CurrentScene == "Level 4-4" || SceneHelper.CurrentScene == "Level 5-2" || SceneHelper.CurrentScene == "Level 5-3"
                    || SceneHelper.CurrentScene == "Level 6-1" || SceneHelper.CurrentScene == "Level 1-E") && Core.data.randomizeSkulls) 
                    LevelManager.AddDoorClosers();

                if (SceneHelper.CurrentScene == "Level 1-E" && Core.data.randomizeSkulls)
                {
                    LevelManager.redDoor = GameObject.Find("Door (Large) With Controllers (3)/Door (Large)/").GetComponent<Door>();
                    if (!Core.data.unlockedSkulls.Contains("101_r")) LevelManager.redDoor.Close(true);
                }

                /*
                if ((SceneHelper.CurrentScene.Contains("Level ") || SceneHelper.CurrentScene == "Endless") && SceneHelper.CurrentScene != "Level 5-S")
                    Core.ValidateArms();
                */

                if (SceneHelper.CurrentScene == "Level 5-S") LevelManager.ForceBlueArm();

                if (Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Skulls >= SkullsType.Normal && Core.data.randomizeSkulls)
                    LevelManager.FindSkulls();

                if (Core.data.deathLink && Core.uim.deathLinkMessage == null) Core.uim.CreateDeathLinkMessage();
            }
            //if (LocationManager.messages.Count > 0 && !UIManager.displayingMessage) Core.uim.StartCoroutine("DisplayMessage");
        }
    }
}
