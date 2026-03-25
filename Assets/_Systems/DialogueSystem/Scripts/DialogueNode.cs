using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Node")]
public class DialogueNode : ScriptableObject
{

    public string PenguinId;
    [TextArea]
    public string Text;

    public List<DialogueChoice> Choices;

    public DialogueNode NextNode;
}
