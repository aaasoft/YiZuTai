using System;

namespace YiZuTai.Design;

public partial class BaseControl_WithBackgroundView<T_BaseControl_WithBackground> : IControlView
{
    
    protected override void OnParametersSet()
    {
        Model.View = this;
    }
    
    public void Refresh()
    {
        InvokeAsync(StateHasChanged);
    }
}
