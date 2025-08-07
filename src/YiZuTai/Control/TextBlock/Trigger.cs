using System.ComponentModel;
using YiZuTai.Design;
using YiZuTai.Design.Editor;
using YiZuTai.Design.Trigger;

namespace YiZuTai.Control.TextBlock;

public class Trigger : ControlDataTrigger
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

    private string _Background;
    [Category("背景")]
    [DisplayName("背景颜色")]
    [Description("控件的背景颜色")]
    [Editor(typeof(EditorInputColor), typeof(string))]
    public virtual string Background
    {
        get { return _Background; }
        set
        {
            _Background = value;
            RaisePropertyChanged(nameof(Background));
        }
    }


    private double? _RotateSpeed;
    [Category("动画")]
    [DisplayName("旋转速度")]
    [Description("控件旋转的速度")]
    public double? RotateSpeed
    {
        get { return _RotateSpeed; }
        set
        {
            _RotateSpeed = value;
            RaisePropertyChanged(nameof(RotateSpeed));
        }
    }

    private int? _FlashInterval;
    [Category("动画")]
    [DisplayName("闪烁间隔")]
    [Description("控件闪烁的间隔，单位：毫秒")]
    public int? FlashInterval
    {
        get { return _FlashInterval; }
        set
        {
            _FlashInterval = value;
            RaisePropertyChanged(nameof(FlashInterval));
        }
    }

    public override void Execute(BaseControl controlModel)
    {
        var model = (Model)controlModel;
        if (!string.IsNullOrEmpty(Format))
            model.Format = Format;
        if (!string.IsNullOrEmpty(Foreground))
            model.Foreground = Foreground;
        if (!string.IsNullOrEmpty(Background))
            model.Background = Background;
        if (RotateSpeed != null)
            model.RotateSpeed = RotateSpeed.Value;
        if (FlashInterval != null)
            model.FlashInterval = FlashInterval.Value;
    }
}