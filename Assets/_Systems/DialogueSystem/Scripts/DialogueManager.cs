using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private GameObject _body;
    [SerializeField] private float _delayBetweenMessage;
    [SerializeField, TextArea] private string _firstMessage;
    [SerializeField] private Dialogue _startDialogue;
    [SerializeField] private OptionButton[] _optionButtons;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _messageContent;

    private DialogueNode currentNode;

    public GameObject Body => _body;

    protected override void Awake()
    {
        base.Awake();
        ChatManager.Instance.OnMessageSent += FixScrollBar;
    }

    void OnDestroy()
    {
        ChatManager.Instance.OnMessageSent -= FixScrollBar;
    }

    void Start()
    {
        ChatManager.Instance.SendMessage(null, _firstMessage, true);

        StartDialogue(_startDialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentNode = dialogue.StartNode;
        DisplayNode();
    }

    void DisplayNode()
    {
        Debug.LogWarning($"starting {currentNode.Text}");
        DeinitializeAll();
        var penguinData = currentNode.PenguinData;
        var penguinIcon = penguinData.PenguinIcon;
        var author = penguinData.DisplayName;
        var message = currentNode.Text;

        ChatManager.Instance.SetChatTitle(author);
        ChatManager.Instance.SendMessage(penguinIcon, message, false);

        if (currentNode.EndingType == EndingType.Good ||
        currentNode.EndingType == EndingType.Bad)
        {
            GameEvent.RaiseEndgameTriggeredEvent(currentNode.EndingType);

            return;
        }

        if (currentNode.Choices == null || currentNode.Choices.Count == 0)
        {
            Continue();

            return;
        }

        for (int i = 0; i < _optionButtons.Length; i++)
        {
            var currentButton = _optionButtons[i];

            if (i < currentNode.Choices.Count)
            {
                var currentChoice = currentNode.Choices[i];
                var selectionIndex = i;
                var context = currentChoice.ToContext();

                currentButton.Initialize(context);
                currentButton.OnButtonClicked += ActionOnSelection;

                void ActionOnSelection()
                {
                    var responseMessage = context.DisplayText;
                    ChatManager.Instance.SendMessage(null, responseMessage, true);
                    currentButton.OnButtonClicked -= ActionOnSelection;

                    SelectChoice(selectionIndex);
                }
            }
            else
            {
                currentButton.Deinitialize();
            }
        }
    }

    public void SelectChoice(int index)
    {
        var choice = currentNode.Choices[index];

        if (choice.Events != null)
        {
            foreach (var e in choice.Events)
            {
                e.Execute();
            }
        }

        currentNode = choice.NextNode;

        if (currentNode != null)
        {
            DisplayNode();
        }
        else
        {
            EndDialogue();
        }
    }

    public void Continue()
    {
        if (currentNode.NextNode != null)
        {
            currentNode = currentNode.NextNode;
            DisplayNode();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        Debug.Log("Dialogue Ended");

        DeinitializeAll();

        currentNode = null;
    }

    private void DeinitializeAll()
    {
        foreach (var option in _optionButtons)
        {
            option.Deinitialize();
        }
    }

    private void FixScrollBar()
    {
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_messageContent);
        _scrollRect.verticalNormalizedPosition = 0f;
    }
}
