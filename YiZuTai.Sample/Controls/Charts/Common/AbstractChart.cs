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
    let root = document.getElementsByTagName('svg')[0];
    //原始相对于图表控件的鼠标位置
    let chartX = e.chartX;
    let chartY = e.chartY;

    //新的相对于图表控件的鼠标位置
    let newX, newY;

    //坐标系拉伸变换
    let preWidth = {project.Width};
    let preHeight = {project.Height};

    let currentWidth = root.width.baseVal.value;
    let currentHeight = root.height.baseVal.value;");

            if (project.Stretch == YiZuTai.Enums.Stretch.Uniform)
            {
                sb.AppendLine($@"
        //偏移量
    let scaleX = currentWidth / preWidth;
    let scaleY = currentHeight / preHeight;
    let scale = Math.min(scaleX, scaleY);
    currentWidth = preWidth * scale;
    currentHeight = preHeight * scale;
");
            }
            sb.AppendLine($@"
    chartX = chartX * preWidth / currentWidth;
    chartY = chartY * preHeight / currentHeight;

    let leftTopPointLocationX = {LeftTopPointLocation.X};
    let leftTopPointLocationY = {LeftTopPointLocation.Y};

    //坐标系偏移变换
    let mX = chartX + Math.min(leftTopPointLocationX, {RightTopPointLocation.X}, {RightBottomPointLocation.X}, {LeftBottomPointLocation.X});
    let mY = chartY + Math.min(leftTopPointLocationY, {RightTopPointLocation.Y}, {RightBottomPointLocation.Y}, {LeftBottomPointLocation.Y});

    mX -= leftTopPointLocationX;
    mY -= leftTopPointLocationY;

    //坐标系旋转变换
    let radian = {Radian};
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
