using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// В этом компоненте указывается количество слотов в инвенторе
public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject pref;
    public InventoryWithSlots Inventory => controller.Inventory;
    private UIInventoryController controller;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            controller.Inventory.TryToAdd(this, pref.GetComponent<ItemPrefab>().Item);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
   
        if (other.CompareTag("Resources"))
        {
            if (controller.Inventory.TryToAdd(this, other.GetComponent<ItemPrefab>().Item))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        controller = new UIInventoryController(uiSlots, 5);
    }
}
