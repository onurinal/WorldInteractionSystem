using UnityEngine;

namespace WorldInteractionSystem.Runtime.Core
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private int amount;

        public InventorySlot(ItemData itemData, int amount)
        {
            this.itemData = itemData;
            this.amount = amount;
        }

        public ItemData ItemData => itemData;
        public int Amount => amount;
        public void AddAmount(int value) => amount += value;

        public int RemoveAmount(int value) => amount = Mathf.Max(0, amount - value);

        public bool IsEmpty()
        {
            if (itemData == null || amount == 0)
            {
                return true;
            }

            return false;
        }
    }
}