using System;
using UnityEngine;

public class Item : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();
    public Item(IInventoryItemInfo info)
    {
        this.Info = info;
        State = new InventoryItemState();
    }
    
    public IInventoryItem Clone()
    {

        var clonedDiamond = new Item(Info)
        {
            State =
            {
                Amount = State.Amount
            }
        };
        return clonedDiamond;
    }
    
}