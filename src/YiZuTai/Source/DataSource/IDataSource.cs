using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source.DataSource
{
    /// <summary>
    /// 实时数据源
    /// </summary>
    public interface IDataSource : ISource
    {
        /// <summary>
        /// 当前值
        /// </summary>
        string Value { get; set; }
        /// <summary>
        /// 输出Json字符串
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
