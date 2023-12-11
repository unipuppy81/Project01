using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] public float missileSpeed = 30f; // �̻��� �ӵ�

    [SerializeField] private Vector3 targetPosition; // �÷��̾��� ���� ��ġ


    [SerializeField] private GameObject enemyBulletEffect;
    [SerializeField] private float Damage;

    private void Start()
    {
        Damage = 15.0f;
    }

    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // �÷��̾��� ���� ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, missileSpeed * Time.deltaTime);
        //transform.LookAt(targetPosition, Vector3.up);

        Vector3 directionToTarget = targetPosition - transform.position;
        transform.eulerAngles = directionToTarget.normalized;


        // ���� ���� �����ϸ� �̻����� ����
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget < 0.1f)
        {
            Instantiate(enemyBulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }

    // �÷��̾��� ���ο� ��ġ�� �޾ƿ� �޼���
    public void SetPlayerPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerh = collision.gameObject.GetComponent<PlayerHealth>();
            playerh.GetDamage(Damage);
            
            Destroy(gameObject);
        }
    }
}
