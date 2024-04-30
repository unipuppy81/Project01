using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapCamera : MonoBehaviour
{
    // ������ ���� ������ �� �ִ� �̴ϸ� ī�޶� �Ӽ�
    public Transform target; // �÷��̾� �Ǵ� Ÿ�� Transform
    public float height = 40f; // ī�޶� ����


    private void Start()
    {

    }

    private void LateUpdate()
    {
        // �̴ϸ� ī�޶� Ÿ�� �ֺ����� �̵�
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
            //transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
        }
    }
}
