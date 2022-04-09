using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private TextMeshProUGUI textAmount;

    public IInventoryItem item { get; private set; }

    public void Refresh(IInventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            CleanUp();
            return;
        }

        item = slot.Item;
        imageIcon.sprite = item.Info.SpriteIcon;
        
        imageIcon.gameObject.SetActive(true);
        var textAmountEnabled = slot.Amount > 1;
        textAmount.gameObject.SetActive(textAmountEnabled);
        if (textAmountEnabled)
            textAmount.text = $"x{slot.Amount}";
    }

    private void CleanUp()
    {
        textAmount.gameObject.SetActive(false);
        imageIcon.gameObject.SetActive(false);
    }
}
