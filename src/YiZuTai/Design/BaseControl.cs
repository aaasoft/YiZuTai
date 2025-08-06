using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Quick.Blazor.Bootstrap.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.ViewModel;

namespace YiZuTai.Design
{
    public abstract partial class BaseControl : PropertyNotifyModel
    {
        public enum CategoryOrder
        {
            设计 = -9000,
            布局 = -8000,
            外观 = -1000,
            特效 = -500,
            提示框 = 0
        }

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
        [Editor(typeof(Editor.EditorInputRange), typeof(double))]
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

        [JsonIgnore]
        [Browsable(false)]
        public virtual bool IsCanvasControl => false;

        public virtual RenderFragment GetView(string ZTFileId)
        {
            var modelType = this.GetType();
            var assembly = modelType.Assembly;
            var viewTypeName = modelType.FullName.Substring(0, modelType.FullName.Length - modelType.Name.Length) + "View";
            var viewType = assembly.GetType(viewTypeName);
            if (viewType == null)
                return null;
            return BlazorUtils.GetRenderFragment(viewType, new Dictionary<string, object>()
            {
                [modelType.Name] = this,
                [nameof(ZTFileId)] = ZTFileId
            });
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
