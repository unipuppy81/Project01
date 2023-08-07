using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // 마우스 커서로 사용할 이미지
    public Vector2 cursorHotspot = Vector2.zero; // 이미지의 중심 지점
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skill01Cursor()
    {
        //Cursor.visible = false;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // 마우스 위치의 Z값을 카메라의 nearClipPlane으로 설정합니다.
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition); // UI Canvas를 마우스 포인터 위치로 이동합니다.
    }

    public void Skill02Cursor()
    {
        Cursor.visible = false;
    }

    public void Skill03Cursor()
    {
        Cursor.visible = false;
    }

    public void JumpSkillCursor()
    {
        Cursor.visible = false;
    }

    void OnGUI()
    {
        // GUI 레이어를 사용하여 마우스 커서 이미지를 표시합니다.
        //GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorHotspot.x, Screen.height - Input.mousePosition.y - cursorHotspot.y, cursorTexture.width, cursorTexture.height), cursorTexture);
    }
}
