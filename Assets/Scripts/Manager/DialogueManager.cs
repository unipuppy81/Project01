using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction;
    public void Action(GameObject scanObj)
    {
        if(isAction)  // Exit Action
        {
            isAction = false;
        }
        else // Enter Action
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "�̸��� : " + scanObject.name + "�Դϴ�.";
        }

        talkPanel.SetActive(isAction);
    }
}
