using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;



public class EnemyBase : MonoBehaviour
{
    public float detectionRadius = 5.0f;
    public float enemySpeed = 3.0f;
    public bool isDragging;
    public bool isDetect;

    [Header("Drag")]
    float timer = 0.0f;
    float timer2 = 0.0f;
    float dragDamage = 1.0f;

    public Vector3 DragPosition;
    public GameObject playerObj;


    Animator anim;
    EnemyHealth enemyH;
    Rigidbody rb;
    Player player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerObj = GameObject.Find("Player01");
        enemyH = GetComponent<EnemyHealth>();
        anim = GetComponentInChildren<Animator>();
        player = playerObj.GetComponent<Player>();
    }

    void Update()
    {
        PlayerDetection();
        if (isDetect && !isDragging && !player.isDamage) { chaseTarget(); }

        if (isDragging)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, DragPosition, Time.deltaTime * 1.0f));
            DragDamage();
        }
    }

    void DragDamage()
    {
        float interval = 1.0f;
        timer2 += Time.deltaTime;

        if (timer2 >= interval)
        {
            enemyH.GetDamage(dragDamage);
            timer2 = 0.0f;
        }
    }

    /*
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
    */

    void OnParticleCollision( )
    {
        if(enemyH != null) 
        {
            enemyH.GetDamage(0.03f);
        }
    }

    public void PlayerDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        Transform p_shortestTarget = null;

        if(hitColliders.Length > 0) 
        { 
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Player")) // 플레이어 태그가 적절한 경우
                {
                    isDetect = true;
                    

                    // 감지된 플레이어와의 상호작용 코드를 작성합니다.
                    Debug.Log("플레이어 감지됨!");
                }
            }   
        }
    }

    void chaseTarget()
    {
        Vector3 nVec = new Vector3(playerObj.transform.position.x, transform.position.y, playerObj.transform.position.z);
        transform.LookAt(nVec);
        transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed);

        anim.SetBool("isWalk", true);
    }

    // ================================= 코루틴 =========================================== //




}
