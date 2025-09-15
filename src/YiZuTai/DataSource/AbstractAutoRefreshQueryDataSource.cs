using YiZuTai.Core.ObjectConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.DataSource
{
    public abstract class AbstractAutoRefreshQueryDataSource : AbstractQueryDataSource
    {
        static AbstractAutoRefreshQueryDataSource()
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
