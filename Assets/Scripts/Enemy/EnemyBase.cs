using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyBase : MonoBehaviour
{
    public float detectionRadius = 5.0f;

    public bool isNavMesh = false;
    public bool isDragging;

    public Vector3 DragPosition;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDead()) return;

        PlayerDetection();

        if (isDragging)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, DragPosition, Time.deltaTime * 1.0f));
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }

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
                    isNavMesh = true;

                    // ������ �÷��̾���� ��ȣ�ۿ� �ڵ带 �ۼ��մϴ�.
                    Debug.Log("�÷��̾� ������!");
                }
            }   
        }






    }

    public void GetDamage()
    {




    }

    public bool isDead() { return false; }
}
