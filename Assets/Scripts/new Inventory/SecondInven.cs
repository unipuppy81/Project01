using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SecondInven : MonoBehaviour
{
    public Transform rootSlot;
    public StoreManager store;

    private List<InvenSlot> slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<InvenSlot>();

        int slotCnt = rootSlot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<InvenSlot>();

            slots.Add(slot);
        }

        store.onSlotClick += BuyItem;
    }

    void BuyItem(ItemProperty item)
    {
        var emptySlot = slots.Find(t =>
        {
            return t.itemPro == null || t.itemPro.name == string.Empty;
        });

        if (emptySlot != null)
        {
            emptySlot.SetItem(item);
        }

    }

    void SortList(List<StoreSlot> list)
    {
        list.Sort();
    }
}
