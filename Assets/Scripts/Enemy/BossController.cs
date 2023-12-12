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

    // ���� ���� ���� ������ �ε���
    private int currentPatternIndex = 0;

    // ������ ���� �������� �Ѿ �������� ��� �ð�
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
    public GameObject projectilePrefab; // ������Ÿ�� ������
    public Transform[] firePoint; // �߻� ��ġ

    public float projectileSpeed = 10f; // ������Ÿ�� �ӵ�
    public float cooldown = 1f; // ���� ��ٿ� �ð�
    private float lastAttackTime; // ������ ���� �ð�

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
            // ���� �ð����� ���� �������� ��ȯ
            timeSinceLastPattern += Time.deltaTime;
            if (timeSinceLastPattern >= timeBetweenPatterns)
            {
                // ���� �������� ��ȯ
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

            // �� �߻�ü���� ���������� �ӵ��� �����Ϸ��� �� �κ��� ����� �� �ֽ��ϴ�.
            // projectileRb.velocity = t.forward * projectileSpeed;

            // �߻� ������ �α� ���� ���� �߻縦 ��ٸ��ϴ�.
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



