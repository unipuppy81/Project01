using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Enemy
{
    public float curHP;
    public float maxHP = 10.0f;

    Animator anim2;
    void Start()
    {
        anim2 = GetComponentInChildren<Animator>();
    }
    void Awake()
    {
        curHP = maxHP;
    }

    public void GetDamage(float damage)
    {
        if(curHP > 0) { 
        curHP -= damage;

        if (IsDeath()) {
            curHP = 0.0f;
            isDie = true;
        }

        StartCoroutine(OnDamage());
        }
        else
        {
            curHP = 0.0f;
        }
    }

    public void RecoveryHP(float hp)
    {
        curHP += hp;

        if (IsFullHealth())
            curHP = maxHP;
    }

    public bool IsDeath() { return (curHP <= 0); }
    public bool IsFullHealth() { return (curHP >= maxHP); }

    public float RemainingAmount() { return Mathf.Clamp01(curHP / maxHP); }



    IEnumerator OnDamage()
    {
        anim2.SetBool("isDamage", true);

        yield return new WaitForSeconds(0.25f);

        anim2.SetBool("isDamage", false);
    }
}
