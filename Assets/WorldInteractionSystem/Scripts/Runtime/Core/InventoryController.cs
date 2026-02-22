using System.Collections.Generic;
using UnityEngine;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Core
{
    public class InventoryController : MonoBehaviour, IInventory
    {
        [SerializeField] private int capacity = 20;
        [SerializeField] private List<InventorySlot> inventorySlots = new();

        public bool AddItem(ItemData itemData, int amount)
        {
            if (itemData == null)
            {
                throw new System.ArgumentNullException(nameof(itemData));
            }

            if (amount <= 0)
            {
                return false;
            }

            int initialAmount = amount;
            int remainingAmount = amount;

            // 1. Try to fill existing stacks first
            if (itemData.IsStackable)
            {
                foreach (var slot in inventorySlots)
                {
                    if (slot.ItemData == itemData && slot.Amount < itemData.MaxStackSize)
                    {
                        int canAdd = itemData.MaxStackSize - slot.Amount;
                        int toAdd = Mathf.Min(canAdd, remainingAmount);

                        slot.AddAmount(toAdd);
                        remainingAmount -= toAdd;

                        if (remainingAmount <= 0)
                        {
                            OnInventoryChanged();
                            return true;
                        }
                    }
                }
            }

            // 2. Add remaining amount to new slots
            while (remainingAmount > 0)
            {
                if (inventorySlots.Count >= capacity)
                {
                    Debug.Log($"{remainingAmount} items couldn't add inventory and were dropped!");
                    if (initialAmount > remainingAmount)
                    {
                        OnInventoryChanged();
                    }

                    return false;
                }

                int toAdd = itemData.IsStackable ? Mathf.Min(remainingAmount, itemData.MaxStackSize) : 1;
                inventorySlots.Add(new InventorySlot(itemData, toAdd));
                remainingAmount -= (itemData.IsStackable ? toAdd : 1);
            }

            OnInventoryChanged();
            return true;
        }

        public void RemoveItem(ItemData itemData, int amount)
        {
            if (inventorySlots.Count == 0)
            {
                return;
            }

            bool inventoryChanged = false;

            for (int i = inventorySlots.Count - 1; i >= 0; i--)
            {
                if (inventorySlots[i].ItemData == itemData)
                {
                    int toRemove = Mathf.Min(amount, inventorySlots[i].Amount);
                    inventorySlots[i].RemoveAmount(toRemove);
                    amount -= toRemove;
                    inventoryChanged = true;

                    if (inventorySlots[i].IsEmpty())
                    {
                        inventorySlots.RemoveAt(i);
                    }

                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }

            if (inventoryChanged)
            {
                OnInventoryChanged();
            }
        }

        public bool HasItem(ItemData itemData, int amount)
        {
            if (inventorySlots.Count == 0)
            {
                return false;
            }

            int total = 0;

            foreach (var slot in inventorySlots)
            {
                if (slot.ItemData == itemData)
                {
                    total += slot.Amount;

                    if (total >= amount)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void OnInventoryChanged()
        {
            EventManager.TriggerOnInventoryChanged();
        }
    }
}