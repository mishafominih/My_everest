using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots: IInventory
{
    public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
    public event Action<object, Type, int> OnInventoryItemRemovedEvent;
    public event Action<object> OnInventoryStateChangeEvent;
    public int Capacity { get; set; }
    public bool IsFull => _slots.All(slot => slot.IsFull);

    private List<IInventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        this.Capacity = capacity;
         _slots = new List<IInventorySlot>();
        
        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }                         
    }
    
    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find((slot => slot.ItemType == itemType)).Item;
    }

    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();
        foreach (var slot in _slots)
        {
            if(!slot.IsEmpty)
                allItems.Add(slot.Item);
        }
        return allItems.ToArray();
    }

    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType);
        foreach (var slot in slotsOfType)
        {
            if(!slot.IsEmpty)
                allItemsOfType.Add(slot.Item);
        }

        return allItemsOfType.ToArray();
    }

    public IInventoryItem[] GetEquippedTItems(Type itemType)
    {
        var equippedItem= new List<IInventoryItem>();
        var requiredSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped);
        foreach (var slot in requiredSlots)
        {
            if(!slot.IsEmpty)
                equippedItem.Add(slot.Item);
        }

        return equippedItem.ToArray();
    }

    public int GetItemAmount(Type itemType)
    {
        var amount = 0;
        var allItemSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType);
        foreach (var slot in allItemSlots)
        {
            amount += slot.Amount;
        }

        return amount;
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty =
            _slots.Find(slot => !slot.IsEmpty && slot.Item.Info.Title == item.Info.Title && !slot.IsFull);
        if (slotWithSameItemButNotEmpty != null)
            return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);

        var emptySlot = _slots.Find(slot => slot.IsEmpty);
        if (emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);
        
        Debug.Log($"Нельзя добавить предмет, тк нет места\nItemType [{item.Type}] Amount = {item.State.Amount}");
        return false;
    }

    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.Amount + item.State.Amount <= item.Info.MaxItemInInventorySlot;
        var amountToAdd = fits ? item.State.Amount : item.Info.MaxItemInInventorySlot - slot.Amount;
        var amountLeft = item.State.Amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.State.Amount = amountToAdd;

        if (slot.IsEmpty)
            slot.SetItem(clonedItem);
        else
            slot.Item.State.Amount += amountToAdd;
        Debug.Log($"Предмет добавлен в инвентарь\nItemType [{item.Type}] Amount = {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(sender,item, amountToAdd);
        OnInventoryStateChangeEvent?.Invoke(sender);

        if (amountLeft <= 0)
            return true;

        item.State.Amount = amountLeft;
        return TryToAdd(sender, item);
    }
    
    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = GetAllSlots(itemType);
        if (slotsWithItem.Length ==0)
            return;

        var amountToRemove = amount;
        var countOfSlots = slotsWithItem.Length;
        for (int i = countOfSlots - 1 ; i >= 0; i--)
        {
            var slot = slotsWithItem[i];
            if (slot.Amount>=amountToRemove)
            {
                slot.Item.State.Amount -= amountToRemove;
                slot.Clear();
                Debug.Log($"Предмет удален из инвентаря.\nItemType - [{itemType}] Amount = {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChangeEvent?.Invoke(sender);
                break;
            }

            var amountRemoved = slot.Amount;
            amountToRemove -= slot.Amount;
            slot.Clear(); 
            Debug.Log($"Предмет удален из инвентаря.\nItemType - [{itemType}] Amount = {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);
            OnInventoryStateChangeEvent?.Invoke(sender);
        }
    }

    private void Swap(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        var toSlotItem = toSlot.Item.Clone();
            
        toSlot.Clear();
        toSlot.SetItem(fromSlot.Item.Clone());
            
        fromSlot.Clear();
        fromSlot.SetItem(toSlotItem);
    }
    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.IsEmpty)
            return;
        if(toSlot.IsFull)
            return;
        if (fromSlot==toSlot)
            return;
        
        if (!toSlot.IsEmpty && toSlot.Item.Info.Title != fromSlot.Item.Info.Title)
        {
            Swap(sender,fromSlot,toSlot);
            return;
        }

        var slotCapacity = fromSlot.Capacity;
        var fits = fromSlot.Amount + toSlot.Amount <= slotCapacity;
        var amountToAdd = fits ? fromSlot.Amount : slotCapacity - toSlot.Amount;
        var amountLeft = fromSlot.Amount - amountToAdd;

        if (toSlot.IsEmpty)
        {
            toSlot.SetItem(fromSlot.Item);
            fromSlot.Clear();
            OnInventoryStateChangeEvent?.Invoke(sender);
        }

        toSlot.Item.State.Amount += amountToAdd;
        if (fits)
            fromSlot.Clear();
        else
            fromSlot.Item.State.Amount = amountLeft;
        OnInventoryStateChangeEvent?.Invoke(sender);
    }
    private IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
    }
    
    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);
        return item != null;
    }
    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
}