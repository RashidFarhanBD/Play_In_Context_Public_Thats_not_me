using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private TMP_Text _dialogueTextRef;
    [SerializeField] private TMP_Text _penguinNameRef;
    [SerializeField] private Image _penguinAvatar;
    [SerializeField] private Dialogue _startDialogue;
    [SerializeField] private OptionButton[] _optionButtons;

    private DialogueNode currentNode;

    void Start()
    {
        StartDialogue(_startDialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentNode = dialogue.StartNode;
        DisplayNode();
    }

    void DisplayNode()
    {
        _dialogueTextRef.SetText(currentNode.Text);
        _penguinNameRef.SetText(currentNode.PenguinId);

        if (currentNode.Choices == null || currentNode.Choices.Count == 0)
        {
            DeinitializeAll();

            return;
        }

        for (int i = 0; i < _optionButtons.Length; i++)
        {
            var currentButton = _optionButtons[i];

            if (i < currentNode.Choices.Count)
            {
                var currentChoice = currentNode.Choices[i];
                Debug.Log($"{currentChoice == null}");
                var selectionIndex = i;
                //var context = currentChoice.ToContext();

                var context = new OptionContext()
                {
                    OptionDisplayText = currentChoice.Text,
                    IsLockedByDefault = currentChoice.IsLocked,
                    PenguinDescription = currentChoice.IsLocked ? currentChoice.PenguinData?.VisualDescription : null,
                };

                currentButton.Initialize(context, ActionOnSelection);

                void ActionOnSelection()
                {
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
        Debug.Log($"Selecting {index}");

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
}
