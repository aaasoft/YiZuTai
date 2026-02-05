using System.ComponentModel;
using System.Diagnostics;

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

        private void onTime(object state)
        {
            try
            {
                RefreshValue();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        private void onStart()
        {
            if (!IsRuning)
                return;
            if (Period == 0)
                onTime(null);
            else
                timer = new Timer(onTime, null, TimeSpan.Zero, TimeSpan.FromSeconds(Period));
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
