using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using YiZuTai.Design;

namespace YiZuTai.Control.Image
{
    [Control(
        Category = "常用",
        Name = "图片"
        )]
    public class Model : BaseControl_WithBackground
    {
        private string _Image;
        [Category("数据")]
        [DisplayName("图片")]
        [Description("图片控件要显示的图片")]
        [Editor(typeof(Design.Editor.EditorInputImage), typeof(string))]
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        private double _RotateSpeed = 0;
        [Category("动画")]
        [DisplayName("旋转速度")]
        [Description("图片旋转的速度")]
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
        [Description("图片闪烁的间隔，单位：毫秒")]
        public double FlashInterval
        {
            get { return _FlashInterval; }
            set
            {
                _FlashInterval = value;
                RaisePropertyChanged(nameof(FlashInterval));
            }
        }

        public Model()
        {
            Width = Height = 60;
            BackgroundOpacity = 0;
        }
    }
}
