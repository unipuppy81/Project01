using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaPosSet : MonoBehaviour
{
    GameObject obj;
    private void Awake()
    {
        obj = GameObject.Find("Player01");
    }
    void Start()
    {
        Vector3 characterosition = new Vector3(0, 0, 0);

        obj.transform.position = characterosition;
    }


}
