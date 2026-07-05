using Microsoft.JSInterop;
using Quick.Blazor.Bootstrap;
using YiZuTai.Components;

namespace YiZuTai.Model;

public class ZTContext
{
    public ZTFile ZTFile { get; set; }
    public Project Project { get; set; }
    public ModalWindow ModalWindow { get; set; }
    public Designer Designer { get; set; }
    public bool DesignMode => Designer != null;
    public IJSRuntime JSRuntime { get; set; }
}