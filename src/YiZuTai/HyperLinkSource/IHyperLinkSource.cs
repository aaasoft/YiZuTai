using YiZuTai.Core.Source;

namespace YiZuTai.HyperLinkSource
{
    public interface IHyperLinkSource : ISource
    {
        void Execute(Core.ZTContext context);
    }
}
