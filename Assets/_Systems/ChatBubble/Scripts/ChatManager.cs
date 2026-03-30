using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : Singleton<ChatManager>
{
    [SerializeField] private Transform _chatBubbleParent;
    [SerializeField] private TMP_Text _chatTitleRef;
    [SerializeField] private ChatBubble _playerChatBubblePrefab;
    [SerializeField] private ChatBubble _otherChatBubblePrefab;

    void Start()
    {
        ClearChat();
    }

    public void SetChatTitle(string title)
    {
        _chatTitleRef.SetText(title);
    }

    public void SendMessage(string author, string message, bool isPlayer)
    {
        var properPrefab = isPlayer ? _playerChatBubblePrefab : _otherChatBubblePrefab;

        var splitText = message.Split("#");

        for (int i = 0; i < splitText.Length; i++)
        {
            var textPart = splitText[i];

            var chatBubble = Instantiate(properPrefab, _chatBubbleParent);
            chatBubble.Initialize(author, textPart);
            chatBubble.AuthorText.SetActive(i == 0);
        }
    }

    private void ClearChat()
    {
        var childCount = _chatBubbleParent.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            var child = _chatBubbleParent.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
