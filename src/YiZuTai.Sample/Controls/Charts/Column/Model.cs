using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.Control;
using YiZuTai.Model;
using YiZuTai.Sample.Controls.Charts.Common;
using YiZuTai.Sample.Controls.Charts.Common.Model;

namespace YiZuTai.Sample.Controls.Charts.Column;

[Category("图表")]
[DisplayName("柱状图")]
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
    public override string SeriesName { get => base.SeriesName; set => base.SeriesName = value; }
    [JsonIgnore]
    [Browsable(false)]
    public override ObservableCollection<SeriesDataItem> SeriesData { get => base.SeriesData; set => base.SeriesData = value; }

    private ObservableCollection<SeriesDataCollectionItem> _SeriesData2 = new ObservableCollection<SeriesDataCollectionItem>();
    [Category("数据列")]
    [DisplayName("数据列数据")]
    [Description("数据列的数据")]
    public virtual ObservableCollection<SeriesDataCollectionItem> SeriesData2
    {
        get { return _SeriesData2; }
        set
        {
            _SeriesData2 = value;
            RaisePropertyChanged(nameof(SeriesData2));
        }
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

        XAxisCategories.Add(new XAxisCategory() { Name = "成都" });
        XAxisCategories.Add(new XAxisCategory() { Name = "上海" });
        XAxisCategories.Add(new XAxisCategory() { Name = "北京" });

        SeriesData2.Add(new SeriesDataCollectionItem()
        {
            Name = "温度",
            Items = new ObservableCollection<SeriesDataSourceItem>()
                {
                    new SeriesDataSourceItem()
                    {
                        DataSource = new DataSources.Simulate()
                        {
                            MinValue = 0,
                            MaxValue = 30,
                            Period = 5
                        }
                    },
                    new SeriesDataSourceItem()
                    {
                        DataSource = new DataSources.Simulate()
                        {
                            MinValue = 0,
                            MaxValue = 30,
                            Period = 5
                        }
                    },
                    new SeriesDataSourceItem()
                    {
                        DataSource = new DataSources.Simulate()
                        {
                            MinValue = 0,
                            MaxValue = 30,
                            Period = 5
                        }
                    }
                }
        });

        SeriesData2.Add(new SeriesDataCollectionItem()
        {
            Name = "湿度",
            Items = new ObservableCollection<SeriesDataSourceItem>()
                {
                    new SeriesDataSourceItem()
                    {
                        DataSource = new DataSources.Simulate()
                        {
                            MinValue = 0,
                            MaxValue = 100,
                            Period = 5
                        }
                    },
                    new SeriesDataSourceItem()
                    {
                        DataSource = new DataSources.Simulate()
                        {
                            MinValue = 0,
                            MaxValue = 100,
                            Period = 5
                        }
                    },
                    new SeriesDataSourceItem()
                    {
                        DataSource = new DataSources.Simulate()
                        {
                            MinValue = 0,
                            MaxValue = 100,
                            Period = 5
                        }
                    }
                }
        });
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