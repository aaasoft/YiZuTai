using Microsoft.AspNetCore.Components;
using YiZuTai.Design;

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
            {
                Project = Model.OpenProject();
                ObjectConnection.ObjectConnectionManager.Instance.AddObject(Project);
            }
        }
        
        public void Dispose()
        {
            ObjectConnection.ObjectConnectionManager.Instance.RemoveObject(Project);
        }
    }
}
