namespace YiZuTai.Sample.Controls.Charts.Common
{
    public class Consts
    {
        public const string FormatDescription = @"格式字符串可以使用变量，变量使用大括号括起来，支持以下变量：
{series.name}： 数据列名称
{point.name}: 数据点的名称
{point.y}: 数据点的值
{point.percentage:.1f}: 数据点所占百分比,冒号后面的.1f代表保留1位小数

格式还可以使用部分HTML标签，支持的HTML标签如下：
<b> : 加粗
<i> : 倾斜
<em> : 强调
<strong> : 特别强调
<br/> : 换行
<span> : 行内元素";
    }
}
