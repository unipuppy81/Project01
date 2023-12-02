using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    static public int nowGold;


    private void Start()
    {
        DataManager.Instance.LoadGameData();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) { Debug.Log(nowGold); }
        nowGold = DataManager.Instance.pData.gold;
    }
}
