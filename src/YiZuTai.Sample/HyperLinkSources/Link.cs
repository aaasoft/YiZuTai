using System.ComponentModel;
using System.Text;
using Microsoft.JSInterop;
using YiZuTai.HyperLinkSource;
using YiZuTai.Model;
using YiZuTai.Sample.Enums;

namespace YiZuTai.Sample.HyperLinkSources
{
    [Category("基础")]
    [DisplayName("跳转链接")]
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
                RaisePropertyChanged(nameof(Url));
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
                RaisePropertyChanged(nameof(Target));
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
                RaisePropertyChanged(nameof(WindowTitle));
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
                RaisePropertyChanged(nameof(WindowWidth));
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
                RaisePropertyChanged(nameof(WindowHeight));
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
                    var sb = new StringBuilder();
                    sb.AppendLine("new YiZuTai.CreateWindow({");
                    if (WindowWidth.HasValue)
                        sb.AppendLine($"width: {WindowWidth},");
                    if (WindowHeight.HasValue)
                        sb.AppendLine($"height: {WindowHeight},");
                    sb.AppendLine(@$"pages: [{{
        title: '{WindowTitle}',
        url: '{Url}'
    }}]
}}).show();");
                    context.JSRuntime.InvokeVoidAsync("eval", sb.ToString());
                    break;
            }
        }
    }
}
