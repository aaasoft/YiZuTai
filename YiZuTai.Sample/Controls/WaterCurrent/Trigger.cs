using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.WaterCurrent;

public class Trigger : BaseControlTrigger
{

    private double? _StartSeat;
    [Category("数据")]
    [DisplayName("起始位置")]
    public double? StartSeat
    {
        get { return _StartSeat; }
        set
        {
            _StartSeat = value;
            RaisePropertyChanged();
        }
    }

    private double? _EndSeat;
    [Category("数据")]
    [DisplayName("结束位置")]
    public double? EndSeat
    {
        get { return _EndSeat; }
        set
        {
            _EndSeat = value;
            RaisePropertyChanged();
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
            RaisePropertyChanged(nameof(Stroke));
        }
    }

    private double? _StrokeOpacity;
    [Category("笔画")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
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

    public override void Execute(BaseControl controlModel)
    {
        base.Execute(controlModel);

        var model = (Model)controlModel;
        if (StartSeat != null)
            model.StartSeat = StartSeat.Value;
        if (EndSeat != null)
            model.EndSeat = EndSeat.Value;
        if (!string.IsNullOrEmpty(Dur))
            model.Dur = Dur;
        if (!string.IsNullOrEmpty(Stroke))
            model.Stroke = Stroke;
        if (StrokeOpacity != null)
            model.StrokeOpacity = StrokeOpacity.Value;
    }
}

public class DataSourceAndTrigger : DataSourceAndTrigger<Trigger> { }