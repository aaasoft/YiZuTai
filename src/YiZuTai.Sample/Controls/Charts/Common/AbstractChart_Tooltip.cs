using System.ComponentModel;

namespace YiZuTai.Sample.Controls.Charts.Common
{

    public partial class AbstractChart
    {
        private const string 图表提示框 = nameof(图表提示框);
        private string _TooltipPointFormat;
        [Category(图表提示框)]
        [DisplayName("文字格式")]
        [Description(Consts.FormatDescription)]
        public virtual string TooltipPointFormat
        {
            get { return _TooltipPointFormat; }
            set
            {
                _TooltipPointFormat = value;
                RaisePropertyChanged(nameof(TooltipPointFormat));
            }
        }

        private bool _TooltipEnabled = true;
        [Category(图表提示框)]
        [DisplayName("是否启用")]
        [Description("是否启用提示框")]
        public virtual bool TooltipEnabled
        {
            get { return _TooltipEnabled; }
            set
            {
                _TooltipEnabled = value;
                RaisePropertyChanged(nameof(TooltipEnabled));
            }
        }

        private bool _TooltipSplit = false;
        [Category(图表提示框)]
        [DisplayName("分开显示")]
        [Description("是否让提示框分开显示")]
        public virtual bool TooltipSplit
        {
            get { return _TooltipSplit; }
            set
            {
                _TooltipSplit = value;
                RaisePropertyChanged(nameof(TooltipSplit));
            }
        }
    }
}
