using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack01 : MonoBehaviour
{
    public AttackType attackType;

    [SerializeField] private GameObject Effect;



    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        float damage;

        damage = 8.0f;
        if (other.gameObject.tag == "Enemy") 
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.GetDamage(damage);
        }
        Instantiate(Effect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
