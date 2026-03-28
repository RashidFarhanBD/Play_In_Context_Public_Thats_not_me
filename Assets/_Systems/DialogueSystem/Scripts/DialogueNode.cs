using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Node")]
public class DialogueNode : ScriptableObject
{

    public PenguinData PenguinData;
    [TextArea]
    public string Text;

    public List<DialogueChoice> Choices;

    public DialogueNode NextNode;
}
