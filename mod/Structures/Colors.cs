using ArchipelagoULTRAKILL.Config;
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
        public static Color ActHighlight => ColorConfig.actHighlightColor.value;
        public static Color ActComplete => ColorConfig.actCompleteColor.value;
        public static Color ActGoal => ColorConfig.actGoalColor.value;
        public static Color PlayerSelf => ColorConfig.APPlayerSelf.value;
        public static Color PlayerOther => ColorConfig.APPlayerOther.value;
        public static Color ItemAdvancement => ColorConfig.APItemAdvancement.value;
        public static Color ItemNeverExclude => ColorConfig.APItemNeverExclude.value;
        public static Color ItemFiller => ColorConfig.APItemFiller.value;
        public static Color ItemTrap => ColorConfig.APItemTrap.value;
        public static Color Location => ColorConfig.APLocation.value;
        public static Color Layer0 => ColorConfig.layer0Color.value;
        public static Color Layer1 => ColorConfig.layer1Color.value;
        public static Color Layer2 => ColorConfig.layer2Color.value;
        public static Color Layer3 => ColorConfig.layer3Color.value;
        public static Color Layer4 => ColorConfig.layer4Color.value;
        public static Color Layer5 => ColorConfig.layer5Color.value;
        public static Color Layer6 => ColorConfig.layer6Color.value;
        public static Color Layer7 => ColorConfig.layer7Color.value;
        public static Color Layer8 => ColorConfig.layer8Color.value;
        public static Color Encore0 => ColorConfig.encore0Color.value;
        public static Color Encore1 => ColorConfig.encore1Color.value;
        public static Color Prime => ColorConfig.primeColor.value;
        public static Color WeaponAlt => ColorConfig.altColor.value;
        public static Color VariationBlue => ColorBlindSettings.Instance.variationColors[0];
        public static Color VariationGreen => ColorBlindSettings.Instance.variationColors[1];
        public static Color VariationRed => ColorBlindSettings.Instance.variationColors[2];
        public static Color Stamina => ColorBlindSettings.Instance.staminaColor;
        public static Color BlueSkull => ColorConfig.blueSkullColor.value;
        public static Color RedSkull => ColorConfig.redSkullColor.value;
        public static Color Switch => ColorConfig.switchColor.value;
        public static Color Points => ColorConfig.pointsColor.value;
        public static Color Overheal => ColorBlindSettings.Instance.overHealColor;
        public static Color DualWield => ColorConfig.dualwieldColor.value;
        public static Color DoubleJump => ColorConfig.doublejumpColor.value;
        public static Color Confusion => ColorConfig.confusionColor.value;
        public static Color Trap => ColorConfig.trapColor.value;
    }
}
