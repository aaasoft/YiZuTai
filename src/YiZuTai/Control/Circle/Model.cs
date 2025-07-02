using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiZuTai.Design;

namespace YiZuTai.Control.Circle
{
    [Control(
        Category = "图形",
        Name = "圆形"
        )]
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
