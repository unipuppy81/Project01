using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{
    [SerializeField] GameObject attack01PosBox;
    [SerializeField] Transform[] attack01Pos;

    [SerializeField] GameObject attack01 = null;

    int spawnAttack = 9;
   
    
    void Start()
    {
        attack01Pos = attack01PosBox.transform.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        
    }

    public void Attack01Skill()
    {
        Transform[] newAttackPos = new Transform[attack01Pos.Length - 1];

        for(int i = 1; i < attack01Pos.Length; i++)
        {
            newAttackPos[i - 1] = attack01Pos[i];
        }


        for (int i = 0; i < newAttackPos.Length; i++)
        {
            GameObject shotBullet = Instantiate(attack01, newAttackPos[i].transform.position, newAttackPos[i].transform.rotation);
        }
    }

    public void Attack02Skill()
    {

    }
}
