using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    private List<StoreSlot> slots;

    public System.Action<ItemProperty> onSlotClick;
    void Start()
    {
        slots = new List<StoreSlot>();

        int slotCnt = slotRoot.childCount;
        
        for(int i=0; i< slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<StoreSlot>();

            if(i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i]);
            }
            else
            {
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }

            slots.Add(slot);
        }

        onSlotClick += OnClickSlotEx;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickSlotEx(ItemProperty prop)
    {

    }

    public void OnClickSlot(StoreSlot slot)
    {
        if(onSlotClick != null)
        {
            onSlotClick(slot.item);
        }

        Debug.Log(slot.name);
    }
}
