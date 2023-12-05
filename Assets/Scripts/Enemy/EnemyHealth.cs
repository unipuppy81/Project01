using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Enemy
{
    public float curHP;
    public float maxHP = 10.0f;
    EnemyBase eb;

    public GameObject hudDamageText;
    public Transform hudPos;

    void Start()
    {
        eb = GetComponent<EnemyBase>();
        //anim2 = GetComponentInChildren<Animator>();
    }
    void Awake()
    {
        curHP = maxHP;
    }

    public void GetDamage(float damage)
    {
        if(curHP > 0) 
        { 
            curHP -= damage;
            DamageText(damage);
            if (IsDeath()) 
            {
                curHP = 0.0f;
                isDie = true;
            }

            StartCoroutine(eb.OnDamage());
        }
        else
        {
            curHP = 0.0f;
        }
    }

    void DamageText(float damage)
    {
        GameObject hudText = Instantiate(hudDamageText); // ������ �ؽ�Ʈ ������Ʈ
        hudText.transform.position = hudPos.position; // ǥ�õ� ��ġ
        hudText.GetComponent<DamageText>().damage = damage; // ������ ����
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
