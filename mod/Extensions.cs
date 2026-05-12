using System.Collections.Generic;
using UnityEngine;

namespace ArchipelagoULTRAKILL
{
    public static class Extensions
    {
        public static string GetFullPath(this Transform transform)
        {
            string path = transform.name;
            Transform current = transform;

            while (current.parent != null)
            {
                path = current.parent.name + "/" + path;
                current = current.parent;
            }

            return path;
        }

        public static string GetFullPath(this GameObject gameObject)
        {
            return GetFullPath(gameObject.transform);
        }

        public static string ToReadableName(this EnemyType enemyType)
        {
            Dictionary<EnemyType, string> names = new Dictionary<EnemyType, string>()
            {
                [EnemyType.Minos] = "The Corpse of King Minos",
                [EnemyType.Sisyphus] = "Insurrectionist",
                [EnemyType.MirrorReaper] = "Mirror Reaper",
                [EnemyType.V2Second] = "V2 (2nd)",
                [EnemyType.Turret] = "Sentry",
                [EnemyType.Centaur] = "Earthmover",
                [EnemyType.MaliciousFace] = "Malicious Face",
                [EnemyType.HideousMass] = "Hideous Mass",
                [EnemyType.Geryon] = "Geryon, Watcher of the Skies",
                [EnemyType.Gabriel] = "Gabriel, Judge of Hell",
                [EnemyType.GabrielSecond] = "Gabriel, Apostate of Hate",
                [EnemyType.FleshPrison] = "Flesh Prison",
                [EnemyType.FleshPanopticon] = "Flesh Panopticon",
                [EnemyType.MinosPrime] = "Minos Prime",
                [EnemyType.SisyphusPrime] = "Sisyphus Prime",
                [EnemyType.VeryCancerousRodent] = "Very Cancerous Rodent",
                [EnemyType.Mandalore] = "Mysterious Druid Knight (& Owl)"
            };

            if (names.ContainsKey(enemyType)) return names[enemyType];
            else return enemyType.ToString();
        }
    }
}
