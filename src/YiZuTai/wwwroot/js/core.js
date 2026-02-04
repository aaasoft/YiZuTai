var YiZuTai = {
    SetTitle: function (title) {
        document.title = title;
    },
    BeginAnimate: function (controlName) {
        jQuery('#svg_' + controlName + ' animate').each(function (index, value) { { value.beginElement(); } });
    },
    LoadScript: function (url) {
        let script = document.createElement('script');
        script.src = url;
        document.head.appendChild(script);
    },
    CreateWindow: function (params) {
        var maxmin = params.maxmin ? params.maxmin : true;
        var pages = params.pages;

        var item = {
            maxmin: maxmin,
            tab: []
        };
        if (params.width != undefined && params.height != undefined)
            item.area = [params.width + 'px', params.height + 'px'];
        jQuery.each(pages, function (index, value) {
            item.tab.push({
                title: value.title,
                content: '<iframe src="' + value.url + '"></iframe>'
            });
        });
        this.show = function () {
            if (pages.length == 1) {
                var page = pages[0];
                layer.open({
                    type: 2,
                    maxmin: maxmin,
                    area: item.area,
                    title: page.title,
                    content: page.url
                });
            } else {
                layer.tab(item);
            }
        };
        return this;
    }
};