using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "WorldInteractionSystem/Interactable/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private string itemName;
        [SerializeField] private int itemId;

        public GameObject ItemPrefab => itemPrefab;
        public string ItemName => itemName;
        public int ItemId => itemId;
    }
}