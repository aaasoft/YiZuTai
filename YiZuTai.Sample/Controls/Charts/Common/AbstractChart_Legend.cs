using Newtonsoft.Json;
using YiZuTai.Sample.Controls.Charts.Common.Model;
using System.ComponentModel;
using YiZuTai.Components.Editors;

namespace YiZuTai.Sample.Controls.Charts.Common
{
    public partial class AbstractChart
    {
        private string _LegendItemColor = "#333333";
        [Category("图例")]
        [DisplayName("颜色")]
        [Description("图例对象的颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string LegendItemColor
        {
            get { return _LegendItemColor; }
            set
            {
                _LegendItemColor = value;
                RaisePropertyChanged(nameof(LegendItemColor));
            }
        }

        private bool _LegendEnable;
        [Category("图例")]
        [DisplayName("是否显示")]
        [Description("是否显示图例")]
        public virtual bool LegendEnable
        {
            get { return _LegendEnable; }
            set
            {
                _LegendEnable = value;
                RaisePropertyChanged(nameof(LegendEnable));
            }
        }

        private bool _LegendFloating = false;
        [Category("图例")]
        [DisplayName("是否浮动")]
        [Description("图例容器是否可以浮动，当设置为浮动（true）时，图例将不占用绘图区空间，并可以层叠在图形上方。")]
        public virtual bool LegendFloating
        {
            get { return _LegendFloating; }
            set
            {
                _LegendFloating = value;
                RaisePropertyChanged(nameof(LegendFloating));
            }
        }

        private Layout _LegendLayout = Layout.horizontal;
        [Category("图例")]
        [DisplayName("布局")]
        [Description("布局的方式")]
        public virtual Layout LegendLayout
        {
            get { return _LegendLayout; }
            set
            {
                _LegendLayout = value;
                RaisePropertyChanged(nameof(LegendLayout));
            }
        }

        private Align _LegendAlign = Align.center;
        [Category("图例")]
        [DisplayName("水平对齐")]
        [Description("水平对齐的方式")]
        public virtual Align LegendAlign
        {
            get { return _LegendAlign; }
            set
            {
                _LegendAlign = value;
                RaisePropertyChanged(nameof(LegendAlign));
            }
        }

        private VerticalAlign _LegendVerticalAlign = VerticalAlign.bottom;
        [Category("图例")]
        [DisplayName("垂直对齐")]
        [Description("垂直对齐的方式")]
        public virtual VerticalAlign LegendVerticalAlign
        {
            get { return _LegendVerticalAlign; }
            set
            {
                _LegendVerticalAlign = value;
                RaisePropertyChanged(nameof(LegendVerticalAlign));
            }
        }

        public string GetLegendOptions()
        {
            return $@"{{
    enabled: {LegendEnable.ToString().ToLower()},
    align: '{LegendAlign}',
    verticalAlign: '{LegendVerticalAlign}',
    floating: {LegendFloating.ToString().ToLower()},
    layout: '{LegendLayout}',
    itemStyle: {{
        color: '{LegendItemColor}'
    }}
}}";
        }
    }
}
