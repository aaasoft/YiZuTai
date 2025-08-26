using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.Model;
using YiZuTai.Sample.Controls.Charts.Common;
using YiZuTai.Sample.Controls.Charts.Common.Model;

namespace YiZuTai.Sample.Controls.Charts.Pie;

[Category("图表")]
[DisplayName("饼图")]
public class Model : AbstractChart
{
    protected override Type GetViewType() => typeof(View);
    public override string ChartType => "pie";

    [JsonIgnore]
    [Browsable(false)]
    public override bool TooltipSplit { get => base.TooltipSplit; set => base.TooltipSplit = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override bool XAxisCrosshair { get => base.XAxisCrosshair; set => base.XAxisCrosshair = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override bool XAxisVisible { get => base.XAxisVisible; set => base.XAxisVisible = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override XAxisType XAxisType { get => base.XAxisType; set => base.XAxisType = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override string XAxisLabelColor { get => base.XAxisLabelColor; set => base.XAxisLabelColor = value; }

    [JsonIgnore]
    [Browsable(false)]
    public override bool YAxisCrosshair { get => base.YAxisCrosshair; set => base.YAxisCrosshair = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override bool YAxisVisible { get => base.YAxisVisible; set => base.YAxisVisible = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override string YAxisTitleText { get => base.YAxisTitleText; set => base.YAxisTitleText = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override bool YAxisAllowDecimals { get => base.YAxisAllowDecimals; set => base.YAxisAllowDecimals = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override string YAxisLabelColor { get => base.YAxisLabelColor; set => base.YAxisLabelColor = value; }

    [JsonIgnore]
    [Browsable(false)]
    public override Step SeriesStep { get => base.SeriesStep; set => base.SeriesStep = value; }

    private double _InnerSize = 0;
    [Category("图形")]
    [DisplayName("圆环比例")]
    [Description("饼图圆环的比例，0~100，为0时为一个完整的饼，大于0时为圆环")]
    public double InnerSize
    {
        get { return _InnerSize; }
        set
        {
            _InnerSize = value;
            RaisePropertyChanged(nameof(InnerSize));
        }
    }

    private double _StartAngle = 0;
    [Category("图形")]
    [DisplayName("起始角度")]
    [Description("起始角度")]
    public double StartAngle
    {
        get { return _StartAngle; }
        set
        {
            _StartAngle = value;
            RaisePropertyChanged(nameof(StartAngle));
        }
    }

    private double? _EndAngle;
    [Category("图形")]
    [DisplayName("结束角度")]
    [Description("结束角度")]
    public double? EndAngle
    {
        get { return _EndAngle; }
        set
        {
            _EndAngle = value;
            RaisePropertyChanged(nameof(EndAngle));
        }
    }

    private double _PieBorderWidth = 1;
    [Category("图形")]
    [DisplayName("边框宽度")]
    [Description("图形的边框宽度")]
    public double PieBorderWidth
    {
        get { return _PieBorderWidth; }
        set
        {
            if (value < 0)
                return;
            _PieBorderWidth = value;
            RaisePropertyChanged(nameof(PieBorderWidth));
        }
    }

    private string _PieBorderColor = "#FFFFFF";
    [Category("图形")]
    [DisplayName("边框颜色")]
    [Description("图形的边框颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string PieBorderColor
    {
        get { return _PieBorderColor; }
        set
        {
            _PieBorderColor = value;
            RaisePropertyChanged(nameof(PieBorderColor));
        }
    }

    protected override string GetPlotOptionsExtra()
    {
        return $@",
    pie: {{
        startAngle: {StartAngle},
{(EndAngle == null ? string.Empty : $"endAngle: {EndAngle},")}
        innerSize: '{InnerSize}%',
        borderWidth: {PieBorderWidth},
        borderColor: '{PieBorderColor}',
        showInLegend: {LegendEnable.ToString().ToLower()}
    }}";
    }


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
            y: null,
            z: {Width * 0.7 * (1 + index / 10)}
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
        sb.AppendLine(string.Join(',', SeriesData.Select(t => GetSeriesOption(t, index++))));
        sb.AppendLine("]}]");
        return sb.ToString();
    }

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);
        Width = 300;
        Height = 240;
        LegendEnable = true;
        SeriesDataLabelsEnable = false;
        TooltipPointFormat = "值: {point.y}";
        SeriesAllowPointSelect = true;
        SeriesDataLabelsFormat = "<b>{point.name}</b>: {point.y}";

        SeriesData.Add(new SeriesDataItem() { Name = "成都", DataSource = new DataSources.Input() { Content = "1" } });
        SeriesData.Add(new SeriesDataItem() { Name = "上海", DataSource = new DataSources.Input() { Content = "2" } });
        SeriesData.Add(new SeriesDataItem() { Name = "北京", DataSource = new DataSources.Input() { Content = "3" } });
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