using UnityEngine;

public abstract class DialogueEvent : ScriptableObject
{
    public abstract void Execute();
}

public class InitiatePenguinSearchEvent : DialogueEvent
{
    [SerializeField] private string _penguinId;

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}