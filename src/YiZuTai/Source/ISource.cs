using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source
{
    public interface ISource : INotifyPropertyChanged
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
