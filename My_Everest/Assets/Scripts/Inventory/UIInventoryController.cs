using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController 
{

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


    private void OnInventorySteteChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
    
    
}
