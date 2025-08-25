using System.ComponentModel;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public enum LineStyle
    {
        [DefaultValue("折线")]
        line,
        [DefaultValue("曲线")]
        spline,
        [DefaultValue("面积折线")]
        area,
        [DefaultValue("面积曲线")]
        areaspline
    }
}
