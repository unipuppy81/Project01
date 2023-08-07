using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // ���콺 Ŀ���� ����� �̹���
    public Vector2 cursorHotspot = Vector2.zero; // �̹����� �߽� ����
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
        mousePosition.z = Camera.main.nearClipPlane; // ���콺 ��ġ�� Z���� ī�޶��� nearClipPlane���� �����մϴ�.
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition); // UI Canvas�� ���콺 ������ ��ġ�� �̵��մϴ�.
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
        // GUI ���̾ ����Ͽ� ���콺 Ŀ�� �̹����� ǥ���մϴ�.
        //GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorHotspot.x, Screen.height - Input.mousePosition.y - cursorHotspot.y, cursorTexture.width, cursorTexture.height), cursorTexture);
    }
}
