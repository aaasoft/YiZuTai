var YiZuTai = {
    SetTitle: function (title) {
        document.title = title;
    },
    BeginAnimate:function(controlName){
        jQuery('#svg_' + controlName + ' animate').each(function (index, value) { { value.beginElement(); } });
    },
    LoadScript: function (url) {
        let script = document.createElement('script');
        script.src = url;
        document.head.appendChild(script);
    },
    CreateWindow : function (params) {
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