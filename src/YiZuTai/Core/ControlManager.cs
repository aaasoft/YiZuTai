using System.ComponentModel;
using System.Reflection;
using YiZuTai.Control;

namespace YiZuTai.Core
{
    public class ControlManager
    {
        public static ControlManager Instance { get; } = new ControlManager();
        private ControlManager() { }
        private List<ControlAttribute> controlAttrList = new List<ControlAttribute>();

        public ControlAttribute GetItem(string clazz)
        {
            return controlAttrList.FirstOrDefault(t => t.Clazz == clazz);
        }

        public ControlAttribute[] GetItems()
        {
            return controlAttrList.ToArray();
        }

        public void RegisterControl(Type type)
        {
            var category = type.GetCustomAttribute<CategoryAttribute>()?.Category ?? "未分组";
            var name = type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? type.Name;
            var attr = new ControlAttribute()
            {
                Category = category,
                Name = name,
                Type = type
            };
            var instance = attr.CreateInstance();
            attr.Icon = instance.GetIcon();
            controlAttrList.Add(attr);
        }

        public void RegisterControl<T>()
            where T : BaseControl, new()
        {
            RegisterControl(typeof(T));
        }
    }
}
