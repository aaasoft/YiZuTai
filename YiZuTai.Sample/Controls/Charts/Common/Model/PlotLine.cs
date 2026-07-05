using Newtonsoft.Json;
using Quick.Blazor.Bootstrap;
using System.ComponentModel;
using YiZuTai.Components.Editors;
using YiZuTai.DataSource;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    [DisplayName("标志线")]
    public class PlotLine: PropertyNotifyModel
    {
        private IDataSource _Value;
        [DisplayName("线条值")]
        [Description("线条的值")]        
        public IDataSource Value
        {
            get { return _Value; }
            set
            {
                RaisePropertyChanging(nameof(Value));
                _Value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        private DashStyle _DashStyle = DashStyle.Dash;
        [DisplayName("线条样式")]
        [Description("线条的样式")]
        public virtual DashStyle DashStyle
        {
            get { return _DashStyle; }
            set
            {
                _DashStyle = value;
                RaisePropertyChanged(nameof(DashStyle));
            }
        }

        private double _LineWidth = 2;
        [DisplayName("线条宽度")]
        [Description("线条的宽度")]
        public virtual double LineWidth
        {
            get { return _LineWidth; }
            set
            {
                if (value < 1)
                    return;
                _LineWidth = value;
                RaisePropertyChanged(nameof(LineWidth));
            }
        }

        private string _LineColor = "#000000";
        [DisplayName("线条颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                RaisePropertyChanged(nameof(LineColor));
            }
        }

        public override string ToString()
        {
            return "标志线";
        }
    }
}
