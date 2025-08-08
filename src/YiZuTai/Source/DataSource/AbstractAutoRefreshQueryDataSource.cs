using YiZuTai.Core.ObjectConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source.DataSource
{
    public abstract class AbstractAutoRefreshQueryDataSource : AbstractQueryDataSource
    {
        static AbstractAutoRefreshQueryDataSource()
        {
            ObjectConnectionManager.Instance.RegisterHandler(new AbstractAutoRefreshDataSourceHandler());
        }

        public virtual void Start() { }

        public virtual void Stop() { }
    }
}
