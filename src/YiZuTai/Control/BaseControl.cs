using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Quick.Blazor.Bootstrap;
using Quick.Blazor.Bootstrap.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components;
using YiZuTai.Components.Editors;
using YiZuTai.Core;
using YiZuTai.DataSource;
using YiZuTai.Enums;
using YiZuTai.HyperLinkSource;

namespace YiZuTai.Control
{
    public abstract partial class BaseControl : PropertyNotifyModel
    {
        private string _Name;
        [Category(nameof(CategoryOrder.设计))]
        [DisplayName("名称")]
        [Description("控件的名称")]
        public virtual string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                RaisePropertyChanged();
            }
        }

        private double _Opacity = 1;
        [Category(nameof(CategoryOrder.外观))]
        [DisplayName("透明度")]
        [Description("控件的透明度(范围：0~1)")]
        [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
        [Range(0, 1)]
        [StringLength(100)]
        public virtual double Opacity
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

        private bool _Visiable = true;
        [Category(nameof(CategoryOrder.外观))]
        [DisplayName("是否可见")]
        [Description("控件是否可见")]
        public virtual bool Visiable
        {
            get { return _Visiable; }
            set
            {
                _Visiable = value;
                RaisePropertyChanged();
            }
        }

        private double _Left;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("左边距")]
        [Description("控件距离左边的距离")]
        public virtual double Left
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

        private double _Top;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("上边距")]
        [Description("控件距离上边的距离")]
        public virtual double Top
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

        private double _Width = 60;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("宽度")]
        [Description("控件的宽度")]
        public virtual double Width
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

        private double _Height = 40;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("高度")]
        [Description("控件的高度")]
        public virtual double Height
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

        private double _Angle;
        [Category(nameof(CategoryOrder.布局))]
        [DisplayName("角度")]
        [Description("控件的旋转角度")]
        public virtual double Angle
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

        private bool _IsLocked;
        [Category("设计")]
        [DisplayName("是否锁定")]
        [Description("控件锁定后，就不能通过设计器去调整位置和大小等参数")]
        public virtual bool IsLocked
        {
            get { return _IsLocked; }
            set
            {
                _IsLocked = value;
                RaisePropertyChanged();
            }
        }


        private IHyperLinkSource _HyperLinkSource;
        [Category("超链接")]
        [DisplayName("源")]
        public virtual IHyperLinkSource HyperLinkSource
        {
            get { return _HyperLinkSource; }
            set
            {
                RaisePropertyChanging(nameof(HyperLinkSource));
                _HyperLinkSource = value;
                RaisePropertyChanged(nameof(HyperLinkSource));
            }
        }

        /// <summary>
        /// 根据角度属性计算的弧度
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public double Radian { get { return Deg2Rad(Angle); } }

        //角度转弧度
        private double Deg2Rad(double deg) => deg * Math.PI / 180;

        /// <summary>
        /// 中心点坐标
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        [DisplayName("中心点")]
        public Point CenterPointLocation
        {
            get
            {
                return new Point(
                    Left + Width / 2,
                    Top + Height / 2
                    );
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public double RotateCenterX
        {
            get { return Width / 2; }
        }

        [JsonIgnore]
        [Browsable(false)]
        public double RotateCenterY
        {
            get { return Height / 2; }
        }

        /*
假设对图片上任意点(x,y)，绕一个坐标点(rx0,ry0)逆时针旋转a角度后的新的坐标设为(x0, y0)，有公式：
x0= (x - rx0)*cos(a) - (y - ry0)*sin(a) + rx0 ;
y0= (x - rx0)*sin(a) + (y - ry0)*cos(a) + ry0 ;
        */

        /// <summary>
        /// 左上角坐标
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        [DisplayName("左上点")]
        public Point LeftTopPointLocation
        {
            get
            {
                var rx0 = CenterPointLocation.X;
                var ry0 = CenterPointLocation.Y;
                var x = rx0 - Width / 2;
                var y = ry0 - Height / 2;
                return new Point(
                    (x - rx0) * Math.Cos(Radian) - (y - ry0) * Math.Sin(Radian) + rx0,
                    (x - rx0) * Math.Sin(Radian) + (y - ry0) * Math.Cos(Radian) + ry0
                    );
            }
        }

        /// 右上角坐标
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        [DisplayName("右上点")]
        public Point RightTopPointLocation
        {
            get
            {
                var rx0 = CenterPointLocation.X;
                var ry0 = CenterPointLocation.Y;
                var x = rx0 + Width / 2;
                var y = ry0 - Height / 2;
                return new Point(
                    (x - rx0) * Math.Cos(Radian) - (y - ry0) * Math.Sin(Radian) + rx0,
                    (x - rx0) * Math.Sin(Radian) + (y - ry0) * Math.Cos(Radian) + ry0
                    );
            }
        }

        /// 右下角坐标
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        [DisplayName("右下点")]
        public Point RightBottomPointLocation
        {
            get
            {
                var rx0 = CenterPointLocation.X;
                var ry0 = CenterPointLocation.Y;
                var x = rx0 + Width / 2;
                var y = ry0 + Height / 2;
                return new Point(
                    (x - rx0) * Math.Cos(Radian) - (y - ry0) * Math.Sin(Radian) + rx0,
                    (x - rx0) * Math.Sin(Radian) + (y - ry0) * Math.Cos(Radian) + ry0
                    );
            }
        }

        /// <summary>
        /// 左下角坐标
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        [DisplayName("左下点")]
        public Point LeftBottomPointLocation
        {
            get
            {
                var rx0 = CenterPointLocation.X;
                var ry0 = CenterPointLocation.Y;
                var x = rx0 - Width / 2;
                var y = ry0 + Height / 2;
                return new Point(
                    (x - rx0) * Math.Cos(Radian) - (y - ry0) * Math.Sin(Radian) + rx0,
                    (x - rx0) * Math.Sin(Radian) + (y - ry0) * Math.Cos(Radian) + ry0
                    );
            }
        }

        /// <summary>
        /// 旋转角度偏移量
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public double MoveAngleOffset { get; set; }

        /// <summary>
        /// 移动位置偏移移量
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual Point MoveLocationOffset { get; set; }

        /// <summary>
        /// 父控件
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual BaseControl Parent { get; set; }

        /// <summary>
        /// 子控件
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<BaseControl> Controls { get; set; } = null;

        /// <summary>
        /// 快捷操作
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual IControlShotcut[] Shotcuts { get; } = null;
        /// <summary>
        /// 当加载时
        /// </summary>
        public virtual void OnLoad() { }
        /// <summary>
        /// 当卸载时
        /// </summary>
        public virtual void OnUnload() { }

        protected abstract Type GetViewType();
        protected virtual Type GetHtmlViewType() => null;
        
        public virtual Dictionary<string, Func<IDataSource>> GetDataSourceDict() => null;

        public RenderFragment GetView(ZTContext context)
        {
            var viewType = GetViewType();
            return BlazorUtils.GetRenderFragment(viewType, new Dictionary<string, object>()
            {
                [nameof(AbstractControlView<MultiControl>.ControlModel)] = this,
                [nameof(ZTContext)] = context
            });
        }

        public RenderFragment GetHtmlView(ZTContext context)
        {
            var extraViewType = GetHtmlViewType();
            if (extraViewType == null)
                return null;
            return BlazorUtils.GetRenderFragment(extraViewType, new Dictionary<string, object>()
            {
                [nameof(AbstractControlView<MultiControl>.ControlModel)] = this,
                [nameof(ZTContext)] = context
            });
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
