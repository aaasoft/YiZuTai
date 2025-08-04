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

        //初始化
        public void Init()
        {
        }

        public void RegisterSource<T>()
        {
            var type = typeof(T);
            var attr = type.GetCustomAttributes(typeof(SourceAttribute), false)
                    .Cast<SourceAttribute>()
                    .FirstOrDefault();
            if (attr == null)
                throw new NullReferenceException($"类型[{type.FullName}]未定义特性[SourceAttribute]");
            attr.Type = type;
            itemList.Add(attr);
        }

        internal void UseBuiltInSources()
        {
            RegisterSource<YiZuTai.DataSource.Input>();
            RegisterSource<YiZuTai.DataSource.Simulate>();
            RegisterSource<YiZuTai.DataSource.DateTime>();
        }
    }
}
