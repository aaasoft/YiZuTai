{
    type: '$Item.XAxisType',
    crosshair: '$Item.XAxisCrosshair' == 'True' ? true : false,
#if ($showCategories)
    categories: [
#foreach($item in $Item.XAxisCategories)
        #if ($velocityCount > 1)
            ,
        #end
        '$item.Name'
#end
    ],
#end
    visible: '$Item.XAxisVisible' == 'True' ? true : false,
    labels: {
        //rotation: 0,
        style: { color: '$Item.XAxisLabelColorColor', fontSize: '${Item.XAxisLabelFontSize}px',fontFamily: '$Item.XAxisLabelFontFamily'}
    }

}