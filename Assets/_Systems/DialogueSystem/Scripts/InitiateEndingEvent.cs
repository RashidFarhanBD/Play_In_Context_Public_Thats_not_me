using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/EndingEvent")]
public class InitiateEndingEvent : DialogueEvent
{
    [SerializeField] private EndingType _endingType;

    public override void Execute()
    {
        GameEvent.RaiseEndgameTriggeredEvent(_endingType);
    }
}