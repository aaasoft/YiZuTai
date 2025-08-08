using System.ComponentModel;

namespace YiZuTai.Sample.Enums
{
    /// <summary>
    /// A标签目标
    /// </summary>
    public enum HyperLinkTarget
    {
        [Description("新页面")]
        _blank = 0,
        [Description("当前页面")]
        _self = 1,
        [Description("父框架")]
        _parent = 2,
        [Description("顶层框架")]
        _top = 3,
        [Description("弹出窗口")]
        _window = 4
    }
}
