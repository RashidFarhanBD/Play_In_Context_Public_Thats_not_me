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

public struct OptionContext
{
    public string OptionDisplayText;
    public string PenguinDescription;
    public bool IsLockedByDefault;
}
