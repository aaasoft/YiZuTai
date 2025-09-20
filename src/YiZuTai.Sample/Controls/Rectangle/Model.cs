using System.ComponentModel;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Rectangle;

[Category("图形")]
[DisplayName("矩形")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);

    public Model()
    {
        Border = "#000000";
        BorderThickness = 2;
    }

    public static IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut<Model>("样式-浅色", t =>
        {
            t.Border = "#000000";
        }),
        new ControlShotcut<Model>("样式-深色", t =>
        {
            t.Border = "#FFFFFF";
        })
    ];
}