using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] public float missileSpeed = 30f; // 미사일 속도

    [SerializeField] private Vector3 targetPosition; // 플레이어의 이전 위치

    private void Start()
    {
        
    }

    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // 플레이어의 이전 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, missileSpeed * Time.deltaTime);
        //transform.LookAt(targetPosition, Vector3.up);

        Vector3 directionToTarget = targetPosition - transform.position;
        transform.eulerAngles = directionToTarget.normalized;


        // 만약 거의 도착하면 미사일을 제거
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    // 플레이어의 새로운 위치를 받아올 메서드
    public void SetPlayerPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>();

            
            Destroy(gameObject);
        }
    }
}
