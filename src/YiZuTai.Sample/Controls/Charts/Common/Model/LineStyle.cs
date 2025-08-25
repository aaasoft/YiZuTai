using System.ComponentModel;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public enum LineStyle
    {
        [Description("折线")]
        line,
        [Description("曲线")]
        spline,
        [Description("面积折线")]
        area,
        [Description("面积曲线")]
        areaspline
    }
}
