using Quick.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Design
{
    public class ControlFieldGroup
    {
        public string Name { get; set; }
        public List<ControlField> FieldList { get; set; } = new List<ControlField>();
    }
}
