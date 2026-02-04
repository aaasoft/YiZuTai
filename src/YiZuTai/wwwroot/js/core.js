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
    openWindow: function (params) {
        var maxmin = params.maxmin ? params.maxmin : true;
        var pages = params.pages;

        var item = {
            maxmin: maxmin,
            tab: []
        };
        if(params.width==undefined)
            params.width = window.innerWidth  / 2;
        if(params.height==undefined)
            params.height= window.innerHeight  / 2;
        
        if(params.width > window.innerWidth)
            params.width = window.innerWidth
        if(params.height > window.innerHeight)
            params.height = window.innerHeight
        
        item.area = [params.width + 'px', params.height + 'px'];
        
        jQuery.each(pages, function (index, value) {
            item.tab.push({
                title: value.title,
                content: '<iframe src="' + value.url + '"></iframe>'
            });
        });
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
    }
};