using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public InputField messageInputField;
    public TextMeshProUGUI chatText;
    public ScrollRect scrollRect;

    public void SendMessage()
    {
        string message = messageInputField.text;

        if (!string.IsNullOrEmpty(message))
        {
            // �޽����� ä��â�� �߰�
            chatText.text += "\n" + message;

            // ��ũ���� �Ʒ��� �̵�
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;

            // �Է� �ʵ� �ʱ�ȭ
            messageInputField.text = "";
        }
    }
}