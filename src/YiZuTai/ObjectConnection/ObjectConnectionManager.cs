using YiZuTai.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.ObjectConnection
{
    public class ObjectConnectionManager
    {
        public static ObjectConnectionManager Instance { get; } = new ObjectConnectionManager();

        private List<IObjectConnectionHandler> handlerList = new List<IObjectConnectionHandler>();
        private Dictionary<object, ObjectConnectionInfo> dict = new Dictionary<object, ObjectConnectionInfo>();

        /// <summary>
        /// 获取对象的父关系对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ObjectConnectionInfo GetParent(object obj)
        {
            lock (dict)
            {
                if (!dict.ContainsKey(obj))
                    return null;
                var objectConnectionInfo = dict[obj];
                return objectConnectionInfo;
            }
        }

        public ObjectConnectionInfo[] FindAll<T>(object obj)
        {
            if (obj == null)
                return null;

            List<ObjectConnectionInfo> list = new List<ObjectConnectionInfo>();
            var currentObj = obj;
            while (true)
            {
                var parentInfo = GetParent(currentObj);
                list.Add(parentInfo);

                currentObj = parentInfo?.NotifyObject;
                if (currentObj == null)
                    return null;
                if (currentObj is T)
                    return list.ToArray();
            }
        }

        /// <summary>
        /// 查找指定类型的上级关系对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Find<T>(object obj)
        {
            if (obj == null)
                return default(T);

            var currentObj = obj;
            while (true)
            {
                currentObj = GetParent(currentObj)?.NotifyObject;
                if (currentObj == null)
                    return default(T);
                if (currentObj is T)
                    return (T)currentObj;
            }
        }

        public void RegisterHandler(IObjectConnectionHandler handler)
        {
            handlerList.Add(handler);
        }

        public IObjectConnectionHandler[] GetHandler(Type type)
        {
            return handlerList.Where(t => t.Type.IsAssignableFrom(type)).ToArray();
        }

        public void AddObject(object obj, PropertyNotifyModel notifyObj = null, string notifyPropertyName = null)
        {
            lock (dict)
            {
                if (obj == null)
                    return;
                if (dict.ContainsKey(obj))
                    return;

                var ObjectConnectionInfo = new ObjectConnectionInfo()
                {
                    Manager = this,
                    Object = obj,
                    NotifyObject = notifyObj,
                    NotifyPropertyName = notifyPropertyName
                };
                dict[obj] = ObjectConnectionInfo;

                //如果是对象关系树首次添加对象关系
                foreach (var handler in GetHandler(obj.GetType()))
                    handler.NewValueHandle(obj);

                if (obj is INotifyCollectionChanged)
                {
                    var item = (INotifyCollectionChanged)obj;
                    item.CollectionChanged += ObjectConnectionInfo.NotifyCollectionChangedEventHandler;

                    foreach (var t in (System.Collections.ICollection)item)
                        AddObject(t, notifyObj, notifyPropertyName);
                }
                if (obj is PropertyNotifyModel)
                {
                    var item = (PropertyNotifyModel)obj;
                    item.PropertyChanging += ObjectConnectionInfo.PropertyChangingEventHandler;
                    item.PropertyChanged += ObjectConnectionInfo.PropertyChangedEventHandler;

                    foreach (var propertyInfo in ObjectConnectionUtils.GetObjectProperties(item))
                        AddObject(propertyInfo.GetValue(item), item, propertyInfo.Name);
                }
            }
        }

        public void RemoveObject(object obj)
        {
            lock (dict)
            {
                if (obj == null)
                    return;
                if (!dict.ContainsKey(obj))
                    return;

                var objectEventHandler = dict[obj];
                dict.Remove(obj);

                if (obj is INotifyCollectionChanged)
                {
                    var item = (INotifyCollectionChanged)obj;
                    item.CollectionChanged -= objectEventHandler.NotifyCollectionChangedEventHandler;

                    foreach (var t in (System.Collections.ICollection)item)
                        RemoveObject(t);
                }
                if (obj is PropertyNotifyModel)
                {
                    var item = (PropertyNotifyModel)obj;
                    item.PropertyChanging -= objectEventHandler.PropertyChangingEventHandler;
                    item.PropertyChanged -= objectEventHandler.PropertyChangedEventHandler;

                    foreach (var propertyInfo in ObjectConnectionUtils.GetObjectProperties(item))
                        RemoveObject(propertyInfo.GetValue(item));
                }
            }
        }
    }
}
