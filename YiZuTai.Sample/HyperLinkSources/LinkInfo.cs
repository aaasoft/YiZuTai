using System.ComponentModel;
using Quick.Blazor.Bootstrap;

namespace YiZuTai.Sample.HyperLinkSources
{
    public class LinkInfo : PropertyNotifyModel
    {
        private string _Url;
        [DisplayName("URL")]
        [Description("要链接的URL")]
        public virtual string Url
        {
            get { return _Url; }
            set
            {
                _Url = value;
                RaisePropertyChanged();
            }
        }

        private string _WindowTitle;
        [DisplayName("标题")]
        public virtual string WindowTitle
        {
            get { return _WindowTitle; }
            set
            {
                _WindowTitle = value;
                RaisePropertyChanged();
            }
        }
        public override string ToString()
        {
            return $"{WindowTitle} [{Url}]";
        }
    }
}
