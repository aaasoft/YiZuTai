using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Enums;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.TempAndHum;

public class Trigger : BaseControlTrigger
{
    private TextAnchor? _TitleTextAnchor;
    [Category("标题")]
    [DisplayName("对齐")]
    [Description("对齐方式")]
    public TextAnchor? TitleTextAnchor
    {
        get { return _TitleTextAnchor; }
        set
        {
            _TitleTextAnchor = value;
            RaisePropertyChanged();
        }
    }

    private string _TitleFormat;
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
    private string _TitleForeground;
    [Category("标题")]
    [DisplayName("前景色")]
    [Description("标题的前景颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public string TitleForeground
    {
        get { return _TitleForeground; }
        set
        {
            _TitleForeground = value;
            RaisePropertyChanged(nameof(TitleForeground));
        }
    }


    private double? _TitleForegroundOpacity;
    [Category("标题")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
    [Range(0, 1)]
    [StringLength(100)]
    public double? TitleForegroundOpacity
    {
        get { return _TitleForegroundOpacity; }
        set
        {
            _TitleForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    //默认为宋体
    private string _TitleFontFamily;
    [Category("标题")]
    [DisplayName("字体")]
    [Description("标题的字体")]
    public string TitleFontFamily
    {
        get { return _TitleFontFamily; }
        set
        {
            _TitleFontFamily = value;
            RaisePropertyChanged(nameof(TitleFontFamily));
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

    private string _TempForeground;
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


    private double? _TempForegroundOpacity;
    [Category("温度")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
    [Range(0, 1)]
    [StringLength(100)]
    public double? TempForegroundOpacity
    {
        get { return _TempForegroundOpacity; }
        set
        {
            _TempForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private TextAnchor? _TempTextAnchor;
    [Category("温度")]
    [DisplayName("对齐")]
    [Description("对齐方式")]
    public TextAnchor? TempTextAnchor
    {
        get { return _TempTextAnchor; }
        set
        {
            _TempTextAnchor = value;
            RaisePropertyChanged();
        }
    }

    private string _TempFormat;
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

    private string _HumForeground;
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

    private double? _HumForegroundOpacity;
    [Category("湿度")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
    [Range(0, 1)]
    [StringLength(100)]
    public double? HumForegroundOpacity
    {
        get { return _HumForegroundOpacity; }
        set
        {
            _HumForegroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private TextAnchor? _HumTextAnchor;
    [Category("湿度")]
    [DisplayName("对齐")]
    [Description("对齐方式")]
    public TextAnchor? HumTextAnchor
    {
        get { return _HumTextAnchor; }
        set
        {
            _HumTextAnchor = value;
            RaisePropertyChanged(nameof(HumTextAnchor));
        }
    }

    private string _HumFormat;
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

    public override void Execute(BaseControl controlModel)
    {
        base.Execute(controlModel);

        var model = (Model)controlModel;
        if (TitleTextAnchor != null)
            model.TitleTextAnchor = TitleTextAnchor.Value;
        if (!string.IsNullOrEmpty(TitleFormat))
            model.TitleFormat = TitleFormat;
        if (!string.IsNullOrEmpty(TitleForeground))
            model.TitleForeground = TitleForeground;
        if (TitleForegroundOpacity != null)
            model.TitleForegroundOpacity = TitleForegroundOpacity.Value;
        if (!string.IsNullOrEmpty(TitleFontFamily))
            model.TitleFontFamily = TitleFontFamily;

        if (!string.IsNullOrEmpty(TempFontFamily))
            model.TempFontFamily = TempFontFamily;
        if (!string.IsNullOrEmpty(TempForeground))
            model.TempForeground = TempForeground;
        if (TempForegroundOpacity != null)
            model.TempForegroundOpacity = TempForegroundOpacity.Value;
        if (TempTextAnchor != null)
            model.TempTextAnchor = TempTextAnchor.Value;
        if (!string.IsNullOrEmpty(TempFormat))
            model.TempFormat = TempFormat;

        if (!string.IsNullOrEmpty(HumFontFamily))
            model.HumFontFamily = HumFontFamily;
        if (!string.IsNullOrEmpty(HumForeground))
            model.HumForeground = HumForeground;
        if (HumForegroundOpacity != null)
            model.HumForegroundOpacity = HumForegroundOpacity.Value;
        if (HumTextAnchor != null)
            model.HumTextAnchor = HumTextAnchor.Value;
        if (!string.IsNullOrEmpty(HumFormat))
            model.HumFormat = HumFormat;
    }
}

public class DataSourceAndTrigger : DataSourceAndTrigger<Trigger> { }