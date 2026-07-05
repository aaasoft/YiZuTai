using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Utils
{
    public class ColorUtils
    {
        private static ColorConverter colorConverter = new ColorConverter();
        public static Color ParseColor(string text)
        {
            return (Color)colorConverter.ConvertFromString(text);
        }

        public static double GetAlphaValueIn0To1(Color color)
        {
            return color.A * 1D / 256;
        }

        public static string GetRGBString(Color color)
        {
            return $"#{color.R.ToString("X2")}{color.G.ToString("X2")}{color.B.ToString("X2")}";
        }

        public static string GetRGBA_Func_String(Color color)
        {
            return $"rgba({color.R}, {color.G}, {color.B}, {color.A})";
        }
    }
}
