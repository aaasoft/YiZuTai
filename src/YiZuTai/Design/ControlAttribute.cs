using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace YiZuTai.Design
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ControlAttribute : Attribute
    {
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visiable { get; set; } = true;
        private string _Name;

        /// <summary>
        /// 是否过时
        /// </summary>
        public bool Obsolete
        {
            get
            {
                if (Type == null)
                    return false;
                var attr = Type.GetCustomAttribute<ObsoleteAttribute>();
                return attr != null;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                if (Obsolete)
                    return $"[已过时]{_Name}";
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        /// <summary>
        /// 分组
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 类名称
        /// </summary>
        public string Clazz { get { return Type?.FullName; } }
        
        /// <summary>
        /// 设计时拉伸方式
        /// </summary>
        public Stretch Stretch { get; set; } = Stretch.Fill;

        /// <summary>
        /// 配置类型
        /// </summary>
        public Type ConfigType { get; set; }

        /// <summary>
        /// 创建一个控件实例
        /// </summary>
        /// <returns></returns>
        public BaseControl CreateInstance()
        {
            var item = Activator.CreateInstance(Type) as BaseControl;
            return item;
        }
    }
}
