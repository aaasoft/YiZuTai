using Microsoft.AspNetCore.Components;
namespace YiZuTai.Control.Image
{
    public partial class View
    {
        [Parameter]
        public string ZTFileId { get; set; }
        [Parameter]
        public Model Model { get; set; }
    }
}
