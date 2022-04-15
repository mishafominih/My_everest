using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// В этом компоненте указывается количество слотов в инвенторе
public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField]private float colSphereRadius;
    public InventoryWithSlots Inventory => controller.Inventory;
    private UIInventoryController controller;
    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        controller = new UIInventoryController(uiSlots, 5);
        Inventory.OnInventoryItemDropEvent += DropItem;
    }

    public void OpenInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }

    public Dictionary<Resource, int> GetAllItems()
    {
        var res = new Dictionary<Resource, int>();
        foreach (var slot in controller.Inventory.GetAllSlots())
        {
            if (!slot.IsEmpty) {
                var resource_type = slot.Item.Info.ResourceType;
                var count = slot.Amount;
                if (res.Keys.Contains(resource_type))
                    res[resource_type] += count;
                else
                    res[resource_type] = count;
            }
        }
        return res;
    }

    public void TryToPickUp()
    {
        Collider2D[] colliders;
        Debug.DrawLine(transform.localPosition, transform.localPosition*6, Color.cyan);

        colliders = Physics2D.OverlapCircleAll(transform.position, colSphereRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Resources"))
            {
                if (controller.Inventory.TryToAdd(this, collider.GetComponent<ItemPrefab>().Item))
                {
                    Debug.Log("подобран предмет");
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        // if (isBtnPressed)
        // {
        //     if (other.CompareTag("Resources"))
        //     {
        //         if (controller.Inventory.TryToAdd(this, other.GetComponent<ItemPrefab>().Item))
        //         {
        //             Debug.Log("подобран предмет");
        //             Destroy(other.gameObject);
        //         }
        //     }
        // }
        
    }
    

    private void DropItem(object sender, IInventoryItem item)
    {
        var prefab = prefabs.Find((x) => x.name == item.Info.Title);


        var itemPrefab =Instantiate(prefab, transform.position , Quaternion.identity);
        itemPrefab.GetComponent<ItemPrefab>().Item.State.Amount = item.State.Amount;
    }
}
