using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum TextAnchor
    {
        [Description("起始")]
        start,
        [Description("居中")]
        middle,
        [Description("结束")]
        end
    }


    public class TextAnchorItemSource : AbstractEnumItemSource<TextAnchor> { }
}
