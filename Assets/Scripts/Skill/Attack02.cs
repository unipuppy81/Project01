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
        // 오브젝트의 정면 방향으로 레이캐스트를 쏩니다.
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            // 레이캐스트가 무언가에 맞았습니다. 이 경우 감지됨.
            Debug.Log("감지되었습니다.");

            // 맞은 오브젝트는 hit.collider를 통해 접근할 수 있습니다.
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
