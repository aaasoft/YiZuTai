using System.ComponentModel;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.DataSources;

[Category("基础")]
[DisplayName("本周第几天")]
public class DayOfWeek : AbstractAutoRefreshOnTimerDataSource
{
    public override bool RefreshValue()
    {
        var value = (int)System.DateTime.Now.DayOfWeek;
        Value = value.ToString();
        return true;
    }
}