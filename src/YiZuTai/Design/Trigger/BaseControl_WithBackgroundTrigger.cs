using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiZuTai.Components.Editors;

namespace YiZuTai.Design.Trigger
{
    public abstract class BaseControl_WithBackgroundTrigger : BaseControlTrigger
    {
        private string _Background { get; set; }
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

        private double? _BackgroundOpacity;
        [Category("背景")]
        [DisplayName("背景透明")]
        [Description("控件的背景透明度")]
        [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
        [Range(0, 1)]
        [StringLength(100)]
        public virtual double? BackgroundOpacity
        {
            get { return _BackgroundOpacity; }
            set
            {
                _BackgroundOpacity = value;
                RaisePropertyChanged();
            }
        }

        private string _Border;
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

        private double? _BorderOpacity;
        [Category("背景")]
        [DisplayName("边框透明")]
        [Description("控件的边框透明度")]
        [Editor(typeof(EditorInputRange_Nullable), typeof(EditorInputRange_Nullable))]
        [Range(0, 1)]
        [StringLength(100)]
        public virtual double? BorderOpacity
        {
            get { return _BorderOpacity; }
            set
            {
                _BorderOpacity = value;
                RaisePropertyChanged();
            }
        }

        private double? _BorderThickness;
        [Category("背景")]
        [DisplayName("边框宽度")]
        [Description("控件的边框宽度")]
        public virtual double? BorderThickness
        {
            get { return _BorderThickness; }
            set
            {
                _BorderThickness = value;
                RaisePropertyChanged();
            }
        }

        private double? _CornerRadius;
        [Category("背景")]
        [DisplayName("边框圆角")]
        [Description("边框的圆角半径，为0时为直角")]
        public virtual double? CornerRadius
        {
            get { return _CornerRadius; }
            set
            {
                _CornerRadius = value;
                RaisePropertyChanged();
            }
        }

        public override void Execute(BaseControl controlModel)
        {
            base.Execute(controlModel);

            var model = (BaseControl_WithBackground)controlModel;
            if (Background != null)
                model.Background = Background;
            if (BackgroundOpacity != null)
                model.BackgroundOpacity = BackgroundOpacity.Value;
            if (Border != null)
                model.Border = Border;
            if (BorderOpacity != null)
                model.BorderOpacity = BorderOpacity.Value;
            if (BorderThickness != null)
                model.BorderThickness = BorderThickness.Value;
            if (CornerRadius != null)
                model.CornerRadius = CornerRadius.Value;
        }
    }
}
