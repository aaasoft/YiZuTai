using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Core.ObjectConnection
{
    public abstract class AbstractObjectConnectionHandler<T> : IObjectConnectionHandler
    {
        public Type Type => typeof(T);
        public virtual void PreviewPropertyChanged(T t, string propertyName) { }
        public virtual void NewValueHandle(T t) { }
        public virtual void OldValueHandle(T t) { }

        public void PreviewPropertyChanged(object obj, string propertyName) => PreviewPropertyChanged((T)obj, propertyName);
        public void NewValueHandle(object obj) => NewValueHandle((T)obj);
        public void OldValueHandle(object obj) => OldValueHandle((T)obj);
    }
}
