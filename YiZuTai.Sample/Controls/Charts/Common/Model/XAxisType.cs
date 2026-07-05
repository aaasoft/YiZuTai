using System.ComponentModel;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public enum XAxisType
    {
        [Description("线性")]
        linear,
        [Description("对数")]
        logarithmic,
        [Description("日期时间")]
        datetime,
        [Description("类别")]
        category
    }
}
