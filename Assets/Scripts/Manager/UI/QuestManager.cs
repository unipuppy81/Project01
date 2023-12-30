using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    public GameObject[] questObject; // portal

    [SerializeField] GameObject questListManagerObject;
    QuestListManager qListManager;


    public Dictionary<int, QuestData> questList;



    [Header("GameQuest")]
    public int killCount;
    public int hpGlobeCount;
    public int mpGlobeCount;

    [SerializeField] Text hpGlobeText;
    [SerializeField] Text mpGlobeText;
    
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void Start()
    {
        qListManager = questListManagerObject.GetComponent<QuestListManager>();
    }

    private void Update()
    {
        if (killCount >= 5){ KillEnemyFive(); }
        if( hpGlobeCount >= 3) { purchaseHpGlobe(); }
        if (mpGlobeCount >= 3) { purchaseMpGlobe(); }
    }

    // 대화 퀘스트 
    void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문", 
                                new int[] { 1000, 2000 },
                                "0"));

        questList.Add(20, new QuestData("포션사기",
                                 new int[] { 2000, 1000 },
                                 "0"));

        /*questList.Add(30, new QuestData("퀘스트 클리어",
                         new int[] { 0 }));   */

        questList.Add(30, new QuestData("던전 들어가기",
                                new int[] { 3000 },
                                "0"));


        questList.Add(40, new QuestData("던전 클리어하기",
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
                if (questActionIndex == 1)
                {
                    QuestBtn qBtn;
                    qBtn = qListManager.townChildObjects[0].GetComponent<QuestBtn>();
                    qBtn.ClearSet();

                    Debug.Log("이장 냥이에게 말 걸기 클리어");
                }
                else if (questActionIndex == 2)
                {
            
                    questObject[0].SetActive(true);
                }
                break;


            case 20:
                if(questActionIndex == 1) 
                {
                    QuestBtn qBtn;
                    qBtn = qListManager.shopChildObjects[0].GetComponent<QuestBtn>();
                    qBtn.ClearSet();
                    Debug.Log("포션 구매하기 클리어");
                }
                else if (questActionIndex == 2)
                {

                }
                break;

            case 30:
                if (questActionIndex == 1)
                {
                    QuestBtn qBtn;
                    qBtn = qListManager.dungeonChildObjects[0].GetComponent<QuestBtn>();
                    qBtn.ClearSet();
                    questObject[1].SetActive(true);
                }
                break;

            case 40:
                if (questActionIndex == 2)
                {
                    QuestBtn qBtn;
                    qBtn = qListManager.dungeonChildObjects[1].GetComponent<QuestBtn>();
                    qBtn.ClearSet();
                    Debug.Log("던전 클리ㅣㅣ어");
                    
                }
                break;
        }
    }

    // 게임 퀘스트





    // 마을

    public void EnterDungeon()
    {
        QuestBtn qBtn;
        qBtn = qListManager.townChildObjects[1].GetComponent<QuestBtn>();
        qBtn.ClearSet();
        Debug.Log("던전 들어가기 클리어");
    }

    // 던전
    public void KillEnemyFive()
    {
        QuestBtn qBtn;
        qBtn = qListManager.dungeonChildObjects[0].GetComponent<QuestBtn>();
        qBtn.ClearSet();
        Debug.Log("5킬 클리어");
    }
    public void ClearDungeon()
    {
        QuestBtn qBtn;
        qBtn = qListManager.dungeonChildObjects[1].GetComponent<QuestBtn>();
        qBtn.ClearSet();
        Debug.Log("던전 클리어를 클리어");
    }

    public void HpGlobeCount()
    {
        hpGlobeCount++;
        hpGlobeText.text = "체력 포션 3개 구매하기 ( " + hpGlobeCount + " / 3 )";
    }
    public void MpGlobeCount()
    {
        mpGlobeCount++;
        mpGlobeText.text = "체력 포션 3개 구매하기 ( " + mpGlobeCount + " / 3 )";
    }

    void purchaseHpGlobe()
    {
        QuestBtn qBtn;
        qBtn = qListManager.shopChildObjects[0].GetComponent<QuestBtn>();
        qBtn.ClearSet();
    }

    void purchaseMpGlobe()
    {
        QuestBtn qBtn;
        qBtn = qListManager.shopChildObjects[1].GetComponent<QuestBtn>();
        qBtn.ClearSet();
    }

}
