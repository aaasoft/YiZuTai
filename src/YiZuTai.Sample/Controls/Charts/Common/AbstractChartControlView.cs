using Microsoft.JSInterop;
using YiZuTai.Components;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Charts.Common;

public abstract class AbstractChartControlView<TControl> : AbstractControlView<TControl>
    where TControl : BaseControl, new()
{
    private async Task<bool> IsHighChartsUndefined()
    {
        return await ZTContext.JSRuntime.InvokeAsync<bool>("eval", ["typeof Highcharts === 'undefined'"]);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            //判断是否加载Highcharts相关资源
            if (ControlModel.RefrenceJsFiles != null)
            {
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
    }
}
