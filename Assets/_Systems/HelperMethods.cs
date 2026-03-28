public static class HelperMethods
{
    public static OptionContext ToContext(this DialogueChoice choice)
    {
        return new OptionContext()
        {
            DisplayText = choice.Text,
            IsLockedByDefault = choice.IsLocked,
            PenguinData = choice.IsLocked ? choice.PenguinData : null
        };
    }

    public static PenguinContext ToContext(this PenguinData data)
    {
        return new PenguinContext()
        {
            DisplayText = data.DisplayName,
            PenguinData = data
        };
    }
}
