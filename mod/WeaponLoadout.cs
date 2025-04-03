namespace ArchipelagoULTRAKILL
{
    public class WeaponLoadout
    {
        public enum EquipState
        {
            None,
            Standard,
            Alternate
        }

        public EquipState rev0;
        public EquipState rev1;
        public EquipState rev2;

        public EquipState sho0;
        public EquipState sho1;
        public EquipState sho2;

        public EquipState nai0;
        public EquipState nai1;
        public EquipState nai2;

        public EquipState rai0;
        public EquipState rai1;
        public EquipState rai2;

        public EquipState rock0;
        public EquipState rock1;
        public EquipState rock2;

        public EquipState arm0;
        public EquipState arm1;
        public EquipState arm2;

        public static WeaponLoadout Get()
        {
            return new WeaponLoadout()
            {
                rev0 = (EquipState)PrefsManager.Instance.GetInt("weapon.rev0", 0),
                rev1 = (EquipState)PrefsManager.Instance.GetInt("weapon.rev1", 0),
                rev2 = (EquipState)PrefsManager.Instance.GetInt("weapon.rev2", 0),

                sho0 = (EquipState)PrefsManager.Instance.GetInt("weapon.sho0", 0),
                sho1 = (EquipState)PrefsManager.Instance.GetInt("weapon.sho1", 0),
                sho2 = (EquipState)PrefsManager.Instance.GetInt("weapon.sho2", 0),

                nai0 = (EquipState)PrefsManager.Instance.GetInt("weapon.nai0", 0),
                nai1 = (EquipState)PrefsManager.Instance.GetInt("weapon.nai1", 0),
                nai2 = (EquipState)PrefsManager.Instance.GetInt("weapon.nai2", 0),

                rai0 = (EquipState)PrefsManager.Instance.GetInt("weapon.rai0", 0),
                rai1 = (EquipState)PrefsManager.Instance.GetInt("weapon.rai1", 0),
                rai2 = (EquipState)PrefsManager.Instance.GetInt("weapon.rai2", 0),

                rock0 = (EquipState)PrefsManager.Instance.GetInt("weapon.rock0", 0),
                rock1 = (EquipState)PrefsManager.Instance.GetInt("weapon.rock1", 0),
                rock2 = (EquipState)PrefsManager.Instance.GetInt("weapon.rock2", 0),

                arm0 = (EquipState)PrefsManager.Instance.GetInt("weapon.arm0", 0),
                arm1 = (EquipState)PrefsManager.Instance.GetInt("weapon.arm1", 0),
                arm2 = (EquipState)PrefsManager.Instance.GetInt("weapon.arm2", 0),
            };
        }

        public void Set()
        {
            PrefsManager.Instance.SetInt("weapon.rev0", (int)rev0);
            PrefsManager.Instance.SetInt("weapon.rev1", (int)rev1);
            PrefsManager.Instance.SetInt("weapon.rev2", (int)rev2);

            PrefsManager.Instance.SetInt("weapon.sho0", (int)sho0);
            PrefsManager.Instance.SetInt("weapon.sho1", (int)sho1);
            PrefsManager.Instance.SetInt("weapon.sho2", (int)sho2);

            PrefsManager.Instance.SetInt("weapon.nai0", (int)nai0);
            PrefsManager.Instance.SetInt("weapon.nai1", (int)nai1);
            PrefsManager.Instance.SetInt("weapon.nai2", (int)nai2);

            PrefsManager.Instance.SetInt("weapon.rai0", (int)rai0);
            PrefsManager.Instance.SetInt("weapon.rai1", (int)rai1);
            PrefsManager.Instance.SetInt("weapon.rai2", (int)rai2);

            PrefsManager.Instance.SetInt("weapon.rock0", (int)rock0);
            PrefsManager.Instance.SetInt("weapon.rock1", (int)rock1);
            PrefsManager.Instance.SetInt("weapon.rock2", (int)rock2);

            PrefsManager.Instance.SetInt("weapon.arm0", (int)arm0);
            PrefsManager.Instance.SetInt("weapon.arm1", (int)arm1);
            PrefsManager.Instance.SetInt("weapon.arm2", (int)arm2);
        }
    }
}
