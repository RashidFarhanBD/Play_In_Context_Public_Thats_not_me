using System;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ChatManager : Singleton<ChatManager>
{
    [SerializeField] private Transform _chatBubbleParent;
    [SerializeField] private TMP_Text _chatTitleRef;
    [SerializeField] private ChatBubble _playerChatBubblePrefab;
    [SerializeField] private ChatBubble _otherChatBubblePrefab;

    public event Action OnMessageSent;

    void Start()
    {
        ClearChat();
    }

    public void SetChatTitle(string title)
    {
        _chatTitleRef.SetText(title);
    }

    public void SendMessage(Sprite authorIcon, string message, bool isPlayer)
    {
        var properPrefab = isPlayer ? _playerChatBubblePrefab : _otherChatBubblePrefab;

        if (LocalizationManager.Instance.TryGetText(message, out var text))
        {
            var splitText = text.Split("%");

            //Turn to coroutine
            for (int i = 0; i < splitText.Length; i++)
            {
                var textPart = splitText[i];

                var chatBubble = Instantiate(properPrefab, _chatBubbleParent);
                chatBubble.Initialize(authorIcon, textPart);
                chatBubble.AuthorIcon.SetActive(i == 0);

                OnMessageSent?.Invoke();
            }
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
