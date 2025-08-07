using System.ComponentModel;

namespace YiZuTai.Design.Trigger
{
    public abstract class BaseControl_WithBackgroundAndAnimateTrigger : BaseControl_WithBackgroundTrigger
    {
        private double? _RotateSpeed;
        [Category("动画")]
        [DisplayName("旋转速度")]
        [Description("控件旋转的速度")]
        public double? RotateSpeed
        {
            get { return _RotateSpeed; }
            set
            {
                _RotateSpeed = value;
                RaisePropertyChanged(nameof(RotateSpeed));
            }
        }

        private double? _FlashInterval;
        [Category("动画")]
        [DisplayName("闪烁间隔")]
        [Description("控件闪烁的间隔，单位：毫秒")]
        public double? FlashInterval
        {
            get { return _FlashInterval; }
            set
            {
                _FlashInterval = value;
                RaisePropertyChanged(nameof(FlashInterval));
            }
        }


        public override void Execute(BaseControl controlModel)
        {
            base.Execute(controlModel);

            var model = (BaseControl_WithBackgroundAndAnimate)controlModel;
            if (RotateSpeed != null)
                model.RotateSpeed = RotateSpeed.Value;
            if (FlashInterval != null)
                model.FlashInterval = FlashInterval.Value;
        }
    }
}
