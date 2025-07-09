using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum InnerStretch
    {
        [DefaultValue("居中")]
        None = 0,
        [DefaultValue("拉伸")]
        Fill = 1,
        [DefaultValue("适应")]
        Uniform = 2,
        [DefaultValue("填充")]
        UniformToFill = 3
    }

    public class StretchItemSource : AbstractEnumItemSource<InnerStretch> { }
}
