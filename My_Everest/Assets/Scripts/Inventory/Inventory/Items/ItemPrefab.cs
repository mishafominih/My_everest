using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo info;
    public Item Item { get; private set; }

    private void Awake()
    {
        Item = new Item(info);
    }
}
