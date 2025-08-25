{
    allowPointSelect: '$Item.SeriesAllowPointSelect' == 'True' ? true : false,
    cursor: 'pointer',
    shadow: '$Item.SeriesShadow' == 'True' ? true : false,
#if ($Item.SeriesStep != 'false')
    step: '$Item.SeriesStep',
#end
    borderWidth: $Item.SeriesBorderWidth,
    borderColor: '$Item.SeriesBorderColorColor',
    lineWidth: $Item.SeriesLineWidth,
    dashStyle: '$Item.SeriesDashStyle',
    dataLabels: {
        enabled: '$Item.SeriesDataLabelsEnable' == 'True' ? true : false,
        inside: '$Item.SeriesDataLabelsInside' == 'True' ? true : false,
        color: "$Item.SeriesDataLabelsColorColor",
        backgroundColor: '$Item.SeriesDataLabelsBackgroundColorColor',
        shape:'$Item.SeriesDataLabelsShape',
        borderWidth: 0,
        rotation: $Item.SeriesDataLabelsRotation,
        align: '$Item.SeriesDataLabelsAlign',
        verticalAlign:'$Item.SeriesDataLabelsVerticalAlign',
        format: '$Item.SeriesDataLabelsFormat',
        style: { fontSize: "${Item.SeriesDataLabelsFontSize}px", fontFamily:"$Item.SeriesDataLabelsFontFamily","textOutline": null}
    }
}