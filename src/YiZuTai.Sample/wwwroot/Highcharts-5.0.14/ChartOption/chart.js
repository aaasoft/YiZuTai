{
    type: '$Item.ChartType',
    width: $Item.Width,
    height: $Item.Height,
    animation: Highcharts.svg,
    shadow: '$Item.ChartShadow' == 'True' ? true : false,
#if ($Item.ChartPlotBackgroundOpacity>0)
    plotBackgroundColor: '$Item.ChartPlotBackgroundColor',
#end
#if ($Item.ChartPlotBorderOpacity>0)
    plotBorderColor: '$Item.ChartPlotBorderColor',
#end
    plotBorderWidth: $Item.ChartPlotBorderWidth,
}