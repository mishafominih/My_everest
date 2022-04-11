using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// В этом компоненте указывается количество слотов в инвенторе
public class UIInventory : MonoBehaviour
{
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private List<GameObject> prefabs;
    public InventoryWithSlots Inventory => controller.Inventory;
    private UIInventoryController controller;
    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        controller = new UIInventoryController(uiSlots, 5);
        Inventory.OnInventoryItemDropEvent += DropItem;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
   
        if (other.CompareTag("Resources"))
        {
            if (controller.Inventory.TryToAdd(this, other.GetComponent<ItemPrefab>().Item))
            {
                Debug.Log("подобран предмет");
                Destroy(other.gameObject);
            }
        }
    }

    private void DropItem(object sender, IInventoryItem item)
    {
        var prefab = prefabs.Find((x) => x.name == item.Info.Title);


        var itemPrefab =Instantiate(prefab, transform.position + 2.5f*Random.onUnitSphere , Quaternion.identity);
        itemPrefab.GetComponent<ItemPrefab>().Item.State.Amount = item.State.Amount;
    }
}
