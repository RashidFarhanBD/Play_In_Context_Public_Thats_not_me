using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] private Image _authorImageRef;
    [SerializeField] private TMP_Text _messageTextRef;

    public GameObject AuthorIcon => _authorImageRef.gameObject;

    public void Initialize(Sprite authorIcon, string message)
    {
        if (authorIcon != null)
        {
            _authorImageRef.sprite = authorIcon;
        }

        _messageTextRef.SetText(message);
    }
}
