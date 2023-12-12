using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    public AudioClip dungeonAudio;
    public AudioClip bossAudio;


    public void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "DungeonZone")
        {
            if (other.CompareTag("Player"))
            {
                LoadSceneSetPos loadSceneSetPos = new LoadSceneSetPos();
                loadSceneSetPos = other.gameObject.GetComponent<LoadSceneSetPos>();
                loadSceneSetPos.SetPosDungeonEnter();

                SoundManager.Instance.PlaySound(dungeonAudio);
            }
        }
        else if (this.gameObject.name == "BossRoomTP")
        {
            if (other.CompareTag("Player"))
            {
                LoadSceneSetPos loadSceneSetPos = new LoadSceneSetPos();
                loadSceneSetPos = other.gameObject.GetComponent<LoadSceneSetPos>();
                loadSceneSetPos.SetPosBossEnter();

                SoundManager.Instance.PlaySound(bossAudio);
            }
        }
        else if(this.gameObject.name == "GoTown")
        {
            LoadSceneSetPos loadSceneSetPos = new LoadSceneSetPos();
            loadSceneSetPos = other.gameObject.GetComponent<LoadSceneSetPos>();
            loadSceneSetPos.SetPosTown();
        }
    }
}
