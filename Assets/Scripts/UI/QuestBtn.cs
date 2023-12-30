using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBtn : MonoBehaviour
{
    public GameObject clear;
    public GameObject non;

    public void ClearSet()
    {
        clear.SetActive(true);
        non.SetActive(false);
    }
}
