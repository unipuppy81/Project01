using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNPC : MonoBehaviour
{
    public GameObject uiGruop;
    public Animator anim;

    Player enterPlayer;

    public void Enter()
    {
        uiGruop.SetActive(true);
    }

    public void Exit()
    {
        uiGruop.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Enter();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Exit();
        }
    }
}
