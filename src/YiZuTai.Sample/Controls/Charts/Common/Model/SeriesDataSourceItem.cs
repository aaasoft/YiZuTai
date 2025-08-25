using Quick.Blazor.Bootstrap;
using System.ComponentModel;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public class SeriesDataSourceItem : PropertyNotifyModel
    {
        private IDataSource _DataSource;
        [DisplayName("数据源")]
        [Description("对象的数据")]
        public IDataSource DataSource
        {
            get { return _DataSource; }
            set
            {
                _DataSource = value;
                RaisePropertyChanged(nameof(DataSource));
            }
        }

        public override string ToString()
        {
            return DataSource?.Name ?? "未选择";
        }
    }
}
