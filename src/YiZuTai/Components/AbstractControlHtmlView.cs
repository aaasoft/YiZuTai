using System;
using Microsoft.AspNetCore.Components;
using YiZuTai.Control;
using YiZuTai.Core;

namespace YiZuTai.Components;

public class AbstractControlHtmlView<TControl> : ComponentBase, IControlView
    where TControl : BaseControl, new()
{
    [Parameter]
    public ZTContext ZTContext { get; set; }
    [Parameter]
    public TControl ControlModel { get; set; }

    public void Refresh()
    {
        InvokeAsync(StateHasChanged);
    }

    public virtual void OnDoubleClick() { }
}
