using System;
using System.ComponentModel;
using YiZuTai.Control;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.Progress.HorizontalBar;


[Category("进度")]
[DisplayName("横条")]
public class Model : AbstractControl
{
    protected override Type GetViewType() => typeof(View);

    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);

        Border = "#000000";
        BorderThickness = 2;
    }
}