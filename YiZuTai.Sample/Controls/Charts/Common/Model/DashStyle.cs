using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    public enum DashStyle
    {
        [Description("实线")]
        Solid,
        [Description("短线")]
        ShortDash,
        [Description("短点")]
        ShortDot,
        [Description("短线点")]
        ShortDashDot,
        [Description("短线点点")]
        ShortDashDotDot,
        [Description("点")]
        Dot,
        [Description("线")]
        Dash,
        [Description("长线")]
        LongDash,
        [Description("线点")]
        DashDot,
        [Description("长线点")]
        LongDashDot,
        [Description("长线点点")]
        LongDashDotDot
    }
}
