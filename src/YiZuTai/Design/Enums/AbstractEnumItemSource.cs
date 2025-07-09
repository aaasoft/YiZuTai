using System.ComponentModel;
using System.Reflection;

namespace YiZuTai.Design.Enums
{
    public abstract class AbstractEnumItemSource<T>// : IItemsSource
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
        //private static ItemCollection collection = new ItemCollection();

        static AbstractEnumItemSource()
        {
            var type = typeof(T);
            foreach (var name in System.Enum.GetNames(type))
            {
                MemberInfo itemMemberInfo = type.GetMember(name)[0];
                var value = itemMemberInfo.GetCustomAttribute<DefaultValueAttribute>()?.Value?.ToString() ?? name;
                //collection.Add(name, value);
                dict[name] = value;
            }
        }

        public static string GetDisplayName(string name)
        {
            if (dict.ContainsKey(name))
                return dict[name];
            return name;
        }

        //public ItemCollection GetValues() => collection;
    }
}
