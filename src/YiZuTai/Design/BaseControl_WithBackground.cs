using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;

namespace YiZuTai.Design
{
    public abstract class BaseControl_WithBackground : BaseControl
    {
        private string _Background { get; set; } = "#ffffff";
        //默认为透明
        [Category("背景")]
        [DisplayName("背景颜色")]
        [Description("控件的背景颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string Background
        {
            get { return _Background; }
            set
            {
                _Background = value;
                RaisePropertyChanged();
            }
        }

        private double _BackgroundOpacity = 1;
        [Category("背景")]
        [DisplayName("背景透明")]
        [Description("控件的背景透明度")]
        [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
        [Range(0, 1)]
        [StringLength(100)]
        public virtual double BackgroundOpacity
        {
            get { return _BackgroundOpacity; }
            set
            {
                _BackgroundOpacity = value;
                RaisePropertyChanged();
            }
        }

        private string _Border = "#000000";
        [Category("背景")]
        [DisplayName("边框颜色")]
        [Description("控件的边框颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string Border
        {
            get { return _Border; }
            set
            {
                _Border = value;
                RaisePropertyChanged();
            }
        }

        private double _BorderOpacity = 1;
        [Category("背景")]
        [DisplayName("边框透明")]
        [Description("控件的边框透明度")]
        [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
        [Range(0, 1)]
        [StringLength(100)]
        public virtual double BorderOpacity
        {
            get { return _BorderOpacity; }
            set
            {
                _BorderOpacity = value;
                RaisePropertyChanged();
            }
        }

        private double _BorderThickness = 0;
        [Category("背景")]
        [DisplayName("边框宽度")]
        [Description("控件的边框宽度")]
        public virtual double BorderThickness
        {
            get { return _BorderThickness; }
            set
            {
                _BorderThickness = value;
                RaisePropertyChanged();
            }
        }

        private double _CornerRadius = 0;
        [Category("背景")]
        [DisplayName("边框圆角")]
        [Description("边框的圆角半径，为0时为直角")]
        public virtual double CornerRadius
        {
            get { return _CornerRadius; }
            set
            {
                _CornerRadius = value;
                RaisePropertyChanged();
            }
        }

        public override string[] GetHasBussinesToDispayProperties()
        {
            return base.GetHasBussinesToDispayProperties().Concat(
                    [
                        nameof(Background),
                        nameof(Border),
                        nameof(BorderThickness),
                        nameof(CornerRadius)
                    ]).ToArray();
        }
    }
}
