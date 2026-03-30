using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] private TMP_Text _authorTextRef;
    [SerializeField] private TMP_Text _messageTextRef;

    public GameObject AuthorText => _authorTextRef.gameObject;

    public void Initialize(string author, string message)
    {
        _authorTextRef.SetText(author);
        _messageTextRef.SetText(message);
    }
}
