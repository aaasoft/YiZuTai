using Newtonsoft.Json;
using System.ComponentModel;
using YiZuTai.Core;
using YiZuTai.Core.Source;

namespace YiZuTai.DataSource
{
    public abstract class AbstractDataSource : AbstractSource, IDataSource
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
        public string DataSource { get { return Attribute.Clazz; } }

        private string _Value;
        [Browsable(false)]
        [JsonIgnore]
        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                RaisePropertyChanged();
            }
        }

        public AbstractDataSource()
        {
            Attribute = SourceManager.Instance.GetItem(this.GetType().FullName);
        }
    }
}
