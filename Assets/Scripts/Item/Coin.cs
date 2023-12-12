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
            coinValue = 100;
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
            DataManager.Instance.pData.gold += coinValue;
            DataManager.Instance.SaveGameData();
            Destroy(this.gameObject);
        }
    }
}
