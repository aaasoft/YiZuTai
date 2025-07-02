using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Control.Circle
{
    public partial class View
    {
        [Parameter]
        public Model Model { get; set; }
        public double cx => Model.Width / 2;
        public double cy => Model.Height / 2;
        public double r
        {
            get
            {
                double tmpR = 0;
                var rx = Model.Width / 2 - Model.BorderThickness;
                var ry = Model.Height / 2 - Model.BorderThickness;
                if (rx > ry)
                    tmpR = ry;
                else
                    tmpR = rx;
                return tmpR;
            }
        }
    }
}
