using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum TextAnchor
    {
        [DefaultValue("起始")]
        start,
        [DefaultValue("居中")]
        middle,
        [DefaultValue("结束")]
        end
    }


    public class TextAnchorItemSource : AbstractEnumItemSource<TextAnchor> { }
}
