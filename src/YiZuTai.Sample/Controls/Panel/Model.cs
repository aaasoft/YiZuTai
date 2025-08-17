using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Panel;

[Category("容器")]
[DisplayName("面板")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);
    public override ObservableCollection<BaseControl> Controls { get; set; } = new ObservableCollection<BaseControl>();

    public override void OnDragDropCreate(Point location)
    {
        Background = "#000000";
        BackgroundOpacity = 1;
    }
}
