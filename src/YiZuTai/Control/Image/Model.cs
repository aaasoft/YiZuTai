using System.ComponentModel;
using YiZuTai.Design;

namespace YiZuTai.Control.Image
{
    [Category("常用")]
    [DisplayName("图片")]
    public class Model : BaseControl_WithBackgroundAndAnimate
    {
        protected override Type GetViewType() => typeof(View);

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

        public override void OnDragDropCreate(Point location)
        {
            base.OnDragDropCreate(location);
            Width = 160;
            Height = 120;
            BackgroundOpacity = 0;
            Image = $"../../../_content/{nameof(YiZuTai)}/default_image.svg";
        }
    }
}
