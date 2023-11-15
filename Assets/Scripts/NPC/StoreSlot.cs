using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;

    public Image image;
    public Button sellBtn;


    public Inventory_ inven_;

    private void Awake()
    {
        SetSellBtnInteractable(false);
    }

    private void Start()
    {
        inven_ = GetComponent<Inventory_>();
    }

    void SetSellBtnInteractable(bool b)
    {
        if(sellBtn != null)
        {
            sellBtn.interactable = b;

        }
    }
    public void SetItem(ItemProperty item)
    {
        this.item = item;

        if(item == null)
        {
            image.enabled = false;
            SetSellBtnInteractable(false);
            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;

            gameObject.name = item.name;
            image.sprite = item.sprite;
            SetSellBtnInteractable(true);
        }
    }

    public void OnClickSellBtn()
    {
        SetItem(null);
    }
}
