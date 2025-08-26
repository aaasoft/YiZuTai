using Newtonsoft.Json;
using YiZuTai.Sample.Controls.Charts.Common.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using YiZuTai.Components.Editors;
using System.Text;

namespace YiZuTai.Sample.Controls.Charts.Common
{
    public partial class AbstractChart
    {
        private string _SeriesName;
        [Category("数据列")]
        [DisplayName("数据列名称")]
        [Description("数据列的名称")]
        public virtual string SeriesName
        {
            get { return _SeriesName; }
            set
            {
                _SeriesName = value;
                RaisePropertyChanged(nameof(SeriesName));
            }
        }

        private bool _SeriesShadow = false;
        [Category("数据列")]
        [DisplayName("显示阴影")]
        [Description("在图形线条上是否显示阴影")]
        public virtual bool SeriesShadow
        {
            get { return _SeriesShadow; }
            set
            {
                _SeriesShadow = value;
                RaisePropertyChanged(nameof(SeriesShadow));
            }
        }

        private bool _SeriesAllowPointSelect = false;
        [Category("数据列")]
        [DisplayName("点位选中")]
        [Description("是否允许在点击数据点标记（markers）、柱子（柱形图）、扇区（饼图）时选中该点。")]
        public virtual bool SeriesAllowPointSelect
        {
            get { return _SeriesAllowPointSelect; }
            set
            {
                _SeriesAllowPointSelect = value;
                RaisePropertyChanged(nameof(SeriesAllowPointSelect));
            }
        }

        private DashStyle _SeriesDashStyle = DashStyle.Solid;
        [Category("数据列")]
        [DisplayName("线条样式")]
        [Description("线条的样式")]
        public virtual DashStyle SeriesDashStyle
        {
            get { return _SeriesDashStyle; }
            set
            {
                _SeriesDashStyle = value;
                RaisePropertyChanged(nameof(SeriesDashStyle));
            }
        }

        private double _SeriesLineWidth = 2;
        [Category("数据列")]
        [DisplayName("线条宽度")]
        [Description("线条的宽度(折线图使用)")]
        public virtual double SeriesLineWidth
        {
            get { return _SeriesLineWidth; }
            set
            {
                if (value < 1)
                    return;
                _SeriesLineWidth = value;
                RaisePropertyChanged(nameof(SeriesLineWidth));
            }
        }
        private double _SeriesBorderWidth = 0;
        [Category("数据列")]
        [DisplayName("边框宽度")]
        [Description("边框的宽度(柱状图使用)")]
        public virtual double SeriesBorderWidth
        {
            get { return _SeriesBorderWidth; }
            set
            {
                if (value < 0)
                    return;
                _SeriesBorderWidth = value;
                RaisePropertyChanged(nameof(SeriesBorderWidth));
            }
        }

        private string _SeriesBorderColor = "#FFFFFF";
        [Category("数据列")]
        [DisplayName("边框颜色")]
        [Description("边框的宽度(柱状图使用)")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string SeriesBorderColor
        {
            get { return _SeriesBorderColor; }
            set
            {
                _SeriesBorderColor = value;
                RaisePropertyChanged(nameof(SeriesBorderColor));
            }
        }

        private Step _SeriesStep;
        [Category("数据列")]
        [DisplayName("阶梯样式")]
        [Description("线条的阶梯样式")]
        public virtual Step SeriesStep
        {
            get { return _SeriesStep; }
            set
            {
                _SeriesStep = value;
                RaisePropertyChanged(nameof(SeriesStep));
            }
        }

        private bool _SeriesDataLabelsInside = false;
        [Category("数据标签")]
        [DisplayName("内部")]
        public bool SeriesDataLabelsInside
        {
            get { return _SeriesDataLabelsInside; }
            set
            {
                _SeriesDataLabelsInside = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsInside));
            }
        }

        private double _SeriesDataLabelsRotation = 0;
        [Category("数据标签")]
        [DisplayName("旋转角度")]
        public double SeriesDataLabelsRotation
        {
            get { return _SeriesDataLabelsRotation; }
            set
            {
                _SeriesDataLabelsRotation = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsRotation));
            }
        }

        private Align _SeriesDataLabelsAlign = Align.center;
        [Category("数据标签")]
        [DisplayName("水平对齐")]
        public Align SeriesDataLabelsAlign
        {
            get { return _SeriesDataLabelsAlign; }
            set
            {
                _SeriesDataLabelsAlign = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsAlign));
            }
        }

        private VerticalAlign _SeriesDataLabelsVerticalAlign = VerticalAlign.bottom;
        [Category("数据标签")]
        [DisplayName("垂直对齐")]
        public VerticalAlign SeriesDataLabelsVerticalAlign
        {
            get { return _SeriesDataLabelsVerticalAlign; }
            set
            {
                _SeriesDataLabelsVerticalAlign = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsVerticalAlign));
            }
        }


        private bool _SeriesDataLabelsEnable = true;
        [Category("数据标签")]
        [DisplayName("是否启用")]
        [Description("是否启用数据标签")]
        public virtual bool SeriesDataLabelsEnable
        {
            get { return _SeriesDataLabelsEnable; }
            set
            {
                _SeriesDataLabelsEnable = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsEnable));
            }
        }

        private string _SeriesDataLabelsFormat = "{y}";
        [Category("数据标签")]
        [DisplayName("文字格式")]
        [Description(Consts.FormatDescription)]
        public virtual string SeriesDataLabelsFormat
        {
            get { return _SeriesDataLabelsFormat; }
            set
            {
                _SeriesDataLabelsFormat = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsFormat));
            }
        }

        private string _SeriesDataLabelsFontFamily;
        [Category("数据标签")]
        [DisplayName("字体")]
        public string SeriesDataLabelsFontFamily
        {
            get { return _SeriesDataLabelsFontFamily; }
            set
            {
                _SeriesDataLabelsFontFamily = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsFontFamily));
            }
        }

        private double _SeriesDataLabelsFontSize = 11;
        [Category("数据标签")]
        [DisplayName("字号")]
        public double SeriesDataLabelsFontSize
        {
            get { return _SeriesDataLabelsFontSize; }
            set
            {
                _SeriesDataLabelsFontSize = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsFontSize));
            }
        }

        private string _SeriesDataLabelsColor = "#000000";
        [Category("数据标签")]
        [DisplayName("颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string SeriesDataLabelsColor
        {
            get { return _SeriesDataLabelsColor; }
            set
            {
                _SeriesDataLabelsColor = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsColor));
            }
        }

        private string _SeriesDataLabelsBackgroundColor = "#FFFFFF";
        [Category("数据标签")]
        [DisplayName("背景颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string SeriesDataLabelsBackgroundColor
        {
            get { return _SeriesDataLabelsBackgroundColor; }
            set
            {
                _SeriesDataLabelsBackgroundColor = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsBackgroundColor));
            }
        }

        private Shape _SeriesDataLabelsShape = Shape.square;
        [Category("数据标签")]
        [DisplayName("垂直对齐")]
        public Shape SeriesDataLabelsShape
        {
            get { return _SeriesDataLabelsShape; }
            set
            {
                _SeriesDataLabelsShape = value;
                RaisePropertyChanged(nameof(SeriesDataLabelsShape));
            }
        }

        private ObservableCollection<SeriesDataItem> _SeriesData = new ObservableCollection<SeriesDataItem>();
        [Category("数据列")]
        [DisplayName("数据列数据")]
        [Description("数据列的数据")]
        public virtual ObservableCollection<SeriesDataItem> SeriesData
        {
            get { return _SeriesData; }
            set
            {
                _SeriesData = value;
                RaisePropertyChanged(nameof(SeriesData));
            }
        }

        protected virtual string GetPlotOptionsExtra() => null;

        public string GetPlotOptions()
        {
            var sb = new StringBuilder();
            sb.AppendLine($@"{{
    series: {{
        allowPointSelect: {SeriesAllowPointSelect.ToString().ToLower()},
        cursor: 'pointer',
        shadow: {SeriesShadow.ToString().ToLower()},");
            if (SeriesStep != Step.@false)
                sb.AppendLine($@"
        step: '{SeriesStep}',");
            sb.AppendLine($@"
        borderWidth: {SeriesBorderWidth},
        borderColor: '{SeriesBorderColor}',
        lineWidth: {SeriesLineWidth},
        dashStyle: '{SeriesDashStyle}',
        dataLabels: {{
            enabled: {SeriesDataLabelsEnable.ToString().ToLower()},
            inside: {SeriesDataLabelsInside.ToString().ToLower()},
            color: '{SeriesDataLabelsColor}',    
            backgroundColor: '{SeriesDataLabelsBackgroundColor}',
            shape: '{SeriesDataLabelsShape}',
            borderWidth: 0,
            rotation: {SeriesDataLabelsRotation},
            align: '{SeriesDataLabelsAlign}',
            verticalAlign: '{SeriesDataLabelsVerticalAlign}',
            format: '{SeriesDataLabelsFormat}',
            style: {{
                fontSize: '{SeriesDataLabelsFontSize}px',
                fontFamily: '{SeriesDataLabelsFontFamily}',
                textOutline: null
            }}
        }}
    }}
    {GetPlotOptionsExtra()}
}}");
            return sb.ToString();
        }

        private string GetSeriesOption(SeriesDataItem series)
        {
            return $"{{name:'{series.Name}',data:[]}}";
        }

        public virtual string GetSeriesOptions()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(string.Join(',', SeriesData.Select(t => GetSeriesOption(t))));
            sb.Append("]");
            return sb.ToString();
        }
    }
}
