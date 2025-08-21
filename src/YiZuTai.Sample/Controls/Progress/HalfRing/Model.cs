using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using YiZuTai.Control;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.Progress.HalfRing;

[Category("进度")]
[DisplayName("半圆环")]
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
            RaisePropertyChanged(nameof(InnerSize));
        }
    }

    [Browsable(false)]
    [JsonIgnore]
    public double NumberValue
    {
        get
        {
            double value = MinValue;
            double.TryParse(Value?.Value, out value);
            if (value > MaxValue)
                value = MaxValue;
            if (value < MinValue)
                value = MinValue;
            return value;
        }
    }
    [Browsable(false)]
    [JsonIgnore]
    public double CenterX => Width / 2;
    [Browsable(false)]
    [JsonIgnore]
    public double CenterY
    {
        get
        {
            var sourceR = Math.Min(CenterX, Height);
            return (Height - sourceR) / 2 + sourceR;
        }
    }
    [Browsable(false)]
    [JsonIgnore]
    public double TextX => CenterX;
    [Browsable(false)]
    [JsonIgnore]
    public double TextY => CenterY;
    [Browsable(false)]
    [JsonIgnore]
    public double BorderWidth => Math.Min(CenterX, CenterY) * (100 - InnerSize) / 100;
    [Browsable(false)]
    [JsonIgnore]
    public double R => Math.Min(CenterX, Height) - BorderWidth;
    [Browsable(false)]
    [JsonIgnore]
    public string StrokeDasharray
    {
        get
        {
            var percent = (NumberValue - MinValue) / (MaxValue - MinValue);
            var perimeter = Math.PI * (R + BorderWidth / 2);
            return $"{perimeter * percent} {perimeter * (1 - percent)}";
        }
    }
    [Browsable(false)]
    [JsonIgnore]
    public string Text => TextFormat.Replace("{value}", Value?.Value ?? "");


    [Browsable(false)]
    [JsonIgnore]
    public double HalfRingStartX => CenterX - R - (BorderWidth / 2);
    [Browsable(false)]
    [JsonIgnore]
    public double HalfRingStartY => CenterY;
    [Browsable(false)]
    [JsonIgnore]
    public double HalfRingEndX => CenterX + R + BorderWidth / 2;
    [Browsable(false)]
    [JsonIgnore]
    public double HalfRingEndY => CenterY;

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);

        Border = "#000000";
        BorderThickness = 0;
        Width = 120;
        Height = 60;
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