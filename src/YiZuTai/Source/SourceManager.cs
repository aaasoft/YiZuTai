using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source
{
    public class SourceManager
    {
        public static SourceManager Instance { get; } = new SourceManager();
        private SourceManager() { }
        private List<SourceAttribute> itemList = new List<SourceAttribute>();

        public SourceAttribute GetItem(string clazz)
        {
            return itemList.FirstOrDefault(t => t.Clazz == clazz);
        }

        /// <summary>
        /// 获取指定来源类型的全部来源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SourceAttribute[] GetItems<T>() where T : ISource
        {
            return GetItems(typeof(T));
        }

        /// <summary>
        /// 获取指定来源类型的全部来源
        /// </summary>
        /// <param name="sourceInterface"></param>
        /// <returns></returns>
        public SourceAttribute[] GetItems(Type sourceInterface)
        {
            return itemList.Where(t => sourceInterface.IsAssignableFrom(t.Type)).ToArray();
        }

        //获取全部配置类型
        public Type[] GetConfigTypes()
        {
            return itemList
                .Select(t => t.ConfigType)
                .Where(t => t != null)
                .Distinct()
                .ToArray();
        }

        //初始化扫描
        public void Init(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsPublic
                    || !type.IsClass
                    || type.IsAbstract
                    || type.IsAssignableFrom(typeof(ISource)))
                    continue;
                var attr = type.GetCustomAttributes(typeof(SourceAttribute), false)
                    .Cast<SourceAttribute>()
                    .FirstOrDefault();
                if (attr == null)
                    continue;
                attr.Type = type;
                itemList.Add(attr);
            }
        }
    }
}
