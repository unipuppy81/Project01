using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{

    [SerializeField] GameObject coin;
    public bool isDie;
    public float damage;

    GameObject playerObj1;
    Player player1;

    void Start()
    {
        damage = 10.0f;
        isDie = false;
        playerObj1 = GameObject.Find("Player01");
        player1 = playerObj1.GetComponent<Player>();
    }

    void Update()
    {
        if (isDie) { DieNow(); }

        /*
        if (player1.isDamage)
        {
            StartCoroutine(Attack());
        }
        */
    }

    void DieNow()
    {
        Vector3 coinPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);


        Instantiate(coin, coinPos, transform.rotation);
     
        Destroy(this.gameObject);       
    }
}
