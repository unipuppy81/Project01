using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : Skill
{
    public AttackType attackType;

    [Header("NormalAttack")]
    [SerializeField] private GameObject NormalEffect;
    public Transform target = null;
    public float speed = 5.0f;
    public float rotationSpeed = 5f;

    public bool isAlive = false;

    public float damage;
    void Start()
    {
        damage = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GoTarget();
        if(target == null) { Destroy(this.gameObject); }
    }

    void GoTarget()
    {
        if (isAlive && target != null)
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
        float Damage;

        if (attackType == AttackType.NormalAttack && other.tag == "Enemy")
        { 

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.GetDamage(4.0f);
            Instantiate(NormalEffect, transform.position, transform.rotation);




            Destroy(this.gameObject);
        }
    }
}
