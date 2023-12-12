using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LoadSceneSetPos : MonoBehaviour
{
    [SerializeField] GameObject dungeonEnterPos;
    [SerializeField] GameObject bossEnterPos;
    [SerializeField] GameObject comeBackHomePos;

    [SerializeField] GameObject Boss;
    BossController bossController;

    Player player;
    bool isLoading = false;

    void Start()
    {
        player = GetComponent<Player>();
        bossController = Boss.GetComponent<BossController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetPosBossEnter();
        }
    }
    public void SetPosDungeonEnter()
    {
        StartCoroutine(SetPos(dungeonEnterPos));
    }

    public void SetPosBossEnter()
    {
        StartCoroutine(SetPos(bossEnterPos));
        bossController.bossStart = true;
    }

    public void SetPosTown()
    {
        StartCoroutine(SetPos(comeBackHomePos)); 
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

