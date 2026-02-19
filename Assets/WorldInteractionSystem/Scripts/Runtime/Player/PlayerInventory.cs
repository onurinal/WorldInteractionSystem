using System.Collections.Generic;
using UnityEngine;
using WorldInteractionSystem.Runtime.Core;

namespace WorldInteractionSystem.Runtime.Player
{
    public sealed class PlayerInventory
    {
        private readonly Dictionary<ItemData, int> inventory = new();

        public void AddItem(ItemData itemData, int amount)
        {
            if (itemData == null || amount <= 0)
            {
                return;
            }

            if (inventory.TryGetValue(itemData, out int currentAmount))
            {
                inventory[itemData] = currentAmount + amount;
            }
            else
            {
                inventory[itemData] = amount;
            }
        }

        public void RemoveItem(ItemData itemData, int amount)
        {
            if (itemData == null || amount <= 0)
            {
                return;
            }

            if (!inventory.TryGetValue(itemData, out int currentAmount))
            {
                return;
            }

            var newAmount = currentAmount - amount;
            if (newAmount <= 0)
            {
                inventory.Remove(itemData);
            }
            else
            {
                inventory[itemData] = newAmount;
            }
        }

        public bool HasItem(ItemData itemData)
        {
            if (inventory.ContainsKey(itemData))
            {
                return true;
            }

            return false;
        }

        public int GetAmount(ItemData item)
        {
            return inventory.TryGetValue(item, out var amount) ? amount : 0;
        }
    }
}