using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotR : MonoBehaviour
{
    private InventoryController inventory;
    public int i;
    public TextMeshProUGUI amountText;
    public int amount;


    void Start()
    {
        inventory = FindObjectOfType<InventoryController>();    
    }

    void Update()
    {
        amountText.text = amount.ToString();

        if(transform.childCount == 2)
        {
            inventory.isFull[i] = false;
        }
    }
}
