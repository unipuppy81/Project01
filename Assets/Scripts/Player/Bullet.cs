using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum AttackType
    {
        NormalAttack,
        Attack01,
        Attack02,
        Attack03,
        JumpAttack
    }


    [SerializeField] private GameObject Effect;

    public Transform target = null;

    public float speed = 5.0f;
    public float rotationSpeed = 5f;

    public bool isAlive = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GoTarget();
    }


    void GoTarget()
    {
        if (isAlive)
        {
            // 목표 방향으로 회전
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // 유도탄을 목표 방향으로 이동
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        Instantiate(Effect, transform.position, transform.rotation);

        Destroy(this.gameObject);

    }
}
