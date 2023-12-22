using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    private void Awake()
    {
       talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // Talk Data
        // NPC : 1000, ShopNPC : 2000
        // Box : 100, Desk : 200


        talkData.Add(1000, new string[]
        { 
            "너 뭐임?",                                    
            "안녕"
        });

        talkData.Add(2000, new string[] 
        {
            "상점입니다.", 
            "상점이라고" 
        });



        // Quest Talk
        talkData.Add(10 + 1000, new string[]
        {
            "어서 와.",
            "저기 뒤에 있는 상점에 가봐",
            "돈이없다고? ... 어.. 돈 줄게 .."
        });

        talkData.Add(11 + 2000, new string[]
        {
            "어 그래 여긴 상점이라고.",
            "체력 포션 5개, 마나 포션 5개 사봐 한 번",
            "어디서 사냐고? 다 알게되있어 임마"
        });


        talkData.Add(20 + 2000, new string[]
        {
            "어 그래 샀으면 앞에 가봐",
            "어떻게 쓰냐고?",
            "R : 체력 포션, T : 마나 포션",
            "이제 좀 가"
        });

        talkData.Add(20 + 1000, new string[]
        {
            "왔니",
            "그래 둘다 먹으면 체력이랑 마나를 10 올려주니 잘 써먹도록",
            "사냥한번 가볼까?",
            "저기 포탈이 생길거야 갔다와"
        });



        //Portrait Data
        portraitData.Add(1000, portraitArr[0]);
        portraitData.Add(2000, portraitArr[1]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        // Exception
        if (!talkData.ContainsKey(id))
        {
            // 해당 퀘스트 진행 순서 대사가 없을 때.
            // 퀘스트 맨 처음 대사를 가지고 온다.
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
                
            }
            else
            {
                // 퀘스트 맨 처음 대사마저 없을 때 ( 물건 )
                // 기본 대사를 가지고 온다.
                return GetTalk(id - id % 10, talkIndex);
            }
        }


        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

}
