using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItemInfo 
{
    string Id { get; }
    string Title { get; }
    string Description { get; }
    int MaxItemInInventorySlot { get; }         // Максимальное число предметов в слоте. Необязательное св-во
    Sprite SpriteIcon { get; }
    
}
