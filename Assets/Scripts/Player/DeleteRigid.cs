using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRigid : MonoBehaviour
{
    void Start()
    {
        // Rigidbody를 참조하는 스크립트의 참조를 얻습니다.
        Player playerScript = GetComponent<Player>();

        // Rigidbody에 대한 참조를 해제합니다.
        playerScript.ReleaseRigidbodyReference();

        // Rigidbody를 삭제합니다.
        Destroy(GetComponent<Rigidbody>());
    }

}
