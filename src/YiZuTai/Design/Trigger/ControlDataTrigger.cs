using System.ComponentModel;
using YiZuTai.ViewModel;

namespace YiZuTai.Design.Trigger
{
    /// <summary>
    /// 值触发器
    /// </summary>
    public abstract class ControlDataTrigger : PropertyNotifyModel
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

        public abstract void Execute(BaseControl controlModel);

        //public override string ToString() => $"{OperaterItemsSource.Instance.GetDisplayName(Operater)} {Value}";
        public override string ToString() => $"{Operater} {Value}";
    }
}
