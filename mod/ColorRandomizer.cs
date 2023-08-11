using UnityEngine;

namespace ArchipelagoULTRAKILL
{
    public static class ColorRandomizer
    {
        public static void RandomizeGunColors()
        {
            PrefsManager.Instance.SetBool("gunColorType.1", true);
            PrefsManager.Instance.SetBool("gunColorType.1.a", true);
            PrefsManager.Instance.SetBool("gunColorType.2", true);
            PrefsManager.Instance.SetBool("gunColorType.3", true);
            PrefsManager.Instance.SetBool("gunColorType.3.a", true);
            PrefsManager.Instance.SetBool("gunColorType.4", true);
            PrefsManager.Instance.SetBool("gunColorType.5", true);

            for (int i = 1; i <= 5; i++) // weaponNumber
            {
                for (int j = 1; j <= 2; j++) // is alternate?
                {
                    if (j == 2 && (i == 2 || i == 4 || i == 5)) continue; // skip shotgun, railcannon, rocket launcher - no alts
                    bool alt = false;
                    if (j == 2) alt = true;

                    for (int k = 1; k <= 3; k++) // colorNumber
                    {
                        PrefsManager.Instance.SetFloat(string.Concat(new object[]
                        {
                        "gunColor.",
                        i.ToString(),
                        ".",
                        k.ToString(),
                        alt ? ".a" : ".",
                        "r"
                        }), Random.Range(0f, 1f));

                        PrefsManager.Instance.SetFloat(string.Concat(new object[]
                        {
                        "gunColor.",
                        i.ToString(),
                        ".",
                        k.ToString(),
                        alt ? ".a" : ".",
                        "g"
                        }), Random.Range(0f, 1f));

                        PrefsManager.Instance.SetFloat(string.Concat(new object[]
                        {
                        "gunColor.",
                        i.ToString(),
                        ".",
                        k.ToString(),
                        alt ? ".a" : ".",
                        "b"
                        }), Random.Range(0f, 1f));

                        PrefsManager.Instance.SetFloat(string.Concat(new object[]
                        {
                        "gunColor.",
                        i.ToString(),
                        ".",
                        k.ToString(),
                        alt ? ".a" : ".",
                        "a"
                        }), Random.Range(0f, 1f));
                    }
                }
            }

            if (Core.inLevel)
            {
                GunColorController.Instance.UpdateGunColors();

                foreach (GunColorTypeGetter gctg in Object.FindObjectsOfType<GunColorTypeGetter>())
                {
                    gctg.UpdatePreview();
                }
            }
        }

        public static void RandomizeUIColors()
        {
            for (int i = 1; i <= 3; i++)
            {
                string rgb = ".r";
                if (i == 2) rgb = ".g";
                if (i == 3) rgb = ".b";

                PrefsManager.Instance.SetFloat("hudColor.hp" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.hptext" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.hpaft" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.hphdmg" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.hpover" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.stm" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.stmchr" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.stmemp" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.raifull" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.raicha" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.var0" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.var1" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.var2" + rgb, Random.Range(0f, 1f));
                PrefsManager.Instance.SetFloat("hudColor.var3" + rgb, Random.Range(0f, 1f));
            }

            if (Core.inLevel)
            {
                ColorBlindSettings.Instance.UpdateHudColors();
                ColorBlindSettings.Instance.UpdateWeaponColors();
            }
        }
    }
}
