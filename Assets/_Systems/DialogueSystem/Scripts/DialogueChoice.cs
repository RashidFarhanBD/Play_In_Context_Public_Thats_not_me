using System.Collections.Generic;

[System.Serializable]
public class DialogueChoice
{
    public string Text;
    public DialogueNode NextNode;
    public bool IsLocked;
    public PenguinData PenguinData;
    public List<DialogueEvent> Events;
}
