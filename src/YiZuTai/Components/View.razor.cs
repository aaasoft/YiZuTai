using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core;
using YiZuTai.Core.ObjectConnection;
using YiZuTai.Control;
using YiZuTai.Enums;
using Microsoft.JSInterop;
using System.Web;
using Quick.Xml;

namespace YiZuTai.Components
{
    public partial class View : IDisposable
    {
        [Inject]
        private NavigationManager NavigationManager{ get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public Core.ZTFile Model { get; set; }

        [Parameter]
        public Project Project { get; set; }

        public Canvas Canvas => Project.Canvas;

        private ModalWindow modalWindow;

        private ZTContext ZTContext;
        [Inject]
        private IJSRuntime JSRuntime{ get; set; }

        private string preserveAspectRatio
        {
            get
            {
                switch (Project.Canvas.Stretch)
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

        private string GetControlALinkStyle(BaseControl baseControl) => baseControl.HyperLinkSource == null ? "" : "cursor: pointer;";
        private void OnControlClick(BaseControl baseControl)
        {
            baseControl.HyperLinkSource?.Execute(ZTContext);
        }

        protected override void OnParametersSet()
        {
            if (Model == null)
                Model = Core.ZTFileManager.Instance.Get(Id);
            var query = HttpUtility.ParseQueryString(new Uri(NavigationManager.Uri).Query);
            var designerId = query.Get("DesignerId");
            if (!string.IsNullOrEmpty(designerId))
            {
                var srcProject = Designer.GetDesigner(designerId)?.Project;
                if (srcProject == null)
                    return;                
                Project = (Project)XmlConvert.Deserialize(XmlConvert.Serialize(srcProject));                
            }
            if (Project == null)
                Project = Model.OpenProject();

            ObjectConnectionManager.Instance.AddObject(Project);
            ZTContext = new ZTContext()
            {
                ZTFile = Model,
                Project = Project,
                DesignMode = false,
                JSRuntime = JSRuntime
            };
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (ZTContext != null)
                    ZTContext.ModalWindow = modalWindow;
            }
        }

        public void Dispose()
        {
            ObjectConnectionManager.Instance.RemoveObject(Project);
        }
    }
}
