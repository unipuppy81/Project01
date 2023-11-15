using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory_ : MonoBehaviour
{
    public Transform rootSlot;
    public StoreManager store;

    public List<StoreSlot> slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<StoreSlot>();

        int slotCnt = rootSlot.childCount;

        for(int i =0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<StoreSlot>();

            slots.Add(slot);
        }

        store.onSlotClick += BuyItem;
    }

    void BuyItem(ItemProperty item)
    {
        var emptySlot = slots.Find(t =>
        {
            return t.item == null || t.item.name == string.Empty;
        });

        if(emptySlot != null)
        {
            emptySlot.SetItem(item);
        }

    }

    public void SortList(List<StoreSlot> list)
    {
        list.Sort();
    }
}
