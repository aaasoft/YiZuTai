using System.ComponentModel;
using YiZuTai.Design.Editor;
using YiZuTai.Source;
using YiZuTai.Source.DataSource;

namespace YiZuTai.Sample.DataSources
{
    [Category("基础")]
    [DisplayName("输入数据")]
    public class Input : AbstractDataSource
    {
        [DisplayName("输入")]
        [Description("输入的内容")]
        [Editor(typeof(MultilineTextBoxEditor), typeof(MultilineTextBoxEditor))]
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
