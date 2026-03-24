using System.Collections.Generic;
using UnityEngine;

public struct DialogueNodeContext
{
    public string Speaker;
    public string Text;
    public List<DialogueChoice> DialogueChoices;
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Dialogue _startDialogue;
    [SerializeField] private OptionButton[] _optionButtons;

    private DialogueNode currentNode;

    void Start()
    {
        StartDialogue(_startDialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentNode = dialogue.startNode;
        DisplayNode();
    }

    void DisplayNode()
    {
        Debug.Log($"{currentNode.speaker}: {currentNode.text}");

        if (currentNode.choices == null) return;

        for (int i = 0; i < _optionButtons.Length; i++)
        {
            Debug.Log($"{i}");
            var currentButton = _optionButtons[i];

            if (i < currentNode.choices.Count)
            {
                var currentChoice = currentNode.choices[i];
                var selectionIndex = i;
                currentButton.Initialize(currentChoice.text, () =>
                {
                    SelectChoice(selectionIndex);
                });
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

        var choice = currentNode.choices[index];

        if (choice.events != null)
        {
            foreach (var e in choice.events)
            {
                e.Execute();
            }
        }

        // Move to next node
        currentNode = choice.nextNode;

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
        if (currentNode.nextNode != null)
        {
            currentNode = currentNode.nextNode;
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
        currentNode = null;
    }
}
