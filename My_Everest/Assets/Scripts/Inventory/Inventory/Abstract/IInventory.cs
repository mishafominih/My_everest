using System;

public interface IInventory 
{
    int Capacity { get; set; } // Количество слотов
    bool IsFull { get; }

    IInventoryItem GetItem(Type itemType);
    IInventoryItem[] GetAllItems();
    IInventoryItem[] GetAllItems(Type temType);
    IInventoryItem[] GetEquippedTItems(Type temType);
    int GetItemAmount(Type itemType);

    bool TryToAdd(object sender, IInventoryItem item);
    void Remove(object sender, Type itemType, int amount = 1);
    bool HasItem(Type type, out IInventoryItem item);
}