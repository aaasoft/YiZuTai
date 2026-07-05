using System.ComponentModel;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Circle;

[Category("图形")]
[DisplayName("圆形")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);

    public Model()
    {
        Border = "#000000";
        BorderThickness = 2;
        Width = Height = 60;
    }

    public static IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut<Model>("样式-浅色", (context, t) =>
        {
            t.Border = "#000000";
        }),
        new ControlShotcut<Model>("样式-深色", (context, t) =>
        {
            t.Border = "#FFFFFF";
        })
    ];
}