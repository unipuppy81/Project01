using System.Collections;
using System.Collections.Generic;


public class QuestData
{
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc)
    {
        this.questName = name;
        this.npcId = npc;
    }


}
