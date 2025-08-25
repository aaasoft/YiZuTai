using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using YiZuTai.Components.Editors;
using YiZuTai.Sample.Controls.Charts.Common.Model;

namespace YiZuTai.Sample.Controls.Charts.Common
{
    public partial class AbstractChart
    {
        private bool _ChartShadow = false;
        [Category("图表")]
        [DisplayName("显示阴影")]
        [Description("在整个图表上是否显示阴影")]
        public bool ChartShadow
        {
            get { return _ChartShadow; }
            set
            {
                _ChartShadow = value;
                RaisePropertyChanged(nameof(ChartShadow));
            }
        }

        private double _ChartPlotBorderWidth;
        [Category("绘图区域")]
        [DisplayName("边框宽度")]
        [Description("绘图区域的边框宽度")]
        public double ChartPlotBorderWidth
        {
            get { return _ChartPlotBorderWidth; }
            set
            {
                if (value < 0)
                    return;
                _ChartPlotBorderWidth = value;
                RaisePropertyChanged(nameof(ChartPlotBorderWidth));
            }
        }

        private string _ChartPlotBackground = "#FFFFFF";
        [Category("绘图区域")]
        [DisplayName("背景颜色")]
        [Description("绘图区域的背景颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public string ChartPlotBackground
        {
            get { return _ChartPlotBackground; }
            set
            {
                _ChartPlotBackground = value;
                RaisePropertyChanged(nameof(ChartPlotBackground));
            }
        }

        private double _ChartPlotBackgroundOpacity = 0;

        [Category("绘图区域")]
        [DisplayName("背景透明度")]
        [Description("绘图区域的背景透明度")]
        [Range(0, 1)]
        [StringLength(100)]
        public double ChartPlotBackgroundOpacity
        {
            get { return _ChartPlotBackgroundOpacity; }
            set
            {
                _ChartPlotBackgroundOpacity = value;
                RaisePropertyChanged();
            }
        }

        private string _ChartPlotBorder = "#CCCCCC";
        [Category("绘图区域")]
        [DisplayName("边框颜色")]
        [Description("绘图区域的边框颜色")]
        public string ChartPlotBorder
        {
            get { return _ChartPlotBorder; }
            set
            {
                _ChartPlotBorder = value;
                RaisePropertyChanged(nameof(ChartPlotBorder));
            }
        }

        private double _ChartPlotBorderOpacity = 1;

        [Category("绘图区域")]
        [DisplayName("边框透明度")]
        [Description("绘图区域的边框透明度")]
        [Range(0, 1)]
        [StringLength(100)]
        public double ChartPlotBorderOpacity
        {
            get { return _ChartPlotBorderOpacity; }
            set
            {
                _ChartPlotBorderOpacity = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ColorItem> _ChartColors = new ObservableCollection<ColorItem>();
        [Category("图表")]
        [DisplayName("图表颜色")]
        [Description("图表显示的颜色序列")]
        public virtual ObservableCollection<ColorItem> ChartColors
        {
            get { return _ChartColors; }
            set
            {
                _ChartColors = value;
                RaisePropertyChanged(nameof(ChartColors));
            }
        }

        public string GetCreditsOptions()
        {
            return "{ enabled : false}";
        }

        public string GetChartOptions()
        {
            return $@"{{
    type: '{ChartType}',
    width: {Width},
    height: {Height},
    animation: Highcharts.svg,
    shadow: {ChartShadow.ToString().ToLower()},
    plotBackgroundColor: '{ChartPlotBackground}',
    plotBorderColor: '{ChartPlotBorder}',
    plotBorderWidth: '{ChartPlotBorderWidth}',
}}";
        }
        private static string defaultColors = "['#7cb5ec', '#434348', '#90ed7d', '#f7a35c', '#8085e9', '#f15c80', '#e4d354', '#2b908f', '#f45b5b', '#91e8e1']";

        public string GetColorsOptions()
        {
            if (ChartColors == null || ChartColors.Count == 0)
                return defaultColors;

            return "[" + string.Join(',', ChartColors.Select(t => $"'{t.Value}'")) + "]";
        }
    }
}
