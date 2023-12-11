using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRigid : MonoBehaviour
{
    void Start()
    {
        // Rigidbody�� �����ϴ� ��ũ��Ʈ�� ������ ����ϴ�.
        Player playerScript = GetComponent<Player>();

        // Rigidbody�� ���� ������ �����մϴ�.
        playerScript.ReleaseRigidbodyReference();

        // Rigidbody�� �����մϴ�.
        Destroy(GetComponent<Rigidbody>());
    }

}
