using System.ComponentModel;
using System.Diagnostics;

namespace YiZuTai.Source.DataSource
{
    public abstract class AbstractAutoRefreshOnTimerDataSource : AbstractAutoRefreshDataSource
    {
        private int _Period = 10;
        [DisplayName("刷新间隔")]
        [Description("单位：秒，等于0时：对于支持实时推送的数据代表实时推送；对于不支持实时推送的数据代表只刷新一次，不再定时刷新")]
        public int Period
        {
            get { return _Period; }
            set
            {
                if (value < 0)
                    return;
                _Period = value;
                RaisePropertyChanged(nameof(Period));
            }
        }

        public AbstractAutoRefreshOnTimerDataSource()
        {
            this.PropertyChanged += AbstractAutoRefreshDataSource_PropertyChanged;
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
                if (RefreshValue())
                    RaisePropertyChanged(nameof(Value));
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        public override void Start()
        {
            if (Period == 0)
                onTime(null);
            else
                timer = new Timer(onTime, 0, TimeSpan.Zero, TimeSpan.FromSeconds(Period));
        }

        public override void Stop()
        {
            if (timer != null)
                timer.Dispose();
            timer = null;
        }

        private void AbstractAutoRefreshDataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Period):
                    if (Period == 0)
                    {
                        Stop();
                    }
                    else
                    {
                        if (timer == null)
                            Start();
                        else
                            timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(Period));
                    }
                    break;
            }
        }
    }
}
