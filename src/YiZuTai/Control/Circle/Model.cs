using System.ComponentModel;
using YiZuTai.Design;

namespace YiZuTai.Control.Circle
{
    [Category("图形")]
    [DisplayName("圆形")]
    public class Model : BaseControl_WithBackground
    {
        public Model()
        {
            Border = "#000000";
            BorderThickness = 2;
            Width = Height = 60;
        }
    }
}
