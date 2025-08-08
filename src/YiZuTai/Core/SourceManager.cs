using System.ComponentModel;
using System.Reflection;
using YiZuTai.Source;

namespace YiZuTai.Core
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

        //初始化
        public void Init()
        {
        }

        public void RegisterSource<T>()
        {
            var type = typeof(T);
            var category = type.GetCustomAttribute<CategoryAttribute>()?.Category ?? "未分组";
            var name = type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? type.Name;
            var attr = new SourceAttribute()
            {
                Category = category,
                Name = name,
                Type = type
            };
            itemList.Add(attr);
        }
    }
}
