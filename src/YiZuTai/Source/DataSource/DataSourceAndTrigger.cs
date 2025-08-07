using YiZuTai.Design.Trigger;
using YiZuTai.ViewModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YiZuTai.Source.DataSource
{
    /// <summary>
    /// 数据源与触发器类
    /// </summary>
    [DisplayName("数据源与触发器")]
    public class DataSourceAndTrigger<TControlDataTrigger> : PropertyNotifyModel
        where TControlDataTrigger : ControlDataTrigger
    {
        private IDataSource _DataSource;
        [DisplayName("数据源")]
        [Editor(typeof(Design.Editor.SourceTypeEditor), typeof(IDataSource))]
        public IDataSource DataSource
        {
            get { return _DataSource; }
            set
            {
                RaisePropertyChanging(nameof(DataSource));
                _DataSource = value;
                RaisePropertyChanged(nameof(DataSource));
            }
        }

        private ObservableCollection<TControlDataTrigger> _Triggers = new ObservableCollection<TControlDataTrigger>();
        [DisplayName("触发器")]
        [Editor(typeof(Design.Editor.CollectionEditor), typeof(Design.Editor.CollectionEditor))]
        public ObservableCollection<TControlDataTrigger> Triggers
        {
            get { return _Triggers; }
            set
            {
                _Triggers = value;
                RaisePropertyChanged(nameof(Triggers));
            }
        }

        public override string ToString()
        {
            return $"数据源[{DataSource?.Name ?? "无"}]-触发器[{Triggers?.Count ?? 0}]";
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
