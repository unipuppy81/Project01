using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LoadSceneSetPos : MonoBehaviour
{
    [SerializeField] GameObject stage1Pos;

    Player player;
    bool isLoading = false;

    void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SetPos());
        }
    }


    IEnumerator SetPos()
    {
        player.agent.enabled = false;
        this.transform.position = stage1Pos.transform.position;
        
        yield return new WaitForSeconds(2.0f);
        
        player.agent.enabled = true;
        
        yield return null;

    }
}

