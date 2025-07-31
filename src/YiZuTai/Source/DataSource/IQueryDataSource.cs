using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source.DataSource
{
    /// <summary>
    /// 查询数据源
    /// </summary>
    public interface IQueryDataSource : ISource
    {
        /// <summary>
        /// 查询结果
        /// </summary>
        QueryDataSourceResult[] Value { get; set; }
        /// <summary>
        /// 输出Json字符串
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
