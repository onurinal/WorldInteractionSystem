using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public class ItemData : ScriptableObject
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private string itemName;
        [SerializeField] private int itemId;
        [TextArea(3, 2)] [SerializeField] private string itemDescription;
        [SerializeField] private ItemType itemType;

        public GameObject ItemPrefab => itemPrefab;
        public string ItemName => itemName;
        public int ItemId => itemId;

        public ItemType ItemType
        {
            get => itemType;
            protected set => itemType = value;
        }

        public string ItemDescription => itemDescription;
    }
}