using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Enemy
{
    [SerializeField] EnemyType type;

    public float curHP;
    public float maxHP;
    EnemyBase eb;

    public GameObject hudDamageText;
    public Transform hudPos;

    void Start()
    {
        eb = GetComponent<EnemyBase>();
    }
    void Awake()
    {
        if (type == EnemyType.A)
        {
            maxHP = 10.0f;
            curHP = maxHP;
        }
        else if (type == EnemyType.B)
        {
            maxHP = 100.0f;
            curHP = maxHP;
        }


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
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponent<DamageText>().damage = damage; // 데미지 전달
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
