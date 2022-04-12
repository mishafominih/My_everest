using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Items/Create new InventoryItemInfo")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string id;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private int maxItemInInventorySlot;
    [SerializeField] private Sprite spriteIcon;
    [SerializeField] private Resource resourceType;
    public string Id => id;
    public string Title => title;
    public string Description => description;
    public int MaxItemInInventorySlot => maxItemInInventorySlot;
    public Sprite SpriteIcon => spriteIcon;
    public Resource ResourceType => resourceType;
}
