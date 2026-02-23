using System;
using System.Collections.Generic;

namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInventory
    {
        int AddItem(ItemData item, int amount);
        void RemoveItem(ItemData item, int amount);
        bool HasItem(ItemData item, int amount);
        IReadOnlyList<InventorySlot> Slots { get; }
    }
}