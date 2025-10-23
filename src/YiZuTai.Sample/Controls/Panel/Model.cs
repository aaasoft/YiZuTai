using System.Collections.ObjectModel;
using System.ComponentModel;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.Panel;

[Category("容器")]
[DisplayName("面板")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);
    public override bool IsContainer => true;
    public override ObservableCollection<BaseControl> Controls { get; set; } = new ObservableCollection<BaseControl>();

    private string _Image;
    [Category("数据")]
    [DisplayName("图片")]
    [Description("控件要显示的图片")]
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
        Width = 400;
        Height = 300;
    }
}
