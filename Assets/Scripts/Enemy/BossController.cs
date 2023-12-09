using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossStart;

    public AttackPattern[] attackPatterns;

    // ���� ���� ���� ������ �ε���
    private int currentPatternIndex = 0;

    // ������ ���� �������� �Ѿ �������� ��� �ð�
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
            // ���� �ð����� ���� �������� ��ȯ
            timeSinceLastPattern += Time.deltaTime;
            if (timeSinceLastPattern >= timeBetweenPatterns)
            {
                // ���� �������� ��ȯ
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
        // ���� ������ ��Ȱ��ȭ
        if (attackPatterns.Length > 0)
        {
            attackPatterns[currentPatternIndex].StopPattern(anim);
            // ���� ���� �ε����� �̵�
            currentPatternIndex = (currentPatternIndex + 1) % attackPatterns.Length;

            // ���ο� ������ ����
            attackPatterns[currentPatternIndex].StartPattern(anim);
            BossAttackLogic(currentPatternIndex);

            // ��� �ð� �ʱ�ȭ
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



