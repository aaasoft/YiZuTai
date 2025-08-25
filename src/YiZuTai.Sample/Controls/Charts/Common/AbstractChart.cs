using Newtonsoft.Json;
using System.ComponentModel;
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
    }
}
