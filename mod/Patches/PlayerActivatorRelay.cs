using ArchipelagoULTRAKILL.Components;
using ArchipelagoULTRAKILL.Structures;
using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // set playerActive when landing at the beginning of a level
    [HarmonyPatch(typeof(PlayerActivatorRelay), "Activate")]
    class PlayerActivatorRelay_Activate_Patch
    {
        public static void Postfix()
        {
            if (Core.DataExists() && PlayerHelper.Instance == null)
            {
                NewMovement.Instance.gameObject.AddComponent<PlayerHelper>().Init(NewMovement.Instance);
                if (SceneHelper.CurrentScene == "Level 0-1")
                {
                    GunSetter.Instance.ResetWeapons();
                    GunControl.Instance.UpdateWeaponList();
                    GunControl.Instance.YesWeapon();
                    FistControl.Instance.ResetFists();
                    LevelManager.AddGlassComponents();
                }
                if ((SceneHelper.CurrentScene == "Level 1-1" || SceneHelper.CurrentScene == "Level 1-2" || SceneHelper.CurrentScene == "Level 2-3" || SceneHelper.CurrentScene == "Level 4-4" || SceneHelper.CurrentScene == "Level 5-2" || SceneHelper.CurrentScene == "Level 5-3" || SceneHelper.CurrentScene == "Level 6-1") && Core.data.randomizeSkulls) LevelManager.AddDoorClosers();
                if (SceneHelper.CurrentScene.Contains("Level ") || SceneHelper.CurrentScene == "Endless")
                {
                    Core.ValidateArms();
                }
                if (Core.CurrentLevelHasInfo && Core.CurrentLevelInfo.Skulls >= SkullsType.Normal && Core.data.randomizeSkulls)
                {
                    LevelManager.FindSkulls();
                }
                if (Core.data.deathLink && Core.uim.deathLinkMessage == null) Core.uim.CreateDeathLinkMessage();
            }
            //if (LocationManager.messages.Count > 0 && !UIManager.displayingMessage) Core.uim.StartCoroutine("DisplayMessage");
        }
    }
}
