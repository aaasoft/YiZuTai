using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using YiZuTai.Design;

namespace YiZuTai.Control.TextBlock
{
    public partial class View
    {
        private BaseControl_WithBackgroundView baseControlView;
        private ElementReference dataElementReference;

        [Parameter]
        public Model Model { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                baseControlView.RegisterDataSource(Model,
                [
                    new (nameof(Model.Value), () => Model.Value )
                ]);

                var svgElement = $"document.querySelector('[_bl_{baseControlView.Id}]')";
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
}
