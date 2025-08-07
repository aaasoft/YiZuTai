using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using Quick.Xml;
using YiZuTai.Design;
using YiZuTai.Design.Editor;
using YiZuTai.Design.Enums;
using YiZuTai.Design.Trigger;
using YiZuTai.Source.DataSource;

namespace YiZuTai.Control.TextBlock;

[Category("常用")]
[DisplayName("文本")]
public class Model : BaseControl_WithBackgroundAndAnimate
{
    protected override Type GetViewType() => typeof(View);

    public Model()
    {
        Border = "#000000";
        BackgroundOpacity = 0;
        Width = 120;
        Height = 40;
    }

    //默认为黑色
    private string _Foreground = "#000000";
    [Category("文本")]
    [DisplayName("颜色")]
    [Description("文字颜色")]
    [Editor(typeof(EditorInputColor), typeof(string))]
    public virtual string Foreground
    {
        get { return _Foreground; }
        set
        {
            _Foreground = value;
            RaisePropertyChanged(nameof(Foreground));
        }
    }

    private string _Format = "{value}";
    [Category("文本")]
    [DisplayName("格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    [DefaultValue("{value}")]
    public string Format
    {
        get { return _Format; }
        set
        {
            _Format = value;
            RaisePropertyChanged(nameof(Format));
        }
    }

    private double _FontSize = 24;
    [Category("文本")]
    [DisplayName("字号")]
    [Description("文本的字号")]
    [DefaultValue(24D)]
    public virtual double FontSize
    {
        get { return _FontSize; }
        set
        {
            _FontSize = value;
            RaisePropertyChanged(nameof(FontSize));
        }
    }

    //默认为宋体
    private string _FontFamily = "Microsoft YaHei";
    [Category("文本")]
    [DisplayName("字体")]
    [Description("文字的字体")]
    public virtual string FontFamily
    {
        get { return _FontFamily; }
        set
        {
            _FontFamily = value;
            RaisePropertyChanged(nameof(FontFamily));
        }
    }

    private TextAnchor _TextAnchor = TextAnchor.middle;
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

    private TextFontWeight _FontWeight = TextFontWeight.normal;
    [Category("文本")]
    [DisplayName("粗细")]
    [Description("文字的粗细")]
    public virtual TextFontWeight FontWeight
    {
        get { return _FontWeight; }
        set
        {
            _FontWeight = value;
            RaisePropertyChanged(nameof(FontWeight));
        }
    }

    private double _TextVerticalOffset = 0.5;
    [Category("文本")]
    [DisplayName("垂直偏移量")]
    [Description(@"当值在0~1之间时，偏移量为比例
当值大于1时，偏移量为像素")]
    public virtual double TextVerticalOffset
    {
        get { return _TextVerticalOffset; }
        set
        {
            if (value < 0)
                return;
            _TextVerticalOffset = value;
            RaisePropertyChanged(nameof(TextVerticalOffset));
        }
    }

    private string _Image;
    [Category("背景")]
    [DisplayName("背景图片")]
    [Description("背景上要显示的图片")]
    [Editor(typeof(Design.Editor.EditorInputImage), typeof(string))]
    [DefaultValue(null)]
    public string Image
    {
        get { return _Image; }
        set
        {
            _Image = value;
            RaisePropertyChanged(nameof(Image));
        }
    }


    private IDataSource _Value;
    [Category("文本")]
    [DisplayName("值")]
    [Description("文本块控件要显示的值")]
    [Editor(typeof(Design.Editor.SourceTypeEditor), typeof(IDataSource))]
    public IDataSource Value
    {
        get { return _Value; }
        set
        {
            RaisePropertyChanging(nameof(Value));
            _Value = value;
            RaisePropertyChanged(nameof(Value));
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public string Text => XmlConvert.Escape(Format.Replace("{value}", Value?.Value ?? ""));

    [JsonIgnore]
    public double TextX
    {
        get
        {
            var cx = Width / 2;
            double textX = 0;
            switch (TextAnchor)
            {
                case TextAnchor.start:
                    textX = 0;
                    break;
                case TextAnchor.middle:
                    textX = cx;
                    break;
                case TextAnchor.end:
                    textX = Width;
                    break;
            }
            return textX;
        }
    }

    [JsonIgnore]
    public double TextY
    {
        get
        {
            var cy = Height / 2;
            double textY = 0;
            if (TextVerticalOffset > 1)
                textY = TextVerticalOffset;
            else
                textY = (Height - FontSize) * TextVerticalOffset + FontSize * 7 / 8;
            return textY;
        }
    }

    /*
    public override void OnDoubleClick()
    {
        showDataSourceWindow(Value, t => Value = t);
    }
    */

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);
        Value = new DataSource.Input() { Value = Name };
        ValueTriggers = new ObservableCollection<Trigger>();
    }

    private ObservableCollection<Trigger> _ValueTriggers;
    [Category("数据源与触发器")]
    [DisplayName("值触发器")]
    [Editor(typeof(CollectionEditor), typeof(CollectionEditor))]
    public ObservableCollection<Trigger> ValueTriggers
    {
        get { return _ValueTriggers; }
        set
        {
            _ValueTriggers = value;
            RaisePropertyChanged(nameof(ValueTriggers));
        }
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

    public override string[] GetHasBussinesToDispayProperties()
    {
        return base.GetHasBussinesToDispayProperties().Concat(new string[]
            {
                nameof(Width),
                nameof(Height),
                nameof(Foreground),
                nameof(Value),
                nameof(Format),
                nameof(FontSize),
                nameof(FontFamily),
                nameof(TextVerticalOffset),
                nameof(Image)
            }).ToArray();
    }

    private Dictionary<IDataSource, ControlDataTrigger[]> cachedDataSourceTriggerDict = null;
    protected override Dictionary<IDataSource, ControlDataTrigger[]> GetDataSourceTriggerDict()
    {
        if (cachedDataSourceTriggerDict == null)
        {
            cachedDataSourceTriggerDict = new();
            if (Value != null && ValueTriggers.Count > 0)
                cachedDataSourceTriggerDict[Value] = ValueTriggers.ToArray();
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