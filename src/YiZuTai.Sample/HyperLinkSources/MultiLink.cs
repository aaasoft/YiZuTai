using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Microsoft.JSInterop;
using Quick.Blazor.Bootstrap;
using YiZuTai.HyperLinkSource;
using YiZuTai.Model;
using YiZuTai.Sample.Enums;

namespace YiZuTai.Sample.HyperLinkSources
{
    [Category("基础")]
    [DisplayName("访问多链接")]
    public class MultiLink : AbstractHyperLinkSource
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

        private ObservableCollection<LinkInfo> _LinkInfos = new ObservableCollection<LinkInfo>();
        [DisplayName("链接列表")]
        public ObservableCollection<LinkInfo> LinkInfos
        {
            get { return _LinkInfos; }
            set
            {
                _LinkInfos = value;
                RaisePropertyChanged();
            }
        }

        private int? _WindowWidth;
        [DisplayName("窗口宽度")]
        [Description("当目标设置为_windows时有效")]
        public virtual int? WindowWidth
        {
            get { return _WindowWidth; }
            set
            {
                _WindowWidth = value;
                RaisePropertyChanged();
            }
        }

        private int? _WindowHeight;
        [DisplayName("窗口高度")]
        [Description("当目标设置为_windows时有效")]
        public virtual int? WindowHeight
        {
            get { return _WindowHeight; }
            set
            {
                _WindowHeight = value;
                RaisePropertyChanged();
            }
        }

        private bool _MaxMin = true;
        [DisplayName("允许最大最小化")]
        public virtual bool MaxMin
        {
            get { return _MaxMin; }
            set
            {
                _MaxMin = value;
                RaisePropertyChanged();
            }
        }

        public override void Execute(ZTContext context)
        {
            var sb = new StringBuilder();
            sb.AppendLine("YiZuTai.openWindow({");
            sb.AppendLine($"maxmin: {MaxMin.ToString().ToLower()},");
            if (WindowWidth.HasValue)
                sb.AppendLine($"width: {WindowWidth},");
            if (WindowHeight.HasValue)
                sb.AppendLine($"height: {WindowHeight},");
            sb.AppendLine("pages: [");
            sb.AppendLine(string.Join(",", LinkInfos.Select(t => @$"{{
        title: '{t.WindowTitle}',
        url: '{t.Url}'
    }}")));
            sb.AppendLine("]});");
            context.JSRuntime.InvokeVoidAsync("eval", sb.ToString());
        }
    }
}