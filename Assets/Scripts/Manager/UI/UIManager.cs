using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Inventory inven;

    public GameObject inventoryPanel;
    bool activeInventory = false;


    public InvenSlot[] slots;
    public Transform slotHolder;


    [Header("Coin")]

    public TextMeshProUGUI coinText;
    public string coinString;
    [SerializeField] private GameObject coinErrorPanel;


    [Header("Quest")]
    public GameObject questPanel;
    bool activeQuestList;


    [Header("UI")]
    [SerializeField] GameObject explainPanel;
    bool activeExplain;

    [SerializeField] GameObject SetPanel;


    public Slider soundSlider; // ���� �Ҹ� ������ ���� Slider
    public Slider mouseSensitivitySlider; // ���콺 �ӵ� ������ ���� Slider





    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<InvenSlot>();
        coinErrorPanel = GameObject.Find("Canvas").transform.Find("StoreUI").transform.Find("ErrorPanel").gameObject;
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
        // Slider�� �̺�Ʈ ������ �߰�
        soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
        mouseSensitivitySlider.onValueChanged.AddListener(ChangeMouseSensitivity);

        if (questPanel.activeSelf)
        {


            questPanel.SetActive(false);


        }
    }



    private void Update()
    {
        coinToString(PlayerGold.nowGold);
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            activeQuestList = !activeQuestList;
            questPanel.SetActive(activeQuestList);
        }
    }

    private void SlotChange(int val)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;

            if(i < inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i =0; i<inven.items.Count; i++)
        {
            slots[i].item_f =inven.items[i];
            slots[i].UpDateSlotUI();
        }
    }


    void coinToString(int coin)
    {
        coinString = coin.ToString();
        coinText.text = coinString;
    }

    public void coinErrorExit()
    {
        coinErrorPanel.SetActive(false);
    }

    public void coinErrorEnter()
    {
        coinErrorPanel.SetActive(true);
    }

    public void ExplainPanelOnOff()
    {
        activeExplain = !activeExplain;
        explainPanel.SetActive(activeExplain);
    }

    public void EnterSetPanel()
    {
        SetPanel.SetActive(true);
    }
    public void ExitSetPanel()
    {
        SetPanel.SetActive(false);
    }


    private void ChangeSoundVolume(float volume)
    {
        // ���� ���� ����
        AudioListener.volume = volume;
    }

    private void ChangeMouseSensitivity(float sensitivity)
    {
        // ���콺 �ӵ� ����
        // ���⼭�� ���÷� ���콺 ȸ�� �ӵ��� �����մϴ�.
        float rotationSpeed = sensitivity * 100.0f;
        // ���⿡ ���ϴ� ������ �߰��Ͻʽÿ�.
    }
}
