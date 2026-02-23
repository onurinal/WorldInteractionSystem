using System;
using System.Collections.Generic;
using UnityEngine;
using WorldInteractionSystem.Runtime.Manager;

namespace WorldInteractionSystem.Runtime.Core
{
    public class InventoryController : MonoBehaviour, IInventory
    {
        private const int Capacity = 10;
        [SerializeField] private List<InventorySlot> inventorySlots = new();

        public IReadOnlyList<InventorySlot> Slots => inventorySlots;

        public void Initialize()
        {
            InitializeInventory();
        }

        private static void InitializeInventory()
        {
            EventManager.TriggerOnInventoryInitialized(Capacity);
        }

        public int AddItem(ItemData itemData, int amount)
        {
            Validate(itemData, amount);

            int remainingAmount = amount;

            if (itemData.IsStackable)
            {
                remainingAmount = FillExistingStacks(itemData, remainingAmount);
            }

            remainingAmount = FillNewSlots(itemData, remainingAmount);

            int added = amount - remainingAmount;

            if (added <= 0)
            {
                return 0;
            }

            OnInventoryChanged();
            if (remainingAmount > 0)
            {
                Debug.LogWarning(
                    $"{remainingAmount}/{amount} '{itemData.ItemName}' could not be added — inventory full.");
            }

            return added;
        }

        private int FillExistingStacks(ItemData itemData, int remainingAmount)
        {
            foreach (var slot in inventorySlots)
            {
                if (remainingAmount <= 0)
                {
                    break;
                }

                if (slot.ItemData != itemData || slot.Amount >= itemData.MaxStackSize)
                {
                    continue;
                }

                int canAdd = itemData.MaxStackSize - slot.Amount;
                int toAdd = Mathf.Min(canAdd, remainingAmount);

                slot.AddAmount(toAdd);
                remainingAmount -= toAdd;
            }

            return remainingAmount;
        }

        private int FillNewSlots(ItemData itemData, int remainingAmount)
        {
            while (remainingAmount > 0)
            {
                if (IsFull())
                {
                    break;
                }

                int toAdd = itemData.IsStackable ? Mathf.Min(remainingAmount, itemData.MaxStackSize) : 1;
                inventorySlots.Add(new InventorySlot(itemData, toAdd));
                remainingAmount -= toAdd;
            }

            return remainingAmount;
        }

        public void RemoveItem(ItemData itemData, int amount)
        {
            Validate(itemData, amount);

            var remainingAmount = amount;

            for (int i = inventorySlots.Count - 1; remainingAmount > 0; i--)
            {
                if (inventorySlots[i].ItemData != itemData)
                {
                    continue;
                }

                int toRemove = Mathf.Min(remainingAmount, inventorySlots[i].Amount);
                inventorySlots[i].RemoveAmount(toRemove);
                remainingAmount -= toRemove;

                if (inventorySlots[i].IsEmpty())
                {
                    inventorySlots.RemoveAt(i);
                }
            }

            int removed = amount - remainingAmount;
            if (removed > 0)
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

        private static void Validate(ItemData itemData, int amount)
        {
            if (itemData == null)
            {
                throw new ArgumentNullException(nameof(itemData));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
            }
        }

        private bool IsFull()
        {
            if (inventorySlots.Count >= Capacity)
            {
                return true;
            }

            return false;
        }

        private void OnInventoryChanged()
        {
            EventManager.TriggerOnInventoryChanged(Slots);
        }
    }
}