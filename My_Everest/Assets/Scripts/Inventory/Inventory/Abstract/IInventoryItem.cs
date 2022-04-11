using System;
using UnityEngine;

public interface IInventoryItem 
{
    IInventoryItemInfo Info { get; }
    IInventoryItemState State { get; }
    Type Type { get; }
    IInventoryItem Clone();
}
