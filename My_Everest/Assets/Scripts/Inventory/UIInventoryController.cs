using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController 
{
    private InventoryItemInfo _diamondInfo;
    private InventoryItemInfo _microcircuitInfo;
    private UIInventorySlot[] _uiSlots;
    
    public InventoryWithSlots Inventory { get; }

    public UIInventoryController(UIInventorySlot[] uiInventorySlots, int capacity)
    {
        _uiSlots = uiInventorySlots;
        
        Inventory = new InventoryWithSlots(capacity);
        
        SetupInventoryUI(Inventory);
        Inventory.OnInventoryStateChangeEvent += OnInventorySteteChanged;
    }
    

    private void SetupInventoryUI(InventoryWithSlots inventoryWithSlots)
    {
        var allSlots = Inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }
    private IInventorySlot AddRandomDiamond(List<IInventorySlot> slots)
    {
        var rslotIndex = Random.Range(0, slots.Count);
        var rslot = slots[rslotIndex];
        var rCount = Random.Range(1, 4);
        var diamond = new Item(_diamondInfo);
        diamond.State.Amount = rCount;
        
        Inventory.TryToAddToSlot(this, rslot, diamond);
        return rslot;
    }
    

    private void OnInventorySteteChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
    
    
}
