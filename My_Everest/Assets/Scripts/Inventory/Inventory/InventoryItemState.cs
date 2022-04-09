using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class InventoryItemState : IInventoryItemState
{
    public int amount;
    public bool isEquipped;
    
    
    public int Amount { get=>amount; set=>amount = value; }
    public bool IsEquipped { get=>isEquipped; set=>isEquipped=value; }

    public InventoryItemState()
    {
        amount = 1;
        isEquipped = false;
    }
}
