namespace YiZuTai.Control;

public class ControlShotcut<TControl> : IControlShotcut
    where TControl : BaseControl, new()
{
    public Type ControlType => typeof(TControl);
    public string Name { get; set; }
    public Action<TControl> ExecuteAction { get; set; }

    public virtual void Execute(TControl control)
    {
        ExecuteAction?.Invoke(control);
    }

    void IControlShotcut.Execute(object control)
    {
        Execute((TControl)control);
    }

    public ControlShotcut() { }
    public ControlShotcut(string name, Action<TControl> executeAction)
    {
        Name = name;
        ExecuteAction = executeAction;
    }
}