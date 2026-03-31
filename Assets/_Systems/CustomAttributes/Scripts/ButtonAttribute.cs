using System;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class ButtonAttribute : Attribute
{
    /// <summary>The label shown on the inspector button. Null means "use method name".</summary>
    public string Label { get; }

    /// <summary>Optional tooltip shown when hovering the button.</summary>
    public string Tooltip { get; }

    public ButtonAttribute(string label = null, string tooltip = null)
    {
        Label = label;
        Tooltip = tooltip;
    }
}