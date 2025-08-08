using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using static YiZuTai.Control.BaseControl;

namespace YiZuTai.Trigger
{
    public abstract class BaseControlTrigger : AbstractTrigger
    {
        private double? _Opacity;
        [Category(nameof(CategoryOrder.外观))]
        [DisplayName("透明度")]
        [Description("控件的透明度(范围：0~1)")]
        [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
        [Range(0, 1)]
        [StringLength(100)]
        public virtual double? Opacity
        {
            get { return _Opacity; }
            set
            {
                if (value < 0 || value > 1)
                    return;
                _Opacity = value;
                RaisePropertyChanged();
            }
        }

        private bool? _Visiable;
        [Category(nameof(CategoryOrder.外观))]
        [DisplayName("是否可见")]
        [Description("控件是否可见")]
        public virtual bool? Visiable
        {
            get { return _Visiable; }
            set
            {
                _Visiable = value;
                RaisePropertyChanged();
            }
        }

        private double? _Left;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("左边距")]
        [Description("控件距离左边的距离")]
        public virtual double? Left
        {
            get { return _Left; }
            set
            {
                if (_Left == value)
                    return;
                _Left = value;
                RaisePropertyChanged();
            }
        }

        private double? _Top;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("上边距")]
        [Description("控件距离上边的距离")]
        public virtual double? Top
        {
            get { return _Top; }
            set
            {
                if (_Top == value)
                    return;
                _Top = value;
                RaisePropertyChanged();
            }
        }

        private double? _Width;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("宽度")]
        [Description("控件的宽度")]
        public virtual double? Width
        {
            get { return _Width; }
            set
            {
                if (value < 1)
                    return;
                _Width = value;
                RaisePropertyChanged();
            }
        }

        private double? _Height;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("高度")]
        [Description("控件的高度")]
        public virtual double? Height
        {
            get { return _Height; }
            set
            {
                if (value < 1)
                    return;
                _Height = value;
                RaisePropertyChanged();
            }
        }

        private double? _Angle;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("角度")]
        [Description("控件的旋转角度")]
        public virtual double? Angle
        {
            get { return _Angle; }
            set
            {
                while (value < 360)
                    value += 360;
                value = value % 360;
                _Angle = value;
                _Angle -= _Angle % 1;
                RaisePropertyChanged();
            }
        }

        public override void Execute(BaseControl controlModel)
        {
            if (Opacity != null)
                controlModel.Opacity = Opacity.Value;
            if(Visiable!=null)
                controlModel.Visiable = Visiable.Value;
            if (Left != null)
                controlModel.Left = Left.Value;
            if (Top != null)
                controlModel.Top = Top.Value;
            if (Width != null)
                controlModel.Width = Width.Value;
            if (Height != null)
                controlModel.Height = Height.Value;
            if (Angle != null)
                controlModel.Angle = Angle.Value;
        }
    }
}
