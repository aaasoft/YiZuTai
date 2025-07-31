using YiZuTai.ObjectConnection;

namespace YiZuTai.Source.DataSource
{
    /// <summary>
    /// 自动刷新数据来源抽象类
    /// </summary>
    public abstract class AbstractAutoRefreshDataSource : AbstractDataSource
    {
        static AbstractAutoRefreshDataSource()
        {
            ObjectConnectionManager.Instance.RegisterHandler(new AbstractAutoRefreshDataSourceHandler());
        }

        public virtual void Start() { }

        public virtual void Stop() { }

    }
}
