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
            // ��ǥ �������� ȸ��
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // ����ź�� ��ǥ �������� �̵�
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");
        Instantiate(Effect, transform.position, transform.rotation);

        Destroy(this.gameObject);

    }
}
