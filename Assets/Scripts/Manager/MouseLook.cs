using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Texture2D clickIndicatorTexture; // Ŭ�� ǥ�ÿ� ����� 2D �ؽ�ó
    public Texture2D cursorTexture;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(16, 16); // �� �� ������ ũ�� ����

    void Start()
    {
        // ���� �� Ŀ�� �̹��� ����
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void Update()
    {
        // ���⿡ ���� ���� �߰�

        // ESC Ű�� ������ �� ���� Ŀ���� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

}
