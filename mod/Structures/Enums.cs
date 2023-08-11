using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchipelagoULTRAKILL.Structures
{
    public class Enums
    {
        public enum UKItemType
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
            Fire2
        }

        public enum Powerup
        {
            DualWield,
            InfiniteStamina,
            StaminaLimiter,
            WalljumpLimiter,
            EmptyAmmo,
            DoubleJump,
            Radiance
        }

        public enum ColorOptions
        {
            Off,
            Once,
            EveryLoad
        }
    }
}
