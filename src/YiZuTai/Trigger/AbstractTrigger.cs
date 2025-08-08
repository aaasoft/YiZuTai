using System.ComponentModel;
using System.Reflection;
using Quick.Blazor.Bootstrap;
using YiZuTai.Control;
using YiZuTai.Enums;

namespace YiZuTai.Trigger
{
    /// <summary>
    /// 值触发器
    /// </summary>
    public abstract class AbstractTrigger : PropertyNotifyModel
    {
        private Operators _Operater = Operators.EqualTo;
        [Category("触发条件")]
        [DisplayName("运算符")]
        [Description(@"运算符，实时值在运算符左侧，设定值在运算符右侧。
其中大于、大于等于、小于、小于等于是数字判断，其他操作符是字符串判断。")]
        public Operators Operater
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
                case Operators.EqualTo:
                    return Value == value;
                case Operators.NotEqualTo:
                    return Value != value;
                case Operators.Contains:
                    return value != null && value.Contains(Value);
                case Operators.NotContains:
                    return value != null && !value.Contains(Value);
            }
            try
            {
                double currentValue = double.Parse(value);
                double setValue = double.Parse(Value);
                switch (Operater)
                {
                    case Operators.GreaterThan:
                        return currentValue > setValue;
                    case Operators.GreaterThanOrEqualTo:
                        return currentValue >= setValue;
                    case Operators.LessThan:
                        return currentValue < setValue;
                    case Operators.LessThanOrEqualTo:
                        return currentValue <= setValue;
                }
            }
            catch { }
            return false;
        }

        public abstract void Execute(BaseControl controlModel);

        public override string ToString()
        {
            var displayName = typeof(Operators).GetMember(Operater.ToString())[0].GetCustomAttribute<DescriptionAttribute>();
            return $"触发器: {displayName.Description} {Value}";
        }
    }
}
