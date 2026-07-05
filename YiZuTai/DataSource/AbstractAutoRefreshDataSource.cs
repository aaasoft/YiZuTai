using YiZuTai.Core;
using YiZuTai.Core.ObjectConnection;

namespace YiZuTai.DataSource
{
    /// <summary>
    /// 自动刷新数据来源抽象类
    /// </summary>
    public abstract class AbstractAutoRefreshDataSource : AbstractDataSource, IRunnable
    {
        static AbstractAutoRefreshDataSource()
        {
            ObjectConnectionManager.Instance.RegisterHandler(new AbstractAutoRefreshDataSourceHandler());
        }

        protected bool IsRuning { get; private set; } = false;

        public virtual void Start()
        {
            IsRuning = true;
        }

        public virtual void Stop()
        {
            IsRuning = false;
        }
    }
}
