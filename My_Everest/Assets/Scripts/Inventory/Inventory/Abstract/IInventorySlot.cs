using System;

public interface IInventorySlot
{
    bool IsFull { get; }
    bool IsEmpty { get; }

    IInventoryItem Item { get; }
    Type ItemType { get; }          // Для удобства будет возвращать Item.Type
    int Amount { get; }             // количество объектов в слоте. Так же для удобства будет возвращать Item.Amount
    int Capacity { get; }           // Емкость слота. Значение так же берем с Item.MaxItemsInInventorySlot

    void SetItem(IInventoryItem item);
    void Clear();
}