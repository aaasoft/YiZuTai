using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.Curve;


public class Trigger : BaseControl_WithBackgroundTrigger
{
    private string _Stroke;
    [Category("笔画")]
    [DisplayName("颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string Stroke
    {
        get { return _Stroke; }
        set
        {
            _Stroke = value;
            RaisePropertyChanged();
        }
    }

    private double? _StrokeOpacity;
    [Category("笔画")]
    [DisplayName("颜色透明")]
    [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public double? StrokeOpacity
    {
        get { return _StrokeOpacity; }
        set
        {
            _StrokeOpacity = value;
            RaisePropertyChanged();
        }
    }

    private double? _StrokeWidth;
    [Category("笔画")]
    [DisplayName("宽度")]
    public double? StrokeWidth
    {
        get { return _StrokeWidth; }
        set
        {
            _StrokeWidth = value;
        }
    }


    private string _Fill;
    [Category("填充")]
    [DisplayName("颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public string Fill
    {
        get { return _Fill; }
        set
        {
            _Fill = value;
            RaisePropertyChanged(nameof(Fill));
        }
    }

    private double? _FillOpacity;
    [Category("填充")]
    [DisplayName("颜色透明")]
    [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
    [Range(0, 1)]
    [StringLength(100)]
    public double? FillOpacity
    {
        get { return _FillOpacity; }
        set
        {
            _FillOpacity = value;
            RaisePropertyChanged();
        }
    }

    private bool? _EnableAnimation;
    [Category("动画")]
    [DisplayName("启用")]
    [Description("是否启用动画")]
    public bool? EnableAnimation
    {
        get { return _EnableAnimation; }
        set
        {
            _EnableAnimation = value;
            RaisePropertyChanged(nameof(EnableAnimation));
        }
    }
    private bool? _AnimationReverse;
    [Category("动画")]
    [DisplayName("反向")]
    [Description("是否反向动画")]
    public bool? AnimationReverse
    {
        get { return _AnimationReverse; }
        set
        {
            _AnimationReverse = value;
            RaisePropertyChanged(nameof(AnimationReverse));
        }
    }

    private bool? _AnimationToAndBack;
    [Category("动画")]
    [DisplayName("往返")]
    [Description("是否往返动画")]
    public bool? AnimationToAndBack
    {
        get { return _AnimationToAndBack; }
        set
        {
            _AnimationToAndBack = value;
            RaisePropertyChanged(nameof(AnimationToAndBack));
        }
    }

    private string _Dur;
    [Category("动画")]
    [DisplayName("时长")]
    [Description("动画的时长。指定简单持续时间的时长。值必须大于 0。可以用小时（h）、分钟（m）、秒（s）、毫秒（ms）表达这个值。可以组合这些时间表达式以提供一个复合的持续时间，比如这样：hh:mm:ss.iii或者这样：mm:ss.iii。")]
    public string Dur
    {
        get { return _Dur; }
        set
        {
            _Dur = value;
            RaisePropertyChanged(nameof(Dur));
        }
    }

    private string _Image;
    [Category("动画")]
    [DisplayName("图片")]
    [Description("动画中要显示的图片")]
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

    private double? _ImageWidth;
    [Category("动画")]
    [DisplayName("图片宽度")]
    [Description("图片的宽度")]
    public double? ImageWidth
    {
        get { return _ImageWidth; }
        set
        {
            _ImageWidth = value;
            RaisePropertyChanged();
        }
    }

    private double? _ImageHeight;
    [Category("动画")]
    [DisplayName("图片高度")]
    [Description("图片的高度")]
    public double? ImageHeight
    {
        get { return _ImageHeight; }
        set
        {
            _ImageHeight = value;
            RaisePropertyChanged();
        }
    }

    public override void Execute(BaseControl controlModel)
    {
        var model = (Model)controlModel;
        base.Execute(controlModel);

        if (Stroke != null)
            model.Stroke = Stroke;
        if (StrokeOpacity != null)
            model.StrokeOpacity = StrokeOpacity.Value;
        if (StrokeWidth != null)
            model.StrokeWidth = StrokeWidth.Value;
        if (Fill != null)
            model.Fill = Fill;
        if (FillOpacity != null)
            model.FillOpacity = FillOpacity.Value;
        if (EnableAnimation != null)
            model.EnableAnimation = EnableAnimation.Value;
        if (AnimationReverse != null)
            model.AnimationReverse = AnimationReverse.Value;
        if (AnimationToAndBack != null)
            model.AnimationToAndBack = AnimationToAndBack.Value;
        if (Dur != null)
            model.Dur = Dur;
        if (Image != null)
            model.Image = Image;
        if (ImageWidth != null)
            model.ImageWidth = ImageWidth.Value;
        if (ImageHeight != null)
            model.ImageHeight = ImageHeight.Value;
    }
}

public class DataSourceAndTrigger : DataSourceAndTrigger<Trigger> { }