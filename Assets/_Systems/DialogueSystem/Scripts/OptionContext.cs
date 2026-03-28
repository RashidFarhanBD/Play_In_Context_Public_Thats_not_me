public struct OptionContext : IContextHolder
{
    public string DisplayText { get; set; }
    public PenguinData PenguinData;
    public bool IsLockedByDefault;
}

public struct PenguinContext : IContextHolder
{
    public string DisplayText { get; set; }
    public PenguinData PenguinData;
}
