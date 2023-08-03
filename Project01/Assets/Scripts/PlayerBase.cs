using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerBase : MonoBehaviour
{
    [Header("PlayerBase")]
    public Rigidbody b_rigid;
    public Animator b_animator;

    public CH_STATE ch_State;
    public bool isNewState;

    public CharacterState State;

    protected virtual void Awake()
    {
        b_rigid = GetComponentInChildren<Rigidbody>();
        b_animator = GetComponentInChildren<Animator>();
        State = GetComponent<CharacterState>();
    }

    protected virtual void OnEnable()
    {
        ch_State = CH_STATE.Idle;
        StartCoroutine(MainC());
    }

    public void SetState(CH_STATE newState)
    {
        isNewState = true;
        ch_State = newState;

        b_animator.SetInteger("State", (int)ch_State);
    }

    protected virtual IEnumerator MainC()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(ch_State.ToString());
        }
    }

    protected virtual IEnumerator Idle()
    {
        do
        {
            yield return null;
        }
        while (!isNewState);
    }
}
