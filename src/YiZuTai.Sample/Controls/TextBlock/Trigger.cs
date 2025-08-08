using System.ComponentModel;
using YiZuTai.Control;
using YiZuTai.Components.Editors;
using YiZuTai.Enums;
using YiZuTai.Trigger;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.Controls.TextBlock;

public class Trigger : BaseControl_WithBackgroundAndAnimateTrigger
{
    private string _Format;
    [Category("文本")]
    [DisplayName("格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    [DefaultValue(null)]
    public string Format
    {
        get { return _Format; }
        set
        {
            _Format = value;
            RaisePropertyChanged(nameof(Format));
        }
    }

    private string _Foreground;
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


    private double? _FontSize;
    [Category("文本")]
    [DisplayName("字号")]
    [Description("文本的字号")]
    public virtual double? FontSize
    {
        get { return _FontSize; }
        set
        {
            _FontSize = value;
            RaisePropertyChanged(nameof(FontSize));
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
            RaisePropertyChanged(nameof(FontFamily));
        }
    }

    private TextAnchor? _TextAnchor;
    [Category("文本")]
    [DisplayName("对齐")]
    [Description("文字的对齐方式")]
    public virtual TextAnchor? TextAnchor
    {
        get { return _TextAnchor; }
        set
        {
            _TextAnchor = value;
            RaisePropertyChanged(nameof(TextAnchor));
        }
    }

    private TextFontWeight? _FontWeight;
    [Category("文本")]
    [DisplayName("粗细")]
    [Description("文字的粗细")]
    public virtual TextFontWeight? FontWeight
    {
        get { return _FontWeight; }
        set
        {
            _FontWeight = value;
            RaisePropertyChanged(nameof(FontWeight));
        }
    }

    private double? _TextVerticalOffset;
    [Category("文本")]
    [DisplayName("垂直偏移量")]
    [Description(@"当值在0~1之间时，偏移量为比例
当值大于1时，偏移量为像素")]
    public virtual double? TextVerticalOffset
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
    [Editor(typeof(EditorInputImage), typeof(EditorInputImage))]
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


    public override void Execute(BaseControl controlModel)
    {
        base.Execute(controlModel);

        var model = (Model)controlModel;
        if (!string.IsNullOrEmpty(Format))
            model.Format = Format;
        if (!string.IsNullOrEmpty(Foreground))
            model.Foreground = Foreground;
        if (FontSize != null)
            model.FontSize = FontSize.Value;
        if (!string.IsNullOrEmpty(FontFamily))
            model.FontFamily = FontFamily;
        if (TextAnchor != null)
            model.TextAnchor = TextAnchor.Value;
        if (FontWeight != null)
            model.FontWeight = FontWeight.Value;
        if (TextVerticalOffset != null)
            model.TextVerticalOffset = TextVerticalOffset.Value;
        if (!string.IsNullOrEmpty(Image))
            model.Image = Image;
    }
}

public class DataSourceAndTrigger : DataSourceAndTrigger<Trigger> { }