using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapCamera : MonoBehaviour
{
    // 설정에 따라 변경할 수 있는 미니맵 카메라 속성
    public Transform target; // 플레이어 또는 타겟 Transform
    public float height = 40f; // 카메라 높이


    private void Start()
    {

    }

    private void LateUpdate()
    {
        // 미니맵 카메라를 타겟 주변으로 이동
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
            //transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
        }
    }
}
