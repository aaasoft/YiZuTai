var YiZuTai = YiZuTai || {};

YiZuTai.Designer = {
    AddWindowOnBeforeUnloadEvent: function () {
        window.onbeforeunload = function () {
            return '有更改尚未保存，确认离开？';
        };
    },
    RemoveWindowOnBeforeUnloadEvent: function () {
        window.onbeforeunload = null;
    }
};

window.onload = function (evt) {
    document.body.style.overflow = 'hidden';
    window.addEventListener("keydown", function (e) {
        //让Ctrl + S组合键不触发浏览器的保存网页功能，能够传递到后台
        //可以判断是不是mac，如果是mac,ctrl变为花键
        //event.preventDefault() 方法阻止元素发生默认的行为。
        if (e.keyCode == 83 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) {
            e.preventDefault();
        }
    }, false);
};