using YiZuTai.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source
{
    public abstract class AbstractSource : PropertyNotifyModel, ISource
    {
        public abstract string Category { get; }
        public abstract string Name { get; }

        public override string ToString()
        {
            return $"{Category} - {Name}";
        }
    }
}
