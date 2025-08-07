using System.ComponentModel;

namespace YiZuTai.Design.Enums;

public enum Operators
{
    [Description("等于")]
    EqualTo,
    [Description("不等于")]
    NotEqualTo,
    [Description("大于")]
    GreaterThan,
    [Description("小于")]
    LessThan,
    [Description("大于等于")]
    GreaterThanOrEqualTo,
    [Description("小于等于")]
    LessThanOrEqualTo,
    [Description("包含")]
    Contains,
    [Description("不包含")]
    NotContains
}
