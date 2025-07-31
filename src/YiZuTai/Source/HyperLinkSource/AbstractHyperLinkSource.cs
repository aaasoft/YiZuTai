using YiZuTai.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source.HyperLinkSource
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
    }
}
