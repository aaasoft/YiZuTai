using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Charts.Common
{
    public abstract partial class AbstractChart : BaseControl_WithBackground
    {
        [Browsable(false)]
        [JsonIgnore]
        public abstract string ChartType { get; }
        public override string[] RefrenceJsFiles =>
        [
            $"_content/{nameof(YiZuTai)}.{nameof(Sample)}/Highcharts-5.0.14/highcharts.src.js",
            $"_content/{nameof(YiZuTai)}.{nameof(Sample)}/Highcharts-5.0.14/highcharts-zh_CN.js"
        ];

        public string GetNormalizeHandlerOptions(Project project)
        {
            var sb = new StringBuilder();
            sb.AppendLine($@"function (e) {{
    //如果是单独预览控件，没有params.root对象
    if (params.root == null)
        return e;

    //原始相对于图表控件的鼠标位置
    var chartX = e.chartX;
    var chartY = e.chartY;

    //新的相对于图表控件的鼠标位置
    var newX, newY;

    //坐标系拉伸变换
    var preWidth = $Project.Canvas.Width;
    var preHeight = $Project.Canvas.Height;

    var currentWidth = params.root.width.baseVal.value;
    var currentHeight = params.root.height.baseVal.value;");

            if (project.Stretch == YiZuTai.Enums.Stretch.Uniform)
            {
                sb.AppendLine($@"
        //偏移量
    var scaleX = currentWidth / $Project.Canvas.Width;
    var scaleY = currentHeight / $Project.Canvas.Height;
    var scale = Math.min(scaleX, scaleY);
    currentWidth = $Project.Canvas.Width * scale;
    currentHeight = $Project.Canvas.Height * scale;
");
            }
            sb.AppendLine($@"
    chartX = chartX * preWidth / currentWidth;
    chartY = chartY * preHeight / currentHeight;

    //坐标系偏移变换
    var mX = chartX + Math.min($Item.LeftTopPointLocation.X, $Item.RightTopPointLocation.X, $Item.RightBottomPointLocation.X, $Item.LeftBottomPointLocation.X);
    var mY = chartY + Math.min($Item.LeftTopPointLocation.Y, $Item.RightTopPointLocation.Y, $Item.RightBottomPointLocation.Y, $Item.LeftBottomPointLocation.Y);

    mX -= $Item.LeftTopPointLocation.X;
    mY -= $Item.LeftTopPointLocation.Y;

    //坐标系旋转变换
    var radian = $Item.Radian;
    newX = mX * Math.cos(radian) + mY * Math.sin(radian);
    newY = mY * Math.cos(radian) - mX * Math.sin(radian);

    e.chartX = newX;
    e.chartY = newY;
    return e;
}}
            ");
            return sb.ToString();
        }
    }
}
