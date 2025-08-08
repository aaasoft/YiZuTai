using System.ComponentModel;

namespace YiZuTai.Control
{
    public abstract class BaseControl_WithBackgroundAndAnimate : BaseControl_WithBackground
    {
        private double _RotateSpeed = 0;
        [Category("动画")]
        [DisplayName("旋转速度")]
        [Description("控件旋转的速度")]
        public double RotateSpeed
        {
            get { return _RotateSpeed; }
            set
            {
                _RotateSpeed = value;
                RaisePropertyChanged(nameof(RotateSpeed));
            }
        }

        private double _FlashInterval = 0;
        [Category("动画")]
        [DisplayName("闪烁间隔")]
        [Description("控件闪烁的间隔，单位：毫秒")]
        public double FlashInterval
        {
            get { return _FlashInterval; }
            set
            {
                _FlashInterval = value;
                RaisePropertyChanged(nameof(FlashInterval));
            }
        }

        public override string[] GetHasBussinesToDispayProperties()
        {
            return base.GetHasBussinesToDispayProperties().Concat(
                [
                    nameof(RotateSpeed),
                    nameof(FlashInterval)
                ]).ToArray();
        }
    }
}
