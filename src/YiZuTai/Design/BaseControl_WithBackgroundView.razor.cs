using System;
using Microsoft.AspNetCore.Components;

namespace YiZuTai.Design;

public partial class BaseControl_WithBackgroundView<T_BaseControl_WithBackground> : IControlView
{
    [Parameter]
    public bool DesignMode { get; set; } = false;
    
    protected override void OnParametersSet()
    {
        Model.View = this;
    }
    
    public void Refresh()
    {
        InvokeAsync(StateHasChanged);
    }
}
