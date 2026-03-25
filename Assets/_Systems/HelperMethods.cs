public static class HelperMethods
{
    public static OptionContext ToContext(this DialogueChoice choice)
    {
        return new OptionContext()
        {
            OptionDisplayText = choice.Text,
            PenguinDescription = choice.PenguinData.VisualDescription,
            IsLockedByDefault = choice.IsLocked
        };
    }
}
