using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core;
using YiZuTai.Core.ObjectConnection;
using YiZuTai.Control;
using YiZuTai.Enums;

namespace YiZuTai.Components
{
    public partial class View : IDisposable
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public Core.ZTFile Model { get; set; }

        [Parameter]
        public Project Project { get; set; }

        public Canvas Canvas => Project.Canvas;

        private ModalWindow modalWindow;

        private ZTContext ZTContext;

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

        protected override void OnParametersSet()
        {
            if (Model == null)
                Model = Core.ZTFileManager.Instance.Get(Id);
            if (Project == null)
                Project = Model.OpenProject();

            ObjectConnectionManager.Instance.AddObject(Project);
            ZTContext = new ZTContext()
            {
                ZTFile = Model,
                Project = Project,
                DesignMode = false
            };
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                ZTContext.ModalWindow = modalWindow;
            }
        }

        public void Dispose()
        {
            ObjectConnectionManager.Instance.RemoveObject(Project);
        }
    }
}
