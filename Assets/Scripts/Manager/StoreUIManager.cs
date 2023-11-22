using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUIManager : MonoBehaviour
{
    public GameObject HpPotionPanel;
    public GameObject MpPotionPanel;

    public void HPExplainBtn()
    {
        HpPotionPanel.SetActive(true);
        MpPotionPanel.SetActive(false);
    }

    public void MPExplainBtn()
    {
        HpPotionPanel.SetActive(false);
        MpPotionPanel.SetActive(true);
    }
}
