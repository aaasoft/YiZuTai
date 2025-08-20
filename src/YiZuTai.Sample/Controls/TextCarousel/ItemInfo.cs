using Newtonsoft.Json;
using Quick.Blazor.Bootstrap;
using Quick.Xml;
using System.ComponentModel;
using YiZuTai.Components.Editors;
using YiZuTai.DataSource;
using YiZuTai.Enums;
using YiZuTai.HyperLinkSource;

namespace YiZuTai.Sample.Controls.TextCarousel
{
    public class ItemInfo : PropertyNotifyModel
    {
        //默认为黑色
        private string _Foreground = "#FFFFFF";
        [Category("文本")]
        [DisplayName("颜色")]
        [Description("文字颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string Foreground
        {
            get { return _Foreground; }
            set
            {
                _Foreground = value;
                RaisePropertyChanged();
            }
        }


        private string _Format = "{value}";
        [Category("文本")]
        [DisplayName("格式")]
        [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
        public string Format
        {
            get { return _Format; }
            set
            {
                _Format = value;
                RaisePropertyChanged();
            }
        }

        private double _FontSize = 16;
        [Category("文本")]
        [DisplayName("字号")]
        [Description("文本的字号")]
        [DefaultValue(16D)]
        public virtual double FontSize
        {
            get { return _FontSize; }
            set
            {
                _FontSize = value;
                RaisePropertyChanged(nameof(FontSize));
            }
        }

        private double _LineHeight = 32;
        [Category("文本")]
        [DisplayName("行高")]
        [DefaultValue(32D)]
        public virtual double LineHeight
        {
            get { return _LineHeight; }
            set
            {
                _LineHeight = value;
                RaisePropertyChanged(nameof(LineHeight));
            }
        }

        //默认为宋体
        private string _FontFamily;
        [Category("文本")]
        [DisplayName("字体")]
        [Description("文字的字体")]
        public virtual string FontFamily
        {
            get { return _FontFamily; }
            set
            {
                _FontFamily = value;
                RaisePropertyChanged();
            }
        }

        private TextAnchor _TextAnchor = TextAnchor.start;
        [Category("文本")]
        [DisplayName("对齐")]
        [Description("文字的对齐方式")]
        public virtual TextAnchor TextAnchor
        {
            get { return _TextAnchor; }
            set
            {
                _TextAnchor = value;
                RaisePropertyChanged(nameof(TextAnchor));
            }
        }


        private IDataSource _Value;
        [Category("文本")]
        [DisplayName("值")]
        [Description("文本块控件要显示的值")]
        public IDataSource Value
        {
            get { return _Value; }
            set
            {
                RaisePropertyChanging();
                _Value = value;
                RaisePropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"文本项";
        }

        private IHyperLinkSource _HyperLinkSource;
        [Category("超链接")]
        [DisplayName("源")]
        public virtual IHyperLinkSource HyperLinkSource
        {
            get { return _HyperLinkSource; }
            set
            {
                RaisePropertyChanging(nameof(HyperLinkSource));
                _HyperLinkSource = value;
                RaisePropertyChanged(nameof(HyperLinkSource));
            }
        }
        [JsonIgnore]
        [Browsable(false)]
        public string Text => string.IsNullOrEmpty(Format) ? null : XmlConvert.Escape(Format.Replace("{value}", Value?.Value ?? ""));
        
/*
        private ObservableCollection<ItemTrigger> _ValueTriggers = new ObservableCollection<ItemTrigger>();
        [Category("数据源与触发器")]
        [DisplayName("值触发器")]
        [Editor(typeof(App.Core.TypeEditors.CollectionEditor), typeof(App.Core.TypeEditors.CollectionEditor))]
        public ObservableCollection<ItemTrigger> ValueTriggers
        {
            get { return _ValueTriggers; }
            set
            {
                _ValueTriggers = value;
                RaisePropertyChanged(nameof(ValueTriggers));
            }
        }

        private ObservableCollection<ItemDataSourceAndTrigger> _DataSourceAndTriggers = new ObservableCollection<ItemDataSourceAndTrigger>();
        [Category("数据源与触发器")]
        [DisplayName("更多")]
        [Editor(typeof(App.Core.TypeEditors.CollectionEditor), typeof(App.Core.TypeEditors.CollectionEditor))]
        public ObservableCollection<ItemDataSourceAndTrigger> DataSourceAndTriggers
        {
            get { return _DataSourceAndTriggers; }
            set
            {
                _DataSourceAndTriggers = value;
                RaisePropertyChanged(nameof(DataSourceAndTriggers));
            }
        }

        /// <summary>
        /// 全部的数据源与触发器
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public IEnumerable<ItemDataSourceAndTrigger> AllDataSourceAndTriggers
        {
            get
            {
                List<ItemDataSourceAndTrigger> list = new List<ItemDataSourceAndTrigger>();
                list.Add(new ItemDataSourceAndTrigger()
                {
                    DataSource = Value,
                    Triggers = ValueTriggers
                });
                list.AddRange(DataSourceAndTriggers);
                return list;
            }
        }
        /// <summary>
        /// 全部的数据源与触发器的JSON字符串
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public string AllDataSourceAndTriggersJson => JsonConvert.SerializeObject(AllDataSourceAndTriggers);
*/
    }
}
