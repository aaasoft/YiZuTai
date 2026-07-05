using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace YiZuTai.Components.Editors
{
    public partial class EditorInputSelect
    {
        private Dictionary<string, string> options = new Dictionary<string, string>();
        private bool IsEnum = false;

        public string StringValue
        {
            get { return Field.Value.ToString(); }
            set
            {
                if (IsEnum)
                    Field.Value = Enum.Parse(Field.PropertyInfo.PropertyType, value);
                else
                    Field.Value = value;
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Type enumType = null;
            //如果类型是枚举
            if (Field.PropertyInfo.PropertyType.IsEnum)
            {
                IsEnum = true;
                enumType = Field.PropertyInfo.PropertyType;
            }
            else
            {
                var enumDataTypeAttribute = Field.PropertyInfo.GetCustomAttribute<EnumDataTypeAttribute>();
                if (enumDataTypeAttribute == null)
                    throw new ArgumentNullException("Need EnumDataTypeAttribute.");
                enumType = enumDataTypeAttribute.EnumType;
            }
            var array = Enum.GetValues(enumType);
            foreach (var item in array)
            {
                var displayName = item.ToString();
                var descriptionAttribute = enumType.GetMember(item.ToString())[0].GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                    displayName = descriptionAttribute.Description;
                options[item.ToString()] = displayName;
            }
        }
    }
}
