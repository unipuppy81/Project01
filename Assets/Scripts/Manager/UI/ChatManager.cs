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
            // 메시지를 채팅창에 추가
            chatText.text += "\n" + message;

            // 스크롤을 아래로 이동
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;

            // 입력 필드 초기화
            messageInputField.text = "";
        }
    }
}