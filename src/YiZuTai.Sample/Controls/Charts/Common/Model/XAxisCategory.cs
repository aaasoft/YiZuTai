using Quick.Blazor.Bootstrap;
using System.ComponentModel;

namespace YiZuTai.Sample.Controls.Charts.Common.Model
{
    [DisplayName("X轴分组")]
    public class XAxisCategory : PropertyNotifyModel
    {
        private string _Name;
        [DisplayName("名称")]
        [Description("分组的名称")]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
