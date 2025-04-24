using Archipelago.MultiClient.Net.Enums;
using UnityEngine;

namespace ArchipelagoULTRAKILL.Components
{
    public class ShopAutoHint : MonoBehaviour
    {
        public long id = 0;

        public void OnEnable()
        {
            if (id > 0)
            {
                Multiworld.Session?.Locations.ScoutLocationsAsync(HintCreationPolicy.CreateAndAnnounceOnce, id);
            }
        }
    }
}
