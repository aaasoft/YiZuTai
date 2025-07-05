using System;
using System.Buffers.Text;
using System.Collections.Generic;
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

        //初始化扫描
        public void Init()
        {
            var assembly = this.GetType().Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsPublic
                    || !type.IsClass
                    || type.IsAbstract
                    || type.IsAssignableFrom(typeof(BaseControl)))
                    continue;
                var attr = type.GetCustomAttributes(typeof(ControlAttribute), false)
                    .Cast<ControlAttribute>()
                    .FirstOrDefault();
                if (attr == null)
                    continue;
                if (string.IsNullOrEmpty(attr.Category))
                    continue;
                attr.Type = type;
                controlAttrList.Add(attr);
            }
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
