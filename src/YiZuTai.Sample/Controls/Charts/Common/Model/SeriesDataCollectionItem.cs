using Quick.Blazor.Bootstrap;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public class SeriesDataCollectionItem : PropertyNotifyModel
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

        private ObservableCollection<SeriesDataSourceItem> _Items = new ObservableCollection<SeriesDataSourceItem>();
        [DisplayName("数据项列表")]
        public ObservableCollection<SeriesDataSourceItem> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
