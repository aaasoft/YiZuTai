using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using YiZuTai.Control;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.Progress.Ring;

[Category("进度")]
[DisplayName("圆环")]
public class Model : AbstractControl
{
    protected override Type GetViewType() => typeof(View);

    private string _InnerBackground = "#FFFFFF";
    [Category("内部")]
    [DisplayName("背景颜色")]
    public virtual string InnerBackground
    {
        get { return _InnerBackground; }
        set
        {
            _InnerBackground = value;
            RaisePropertyChanged();
        }
    }

    private double _InnerBackgroundOpacity = 0;
    [Category("内部")]
    [DisplayName("背景透明度")]
    [Range(0, 1)]
    [StringLength(100)]
    public double InnerBackgroundOpacity
    {
        get { return _InnerBackgroundOpacity; }
        set
        {
            _InnerBackgroundOpacity = value;
            RaisePropertyChanged();
        }
    }

    private double _InnerSize = 80;
    [Category("圆环")]
    [DisplayName("比例")]
    [Description("圆环的比例，0~100，为0时为一个完整的饼，大于0时为圆环")]
    public double InnerSize
    {
        get { return _InnerSize; }
        set
        {
            _InnerSize = value;
            RaisePropertyChanged();
        }
    }

    private double _StartAngle = 0;
    [Category("圆环")]
    [DisplayName("起始角度")]
    public double StartAngle
    {
        get { return _StartAngle; }
        set
        {
            _StartAngle = value;
            RaisePropertyChanged();
        }
    }

    [Browsable(false)]
    [JsonIgnore]
    public double BorderWidth => Math.Min(CenterX, CenterY) * (100 - InnerSize) / 100;
    [Browsable(false)]
    [JsonIgnore]
    public double R => Math.Min(CenterX, Height) - BorderWidth;
    
    public string GetStrokeDasharray(double value)
    {
        var percent = value / (MaxValue - MinValue);
        var perimeter = Math.PI * 2 * R;
        return $"{perimeter * percent} {perimeter * (1 - percent)}";
    }

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);

        Border = "#000000";
        BorderThickness = 0;
        Width = 120;
        Height = 120;
    }
    
    public override IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut("样式-浅色", ()=>
        {
            TextForeground = "#000000";
        }),
        new ControlShotcut("样式-深色", ()=>
        {
            TextForeground = "#FFFFFF";
        })
    ];
}