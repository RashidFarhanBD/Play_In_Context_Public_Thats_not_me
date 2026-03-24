using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string dialogueID;
    public DialogueNode startNode;
    public List<DialogueNode> nodes;
}
