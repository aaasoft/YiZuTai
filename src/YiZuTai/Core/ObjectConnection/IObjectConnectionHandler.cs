using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Core.ObjectConnection
{
    public interface IObjectConnectionHandler
    {
        Type Type { get; }
        void PreviewPropertyChanged(object obj, string propertyName);
        void OldValueHandle(object obj);
        void NewValueHandle(object obj);
    }
}
