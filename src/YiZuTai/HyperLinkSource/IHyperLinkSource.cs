using YiZuTai.Core.Source;
using YiZuTai.Model;

namespace YiZuTai.HyperLinkSource
{
    public interface IHyperLinkSource : ISource
    {
        void Execute(ZTContext context);
    }
}
