using Quick.Blazor.Bootstrap;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YiZuTai.Sample.Controls.TextCarousel
{
    public class PageInfo : PropertyNotifyModel
    {
        private int _Timeout = 1000;
        [Category("效果")]
        [DisplayName("停留时间")]
        [Description("单位：毫秒")]
        public int Timeout
        {
            get { return _Timeout; }
            set
            {
                _Timeout = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ItemInfo> _Items = new ObservableCollection<ItemInfo>();
        [Category("数据")]
        [DisplayName("文本项")]
        public ObservableCollection<ItemInfo> Items
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
            return $"页面信息[{Items.Count}文本项]";
        }
    }
}
