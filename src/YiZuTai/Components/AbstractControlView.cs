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

    private async Task<bool> IsHighChartsUndefined()
    {
        return await ZTContext.JSRuntime.InvokeAsync<bool>("eval", ["typeof Highcharts === 'undefined'"]);
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

            //判断是否加载Highcharts相关资源
            var isHighchartsUndefined = await IsHighChartsUndefined();
            if (isHighchartsUndefined)
            {
                //加载控件的JS文件
                foreach (var script in ControlModel.RefrenceJsFiles)
                    await ZTContext.JSRuntime.InvokeVoidAsync("YiZuTai.LoadScript", script);
                //等待控件的JS文件加载完成
                while (isHighchartsUndefined)
                {
                    await Task.Delay(1000);
                    isHighchartsUndefined = await IsHighChartsUndefined();
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
