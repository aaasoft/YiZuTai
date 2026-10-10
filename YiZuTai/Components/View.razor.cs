using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core.ObjectConnection;
using YiZuTai.Enums;
using Microsoft.JSInterop;
using System.Web;
using Quick.Xml;
using YiZuTai.Model;

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
            while (!cancellationToken.IsCancellationRequested)
            {   
                await Task.Delay(timeout, cancellationToken);
                try
                {
                    await JSRuntime.InvokeAsync<long>("eval", timeout, "new Date().getTime();");
                }
                catch
                {
                    Dispose();
                    break;
                }
            }
        }

        public void Dispose()
        {
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
