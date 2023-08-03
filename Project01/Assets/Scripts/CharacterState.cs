using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CH_STATE
{
    Idle, //Idle01 
    Wait, // Idle02
    BattleRunForward, // BattleRunForward
    Attack01, // Attack01
    Attack02Start, // Attack02
    Attack03Start, // Attack03
    JumpStart, // Jump start->up->End
    JumpUpAttack,//Normal Attack
    GetHit,
    Interact,
    Die, // Die
    DieRecovery, // DieRecovery
}



public class CharacterState : MonoBehaviour
{
    public string Name;

}
