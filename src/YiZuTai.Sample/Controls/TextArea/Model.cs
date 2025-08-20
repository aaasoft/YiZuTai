using System.ComponentModel;
using Newtonsoft.Json;
using Quick.Xml;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Enums;

namespace YiZuTai.Sample.Controls.TextArea;

[Category("常用")]
[DisplayName("多行文本")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);

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


    //默认为黑色
    private string _Foreground = "#000000";
    [Category("文本")]
    [DisplayName("颜色")]
    [Description("文字颜色")]
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
    [DefaultValue("{value}")]
    public string Format
    {
        get { return _Format; }
        set
        {
            _Format = value;
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }

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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }

    public double _LineHeight = 28;
    [Category("文本")]
    [DisplayName("行高")]
    public double LineHeight
    {
        get { return _LineHeight; }
        set
        {
            _LineHeight = value;
            RaisePropertyChanged(nameof(LineHeight));
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
            RaisePropertyChanged();
        }
    }

    public IEnumerable<string> Rows
    {
        get
        {
            var charsOneLine = Convert.ToInt32(Math.Floor(Width / FontSize));
            IEnumerable<char> a = Text;
            while (true)
            {
                var line = new string(a.Take(charsOneLine).ToArray());
                if (line.Length <= 0)
                    break;
                yield return line;
                a = a.Skip(charsOneLine);
            }
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public string Text => XmlConvert.Escape(Format.Replace("{value}", Value?.Value ?? ""));

    public override void OnDragDropCreate(Point location)
    {
        Width = 120;
        Height = 40;
    }

    public override IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut("样式-浅色", ()=>
            {
                Foreground = "#000000";
            }),
            new ControlShotcut("样式-深色", ()=>
            {
                Foreground = "#FFFFFF";
            })
    ];
}
