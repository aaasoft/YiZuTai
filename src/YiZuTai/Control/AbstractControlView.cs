using Microsoft.AspNetCore.Components;
using YiZuTai.Design;

namespace YiZuTai.Control;

public abstract class AbstractControlView<TControl> : ComponentBase
    where TControl : BaseControl, new()
{
    [Parameter]
    public string ZTFileId { get; set; }
    [Parameter]
    public TControl ControlModel { get; set; }
}
