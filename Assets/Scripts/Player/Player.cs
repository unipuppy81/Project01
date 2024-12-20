using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : PlayerBase
{
    public static bool isPlayerDead = false;

    [Header("Player")]

    private Camera camera;
    private Skill skill;
    public DialogueManager dialManager;

    public Transform spot;


    public NavMeshAgent agent;
    
    private Vector3 destination;

    [Header("StateBool")]
    public bool canGame = false;
    private bool isMove;
    private bool isAttackNow;
    private bool canAttack;
    public bool isDamage;



    [Header("Skill")]
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform bulletPos = null;
    [SerializeField] private Transform target = null;
    [SerializeField] private GameObject attack01Bullet = null;
    [SerializeField] private float playerAttackRange = 200.0f;




    [Header("Dialogue")]
    [SerializeField] Vector3 dirVec;
    public GameObject scanObject;



    protected override void Awake()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        skill = GetComponent<Skill>();
    }

    void Start()
    {
        Invoke("ClearNav", 2.0f);
        DataManager.Instance.LoadGameData();
    }

    private void Update()
    {
        canGame = agent.enabled;

        if (canGame)
        {
            InputSkillBtn();
            if (Input.GetKeyDown(KeyCode.N)) { SceneManager.LoadScene("Stage1"); }
            if (Input.GetKeyDown(KeyCode.S) && !isAttackNow) { MoveStop(); }
            if (!isMove && !isAttackNow) { NormalAttack(); }
            if (!isAttackNow && !dialManager.isAction) { LookMoveDirection(); } // 움직임
            dirVec = this.gameObject.transform.forward; // Quest


            // scanObj
            if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
            {
                dialManager.Action(scanObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), dirVec, new UnityEngine.Color(0, 1, 0));

        RaycastHit rayHit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), dirVec, out rayHit, 2.0f))
        {

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
                {
                    scanObject = rayHit.collider.gameObject;
                }
                else
                {
                    scanObject = null;
                }
            }
            else
            {
                scanObject = null;
            }
        }
        else
        {
            scanObject = null;
        }

    }

    // navigation
    private void ClearNav()
    {
        agent.enabled = false;
        agent.enabled = true;
    }
   

    // 키 조절
    private void InputSkillBtn()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isMove && !isAttackNow && !skill.isSkill01Cool)
        {
            isAttackNow = true;
            skill.isMouseBtn1 = true; 
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isMove && !isAttackNow && !skill.isSkill02Cool)
        {
            isAttackNow = true;
            skill.isMouseBtn2 = true;

        }
        else if (Input.GetKeyDown(KeyCode.E) && !isMove && !isAttackNow && !skill.isSkill03Cool)
        {
            isAttackNow = true;
            skill.isMouseBtn3 = true;
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            PlayerHealth ph = GetComponent<PlayerHealth>();
            ph.RecoveryHP(20);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerMana pm = GetComponent<PlayerMana>();
            pm.RecoveryMP(20);
        }
        else if (Input.GetKeyDown(KeyCode.D))  // 상태를 푸는 행위
        {
            SetStateNormal();
        }
        
    }

    // 움직임
    public void LookMoveDirection()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {

                if (hit.transform.gameObject.CompareTag("Ground")) 
                { 
                    SetDestination(hit.point);

                    spot.gameObject.SetActive(true);
                    spot.position = new Vector3(hit.point.x, hit.point.y+.5f, hit.point.z);
                }

            }
        }
        else if(agent.remainingDistance < 0.1f)
        {
            spot.gameObject.SetActive(false);
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




    // 공격
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


    // Trigger

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            if (!isDamage) { 
            Bullet enemyBullet = other.GetComponent<Bullet>();
            PlayerHealth playerH = this.GetComponent<PlayerHealth>();

                GameObject enemyObject = other.transform.root.gameObject;
                EnemyBase enemyBase = enemyObject.GetComponent<EnemyBase>();
                
                StartCoroutine(OnDamage(enemyBase));
            playerH.GetDamage(10.0f);
                

            }
        }
    }


    // 상태
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



    // Ondamage

    protected virtual IEnumerator OnDamage(EnemyBase enemyBase)
    {
        isDamage = true;
        enemyBase.isEnemyBullet = true;
        yield return new WaitForSeconds(.5f);
        
        isDamage = false;
        enemyBase.isEnemyBullet = false;
    }
}
