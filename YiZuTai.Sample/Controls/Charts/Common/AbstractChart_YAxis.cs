using Newtonsoft.Json;
using YiZuTai.Sample.Controls.Charts.Common.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Nodes;
using YiZuTai.Components.Editors;

namespace YiZuTai.Sample.Controls.Charts.Common
{
    public partial class AbstractChart
    {
        private string _YAxisTitleText;
        [Category("Y轴")]
        [DisplayName("标题文字")]
        [Description("Y轴的标题文字")]
        public virtual string YAxisTitleText
        {
            get { return _YAxisTitleText; }
            set
            {
                _YAxisTitleText = value;
                RaisePropertyChanged(nameof(YAxisTitleText));
            }
        }

        private bool _YAxisAllowDecimals = true;
        [Category("Y轴")]
        [DisplayName("允许小数")]
        [Description("是否在坐标轴标签中显示小数")]
        public virtual bool YAxisAllowDecimals
        {
            get { return _YAxisAllowDecimals; }
            set
            {
                _YAxisAllowDecimals = value;
                RaisePropertyChanged(nameof(YAxisAllowDecimals));
            }
        }

        private bool _YAxisCrosshair = false;
        [Category("Y轴")]
        [DisplayName("十字准星")]
        [Description("是否启用Y轴上的十字准星")]
        public virtual bool YAxisCrosshair
        {
            get { return _YAxisCrosshair; }
            set
            {
                _YAxisCrosshair = value;
                RaisePropertyChanged(nameof(YAxisCrosshair));
            }
        }

        private bool _YAxisVisible = true;
        [Category("Y轴")]
        [DisplayName("是否可见")]
        [Description("是否显示坐标轴")]
        public virtual bool YAxisVisible
        {
            get { return _YAxisVisible; }
            set
            {
                _YAxisVisible = value;
                RaisePropertyChanged(nameof(YAxisVisible));
            }
        }

        private string _YAxisLabelFontFamily;
        [Category("Y轴")]
        [DisplayName("字体")]
        [Description("Y轴的字体")]
        public string YAxisLabelFontFamily
        {
            get { return _YAxisLabelFontFamily; }
            set
            {
                _YAxisLabelFontFamily = value;
                RaisePropertyChanged(nameof(YAxisLabelFontFamily));
            }
        }

        private double _YAxisLabelFontSize = 11;
        [Category("Y轴")]
        [DisplayName("字号")]
        [Description("Y轴的字号")]
        public double YAxisLabelFontSize
        {
            get { return _YAxisLabelFontSize; }
            set
            {
                _YAxisLabelFontSize = value;
                RaisePropertyChanged(nameof(YAxisLabelFontSize));
            }
        }

        private string _YAxisLabelColor = "#000000";
        [Category("Y轴")]
        [DisplayName("标签颜色")]
        [Description("标签的颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string YAxisLabelColor
        {
            get { return _YAxisLabelColor; }
            set
            {
                _YAxisLabelColor = value;
                RaisePropertyChanged(nameof(YAxisLabelColor));
            }
        }

        private double _YAxisGridLineWidth = 1;
        [Category("Y轴")]
        [DisplayName("网格线宽度")]
        public virtual double YAxisGridLineWidth
        {
            get { return _YAxisGridLineWidth; }
            set
            {
                _YAxisGridLineWidth = value;
                RaisePropertyChanged(nameof(YAxisGridLineWidth));
            }
        }

        private string _YAxisGridLineColor = "#E6E6E6";
        [Category("Y轴")]
        [DisplayName("网格线颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string YAxisGridLineColor
        {
            get { return _YAxisGridLineColor; }
            set
            {
                _YAxisGridLineColor = value;
                RaisePropertyChanged(nameof(YAxisGridLineColor));
            }
        }

        private DashStyle _YAxisGridLineDashStyle = DashStyle.Solid;
        [Category("Y轴")]
        [DisplayName("网格线样式")]
        public virtual DashStyle YAxisGridLineDashStyle
        {
            get { return _YAxisGridLineDashStyle; }
            set
            {
                _YAxisGridLineDashStyle = value;
                RaisePropertyChanged(nameof(YAxisGridLineDashStyle));
            }
        }

        private ObservableCollection<PlotLine> _YAxisPlotLines = new ObservableCollection<PlotLine>();
        [Category("Y轴")]
        [DisplayName("标志线集合")]
        public virtual ObservableCollection<PlotLine> YAxisPlotLines
        {
            get { return _YAxisPlotLines; }
            set
            {
                _YAxisPlotLines = value;
                RaisePropertyChanged(nameof(YAxisPlotLines));
            }
        }

        private string GetPlotLineString(PlotLine plotLine)
        {
            return $@"{{
    color: '{plotLine.LineColor}',
    width: {plotLine.LineWidth},
    dashStyle: '{plotLine.DashStyle}'
}}";
        }
        public string GetYAxisOptions()
        {
            return $@"{{
                title: '{YAxisTitleText}',
                allowDecimals: {YAxisAllowDecimals.ToString().ToLower()},
                {(YAxisPlotLines == null || YAxisPlotLines.Count == 0 ? string.Empty : $"plotLines: [{string.Join(',', YAxisPlotLines.Select(t => GetPlotLineString(t)))}],")}
                crosshair: {YAxisCrosshair.ToString().ToLower()},
                visible: {YAxisVisible.ToString().ToLower()},
                gridLineWidth: '{YAxisGridLineWidth}',
                gridLineColor: '{YAxisGridLineColor}',
                gridLineDashStyle: '{YAxisGridLineDashStyle}',
                labels: {{
                    style: {{
                        color: '{YAxisLabelColor}',
                        fontSize: '{YAxisLabelFontSize}px',
                        fontFamily: '{YAxisLabelFontFamily}'
                    }}
                }}
            }}";
        }
    }
}
