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
        private Dictionary<Type, Dictionary<string, IControlShotcut>> controlShotcutDict = new Dictionary<Type, Dictionary<string, IControlShotcut>>();

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

        public void RegisterControlShotcut(params IControlShotcut[] shotcuts)
        {
            foreach (var shotcut in shotcuts)
            {
                if (!controlShotcutDict.TryGetValue(shotcut.ControlType, out var shotcutDict))
                    controlShotcutDict[shotcut.ControlType] = shotcutDict = new Dictionary<string, IControlShotcut>();
                shotcutDict[shotcut.Name] = shotcut;
            }
        }

        public IControlShotcut[] GetControlShotcuts(Type type)
        {
            if (!controlShotcutDict.TryGetValue(type, out var shotcutDict))
                return [];
            return shotcutDict.Values.ToArray();
        }

        public IControlShotcut GetControlShotcut(Type type, string shotcutName)
        {
            if (!controlShotcutDict.TryGetValue(type, out var shotcutDict))
                return null;
            if (!shotcutDict.TryGetValue(shotcutName, out var shotcut))
                return null;
            return shotcut;
        }

        public IControlShotcut[] GetControlShotcuts(BaseControl baseControl)
        {
            return GetControlShotcuts(baseControl.GetType());
        }
    }
}
