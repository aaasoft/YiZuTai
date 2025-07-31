using Newtonsoft.Json;
using System.ComponentModel;

namespace YiZuTai.Design.Trigger
{
    /// <summary>
    /// 值触发器
    /// </summary>
    public class ControlDataTrigger : PropertyNotifyModel
    {
        private string _Operater = "==";
        [Category("触发条件")]
        [DisplayName("运算符")]
        [Description(@"运算符，实时值在运算符左侧，设定值在运算符右侧。
其中大于、大于等于、小于、小于等于是数字判断，其他操作符是字符串判断。")]
        //[ItemsSource(typeof(OperaterItemsSource))]
        public string Operater
        {
            get { return _Operater; }
            set
            {
                _Operater = value;
                RaisePropertyChanged(nameof(Operater));
            }
        }

        private string _Value;
        [Category("触发条件")]
        [DisplayName("设定值")]
        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        /// <summary>
        /// 根据当前值获取值
        /// </summary>
        /// <typeparam name="TTrigger"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="triggers"></param>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetCurrentValue<TTrigger, TValue>(
            ICollection<TTrigger> triggers,
            string value,
            Func<TTrigger, TValue> func,
            TValue defaultValue)
            where TTrigger : ControlDataTrigger
        {
            if (value == null || triggers.Count == 0)
                return defaultValue;
            foreach (var trigger in triggers)
                if (trigger.IsMatchValue(value))
                    return func.Invoke(trigger);
            return defaultValue;
        }

        public bool IsMatchValue(string value)
        {
            switch (Operater)
            {
                case "==":
                    return Value == value;
                case "!=":
                    return Value != value;
                case "contains":
                    return value != null && value.Contains(Value);
                case "not contains":
                    return value != null && !value.Contains(Value);
            }
            try
            {
                double currentValue = double.Parse(value);
                double setValue = double.Parse(Value);
                switch (Operater)
                {
                    case ">":
                        return currentValue > setValue;
                    case ">=":
                        return currentValue >= setValue;
                    case "<":
                        return currentValue < setValue;
                    case "<=":
                        return currentValue <= setValue;
                }
            }
            catch { }
            return false;
        }
        public string ToJson() => JsonConvert.SerializeObject(this);
        //public override string ToString() => $"{OperaterItemsSource.Instance.GetDisplayName(Operater)} {Value}";
        public override string ToString() => $"{Operater} {Value}";
    }
}
