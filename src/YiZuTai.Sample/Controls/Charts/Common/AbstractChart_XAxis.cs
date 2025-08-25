using Newtonsoft.Json;
using YiZuTai.Sample.Controls.Charts.Common.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Nodes;

namespace YiZuTai.Sample.Controls.Charts.Common
{
    public partial class AbstractChart
    {
        private XAxisType _XAxisType = XAxisType.linear;

        [Category("X轴")]
        [DisplayName("类型")]
        [Description("X轴的类型")]
        public virtual XAxisType XAxisType
        {
            get { return _XAxisType; }
            set
            {
                _XAxisType = value;
                RaisePropertyChanged(nameof(XAxisType));
            }
        }

        private bool _XAxisCrosshair = false;
        [Category("X轴")]
        [DisplayName("十字准星")]
        [Description("是否启用X轴上的十字准星")]
        public virtual bool XAxisCrosshair
        {
            get { return _XAxisCrosshair; }
            set
            {
                _XAxisCrosshair = value;
                RaisePropertyChanged(nameof(XAxisCrosshair));
            }
        }

        private bool _XAxisVisible = true;
        [Category("X轴")]
        [DisplayName("是否可见")]
        [Description("是否显示坐标轴")]
        public virtual bool XAxisVisible
        {
            get { return _XAxisVisible; }
            set
            {
                _XAxisVisible = value;
                RaisePropertyChanged(nameof(XAxisVisible));
            }
        }

        private string _XAxisLabelFontFamily;
        [Category("X轴")]
        [DisplayName("字体")]
        [Description("X轴的字体")]
        public string XAxisLabelFontFamily
        {
            get { return _XAxisLabelFontFamily; }
            set
            {
                _XAxisLabelFontFamily = value;
                RaisePropertyChanged(nameof(XAxisLabelFontFamily));
            }
        }

        private double _XAxisLabelFontSize = 11;
        [Category("X轴")]
        [DisplayName("字号")]
        [Description("X轴的字号")]
        public double XAxisLabelFontSize
        {
            get { return _XAxisLabelFontSize; }
            set
            {
                _XAxisLabelFontSize = value;
                RaisePropertyChanged(nameof(XAxisLabelFontSize));
            }
        }

        private string _XAxisLabelColor = "#000000";
        [Category("X轴")]
        [DisplayName("标签颜色")]
        [Description("标签的颜色")]
        public virtual string XAxisLabelColor
        {
            get { return _XAxisLabelColor; }
            set
            {
                _XAxisLabelColor = value;
                RaisePropertyChanged(nameof(XAxisLabelColor));
            }
        }

        private ObservableCollection<XAxisCategory> _XAxisCategories = new ObservableCollection<XAxisCategory>();
        [Category("X轴")]
        [DisplayName("X轴分组")]
        [Description("X轴的分组")]
        public virtual ObservableCollection<XAxisCategory> XAxisCategories
        {
            get { return _XAxisCategories; }
            set
            {
                _XAxisCategories = value;
                RaisePropertyChanged(nameof(XAxisCategories));
            }
        }

        public string GetXAxisOptions()
        {
            return $@"{{
    type: '{XAxisType}',
    crosshair: {XAxisCrosshair.ToString().ToLower()},
    categories: [{string.Join(',',XAxisCategories.Select(t => $"'{t.Name}'"))}],
    visible: {XAxisVisible.ToString().ToLower()},
    style: {{
        color: '{XAxisLabelColor}',
        fontSize: '{XAxisLabelFontSize}px',
        fontFamily: '{XAxisLabelFontFamily}'
    }}
}}";
        }
    }
}
