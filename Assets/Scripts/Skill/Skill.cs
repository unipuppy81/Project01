using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    Player player;
    PlayerCursor playerCursor;

    private float rotateSpeed = 1.0f;

    private RectTransform transform_cursor;


    public bool isMouseBtn1 = false;
    public bool isMouseBtn2 = false;
    public bool isMouseBtn3 = false;
    public bool isMouseBtn4 = false;

    [Header("Skill01")]

    [SerializeField] GameObject attack01PosBox;
    [SerializeField] GameObject attack01 = null;
    [SerializeField] Transform[] attack01Pos;



    int spawnAttack = 9;

    [Header("Skill02")]

    [SerializeField] GameObject attack02Effect = null;
    [SerializeField] GameObject fireBase = null;



    [Header("Skill03")]

    [SerializeField] GameObject dragObject = null;
    public bool isClick03 = false;

    [Header("SkillJump")]
    [SerializeField] private GameObject JumpEffect;
    float timer = 0.0f;





    void Start()
    {
        attack01Pos = attack01PosBox.transform.GetComponentsInChildren<Transform>();
        player = GetComponent<Player>();
        playerCursor =  GetComponent<PlayerCursor>();
    }

    void Update()
    {
        if (isMouseBtn1) { Attack01Skill(); }
        if (isMouseBtn2) { Attack02Skill(); }
        if (isMouseBtn3) { Attack03Skill(); }
        if (isMouseBtn4) { JumpAttack(); }
    }

    public void Attack01Skill()
    {
            playerCursor.cursor01Changed = true;

            if (Input.GetMouseButtonDown(0)) 
            {
                // 마우스
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;

                if(Physics.Raycast(ray, out rayHit))
                {
                    Vector3 nextVec = new Vector3(rayHit.point.x, attack01PosBox.transform.position.y, rayHit.point.z);
               

                    attack01PosBox.transform.position = nextVec;

                    // 캐릭터 회전
                    Vector3 targetPosition = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                    Vector3 lookAtDirection = targetPosition - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(lookAtDirection);

                    Rigidbody rb2 = player.GetComponent<Rigidbody>();

                    rb2.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 1000.0f * Time.deltaTime));
                }

                // 상태
                player.SetState(CH_STATE.Attack01);


                // 실행
                Transform[] newAttackPos = new Transform[attack01Pos.Length - 1];

                for (int i = 1; i < attack01Pos.Length; i++)
                {
                    newAttackPos[i - 1] = attack01Pos[i];
                }


                for (int i = 0; i < newAttackPos.Length; i++)
                {
                    GameObject shotBullet = Instantiate(attack01, newAttackPos[i].transform.position, newAttackPos[i].transform.rotation);
                }


                // 종료
                isMouseBtn1 = false;
                playerCursor.cursor01Changed = false;
            }
    }



    public void Attack02Skill()
    {
        playerCursor.cursor02Changed = true;
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            Vector3 nextVec = new Vector3(0, 0, 0);
            Quaternion rotation = Quaternion.identity;
            if (Physics.Raycast(ray, out rayHit))
            {
                nextVec = rayHit.point;


                Vector3 targetPosition = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                Vector3 lookAtDirection = targetPosition - transform.position;
                rotation = Quaternion.LookRotation(lookAtDirection);

                // 캐릭터의 회전을 Rigidbody를 이용하여 처리합니다.
                Rigidbody rb2 = player.GetComponent<Rigidbody>();

                rb2.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 100.0f * Time.deltaTime));
            }

            // 상태
            player.SetState(CH_STATE.Attack02Start);

            // 실행
            //GameObject Effect2_1 = Instantiate(attack02Effect, new Vector3(nextVec.x-1, nextVec.y + 1, nextVec.z), this.transform.rotation);
            //GameObject Effect2_2 = Instantiate(attack02Effect, new Vector3(nextVec.x, nextVec.y + 1, nextVec.z), this.transform.rotation);
            //GameObject Effect2_3 = Instantiate(attack02Effect, new Vector3(nextVec.x+1, nextVec.y + 1, nextVec.z), this.transform.rotation);



            Vector3 attackPos1 = new Vector3(this.transform.position.x-1, this.transform.position.y + 1, this.transform.position.z);
            Vector3 attackPos2 = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            Vector3 attackPos3 = new Vector3(this.transform.position.x+1, this.transform.position.y + 1, this.transform.position.z);

            GameObject fireBase1 = Instantiate(fireBase, this.transform.position, rotation);

            GameObject Effect2_1 = Instantiate(attack02Effect, attackPos1, rotation);
            GameObject Effect2_2 = Instantiate(attack02Effect, attackPos2, rotation);
            GameObject Effect2_3 = Instantiate(attack02Effect, attackPos3, rotation);

            Destroy(fireBase1, 3.0f);

            Destroy(Effect2_1, 3.0f);
            Destroy(Effect2_2, 3.0f);
            Destroy(Effect2_3, 3.0f);

            // 종료
            isMouseBtn2 = false;
            playerCursor.cursor02Changed = false;
        }
        
    }

    public void Attack03Skill()
    {
        playerCursor.cursor01Changed = true;
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            Vector3 nextVec = new Vector3(0, 0, 0);
            Quaternion rotation = Quaternion.identity;
            if (Physics.Raycast(ray, out rayHit))
            {
                nextVec = rayHit.point;

                Vector3 targetPosition = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                Vector3 lookAtDirection = targetPosition - transform.position;
                rotation = Quaternion.LookRotation(lookAtDirection);

                // 캐릭터의 회전을 Rigidbody를 이용하여 처리합니다.
                Rigidbody rb2 = player.GetComponent<Rigidbody>();

                rb2.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 1000.0f * Time.deltaTime));
            }

            // 상태
            player.SetState(CH_STATE.Attack03Start);

            // 실행
            Instantiate(dragObject, nextVec, Quaternion.identity);

            // 종료
            isMouseBtn3 = false;
            playerCursor.cursor01Changed = false;
        }

    }

    public void JumpAttack()
    {
        Debug.Log("ATTA");
        Instantiate(JumpEffect, transform.position, transform.rotation);


        timer += Time.deltaTime;
        if(timer > 2.0f)
        {
            timer = 0.0f;
            JumpEffect.SetActive(false);
        }
         
    }













    /////////////////////////////////////////////// Ex ///////////////////////////////////////////////////

    public void Attack01Skill2() 
    {
        Transform[] newAttackPos = new Transform[attack01Pos.Length - 1];

        for (int i = 1; i < attack01Pos.Length; i++)
        {
            newAttackPos[i - 1] = attack01Pos[i];
        }


        for (int i = 0; i < newAttackPos.Length; i++)
        {
            GameObject shotBullet = Instantiate(attack01, newAttackPos[i].transform.position, newAttackPos[i].transform.rotation);
        }
    }


}
