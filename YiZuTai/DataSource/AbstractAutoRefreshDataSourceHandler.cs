using YiZuTai.Control;
using YiZuTai.Core.ObjectConnection;

namespace YiZuTai.DataSource
{
    public class AbstractAutoRefreshDataSourceHandler : AbstractObjectConnectionHandler<AbstractAutoRefreshDataSource>
    {
        public override void PreviewPropertyChanged(AbstractAutoRefreshDataSource t, string propertyName)
        {
            BaseControl control = ObjectConnectionManager.Instance.Find<BaseControl>(t);
            if (control == null)
                return;

            //如果属性是BaseControl类的Display属性相关属性，则通知Display属性变化
            var objectConnections = ObjectConnectionManager.Instance.FindAll<BaseControl>(t);
            var controlConnectionInfo = objectConnections?.LastOrDefault();
            if (controlConnectionInfo == null)
                return;

            control.OnDataSourceValueChanged(t, propertyName);
        }

        public override void NewValueHandle(AbstractAutoRefreshDataSource t)
        {
            // 同步执行，避免在后台线程启动定时器导致与卸载（Stop）产生竞态
            if (t != null)
            {
                t.Stop();
                t.Start();
            }
        }

        public override void OldValueHandle(AbstractAutoRefreshDataSource t)
        {
            // 同步停止并释放定时器，确保视图卸载时定时器立即失效，避免对已释放视图发起刷新
            if (t != null)
                t.Stop();
        }
    }
}
