using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace YiZuTai.DataSource
{
    public abstract class AbstractAutoRefreshOnTimerDataSource : AbstractAutoRefreshDataSource
    {
        private int _Period = 1;
        [DisplayName("刷新间隔")]
        [Description("单位：秒")]
        public int Period
        {
            get { return _Period; }
            set
            {
                if (value < 0)
                    return;
                _Period = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 刷新Value属性
        /// </summary>
        /// <returns>值是否有变化</returns>
        public abstract bool RefreshValue();

        private Timer timer = null;
        private int timerBusy = 0; // 重入保护：RefreshValue 慢于刷新间隔时避免并发执行

        private void onTime(object state)
        {
            // 卸载/停止后不再处理，避免对已释放的视图发起刷新
            if (!IsRuning)
                return;
            // 重入保护
            if (Interlocked.Exchange(ref timerBusy, 1) == 1)
                return;
            try
            {
                RefreshValue();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            finally
            {
                Interlocked.Exchange(ref timerBusy, 0);
            }
        }

        private void onStart()
        {
            if (!IsRuning)
                return;
            // 始终通过 Timer 触发：首刷延迟为 0（立即在后台线程执行，不阻塞 UI）；
            // Period<=0 时仅刷新一次（无限间隔），与原来“Period==0 立即刷新一次”语义一致
            var period = Period > 0 ? TimeSpan.FromSeconds(Period) : Timeout.InfiniteTimeSpan;
            timer = new Timer(onTime, null, TimeSpan.Zero, period);
        }

        private void onStop()
        {
            timer?.Dispose();
            timer = null;
        }

        public override void Start()
        {
            base.Start();
            onStart();
        }

        public override void Stop()
        {
            onStop();
            base.Stop();
        }
    }
}
