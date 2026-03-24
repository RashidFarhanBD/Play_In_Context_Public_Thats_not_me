using System.Collections.Generic;

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public DialogueNode nextNode;

    public List<DialogueEvent> events;
}
