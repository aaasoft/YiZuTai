using System.ComponentModel;
using YiZuTai.Core.Source;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.DataSources
{
    [Category("基础")]
    [DisplayName("模拟数据")]
    public class Simulate : AbstractAutoRefreshOnTimerDataSource
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

        private static Random random = new Random();

        public override bool RefreshValue()
        {
            var d = MaxValue - MinValue;
            var value = MinValue + d * random.NextDouble();
            Value = value.ToString("N" + DecimalPlace);
            return true;
        }
    }
}