using System.ComponentModel;
using YiZuTai.Components.Editors;
using YiZuTai.Design;

namespace YiZuTai.Sample.Controls.Image
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
        [Editor(typeof(EditorInputImage), typeof(EditorInputImage))]
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
            Image = GetIcon();
        }
    }
}
