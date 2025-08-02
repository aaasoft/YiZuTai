using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum InnerStretch
    {
        [Description("居中")]
        None = 0,
        [Description("拉伸")]
        Fill = 1,
        [Description("适应")]
        Uniform = 2,
        [Description("填充")]
        UniformToFill = 3
    }

    public class StretchItemSource : AbstractEnumItemSource<InnerStretch> { }
}
