using Microsoft.AspNetCore.Components;
using YiZuTai.Core;
using YiZuTai.Design;

namespace YiZuTai.Control;

public abstract class AbstractControlView<TControl> : ComponentBase, IControlView
    where TControl : BaseControl, new()
{
    [Parameter]
    public ZTContext ZTContext { get; set; }
    [Parameter]
    public TControl ControlModel { get; set; }

    protected override void OnParametersSet()
    {
        ControlModel.View = this;
    }

    public void Refresh()
    {
        InvokeAsync(StateHasChanged);
    }

    public virtual void OnDoubleClick() { }
}
