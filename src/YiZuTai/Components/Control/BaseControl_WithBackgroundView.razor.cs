using Microsoft.AspNetCore.Components;

namespace YiZuTai.Components.Control;

public partial class BaseControl_WithBackgroundView<T_BaseControl_WithBackground>
{
    [Parameter]
    public bool DesignMode { get; set; } = false;
}
