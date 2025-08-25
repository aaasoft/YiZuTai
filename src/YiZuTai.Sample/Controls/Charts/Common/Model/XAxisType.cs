using System.ComponentModel;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public enum XAxisType
    {
        [DefaultValue("线性")]
        linear,
        [DefaultValue("对数")]
        logarithmic,
        [DefaultValue("日期时间")]
        datetime,
        [DefaultValue("类别")]
        category
    }
}
