using System.ComponentModel;
using YiZuTai.Source;
using YiZuTai.Source.DataSource;

namespace YiZuTai.DataSource
{
    [Source("输入数据", "基础", IsShowPropertiesInWindow = false)]
    public class Input : AbstractDataSource
    {
        [DisplayName("输入")]
        [Description("输入的内容")]
        //[Editor(typeof(MultilineTextBoxEditor), typeof(MultilineTextBoxEditor))]
        public string Content
        {
            get => base.Value;
            set
            {
                base.Value = value;
                RaisePropertyChanged(nameof(Content));
            }
        }
    }
}
