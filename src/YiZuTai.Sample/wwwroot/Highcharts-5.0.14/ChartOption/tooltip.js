{
    dateTimeLabelFormats: {
        millisecond: '%Y-%m-%d %H:%M:%S',
        second: '%Y-%m-%d %H:%M:%S',
        minute: '%Y-%m-%d %H:%M',
        hour: '%Y-%m-%d %H:%M',
        day: '%Y-%m-%d',
        week: '%m-%d',
        month: '%Y-%m',
        year: '%Y'
    },
    enabled: '$Item.TooltipEnabled' == 'True' ? true : false,
    split: '$Item.TooltipSplit' == 'True' ? true : false,        
    pointFormat: '$Item.TooltipPointFormat'
}