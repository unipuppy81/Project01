using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Enemy
{
    public float curHP;
    public float maxHP = 10.0f;

    Material mat;

    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
    }
    void Awake()
    {
        curHP = maxHP;
    }

    public void GetDamage(float damage)
    {
        curHP -= damage;

        if (IsDeath()) {
            curHP = 0.0f;
            isDie = true;
        }

        StartCoroutine(OnDamage());
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
        mat.color = Color.red;
        yield return new WaitForSeconds(0.25f);

        mat.color = Color.white;
    }
}
