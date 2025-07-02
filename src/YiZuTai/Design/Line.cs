using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Design
{
    public class Line
    {
        public double k { get; private set; }
        public double b { get; private set; }

        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public Line(Point p1, Point p2)
        {
            // 设直线方程式为：y = kx + b
            k = (p1.Y - p2.Y) / (p1.X - p2.X);
            b = p1.Y - p1.X * (p1.Y - p2.Y) / (p1.X - p2.X);

            // 设直线方程式为：Ax + By + C = 0，由kx - y + b = 0得：
            A = k;
            B = -1;
            C = b;
        }

        /// <summary>
        /// 计算点到当前线的矢量
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public double ComputePointToLineVector(Point p)
        {
            return (A * p.X + B * p.Y + C) / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2));
        }

        public override string ToString()
        {
            return $"y = {k}x + {b}  或者  {A}x + {B}y + {C} = 0";
        }
    }
}
