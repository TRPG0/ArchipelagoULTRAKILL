using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    // set playerActive when landing at the beginning of a level
    [HarmonyPatch(typeof(PlayerActivatorRelay), "Activate")]
    class PlayerActivatorRelay_Activate_Patch
    {
        public static void Postfix()
        {
            Core.playerActive = true;
            if (Core.DataExists())
            {
                if (SceneHelper.CurrentScene == "Level 0-1")
                {
                    GunSetter.Instance.ResetWeapons();
                    GunControl.Instance.UpdateWeaponList();
                    GunControl.Instance.YesWeapon();
                    FistControl.Instance.ResetFists();
                    if (!Core.CanBreakGlass()) LevelManager.AddGlassComponents();
                }
                if ((SceneHelper.CurrentScene == "Level 1-1" || SceneHelper.CurrentScene == "Level 1-2" || SceneHelper.CurrentScene == "Level 2-3" || SceneHelper.CurrentScene == "Level 4-4" || SceneHelper.CurrentScene == "Level 5-2" || SceneHelper.CurrentScene == "Level 5-3" || SceneHelper.CurrentScene == "Level 6-1") && Core.data.randomizeSkulls) LevelManager.AddDoorClosers();
                if (UIManager.skullLevels.Contains(SceneHelper.CurrentScene) && Core.data.randomizeSkulls)
                {
                    LevelManager.FindSkulls();
                }
            }
            if (LocationManager.messages.Count > 0 && !UIManager.displayingMessage) Core.uim.StartCoroutine("DisplayMessage");
            if (LocationManager.powerupQueue.Count > 0 && !Core.poweredUp)
            {
                Core.obj.GetComponent<Core>().Invoke("AddPowerup", 1f);
                Core.poweredUp = true;
            }
            if (LocationManager.overhealWaiting)
            {
                NewMovement.Instance.SuperCharge();
                LocationManager.overhealWaiting = false;
            }
            if (LocationManager.hardDmgWaiting)
            {
                NewMovement.Instance.ForceAntiHP(50);
                LocationManager.hardDmgWaiting = false;
            }
            if (LocationManager.soapWaiting) Core.SpawnSoap();
        }
    }
}
