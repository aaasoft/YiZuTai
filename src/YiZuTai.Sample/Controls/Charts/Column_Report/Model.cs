using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using YiZuTai.Control;
using YiZuTai.Model;
using YiZuTai.Sample.Controls.Charts.Common;
using YiZuTai.Sample.Controls.Charts.Common.Model;

namespace YiZuTai.Sample.Controls.Charts.Column_Report;

[Category("图表")]
[DisplayName("柱状图_报表")]
public class Model : AbstractChart
{
    protected override Type GetViewType() => typeof(View);
    public override string ChartType => "column";


    [JsonIgnore]
    [Browsable(false)]
    public override bool XAxisCrosshair { get => base.XAxisCrosshair; set => base.XAxisCrosshair = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override bool YAxisCrosshair { get => base.YAxisCrosshair; set => base.YAxisCrosshair = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override Step SeriesStep { get => base.SeriesStep; set => base.SeriesStep = value; }

    [JsonIgnore]
    [Browsable(false)]
    public override ObservableCollection<XAxisCategory> XAxisCategories
    {
        get
        {
            var collection = new ObservableCollection<XAxisCategory>();
            foreach (var item in SeriesData2)
            {
                collection.Add(new XAxisCategory() { Name = item.Name });
            }
            return collection;
        }
        set { }
    }
    [JsonIgnore]
    [Browsable(false)]
    public override ObservableCollection<PlotLine> YAxisPlotLines { get => base.YAxisPlotLines; set => base.YAxisPlotLines = value; }

    [JsonIgnore]
    [Browsable(false)]
    public override string SeriesName { get => base.SeriesName; set => base.SeriesName = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override ObservableCollection<SeriesDataItem> SeriesData { get => base.SeriesData; set => base.SeriesData = value; }

    private ObservableCollection<SeriesDataItem_WithQueryDataSource> _SeriesData2 = new ObservableCollection<SeriesDataItem_WithQueryDataSource>();
    [Category("数据列")]
    [DisplayName("数据列数据")]
    [Description("数据列的数据")]
    public virtual ObservableCollection<SeriesDataItem_WithQueryDataSource> SeriesData2
    {
        get { return _SeriesData2; }
        set
        {
            _SeriesData2 = value;
            RaisePropertyChanged(nameof(SeriesData2));
        }
    }

    
    private string GetSeriesOption(SeriesDataItem_WithQueryDataSource seriesData,int index)
    {
        var sb = new StringBuilder();
        sb.AppendLine($@"{{
    name: '{seriesData.Name}',
    data: []
}}");
        return sb.ToString();
    }

    public override string GetSeriesOptions()
    {
        var sb = new StringBuilder();
            sb.Append("[");
        var index = 0;
            sb.Append(string.Join(',', SeriesData2.Select(t => GetSeriesOption(t,index++))));
            sb.Append("]");
            return sb.ToString();
    }

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);
        Width = 300;
        Height = 240;
        XAxisType = XAxisType.category;
        TooltipPointFormat = "<b>{series.name}</b>: {point.y}";
        SeriesDataLabelsEnable = false;

        LegendEnable = true;
        LegendAlign = Align.right;
        LegendVerticalAlign = VerticalAlign.top;

        SeriesData2.Add(new SeriesDataItem_WithQueryDataSource()
        {
            Name = "成都",
            QueryDataSource = new QueryDataSources.Simulate()
            {
                MinValue = 0,
                MaxValue = 30,
                Period = 10,
                DecimalPlace = 2,
                Count = 10,
                DateTimeFormat = "M-d",
                DataTimeInterval = 24 * 60 * 60 * 1000
            }
        });
        SeriesData2.Add(new SeriesDataItem_WithQueryDataSource()
        {
            Name = "上海",
            QueryDataSource = new QueryDataSources.Simulate()
            {
                MinValue = 0,
                MaxValue = 30,
                Period = 10,
                DecimalPlace = 2,
                Count = 10,
                DateTimeFormat = "M-d",
                DataTimeInterval = 24 * 60 * 60 * 1000
            }
        });
        SeriesData2.Add(new SeriesDataItem_WithQueryDataSource()
        {
            Name = "北京",
            QueryDataSource = new QueryDataSources.Simulate()
            {
                MinValue = 0,
                MaxValue = 30,
                Period = 10,
                DecimalPlace = 2,
                Count = 10,
                DateTimeFormat = "M-d",
                DataTimeInterval = 24 * 60 * 60 * 1000
            }
        });
    }

    public static IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut<Model>("样式-浅色", t =>
        {
            var mainColor = "#000000";
            t.TitleColor = mainColor;
            t.LegendItemColor = mainColor;
            t.XAxisLabelColor = mainColor;
            t.YAxisLabelColor = mainColor;
        }),
        new ControlShotcut<Model>("样式-深色", t =>
        {
            var mainColor = "#FFFFFF";
            t.TitleColor = mainColor;
            t.LegendItemColor = mainColor;
            t.XAxisLabelColor = mainColor;
            t.YAxisLabelColor = mainColor;
        })
    ];
}
