using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Control.Image
{
    public partial class View
    {
        private ElementReference svgElementReference;
        [Parameter]
        public string ZTFileId { get; set; }
        [Parameter]
        public Model Model { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var svgElement = $"document.querySelector('[_bl_{svgElementReference.Id}]')";
                var jsCode = @$"
//开始旋转动画
beginAnimationForever({{
    svgElement: {svgElement},
    gElement: {svgElement}.parentElement,
    getBalanceCallback: function () {{
        if(this.gElement.parentElement==null)
            return null;
        return parseFloat(this.svgElement.getAttribute('data-RotateSpeed'));
    }},
    getInitValueCallback: function () {{ return parseFloat(this.svgElement.getAttribute('data-Angle')); }},
    getValueCallback: function () {{ return this.gElement.transform.baseVal.getItem(0).angle; }},
    setValueCallback: function (value) {{
        value = value % 360;
        var cx = parseFloat(this.svgElement.getAttribute('data-CenterPointLocationX'));
        var cy = parseFloat(this.svgElement.getAttribute('data-CenterPointLocationY'));
        this.gElement.transform.baseVal.getItem(0).setRotate(value,cx,cy);
    }}
}});

//开始闪烁动画
beginValueArrayAnimationForever({{
    svgElement: {svgElement},
    gElement: {svgElement}.parentElement,
    valueArray: [1, 0],
    getIntervalCallback: function () {{
        if(this.gElement.parentElement==null)
            return null;
        return parseFloat(this.svgElement.getAttribute('data-FlashInterval'));
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
