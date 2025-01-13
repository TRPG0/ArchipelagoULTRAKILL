using UnityEngine;

namespace ArchipelagoULTRAKILL.Structures
{
    public static class Colors
    {
        public static Color White => Color.white;
        public static Color Gray => new Color(0.7f, 0.7f, 0.7f);
        public static Color Red => new Color(1, 0.2353f, 0.2353f);
        public static Color Green => new Color(0.2667f, 1, 0.2706f);
        public static Color Perfect => new Color(0.96f, 0.659f, 0);
        public static Color PlayerSelf => ConfigManager.APPlayerSelf.value;
        public static Color PlayerOther => ConfigManager.APPlayerOther.value;
        public static Color ItemAdvancement => ConfigManager.APItemAdvancement.value;
        public static Color ItemNeverExclude => ConfigManager.APItemNeverExclude.value;
        public static Color ItemFiller => ConfigManager.APItemFiller.value;
        public static Color ItemTrap => ConfigManager.APItemTrap.value;
        public static Color Location => ConfigManager.APLocation.value;
        public static Color Layer0 => ConfigManager.layer0Color.value;
        public static Color Layer1 => ConfigManager.layer1Color.value;
        public static Color Layer2 => ConfigManager.layer2Color.value;
        public static Color Layer3 => ConfigManager.layer3Color.value;
        public static Color Layer4 => ConfigManager.layer4Color.value;
        public static Color Layer5 => ConfigManager.layer5Color.value;
        public static Color Layer6 => ConfigManager.layer6Color.value;
        public static Color Layer7 => ConfigManager.layer7Color.value;
        public static Color Prime => ConfigManager.primeColor.value;
        public static Color WeaponAlt => ConfigManager.altColor.value;
        public static Color Arm0 => ConfigManager.arm0Color.value;
        public static Color Arm1 => ConfigManager.arm1Color.value;
        public static Color Arm2 => ConfigManager.arm2Color.value;
        public static Color BlueSkull => ConfigManager.blueSkullColor.value;
        public static Color RedSkull => ConfigManager.redSkullColor.value;
        public static Color Switch => ConfigManager.switchColor.value;
        public static Color Points => ConfigManager.pointsColor.value;
        public static Color DualWield => ConfigManager.dualwieldColor.value;
        public static Color DoubleJump => ConfigManager.doublejumpColor.value;
        public static Color Confusion => ConfigManager.confusionColor.value;
        public static Color Trap => ConfigManager.trapColor.value;
    }
}
