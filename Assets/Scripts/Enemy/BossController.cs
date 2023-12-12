using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossStart;
    public bool bossDie;

    
    [SerializeField] GameObject goTown;
    [SerializeField] GameObject bigCoin;
    [SerializeField] AudioClip bossDieAudio;



    public AttackPattern[] attackPatterns;

    // 현재 실행 중인 패턴의 인덱스
    private int currentPatternIndex = 0;

    // 보스가 다음 패턴으로 넘어갈 때까지의 대기 시간
    public float timeBetweenPatterns = 5f;
    private float timeSinceLastPattern;


    Animator anim;
    EnemyHealth eh;

    [SerializeField]    GameObject HPbar;
    [SerializeField] TextMeshProUGUI textHP;
     


    [Header("SpawnTanker")]
    [SerializeField]    Transform[] spawnPoints;
    [SerializeField]    GameObject tankPrefab;


    [Header("AttackKnife")]
    public GameObject projectilePrefab; // 프로젝타일 프리팹
    public Transform[] firePoint; // 발사 위치

    public float projectileSpeed = 10f; // 프로젝타일 속도
    public float cooldown = 1f; // 공격 쿨다운 시간
    private float lastAttackTime; // 마지막 공격 시간

    [SerializeField] GameObject targetObject;

    [Header("RainShower")]
    [SerializeField]    Transform[] rainSpawnPoint;
    [SerializeField]    GameObject attackPrefab;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        eh = GetComponentInChildren<EnemyHealth>();
        targetObject = GameObject.FindWithTag("Player");
        SetPos();
    }

    void Update()
    {
        if (bossStart)
        {
            HPbar.SetActive(true);
            if (textHP != null) { textHP.text = $"{eh.curHP:F0}/{eh.maxHP:F0}"; };
            // 일정 시간마다 다음 패턴으로 전환
            timeSinceLastPattern += Time.deltaTime;
            if (timeSinceLastPattern >= timeBetweenPatterns)
            {
                // 다음 패턴으로 전환
                SwitchToNextPattern();
            }
        }

        if (bossDie)
        {
            Vector3 coinPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);

            HPbar.SetActive(false);
            Instantiate(bigCoin, coinPos, transform.rotation);
            goTown.SetActive(true);

            SoundManager.Instance.PlaySound(bossDieAudio);

            Destroy(this.gameObject);
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

        Transform shootTransform = GameObject.Find("ShootTransform").transform;
        firePoint = new Transform[shootTransform.childCount];
        for (int i = 0; i < shootTransform.childCount; i++)
        {
            firePoint[i] = shootTransform.GetChild(i);
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
        else if (attackPatterns[index].patternName == "AttackKnife")
        {
            StartCoroutine(FireProjectiles());
        }
        else if (attackPatterns[index].patternName == "RainShower")
        {
            for (int i = 0; i < 10; i++)
            {
                int randomIndex = Random.Range(0, rainSpawnPoint.Length);

                Instantiate(attackPrefab, rainSpawnPoint[randomIndex].position, attackPrefab.transform.rotation);
            }
        }
    }

    IEnumerator FireProjectiles()
    {
        foreach (Transform t in firePoint)
        {
            GameObject projectile = Instantiate(projectilePrefab, t.position, Quaternion.identity);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            EnemyShoot es = projectile.GetComponent<EnemyShoot>();

            Transform targetTransform = targetObject.transform;
            es.SetPlayerPosition(targetTransform.position);

            // 각 발사체마다 개별적으로 속도를 적용하려면 이 부분을 사용할 수 있습니다.
            // projectileRb.velocity = t.forward * projectileSpeed;

            // 발사 간격을 두기 위해 다음 발사를 기다립니다.
            yield return new WaitForSeconds(0.5f);
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



