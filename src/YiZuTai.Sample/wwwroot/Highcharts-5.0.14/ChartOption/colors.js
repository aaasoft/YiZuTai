#if ($Item.ChartColors.Count > 0)
    [
#foreach($item in $Item.ChartColors)
#if ($velocityCount > 1),
#end
'$item.ValueColor'
#end
    ]
#else
    ['#7cb5ec', '#434348', '#90ed7d', '#f7a35c', '#8085e9', '#f15c80', '#e4d354', '#2b908f', '#f45b5b', '#91e8e1']
#end
