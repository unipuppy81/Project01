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
            "�� ����?",                                    
            "�ȳ�"
        });

        talkData.Add(2000, new string[] 
        {
            "�����Դϴ�.", 
            "�����̶��" 
        });



        // Quest Talk
        talkData.Add(10 + 1000, new string[]
        {
            "� ��.",
            "���� �ڿ� �ִ� ������ ����",
            "���̾��ٰ�? ... ��.. �� �ٰ� .."
        });

        talkData.Add(11 + 2000, new string[]
        {
            "�� �׷� ���� �����̶��.",
            "ü�� ���� 5��, ���� ���� 5�� ��� �� ��",
            "��� ��İ�? �� �˰Ե��־� �Ӹ�"
        });


        talkData.Add(20 + 2000, new string[]
        {
            "�� �׷� ������ �տ� ����",
            "��� ���İ�?",
            "R : ü�� ����, T : ���� ����",
            "���� �� ��"
        });

        talkData.Add(20 + 1000, new string[]
        {
            "�Դ�",
            "�׷� �Ѵ� ������ ü���̶� ������ 10 �÷��ִ� �� ��Ե���",
            "����ѹ� ������?",
            "���� ��Ż�� ����ž� ���ٿ�"
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
            // �ش� ����Ʈ ���� ���� ��簡 ���� ��.
            // ����Ʈ �� ó�� ��縦 ������ �´�.
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
                
            }
            else
            {
                // ����Ʈ �� ó�� ��縶�� ���� �� ( ���� )
                // �⺻ ��縦 ������ �´�.
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
