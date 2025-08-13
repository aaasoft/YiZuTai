using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Quick.Xml;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Enums;
using YiZuTai.HyperLinkSource;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.TempAndHum;

[Category("动环")]
[DisplayName("温湿度")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);

    private TextAnchor _TitleTextAnchor = TextAnchor.middle;
    [Category("标题")]
    [DisplayName("对齐")]
    [Description("对齐方式")]
    public TextAnchor TitleTextAnchor
    {
        get { return _TitleTextAnchor; }
        set
        {
            _TitleTextAnchor = value;
            RaisePropertyChanged();
        }
    }

    private IDataSource _TitleValue;
    [Category("标题")]
    [DisplayName("值")]
    [Description("文本块控件要显示的值")]
    public IDataSource TitleValue
    {
        get { return _TitleValue; }
        set
        {
            RaisePropertyChanging(nameof(TitleValue));
            _TitleValue = value;
            RaisePropertyChanged(nameof(TitleValue));
        }
    }

    private string _TitleFormat = "{value}";
    [Category("标题")]
    [DisplayName("格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    public string TitleFormat
    {
        get { return _TitleFormat; }
        set
        {
            _TitleFormat = value;
            RaisePropertyChanged(nameof(TitleFormat));
        }
    }

    //默认为黑色
    private string _TitleForeground = "#FFFFFF";
    [Category("标题")]
    [DisplayName("前景色")]
    [Description("标题的前景颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string TitleForeground
    {
        get { return _TitleForeground; }
        set
        {
            _TitleForeground = value;
            RaisePropertyChanged(nameof(TitleForeground));
        }
    }


    private double _TitleForegroundOpacity = 1;
    [Category("标题")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public virtual double TitleForegroundOpacity
    {
        get { return _TitleForegroundOpacity; }
        set
        {
            _TitleForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private string _TitleFontFamily;
    [Category("标题")]
    [DisplayName("字体")]
    public virtual string TitleFontFamily
    {
        get { return _TitleFontFamily; }
        set
        {
            _TitleFontFamily = value;
            RaisePropertyChanged(nameof(TitleFontFamily));
        }
    }

    private ObservableCollection<Trigger> _TitleTriggers = new ObservableCollection<Trigger>();
    [Category("标题")]
    [DisplayName("标题触发器")]
    public ObservableCollection<Trigger> TitleTriggers
    {
        get { return _TitleTriggers; }
        set
        {
            _TitleTriggers = value;
            RaisePropertyChanged(nameof(TitleTriggers));
        }
    }

    private IDataSource _TempValue;
    [Category("温度")]
    [DisplayName("值")]
    public IDataSource TempValue
    {
        get { return _TempValue; }
        set
        {
            RaisePropertyChanging(nameof(TempValue));
            _TempValue = value;
            RaisePropertyChanged(nameof(TempValue));
        }
    }

    private string _TempFontFamily;
    [Category("温度")]
    [DisplayName("字体")]
    public virtual string TempFontFamily
    {
        get { return _TempFontFamily; }
        set
        {
            _TempFontFamily = value;
            RaisePropertyChanged();
        }
    }

    private string _TempForeground = "#FFFFFF";
    [Category("温度")]
    [DisplayName("颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string TempForeground
    {
        get { return _TempForeground; }
        set
        {
            _TempForeground = value;
            RaisePropertyChanged();
        }
    }


    private double _TempForegroundOpacity = 1;
    [Category("温度")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public virtual double TempForegroundOpacity
    {
        get { return _TempForegroundOpacity; }
        set
        {
            _TempForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private TextAnchor _TempTextAnchor = TextAnchor.middle;
    [Category("温度")]
    [DisplayName("对齐")]
    [Description("对齐方式")]
    public TextAnchor TempTextAnchor
    {
        get { return _TempTextAnchor; }
        set
        {
            _TempTextAnchor = value;
            RaisePropertyChanged();
        }
    }

    private string _TempFormat = "{value} ℃";
    [Category("温度")]
    [DisplayName("格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    public string TempFormat
    {
        get { return _TempFormat; }
        set
        {
            _TempFormat = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<Trigger> _TempTriggers = new ObservableCollection<Trigger>();
    [Category("温度")]
    [DisplayName("温度触发器")]
    public ObservableCollection<Trigger> TempTriggers
    {
        get { return _TempTriggers; }
        set
        {
            _TempTriggers = value;
            RaisePropertyChanged(nameof(TempTriggers));
        }
    }


    private IDataSource _HumValue;
    [Category("湿度")]
    [DisplayName("值")]
    [Description("文本块控件要显示的值")]
    public IDataSource HumValue
    {
        get { return _HumValue; }
        set
        {
            RaisePropertyChanging(nameof(HumValue));
            _HumValue = value;
            RaisePropertyChanged(nameof(HumValue));
        }
    }
    
    private string _HumFontFamily;
    [Category("湿度")]
    [DisplayName("字体")]
    public virtual string HumFontFamily
    {
        get { return _HumFontFamily; }
        set
        {
            _HumFontFamily = value;
            RaisePropertyChanged();
        }
    }

    private string _HumForeground = "#FFFFFF";
    [Category("湿度")]
    [DisplayName("颜色")]
    public string HumForeground
    {
        get { return _HumForeground; }
        set
        {
            _HumForeground = value;
            RaisePropertyChanged(nameof(HumForeground));
        }
    }

    private double _HumForegroundOpacity = 1;
    [Category("湿度")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public double HumForegroundOpacity
    {
        get { return _HumForegroundOpacity; }
        set
        {
            _HumForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private TextAnchor _HumTextAnchor = TextAnchor.middle;
    [Category("湿度")]
    [DisplayName("对齐")]
    [Description("对齐方式")]
    public TextAnchor HumTextAnchor
    {
        get { return _HumTextAnchor; }
        set
        {
            _HumTextAnchor = value;
            RaisePropertyChanged(nameof(HumTextAnchor));
        }
    }

    private string _HumFormat = "{value} ％";
    [Category("湿度")]
    [DisplayName("格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    public string HumFormat
    {
        get { return _HumFormat; }
        set
        {
            _HumFormat = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<Trigger> _HumTriggers = new ObservableCollection<Trigger>();
    [Category("湿度")]
    [DisplayName("湿度触发器")]
    public ObservableCollection<Trigger> HumTriggers
    {
        get { return _HumTriggers; }
        set
        {
            _HumTriggers = value;
            RaisePropertyChanged(nameof(HumTriggers));
        }
    }

    private IHyperLinkSource _TempHyperLinkSource;
    [Category("温度")]
    [DisplayName("超链接")]
    public virtual IHyperLinkSource TempHyperLinkSource
    {
        get { return _TempHyperLinkSource; }
        set
        {
            RaisePropertyChanging(nameof(TempHyperLinkSource));
            _TempHyperLinkSource = value;
            RaisePropertyChanged(nameof(TempHyperLinkSource));
        }
    }

    private IHyperLinkSource _HumHyperLinkSource;
    [Category("湿度")]
    [DisplayName("超链接")]
    public virtual IHyperLinkSource HumHyperLinkSource
    {
        get { return _HumHyperLinkSource; }
        set
        {
            RaisePropertyChanging(nameof(HumHyperLinkSource));
            _HumHyperLinkSource = value;
            RaisePropertyChanged(nameof(HumHyperLinkSource));
        }
    }


    private int padding = 4;

    private double getTextX(TextAnchor textAnchor)
    {
        double textX = 0;
        switch (textAnchor)
        {
            case TextAnchor.start:
                textX = padding;
                break;
            case TextAnchor.middle:
                textX = DefaultWidth / 2;
                break;
            case TextAnchor.end:
                textX = DefaultWidth - padding;
                break;
        }
        return textX;
    }

    [JsonIgnore]
    [Browsable(false)]
    public string TitleText => XmlConvert.Escape(TitleFormat.Replace("{value}", TitleValue?.Value ?? ""));
    [JsonIgnore]
    [Browsable(false)]
    public double TitleTextX => getTextX(TitleTextAnchor);
    [JsonIgnore]
    [Browsable(false)]
    public string TempText => XmlConvert.Escape(TempFormat.Replace("{value}", TempValue?.Value ?? ""));
    [JsonIgnore]
    [Browsable(false)]
    public double TempTextX => getTextX(TempTextAnchor);
    [JsonIgnore]
    [Browsable(false)]
    public string HumText => XmlConvert.Escape(HumFormat.Replace("{value}", HumValue?.Value ?? ""));
    [JsonIgnore]
    [Browsable(false)]
    public double HumTextX => getTextX(HumTextAnchor);

    protected override IEnumerable<Tuple<IDataSource, AbstractTrigger[]>> GetDataSourceAndTriggers()
    {
        if (TitleValue != null && TitleTriggers.Count > 0)
            yield return new(TitleValue, TitleTriggers.ToArray());
        if (TempValue != null && TempTriggers.Count > 0)
            yield return new(TempValue, TempTriggers.ToArray());
        if (HumValue != null && HumTriggers.Count > 0)
            yield return new(HumValue, HumTriggers.ToArray());
    }

    private const double DefaultWidth = 60;
    private const double DefaultHeight = 60;

    public override void OnDragDropCreate(Point location)
    {
        Width = DefaultWidth;
        Height = DefaultHeight;
        Background = "#4282C2"; ;
        BackgroundOpacity = 1;
        CornerRadius = 8;
        TitleValue = new DataSources.Input() { Content = "标题" };
        TempValue = new DataSources.Simulate() { MaxValue = 24, MinValue = 14, DecimalPlace = 1, Period = 1 };
        HumValue = new DataSources.Simulate() { MaxValue = 60, MinValue = 45, DecimalPlace = 1, Period = 1 };
    }
}