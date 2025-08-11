using System.ComponentModel;

namespace YiZuTai.Control
{
    public abstract class BaseControl_WithBackgroundAndAnimate : BaseControl_WithBackground
    {
        private string _RotateDur;
        [Category("动画")]
        [DisplayName("旋转时长")]
        [Description("指定简单持续时间的时长。值必须大于 0。可以用小时（h）、分钟（m）、秒（s）、毫秒（ms）表达这个值。可以组合这些时间表达式以提供一个复合的持续时间，比如这样：hh:mm:ss.iii 或者这样：mm:ss.iii。")]
        public string RotateDur
        {
            get { return _RotateDur; }
            set
            {
                _RotateDur = value;
                RaisePropertyChanged();
            }
        }

        private string _FlashDur;
        [Category("动画")]
        [DisplayName("闪烁时长")]
        [Description("指定简单持续时间的时长。值必须大于 0。可以用小时（h）、分钟（m）、秒（s）、毫秒（ms）表达这个值。可以组合这些时间表达式以提供一个复合的持续时间，比如这样：hh:mm:ss.iii 或者这样：mm:ss.iii。")]
        public string FlashDur
        {
            get { return _FlashDur; }
            set
            {
                _FlashDur = value;
                RaisePropertyChanged();
            }
        }
    }
}
