using Microsoft.AspNetCore.Components;
namespace YiZuTai.Control.Image
{
    public partial class View
    {
        private YiZuTai.Design.BaseControl_WithBackgroundAndAnimateView baseControlView;

        [Parameter]
        public string ZTFileId { get; set; }
        [Parameter]
        public Model Model { get; set; }
    }
}
