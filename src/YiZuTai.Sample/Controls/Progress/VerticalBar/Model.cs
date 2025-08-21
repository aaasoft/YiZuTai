using System.ComponentModel;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.Progress.VerticalBar;


[Category("进度")]
[DisplayName("竖条")]
public class Model : AbstractControl
{
    protected override Type GetViewType() => typeof(View);

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);

        Border = "#000000";
        BorderThickness = 0;
        Width = 30;
        Height = 120;
    }
}