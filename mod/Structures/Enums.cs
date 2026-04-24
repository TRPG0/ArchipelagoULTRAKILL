using System;

namespace ArchipelagoULTRAKILL.Structures
{
    [Flags]
    public enum InfoFlags
    {
        None = 0,
        HasSecrets = 1 << 0,
        HasSecretExit = 1 << 1,
        HasWeapon = 1 << 2,
        HasSecretWeapon = 1 << 3,
        HasRandomMusic = 1 << 4,
        HasSkullsNormal = 1 << 5,
        HasSkullsSpecial = 1 << 6,
        HasAnySkulls = HasSkullsNormal | HasSkullsSpecial
    }

    public enum UKType
    {
        Weapon,
        WeaponAlt,
        Arm,
        Ability,
        Skull,
        Level,
        Layer,
        Points,
        Powerup,
        Trap,
        Soap,
        Fire2,
        LimboSwitch,
        ShotgunSwitch,
        ClashMode,
        SecretMission
    }

    public enum Powerup
    {
        None,
        HardDamage,
        Overheal,
        DualWield,
        InfiniteStamina,
        StaminaLimiter,
        WalljumpLimiter,
        EmptyAmmo,
        DoubleJump,
        Radiance,
        Confusion,
        QuickCharge,
        NoArms,
        Sandstorm
    }

    public enum EnemyOptions
    {
        Disabled,
        Bosses,
        Extra,
        All
    }

    public enum ColorOptions
    {
        Off,
        Once,
        EveryLoad
    }

    public enum WeaponForm
    {
        Standard,
        Alternate
    }

    public enum Fire2Options
    {
        Disabled,
        Split,
        Progressive
    }

    public enum SecretUnlockType
    {
        SecretExits,
        Items
    }

    public enum SecretExitType
    {
        Standard,
        AddRewards
    }

    public enum LogFont
    {
        Pixel1,
        Pixel2,
        SansSerif
    }
}
