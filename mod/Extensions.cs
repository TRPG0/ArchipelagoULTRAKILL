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
    }
}
