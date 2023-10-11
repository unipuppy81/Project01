using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{

    [SerializeField] GameObject coin;
    public bool isDie;
    // Start is called before the first frame update
    void Awake()
    {
        isDie = false;
    }

    void Update()
    {
        if (isDie) { DieNow(); }
    }


    void DieNow()
    {
        Instantiate(coin, transform.position, transform.rotation);
     
        Destroy(this.gameObject);       
    }
    public void GetDamage()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {

        }   
        else if(other.tag == "Bomb")
        {

        }
    }
}
