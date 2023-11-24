using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    UIManager uiManager;
    private InventoryController inventory;
    public GameObject itemButton;
    public string itemName;

    public GameObject coinError;
    public ConsumeItemType consumeType;
    public int potionCost;

    void Start()
    {
        inventory = FindObjectOfType<InventoryController>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(consumeType == ConsumeItemType.HealthPotion)
        {
            potionCost = 1000;
        }
        else if(consumeType == ConsumeItemType.ManaPotion)
        {
            potionCost = 500;
        }
    }

    public void GetItem()
    {
        if(PlayerGold.nowGold < potionCost) 
        {
            Debug.Log("Error");
            uiManager.coinErrorEnter();
        }
        else if(PlayerGold.nowGold > potionCost)
        { 
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == true && inventory.slots[i].transform.GetComponent<SlotR>().amount < 9)
                {
                    if (itemName == inventory.slots[i].transform.GetComponentInChildren<Spawn>().itemName)
                    {
                        inventory.slots[i].GetComponent<SlotR>().amount += 1;
                        break;
                    }
                }
                else if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    inventory.slots[i].GetComponent<SlotR>().amount += 1;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem();
        }
    }
}
