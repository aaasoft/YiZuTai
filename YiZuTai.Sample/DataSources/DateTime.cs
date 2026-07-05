using System.ComponentModel;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.DataSources
{
    [Category("时间")]
    [DisplayName("日期时间")]
    public class DateTime : AbstractAutoRefreshOnTimerDataSource
    {
        private string _Format = "HH:mm:ss";

        [DisplayName("格式")]
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
        public string Format
        {
            get { return _Format; }
            set
            {
                _Format = value;
                RaisePropertyChanged(nameof(Format));
            }
        }

        public override bool RefreshValue()
        {
            var preFix = "_";
            var value = System.DateTime.Now.ToString(preFix + Format);
            Value = value.Substring(preFix.Length);
            return true;
        }
    }
}
