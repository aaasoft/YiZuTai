using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using YiZuTai.Control;
using YiZuTai.Model;
using YiZuTai.Sample.Controls.Charts.Common;
using YiZuTai.Sample.Controls.Charts.Common.Model;

namespace YiZuTai.Sample.Controls.Charts.Column_Single;

[Category("图表")]
[DisplayName("柱状图_单柱")]
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
    public override ObservableCollection<PlotLine> YAxisPlotLines { get => base.YAxisPlotLines; set => base.YAxisPlotLines = value; }

    private string GetSeriesOption(SeriesDataItem seriesData, int index)
    {
        var colorIndex = 0;
        if (ChartColors.Count > 0)
            colorIndex = index % ChartColors.Count;
        var sb = new StringBuilder();
        sb.AppendLine($@"{{
        name: '{seriesData.Name}',
        data: [{{
            {(ChartColors.Count <= 0 ? string.Empty : $"color: '{ChartColors[colorIndex].Value}',")}
            y: null
        }}]
}}");
        return sb.ToString();
    }

    public override string GetSeriesOptions()
    {
        var sb = new StringBuilder();
            sb.AppendLine("[{");
            sb.AppendLine("  name: '',");
            sb.AppendLine("  data: [");
        var index = 0;
            sb.AppendLine(string.Join(',', SeriesData.Select(t => GetSeriesOption(t,index++))));
            sb.AppendLine("]}]");
            return sb.ToString();
    }
    
    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);
        Width = 300;
        Height = 240;
        XAxisType = XAxisType.category;
        TooltipPointFormat = "<b>值</b>: {point.y}";
        SeriesDataLabelsEnable = false;

        SeriesData.Add(new SeriesDataItem() { Name = "成都", DataSource = new DataSources.Simulate() { MinValue = 0, MaxValue = 10, Period = 1 } });
        SeriesData.Add(new SeriesDataItem() { Name = "上海", DataSource = new DataSources.Simulate() { MinValue = 20, MaxValue = 30, Period = 1 } });
        SeriesData.Add(new SeriesDataItem() { Name = "北京", DataSource = new DataSources.Simulate() { MinValue = 40, MaxValue = 50, Period = 1 } });
    }

    public override IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut("样式-浅色", ()=>
        {
            var mainColor = "#000000";
            TitleColor = mainColor;
            LegendItemColor = mainColor;
            XAxisLabelColor = mainColor;
            YAxisLabelColor = mainColor;
        }),
        new ControlShotcut("样式-深色", ()=>
        {
            var mainColor = "#FFFFFF";
            TitleColor = mainColor;
            LegendItemColor = mainColor;
            XAxisLabelColor = mainColor;
            YAxisLabelColor = mainColor;
        })
    ];
}