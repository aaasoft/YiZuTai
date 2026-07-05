using System.ComponentModel;

namespace YiZuTai.Core.Source
{
    public interface ISource : INotifyPropertyChanged
    {
        /// <summary>
        /// 分类
        /// </summary>
        string Category { get; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
