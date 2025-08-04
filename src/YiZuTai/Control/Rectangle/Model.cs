using System.ComponentModel;
using YiZuTai.Design;

namespace YiZuTai.Control.Rectangle
{
    [Category("图形")]
    [DisplayName("矩形")]
    public class Model : BaseControl_WithBackground
    {
        public Model()
        {
            Border = "#000000";
            BorderThickness = 2;
            BackgroundOpacity = 0;
        }
    }
}
