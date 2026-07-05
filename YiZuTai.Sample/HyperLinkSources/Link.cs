using System.ComponentModel;
using System.Text;
using Microsoft.JSInterop;
using YiZuTai.HyperLinkSource;
using YiZuTai.Model;
using YiZuTai.Sample.Enums;

namespace YiZuTai.Sample.HyperLinkSources
{
    [Category("基础")]
    [DisplayName("访问链接")]
    public class Link : AbstractHyperLinkSource
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

        private HyperLinkTarget _Target;
        [DisplayName("目标")]
        public virtual HyperLinkTarget Target
        {
            get { return _Target; }
            set
            {
                _Target = value;
                RaisePropertyChanged();
            }
        }

        private string _WindowTitle;
        [DisplayName("窗口标题")]
        [Description("当目标设置为_windows时有效")]
        public virtual string WindowTitle
        {
            get { return _WindowTitle; }
            set
            {
                _WindowTitle = value;
                RaisePropertyChanged();
            }
        }

        private double? _WindowWidth;
        [DisplayName("窗口宽度")]
        [Description("值为0到1之间时代表比例，大于1代表像素")]
        public virtual double? WindowWidth
        {
            get { return _WindowWidth; }
            set
            {
                _WindowWidth = value;
                RaisePropertyChanged();
            }
        }

        private double? _WindowHeight;
        [DisplayName("窗口高度")]
        [Description("值为0到1之间时代表比例，大于1代表像素")]
        public virtual double? WindowHeight
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
            switch (Target)
            {
                case HyperLinkTarget._blank:
                    context.JSRuntime.InvokeVoidAsync("eval", $"window.open('{Url}', '_blank', 'noreferrer')");
                    break;
                case HyperLinkTarget._self:
                    context.JSRuntime.InvokeVoidAsync("eval", $"window.location.href = '{Url}'");
                    break;
                case HyperLinkTarget._parent:
                    context.JSRuntime.InvokeVoidAsync("eval", $"parent.location.href = '{Url}'");
                    break;
                case HyperLinkTarget._top:
                    context.JSRuntime.InvokeVoidAsync("eval", $"top.location.href = '{Url}'");
                    break;
                case HyperLinkTarget._window:
                    this.OpenLayerWindow(context);
                    break;
            }
        }
    }

    public static class LinkExtensions
    {
        public static void OpenLayerWindow(this Link link, ZTContext context)
        {
            var sb = new StringBuilder();
            sb.AppendLine("YiZuTai.openWindow({");
            sb.AppendLine($"maxmin: {link.MaxMin.ToString().ToLower()},");
            if (link.WindowWidth.HasValue)
                sb.AppendLine($"width: {link.WindowWidth},");
            if (link.WindowHeight.HasValue)
                sb.AppendLine($"height: {link.WindowHeight},");
            sb.AppendLine(@$"pages: [{{
        title: '{link.WindowTitle}',
        url: '{link.Url}'
    }}]
}});");
            context.JSRuntime.InvokeVoidAsync("eval", sb.ToString());
        }
    }
}
