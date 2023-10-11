using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Enemy
{
    public float curHP;
    public float maxHP = 10.0f;
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
}
