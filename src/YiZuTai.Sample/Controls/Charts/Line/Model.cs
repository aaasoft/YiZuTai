using System.ComponentModel;
using YiZuTai.Control;
using YiZuTai.Model;
using YiZuTai.Sample.Controls.Charts.Common;
using YiZuTai.Sample.Controls.Charts.Common.Model;

namespace YiZuTai.Sample.Controls.Charts.Line;

[Category("图表")]
[DisplayName("折线图")]
public class Model : AbstractChart
{
    protected override Type GetViewType() => typeof(View);

    public override string ChartType => ChartLineStyle.ToString();

    private LineStyle _ChartLineStyle = LineStyle.line;

    [Category("图表")]
    [DisplayName("图表样式")]
    [Description("图表风格样式")]
    public virtual LineStyle ChartLineStyle
    {
        get { return _ChartLineStyle; }
        set
        {
            _ChartLineStyle = value;
            RaisePropertyChanged(nameof(ChartLineStyle));
            RaisePropertyChanged(nameof(ChartType));
        }
    }

    private int _SeriesDataMaxCount = 100;
    [Category("数据列")]
    [DisplayName("数据列最大数据量")]
    [Description("数据列最大数据量")]
    public virtual int SeriesDataMaxCount
    {
        get { return _SeriesDataMaxCount; }
        set
        {
            _SeriesDataMaxCount = value;
            RaisePropertyChanged(nameof(SeriesDataMaxCount));
        }
    }

    private bool _EnableMaxValueDataLabel = false;
    [Category("其他")]
    [DisplayName("显示最大值")]
    public bool EnableMaxValueDataLabel
    {
        get { return _EnableMaxValueDataLabel; }
        set
        {
            _EnableMaxValueDataLabel = value;
            RaisePropertyChanged(nameof(EnableMaxValueDataLabel));
        }
    }

    private bool _EnableMinValueDataLabel = false;
    [Category("其他")]
    [DisplayName("显示最小值")]
    public bool EnableMinValueDataLabel
    {
        get { return _EnableMinValueDataLabel; }
        set
        {
            _EnableMinValueDataLabel = value;
            RaisePropertyChanged(nameof(EnableMinValueDataLabel));
        }
    }

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);
        Width = 300;
        Height = 240;
        XAxisType = Common.Model.XAxisType.datetime;
        TooltipPointFormat = "<b>{series.name}</b>: {point.y}";
        SeriesDataLabelsEnable = false;

        SeriesData.Add(new SeriesDataItem() { Name = "成都", DataSource = new DataSources.Simulate() { MinValue = 0, MaxValue = 10, Period = 1 } });
        SeriesData.Add(new SeriesDataItem() { Name = "上海", DataSource = new DataSources.Simulate() { MinValue = 20, MaxValue = 30, Period = 1 } });
        SeriesData.Add(new SeriesDataItem() { Name = "北京", DataSource = new DataSources.Simulate() { MinValue = 40, MaxValue = 50, Period = 1 } });
    }

    public static IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut("样式-浅色", obj =>
        {
            var t= (Model)obj;
            var mainColor = "#000000";
            t.TitleColor = mainColor;
            t.LegendItemColor = mainColor;
            t.XAxisLabelColor = mainColor;
            t.YAxisLabelColor = mainColor;
        }),
        new ControlShotcut("样式-深色", obj =>
        {
            var t= (Model)obj;
            var mainColor = "#FFFFFF";
            t.TitleColor = mainColor;
            t.LegendItemColor = mainColor;
            t.XAxisLabelColor = mainColor;
            t.YAxisLabelColor = mainColor;
        })
    ];
}
