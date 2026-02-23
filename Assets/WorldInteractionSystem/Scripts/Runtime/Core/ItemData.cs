using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private string itemName;
        [SerializeField] private int itemId;
        [SerializeField] private Sprite itemSprite;
        [TextArea(3, 2)] [SerializeField] private string itemDescription;
        [SerializeField] private ItemType itemType;
        [SerializeField] private int maxStackSize = 99;
        [SerializeField] private bool isStackable = true;

        public GameObject ItemPrefab => itemPrefab;
        public string ItemName => itemName;
        public int ItemId => itemId;
        public Sprite ItemSprite => itemSprite;

        public ItemType ItemType
        {
            get => itemType;
            protected set => itemType = value;
        }

        public string ItemDescription => itemDescription;
        public int MaxStackSize => maxStackSize;
        public bool IsStackable => isStackable;
    }
}