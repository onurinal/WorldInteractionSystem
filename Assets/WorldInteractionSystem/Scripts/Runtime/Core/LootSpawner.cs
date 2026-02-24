using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private Transform lootSpawnPoint;

        [Header("Settings")] [SerializeField] private ItemData itemToDrop;

        public void SpawnLoot()
        {
            if (itemToDrop == null || itemToDrop.ItemPrefab == null)
            {
                return;
            }

            Instantiate(itemToDrop.ItemPrefab, lootSpawnPoint.position, Quaternion.identity);
        }
    }
}