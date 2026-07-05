using YiZuTai.Model;

namespace YiZuTai.Control;

public class ControlShotcut<TControl> : IControlShotcut
    where TControl : BaseControl, new()
{
    public Type ControlType => typeof(TControl);
    public string Name { get; set; }
    public Action<ZTContext, TControl> ExecuteAction { get; set; }

    public virtual void Execute(ZTContext context, TControl control)
    {
        ExecuteAction?.Invoke(context, control);
    }

    void IControlShotcut.Execute(ZTContext context, object control)
    {
        Execute(context, (TControl)control);
    }

    public ControlShotcut() { }
    public ControlShotcut(string name, Action<ZTContext, TControl> executeAction)
    {
        Name = name;
        ExecuteAction = executeAction;
    }
}