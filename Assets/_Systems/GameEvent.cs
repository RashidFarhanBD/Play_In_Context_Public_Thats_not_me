using System;

public static class GameEvent
{
    public static event Action<OptionContext> OnLockedOptionClicked;
    public static event Action OnPenguinUnlocked;
    public static event Action OnPenguinRegistered;

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
