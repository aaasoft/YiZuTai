using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core.ObjectConnection;
using YiZuTai.Enums;
using Microsoft.JSInterop;
using System.Web;
using Quick.Xml;
using YiZuTai.Model;
using System.Threading;

namespace YiZuTai.Components
{
    public partial class View : IDisposable
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public ZTFile ZTFile { get; set; }
        public Project Project { get; set; }

        private ModalWindow modalWindow;

        private ZTContext ZTContext;
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private string preserveAspectRatio
        {
            get
            {
                switch (Project.Stretch)
                {
                    case Stretch.Uniform:
                        return "xMidYMin";
                    case Stretch.Fill:
                        return "none";
                    case Stretch.UniformToFill:
                        return "xMinYMin slice";
                    case Stretch.None:
                    default:
                        return "none";
                }
            }
        }

        protected override void OnParametersSet()
        {
            if (ZTFile != null)
                return;
            ZTFile = Core.ZTFileManager.Instance.Get(Id);
            if (ZTFile == null)
                return;
            var query = HttpUtility.ParseQueryString(new Uri(NavigationManager.Uri).Query);
            var designerId = query.Get("DesignerId");
            if (!string.IsNullOrEmpty(designerId))
            {
                var srcProject = Designer.GetDesigner(designerId)?.Project;
                if (srcProject == null)
                    return;
                Project = (Project)XmlConvert.Deserialize(XmlConvert.Serialize(srcProject));
            }
            else
            {
                Project = ZTFile.OpenProject();
            }
            ZTContext = new ZTContext()
            {
                ZTFile = ZTFile,
                Project = Project,
                JSRuntime = JSRuntime
            };
        }

        private CancellationTokenSource checkPagedIsClosedCTS;
        private int isDisposed;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (ZTFile == null)
                    return;
                ObjectConnectionManager.Instance.AddObject(Project);
                Project.OnLoad();
                ZTContext.ModalWindow = modalWindow;
                checkPagedIsClosedCTS = new CancellationTokenSource();
                _ = beginCheckPageIsClosed(checkPagedIsClosedCTS.Token);
            }
        }

        //每隔一段时间检查一下页面是否关闭
        private async Task beginCheckPageIsClosed(CancellationToken cancellationToken)
        {
            var timeout = TimeSpan.FromSeconds(5);
            //连续多次探测失败才判定页面已关闭，避免 SignalR 重连瞬时抖动造成误释放
            const int failThreshold = 3;
            var failCount = 0;
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await JSRuntime.InvokeAsync<long>("eval", timeout, "new Date().getTime();");
                        failCount = 0;
                        try
                        {
                            await Task.Delay(timeout, cancellationToken);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                    }
                    catch
                    {
                        if (++failCount >= failThreshold)
                        {
                            Dispose();
                            return;
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //令牌已取消，安静退出
            }
        }

        public void Dispose()
        {
            //线程安全：框架正常 Dispose 与轮询线程 Dispose 可能并发，用 Interlocked 防止重复释放
            if (Interlocked.Exchange(ref isDisposed, 1) == 1)
                return;
            if (ZTFile == null)
                return;
            if (Project != null)
            {
                ObjectConnectionManager.Instance.RemoveObject(Project);
                Project.OnUnload();
                Project = null;
            }
            checkPagedIsClosedCTS?.Dispose();
            checkPagedIsClosedCTS = null;
        }
    }
}
