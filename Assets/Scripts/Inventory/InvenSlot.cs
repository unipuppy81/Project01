using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour, IPointerUpHandler
{
    [Header("Inven First")]
    public int slotNum;
    public Item item_f;
    public Image itemIcon;


    [Header("Inven Second")]

    public ItemProperty itemPro;

    public Image image_s;


    // ************************* First Inven **************************** //
    public void UpDateSlotUI()
    {
        itemIcon.sprite = item_f.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item_f = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item_f.Use();

        if(isUse)
        {
            Inventory.instance.RemoveItem(slotNum);
        }
    }


    // ************************* Second Inven **************************** //

    public void SetItem(ItemProperty _item)
    {
        this.itemPro = _item;

        if (itemPro == null)
        {
            image_s.enabled = false;

            gameObject.name = "Empty";
        }
        else
        {
            image_s.enabled = true;

            gameObject.name = _item.name;
            image_s.sprite = _item.sprite;
        }
    }
}
