using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Microsoft.JSInterop;
using YiZuTai.HyperLinkSource;
using YiZuTai.Model;

namespace YiZuTai.Sample.HyperLinkSources
{
    [Category("基础")]
    [DisplayName("访问多链接")]
    public class MultiLink : AbstractHyperLinkSource
    {
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
            this.OpenLayerWindow(context);
        }
    }


    public static class MultiLinkExtensions
    {
        public static void OpenLayerWindow(this MultiLink link, ZTContext context)
        {
            var sb = new StringBuilder();
            sb.AppendLine("YiZuTai.openWindow({");
            sb.AppendLine($"maxmin: {link.MaxMin.ToString().ToLower()},");
            if (link.WindowWidth.HasValue)
                sb.AppendLine($"width: {link.WindowWidth},");
            if (link.WindowHeight.HasValue)
                sb.AppendLine($"height: {link.WindowHeight},");
            sb.AppendLine("pages: [");
            sb.AppendLine(string.Join(",", link.LinkInfos.Select(t => @$"{{
        title: '{t.WindowTitle}',
        url: '{t.Url}'
    }}")));
            sb.AppendLine("]});");
            context.JSRuntime.InvokeVoidAsync("eval", sb.ToString());
        }
    }
}