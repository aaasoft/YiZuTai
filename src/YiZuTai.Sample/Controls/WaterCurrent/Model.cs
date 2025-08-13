using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.WaterCurrent;

[Category("动环")]
[DisplayName("漏水")]
public class Model : BaseControl
{
    protected override Type GetViewType() => typeof(View);

    private double _StartSeat;
    [Category("数据")]
    [DisplayName("起始位置")]
    public double StartSeat
    {
        get { return _StartSeat; }
        set
        {
            _StartSeat = value;
            RaisePropertyChanged(nameof(StartSeat));
        }
    }

    private double _EndSeat;
    [Category("数据")]
    [DisplayName("结束位置")]
    public double EndSeat
    {
        get { return _EndSeat; }
        set
        {
            _EndSeat = value;
            RaisePropertyChanged(nameof(EndSeat));
        }
    }

    private IDataSource _CurrentSeat;
    [Category("数据")]
    [DisplayName("漏水位置")]
    public IDataSource CurrentSeat
    {
        get { return _CurrentSeat; }
        set
        {
            RaisePropertyChanging(nameof(CurrentSeat));
            _CurrentSeat = value;
            RaisePropertyChanged(nameof(CurrentSeat));
        }
    }

    private string _Dur = "1s";
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

    private string _Stroke = "#00DD00";
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

    private double _StrokeOpacity = 1;
    [Category("笔画")]
    [DisplayName("透明度")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public virtual double StrokeOpacity
    {
        get { return _StrokeOpacity; }
        set
        {
            _StrokeOpacity = value;
            RaisePropertyChanged();
        }
    }



    [JsonIgnore]
    [Browsable(false)]
    public double LineStrokeWidh => Height / 2;

    [JsonIgnore]
    [Browsable(false)]
    public double LineX1 => 0;

    [JsonIgnore]
    [Browsable(false)]
    public double LineX2 => Width;

    [JsonIgnore]
    [Browsable(false)]
    public double LineY => Height / 2;

    [JsonIgnore]
    [Browsable(false)]
    public string LineStrokeDasharray => $"{LineStrokeDasharrayWidth * 3 / 7}, {LineStrokeDasharrayWidth * 4 / 7}";

    [JsonIgnore]
    [Browsable(false)]
    public double LineStrokeDasharrayWidth
    {
        get
        {
            var ret = Height + Height * 4 / 3;
            if (ret > Width / 3)
                ret = Width / 3;
            return ret;
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public bool IsAlarming
    {
        get
        {
            var isAlarming = false;
            double ret;
            if (double.TryParse(CurrentSeat?.Value, out ret))
            {
                if (ret > StartSeat && ret <= EndSeat)
                {
                    isAlarming = true;
                }
            }
            return isAlarming;
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public double AlarmRectX
    {
        get
        {
            double alarmRectX = 0;
            double ret;
            if (double.TryParse(CurrentSeat?.Value, out ret))
            {
                if (ret > StartSeat && ret <= EndSeat)
                {
                    alarmRectX = (Width - LineStrokeDasharrayWidth) * ((ret - StartSeat) / (EndSeat - StartSeat));
                }
            }
            return alarmRectX;
        }
    }


    private ObservableCollection<Trigger> _ValueTriggers = new ObservableCollection<Trigger>();
    [Category("数据源与触发器")]
    [DisplayName("值触发器")]
    public ObservableCollection<Trigger> ValueTriggers
    {
        get { return _ValueTriggers; }
        set
        {
            _ValueTriggers = value;
            RaisePropertyChanged(nameof(ValueTriggers));
        }
    }

    private ObservableCollection<DataSourceAndTrigger> _DataSourceAndTriggers = new ObservableCollection<DataSourceAndTrigger>();
    [Category("数据源与触发器")]
    [DisplayName("更多")]
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
        if (CurrentSeat != null && ValueTriggers.Count > 0)
            yield return new(CurrentSeat, ValueTriggers.ToArray());
        if (DataSourceAndTriggers != null)
            foreach (var item in DataSourceAndTriggers)
                yield return new(item.DataSource, item.Triggers.ToArray());
    }

    public override void OnDragDropCreate(Point location)
    {
        Width = 200;
        Height = 12;
        StartSeat = 0;
        EndSeat = 100;
        CurrentSeat = new DataSources.Simulate() { MaxValue = 100, MinValue = 0, Period = 1 };
    }
}
