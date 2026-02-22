namespace WorldInteractionSystem.Runtime.Core
{
    public interface IInventory
    {
        bool AddItem(ItemData item, int amount);
        void RemoveItem(ItemData item, int amount);
        bool HasItem(ItemData item, int amount);
    }
}