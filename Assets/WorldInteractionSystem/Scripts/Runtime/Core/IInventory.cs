namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInventory
    {
        void AddItem(ItemData item, int amount);
        void RemoveItem(ItemData item, int amount);
        bool HasItem(ItemData item);
        int GetAmount(ItemData item);
    }
}