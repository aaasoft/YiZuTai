using Microsoft.AspNetCore.Components;

namespace YiZuTai.Design;

public partial class BaseControlView<T_BaseControl> : IControlView
{
    [Parameter]
    public T_BaseControl Model { get; set; }
    [Parameter]
    public bool DesignMode { get; set; } = false;
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    private ElementReference svgElementReference;
    public string Id => svgElementReference.Id;

    protected override void OnParametersSet()
    {
        Model.View = this;
    }

    public void Refresh()
    {
        InvokeAsync(StateHasChanged);
    }
}
