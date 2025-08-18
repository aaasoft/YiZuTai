var YiZuTai = YiZuTai || {};

YiZuTai.Designer = {
    AddWindowOnBeforeUnloadEvent: function () {
        window.onbeforeunload = function () {
            return '有更改尚未保存，确认离开？';
        };
    },
    RemoveWindowOnBeforeUnloadEvent: function () {
        window.onbeforeunload = null;
    },
    ChangeDivHeightFunc: function (windowHeight, div, otherDivs) {
        var divTop = div.getBoundingClientRect().top;
        var divHeight = windowHeight - divTop - 10;
        if(otherDivs!=null && otherDivs.length>0){
            for(var i=0;i<otherDivs.length;i++){
                var otherDiv = otherDivs[i];
                divHeight -= otherDiv.clientHeight;
            }
        }
        div.style.height = divHeight + 'px';
    },
    OnResizeFunc: function () {
        var windowHeight = window.innerHeight;
        YiZuTai.Designer.ChangeDivHeightFunc(windowHeight, document.getElementById('divToolbox'));
        YiZuTai.Designer.ChangeDivHeightFunc(windowHeight, document.getElementById('divProperty'), [ document.getElementById('divPropertyDescription') ]);

        var divCanvas = document.getElementById('divCanvas');
        YiZuTai.Designer.ChangeDivHeightFunc(windowHeight, divCanvas);
        var svgCanvas = document.getElementById('svgCanvas');

        var divCanvasHeight = divCanvas.clientHeight;
        var svgCanvasHeight = svgCanvas.clientHeight;

        var svgCanvasTop = 0;
        if (divCanvasHeight > svgCanvasHeight)
            svgCanvasTop = (divCanvasHeight - svgCanvasHeight) / 2;
        var svgCanvasStyleMarginTop = svgCanvasTop + 'px';
        //如果没有改变
        if (svgCanvasStyleMarginTop == svgCanvas.style.marginTop)
            return false;
        return true;
    },
    LoopOnResizeFunc: function () {
        if (!YiZuTai.Designer.OnResizeFunc())
            return;
        setTimeout(YiZuTai.Designer.LoopOnResizeFunc, 100);
    },
    SetPointerCapture : function(e){
        e.srcElement.setPointerCapture(e.pointerId);
    },
    ReleasePointerCapture : function(e){
        e.srcElement.releasePointerCapture(e.pointerId);
    }
};

window.onload = function (evt) {
    document.body.style.overflow = 'hidden';
    YiZuTai.Designer.LoopOnResizeFunc();

    window.addEventListener("keydown", function (e) {
        //让Ctrl + S组合键不触发浏览器的保存网页功能，能够传递到后台
        //可以判断是不是mac，如果是mac,ctrl变为花键
        //event.preventDefault() 方法阻止元素发生默认的行为。
        if (e.keyCode == 83 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) {
            e.preventDefault();
        }
    }, false);
};

window.onresize = function (evt) {
    YiZuTai.Designer.LoopOnResizeFunc();
};
