using Newtonsoft.Json;
using YiZuTai.Sample.Controls.Charts.Common.Model;
using System.ComponentModel;
using System.Text.Json.Nodes;
using YiZuTai.Components.Editors;

namespace YiZuTai.Sample.Controls.Charts.Common
{
    public partial class AbstractChart
    {
        private string _TitleText;
        [Category("标题")]
        [DisplayName("标题文字")]
        [Description("图表的标题")]
        public virtual string TitleText
        {
            get { return _TitleText; }
            set
            {
                _TitleText = value;
                RaisePropertyChanged(nameof(TitleText));
            }
        }

        private Align _TitleAlign = Align.center;
        [Category("标题")]
        [DisplayName("水平对齐")]
        [Description("水平对齐方式")]
        public virtual Align TitleAlign
        {
            get { return _TitleAlign; }
            set
            {
                _TitleAlign = value;
                RaisePropertyChanged(nameof(TitleAlign));
            }
        }

        private VerticalAlign _TitleVerticalAlign = VerticalAlign.top;
        [Category("标题")]
        [DisplayName("垂直对齐")]
        [Description("垂直对齐方式")]
        public virtual VerticalAlign TitleVerticalAlign
        {
            get { return _TitleVerticalAlign; }
            set
            {
                _TitleVerticalAlign = value;
                RaisePropertyChanged(nameof(TitleVerticalAlign));
            }
        }

        private bool _TitleFloating = true;
        [Category("标题")]
        [DisplayName("是否浮动")]
        [Description("是否浮动，设置浮动后，标题将不占用图表区位置")]
        public virtual bool TitleFloating
        {
            get { return _TitleFloating; }
            set
            {
                _TitleFloating = value;
                RaisePropertyChanged(nameof(TitleFloating));
            }
        }

        private string _TitleFontFamily;
        [Category("标题")]
        [DisplayName("字体")]
        [Description("标题的字体")]
        public string TitleFontFamily
        {
            get { return _TitleFontFamily; }
            set
            {
                _TitleFontFamily = value;
                RaisePropertyChanged(nameof(TitleFontFamily));
            }
        }

        private double _TitleFontSize = 18;
        [Category("标题")]
        [DisplayName("字号")]
        [Description("标题的字号")]
        public double TitleFontSize
        {
            get { return _TitleFontSize; }
            set
            {
                _TitleFontSize = value;
                RaisePropertyChanged(nameof(TitleFontSize));
            }
        }

        private string _TitleColor = "#000000";
        [Category("标题")]
        [DisplayName("颜色")]
        [Description("标题的颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string TitleColor
        {
            get { return _TitleColor; }
            set
            {
                _TitleColor = value;
                RaisePropertyChanged(nameof(TitleColor));
            }
        }

        public string GetTitleOptions()
        {
            return $@"{{
    floating: {TitleFloating.ToString().ToLower()},
    text: '{TitleText}',
    align: '{TitleAlign}',
    verticalAlign: '{TitleVerticalAlign}',
    style: {{
        color: '{TitleColor}',
        fontSize: '{TitleFontSize}px',
        fontFamily: '{TitleFontFamily}'
    }}
}}";
        }
    }
}
