using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Design
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
