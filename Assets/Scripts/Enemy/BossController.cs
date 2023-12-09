using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossStart;

    public AttackPattern[] attackPatterns;

    // 현재 실행 중인 패턴의 인덱스
    private int currentPatternIndex = 0;

    // 보스가 다음 패턴으로 넘어갈 때까지의 대기 시간
    public float timeBetweenPatterns = 5f;
    private float timeSinceLastPattern;


    Animator anim;

    [Header("SpawnTanker")]
    [SerializeField]
    Transform[] spawnPoints;

    [SerializeField]
    GameObject tankPrefab;


    [Header("RainShower")]
    [SerializeField]
    Transform[] rainSpawnPoint;

    [SerializeField]
    GameObject attackPrefab;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SetPos();
    }

    void Update()
    {
        if (bossStart)
        {
            // 일정 시간마다 다음 패턴으로 전환
            timeSinceLastPattern += Time.deltaTime;
            if (timeSinceLastPattern >= timeBetweenPatterns)
            {
                // 다음 패턴으로 전환
                SwitchToNextPattern();
            }
        }
    }

    void SetPos()
    {
        Transform spawnGroup = GameObject.Find("EnemySpawnGroup").transform;
        spawnPoints = new Transform[spawnGroup.childCount];
        for (int i = 0; i < spawnGroup.childCount; i++)
        {
            spawnPoints[i] = spawnGroup.GetChild(i);
        }

        Transform rainSpawnGrop = GameObject.Find("AttackPosGroup").transform;
        rainSpawnPoint = new Transform[rainSpawnGrop.childCount];
        for(int i = 0; i < rainSpawnGrop.childCount; i++)
        {
            rainSpawnPoint[i] = rainSpawnGrop.GetChild(i);
        }

    }
    void SwitchToNextPattern()
    {
        // 현재 패턴을 비활성화
        if (attackPatterns.Length > 0)
        {
            attackPatterns[currentPatternIndex].StopPattern(anim);
            // 다음 패턴 인덱스로 이동
            currentPatternIndex = (currentPatternIndex + 1) % attackPatterns.Length;

            // 새로운 패턴을 시작
            attackPatterns[currentPatternIndex].StartPattern(anim);
            BossAttackLogic(currentPatternIndex);

            // 대기 시간 초기화
            timeSinceLastPattern = 0f;
        }

    }          

    public void BossAttackLogic(int index)
    {
        if (attackPatterns[index].patternName == "SpawnTanker")
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(tankPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
        else if (attackPatterns[index].patternName == "RainShower")
        {
            for (int i = 0; i < 5; i++)
            {
                int randomIndex = Random.Range(0, rainSpawnPoint.Length);

                Instantiate(attackPrefab, rainSpawnPoint[randomIndex].position, attackPrefab.transform.rotation);
            }
        }
    }
}


[System.Serializable]
public class AttackPattern
{
    public string patternName;

    public void StartPattern(Animator bossAnimator)
    {
        if (patternName == "SpawnTanker")
        {
            bossAnimator.SetTrigger("doAttack1");
            bossAnimator.SetBool("Pattern1F", true);
        }
        else if (patternName == "AttackKnife")
        {
            bossAnimator.SetTrigger("doAttack2");
            bossAnimator.SetBool("Pattern2F", true);
        }
        else if(patternName == "RainShower")
        {
            bossAnimator.SetTrigger("doAttack3");
            bossAnimator.SetBool("Pattern3F", true);
        }
    }

    public void StopPattern(Animator bossAnimator)
    {
        if (patternName == "SpawnTanker")
        { 
            bossAnimator.SetBool("Pattern1F", false);
        }
        else if (patternName == "AttackKnife")
        {

            bossAnimator.SetBool("Pattern2F", false);
        }
        else if (patternName == "RainShower")
        {

            bossAnimator.SetBool("Pattern3F", false);
        }
    }
}



