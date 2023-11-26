using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{

    [SerializeField] GameObject coin;
    public bool isDie;
    public float damage;
    // Start is called before the first frame update
    void Awake()
    {
        damage = 10.0f;
        isDie = false;
    }

    void Update()
    {
        if (isDie) { DieNow(); }
    }

    void DieNow()
    {
        Vector3 coinPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);


        Instantiate(coin, coinPos, transform.rotation);
     
        Destroy(this.gameObject);       
    }
}
