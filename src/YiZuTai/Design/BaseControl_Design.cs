using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Design
{
    public partial class BaseControl
    {
        //让控件左边距对齐到网格
        public void SetLeftToGrid(Canvas canvas, double newLeft)
        {
            newLeft += canvas.DesignGridSize / 2;
            if (newLeft >= 0)
                newLeft -= newLeft % canvas.DesignGridSize;
            else
                newLeft += newLeft % canvas.DesignGridSize;

            Left = newLeft;
        }
        //让控件上边距对齐到网格
        public void SetTopToGrid(Canvas canvas, double newTop)
        {
            newTop += canvas.DesignGridSize / 2;
            if (newTop >= 0)
                newTop -= newTop % canvas.DesignGridSize;
            else
                newTop += newTop % canvas.DesignGridSize;

            Top = newTop;
        }
        //让控件宽度对齐到网格
        public void SetWidthToGrid(Canvas canvas, double newWidth)
        {
            newWidth -= newWidth % canvas.DesignGridSize;
            if (newWidth <= 0)
                newWidth = canvas.DesignGridSize;

            Width = newWidth;
        }

        //让控件高度对齐到网格
        public void SetHeightToGrid(Canvas canvas, double newHeight)
        {
            newHeight -= newHeight % canvas.DesignGridSize;
            if (newHeight <= 0)
                newHeight = canvas.DesignGridSize;
            Height = newHeight;
        }
    }
}
