using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YiZuTai.Design
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

        //使用示例控件
        internal void UseBuiltInControls()
        {
            RegisterControl<Control.Circle.Model>();
            RegisterControl<Control.Rectangle.Model>();
            RegisterControl<Control.Image.Model>();
            RegisterControl<Control.TextBlock.Model>();
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
            controlAttrList.Add(attr);
        }

        public void RegisterControl<T>()
            where T : BaseControl, new()
        {
            RegisterControl(typeof(T));
        }

        public string GetControlIconBase64(ControlAttribute item)
        {
            byte[] buffer = null;
            using (var ms = new MemoryStream())
            using (var rs = item.Type.Assembly.GetManifestResourceStream(item.Type.Namespace + ".Icon.svg"))
            {
                rs.CopyTo(ms);
                buffer = ms.ToArray();
            }
            return "data:image/svg+xml;base64," + Convert.ToBase64String(buffer);
        }
    }
}
