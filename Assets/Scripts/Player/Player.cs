using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Player : PlayerBase
{
    [Header("Player")]

    private Camera camera;
    private NavMeshAgent agent;
    private Skill skill;
    private PlayerCursor cursor;


    private bool isMove;
    private bool isAttackNow;
    private bool canAttack;
    private Vector3 destination;


    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform bulletPos = null;
    [SerializeField] private Transform target = null;

    [SerializeField] private GameObject attack01Bullet = null;


    [SerializeField] private float playerAttackRange = 10.0f;

    protected override void Awake()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        skill = GetComponent<Skill>();
        cursor = GetComponent<PlayerCursor>();
    }


    private void Update()
    {
        InputSkillBtn();

        if (Input.GetKeyDown(KeyCode.S) && !isAttackNow) { MoveStop(); }
        if (!isMove && !isAttackNow) { NormalAttack(); } 
        if (!isAttackNow) {  LookMoveDirection(); } // 움직임
    }


   
    private void InputSkillBtn()
    {

        if (Input.GetKeyDown(KeyCode.Q) && !isMove && !isAttackNow)
        {
            isAttackNow = true;
            skill.isMouseBtn1 = true;
            //skill.Attack01Skill2();
            //SetState(CH_STATE.Attack01);
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isMove && !isAttackNow)
        {
            isAttackNow = true;
            skill.isMouseBtn2 = true;
            //SetState(CH_STATE.Attack02Start);
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isMove && !isAttackNow)
        {
            isAttackNow = true;
            skill.isMouseBtn3 = true;
            //SetState(CH_STATE.Attack03Start);
        }
        else if(Input.GetKeyDown(KeyCode.R) && !isMove && !isAttackNow)
        {
            isAttackNow = true;
            SetState(CH_STATE.JumpStart);
        }
        else if (Input.GetKeyDown(KeyCode.D))  // 상태를 푸는 행위
        {
            SetStateNormal();
        }
        
    }

    private void NormalAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.transform.gameObject.CompareTag("Enemy"))
                {

                    Vector3 characterPos = transform.position;
                    Vector3 enemyPos = hit.transform.position;

                    float attackDirf = (enemyPos - characterPos).magnitude;
                    

                    Debug.Log("AT : " + attackDirf);

                    if (attackDirf > playerAttackRange) 
                    {
                        Debug.Log("OUT");
                        SetDestination(hit.point);           
                    }
                    
                    
                    if (attackDirf <= playerAttackRange)
                    {
                        Debug.Log("IN");
                        transform.LookAt(enemyPos);


                        target = hit.transform.gameObject.transform;
                        GameObject shotBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                        shotBullet.GetComponent<Bullet>().isAlive = true;
                        shotBullet.GetComponent<Bullet>().target = target;

                        /*
                        // 플레이어 공격 파티클
                        Vector3 _p = new Vector3(hit.point.x, 0, hit.point.z);
                        var _clone = Instantiate(AttackEffect[0], _p, AttackEffect[0].transform.rotation) as GameObject;
                        */

                        isAttackNow = true;
                        SetState(CH_STATE.JumpUpAttack);
                    }


                    /*
                    if(attackDirf <= playerAttackRange) 
                    {
                        transform.LookAt(enemyPos);


                        isAttackNow = true;
                        SetState(CH_STATE.JumpUpAttack);
                    }
                    else
                    {
                        while (true)
                        {
                            SetDestination(hit.point);
                            if(attackDirf<= playerAttackRange)
                            {
                                break;
                            }
                        }
                    }
                    */
                }
            }
        }
    }

    public void LookMoveDirection()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.gameObject.CompareTag("Ground")){ SetDestination(hit.point); }   
            }
        }

        if (isMove)
        {
            if (agent.velocity.magnitude <= 0.2f)
            {
                isMove = false;
                
                return;
            }

            var dir = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
            transform.forward = dir;
            transform.position += dir.normalized * Time.deltaTime * 5.0f;
            // Time.deltaTime == 게임의 프레임 속도에 영향 받지 않게 해줌
        }
    }


    private void SetDestination(Vector3 hit)
    {
        agent.SetDestination(hit);
        destination = hit;
        isMove = true;
        SetState(CH_STATE.BattleRunForward);
    }

    private void MoveStop()
    {
        agent.SetDestination(transform.position);
        destination = transform.position;
        isMove = false;
        SetState(CH_STATE.Idle);
    }


    private void SetStateNormal()
    {
        if (skill.isMouseBtn1) { skill.isMouseBtn1 = false; }
        if (skill.isMouseBtn2) { skill.isMouseBtn2 = false; }
        if (skill.isMouseBtn3) { skill.isMouseBtn3 = false; }
        if (skill.isMouseBtn4) { skill.isMouseBtn4 = false; }
        if (isMove) { isMove = false; }
        if (isAttackNow) { isAttackNow = false; }

        SetState(CH_STATE.Idle);
    }

    public bool isIdle() { return (CH_STATE.Idle == ch_State); }
    public bool isWait() { return (CH_STATE.Wait == ch_State); }
    public bool isWalk() { return (CH_STATE.BattleRunForward == ch_State); }
    public bool isAttack01() { return (CH_STATE.Attack01 == ch_State); }
    public bool isAttack02() { return (CH_STATE.Attack02Start == ch_State); }
    public bool isAttack03() { return (CH_STATE.Attack03Start == ch_State); }
    public bool isJumpSkill() { return (CH_STATE.JumpStart == ch_State); }
    public bool isDie() { return CH_STATE.Die == ch_State; }
    public bool isDieRecovery() { return CH_STATE.DieRecovery == ch_State; }


    // 코루틴 //
    // Coroutine Name = Animation Name

    protected override IEnumerator Idle()
    {
        do
        {
            yield return null;
        }
        while (!isNewState);
    }

    protected virtual IEnumerator Wait()
    {
        float WaitTime = 0.0f;
        do
        {
            yield return null;

            WaitTime += Time.deltaTime;

            if (WaitTime >= 2.0f)
            {
                WaitTime = 0.0f;
                SetState(CH_STATE.Idle);
            }
        } while(!isNewState);
    }

    protected virtual IEnumerator BattleRunForward()
    {
        do
        {
            yield return null;

            if (!isMove){ SetState(CH_STATE.Idle); }

        } while(!isNewState);
    }
    protected virtual IEnumerator Attack01()
    {
        do
        {
            yield return null;



            yield return new WaitForSeconds(1.0f);

            if (b_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && 
                b_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isAttackNow = false;
                SetState(CH_STATE.Idle);
            }

        } while (!isNewState);
    }
    protected virtual IEnumerator Attack02Start()
    {
        do
        {





            yield return null;

            if (b_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack02Start") &&
                b_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isAttackNow = false;
                SetState(CH_STATE.Idle);
            }

        } while (!isNewState);
    }
    protected virtual IEnumerator Attack03Start()
    {
        do
        {
            yield return new WaitForSeconds(1.0f);

            b_animator.SetBool("isAttackMT", true);

            yield return new WaitForSeconds(1.0f);

            b_animator.SetBool("isAttackMT", false);


            if (b_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack03Maintain") &&
                b_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isAttackNow = false;
                SetState(CH_STATE.Idle);
            }

        } while (!isNewState);
    }

    protected virtual IEnumerator JumpStart()
    {
        do
        {

            //skill.JumpAttack();

            b_animator.SetBool("isJumpSkill", true);

            yield return null;
            //yield return new WaitForSeconds(2.0f);

            b_animator.SetBool("isJumpSkill", false);

            if (b_animator.GetCurrentAnimatorStateInfo(0).IsName("JumpEnd") &&
                b_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.01f)
            {

                SetState(CH_STATE.Idle);
                isAttackNow = false;
            }

        } while (!isNewState);
    }


    // 평타
    protected virtual IEnumerator JumpUpAttack()
    {
        do
        {

            //skill.Attack01Skill();

            yield return null;

            if (b_animator.GetCurrentAnimatorStateInfo(0).IsName("JumpUpAttack") &&
                b_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isAttackNow = false;
                SetState(CH_STATE.Idle);
            }

        } while (!isNewState);
    }
}
