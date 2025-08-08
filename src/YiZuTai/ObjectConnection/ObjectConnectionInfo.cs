using System.Collections.Specialized;
using System.ComponentModel;
using Quick.Blazor.Bootstrap;

namespace YiZuTai.ObjectConnection
{
    public class ObjectConnectionInfo
    {
        public ObjectConnectionManager Manager { get; set; }
        public object Object { get; set; }
        public PropertyNotifyModel NotifyObject { get; set; }
        public string NotifyPropertyName { get; set; }

        public void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var t in e.NewItems)
                        Manager.AddObject(t, NotifyObject, NotifyPropertyName);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var t in e.OldItems)
                        Manager.RemoveObject(t);
                    break;
            }
        }

        public void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e)
        {
            var item = (PropertyNotifyModel)Object;
            //如果属性不相关
            if (!ObjectConnectionUtils.IsPropertyOnBuisses(sender, e.PropertyName))
                return;
            //取出属性的值
            var propertyObj = sender.GetType().GetProperty(e.PropertyName).GetValue(sender);
            if (propertyObj == null)
                return;
            //触发处理器旧值处理
            foreach (var handler in Manager.GetHandler(propertyObj.GetType()))
                handler.OldValueHandle(propertyObj);
            //移除对象连接
            Manager.RemoveObject(propertyObj);
        }

        public void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            var item = (PropertyNotifyModel)Object;

            //获取属性值处理器
            var handlers = Manager.GetHandler(sender.GetType());
            //触发处理器预览新值处理
            foreach (var handler in handlers)
                handler.PreviewPropertyChanged(sender, e.PropertyName);

            //如果属性不相关
            if (!ObjectConnectionUtils.IsPropertyOnBuisses(sender, e.PropertyName))
                return;

            //取出属性的值
            var propertyObj = sender.GetType().GetProperty(e.PropertyName).GetValue(sender);
            if (propertyObj == null)
                return;
            handlers = Manager.GetHandler(propertyObj.GetType());
            //触发处理器新值处理
            foreach (var handler in handlers)
                handler.NewValueHandle(propertyObj);
            //通知属性变化
            NotifyObject?.RaisePropertyChanged(NotifyPropertyName);
            //添加对象连接
            Manager.AddObject(propertyObj, item, e.PropertyName);
        }
    }
}
