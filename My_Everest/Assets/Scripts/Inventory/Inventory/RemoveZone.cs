using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveZone : MonoBehaviour, IDropHandler
{
    private UIInventory _uiInventory;

    private void Start()
    {
        _uiInventory = GetComponentInParent<UIInventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var itemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var slot = itemUI.GetComponentInParent<UIInventorySlot>().Slot;
        var inventory = _uiInventory.Inventory;
        
        inventory.RemoveItemFromSlot(this,slot,  itemUI.item);
    }
}
