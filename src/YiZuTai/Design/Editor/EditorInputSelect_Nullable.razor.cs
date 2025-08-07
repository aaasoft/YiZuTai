using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace YiZuTai.Design.Editor
{
    public partial class EditorInputSelect_Nullable
    {
        private Dictionary<string, string> options = new Dictionary<string, string>();
        private bool IsEnum = false;
        private Type EnumType;

        public string StringValue
        {
            get
            {
                if (Field.Value == null)
                    return null;
                return Field.Value.ToString();
            }
            set
            {
                if (value == null)
                {
                    Field.Value = null;
                }
                else
                {
                    if (IsEnum)
                        Field.Value = Enum.Parse(EnumType, value);
                    else
                        Field.Value = value;
                }
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            //如果类型是枚举
            var propertyType = Field.PropertyInfo.PropertyType;
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var genericArgument1 = propertyType.GetGenericArguments()[0];
                if (genericArgument1.IsEnum)
                {
                    IsEnum = true;
                    EnumType = genericArgument1;
                }
            }
            if (!IsEnum)
            {
                var enumDataTypeAttribute = Field.PropertyInfo.GetCustomAttribute<EnumDataTypeAttribute>();
                if (enumDataTypeAttribute == null)
                    throw new ArgumentNullException("Need EnumDataTypeAttribute.");
                EnumType = enumDataTypeAttribute.EnumType;
            }
            var array = Enum.GetValues(EnumType);
            foreach (var item in array)
            {
                var displayName = item.ToString();
                var descriptionAttribute = EnumType.GetMember(item.ToString())[0].GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                    displayName = descriptionAttribute.Description;
                options[item.ToString()] = displayName;
            }
        }
    }
}
