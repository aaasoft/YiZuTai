using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Quick.Xml;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Enums;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.GroupBox;

[Category("容器")]
[DisplayName("组框")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);
    public override bool IsContainer => true;
    public override ObservableCollection<BaseControl> Controls { get; set; } = new ObservableCollection<BaseControl>();

    private double _TitleHeight = 40;
    [Category("标题栏")]
    [DisplayName("高度")]
    [Description("标题栏的高度")]
    public virtual double TitleHeight
    {
        get { return _TitleHeight; }
        set
        {
            if (value < 1)
                return;
            _TitleHeight = value;
            RaisePropertyChanged(nameof(TitleHeight));
        }
    }

    //默认为黑色
    private string _TitleForeground = "#000000";
    [Category("标题栏")]
    [DisplayName("前景颜色")]
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
    [Category("标题栏")]
    [DisplayName("前景透明度")]
    [Range(0, 1)]
    [StringLength(100)]
    public double TitleForegroundOpacity
    {
        get { return _TitleForegroundOpacity; }
        set
        {
            _TitleForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    //默认为黑色
    private string _TitleTextForeground = "#FFFFFF";
    [Category("标题栏")]
    [DisplayName("文本颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string TitleTextForeground
    {
        get { return _TitleTextForeground; }
        set
        {
            _TitleTextForeground = value;
            RaisePropertyChanged();
        }
    }

    private double _TitleTextForegroundOpacity = 1;
    [Category("标题栏")]
    [DisplayName("文本透明度")]
    [Range(0, 1)]
    [StringLength(100)]
    public double TitleTextForegroundOpacity
    {
        get { return _TitleTextForegroundOpacity; }
        set
        {
            _TitleTextForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private string _TitleBackground = "#000000";
    [Category("标题栏")]
    [DisplayName("背景颜色")]
    [Description("标题栏的背景颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string TitleBackground
    {
        get { return _TitleBackground; }
        set
        {
            _TitleBackground = value;
            RaisePropertyChanged(nameof(TitleBackground));
        }
    }


    private double _TitleBackgroundOpacity = 1;
    [Category("标题栏")]
    [DisplayName("背景透明度")]
    [Range(0, 1)]
    [StringLength(100)]
    public double TitleBackgroundOpacity
    {
        get { return _TitleBackgroundOpacity; }
        set
        {
            _TitleBackgroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private IDataSource _TitleValue;
    [Category("标题栏")]
    [DisplayName("文本值")]
    public IDataSource TitleValue
    {
        get { return _TitleValue; }
        set
        {
            RaisePropertyChanging();
            _TitleValue = value;
            RaisePropertyChanged();
        }
    }


    private double _TitleFontSize = 18;
    [Category("标题栏")]
    [DisplayName("文本字号")]
    [DefaultValue(24D)]
    public virtual double TitleFontSize
    {
        get { return _TitleFontSize; }
        set
        {
            _TitleFontSize = value;
            RaisePropertyChanged();
        }
    }

    //默认为宋体
    private string _TitleFontFamily;
    [Category("标题栏")]
    [DisplayName("文本字体")]
    public virtual string TitleFontFamily
    {
        get { return _TitleFontFamily; }
        set
        {
            _TitleFontFamily = value;
            RaisePropertyChanged();
        }
    }

    private TextAnchor _TitleTextAnchor = TextAnchor.start;
    [Category("标题栏")]
    [DisplayName("文本对齐")]
    [Description("文字的对齐方式")]
    public virtual TextAnchor TitleTextAnchor
    {
        get { return _TitleTextAnchor; }
        set
        {
            _TitleTextAnchor = value;
            RaisePropertyChanged();
        }
    }

    private double _TitleTextVerticalOffset = 0.5;
    [Category("标题栏")]
    [DisplayName("文本垂直偏移量")]
    [Description(@"当值在0~1之间时，偏移量为比例
当值大于1时，偏移量为像素")]
    public virtual double TitleTextVerticalOffset
    {
        get { return _TitleTextVerticalOffset; }
        set
        {
            if (value < 0)
                return;
            _TitleTextVerticalOffset = value;
            RaisePropertyChanged(nameof(TitleTextVerticalOffset));
        }
    }

    private string _TitleFormat = "{value}";
    [Category("标题栏")]
    [DisplayName("文本格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    [DefaultValue("{value}")]
    public string TitleFormat
    {
        get { return _TitleFormat; }
        set
        {
            _TitleFormat = value;
            RaisePropertyChanged();
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public string TitleText => XmlConvert.Escape(TitleFormat.Replace("{value}", TitleValue?.Value ?? ""));

    public override void OnDragDropCreate(Point location)
    {
        Width = 400;
        Height = 300;
        TitleValue = new DataSources.Input() { Value = "标题" };

    }

    public static IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut<Model>("样式-浅色", (context, t) =>
        {
            t.TitleForeground = "#FFFFFF";
            t.TitleForegroundOpacity=1;
            t.TitleBackground ="#000000";
            t.TitleBackgroundOpacity = 0.33;
            t.Background = "#000000";
            t.BackgroundOpacity=0.17;
        }),
        new ControlShotcut<Model>("样式-深色", (context, t) =>
        {
            t.TitleForeground = "#00DAFF";
            t.TitleForegroundOpacity=1;
            t.TitleBackground = "#143F6D";
            t.TitleBackgroundOpacity =0.72;
            t.Background = "#062A56";
            t.BackgroundOpacity=0.66;
        })
    ];
}
