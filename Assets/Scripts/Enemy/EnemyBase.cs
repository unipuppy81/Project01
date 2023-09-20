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
                if (collider.CompareTag("Player")) // 플레이어 태그가 적절한 경우
                {
                    isNavMesh = true;

                    // 감지된 플레이어와의 상호작용 코드를 작성합니다.
                    Debug.Log("플레이어 감지됨!");
                }
            }   
        }






    }

    public void GetDamage()
    {




    }

    public bool isDead() { return false; }
}
