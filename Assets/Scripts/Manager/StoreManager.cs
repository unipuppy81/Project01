using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    private InventoryController inventory;
    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    private List<StoreSlot> slots;

    public System.Action<ItemProperty> onSlotClick;
    void Start()
    {
        inventory = FindObjectOfType<InventoryController>();

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

    public void GetItem()
    {
       /* for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == true && inventory.slots[i].transform.GetComponent<SlotR>().amount < 2)
            {
                if (itemName == inventory.slots[i].transform.GetComponentInChildren<Spawn>().itemName)
                {
                    Destroy(gameObject);
                    inventory.slots[i].GetComponent<SlotR>().amount += 1;
                    break;
                }
            }
            else if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                inventory.slots[i].GetComponent<SlotR>().amount += 1;
                Destroy(gameObject);
                break;
            }
        }*/
    }
}
