using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace YiZuTai.Components.Editors
{
    public partial class EditorInputRange
    {
        private double MinValue = 0;
        private double MaxValue = 100;
        private double Step = 1;
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            var rangeAttribute = Field.PropertyInfo.GetCustomAttribute<RangeAttribute>();
            if (rangeAttribute == null)
                return;
            MinValue = Convert.ToDouble(rangeAttribute.Minimum);
            MaxValue = Convert.ToDouble(rangeAttribute.Maximum);
            var stringLengthAttribute = Field.PropertyInfo.GetCustomAttribute<StringLengthAttribute>();
            if (stringLengthAttribute == null)
                return;
            var length = stringLengthAttribute.MaximumLength;
            Step = (MaxValue - MinValue) / length;
        }
    }
}
