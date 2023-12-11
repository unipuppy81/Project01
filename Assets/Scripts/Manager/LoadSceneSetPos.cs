using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LoadSceneSetPos : MonoBehaviour
{
    [SerializeField] GameObject stage1Pos;
    [SerializeField] GameObject stage2Pos;

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
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dungeon"))
        {
            StartCoroutine(SetPos(stage2Pos));
        }
        else if (other.CompareTag("BossDungeon"))
        {
            StartCoroutine(SetPos(stage1Pos));
        }
    }

    IEnumerator SetPos(GameObject obj)
    {
        player.agent.enabled = false;
        this.transform.position = obj.transform.position;
        
        yield return new WaitForSeconds(2.0f);
        
        player.agent.enabled = true;
        
        yield return null;
    }
}

