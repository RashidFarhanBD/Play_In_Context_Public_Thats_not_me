using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Node")]
public class DialogueNode : ScriptableObject
{

    public string speaker;
    [TextArea]
    public string text;

    public List<DialogueChoice> choices;

    public DialogueNode nextNode;
}
