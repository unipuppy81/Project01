using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

enum EnemyType
{
    A,B,C
}


public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    EnemyType eType;

    public float detectionRadius = 5.0f;
    public float enemySpeed = 3.0f;
    public bool isDragging;
    public bool isDetect;

    [Header("Drag")]
    float timer = 0.0f;
    float timer2 = 0.0f;
    float dragDamage = 1.0f;

    public Vector3 DragPosition;
    GameObject playerObj;


    Animator anim;
    EnemyHealth enemyH;
    Rigidbody rb;
    Player player;

    public RuntimeAnimatorController originalAnimator;
    public bool isEnemyBullet;

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
        if (eType == EnemyType.A)
        {
            PlayerDetection();
            if (isDetect && !isDragging && !isEnemyBullet) { chaseTarget(); }

            if (isDragging)
            {
                rb.MovePosition(Vector3.Lerp(transform.position, DragPosition, Time.deltaTime * 1.0f));
                DragDamage();
            }


            if (isEnemyBullet)
            {
                StartCoroutine(Attack());
                SetAnimBool();
            }

        }
        else if (eType == EnemyType.B)
        {

        }
        else if (eType == EnemyType.C)
        {
            isDetect = true;
            if (isDetect && !isDragging && !isEnemyBullet) { chaseTarget(); }

            if (isDragging)
            {
                rb.MovePosition(Vector3.Lerp(transform.position, DragPosition, Time.deltaTime * 1.0f));
                DragDamage();
            }


            if (isEnemyBullet)
            {
                StartCoroutine(Attack());
                SetAnimBool();
            }
        }

    }

    void SetAnimBool()
    {

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
                if (collider.CompareTag("Player")) 
                {
                    isDetect = true;                   
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

    // ================================= ÄÚ·çÆ¾ =========================================== //

    public IEnumerator OnDamage()
    {
        anim.SetBool("isDamage", true);

        yield return new WaitForSeconds(0.25f);

        anim.SetBool("isDamage", false);
    }

    public IEnumerator Attack()
    {
        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(.5f);

        anim.SetBool("isAttack", false);
        //anim.SetBool("isWalk", true);
    }
}
