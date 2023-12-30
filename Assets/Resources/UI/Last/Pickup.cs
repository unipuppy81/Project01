using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    QuestManager qManager;
    GameObject otherObject;
    UIManager uiManager;
    private InventoryController inventory;
    public GameObject itemButton;
    public string itemName;

    public GameObject coinError;
    public ConsumeItemType consumeType;
    public int potionCost;

    void Start()
    {
        otherObject = GameObject.Find("QuestManager");
        qManager = otherObject.GetComponent<QuestManager>();
        inventory = FindObjectOfType<InventoryController>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(consumeType == ConsumeItemType.HealthPotion)
        {
            potionCost = 0;
        }
        else if(consumeType == ConsumeItemType.ManaPotion)
        {
            potionCost = 0;
        }
    }

    public void GetItem()
    {
        if(PlayerGold.nowGold < potionCost) 
        {
            uiManager.coinErrorEnter();
        }
        else if(PlayerGold.nowGold > potionCost)
        { 
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == true && inventory.slots[i].transform.GetComponent<SlotR>().amount < 99)
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
