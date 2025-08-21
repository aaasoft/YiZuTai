using Newtonsoft.Json;
using System.ComponentModel;
using YiZuTai.Core;
using YiZuTai.Core.Source;
using YiZuTai.Model;

namespace YiZuTai.HyperLinkSource
{
    public abstract class AbstractHyperLinkSource : AbstractSource, IHyperLinkSource
    {
        [Browsable(false)]
        [JsonIgnore]
        public SourceAttribute Attribute { get; private set; }
        [Browsable(false)]
        [JsonIgnore]
        public override string Category { get { return Attribute.Category; } }
        [Browsable(false)]
        [JsonIgnore]
        public override string Name { get { return Attribute.Name; } }

        [Browsable(false)]
        public string Clazz { get { return Attribute.Clazz; } }

        [Browsable(false)]
        [JsonIgnore]
        public virtual string Parameter { get => JsonConvert.SerializeObject(this); }

        public AbstractHyperLinkSource()
        {
            Attribute = SourceManager.Instance.GetItem(this.GetType().FullName);
        }

        public abstract void Execute(ZTContext context);
    }
}
