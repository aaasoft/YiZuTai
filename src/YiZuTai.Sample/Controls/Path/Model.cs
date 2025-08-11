using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.Path;

[Category("图形")]
[DisplayName("路径")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);


    private string _D = "M 10 80 C 40 10, 65 10, 95 80 S 150 150, 180 80z";
    [Category("笔画")]
    [DisplayName("形状")]
    [Description("SVG路径形状，参考文档：https://developer.mozilla.org/zh-CN/docs/Web/SVG/Tutorial/Paths")]
    public virtual string D
    {
        get { return _D; }
        set
        {
            _D = value;
            RaisePropertyChanged(nameof(D));
        }
    }

    private string _Stroke = "#B0E0E6";
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

    private double _StrokeOpacity = 1;
    [Category("笔画")]
    [DisplayName("颜色透明")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public double StrokeOpacity
    {
        get { return _StrokeOpacity; }
        set
        {
            _StrokeOpacity = value;
            RaisePropertyChanged();
        }
    }

    private double _StrokeWidth = 1;
    [Category("笔画")]
    [DisplayName("宽度")]
    public virtual double StrokeWidth
    {
        get { return _StrokeWidth; }
        set
        {
            if (value < 1)
                return;
            _StrokeWidth = value;
            RaisePropertyChanged(nameof(StrokeWidth));
        }
    }


    private string _Fill = "#000000";
    [Category("填充")]
    [DisplayName("颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string Fill
    {
        get { return _Fill; }
        set
        {
            _Fill = value;
            RaisePropertyChanged(nameof(Fill));
        }
    }

    private double _FillOpacity = 0;
    [Category("填充")]
    [DisplayName("颜色透明")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public double FillOpacity
    {
        get { return _FillOpacity; }
        set
        {
            _FillOpacity = value;
            RaisePropertyChanged();
        }
    }

    private bool _EnableAnimation = false;
    [Category("动画")]
    [DisplayName("启用")]
    [Description("是否启用动画")]
    public bool EnableAnimation
    {
        get { return _EnableAnimation; }
        set
        {
            _EnableAnimation = value;
            RaisePropertyChanged(nameof(EnableAnimation));
        }
    }

    private string _Dur = "3s";
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

    private double _ImageWidth = 20;
    [Category("动画")]
    [DisplayName("图片宽度")]
    [Description("图片的宽度")]
    public double ImageWidth
    {
        get { return _ImageWidth; }
        set
        {
            if (value < 1)
                return;
            _ImageWidth = value;
            RaisePropertyChanged(nameof(ImageWidth));
        }
    }

    private double _ImageHeight = 20;
    [Category("动画")]
    [DisplayName("图片高度")]
    [Description("图片的高度")]
    public double ImageHeight
    {
        get { return _ImageHeight; }
        set
        {
            if (value < 1)
                return;
            _ImageHeight = value;
            RaisePropertyChanged(nameof(ImageHeight));
        }
    }

    public override void OnDragDropCreate(Point location)
    {
        Width = 200;
        Height = 200;
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

    protected override IEnumerable<Tuple<IDataSource, AbstractTrigger[]>> GetDataSourceAndTriggers()
    {
        if (DataSourceAndTriggers != null)
            foreach (var item in DataSourceAndTriggers)
                yield return new(item.DataSource, item.Triggers.ToArray());
    }
}
