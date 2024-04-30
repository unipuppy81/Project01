using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, string> nameData;

    public Sprite[] portraitArr;

    private void Awake()
    {
       talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        nameData = new Dictionary<int, string>();
        GenerateData();
    }

    void GenerateData()
    {
        // Talk Data
        // NPC : 1000, ShopNPC : 2000, DungeonNPC : 3000
        // ShopPotal : 100, DungeonPotal : 200, 


        talkData.Add(1000, new string[]
        { 
            "�ȳ�",                                    
            "...",
            "�λ� ���ݾ�.."
        });

        talkData.Add(2000, new string[] 
        {
            "�̰��� �����Դϴ�", 
            "�����̶��.." 
        });

        talkData.Add(3000, new string[]
        {
            "���� �Ա��Դϴ�",
            "�����̶��.."
        });

        talkData.Add(100, new string[]
        {
            "�����Դϴ� ��"
        });

        talkData.Add(200, new string[] { 
            "������ �����մϴ�"
        });

        // Quest Talk
        talkData.Add(10 + 1000, new string[]
        {
            "� ��",
            "���� �ڿ� �ִ� ������ ����",
            "���̾��ٰ�? ... ��.. �� �ٰ� .."
        });

        talkData.Add(11 + 2000, new string[]
        {
            "�� �׷� ���� �����̶��.",
            "ü�� �����̶� ���� ������ ���",
            "��� ��İ�? �� �˰Ե��־� �Ӹ�"
        });


        talkData.Add(20 + 2000, new string[]
        {
            "�� �׷� ������ �ٽ� ���ư���",
            "��� ���İ�?",
            "R : ü�� ����, T : ���� ����",
            "���� �� ��"
        });

        talkData.Add(20 + 1000, new string[]
        {
            "�Դ�?",
            "������ ü���̶� ������ 10 �÷��ִ� �� ��Ե���",
            "����ѹ� ������?",
            "���� �Ա��� ���� ���� �ɾ��",
            "�׸��� �������� ���̷����� �־�",
            "���̷��� �����̿� ���� �� �Ѿƿðž�",
            "�����ϵ��� ��"
        });

        talkData.Add(30 + 3000, new string[]
        {
            "��ø�, ��ų ������ ���ٰ�",
            "Q ��ų�� �ϴÿ��� ��ź�� �������� ��ų�̾�",
            "W ��ų�� �ٶ󺸴� �������� ���� �߻��ϴ� ��ų�̰�",
            "E ��ų�� ������ ���� ���� ������ ��ų�̾�",
            "���� ������ Ŭ���� �� �� �����Ŷ� �Ͼ�"
        });

        talkData.Add(40 + 3000, new string[]
        {
            "����߾�",
            "������ �� �����µ� ����Ѱ�",
            "���� �Խ��� �տ� �ִ� ģ������ ����"
        });

        // Object
        talkData.Add(30 + 300, new string[]
        {
            "�غ�ƾ�?",
            "Ŭ�����ϱ� ���� �� ���ƿ�",
            "�׷� ������"
        });

        talkData.Add(40 + 500, new string[]
        {
            
        });

        //Portrait Data
        portraitData.Add(1000, portraitArr[0]);
        portraitData.Add(2000, portraitArr[1]);
        portraitData.Add(3000, portraitArr[1]);


        //Name Data
        nameData.Add(1000, "���� ����");
        nameData.Add(2000, "���� ����");
        nameData.Add(3000, "���� ����");

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

    public string GetObjName(int id, string name)
    {
        return name;
    }
}
