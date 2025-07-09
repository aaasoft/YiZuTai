using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    /// <summary>
    /// A标签目标
    /// </summary>
    public enum HyperLinkTarget
    {
        [DefaultValue("新页面")]
        _blank = 0,
        [DefaultValue("当前页面")]
        _self = 1,
        [DefaultValue("父框架")]
        _parent = 2,
        [DefaultValue("顶层框架")]
        _top = 3,
        [DefaultValue("弹出窗口")]
        _window = 4
    }
    public class HyperLinkTargetItemSource : AbstractEnumItemSource<HyperLinkTarget> { }
}
