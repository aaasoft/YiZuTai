using YiZuTai.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiZuTai.Source.DataSource
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
        public virtual string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        [Browsable(false)]
        [JsonIgnore]
        public virtual string Parameter { get => ToJson(); }

        public AbstractDataSource()
        {
            Attribute = SourceManager.Instance.GetItem(this.GetType().FullName);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
