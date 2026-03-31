using System;
using Unity.VisualScripting;

public static class GameEvent
{
    public static event Action<OptionContext> OnLockedOptionClicked;
    public static event Action OnPenguinUnlocked;
    public static event Action OnPenguinRegistered;
    public static event Action<EndingType> OnEndgameTriggered;


    public static void RaiseEndgameTriggeredEvent(EndingType endingType)
    {
        OnEndgameTriggered?.Invoke(endingType);
    }

    public static void RaisePenguinUnlockedEvent()
    {
        OnPenguinUnlocked?.Invoke();
    }

    public static void RaisePenguinRegisteredEvent()
    {
        OnPenguinRegistered?.Invoke();
    }

    public static void RaiseLockedOptionClickedEvent(OptionContext context)
    {
        OnLockedOptionClicked?.Invoke(context);
    }
}
