using System.Collections;
using System.Collections.Generic;


public class QuestData
{
    public string questName;
    public int[] npcId;
    public string npcName;

    public QuestData(string name, int[] npc, string npcName)
    {
        this.questName = name;
        this.npcId = npc;
        this.npcName = npcName;
    }


}
