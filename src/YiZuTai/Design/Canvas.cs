using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using YiZuTai.Utils;

namespace YiZuTai.Design
{
    public class Canvas : BaseControl_WithBackground
    {
        public override bool IsCanvasControl => true;

        [JsonIgnore]
        [Browsable(false)]
        public override string Name => "画布";
        [JsonIgnore]
        [Browsable(false)]
        public override bool Visiable => false;

        [JsonIgnore]
        [Browsable(false)]
        public override double Opacity { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public override double Left { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public override double Top { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public override double Angle { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public override bool IsLocked { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public override double BackgroundOpacity { get; set; }

        //前景
        private string _Foreground = "#ffffff";
        [Category("前景")]
        [DisplayName("前景颜色")]
        [Description("控件的前景颜色")]
        [Editor(typeof(Editor.EditorInputColor), typeof(string))]
        public virtual string Foreground
        {
            get { return _Foreground; }
            set
            {
                _Foreground = value;
                RaisePropertyChanged();
            }
        }

        private double _ForegroundOpacity = 1;
        [Category("前景")]
        [DisplayName("前景透明")]
        [Description("控件的前景透明度")]
        [Editor(typeof(Editor.EditorInputRange), typeof(double))]
        [Range(0, 1)]
        [StringLength(100)]
        public double ForegroundOpacity
        {
            get { return _ForegroundOpacity; }
            set
            {
                _ForegroundOpacity = value;
                RaisePropertyChanged();
            }
        }

        private string _Image;
        [Category("前景")]
        [DisplayName("前景图片")]
        [Description("画布上要显示的图片")]
        [Editor(typeof(Design.Editor.EditorInputImage), typeof(string))]
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                RaisePropertyChanged();
            }
        }

        private Stretch _Stretch = Stretch.UniformToFill;
        [Category("公共")]
        [DisplayName("填充方式")]
        [Description(@"
[居中]内容保持其原始大小。
[拉伸]调整内容大小以填充目标尺寸。 不保留纵横比。
[适应]在保留内容原有纵横比的同时调整内容的大小，以适合目标尺寸。
[填充]在保留内容原有纵横比的同时调整内容的大小，以填充目标尺寸。 如果目标矩形的纵横比不同于源矩形的纵横比，则对源内容进行剪裁以适合目标尺寸。")]
        public Stretch Stretch
        {
            get { return _Stretch; }
            set
            {
                _Stretch = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 设计网格大小
        /// </summary>
        [Browsable(false)]
        public int DesignGridSize { get; set; } = 1;
        [Browsable(false)]
        public bool DesignGridVisiable { get; set; } = false;
        /// <summary>
        /// 设计网格的ViewPort
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public Rect DesignGridViewPort => new Rect(DesignGridSize, DesignGridSize, DesignGridSize, DesignGridSize);

        [Browsable(false)]
        [JsonIgnore]
        public string DesignGridColor
        {
            get
            {
                var foreground = ColorUtils.ParseColor(Foreground);
                return ColorUtils.GetRGBString(Color.FromArgb(
                        0x33,
                        (byte)(255 - foreground.R),
                        (byte)(255 - foreground.G),
                        (byte)(255 - foreground.B)
                ));
            }
        }

        public Canvas()
        {
            Width = 800;
            Height = 600;
            Background = "#a9a9a9";
        }
    }
}
