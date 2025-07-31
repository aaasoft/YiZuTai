using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SourceAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 是否在弹出窗口显示属性
        /// </summary>
        public bool IsShowPropertiesInWindow { get; set; } = true;

        public string Clazz { get { return Type?.FullName; } }
        /// <summary>
        /// 来源类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 配置类型
        /// </summary>
        public Type ConfigType { get; set; }
        /// <summary>
        /// JS文件
        /// </summary>
        public string Js { get => $"{Type.FullName}.js;{Type.Assembly.GetName().Name}"; }
        /// <summary>
        /// 引用的JS文件
        /// </summary>
        public string[] RefrenceJsFiles { get; set; }

        public SourceAttribute(string name, string category)
        {
            Name = name;
            Category = category;
        }
    }
}
