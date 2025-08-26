using System.ComponentModel;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.QueryDataSources;

[Category("基础")]
[DisplayName("模拟数据")]
public class Simulate : AbstractAutoRefreshOnTimerQueryDataSource
{
    private double _MaxValue = 100;
    [DisplayName("最大值")]
    [Description("模拟数据的最大值")]
    public double MaxValue
    {
        get { return _MaxValue; }
        set
        {
            if (value < MinValue)
                return;
            _MaxValue = value;
            RaisePropertyChanged(nameof(MaxValue));
        }
    }

    private double _MinValue = 0;
    [DisplayName("最小值")]
    [Description("模拟数据的最小值")]
    public double MinValue
    {
        get { return _MinValue; }
        set
        {
            if (value > MaxValue)
                return;
            _MinValue = value;
            RaisePropertyChanged(nameof(MinValue));
        }
    }
    private int _DecimalPlace = 0;
    [DisplayName("小数位数")]
    [Description("小数保留的位数")]
    public int DecimalPlace
    {
        get { return _DecimalPlace; }
        set
        {
            if (value < 0)
                return;
            _DecimalPlace = value;
            RaisePropertyChanged(nameof(DecimalPlace));
        }
    }

    private string _DateTimeFormat = "T";
    [DisplayName("日期时间格式")]
    [Description(@"yyyy	由四位数字表示的年份。
M	    月份（1 到 12）。
MM	    月份（01 到 12）。
d	    一个月中的某一天（1 到 31）。
dd	    一个月中的某一天（01 到 31）。
H	    采用 24 小时制的小时（从 0 到 23）。
HH	    采用 24 小时制的小时（从 00 到 23）。
m	    分钟（0 到 59）。
mm	    分钟（00 到 59）。
s	    秒（0 到 59）。
ss	    秒（00 到 59）。
T       距 1970 年 1 月 1 日之间的毫秒数。")]
    public string DateTimeFormat
    {
        get { return _DateTimeFormat; }
        set
        {
            _DateTimeFormat = value;
            RaisePropertyChanged(DateTimeFormat);
        }
    }

    private int _DataTimeInterval = 1000;
    [DisplayName("数据间隔时间")]
    [Description("单位：毫秒")]
    public int DataTimeInterval
    {
        get { return _DataTimeInterval; }
        set
        {
            _DataTimeInterval = value;
            RaisePropertyChanged(nameof(DataTimeInterval));
        }
    }

    private int _Count = 10;
    [DisplayName("数据数量")]
    [Description("模拟产生的数据数量")]
    public int Count
    {
        get { return _Count; }
        set
        {
            if (value < 1)
                return;
            _Count = value;
            RaisePropertyChanged(nameof(Count));
        }
    }

    private static Random random = new Random();

    public override bool RefreshValue()
    {
        QueryDataSourceResult[] tmpValue = null;
        if (Value == null || Value.Length != Count)
            tmpValue = new QueryDataSourceResult[Count];
        else
            tmpValue = Value;

        var currentTime = (new System.DateTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;

        var d = MaxValue - MinValue;
        for (var i = 0; i < Count; i++)
        {
            var value = MinValue + d * random.NextDouble();
            string x = null;
            if (string.IsNullOrEmpty(DateTimeFormat) || DateTimeFormat == "T")
                x = currentTime.ToString();
            else
            {
                var time = new DateTime(1970, 1, 1);
                time = time.AddMilliseconds(currentTime);
                x = time.ToString(DateTimeFormat);
            }
            tmpValue[i] = new QueryDataSourceResult
            {
                X = x,
                Y = value.ToString("N" + DecimalPlace)
            };
            currentTime += DataTimeInterval;
        }
        Value = tmpValue;
        return true;
    }
}