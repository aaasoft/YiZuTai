using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using YiZuTai.Control;
using YiZuTai.Model;

namespace YiZuTai.Components;

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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            //初始化数据源
            var dict = ControlModel.GetDataSourceDict();
            if (dict != null)
            {
                foreach (var item in dict)
                {
                    var key = item.Key;
                    var dataSource = item.Value?.Invoke();
                    if (dataSource == null)
                        continue;
                    ControlModel.OnDataSourceValueChanged(dataSource, key);
                }
            }
        }
    }

    public void Refresh()
    {
        InvokeAsync(StateHasChanged);
    }

    public virtual void OnDoubleClick() { }
}
