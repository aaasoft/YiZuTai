{
    title: { text: '$!Item.YAxisTitleText' },
    allowDecimals: '$Item.YAxisAllowDecimals' == 'True' ? true : false,
    plotLines: [{ value: 0, width: 1, color: '#808080' }],
    crosshair: '$Item.YAxisCrosshair' == 'True' ? true : false,
    visible: '$Item.YAxisVisible' == 'True' ? true : false,
    gridLineWidth: '$Item.YAxisGridLineWidth',
    gridLineColor: '$Item.YAxisGridLineColorColor',
    labels: {
        //rotation: 0,
        style: { color: '$Item.YAxisLabelColorColor', fontSize: '${Item.YAxisLabelFontSize}px', fontFamily: '$Item.YAxisLabelFontFamily' }
    }
}