using System.ComponentModel;

namespace YiZuTai.Enums
{
    /// <summary>
    /// 填充方式
    /// </summary>
    public enum Stretch
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
}
