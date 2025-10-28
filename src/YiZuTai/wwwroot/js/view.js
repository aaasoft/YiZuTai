var YiZuTai = YiZuTai || {};

window.addEventListener('load', function () {
    const svgContainer = document.getElementById('svgContainer');
    const canvasDataStretch = svgContainer.getAttribute('data-stretch');

    const canvasWidth = parseFloat(svgContainer.getAttribute('data-width'));
    const canvasHeight = parseFloat(svgContainer.getAttribute('data-height'));
    if (canvasDataStretch != 'UniformToFill')
        return;

    const onWindowsResizeFunc = function () {
        const windowWidth = window.innerWidth - 15;
        const windowHeight = window.innerHeight - 15;

        const scaleX = windowWidth / canvasWidth;
        const scaleY = windowHeight / canvasHeight;
        const scale = Math.max(scaleX, scaleY);

        let newWidth,
            newHeight;
        if (scale == scaleX) {
            newWidth = windowWidth;
            newHeight = canvasHeight * scale;
        } else {
            newWidth = canvasWidth * scale;
            newHeight = windowHeight;
        }
        document.body.style.width = newWidth + 'px';
        document.body.style.height = newHeight + 'px';

        svgContainer.style['width'] = document.body.style.width;
        svgContainer.style['height'] = document.body.style.height;
    };
    window.addEventListener('resize', onWindowsResizeFunc);
    window.addEventListener('load', onWindowsResizeFunc);
});