using System.Collections.ObjectModel;
using System.ComponentModel;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.Image
{
    [Category("常用")]
    [DisplayName("图片")]
    public class Model : BaseControl_WithBackgroundAndAnimate
    {
        protected override Type GetViewType() => typeof(View);

        private string _Image;
        [Category("数据")]
        [DisplayName("图片")]
        [Description("图片控件要显示的图片")]
        [Editor(typeof(EditorInputImage), typeof(EditorInputImage))]
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        public override void OnDragDropCreate(Point location)
        {
            base.OnDragDropCreate(location);
            Width = 160;
            Height = 120;
            BackgroundOpacity = 0;
            Image = GetIcon();
        }


        private ObservableCollection<DataSourceAndTrigger> _DataSourceAndTriggers = new ObservableCollection<DataSourceAndTrigger>();
        [Category("数据源与触发器")]
        [DisplayName("更多")]
        [Editor(typeof(CollectionEditor), typeof(CollectionEditor))]
        public ObservableCollection<DataSourceAndTrigger> DataSourceAndTriggers
        {
            get { return _DataSourceAndTriggers; }
            set
            {
                _DataSourceAndTriggers = value;
                RaisePropertyChanged(nameof(DataSourceAndTriggers));
            }
        }


        private Dictionary<IDataSource, AbstractTrigger[]> cachedDataSourceTriggerDict = null;
        protected override Dictionary<IDataSource, AbstractTrigger[]> GetDataSourceTriggerDict()
        {
            if (cachedDataSourceTriggerDict == null)
            {
                foreach (var item in DataSourceAndTriggers)
                {
                    if (item.DataSource == null || item.Triggers.Count == 0)
                        continue;
                    cachedDataSourceTriggerDict[item.DataSource] = item.Triggers.ToArray();
                }
            }
            return cachedDataSourceTriggerDict;
        }
    }
}
