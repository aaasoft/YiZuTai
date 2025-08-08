using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiZuTai.Source;

namespace YiZuTai.DataSource
{
    public abstract class AbstractQueryDataSource : AbstractSource, IQueryDataSource
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
        
        private QueryDataSourceResult[] _Value;
        [Browsable(false)]
        [JsonIgnore]
        public virtual QueryDataSourceResult[] Value
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

        public AbstractQueryDataSource()
        {
            Attribute = SourceManager.Instance.GetItem(this.GetType().FullName);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
