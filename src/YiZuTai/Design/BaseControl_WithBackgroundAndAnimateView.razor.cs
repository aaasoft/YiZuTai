using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace YiZuTai.Design;

public partial class BaseControl_WithBackgroundAndAnimateView<T_BaseControl_WithBackgroundAndAnimate>
{
    [Parameter]
    public T_BaseControl_WithBackgroundAndAnimate Model { get; set; }
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    [Inject]
    public IJSRuntime JSRuntime { get; set; }
    private ElementReference dataElementReference;
    private BaseControl_WithBackgroundView<T_BaseControl_WithBackgroundAndAnimate> ParentView;
    public string Id => ParentView.Id;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            var svgElement = $"document.querySelector('[_bl_{ParentView.Id}]')";
            var dataElement = $"document.querySelector('[_bl_{dataElementReference.Id}]')";

            var jsCode = @$"
//开始旋转动画
beginAnimationForever({{
svgElement: {svgElement},
gElement: {svgElement}.parentElement,
dataElement: {dataElement},
getBalanceCallback: function () {{
    if(this.gElement.parentElement==null)
        return null;
    return parseFloat(this.dataElement.getAttribute('data-RotateSpeed'));
}},
getInitValueCallback: function () {{ return parseFloat(this.dataElement.getAttribute('data-Angle')); }},
getValueCallback: function () {{ return this.gElement.transform.baseVal.getItem(0).angle; }},
setValueCallback: function (value) {{
    value = value % 360;
    var cx = parseFloat(this.dataElement.getAttribute('data-CenterPointLocationX'));
    var cy = parseFloat(this.dataElement.getAttribute('data-CenterPointLocationY'));
    this.gElement.transform.baseVal.getItem(0).setRotate(value,cx,cy);
}}
}});

//开始闪烁动画
beginValueArrayAnimationForever({{
svgElement: {svgElement},
gElement: {svgElement}.parentElement,
dataElement: {dataElement},
valueArray: [1, 0],
getIntervalCallback: function () {{
    if(this.gElement.parentElement==null)
        return null;
    return parseFloat(this.dataElement.getAttribute('data-FlashInterval'));
}},
setValueCallback: function (value) {{
    this.gElement.style.opacity = value;
}}
}});
";
            JSRuntime.InvokeVoidAsync("eval", jsCode);
        }
    }
}
