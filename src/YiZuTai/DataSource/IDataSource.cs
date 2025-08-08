using YiZuTai.Core.Source;

namespace YiZuTai.DataSource
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
    }
}
