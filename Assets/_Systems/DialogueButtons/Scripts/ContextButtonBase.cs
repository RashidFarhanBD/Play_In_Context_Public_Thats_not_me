public abstract class ContextButtonBase<TContexHolder> : ButtonBase
where TContexHolder : IContextHolder
{
    protected TContexHolder _context;

    public virtual void Initialize(TContexHolder contex)
    {
        _context = contex;
        _displayTextRef.SetText(contex.DisplayText);
    }
}
