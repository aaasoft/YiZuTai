using System.ComponentModel;
using Quick.Blazor.Bootstrap;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public abstract class SeriesDataItem_Abstract : PropertyNotifyModel
    {
        public string _Name;
        [DisplayName("名称")]
        [Description("对象的名称")]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    [DisplayName("数据项")]
    public class SeriesDataItem_WithDataSource : SeriesDataItem_Abstract
    {
        private IDataSource _DataSource;
        [DefaultValue(null)]
        [DisplayName("单条数据源")]
        [Description("单条数据源")]
        public IDataSource DataSource
        {
            get { return _DataSource; }
            set
            {
                _DataSource = value;
                RaisePropertyChanged(nameof(DataSource));
            }
        }
    }

    [DisplayName("数据项")]
    public class SeriesDataItem_WithQueryDataSource : SeriesDataItem_Abstract
    {
        private IQueryDataSource _QueryDataSource;
        [DefaultValue(null)]
        [DisplayName("多条数据源")]
        [Description("多条数据源")]
        public IQueryDataSource QueryDataSource
        {
            get { return _QueryDataSource; }
            set
            {
                _QueryDataSource = value;
                RaisePropertyChanged(nameof(QueryDataSource));
            }
        }
    }

    [DisplayName("数据项")]
    public class SeriesDataItem : SeriesDataItem_WithDataSource
    {
        private IQueryDataSource _QueryDataSource;
        [DefaultValue(null)]
        [DisplayName("多条数据源")]
        [Description("多条数据源")]
        public IQueryDataSource QueryDataSource
        {
            get { return _QueryDataSource; }
            set
            {
                _QueryDataSource = value;
                RaisePropertyChanged(nameof(QueryDataSource));
            }
        }
    }
}
