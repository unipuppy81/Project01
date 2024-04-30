using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Texture2D clickIndicatorTexture; // 클릭 표시에 사용할 2D 텍스처
    public Texture2D cursorTexture;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(16, 16); // 이 값 조절로 크기 조절

    void Start()
    {
        // 시작 시 커서 이미지 설정
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void Update()
    {
        // 여기에 게임 로직 추가

        // ESC 키를 눌렀을 때 원래 커서로 변경
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

}
