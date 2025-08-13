using System;
using System.ComponentModel;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.DataSources;

[Category("基础")]
[DisplayName("今年第几天")]
public class DayOfYear: AbstractAutoRefreshOnTimerDataSource
{
    public override bool RefreshValue()
    {
        var value = (int)System.DateTime.Now.DayOfYear;
        Value = value.ToString();
        return true;
    }
}