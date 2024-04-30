using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBtn : MonoBehaviour
{
    public GameObject clear;
    public GameObject non;

    public GameObject thisPanel;

    public void ClearSet()
    {
        thisPanel.SetActive(true); 
        clear.SetActive(true);
        non.SetActive(false);
        thisPanel.SetActive(false);
    }
}
