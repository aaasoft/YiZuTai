var YiZuTai = YiZuTai || {};
YiZuTai.View = {
    ChangeDivHeightFunc: function (windowHeight, div) {
        var divTop = div.getBoundingClientRect().top;
        var divHeight = windowHeight - divTop - 0;
        div.style.height = divHeight + 'px';
    },
    OnResizeFunc: function () {
        var windowHeight = window.innerHeight;

        var divCanvas = document.getElementById('divCanvas');
        YiZuTai.View.ChangeDivHeightFunc(windowHeight, divCanvas);

        var svgCanvas = document.getElementById('svgCanvas');

        var divCanvasHeight = divCanvas.clientHeight;
        var svgCanvasHeight = svgCanvas.clientHeight;

        var stretch = divCanvas.getAttribute('data-stretch');
        switch (stretch) {
            case 'Fill':
                document.body.style.overflow = 'hidden';
                break;
            case 'None':
                if (svgCanvasHeight <= windowHeight)
                    document.body.style.overflow = 'hidden';
                break;
        }

        var svgCanvasTop = 0;
        if (divCanvasHeight > svgCanvasHeight)
            svgCanvasTop = (divCanvasHeight - svgCanvasHeight) / 2;

        var svgCanvasStyleMarginTop = svgCanvasTop + 'px';
        //如果没有改变
        if (svgCanvasStyleMarginTop == svgCanvas.style.marginTop)
            return false;
        svgCanvas.style.marginTop = svgCanvasStyleMarginTop;
        return true;
    },
    LoopOnResizeFunc: function () {
        if (!YiZuTai.View.OnResizeFunc())
            return;
        setTimeout(YiZuTai.View.LoopOnResizeFunc, 100);
    }
};

window.onload = function (evt) {
    var stretch = document.getElementById('divCanvas').getAttribute('data-stretch');
    if (stretch == 'UniformToFill')
        return;
    YiZuTai.View.LoopOnResizeFunc();
};

window.onresize = function (evt) {
    var stretch = document.getElementById('divCanvas').getAttribute('data-stretch');
    if (stretch == 'UniformToFill')
        return;
    YiZuTai.View.LoopOnResizeFunc();
};