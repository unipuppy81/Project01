using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class EnemyBase : MonoBehaviour
{
    public float detectionRadius = 5.0f;
    public float enemySpeed = 3.0f;
    public bool isDragging;
    public bool isDetect;


    public Vector3 DragPosition;
    public GameObject playerObj;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerObj = GameObject.Find("Player01");
    }

    void Update()
    {
        if (isDead()) return;

        PlayerDetection();
        if (isDetect && !isDragging) { chaseTarget(); }

        if (isDragging)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, DragPosition, Time.deltaTime * 1.0f));
        }
    }

    /*
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
    */

    public void PlayerDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        Transform p_shortestTarget = null;

        if(hitColliders.Length > 0) 
        { 
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Player")) // �÷��̾� �±װ� ������ ���
                {
                    isDetect = true;
                    

                    // ������ �÷��̾���� ��ȣ�ۿ� �ڵ带 �ۼ��մϴ�.
                    Debug.Log("�÷��̾� ������!");
                }
            }   
        }
    }

    void chaseTarget()
    {
        Vector3 nVec = new Vector3(playerObj.transform.position.x, transform.position.y, playerObj.transform.position.z);
        transform.LookAt(nVec);
        transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed);
    }
    public bool isDead() { return false; }
}
