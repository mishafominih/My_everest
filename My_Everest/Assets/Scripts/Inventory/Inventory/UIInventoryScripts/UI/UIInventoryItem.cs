using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : UIItem, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private TextMeshProUGUI discription;

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

        discription.text = item.Info.Description;
        discription.gameObject.SetActive(false);
    }

    private void CleanUp()
    {
        textAmount.gameObject.SetActive(false);
        imageIcon.gameObject.SetActive(false);
        discription.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        discription.gameObject.SetActive(!string.IsNullOrEmpty(discription.text));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        discription.gameObject.SetActive(false);
    }
}
