using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack02 : MonoBehaviour
{
    public AttackType attackType;

    [SerializeField] private GameObject Effect;
    public float detectionDistance = 100.0f;
    private void Start()
    {
        
    }


    void Update()
    {
        // ������Ʈ�� ���� �������� ����ĳ��Ʈ�� ���ϴ�.
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            // ����ĳ��Ʈ�� ���𰡿� �¾ҽ��ϴ�. �� ��� ������.
            Debug.Log("�����Ǿ����ϴ�.");

            // ���� ������Ʈ�� hit.collider�� ���� ������ �� �ֽ��ϴ�.
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        float damage;

        damage = 1.0f;
        if (other.gameObject.tag == "Enemy")
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.GetDamage(damage);

            Debug.Log("At02");
        }
        Instantiate(Effect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
