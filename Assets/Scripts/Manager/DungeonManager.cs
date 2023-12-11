using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "DungeonZone")
        {
            if (other.CompareTag("Player"))
            {
                LoadSceneSetPos loadSceneSetPos = new LoadSceneSetPos();
                loadSceneSetPos = other.gameObject.GetComponent<LoadSceneSetPos>();
                loadSceneSetPos.SetPosDungeonEnter();
            }
        }
        else if(this.gameObject.name == "BossRoomTP")
        {
            if (other.CompareTag("Player"))
            {
                LoadSceneSetPos loadSceneSetPos = new LoadSceneSetPos();
                loadSceneSetPos = other.gameObject.GetComponent<LoadSceneSetPos>();
                loadSceneSetPos.SetPosBossEnter();
            }
        }
    }
}
