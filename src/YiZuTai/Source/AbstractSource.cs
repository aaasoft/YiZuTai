using Quick.Blazor.Bootstrap;

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
