using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.ObjectConnection
{
    public class ObjectConnectionUtils
    {

        public static bool IsPropertyOnBuisses(object obj, string propertyName)
        {
            return GetObjectProperties(obj).Any(t => t.Name == propertyName);
        }

        public static PropertyInfo[] GetObjectProperties(object obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(t =>
                {
                    if (!t.CanRead || !t.CanWrite)
                        return false;
                    if (t.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                        return false;
                    return true;
                })
                .ToArray();
        }
    }
}
