var YiZuTai = {
    SetTitle: function (title) {
        document.title = title;
    },
    Window : function (params) {
        var maxmin = params.maxmin ? params.maxmin : false; 
        var width = params.width ? params.width : 950;
        var height = params.height ? params.height : 580;
        var pages = params.pages;
        var title = params.title ? params.title : '';

        var item = {
            maxmin: maxmin,
            area: [width + 'px', height + 'px'],
            tab: []
        };
        jQuery.each(pages, function (index, value) {
            item.tab.push({
                title: value.title,
                content: '<iframe style="width: ' + width + 'px; height: ' + (height - 84) + 'px; border: 0;" src="' + value.url + '"></iframe>'
            });
        });
        var span = jQuery('<span style="font-weight: bold;cursor: default;">' + title + '</span>');
        this.show = function () {
            if(pages.length==1){
                var page=pages[0];
                layer.open({
                    type: 2,
                    maxmin: maxmin,
                    area: item.area,
                    title: page.title,
                    content: page.url
                });
            }else{
                layer.tab(item);
                jQuery('.layui-layer-title', window.document).prepend(span);
                if (pages.length == 1)
                    jQuery('.layui-this').css('z-index', '-1');
                }
        };
        this.setWindowTitle = function (title) {
            span.html(title);
        };

        return this;
    }
};

/**
 * 开始动画
 * @param {any} parameter {
        value, //结束的值
        frameCount, //总共要显示多少帧
        getValueCallback, //获取值回调
        setValueCallback  //设置值回调
    }
 */
function beginAnimation(parameter) {
    if (parameter.frameCount == null)
        parameter.frameCount = 30;

    //起始值
    var startValue = parameter.getValueCallback();
    //结束值
    var endValue = parameter.value;

    //如果值相同，则不处理
    if (startValue == endValue)
        return;

    //总共显示多少帧
    var frameCount = parameter.frameCount;

    //每一帧变化量
    var thetaDelta = (endValue - startValue) / frameCount;

    //动画帧编号
    var requestAnimationFrameID;
    parameter.doAnim = function () {
        var currentValue = parameter.getValueCallback();
        currentValue += thetaDelta;
        if (
            (thetaDelta > 0 && currentValue >= endValue)
            || (thetaDelta < 0 && currentValue <= endValue)
        ) {
            cancelAnimationFrame(requestAnimationFrameID);
            parameter.setValueCallback(endValue);
            parameter.doAnim = null;
            return;
        }
        parameter.setValueCallback(currentValue);
        requestAnimationFrameID = requestAnimationFrame(parameter.doAnim);
    };
    //开始执行动画
    requestAnimationFrameID = requestAnimationFrame(parameter.doAnim);
    //返回动画编号
    return requestAnimationFrameID;
}

/**
 * 开始无限循环动画
 * @param {any} parameter {
    getBalanceCallback,//每一帧变化量
    getInitValueCallback, //获取初始值回调
    getValueCallback, //获取值回调
    setValueCallback  //设置值回调
}
 */
function beginAnimationForever(parameter) {
    parameter.doAnim = function () {
        var balance = parameter.getBalanceCallback();
        if (balance == null)
            return;
        if (balance == 0) {
            var initValue = parameter.getInitValueCallback();
            parameter.setValueCallback(initValue);
            //等待1秒，再检查，减少浏览器负担
            setTimeout(requestAnimationFrame, 1000, parameter.doAnim);
        } else {
            var currentValue = parameter.getValueCallback();
            currentValue += balance;
            parameter.setValueCallback(currentValue);
            requestAnimationFrame(parameter.doAnim);
        }
    }
    //开始执行动画
    requestAnimationFrame(parameter.doAnim);
}

/**
 * 结束动画
 * @param {any} requestAnimationFrameID
 */
function endAnimation(requestAnimationFrameID) {
    cancelAnimationFrame(requestAnimationFrameID);
}

/**
 * 开始无限循环值动画{
    valueArray,//每一帧变化量
    getIntervalCallback,//获取间隔变化时间
    setValueCallback  //设置值回调
}
 * @param {any} parameter
 */
function beginValueArrayAnimationForever(parameter) {
    var index = 0;
    var doAnimFunc = function () {
        var currentInterval = parameter.getIntervalCallback();
        if (currentInterval == null)
            return;
        if (currentInterval > 0) {
            parameter.setValueCallback(parameter.valueArray[index]);
            index++;
            if (index >= parameter.valueArray.length)
                index = 0;
            setTimeout(doAnimFunc, currentInterval);
        } else {
            parameter.setValueCallback(parameter.valueArray[0]);
            setTimeout(doAnimFunc, 1000);
        }
    }
    doAnimFunc();
}