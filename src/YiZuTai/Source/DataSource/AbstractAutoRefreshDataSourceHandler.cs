using YiZuTai.Design;
using YiZuTai.Core.ObjectConnection;

namespace YiZuTai.Source.DataSource
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
            if (t != null)
                Task.Run(() =>
                {
                    t.Stop();
                    t.Start();
                });
        }

        public override void OldValueHandle(AbstractAutoRefreshDataSource t)
        {
            if (t != null)
                Task.Run(() => t.Stop());
        }
    }
}
