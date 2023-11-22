using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CoinType
{
    Gold,
    Sliver,
    Bronze
}

public class Coin : MonoBehaviour
{

    public CoinType coinT;
    int coinValue;

    private void Start()
    {
        if (coinT == CoinType.Gold)
        {
            coinValue = 30;
         }
        else if (coinT == CoinType.Sliver)
        {
            coinValue = 20;
        }
        else if (coinT == CoinType.Bronze)
        {
            coinValue = 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerGold playerG = other.GetComponent<PlayerGold>();

            Debug.Log("Coin Player");

            if(playerG != null) 
            { 
                PlayerGold.nowGold += coinValue;
                Debug.Log("Coin Player2");
                Destroy(this.gameObject);
            }



        }
    }
}
