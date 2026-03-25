using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string DialogueID;
    public DialogueNode StartNode;
    public List<DialogueNode> Nodes;
}
