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
        const maxmin = params.maxmin ? params.maxmin : true;
        const pages = params.pages;

        let windowWidth = '75vw';
        let windowHeight = '75vh';

        if (params.width != undefined) {
            if (params.width > window.innerWidth)
                windowWidth = window.innerWidth + 'px';
            else if (params.width > 1)
                windowWidth = params.width + 'px';
            else if (params.width > 0)
                windowWidth = (params.width * 100) + 'vw';
        }
        if (params.height != undefined) {
            if (params.height > window.innerHeight)
                windowHeight = window.innerHeight + 'px';
            else if (params.height > 1)
                windowHeight = params.height + 'px';
            else if (params.height > 0)
                windowHeight = (params.height * 100) + 'vh';
        }

        var item = {
            maxmin: maxmin,
            area: [windowWidth, windowHeight],
            tab: []
        };
        
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