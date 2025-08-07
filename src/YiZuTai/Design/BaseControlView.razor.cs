using Microsoft.AspNetCore.Components;

namespace YiZuTai.Design;

public partial class BaseControlView<T_BaseControl>
{
    [Parameter]
    public T_BaseControl Model { get; set; }
    [Parameter]
    public bool DesignMode { get; set; } = false;
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    private ElementReference svgElementReference;
    public string Id => svgElementReference.Id;
}
