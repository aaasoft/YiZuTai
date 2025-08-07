using Microsoft.AspNetCore.Components;
using YiZuTai.Design;

namespace YiZuTai.Control;

public abstract class AbstractControlView<TControl> : ComponentBase
    where TControl : BaseControl, new()
{
    [Parameter]
    public bool DesignMode { get; set; } = false;
    [Parameter]
    public Core.ZTFile ZTFile { get; set; }
    [Parameter]
    public TControl ControlModel { get; set; }
}
