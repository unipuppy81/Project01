using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadSceneSetPos loadSceneSetPos = new LoadSceneSetPos();
            loadSceneSetPos = other.gameObject.GetComponent<LoadSceneSetPos>();
            loadSceneSetPos.SetPosFunction();
        }
    }
}
