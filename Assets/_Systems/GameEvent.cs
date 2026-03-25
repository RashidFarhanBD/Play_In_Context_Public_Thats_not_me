using System;

public static class GameEvent
{
    public static event Action<OptionContext> OnLockedOptionClicked;

    public static void RaiseLockedOptionClickedEvent(OptionContext context)
    {
        OnLockedOptionClicked?.Invoke(context);
    }
}
