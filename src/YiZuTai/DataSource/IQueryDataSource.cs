using YiZuTai.Source;

namespace YiZuTai.DataSource
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
    }
}
