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
    }
}
