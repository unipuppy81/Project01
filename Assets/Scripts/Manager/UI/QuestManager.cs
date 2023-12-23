using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    public GameObject[] questObject; // portal



    Dictionary<int, QuestData> questList;
    
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("ù ���� �湮", 
                                new int[] { 1000, 2000 },
                                "0"));

        questList.Add(20, new QuestData("���ǻ��",
                                 new int[] { 2000, 1000 },
                                 "0"));

        /*questList.Add(30, new QuestData("����Ʈ Ŭ����",
                         new int[] { 0 }));   */

        questList.Add(30, new QuestData("���� ����",
                                new int[] { 3000 },
                                "0"));


        questList.Add(40, new QuestData("���� Ŭ�����ϱ�",
                               new int[] { 3000, 3000 },
                               "0"));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // Next Talk Target
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        // Control Quest Object
        ControlObject();


        Debug.Log(questList[questId].npcId.Length);
        // Talk Complete & Next Quest
        if(questActionIndex  == questList[questId].npcId.Length)
        {
            NextQuest();
        }

        // Quest Name
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }



    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if(questActionIndex == 2)
                {
                    questObject[0].SetActive(true);
                }
                break;


            case 20:
                if (questActionIndex == 3)
                {
                    //questObject[1].SetActive(true);
                }
                break;
            case 30:
                if (questActionIndex == 1)
                {
                    questObject[1].SetActive(true);
                }
                break;

            case 40:
                if (questActionIndex == 2)
                {
                    Debug.Log("���� Ŭ���ӤӾ�");
                    //questObject[1].SetActive(true);
                }
                break;
        }
    }
}
